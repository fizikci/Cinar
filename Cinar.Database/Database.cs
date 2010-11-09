﻿/*
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

namespace Cinar.Database
{
    /// <summary>
    /// Veritabanı metadasını modelleyen sınıf.
    /// </summary>
    [Serializable]
    public class Database
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

        private Hashtable _cache;
        [XmlIgnore]
        public Hashtable Cache
        {
            get
            {
                if (_cache == null)
                    _cache = new Hashtable();
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

        public Database(string connectionString, DatabaseProvider provider, string serializedMetadataFilePath)
        {
            createProviderAndReadMetadata(connectionString, provider, serializedMetadataFilePath);
        }

        /// <summary>
        /// Database constructor
        /// </summary>
        /// <param name="connectionString">Bağlantı bilgisi</param>
        /// <param name="provider">Hangi veritabanı bu?</param>
        public Database(string connectionString, DatabaseProvider provider)
            : this(connectionString, provider, null)
        {
        }

        public Database() 
        { 
        }

        public void SetCollectionParents()
        {
            this.Tables.db = this;
            foreach (Table table in this.Tables)
            {
                table.parent = this.Tables;

                table.Fields.table = table;
                foreach (Field field in table.Fields)
                    field.parent = table.Fields;

                table.Keys.table = table;
                foreach (Key key in table.Keys)
                    key.parent = table.Keys;
            }
        }

        private void createProviderAndReadMetadata(string connectionString, DatabaseProvider provider, string serializedMetadataFilePath)
        {
            this.connectionString = connectionString;
            this.provider = provider;

            CreateDbProvider(true);

            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Application["databaseMetadata"] == null)
                {
                    if (string.IsNullOrEmpty(serializedMetadataFilePath))
                        dbProvider.ReadDatabaseMetadata();
                    else
                        readMetadataFromFile(serializedMetadataFilePath);
                    HttpContext.Current.Application["databaseMetadata"] = this.tables;
                }
                else
                    this.tables = (TableCollection)HttpContext.Current.Application["databaseMetadata"];
            }
            else
            {
                if (!Cache.ContainsKey("databaseMetadata"))
                {
                    if (string.IsNullOrEmpty(serializedMetadataFilePath))
                        dbProvider.ReadDatabaseMetadata();
                    else
                        readMetadataFromFile(serializedMetadataFilePath);
                    Cache["databaseMetadata"] = this.tables;
                }
                else
                    this.tables = (TableCollection)Cache["databaseMetadata"];
            }
        }

        private void readMetadataFromFile(string serializedMetadataFilePath)
        {
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
                default:
                    throw new ApplicationException("It is not that much provided.");
            }
        }

        public Database(DatabaseProvider provider, string host, string dbName, string userName, string password, int defaultCommandTimeout)
            : this(provider, host, dbName, userName, password, defaultCommandTimeout, null)
        {
        }

        public Database(DatabaseProvider provider, string host, string dbName, string userName, string password, int defaultCommandTimeout, string serializedMetadataFilePath)
        {
            SetConnectionString(provider, host, dbName, userName, password, defaultCommandTimeout);
            createProviderAndReadMetadata(this.connectionString, provider, serializedMetadataFilePath);
        }

        public void SetConnectionString(DatabaseProvider provider, string host, string dbName, string userName, string password, int defaultCommandTimeout)
        {
            this.provider = provider;
            switch (provider)
            {
                case DatabaseProvider.PostgreSQL:
                    this.connectionString = String.Format("Server={0};Database={1};User Id={2};Password={3};",
                                                          host, dbName, userName, password);
                    break;
                case DatabaseProvider.MySQL:
                    if (defaultCommandTimeout == 0) defaultCommandTimeout = 20;
                    this.connectionString = String.Format("Server={0};Database={1};Uid={2};Pwd={3};old syntax=yes;charset=utf8;default command timeout={4};",
                                                          host, dbName, userName, password, defaultCommandTimeout);
                    break;
                case DatabaseProvider.SQLServer:
                    this.connectionString = String.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};",
                                                          host, dbName, userName, password);
                    break;
                default:
                    throw new ApplicationException("It is not that much provided.");
            }
        }

        public Database CreateNewInstance()
        {
            Database db = new Database();
            db.connectionString = this.connectionString;
            db.provider = this.provider;
            db.tables = this.tables;

            switch (provider)
            {
                case DatabaseProvider.PostgreSQL:
                    db.dbProvider = new Providers.PostgreSQLProvider(this, true);
                    break;
                case DatabaseProvider.MySQL:
                    db.dbProvider = new Providers.MySqlProvider(this, true);
                    break;
                case DatabaseProvider.SQLServer:
                    db.dbProvider = new Providers.SQLServerProvider(this, true);
                    break;
                default:
                    throw new ApplicationException("It is not that much provided.");
            }

            return db;
        }

        private string getReservedWordToken(bool left) {
            switch (provider)
            {
                case DatabaseProvider.PostgreSQL:
                    return "\"";
                case DatabaseProvider.MySQL:
                    return "`";
                case DatabaseProvider.SQLServer:
                    return left ? "[" : "]";
            }
            return "";
        }
        private string editSQLAsForProvider(string sql)
        {
            StringComparison ic = StringComparison.InvariantCultureIgnoreCase;

            sql = sql.Trim();
            if (provider != DatabaseProvider.SQLServer && (sql.StartsWith("select top ", ic) || sql.StartsWith("select distinct top ", ic)))
            {
                string[] parts = sql.Split(' ');
                int rowCountIndex = sql.StartsWith("select distinct top ", ic) ? 3 : 2;
                sql = sql.Replace(" top " + parts[rowCountIndex], "");
                sql += " limit " + parts[rowCountIndex];
            }
            sql = sql.Replace("[", getReservedWordToken(true)).Replace("]", getReservedWordToken(false));

            if (this.EnableSQLLog)
            {
                StringBuilder sbSQL = new StringBuilder(sql + "\n");
                StackTrace st = new StackTrace(true);
                for (int i = 0; i < st.FrameCount; i++)
                {
                    StackFrame sf = st.GetFrame(i);
                    if (sf.GetFileName() != null && sf.GetFileName().Contains("Sitematik\\Library"))
                        sbSQL.AppendFormat("{0}({1}), ", sf.GetMethod().DeclaringType.Name + "." + sf.GetMethod().Name, sf.GetFileLineNumber());
                }
                SQLLog.Add(sbSQL.ToString() + "\n");
            }

            return sql;
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
            fieldMappingInfo = new Dictionary<PropertyInfo, Field>();

            if (HttpContext.Current != null)
            {
                HttpContext.Current.Application.Remove("databaseMetadata");
                HttpContext.Current.Application["databaseMetadata"] = this.tables;
            }
            else
            {
                Cache.Remove("databaseMetadata");
                Cache["databaseMetadata"] = this.tables;
            }
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
        public IDbDataAdapter CreateDataAdapter(IDbCommand selectCommand)
        {
            return dbProvider.CreateDataAdapter(selectCommand);
        }
        public IDbDataAdapter CreateDataAdapter(string selectCommandText)
        {
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
            return dbProvider.CreateParameter(parameterName, value);
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

        public void Execute(DbAction dbAction)
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
        public int Insert(string tableName, Hashtable data, bool bypassAutoIncrementField)
        {
            // validate parameters
            if (data.Count == 0) return -1;
            
            Table tbl = this.Tables[tableName];
            if (tbl == null) //throw new ApplicationException("There is no such table : " + tableName);
                this.CreateTable(tbl, null, true);

            FieldCollection fields = tbl.Fields;
            int validFieldNumber = 0;
            foreach (Field fld in tbl.Fields)
                if (data.ContainsKey(fld.Name) && !(fld.IsAutoIncrement && bypassAutoIncrementField))
                    validFieldNumber++;
            if (validFieldNumber == 0) throw new ApplicationException("The table and the hashtable have no similarity!");

            // ok valid, prepare SQL
            List<string> tmpList = new List<string>();

            StringBuilder sb = new StringBuilder();
            sb.Append("insert into [" + tableName + "] (");
            for (int i = 0; i < fields.Count; i++)
            {
                Field fld = fields[i];
                if (fld.IsAutoIncrement && bypassAutoIncrementField) continue;
                if (data.ContainsKey(fld.Name))
                {
                    // if field is reference and the value equals 0, continue
                    if (fld.ReferenceField != null && fld.IsNullable && fld.FieldType==DbType.Int32) {
                        if (data[fld.Name] == null || data[fld.Name] == DBNull.Value) data[fld.Name] = 0;
                        int refId = (int) data[fld.Name];
                        if (refId == 0) continue; //***
                    }
                    // SQLServer'da 1753'ten küçük tarihler sorun oluyor!!!
                    if (provider == DatabaseProvider.SQLServer && fld.IsDateType())
                        if (DBNull.Value.Equals(data[fld.Name]) || (DateTime)data[fld.Name] <= new DateTime(1753, 1, 1, 12, 0, 0))
                            continue;//***

                    tmpList.Add("[" + fld.Name + "]");
                }
            }
            sb.Append(String.Join(",", tmpList.ToArray()));
            tmpList.Clear();
            sb.Append(") values (");
            for (int i = 0; i < fields.Count; i++)
            {
                Field fld = fields[i];
                if (fld.IsAutoIncrement && bypassAutoIncrementField) continue;
                if (data.ContainsKey(fld.Name))
                {
                    // if field is reference and the value equals 0, continue
                    if (fld.ReferenceField != null && fld.IsNullable && fld.FieldType == DbType.Int32)
                    {
                        if (data[fld.Name] == null || data[fld.Name] == DBNull.Value) data[fld.Name] = 0;
                        int refId = (int)data[fld.Name];
                        if (refId == 0) continue; //***
                    }
                    // SQLServer'da 1753'ten küçük tarihler sorun oluyor!!!
                    if (provider == DatabaseProvider.SQLServer && fld.IsDateType())
                        if (DBNull.Value.Equals(data[fld.Name]) || (DateTime)data[fld.Name] <= new DateTime(1753, 1, 1, 12, 0, 0))
                            continue; //***

                    tmpList.Add("@_" + fld.Name);
                }
            }
            sb.Append(String.Join(",", tmpList.ToArray()));
            sb.Append(");");

            if (!bypassAutoIncrementField && provider == DatabaseProvider.SQLServer && tbl.HasAutoIncrementField())
            {
                sb.Insert(0, "SET IDENTITY_INSERT [" + tbl.Name + "] ON;" + Environment.NewLine);
                sb.Append(Environment.NewLine + "SET IDENTITY_INSERT [" + tbl.Name + "] OFF;");
            }

            IDbCommand cmd = this.CreateCommand(sb.ToString());
            for (int i = 0; i < fields.Count; i++)
            {
                Field fld = fields[i];
                if (fld.IsAutoIncrement && bypassAutoIncrementField) continue;
                if (data.ContainsKey(fld.Name))
                {
                    // if field is reference and the value equals 0, continue
                    if (fld.ReferenceField != null && fld.IsNullable && fld.FieldType == DbType.Int32)
                    {
                        if (data[fld.Name] == null || data[fld.Name] == DBNull.Value) data[fld.Name] = 0;
                        int refId = (int)data[fld.Name];
                        if (refId == 0) continue; //***
                    }
                    // SQLServer'da 1753'ten küçük tarihler sorun oluyor!!!
                    if (provider == DatabaseProvider.SQLServer && fld.IsDateType())
                        if (DBNull.Value.Equals(data[fld.Name]) || (DateTime)data[fld.Name] <= new DateTime(1753, 1, 1, 12, 0, 0))
                            continue; //***

                    IDbDataParameter param = this.CreateParameter("@_" + fld.Name, data[fld.Name]);
                    if (fld.FieldType == DbType.Image)
                        param.DbType = System.Data.DbType.Binary;
                    cmd.Parameters.Add(param);
                }
            }

            int res = this.ExecuteNonQuery(cmd);

            return res;
        }
        public int Insert(string tableName, DataRow dataRow, bool bypassAutoIncrementField)
        {
            Hashtable ht = new Hashtable();
            foreach (DataColumn dc in dataRow.Table.Columns)
                ht[dc.ColumnName] = dataRow[dc];
            return this.Insert(tableName, ht, bypassAutoIncrementField);
        }
        public int Insert(string tableName, DataRow dataRow)
        {
            return this.Insert(tableName, dataRow, true);
        }
        public int Update(string tableName, Hashtable data)
        {
            // validate parameters
            if (data.Count == 0) return -1;
            Table tbl = this.Tables[tableName];
            if (tbl == null) throw new ApplicationException("There is no such database : " + tableName);
            FieldCollection fields = tbl.Fields;
            int validFieldNumber = 0;
            foreach (Field fld in tbl.Fields)
                if (data.ContainsKey(fld.Name) && data[fld.Name] != null && !fld.IsAutoIncrement)
                    validFieldNumber++;
            if (validFieldNumber == 0) throw new ApplicationException("The table and the hashtable have no similarity!");

            StringBuilder sb = new StringBuilder();
            sb.Append("update [" + tableName + "] set ");
            for (int i = 0; i < fields.Count; i++)
            {
                Field fld = fields[i];
                if (fld.IsAutoIncrement) continue;
                if (data.ContainsKey(fld.Name) && data[fld.Name]!=null)
                {
                    // if field is reference and the value equals 0, continue
                    //if (fld.ReferenceField != null && fld.IsNullable && fld.FieldType == DbType.Int32)
                    //{
                    //    int refId = (int)data[fld.Name];
                    //    if (refId == 0) continue; //***
                    //}
                    sb.AppendFormat("[{0}] = @_{0}", fld.Name);
                    sb.Append(", ");
                }
            }
            sb.Remove(sb.Length-2,2);
            sb.AppendFormat(" where [{0}]=@_{0}", tbl.PrimaryField.Name);

            IDbCommand cmd = this.CreateCommand(sb.ToString());
            for (int i = 0; i < fields.Count; i++)
            {
                Field fld = fields[i];
                if (data.ContainsKey(fld.Name) && data[fld.Name] != null)
                {
                    // if field is reference and the value equals 0, continue
                    if (fld.ReferenceField != null && fld.IsNullable && fld.FieldType == DbType.Int32)
                    {
                        int refId = (int)data[fld.Name];
                        if (refId == 0) data[fld.Name]=DBNull.Value; //***
                    }
                    IDbDataParameter param = this.CreateParameter("@_" + fld.Name, data[fld.Name]);
                    cmd.Parameters.Add(param);
                }
            }
            int res = this.ExecuteNonQuery(cmd);

            return res;
        }
        public int Update(string tableName, DataRow dataRow)
        {
            Hashtable ht = new Hashtable();
            foreach (DataColumn dc in dataRow.Table.Columns)
                if(!dataRow.IsNull(dc))
                    ht[dc.ColumnName] = dataRow[dc];
            return this.Update(tableName, ht);
        }

        public string GetIdFieldName(IDatabaseEntity entity)
        {
            return GetIdFieldName(entity.GetType());
        }
        public string GetIdFieldName(Type entityType)
        {
            PropertyInfo pi = entityType.GetProperty("Id");
            if (fieldMappingInfo.ContainsKey(pi))
                return fieldMappingInfo[pi].Name;
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

                if (tbl == null)
                    tbl = tableMappingInfo[entity.GetType()] = this.CreateTableForType(entity.GetType());

                Hashtable ht = EntityToHashtable(entity);

                if (entity.Id == 0)
                {
                    this.Insert(tbl.Name, ht);
                    object id = this.GetValue("select max(" + GetIdFieldName(entity) + ") from [" + tbl.Name + "]");
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
                throw ex;
            }
        }
        public Hashtable EntityToHashtable(IDatabaseEntity entity)
        {
            Hashtable ht = new Hashtable();
            foreach (PropertyInfo pi in entity.GetType().GetProperties())
                if (canBeMappedToDBTable(pi))
                {
                    Field f = GetFieldForProperty(pi);
                    if (f != null)
                        ht[f.Name] = pi.GetValue(entity, null);
                }
            return ht;
        }
        public Hashtable DataRowToHashtable(DataRow dataRow)
        {
            Hashtable ht = new Hashtable();
            foreach (DataColumn dc in dataRow.Table.Columns)
                    ht[dc.ColumnName] = dataRow[dc];
            return ht;
        }

        private bool canBeMappedToDBTable(PropertyInfo pi)
        {
            return (pi.PropertyType.IsValueType || pi.PropertyType.Name == "String" || pi.PropertyType.FullName.Contains("Byte[]")) && pi.GetSetMethod() != null;
        }

        public DataRow EntityToDataRow(IDatabaseEntity entity)
        {
            DataTable dt = new DataTable();
            foreach (PropertyInfo pi in entity.GetType().GetProperties())
                if (canBeMappedToDBTable(pi))
                {
                    Field f = GetFieldForProperty(pi);
                    if (f != null)
                        dt.Columns.Add(f.Name, pi.PropertyType);
                }

            DataRow dr = dt.NewRow();
            foreach (PropertyInfo pi in entity.GetType().GetProperties())
                if (canBeMappedToDBTable(pi))
                {
                    Field f = GetFieldForProperty(pi);
                    if (f != null)
                        dr[f.Name] = pi.GetValue(entity, null);
                }

            return dr;
        }
        public IDatabaseEntity DataRowToEntity(Type entityType, DataRow dr)
        {
            if (dr == null) return null; //***

            IDatabaseEntity entity = (IDatabaseEntity)entityType.GetConstructor(Type.EmptyTypes).Invoke(null);
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
                    GetFieldForProperty(pi);

            DataRowVersion rowVersion = DataRowVersion.Default;
            if (dr.RowState == DataRowState.Deleted)
                rowVersion = DataRowVersion.Original;

            foreach (DataColumn dc in dr.Table.Columns)
            {
                string colName = dc.ColumnName;
                entity.GetOriginalValues()[colName] = dr.IsNull(dc, rowVersion) ? null : dr[colName, rowVersion];
                if (dr.IsNull(dc, rowVersion)) continue;

                if (table.Fields[colName] == null)
                {
                    entity[colName] = dr[colName, rowVersion];
                    continue;
                }

                PropertyInfo pi = GetPropertyInfoForField(table.Fields[colName]);
                if (pi == null) continue;

                object val = dr[colName, rowVersion];
                if (val.Equals(DBNull.Value))
                    val = null;

                if (pi.PropertyType.IsEnum)
                {
                    if (val!=null && val.GetType()==typeof(string))
                        val = Enum.Parse(pi.PropertyType, val.ToString());
                    else
                        val = Enum.ToObject(pi.PropertyType, val);
                }
                else
                    val = Convert.ChangeType(val, pi.PropertyType);

                pi.SetValue(entity, val, null);

                //if (pi.PropertyType == typeof(string))
                //    pi.SetValue(entity, val.ToString(), null);
                //else if (dc.DataType.Name.EndsWith("SByte"))
                //    pi.SetValue(entity, ((sbyte)val) == 1, null);
                //else
                //    pi.SetValue(entity, val, null);
            }
        }
        public void FillEntity(IDatabaseEntity entity)
        {
            string tableName = GetTableForEntityType(entity.GetType()).Name;
            this.FillEntity(entity, GetDataRow("select * from " + tableName + " where " + GetIdFieldName(entity) + "={0}", entity.Id));
        }
        public void FillDataRow(IDatabaseEntity entity, DataRow dr)
        {
            Type entityType = entity.GetType();

            foreach (PropertyInfo pi in entityType.GetProperties())
                if (canBeMappedToDBTable(pi))
                {
                    Field f = GetFieldForProperty(pi);
                    if (f != null)
                        dr[f.Name] = pi.GetValue(entity, null);
                }
        }
        #endregion

        #region read data
        public DataSet GetDataSet(DataSet ds, string sql, params object[] parameters)
        {
            sql = editSQLAsForProvider(sql);
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
        public List<T> GetList<T>(string sql, params object[] parameters)
        {
            return GetDataTable(sql, parameters).Rows.OfType<DataRow>().Select(dr=>(T)dr[0]).ToList();
        }
        public Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string sql, params object[] parameters)
        {
            DataTable dt = GetDataTable(sql, parameters);
            if(dt.Columns.Count<2)
                throw new Exception("select at least 2 columns for dictionary");

            Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
            foreach (DataRow dr in dt.Rows)
            {
                TKey key = (TKey) Convert.ChangeType(dr[0], typeof (TKey));
                if(!dict.ContainsKey(key))
                    dict.Add(key, (TValue) Convert.ChangeType(dr[1], typeof (TValue)));
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
            return Convert.ToBoolean(GetValue(sql, parameters));
        }
        public bool GetBool(string sql)
        {
            return Convert.ToBoolean(GetValue(sql));
        }
        public int GetInt(string sql, params object[] parameters)
        {
            return Convert.ToInt32(GetValue(sql, parameters));
        }
        public int GetInt(string sql)
        {
            return Convert.ToInt32(GetValue(sql));
        }

        public IDatabaseEntity Read(Type entityType, int id)
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items[entityType.Name + "_" + id] != null)
                    return (IDatabaseEntity)HttpContext.Current.Items[entityType.Name + "_" + id];
                else
                {
                    HttpContext.Current.Items[entityType.Name + "_" + id] = Read(entityType, GetIdFieldName(entityType) + "=" + id);
                    return (IDatabaseEntity)HttpContext.Current.Items[entityType.Name + "_" + id];
                }
            }
            else
            {
                return Read(entityType, "" + GetIdFieldName(entityType) + "=" + id);
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

                result = DataRowToEntity(entityType, this.GetDataRow("select * from [" + table.Name + "] where " + where, parameters));

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
            string selectSQL = "select * from [" + typeof(T).Name + "] " + filterExpression.ToParamString(); // limit-offset ToParamString() tarafından ekleniyor
            return ReadList<T>(selectSQL, filterExpression.GetParamValues());
        }
        public IDatabaseEntity[] ReadList(Type entityType, FilterExpression filterExpression)
        {
            string selectSQL = "select * from [" + entityType.Name + "] " + filterExpression.ToParamString(); // limit-offset ToParamString() tarafından ekleniyor
            return ReadList(entityType, selectSQL, filterExpression.GetParamValues());
        }
        public int ReadCount(Type entityType, FilterExpression filterExpression)
        {
            string selectSQL = "select count(*) from [" + entityType.Name + "] " + filterExpression.Criterias.ToParamString();
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
            foreach (Field field in tbl.Fields)
                if (field.ReferenceField != null)
                    sb.AppendFormat("\tleft join [{0}] as {1} ON {1}.{2} = [{3}].{4}\n", field.ReferenceField.Table.Name, "T" + field.Name, field.ReferenceField.Name, tbl.Name, field.Name);

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

            Table tbl = this.CreateTableMetadataForType(type);

            this.CreateTable(tbl, defaultDataArr, refreshMetadata);

            return this.Tables[tbl.Name];
        }

        public void CreateTable(Table tbl, DefaultDataAttribute[] defaultDataArr, bool refreshMetadata)
        {
            Database originalDb = tbl.parent.db;
            tbl.parent.db = this;

            IDbCommand cmd = this.CreateCommand(tbl.ToDDL());
            this.ExecuteNonQuery(cmd);

            if (defaultDataArr != null)
            {
                if (this.provider == DatabaseProvider.SQLServer && tbl.PrimaryField != null && tbl.PrimaryField.IsAutoIncrement)
                    this.ExecuteNonQuery("SET IDENTITY_INSERT [" + tbl.Name + "] ON");

                foreach (DefaultDataAttribute defaultData in defaultDataArr)
                    this.ExecuteNonQuery("insert into [" + tbl.Name + "](" + defaultData.FieldList + ") values(" + defaultData.ValueList + ")");

                if (this.provider == DatabaseProvider.SQLServer && tbl.PrimaryField != null && tbl.PrimaryField.IsAutoIncrement)
                    this.ExecuteNonQuery("SET IDENTITY_INSERT [" + tbl.Name + "] OFF");
            }

            tbl.parent.db = originalDb;
            
            if(refreshMetadata)
                this.Refresh();
        }

        public Table CreateTableMetadataForType(Type type)
        {
            object[] attribs = type.GetCustomAttributes(typeof(TableDetailAttribute), false);
            TableDetailAttribute tableProps = new TableDetailAttribute();
            if (attribs.Length > 0) tableProps = (TableDetailAttribute)attribs[0];

            Table tbl = new Table();
            tbl.IsView = false;
            tbl.Name = string.IsNullOrEmpty(tableProps.Name) ? type.Name : tableProps.Name;
            tbl.Fields = new FieldCollection(tbl);
            foreach (PropertyInfo pi in type.GetProperties())
                if (canBeMappedToDBTable(pi))
                {
                    Field f = new Field();
                    tbl.Fields.Add(f);
                    setFieldPropsFromPropertyInfo(f, pi, tbl);
                }
            this.Tables.Add(tbl);
            return tbl;
        }
        private void setFieldPropsFromPropertyInfo(Field f, PropertyInfo pi, Table forTable)
        {
            object[] attribs = pi.GetCustomAttributes(typeof(FieldDetailAttribute), true);
            FieldDetailAttribute fieldProps = new FieldDetailAttribute();
            if (attribs.Length > 0) fieldProps = (FieldDetailAttribute)attribs[0];

            f.Name = string.IsNullOrEmpty(fieldProps.Name) ? pi.Name : fieldProps.Name;
            f.DefaultValue = fieldProps.DefaultValue;
            if (fieldProps.FieldType == DbType.Undefined)
                f.FieldType = GetDbType(pi.PropertyType);
            else
                f.FieldType = fieldProps.FieldType;
            f.IsAutoIncrement = fieldProps.IsAutoIncrement;
            f.IsNullable = !fieldProps.IsNotNull;
            if (fieldProps.IsPrimaryKey)
            {
                if (f.Table.Keys == null) f.Table.Keys = new KeyCollection(f.Table);
                Key key = new Key();
                key.FieldNames = new List<string> { f.Name };
                key.IsPrimary = true;
                key.IsUnique = true;
                key.Name = "PK_" + f.Name;
                f.Table.Keys.Add(key);
            }
            f.Length = fieldProps.Length;

            //TODO: burada field'ın refer ettiği tablo create ediliyordu ama stack overflow'a neden olduğu için kaldırıldı.

            //if (fieldProps.References != null) { 
            //    Table referencedTable = this.Tables[fieldProps.References.Name];
            //    if (referencedTable == null){
            //        if (fieldProps.References.Name != forTable.Name)
            //        {
            //            Table newTable = CreateTableForType(fieldProps.References);
            //            f.ReferenceField = newTable.PrimaryField;
            //        }else
            //        {
            //            f.ReferenceField = forTable.PrimaryField;
            //        }
            //    }
            //}
        }
        public DbType GetDbType(Type type)
        {
            DbType res = DbType.VarChar;

            if (type.IsEnum)
                return DbType.Int32;

            if (type.FullName.Contains("Byte[]"))
                return DbType.BlobLong;

            switch (type.Name)
            {
                case "Int16":
                    res = DbType.Int16;
                    break;
                case "Int32":
                    res = DbType.Int32;
                    break;
                case "Int64":
                    res = DbType.Int64;
                    break;
                case "Boolean":
                    res = DbType.Boolean;
                    break;
                case "Byte":
                    break;
                case "String":
                    res = DbType.VarChar;
                    break;
                case "DateTime":
                    res = DbType.DateTime;
                    break;
                case "Decimal":
                    res = DbType.Decimal;
                    break;
                case "Float":
                    res = DbType.Float;
                    break;
                case "Double":
                    res = DbType.Double;
                    break;
            }
            return res;
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
        //TODO: clear these dictionaries on refresh metadata
        private Dictionary<Type, Table> tableMappingInfo = new Dictionary<Type, Table>();
        private Dictionary<PropertyInfo, Field> fieldMappingInfo = new Dictionary<PropertyInfo, Field>();
        public Table GetTableForEntityType(Type entityType)
        {
            if (tableMappingInfo.ContainsKey(entityType))
                return tableMappingInfo[entityType];

            object[] attribs = entityType.GetCustomAttributes(typeof(TableDetailAttribute), false);
            if (attribs != null && attribs.Length >= 1 && !string.IsNullOrEmpty((attribs[0] as TableDetailAttribute).Name))
                tableMappingInfo[entityType] = this.Tables[(attribs[0] as TableDetailAttribute).Name];
            if (!tableMappingInfo.ContainsKey(entityType))
                tableMappingInfo[entityType] = this.Tables[entityType.Name];
            // Bunu yapmışız ama veritabanında henüz hiç tablo yokken, tabloların otomatik create edilebilmesi için commentlemem icap etti..
            //if (tableMappingInfo[entityType] == null)
            //    throw new Exception(entityType.Name + " isimli tablo bulunamadı!");
            return tableMappingInfo[entityType];
            //return tableMappingInfo.ContainsKey(entityType) ? tableMappingInfo[entityType] : null;
        }
        public Field GetFieldForProperty(PropertyInfo propertyInfo)
        {
            if (fieldMappingInfo.ContainsKey(propertyInfo))
                return fieldMappingInfo[propertyInfo];

            Table table = GetTableForEntityType(propertyInfo.ReflectedType);
            if (table == null)
                return null;

            object[] attribs = propertyInfo.GetCustomAttributes(typeof(FieldDetailAttribute), false);
            if (attribs != null && attribs.Length >= 1 && !string.IsNullOrEmpty((attribs[0] as FieldDetailAttribute).Name))
                fieldMappingInfo[propertyInfo] = table.Fields[(attribs[0] as FieldDetailAttribute).Name];
            if (!fieldMappingInfo.ContainsKey(propertyInfo))
                fieldMappingInfo[propertyInfo] = table.Fields[propertyInfo.Name];
            if (fieldMappingInfo[propertyInfo] == null)
            {
                //throw new Exception(propertyInfo.ReflectedType.Name + " veya " + propertyInfo.DeclaringType.Name + " isimli tabloya ait " + propertyInfo.Name + " isimli alan bulunamadı!");
            }
            return fieldMappingInfo[propertyInfo];
            //return fieldMappingInfo.ContainsKey(propertyInfo) ? fieldMappingInfo[propertyInfo] : null;
        }
        public PropertyInfo GetPropertyInfoForField(Field field)
        {
            if(fieldMappingInfo.ContainsValue(field))
                return fieldMappingInfo.First(p => p.Value == field).Key;
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
                            sb.AppendLine("DROP TABLE IF EXISTS " + tbl.Name + ";");
                            break;
                        case DatabaseProvider.SQLServer:
                            sb.AppendLine("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[" + tbl.Name + "]') AND type in (N'U')) DROP TABLE [" + tbl.Name + "];");
                            break;
                        case DatabaseProvider.PostgreSQL:
                            //TODO: add drop table syntax for postgre...
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
                default:
                    throw new ApplicationException("It is not that much provided.");
            }

            return dbPrvdr.GetTableDDL(table);
        }
        public string GetFieldDDL(Field field)
        {
            return dbProvider.GetFieldDDL(field);
        }

        public void AlterTableRename(string oldName, string newName)
        {
            switch (Provider)
            {
                case DatabaseProvider.PostgreSQL:
                case DatabaseProvider.MySQL:
                    this.ExecuteNonQuery("ALTER TABLE [" + oldName + "] RENAME TO [" + newName + "]");
                    break;
                case DatabaseProvider.SQLServer:
                    this.ExecuteNonQuery("EXEC sp_rename {0}, {1}", oldName, newName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public void AlterTableRenameColumn(string tableName, string oldColumnName, string newColumnName)
        {
            switch (Provider)
            {
                case DatabaseProvider.PostgreSQL:
                    this.ExecuteNonQuery("ALTER TABLE [" + tableName + "] RENAME [" + oldColumnName + "] TO [" + newColumnName + "]");
                    break;
                case DatabaseProvider.MySQL:
                    this.ExecuteNonQuery("ALTER TABLE [" + tableName + "] CHANGE [" + oldColumnName + "] " + GetFieldDDL(Tables[tableName].Fields[oldColumnName]).Replace(oldColumnName, newColumnName));
                    break;
                case DatabaseProvider.SQLServer:
                    this.ExecuteNonQuery(string.Format("EXEC sp_rename @objname = '{0}.{1}', @newname = '{2}', @objtype = 'COLUMN'", tableName, oldColumnName, newColumnName));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public void AlterTableAddColumn(Field field)
        {
            this.ExecuteNonQuery("alter table [" + field.Table.Name + "] add " + GetFieldDDL(field));
        }
        public void AlterTableDropColumn(Field field)
        {
            this.ExecuteNonQuery("alter table [" + field.Table.Name + "] drop [" + field.Name + "]");
        }

        public void AlterTableChangeColumn(Field field)
        {
            switch (Provider)
            {
                case DatabaseProvider.PostgreSQL:
                    throw new Exception("Alter column is not implemented for Postgre SQL!");
                case DatabaseProvider.MySQL:
                    this.ExecuteNonQuery("alter table [" + field.Table.Name + "] modify column " + GetFieldDDL(field));
                    break;
                case DatabaseProvider.SQLServer:
                    this.ExecuteNonQuery("alter table [" + field.Table.Name + "] alter column " + GetFieldDDL(field));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

    }

    public delegate void DbAction();
}
