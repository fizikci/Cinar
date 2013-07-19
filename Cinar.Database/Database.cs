/*
Copyright (C) 2006  Bülent Keskin

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Reflection;
using System.Web;
using System.Linq;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Cinar.Database
{
    /// <summary>
    /// Veritabanı metadasını modelleyen sınıf.
    /// </summary>
    [Serializable]
    public class Database : IDatabase
    {
        /// <summary>
        /// Farklı veritabanı türleriyle çalışılabilmesi için veritabanı işlemleri bu interface üzerinden yapılır.
        /// </summary>
        internal IDatabaseProvider dbProvider;

        private bool noTransactions = true;
        [XmlIgnore]
        public bool NoTransactions
        {
            get { return noTransactions; }
            set { noTransactions = value; }
        }

        private bool enableSQLLog;
        [XmlIgnore]
        public bool EnableSQLLog
        {
            get { return enableSQLLog; }
            set { enableSQLLog = value; }
        }

        [XmlIgnore]
        public string Name 
        { 
            get {
                if (!string.IsNullOrEmpty(temporaryDbName))
                    return temporaryDbName;

                if (this.dbProvider != null && this.dbProvider.Connection != null)
                    return this.dbProvider.Connection.Database;
                else
                    return "";
            }
            set 
            {
                temporaryDbName = value;
            }
        }
        private string temporaryDbName = ""; // bu sadece Database.Name'i geçici olarak değiştirmek için kullanılır.

        [XmlIgnore]
        public string Host
        {
            get
            {
                return this.host;
            }
        }
        private string host;

        [XmlIgnore]
        public List<string> SQLLog
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (System.Web.HttpContext.Current.Session["sqlLog"] == null)
                        System.Web.HttpContext.Current.Session["sqlLog"] = new List<string>();
                    return (List<string>)System.Web.HttpContext.Current.Session["sqlLog"];
                }
                else
                {
                    if (!Cache.ContainsKey("sqlLog"))
                        Cache["sqlLog"] = new List<string>();
                    return (List<string>)Cache["sqlLog"];
                }
            }
        }

        private CacheProvider _cache;
        [XmlIgnore]
        public CacheProvider Cache
        {
            get
            {
                if (_cache == null)
                    _cache = new CacheProvider();
                return _cache;
            }
        }

        public void ClearSQLLog()
        {
            if (HttpContext.Current != null)
                System.Web.HttpContext.Current.Session["sqlLog"] = null;
            else
                Cache["sqlLog"] = null;
        }

        public Database(string connectionString, DatabaseProvider provider, string serializedMetadataFilePath = null, bool createDatabaseIfNotExist = true)
        {
            createProviderAndReadMetadata(connectionString, provider, serializedMetadataFilePath, createDatabaseIfNotExist);
        }

        public Database() 
        { 
        }

        public void SetCollectionParents()
        {
            if (this.Tables == null)
                return;

            this.Tables.db = this;
            foreach (Table table in this.Tables)
            {
                table.parent = this.Tables;

                table.Columns.table = table;
                foreach (Column column in table.Columns)
                    column.parent = table.Columns;

                table.Indices.table = table;
                foreach (Index index in table.Indices)
                    index.parent = table.Indices;

                table.Constraints.table = table;
                foreach (Constraint con in table.Constraints)
                    con.parent = table.Constraints;
            }
        }

        private string serializedMetadataFilePath;

        private string uniqueDatabaseName {
            get { return this.Host + this.Provider + this.Name; }
        }

        private void createProviderAndReadMetadata(string connectionString, DatabaseProvider provider,
                                                   string serializedMetadataFilePath, bool createDatabaseIfNotExist)
        {
            this.connectionString = connectionString;
            this.provider = provider;
            this.serializedMetadataFilePath = serializedMetadataFilePath;

            CreateDbProvider(createDatabaseIfNotExist);


            if (!Cache.ContainsKey(uniqueDatabaseName))
            {
                if (string.IsNullOrEmpty(serializedMetadataFilePath))
                    dbProvider.ReadDatabaseMetadata();
                else
                    readMetadataFromFile(serializedMetadataFilePath);
                Cache[uniqueDatabaseName] = this.tables;
            }
            else
                this.tables = (TableCollection)Cache[uniqueDatabaseName];
        }

        private void readMetadataFromFile(string serializedMetadataFilePath)
        {
            if (dbProvider.CreatedNow && File.Exists(serializedMetadataFilePath))
            {
                File.Delete(serializedMetadataFilePath);
                dbProvider.CreatedNow = false;
            }

            if (File.Exists(serializedMetadataFilePath))
            {
                XmlSerializer ser = new XmlSerializer(typeof(TableCollection));
                using (StreamReader sr = new StreamReader(serializedMetadataFilePath))
                {
                    this.tables = (TableCollection)ser.Deserialize(sr);
                    this.SetCollectionParents();
                }
            }
            else
            {
                dbProvider.ReadDatabaseMetadata();
                XmlSerializer ser = new XmlSerializer(typeof(TableCollection));
                using (StreamWriter sr = new StreamWriter(serializedMetadataFilePath))
                    ser.Serialize(sr, this.tables);
            }
        }

        public void GenerateUIMetadata()
        {
            foreach (Table tbl in this.Tables)
                tbl.GenerateUIMetadata();
        }

        public void CreateDbProvider(bool createDatabaseIfNotExist)
        {
            switch (this.provider)
            {
                case DatabaseProvider.PostgreSQL:
                    dbProvider = new Providers.PostgreSQLProvider(this, createDatabaseIfNotExist);
                    break;
                case DatabaseProvider.MySQL:
                    dbProvider = new Providers.MySqlProvider(this, createDatabaseIfNotExist);
                    break;
                case DatabaseProvider.SQLServer:
                    dbProvider = new Providers.SQLServerProvider(this, createDatabaseIfNotExist);
                    break;
                case DatabaseProvider.Cinar:
                    dbProvider = new Providers.CinarProvider(this, createDatabaseIfNotExist);
                    break;
                case DatabaseProvider.SQLite:
                    dbProvider = new Providers.SQLiteProvider(this, createDatabaseIfNotExist);
                    break;
                default:
                    throw new ApplicationException("It is not that much provided.");
            }
        }

        public int DefaultCommandTimeout { get; set; }

        public Database(DatabaseProvider provider, string host, string dbName, string userName, string password, int defaultCommandTimeout, string serializedMetadataFilePath = null, bool createDatabaseIfNotExist = false)
        {
            this.DefaultCommandTimeout = defaultCommandTimeout == 0 ? 100 : defaultCommandTimeout;
            SetConnectionString(provider, host, dbName, userName, password);
            createProviderAndReadMetadata(this.connectionString, provider, serializedMetadataFilePath, createDatabaseIfNotExist);
        }

        public void SetConnectionString(DatabaseProvider provider, string host, string dbName, string userName, string password)
        {
            this.host = host;
            this.provider = provider;
            switch (provider)
            {
                case DatabaseProvider.PostgreSQL:
                    this.connectionString = String.Format("Server={0};Database={1};User Id={2};Password={3};",
                                                          host, dbName, userName, password).Replace("Database=;", "");
                    break;
                case DatabaseProvider.MySQL:
                    this.connectionString = String.Format("Server={0};Database={1};Uid={2};Pwd={3};old syntax=yes;charset=utf8;default command timeout={4};",
                                                          host, dbName, userName, password, DefaultCommandTimeout).Replace("Database=;", "");
                    break;
                case DatabaseProvider.SQLServer:
                    this.connectionString = String.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};",
                                                          host, dbName, userName, password).Replace("Initial Catalog=;", "");
                    break;
                case DatabaseProvider.Cinar:
                    this.connectionString = "cinar";
                    break;
                case DatabaseProvider.SQLite:
                    this.connectionString = String.Format("Data Source={1};Version=3;{3}",
                        host, dbName, userName, string.IsNullOrWhiteSpace(password) ? "" : ("Password=" + password + ";"), DefaultCommandTimeout).Replace("Data Source=;", "");
                    break;
                default:
                    throw new ApplicationException("It is not that much provided.");
            }
        }

        public Database CreateNewInstance()
        {
            Database database = new Database();
            database.host = this.host;
            database.connectionString = this.connectionString;
            database.provider = this.provider;
            database.tables = this.tables;

            switch (provider)
            {
                case DatabaseProvider.PostgreSQL:
                    database.dbProvider = new Providers.PostgreSQLProvider(this, true);
                    break;
                case DatabaseProvider.MySQL:
                    database.dbProvider = new Providers.MySqlProvider(this, true);
                    break;
                case DatabaseProvider.SQLServer:
                    database.dbProvider = new Providers.SQLServerProvider(this, true);
                    break;
                case DatabaseProvider.SQLite:
                    database.dbProvider = new Providers.SQLiteProvider(this, true);
                    break;
                default:
                    throw new ApplicationException("It is not that much provided.");
            }

            return database;
        }

        private string getReservedWordToken(bool left) {
            switch (provider)
            {
                case DatabaseProvider.PostgreSQL:
                    return "\"";
                case DatabaseProvider.MySQL:
                    return "`";
                case DatabaseProvider.SQLServer:
                case DatabaseProvider.SQLite:
                    return left ? "[" : "]";
            }
            return "";
        }
        private string editSQLAsForProvider(string sql)
        {
            StringComparison ic = StringComparison.InvariantCultureIgnoreCase;

            sql = sql.Trim();

            //SQLParser.Tokenizer tokenizer = new SQLParser.Tokenizer(new StringReader(sql));
            //SQLParser.Token token = tokenizer.ReadNextToken();
            //StringBuilder sb = new StringBuilder();
            //while (token != null)
            //{
            //    if (token.Type == SQLParser.TokenType.Word && ((token.Value.StartsWith("[") && token.Value.EndsWith("]")) || (token.Value.StartsWith("`") && token.Value.EndsWith("`")) || (token.Value.StartsWith("\"") && token.Value.EndsWith("\""))))
            //        sb.Append(" " + getReservedWordToken(true) + token.Value.Substring(1, token.Value.Length - 2) + getReservedWordToken(false));
            //    else if (token.Value == "{")
            //    {
            //        sb.Append(" " + token.Value);
            //        token = tokenizer.ReadNextToken();
            //        sb.Append(token.Value);
            //    }
            //    else if (token.Value == "@")
            //    {
            //        sb.Append(" " + token.Value);
            //        token = tokenizer.ReadNextToken();
            //        sb.Append(token.Value);
            //    }
            //    else if (token.Value == "}")
            //        sb.Append(token.Value);
            //    else
            //        sb.Append(" " + token.Value);
            //    token = tokenizer.ReadNextToken();
            //}
            //sql = sb.ToString().Trim();

            if (provider != DatabaseProvider.SQLServer && (sql.StartsWith("select top ", ic) || sql.StartsWith("select distinct top ", ic)))
            {
                string[] parts = sql.Split(' ');
                int rowCountIndex = sql.StartsWith("select distinct top ", ic) ? 3 : 2;
                parts[rowCountIndex - 1] = "";
                string count = parts[rowCountIndex];
                parts[rowCountIndex] = "";
                sql = string.Join(" ", parts);//sql.Replace(" top " + parts[rowCountIndex], "");
                sql += " limit " + count;
            }
            
            sql = sql.Replace("[", getReservedWordToken(true)).Replace("]", getReservedWordToken(false));
            
            return sql;
        }

        private void addToSQLLog(string sql)
        {
            if (this.EnableSQLLog)
            {
                StringBuilder sbSQL = new StringBuilder(sql + "\n");
                //StackTrace st = new StackTrace(true);
                //for (int i = 0; i < st.FrameCount; i++)
                //{
                //    StackFrame sf = st.GetFrame(i);
                //    if (sf.GetFileName() != null && sf.GetFileName().Contains("Cinar.CMS\\Cinar.CMS.Library"))
                //        sbSQL.AppendFormat("{0}({1}), ", sf.GetMethod().DeclaringType.Name + "." + sf.GetMethod().Name, sf.GetFileLineNumber());
                //}
                SQLLog.Add(sql + "\n");
            }
        }

        private string connectionString;
        [XmlIgnore]
        public string ConnectionString
        {
            get { return connectionString; }
        }

        private DatabaseProvider provider;
        [XmlIgnore]
        public DatabaseProvider Provider
        {
            get { return provider; }
        }

        /// <summary>
        /// Veritabanında değişiklik yapıldıysa (alter table gibi) metadatayı tekrar baştan okumak için kullanılan metod.
        /// Tek yaptığı şey ReadMetadata() metodunu çağırmaktır.
        /// DİKKAT: bütün nesne referanslarını değiştirir!!!
        /// </summary>
        public void Refresh()
        {
            dbProvider.ReadDatabaseMetadata();
            tableMappingInfo = new Dictionary<Type, Table>();
            columnMappingInfo = new Dictionary<PropertyInfo, Column>();


            Cache.Remove(uniqueDatabaseName);
            Cache[uniqueDatabaseName] = this.tables;

            if (!string.IsNullOrWhiteSpace(serializedMetadataFilePath))
                File.Delete(serializedMetadataFilePath);
        }

        //private string name;
        ///// <summary>
        ///// Veritabanı adı
        ///// </summary>
        //public string Name
        //{
        //    get { return name; }
        //    internal set { name = value; }
        //}

        private TableCollection tables;
        /// <summary>
        /// Veritabanındaki tablolar
        /// </summary>
        public TableCollection Tables
        {
            get { return tables; }
            set { tables = value; }
        }

        public Constraint GetConstraint(string constraintName)
        {
            foreach (Table tbl in Tables)
                foreach (Constraint c in tbl.Constraints)
                    if (c.Name == constraintName)
                        return c;
            return null;
        }

        public override string ToString()
        {
            return this.ConnectionString;
        }

        #region connection
        [XmlIgnore]
        public IDbConnection Connection
        {
            get
            {
                return dbProvider.Connection;
            }
        }
        #endregion

        #region create DataAdapter, Command, Parameter
        public DbDataAdapter CreateDataAdapter(IDbCommand selectCommand)
        {
            return dbProvider.CreateDataAdapter(selectCommand);
        }
        public DbDataAdapter CreateDataAdapter(string selectCommandText)
        {
            addToSQLLog(selectCommandText);
            return dbProvider.CreateDataAdapter(selectCommandText);
        }
        public IDbCommand CreateCommand(string cmdText)
        {
            if (useTransaction != null)
                return dbProvider.CreateCommand(cmdText, useTransaction);
            else
                return dbProvider.CreateCommand(cmdText);
        }
        public IDbDataParameter CreateParameter(string parameterName, object value)
        {
            return dbProvider.CreateParameter(parameterName, value ?? DBNull.Value);
        }
        #endregion

        #region transaction begin
        private IDbTransaction useTransaction = null;
        private int beginCount = 0;
        public void Begin(IsolationLevel level)
        {
            if (this.noTransactions)
                return;

            if (beginCount == 0)
            {
                if (this.Connection.State == ConnectionState.Closed)
                    this.Connection.Open();
                useTransaction = this.Connection.BeginTransaction(level);
            }
            beginCount++;
        }
        public void Begin()
        {
            if (this.noTransactions)
                return;

            this.Begin(IsolationLevel.ReadCommitted);
        }
        public void Commit()
        {
            if (this.noTransactions)
                return;

            beginCount--;
            if (beginCount == 0)
            {
                this.useTransaction.Commit();
                this.useTransaction = null;
                this.beginCount = 0;
                this.Connection.Close();
            }
        }
        public void Rollback()
        {
            if (this.noTransactions)
                return;

            if (this.useTransaction != null)
                this.useTransaction.Rollback();
            this.useTransaction = null;
            beginCount = 0;
            this.Connection.Close();
        }

        public void Execute(Action dbAction)
        {
            try
            {
                this.Begin();
                dbAction();
                this.Commit();
            }
            catch(Exception ex)
            {
                this.Rollback();
                throw ex;
            }
        }
        #endregion

        #region Execute Non-query
        public int ExecuteNonQuery(IDbCommand cmd)
        {
            cmd.CommandText = editSQLAsForProvider(cmd.CommandText);
            if (useTransaction != null)
            {
                cmd.Connection = this.Connection;
                cmd.Transaction = useTransaction;
            }
            else
            {
                this.Connection.Open();
            }

            int res = 0;
            try
            {
                res = cmd.ExecuteNonQuery();
                addToSQLLog(cmd.CommandText);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (useTransaction == null)
                    this.Connection.Close();
            }
            return res;
        }

        public int ExecuteNonQuery(string sql) { return this.ExecuteNonQuery(this.CreateCommand(sql)); }
        public int ExecuteNonQuery(string sql, params object[] parameters) 
        {
            IDbCommand cmd = dbProvider.CreateCommand(sql, useTransaction, parameters);
            return this.ExecuteNonQuery(cmd); 
        }
        #endregion

        #region insert & update & save
        public int Insert(string tableName, Hashtable data)
        {
            return Insert(tableName, data, true);
        }
        public int Insert(string tableName, Hashtable data, bool bypassAutoIncrementColumn)
        {
            // validate parameters
            if (data.Count == 0) return -1;

            IDbCommand cmd = CreateInsertCommand(tableName, data, bypassAutoIncrementColumn);

            DbDataAdapter ad = this.CreateDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);

            if (dt.Rows.Count == 1)
                return Convert.ToInt32(dt.Rows[0][0]);

            return 0;
        }
        public IDbCommand CreateInsertCommand(string tableName, Hashtable data, bool bypassAutoIncrementColumn)
        {
            string insertSQL = GetSQLInsert(tableName, data, bypassAutoIncrementColumn);

            Table tbl = this.Tables[tableName];

            IDbCommand cmd = this.CreateCommand(insertSQL);
            for (int i = 0; i < tbl.Columns.Count; i++)
            {
                Column column = tbl.Columns[i];
                if (column.IsAutoIncrement && bypassAutoIncrementColumn) continue;
                if (data.ContainsKey(column.Name))
                {
                    // if column is reference and the value equals 0, continue
                    if (column.ReferenceColumn != null && column.IsNullable && column.IsNumericType())
                    {
                        if (data[column.Name] == null || data[column.Name] == DBNull.Value) data[column.Name] = 0;
                        int refId = (int)data[column.Name];
                        if (refId == 0) continue; //***
                    }
                    // SQLServer'da 1753'ten küçük tarihler sorun oluyor!!!
                    if (provider == DatabaseProvider.SQLServer && column.IsDateType())
                        if (DBNull.Value.Equals(data[column.Name]) || data[column.Name]==null || (DateTime)data[column.Name] <= new DateTime(1753, 1, 1, 12, 0, 0))
                            continue; //***

                    IDbDataParameter param = this.CreateParameter("@_" + column.Name, data[column.Name] ?? System.DBNull.Value);
                    if (column.ColumnType == DbType.Image)
                        param.DbType = System.Data.DbType.Binary;
                    cmd.Parameters.Add(param);
                }
            }
            return cmd;
        }
        public string GetSQLInsert(string tableName, Hashtable data, bool bypassAutoIncrementColumn)
        {
            Table tbl = this.Tables[tableName];
            if (tbl == null)
                throw new ApplicationException("There is no such table : " + tableName); //this.CreateTable(tbl, null, true);

            //ColumnCollection columns = tbl.Columns;
            int validColumnNumber = 0;
            foreach (Column fld in tbl.Columns)
                if (data.ContainsKey(fld.Name) && !(fld.IsAutoIncrement && bypassAutoIncrementColumn))
                    validColumnNumber++;
            if (validColumnNumber == 0) throw new ApplicationException("The table and the hashtable have no similarity!");

            // ok valid, prepare SQL
            List<string> tmpList = new List<string>();

            StringBuilder sb = new StringBuilder();
            sb.Append("insert into [" + tableName + "] (");
            for (int i = 0; i < tbl.Columns.Count; i++)
            {
                Column fld = tbl.Columns[i];
                if (fld.IsAutoIncrement && bypassAutoIncrementColumn) continue;
                if (data.ContainsKey(fld.Name))
                {
                    // if column is reference and the value equals 0, continue
                    if (fld.ReferenceColumn != null && fld.IsNullable && fld.ColumnType == DbType.Int32)
                    {
                        if (data[fld.Name] == null || data[fld.Name] == DBNull.Value) data[fld.Name] = 0;
                        int refId = (int)data[fld.Name];
                        if (refId == 0) continue; //***
                    }
                    // SQLServer'da 1753'ten küçük tarihler sorun oluyor!!!
                    if (provider == DatabaseProvider.SQLServer && fld.IsDateType())
                        if (DBNull.Value.Equals(data[fld.Name]) || data[fld.Name]==null || (DateTime)data[fld.Name] <= new DateTime(1753, 1, 1, 12, 0, 0))
                            continue;//***

                    tmpList.Add("[" + fld.Name + "]");
                }
            }
            sb.Append(String.Join(",", tmpList.ToArray()));
            tmpList.Clear();
            sb.Append(") values (");
            for (int i = 0; i < tbl.Columns.Count; i++)
            {
                Column fld = tbl.Columns[i];
                if (fld.IsAutoIncrement && bypassAutoIncrementColumn) continue;
                if (data.ContainsKey(fld.Name))
                {
                    // if column is reference and the value equals 0, continue
                    if (fld.ReferenceColumn != null && fld.IsNullable && fld.ColumnType == DbType.Int32)
                    {
                        if (data[fld.Name] == null || data[fld.Name] == DBNull.Value) data[fld.Name] = 0;
                        int refId = (int)data[fld.Name];
                        if (refId == 0) continue; //***
                    }
                    // SQLServer'da 1753'ten küçük tarihler sorun oluyor!!!
                    if (provider == DatabaseProvider.SQLServer && fld.IsDateType())
                        if (DBNull.Value.Equals(data[fld.Name]) || data[fld.Name]==null || (DateTime)data[fld.Name] <= new DateTime(1753, 1, 1, 12, 0, 0))
                            continue; //***

                    tmpList.Add("@_" + fld.Name);
                }
            }
            sb.Append(String.Join(",", tmpList.ToArray()));
            sb.Append(");");

            if (!bypassAutoIncrementColumn && provider == DatabaseProvider.SQLServer && tbl.HasAutoIncrementColumn())
            {
                sb.Insert(0, "SET IDENTITY_INSERT [" + tbl.Name + "] ON;" + Environment.NewLine);
                sb.Append(Environment.NewLine + "SET IDENTITY_INSERT [" + tbl.Name + "] OFF;");
            }

            if (tbl.HasAutoIncrementColumn())
            {
                if (provider == DatabaseProvider.SQLServer)
                    sb.AppendLine("SELECT @@identity;");
                if (provider == DatabaseProvider.MySQL)
                    sb.AppendLine("SELECT LAST_INSERT_ID();");
                if (provider == DatabaseProvider.SQLite)
                    sb.AppendLine("SELECT LAST_INSERT_ROWID();"); //-1 yapmak gerekebilir, öyle bişey vardı sanki
                if (provider == DatabaseProvider.PostgreSQL)
                    ; //TODO: Get auto increment for Postgres
            }

            return editSQLAsForProvider(sb.ToString());
        }

        public int Insert(string tableName, DataRow dataRow, bool bypassAutoIncrementColumn)
        {
            Hashtable ht = new Hashtable();
            foreach (DataColumn dc in dataRow.Table.Columns)
                ht[dc.ColumnName] = dataRow[dc];

            int res = this.Insert(tableName, ht, bypassAutoIncrementColumn);
            if (dataRow.RowState != DataRowState.Detached)
                dataRow.AcceptChanges();
            
            return res;
        }
        public int Insert(string tableName, DataRow dataRow)
        {
            return this.Insert(tableName, dataRow, true);
        }
        public int Insert(string tableName, object record)
        {
            Hashtable ht = new Hashtable();
            foreach (PropertyInfo pi in record.GetType().GetProperties())
                if(pi.GetIndexParameters().Length==0)
                    ht[pi.Name] = pi.GetValue(record, null);
            return Insert(tableName, ht);
        }

        public int Update(string tableName, Hashtable data, Hashtable originalData = null)
        {
            // validate parameters
            if (data.Count == 0) return -1;
            Table tbl = this.Tables[tableName];
            if (tbl == null) throw new ApplicationException("There is no such table : " + tableName);
            ColumnCollection columns = tbl.Columns;
            int validColumnNumber = 0;
            foreach (Column fld in tbl.Columns)
                if (data.ContainsKey(fld.Name) && data[fld.Name] != null && !fld.IsAutoIncrement)
                    validColumnNumber++;
            if (validColumnNumber == 0) throw new ApplicationException("The table and the hashtable have no similarity!");

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE [" + tableName + "] SET ");
            for (int i = 0; i < columns.Count; i++)
            {
                Column fld = columns[i];
                if (fld.IsAutoIncrement || !data.ContainsKey(fld.Name) || data[fld.Name] == null) continue;

                // SQLServer'da 1753'ten küçük tarihler sorun oluyor!!!
                if (provider == DatabaseProvider.SQLServer && fld.IsDateType())
                    if (DBNull.Value.Equals(data[fld.Name]) || (DateTime)data[fld.Name] <= new DateTime(1753, 1, 1, 12, 0, 0))
                        continue;//***

                sb.AppendFormat("[{0}] = @_{0}", fld.Name);
                sb.Append(", ");
            }
            sb.Remove(sb.Length-2,2);

            Column[] whereColumns = null;

            if (tbl.PrimaryColumn != null && data.ContainsKey(tbl.PrimaryColumn.Name) && data[tbl.PrimaryColumn.Name]!=null)
                sb.AppendFormat(" WHERE [{0}]=@_{0}", tbl.PrimaryColumn.Name);
            else if (originalData == null)
                throw new Exception("Update failed because there is no primary column OR original data.");
            else
            {
                whereColumns = tbl.Columns.Where(f => !(f.SimpleColumnType == SimpleDbType.ByteArray || f.SimpleColumnType == SimpleDbType.Text)).ToArray();
                string where = " WHERE " + string.Join(" AND ", whereColumns.Select((f, i) => originalData[f.Name] == null ? string.Format("[{0}] IS NULL", f.Name) : string.Format("[{0}]={{{1}}}", f.Name, i)).ToArray());
                object[] values = new object[whereColumns.Length];
                for (int i = 0; i < values.Length; i++) values[i] = originalData[whereColumns[i].Name];

                int count = GetInt("SELECT COUNT(*) FROM [" + tableName + "]" + where, values);
                if (count > 1)
                    throw new Exception("There are more than one row to be updated!");
                if (count < 1)
                    throw new Exception("There is no row to be updated!");
                sb.AppendFormat(" WHERE " + string.Join(" AND ", whereColumns.Select(f => originalData[f.Name] == null ? string.Format("[{0}] IS NULL", f.Name) : string.Format("[{0}] = @_org_{0}", f.Name)).ToArray()));
            }

            IDbCommand cmd = this.CreateCommand(sb.ToString());
            for (int i = 0; i < columns.Count; i++)
            {
                Column fld = columns[i];
                if (data.ContainsKey(fld.Name) && data[fld.Name] != null)
                {
                    // if column is reference and the value equals 0, continue
                    if (fld.ReferenceColumn != null && fld.IsNullable && fld.ColumnType == DbType.Int32)
                    {
                        int refId = (int)data[fld.Name];
                        if (refId == 0) data[fld.Name]=DBNull.Value; //***
                    }

                    // SQLServer'da 1753'ten küçük tarihler sorun oluyor!!!
                    if (provider == DatabaseProvider.SQLServer && fld.IsDateType())
                        if (DBNull.Value.Equals(data[fld.Name]) || (DateTime)data[fld.Name] <= new DateTime(1753, 1, 1, 12, 0, 0))
                            continue;//***
                    
                    IDbDataParameter param = this.CreateParameter("@_" + fld.Name, data[fld.Name]);
                    cmd.Parameters.Add(param);
                }
            }
            if (whereColumns != null && whereColumns.Length > 0)
                foreach (Column f in whereColumns)
                {
                    if (originalData[f.Name] == null) continue;
                    IDbDataParameter param = this.CreateParameter("@_org_" + f.Name, originalData[f.Name]);
                    cmd.Parameters.Add(param);
                }

            int res = this.ExecuteNonQuery(cmd);

            return res;
        }
        public int Update(string tableName, DataRow dataRow)
        {
            Hashtable ht = new Hashtable();
            Hashtable originalHt = new Hashtable();

            foreach (DataColumn dc in dataRow.Table.Columns)
                if (!dataRow.IsNull(dc))
                {
                    ht[dc.ColumnName] = dataRow[dc];
                    originalHt[dc.ColumnName] = dataRow[dc, DataRowVersion.Original];
                }

            int res = this.Update(tableName, ht, originalHt);
            dataRow.AcceptChanges();
            
            return res;
        }

        public int Delete(string tableName, Hashtable data)
        {
            // validate parameters
            if (data.Count == 0) return -1;
            Table tbl = this.Tables[tableName];
            if (tbl == null) throw new ApplicationException("There is no such table : " + tableName);

            ColumnCollection columns = tbl.Columns;

            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM [" + tableName + "]");

            Column[] whereColumns = null;
            IDbCommand cmd = null;

            if (tbl.PrimaryColumn != null)
            {
                sb.AppendFormat(" WHERE [{0}] = @_{0}", tbl.PrimaryColumn.Name);
                cmd = this.CreateCommand(sb.ToString());
                cmd.Parameters.Add(this.CreateParameter("@_" + tbl.PrimaryColumn.Name, data[tbl.PrimaryColumn.Name]));
            }
            else
            {
                whereColumns = tbl.Columns.Where(f => !(f.SimpleColumnType == SimpleDbType.ByteArray || f.SimpleColumnType == SimpleDbType.Text)).ToArray();
                string where = " WHERE " + string.Join(" AND ", whereColumns.Select((f, i) => data[f.Name] == null ? string.Format("[{0}] IS NULL", f.Name) : string.Format("[{0}]={{{1}}}", f.Name, i)).ToArray());
                object[] values = new object[whereColumns.Length];
                for (int i = 0; i < values.Length; i++) values[i] = data[whereColumns[i].Name];

                int count = GetInt("SELECT COUNT(*) FROM [" + tableName + "]" + where, values);
                if (count > 1)
                    throw new Exception("There are more than one row to be deleted!");
                if (count < 1)
                    throw new Exception("There is no row to be deleted!");
                sb.AppendFormat(" WHERE " + string.Join(" AND ", whereColumns.Select(f => data[f.Name] == null ? string.Format("[{0}] IS NULL", f.Name) : string.Format("[{0}] = @_{0}", f.Name)).ToArray()));

                cmd = this.CreateCommand(sb.ToString());
                if (whereColumns != null && whereColumns.Length > 0)
                    foreach (Column f in whereColumns)
                    {
                        if (data[f.Name] == null) continue;
                        IDbDataParameter param = this.CreateParameter("@_" + f.Name, data[f.Name]);
                        cmd.Parameters.Add(param);
                    }
            }

            return this.ExecuteNonQuery(cmd);
        }
        public int Delete(string tableName, DataRow dataRow)
        {
            Hashtable ht = new Hashtable();
            Hashtable originalHt = new Hashtable();

            foreach (DataColumn dc in dataRow.Table.Columns)
                if (!dataRow.IsNull(dc))
                    ht[dc.ColumnName] = dataRow[dc, DataRowVersion.Original];

            int res = this.Delete(tableName, ht);
            //dataRow.AcceptChanges();

            return res;
        }

        public string GetIdColumnName(IDatabaseEntity entity)
        {
            return GetIdColumnName(entity.GetType());
        }
        public string GetIdColumnName(Type entityType)
        {
            PropertyInfo pi = entityType.GetProperty("Id");
            Column c = GetColumnForProperty(pi);
            Table t = GetTableForEntityType(entityType);
            if (c != null)
                return c.Name;
            else if (t!=null && t.PrimaryColumn != null)
                return t.PrimaryColumn.Name;
            else
                return "Id";
        }

        // ENTITY SAVE
        public void Save(IDatabaseEntity entity)
        {
            try
            {
                this.Begin();

                // first check the table existance
                Table tbl = GetTableForEntityType(entity.GetType());//this.Tables[entity.GetType().Name];
                Type type = entity.GetType();

                if (tbl == null)
                    tbl = tableMappingInfo[entity.GetType()] = this.CreateTableForType(type);

                if (entity is ISerializeSubclassFields)
                    serialize(entity as ISerializeSubclassFields);

                Hashtable ht = EntityToHashtable(entity);

                if (entity.Id == 0)
                {
                    this.Insert(tbl.Name, ht);
                    object id = this.GetValue("select max(" + GetIdColumnName(entity) + ") from [" + tbl.Name + "]");
                    if (id == null) id = 0;
                    entity.Id = Convert.ToInt32(id);
                }
                else
                {
                    this.Update(tbl.Name, ht);
                }

                this.Commit();
            }
            catch (Exception ex)
            {
                this.Rollback();
                throw new CinarException(ex);
            }
        }

        private void serialize(ISerializeSubclassFields entity)
        {
            Type baseType = entity.GetType();
            while (baseType.BaseType.GetInterface("ISerializeSubclassFields") != null)
                baseType = baseType.BaseType;

            entity.Details = entity.SerializeToString(pi => pi.DeclaringType.IsSubclassOf(baseType));
            entity.TypeName = entity.GetType().Name;
        }

        public Hashtable EntityToHashtable(IDatabaseEntity entity)
        {
            Hashtable ht = new Hashtable();
            foreach (PropertyInfo pi in entity.GetType().GetProperties())
                if (canBeMappedToDBTable(pi))
                {
                    Column f = GetColumnForProperty(pi);
                    if (f == null) continue;

                    object val = null;

                    if (pi.PropertyType.IsEnum && f.IsStringType())
                        val = pi.GetValue(entity, null).ToString();
                    else
                        val = pi.GetValue(entity, null);
                    ht[f.Name] = val;
                }
            return ht;
        }
        public Hashtable DataRowToHashtable(DataRow dataRow)
        {
            if (dataRow == null)
                return null;

            Hashtable ht = new Hashtable();
            foreach (DataColumn dc in dataRow.Table.Columns)
                    ht[dc.ColumnName] = dataRow[dc];
            return ht;
        }

        private bool canBeMappedToDBTable(PropertyInfo pi)
        {
            return (pi.PropertyType.IsValueType || pi.PropertyType.Name == "String" || (pi.PropertyType.FullName !=null && pi.PropertyType.FullName.Contains("Byte[]"))) && pi.GetSetMethod() != null;
        }

        public DataRow EntityToDataRow(IDatabaseEntity entity)
        {
            DataTable dt = new DataTable();
            foreach (PropertyInfo pi in entity.GetType().GetProperties())
                if (canBeMappedToDBTable(pi))
                {
                    Column f = GetColumnForProperty(pi);
                    if (f != null)
                        dt.Columns.Add(f.Name, pi.PropertyType);
                }

            DataRow dr = dt.NewRow();
            foreach (PropertyInfo pi in entity.GetType().GetProperties())
                if (canBeMappedToDBTable(pi))
                {
                    Column f = GetColumnForProperty(pi);
                    if (f != null)
                        dr[f.Name] = pi.GetValue(entity, null);
                }

            return dr;
        }
        public IDatabaseEntity DataRowToEntity(Type entityType, DataRow dr)
        {
            if (dr == null) return null; //***

            IDatabaseEntity entity = null;
            if (entityType.GetInterface("ISerializeSubclassFields")!=null)
            {
                string typeName = dr["TypeName"].ToString();
                entity = (IDatabaseEntity)Activator.CreateInstance(entityType.Assembly.GetTypes().Where(t => t.Name == typeName).First());
            }
            else
                entity = (IDatabaseEntity)Activator.CreateInstance(entityType);

            FillEntity(entity, dr);
            entity.Initialize();

            return entity;
        }
        public T DataRowToEntity<T>(DataRow dr)
        {
            return (T)DataRowToEntity(typeof(T), dr);
        }

        // ENTITY FILL
        public void FillEntity(IDatabaseEntity entity, DataRow dr)
        {
            Type entityType = entity.GetType();
            
            // first read entity mappings
            Table table = GetTableForEntityType(entityType);
            foreach (PropertyInfo pi in entityType.GetProperties())
                if (canBeMappedToDBTable(pi))
                    GetColumnForProperty(pi);

            DataRowVersion rowVersion = DataRowVersion.Default;
            if (dr.RowState == DataRowState.Deleted)
                rowVersion = DataRowVersion.Original;

            foreach (DataColumn dc in dr.Table.Columns)
            {
                string colName = dc.ColumnName;
                entity.GetOriginalValues()[colName] = dr.IsNull(dc, rowVersion) ? null : dr[colName, rowVersion];
                if (dr.IsNull(dc, rowVersion)) continue;

                if (table.Columns[colName] == null)
                {
                    entity[colName] = dr[colName, rowVersion];
                    continue;
                }

                PropertyInfo pi = GetPropertyInfoForColumn(table.Columns[colName]);
                if (pi == null) continue;

                object drValue = dr[colName, rowVersion];

                if (drValue.Equals(DBNull.Value))
                    drValue = null;

                if (pi.PropertyType.IsEnum)
                {
                    if (drValue!=null && drValue.GetType()==typeof(string))
                        drValue = Enum.Parse(pi.PropertyType, drValue.ToString());
                    else
                        drValue = Enum.ToObject(pi.PropertyType, drValue);
                }
                else
                    drValue = Convert.ChangeType(drValue, pi.PropertyType);

                pi.SetValue(entity, drValue, null);

                //if (pi.PropertyType == typeof(string))
                //    pi.SetValue(entity, val.ToString(), null);
                //else if (dc.DataType.Name.EndsWith("SByte"))
                //    pi.SetValue(entity, ((sbyte)val) == 1, null);
                //else
                //    pi.SetValue(entity, val, null);
            }

            if(entity is ISerializeSubclassFields)
                entity.DeserializeFromString((entity as ISerializeSubclassFields).Details);
        }
        public void FillEntity(IDatabaseEntity entity)
        {
            string tableName = GetTableForEntityType(entity.GetType()).Name;
            this.FillEntity(entity, GetDataRow("select * from " + tableName + " where " + GetIdColumnName(entity) + "={0}", entity.Id));
        }
        public void FillDataRow(IDatabaseEntity entity, DataRow dr)
        {
            Type entityType = entity.GetType();

            foreach (PropertyInfo pi in entityType.GetProperties())
                if (canBeMappedToDBTable(pi))
                {
                    Column f = GetColumnForProperty(pi);
                    if (f != null)
                        dr[f.Name] = pi.GetValue(entity, null);
                }
        }

        public void Delete(IDatabaseEntity entity)
        {
            if(entity==null)
                throw new ArgumentNullException("entity");

            string tableName = GetTableForEntityType(entity.GetType()).Name;
            string primaryColumnName = GetIdColumnName(entity.GetType());
            ExecuteNonQuery("delete from [" + tableName + "] where [" + primaryColumnName + "] = {0}", entity.Id);
        }

        #endregion

        #region read data
        public DataSet GetDataSet(DataSet ds, string sql, params object[] parameters)
        {
            sql = editSQLAsForProvider(sql);
            addToSQLLog(sql);
            DbDataAdapter da = this.dbProvider.CreateDataAdapter(sql, parameters);
            if (useTransaction != null)
            {
                da.SelectCommand.Connection = (DbConnection)this.Connection;
                da.SelectCommand.Transaction = (DbTransaction)useTransaction;
            }
            if(ds==null)
                ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataSet GetDataSet(string sql, params object[] parameters)
        {
            return GetDataSet(null, sql, parameters);
        }
        public DataSet GetDataSet(string sql)
        {
            return GetDataSet(null, sql, new object[0]);
        }
        public DataTable GetDataTable(DataSet ds, string tableName, string sql, params object[] parameters)
        {
            ds = GetDataSet(ds, sql, parameters);
            if (ds.Tables.Count >= 1)
            {
                ds.Tables[tableName].AcceptChanges();
                return ds.Tables[tableName];
            }
            else
                return null;
        }
        public DataTable GetDataTable(string sql, params object[] parameters)
        {
            return GetDataTable(new DataSet(), "Table", sql, parameters);
        }
        public DataTable GetDataTable(string sql)
        {
            return GetDataTable(sql, new object[0]);
        }
        public DataTable GetDataTableFor(string tableName, FilterExpression fExp)
        {
            return getDataTableFor(tableName, fExp);
        }
        public DataTable GetDataTableFor(Type entityType, FilterExpression fExp)
        {
            Table table = GetTableForEntityType(entityType);
            if (table == null)
                table = tableMappingInfo[entityType] = this.CreateTableForType(entityType);

            return getDataTableFor(table.Name, fExp);
        }

        private DataTable getDataTableFor(string tableName, FilterExpression fExp)
        {
            Table table = Tables[tableName];

            if (fExp == null) fExp = new FilterExpression();
            if (fExp.PageSize == 0) fExp.PageSize = 10000;
            string where = fExp.Criterias.ToParamString();
            string orderBy = fExp.Orders.ToString();
            if (orderBy == "") orderBy = " ORDER BY [" + (table.PrimaryColumn != null ? table.PrimaryColumn.Name : table.Columns[0].Name) + "]";
            string sql = "";
            switch (Provider)
            {
                case DatabaseProvider.PostgreSQL:
                    sql = "SELECT * FROM \"" + table.Name + "\"" + where + orderBy + (fExp.PageSize > 0 ? " LIMIT " + fExp.PageSize + " OFFSET " + fExp.PageSize * fExp.PageNo : "");
                    break;
                case DatabaseProvider.MySQL:
                    sql = "SELECT * FROM `" + table.Name + "`" + where + orderBy + (fExp.PageSize > 0 ? " LIMIT " + fExp.PageSize + " OFFSET " + fExp.PageSize * fExp.PageNo : "");
                    break;
                case DatabaseProvider.SQLite:
                    sql = "SELECT * FROM [" + table.Name + "]" + where + orderBy + (fExp.PageSize > 0 ? " LIMIT " + fExp.PageSize + " OFFSET " + fExp.PageSize * fExp.PageNo : "");
                    break;
                case DatabaseProvider.SQLServer:
                    sql = @"WITH _CinarResult AS
                                (
                                    SELECT " + string.Join(", ", table.Columns.Select(f => "[" + f.Name + "]").ToArray()) + @",
                                    ROW_NUMBER() OVER (" + orderBy + @") AS '_CinarRowNumber'
                                    FROM [" + table.Name + @"] 
                                    " + where + @"
                                ) 
                                SELECT " + string.Join(", ", table.Columns.Select(f => "[" + f.Name + "]").ToArray()) + @" 
                                FROM _CinarResult 
                                WHERE _CinarRowNumber BETWEEN " + (fExp.PageSize * fExp.PageNo) + " AND " + (fExp.PageSize * fExp.PageNo + fExp.PageSize) + ";";
                    break;
                default:
                    break;
            }

            return this.GetDataTable(sql, fExp.GetParamValues());
        }

        public string AddPagingToSQL(string sql, int pageSize, int pageNo)
        {
            if (pageSize == 0)
                return sql;

            switch (Provider)
            {
                case DatabaseProvider.PostgreSQL:
                case DatabaseProvider.MySQL:
                case DatabaseProvider.SQLite:
                    return sql + " LIMIT " + pageSize + " OFFSET " + (pageSize * pageNo);
                case DatabaseProvider.SQLServer:
                    int indexOfFrom = sql.IndexOf("from", StringComparison.InvariantCultureIgnoreCase);
                    int indexOfOrderBy = sql.LastIndexOf("order by", StringComparison.InvariantCultureIgnoreCase);
                    return "WITH _CinarResult AS (" +
                            sql.Substring(0, indexOfFrom) +
                            ", ROW_NUMBER() OVER (" + sql.Substring(indexOfOrderBy) + @") AS _CinarRowNumber " +
                            sql.Substring(indexOfFrom, indexOfOrderBy - indexOfFrom) +
                            ") select * from _CinarResult WHERE _CinarRowNumber BETWEEN " + (pageSize * pageNo + 1) + " AND " + (pageSize * pageNo + pageSize) + ";";
            }

            return sql;
        }

        public List<T> GetList<T>(string sql, params object[] parameters)
        {
            return GetDataTable(sql, parameters).Rows.OfType<DataRow>().Select(dr => (T)(dr[0] == DBNull.Value ? default(T) : dr[0])).ToList();
        }
        public Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string sql, params object[] parameters)
        {
            DataTable dt = GetDataTable(sql, parameters);
            if (dt.Columns.Count < 2)
                throw new Exception("select at least 2 columns for dictionary");

            Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
            foreach (DataRow dr in dt.Rows)
            {
                TKey key = (TKey)Convert.ChangeType(dr[0], typeof(TKey));
                if (!dict.ContainsKey(key))
                    dict.Add(key, (TValue)Convert.ChangeType(dr[1], typeof(TValue)));
            }

            return dict;
        }
        public Hashtable GetKeyValueList(string sql, params object[] parameters)
        {
            DataTable dt = GetDataTable(sql, parameters);
            if (dt.Columns.Count < 2)
                throw new Exception("select at least 2 columns for dictionary");

            Hashtable dict = new Hashtable();
            foreach (DataRow dr in dt.Rows)
            {
                object key = dr[0];
                if (!dict.ContainsKey(key))
                    dict.Add(key, dr[1]);
            }

            return dict;
        }

        public DataRow GetDataRow(string sql, params object[] parameters)
        {
            DataTable dt = this.GetDataTable(sql, parameters);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            else if (dt.Rows.Count == 1)
                return dt.Rows[0];
            else
                throw new ApplicationException("There is more than one row returned by that sql. (" + sql + ")");
        }
        public DataRow GetDataRow(string sql)
        {
            return GetDataRow(sql, new object[0]);
        }
        public Hashtable GetHashtable(string sql)
        {
            return GetHashtable(sql, new object[0]);
        }
        public Hashtable GetHashtable(string sql, params object[] parameters)
        {
            return DataRowToHashtable(GetDataRow(sql, parameters));
        }
        public object GetValue(string sql, params object[] parameters)
        {
            DataRow dr = this.GetDataRow(sql, parameters);
            if (dr != null)
            {
                if (dr.IsNull(0))
                    return null;
                else
                    return dr[0];
            }
            else
                return null;
        }
        public object GetValue(string sql)
        {
            return this.GetValue(sql, new object[0]);
        }
        public string GetString(string sql, params object[] parameters)
        {
            return Convert.ToString(GetValue(sql, parameters));
        }
        public string GetString(string sql)
        {
            return Convert.ToString(GetValue(sql));
        }
        public DateTime GetDateTime(string sql, params object[] parameters)
        {
            object obj = GetValue(sql, parameters);
            if (obj != null)
                return Convert.ToDateTime(obj);
            else
                return new DateTime();
        }
        public DateTime GetDateTime(string sql)
        {
            object obj = GetValue(sql);
            if (obj != null)
                return Convert.ToDateTime(obj);
            else
                return new DateTime();
        }
        public bool GetBool(string sql, params object[] parameters)
        {
            object o = GetValue(sql, parameters);
            return Convert.ToBoolean(o ?? false);
        }
        public bool GetBool(string sql)
        {
            return Convert.ToBoolean(GetValue(sql));
        }
        public int GetInt(string sql, params object[] parameters)
        {
            object o = GetValue(sql, parameters);
            return Convert.ToInt32(o ?? 0);
        }
        public int GetInt(string sql)
        {
            object o = GetValue(sql);
            return Convert.ToInt32(o ?? 0);
        }

        public void ClearEntityWebCache(Type entityType, int id)
        {
            HttpContext.Current.Items[entityType.Name + "_" + id] = null;
        }

        public IDatabaseEntity Read(Type entityType, int id)
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items[entityType.Name + "_" + id] != null)
                    return (IDatabaseEntity)HttpContext.Current.Items[entityType.Name + "_" + id];
                else
                {
                    HttpContext.Current.Items[entityType.Name + "_" + id] = Read(entityType, GetIdColumnName(entityType) + "=" + id);
                    return (IDatabaseEntity)HttpContext.Current.Items[entityType.Name + "_" + id];
                }
            }
            else
            {
                return Read(entityType, "" + GetIdColumnName(entityType) + "=" + id);
            }
        }
        public T Read<T>(int id)
        {
            return (T)Read(typeof(T), id);
        }
        // READ ENTITY
        public IDatabaseEntity Read(Type entityType, string where, params object[] parameters)
        {
            IDatabaseEntity result = null;

            try
            {
                this.Begin();

                // first check the table existance
                Table table = GetTableForEntityType(entityType);
                if (table == null)
                    table = tableMappingInfo[entityType] = this.CreateTableForType(entityType);
                
                where = where.Trim();
                string sql = where.StartsWith("select", StringComparison.InvariantCultureIgnoreCase) ? where : ("select * from [" + table.Name + "] where " + where);
                sql = editSQLAsForProvider(sql);
                result = DataRowToEntity(entityType, this.GetDataRow(sql, parameters));

                this.Commit();
            }
            catch (Exception ex)
            {
                this.Rollback();
                throw ex;
            }
            return result;
        }
        public T Read<T>(string where, params object[] parameters)
        {
            return (T)Read(typeof(T), where, parameters);
        }

        public IDatabaseEntity[] ReadList(Type entityType, string selectSql, params object[] parameters)
        {
            ArrayList result = new ArrayList();

            try
            {
                this.Begin();

                DataTable dt = this.ReadTable(entityType, selectSql, parameters);
                foreach (DataRow dr in dt.Rows)
                    result.Add(DataRowToEntity(entityType, dr));

                this.Commit();
            }
            catch (Exception ex)
            {
                this.Rollback();
                throw ex;
            }
            return (IDatabaseEntity[])result.ToArray(entityType);
        }
        public List<T> ReadList<T>(string selectSql, params object[] parameters) where T : IDatabaseEntity
        {
            List<T> list = new List<T>();
            IDatabaseEntity[] arr = ReadList(typeof(T), selectSql, parameters);
            if (list != null && arr.Length > 0)
                for (int i = 0; i < arr.Length; i++)
                    list.Add((T)arr[i]);
            return list;

        }
        public List<T> ReadList<T>(FilterExpression fExp, int pageNo, int pageSize) where T : IDatabaseEntity
        {
            fExp.PageNo = pageNo;
            fExp.PageSize = pageSize;
            return ReadList<T>(fExp);
        }
        public List<T> ReadList<T>(FilterExpression filterExpression) where T : IDatabaseEntity
        {
            DataTable dt = GetDataTableFor(typeof(T), filterExpression);
            List<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
                list.Add(DataRowToEntity<T>(dr));
            return list;
        }
        public List<T> ReadList<T>() where T : IDatabaseEntity
        {
            return ReadList<T>(new FilterExpression());
        }
        public IDatabaseEntity[] ReadList(Type entityType, FilterExpression filterExpression)
        {
            DataTable dt = GetDataTableFor(entityType, filterExpression);
            List<IDatabaseEntity> list = new List<IDatabaseEntity>();
            foreach (DataRow dr in dt.Rows)
                list.Add(DataRowToEntity(entityType, dr));
            return list.ToArray();
        }
        public int ReadCount(Type entityType, FilterExpression filterExpression)
        {
            string selectSQL = "SELECT COUNT(*) FROM [" + entityType.Name + "] " + filterExpression.Criterias.ToParamString();
            return GetInt(selectSQL, filterExpression.GetParamValues());
        }
        public DataTable ReadTable(Type entityType, string selectSql, params object[] parameters)
        {
            DataTable dt = null;

            try
            {
                this.Begin();

                // first check the table existance
                Table table = GetTableForEntityType(entityType);
                if (table == null)
                    table = tableMappingInfo[entityType] = this.CreateTableForType(entityType);

                dt = this.GetDataTable(selectSql, parameters);

                this.Commit();
            }
            catch (Exception ex)
            {
                this.Rollback();
                throw ex;
            }

            return dt;
        }

        public string GetFromWithJoin(Type entityType)
        {
            // first check the table existance
            Table tbl = GetTableForEntityType(entityType);
            if (tbl == null)
                tbl = tableMappingInfo[entityType] = this.CreateTableForType(entityType);

            return GetFromWithJoin(tbl);
        }
        public string GetFromWithJoin(Table tbl)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("[{0}]\n", tbl.Name);
            foreach (Column column in tbl.Columns)
                if (column.ReferenceColumn != null)
                    sb.AppendFormat("\tleft join [{0}] as [{1}] ON [{1}].[{2}] = [{3}].[{4}]\n", column.ReferenceColumn.Table.Name, "T" + column.Name, column.ReferenceColumn.Name, tbl.Name, column.Name);

            return sb.ToString();
        }
        public string GetFieldNamesWithJoin(Table tbl)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Column column in tbl.Columns)
            {
                sb.AppendFormat("[{0}].[{1}] as [{1}],\n", column.Table.Name, column.Name);
                if (column.ReferenceColumn != null)
                    sb.AppendFormat("[{0}].[{1}] as [{0}.{1}],\n", "T" + column.Name, column.ReferenceColumn.Table.StringColumn.Name);
            }
            sb.Remove(sb.Length - 2, 1);
            return sb.ToString();
        }
        #endregion

        #region create table
        /// <summary>
        /// Parametre olarak verilen tipe uygun tablo create eder.
        /// DİKKAT: bütün nesne referanslarını değiştirir!!!
        /// </summary>
        public Table CreateTableForType(Type type)
        {
            return CreateTableForType(type, true);
        }
        public Table CreateTableForType(Type type, bool refreshMetadata)
        {
            if (!typeof(IDatabaseEntity).IsAssignableFrom(type))
                throw new Exception("The type to create table must implement IDatabaseEntity.");

            DefaultDataAttribute[] defaultDataArr = (DefaultDataAttribute[])type.GetCustomAttributes(typeof(DefaultDataAttribute), false);

            if (type.GetInterface("ISerializeSubclassFields") != null) // bu tip base class ile map ediliyor olabilir
            {
                while (type.BaseType.GetInterface("ISerializeSubclassFields") != null)
                    type = type.BaseType;
            }

            Table tbl = this.CreateTableMetadataForType(type);
            this.Tables.Add(tbl);

            this.CreateTable(tbl, defaultDataArr, refreshMetadata);

            return this.Tables[tbl.Name];
        }

        public void CreateTable(Table tbl, DefaultDataAttribute[] defaultDataArr, bool refreshMetadata)
        {
            Database originalDatabase = (Database)tbl.parent.db;
            tbl.parent.db = this;

            IDbCommand cmd = this.CreateCommand(tbl.ToDDL());
            this.ExecuteNonQuery(cmd);

            if (defaultDataArr != null)
                foreach (DefaultDataAttribute defaultData in defaultDataArr)
                    this.ExecuteNonQuery("insert into [" + tbl.Name + "](" + defaultData.ColumnList + ") values(" + defaultData.ValueList + ")");

            tbl.parent.db = originalDatabase;
            
            if(refreshMetadata)
                this.Refresh();
        }

        public Table CreateTableMetadataForType(Type type)
        {
            TableDetailAttribute tableProps = type.GetAttribute<TableDetailAttribute>();

            Table tbl = new Table();
            tbl.IsView = tableProps.IsView;
            tbl.ViewSQL = tableProps.ViewSQL;
            tbl.Name = string.IsNullOrEmpty(tableProps.Name) ? type.Name : tableProps.Name;
            foreach (PropertyInfo pi in type.GetProperties())
                if (canBeMappedToDBTable(pi))
                {
                    Column f = new Column();
                    tbl.Columns.Add(f);
                    setColumnPropsFromPropertyInfo(f, pi, tbl);
                }

            foreach (var index in type.GetAttributes<Index>())
                tbl.Indices.Add(index);
            foreach (var unq in type.GetAttributes<UniqueConstraint>())
                tbl.Constraints.Add(unq);
            foreach (var fk in type.GetAttributes<ForeignKeyConstraint>())
                tbl.Constraints.Add(fk);
            foreach (var pk in type.GetAttributes<PrimaryKeyConstraint>())
                tbl.Constraints.Add(pk);
            foreach (var cc in type.GetAttributes<CheckConstraint>())
                tbl.Constraints.Add(cc);

            return tbl;
        }
        private void setColumnPropsFromPropertyInfo(Column f, PropertyInfo pi, Table forTable)
        {
            object[] attribs = pi.GetCustomAttributes(typeof(ColumnDetailAttribute), true);
            ColumnDetailAttribute columnProps = new ColumnDetailAttribute();
            if (attribs.Length > 0) columnProps = (ColumnDetailAttribute)attribs[0];

            f.Name = string.IsNullOrEmpty(columnProps.Name) ? pi.Name : columnProps.Name;
            f.DefaultValue = columnProps.DefaultValue;
            if (columnProps.ColumnType == DbType.Undefined)
                f.ColumnType = Column.GetDbTypeOf(pi.PropertyType);
            else
                f.ColumnType = columnProps.ColumnType;
            f.IsAutoIncrement = columnProps.IsAutoIncrement;
            f.IsNullable = !columnProps.IsNotNull;
            if (columnProps.IsPrimaryKey)
            {
                PrimaryKeyConstraint pk = new PrimaryKeyConstraint();
                pk.ColumnNames = new List<string> { f.Name };
                pk.Name = string.Format("PK_{0}_{1}", f.Table.Name, f.Name);
                f.Table.Constraints.Add(pk);
            }
            f.Length = columnProps.Length;

            if (!columnProps.IsPrimaryKey && columnProps.IsUnique)
            {
                UniqueConstraint uc = new UniqueConstraint();
                uc.ColumnNames = new List<string> { f.Name };
                uc.Name = string.Format("UNQ_{0}_{1}", f.Table.Name, f.Name);
                f.Table.Constraints.Add(uc);
            }

            //TODO: burada column'ın refer ettiği tablo create ediliyordu ama stack overflow'a neden olduğu için kaldırıldı.

            //if (columnProps.References != null) { 
            //    Table referencedTable = this.Tables[columnProps.References.Name];
            //    if (referencedTable == null){
            //        if (columnProps.References.Name != forTable.Name)
            //        {
            //            Table newTable = CreateTableForType(columnProps.References);
            //            f.ReferenceColumn = newTable.PrimaryColumn;
            //        }else
            //        {
            //            f.ReferenceColumn = forTable.PrimaryColumn;
            //        }
            //    }
            //}
        }

        public void CreateTablesForAllTypesIn(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsAbstract || type.GetInterface("IDatabaseEntity") == null)
                    continue;

                if(GetTableForEntityType(type)==null)
                    tableMappingInfo[type] = this.CreateTableForType(type, false);
            }

            this.Refresh();
        }
        #endregion

        #region mapping
        private Dictionary<Type, Table> tableMappingInfo = new Dictionary<Type, Table>();
        private Dictionary<PropertyInfo, Column> columnMappingInfo = new Dictionary<PropertyInfo, Column>();
        public Table GetTableForEntityType(Type entityType)
        {
            if (tableMappingInfo.ContainsKey(entityType))
                return tableMappingInfo[entityType];
            else if (entityType.GetInterface("ISerializeSubclassFields") != null) // bu tip base class ile map ediliyor olabilir
            {
                Type baseType = entityType;
                while (baseType.BaseType.GetInterface("ISerializeSubclassFields") != null)
                    baseType = baseType.BaseType;
                if (tableMappingInfo.ContainsKey(baseType))
                {
                    tableMappingInfo[entityType] = tableMappingInfo[baseType];
                    return tableMappingInfo[entityType];
                }
                else
                {
                    object[] attribs2 = baseType.GetCustomAttributes(typeof(TableDetailAttribute), false);
                    if (attribs2 != null && attribs2.Length >= 1 && !string.IsNullOrEmpty((attribs2[0] as TableDetailAttribute).Name))
                        tableMappingInfo[baseType] = this.Tables[(attribs2[0] as TableDetailAttribute).Name];
                    if (!tableMappingInfo.ContainsKey(baseType))
                        tableMappingInfo[baseType] = this.Tables[baseType.Name];

                    return tableMappingInfo[entityType] = tableMappingInfo[baseType];
                }
            }


            object[] attribs = entityType.GetCustomAttributes(typeof(TableDetailAttribute), false);
            if (attribs != null && attribs.Length >= 1 && !string.IsNullOrEmpty((attribs[0] as TableDetailAttribute).Name))
                tableMappingInfo[entityType] = this.Tables[(attribs[0] as TableDetailAttribute).Name];
            if (!tableMappingInfo.ContainsKey(entityType))
                tableMappingInfo[entityType] = this.Tables[entityType.Name];

            return tableMappingInfo[entityType];
        }
        public Column GetColumnForProperty(PropertyInfo propertyInfo)
        {
            if (columnMappingInfo.ContainsKey(propertyInfo))
                return columnMappingInfo[propertyInfo];

            Table table = GetTableForEntityType(propertyInfo.ReflectedType);
            if (table == null)
                return null;

            object[] attribs = propertyInfo.GetCustomAttributes(typeof(ColumnDetailAttribute), false);
            if (attribs != null && attribs.Length >= 1 && !string.IsNullOrEmpty((attribs[0] as ColumnDetailAttribute).Name))
                columnMappingInfo[propertyInfo] = table.Columns[(attribs[0] as ColumnDetailAttribute).Name];
            if (!columnMappingInfo.ContainsKey(propertyInfo))
                columnMappingInfo[propertyInfo] = table.Columns[propertyInfo.Name];
            if (columnMappingInfo[propertyInfo] == null)
            {
                //throw new Exception(propertyInfo.ReflectedType.Name + " veya " + propertyInfo.DeclaringType.Name + " isimli tabloya ait " + propertyInfo.Name + " isimli alan bulunamadı!");
            }
            return columnMappingInfo[propertyInfo];
            //return columnMappingInfo.ContainsKey(propertyInfo) ? columnMappingInfo[propertyInfo] : null;
        }
        public PropertyInfo GetPropertyInfoForColumn(Column column)
        {
            if(columnMappingInfo.ContainsValue(column))
                return columnMappingInfo.First(p => p.Value == column).Key;
            return null;
        }
        public Type GetEntityTypeForTable(Table table)
        {
            if(tableMappingInfo.ContainsValue(table))
                return tableMappingInfo.First(p => p.Value == table).Key;
            return null;
        }
        #endregion

        #region DDL
        public string DbTypeToString(DbType dbType) { return dbProvider.DbTypeToString(dbType); }
        public DbType StringToDbType(string dbType) { return dbProvider.StringToDbType(dbType); }
        public string[] GetOriginalColumnTypes()
        {
            return dbProvider.GetColumnTypes();
        }

        public string GetDatabaseDDL(bool addDropTable)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Table tbl in this.Tables.OrderBy(t=>t.Name))
            {
                if (addDropTable)
                {
                    switch (Provider)
                    {
                        case DatabaseProvider.MySQL:
                        case DatabaseProvider.PostgreSQL:
                        case DatabaseProvider.SQLite:
                            sb.AppendLine("DROP TABLE IF EXISTS [" + tbl.Name + "];");
                            break;
                        case DatabaseProvider.SQLServer:
                            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[" + tbl.Name + "]') AND type in (N'U')) DROP TABLE [" + tbl.Name + "];");
                            break;
                    }
                }

                sb.AppendLine(GetTableDDL(tbl));
            }

            return sb.ToString();
        }
        public string GetTableDDL(Table table)
        {
            return dbProvider.GetTableDDL(table);
        }
        public string GetTableDDL(Table table, DatabaseProvider provider)
        {
            if (provider == this.Provider)
                return GetTableDDL(table);

            IDatabaseProvider dbPrvdr = null;
            switch (provider)
            {
                case DatabaseProvider.PostgreSQL:
                    dbPrvdr = new Providers.PostgreSQLProvider(this, false);
                    break;
                case DatabaseProvider.MySQL:
                    dbPrvdr = new Providers.MySqlProvider(this, false);
                    break;
                case DatabaseProvider.SQLServer:
                    dbPrvdr = new Providers.SQLServerProvider(this, false);
                    break;
                case DatabaseProvider.SQLite:
                    dbPrvdr = new Providers.SQLiteProvider(this, false);
                    break;
                default:
                    throw new ApplicationException("It is not that much provided.");
            }

            return dbPrvdr.GetTableDDL(table);
        }
        public string GetColumnDDL(Column column)
        {
            return dbProvider.GetColumnDDL(column);
        }
        #endregion

        #region vendor spesific SQL syntax

        public string GetSQLTableList()
        {
            return dbProvider.GetSQLTableList();
        }

        public string GetSQLTableRename(string oldName, string newName)
        {
            return dbProvider.GetSQLTableRename(oldName, newName);
        }

        public string GetSQLTableDrop(Table table)
        {
            return dbProvider.GetSQLTableDrop(table);
        }

        public string GetSQLColumnList(string tableName)
        {
            return dbProvider.GetSQLColumnList(tableName);
        }

        public string GetSQLColumnAdd(string toTable, Column column)
        {
            return dbProvider.GetSQLColumnAdd(toTable, column);
        }

        public string GetSQLColumnRemove(Column column)
        {
            return dbProvider.GetSQLColumnRemove(column);
        }

        public string GetSQLColumnRename(string oldColumnName, Column column)
        {
            return dbProvider.GetSQLColumnRename(oldColumnName, column);
        }

        public string GetSQLColumnChangeDataType(Column column)
        {
            return dbProvider.GetSQLColumnChangeDataType(column);
        }

        public string GetSQLColumnChangeDefault(Column column)
        {
            return dbProvider.GetSQLColumnChangeDefault(column);
        }

        public string GetSQLConstraintList()
        {
            return dbProvider.GetSQLConstraintList();
        }

        public string GetSQLConstraintRemove(Constraint constraint)
        {
            return dbProvider.GetSQLConstraintRemove(constraint);
        }

        public string GetSQLConstraintAdd(Constraint constraint)
        {
            return dbProvider.GetSQLConstraintAdd(constraint);
        }

        public string GetSQLConstraintAdd(CheckConstraint constraint)
        {
            return dbProvider.GetSQLConstraintAdd(constraint);
        }

        public string GetSQLConstraintAdd(UniqueConstraint constraint)
        {
            return dbProvider.GetSQLConstraintAdd(constraint);
        }

        public string GetSQLConstraintAdd(ForeignKeyConstraint constraint)
        {
            return dbProvider.GetSQLConstraintAdd(constraint);
        }

        public string GetSQLConstraintAdd(PrimaryKeyConstraint constraint)
        {
            return dbProvider.GetSQLConstraintAdd(constraint);
        }

        public string GetSQLConstraintRemove(PrimaryKeyConstraint constraint)
        {
            return dbProvider.GetSQLConstraintRemove(constraint);
        }

        public string GetSQLColumnAddNotNull(Column column)
        {
            return dbProvider.GetSQLColumnAddNotNull(column);
        }

        public string GetSQLColumnRemoveNotNull(Column column)
        {
            return dbProvider.GetSQLColumnRemoveNotNull(column);
        }

        public string GetSQLColumnSetAutoIncrement(Column column)
        {
            return dbProvider.GetSQLColumnSetAutoIncrement(column);
        }

        public string GetSQLColumnRemoveAutoIncrement(Column column)
        {
            return dbProvider.GetSQLColumnRemoveAutoIncrement(column);
        }

        public string GetSQLIndexAdd(Index index)
        {
            return dbProvider.GetSQLIndexAdd(index);
        }

        public string GetSQLIndexRemove(Index index)
        {
            return dbProvider.GetSQLIndexRemove(index);
        }

        public string GetSQLBaseIndexConstraintAdd(BaseIndexConstraint obj)
        {
            if(obj is Index)
                return GetSQLIndexAdd((Index)obj);
            else
                return GetSQLConstraintAdd((Constraint)obj);
        }

        public string GetSQLBaseIndexConstraintRemove(BaseIndexConstraint obj)
        {
            if(obj is Index)
                return GetSQLIndexRemove((Index)obj);
            else
                return GetSQLConstraintRemove((Constraint)obj);
        }

        public string GetSQLViewCreate(Table view)
        {
            return dbProvider.GetSQLViewCreate(view);
        }

        public string GetSQLDateYearMonthPart(string columnName)
        {
            return dbProvider.GetSQLDateYearMonthPart(columnName);
        }

        // http://troels.arvin.dk/db/rdbms/
        // http://en.wikibooks.org/wiki/SQL_dialects_reference
        public List<string> GetDatabases()
        {
            switch (Provider)
            {
                case DatabaseProvider.PostgreSQL:
                    return this.GetList<string>("SELECT datname FROM pg_catalog.pg_database");
                case DatabaseProvider.MySQL:
                    return this.GetList<string>("SHOW DATABASES");
                case DatabaseProvider.SQLServer:
                    return this.GetList<string>("EXEC SP_HELPDB");
                case DatabaseProvider.SQLite:
                    return new List<string>() {this.Name };
                case DatabaseProvider.Cinar:
                    return new List<string>() {this.Name };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public string GetVersion() {
            switch (Provider)
            {
                case DatabaseProvider.PostgreSQL:
                case DatabaseProvider.MySQL:
                    return this.GetString("SELECT version()");
                case DatabaseProvider.SQLite:
                    return this.GetString("select sqlite_version();");
                case DatabaseProvider.SQLServer:
                    return this.GetString("SELECT SERVERPROPERTY('ProductVersion') ");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }
}
