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
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Cinar.Database.Providers
{
    /// <summary>
    /// MySQL'den metadata okuyan sýnýf
    /// </summary>
    public class MySqlProvider : IDatabaseProvider
    {
        private System.Data.IDbConnection con;
        public System.Data.IDbConnection Connection
        {
            get { return con; }
        }

        private Database db = null;

        public MySqlProvider(Database db, bool createDatabaseIfNotExist)
        {
            this.db = db;
            try
            {
                con = new MySqlConnection(db.ConnectionString);
                if (createDatabaseIfNotExist)
                {
                    con.Open();
                    con.Close();
                }
            }
            catch
            {
                if (createDatabaseIfNotExist)
                {
                    // ihtimal, veritabaný create edilmemiþ. create edelim o zaman:
                    string newConnStr = "";
                    string dbName = "";
                    foreach (string param in db.ConnectionString.Split(';'))
                    {
                        if (param.StartsWith("Database=", StringComparison.InvariantCultureIgnoreCase))
                            dbName = param.Split('=')[1];
                        else
                            newConnStr += param + ";";
                    }
                    con = new MySqlConnection(newConnStr);
                    try
                    {
                        con.Open();
                        IDbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "create database " + dbName + " default charset utf8 collate utf8_turkish_ci";
                        cmd.ExecuteNonQuery();
                        con.ChangeDatabase(dbName);
                        con.Close();
                    }
                    catch { }
                }
            }
        }
        

        /// <summary>
        /// Metadata okuma iþini yapan asýl metod. Sýrayla bütün veritabaný nesnelerini okur.
        /// </summary>
        /// <param name="db"></param>
        public void ReadDatabaseMetadata()
        {
            // tables and views
            db.Tables = new TableCollection((Database)db);

            // columns
            DataTable dtTables = db.GetDataTable(String.Format(this.SQLTables, con.Database));
            foreach (DataRow drTable in dtTables.Rows)
            {
                Table tbl = new Table();
                tbl.Name = drTable["TABLE_NAME"].ToString();
                tbl.IsView = drTable["TABLE_TYPE"].ToString() == "VIEW";
                db.Tables.Add(tbl);

                tbl.Fields = new FieldCollection(tbl);

                DataTable dtColumns = db.GetDataTable(String.Format(this.SQLFields, tbl.Name, con.Database));
                foreach (DataRow drColumn in dtColumns.Rows)
                {
                    Field f = new Field();
                    f.DefaultValue = drColumn["COLUMN_DEFAULT"].ToString();
                    f.FieldTypeOriginal = drColumn["COLUMN_TYPE"].ToString()=="tinyint(1)" ? "BOOL" : drColumn["DATA_TYPE"].ToString().ToUpperInvariant();
                    f.FieldType = StringToDbType(f.FieldTypeOriginal);
                    f.Length = drColumn.IsNull("CHARACTER_MAXIMUM_LENGTH") ? 0 : Convert.ToInt64(drColumn["CHARACTER_MAXIMUM_LENGTH"]);
                    f.IsNullable = drColumn["IS_NULLABLE"].ToString() != "NO";
                    f.Name = drColumn["COLUMN_NAME"].ToString();
                    f.IsAutoIncrement = drColumn["IS_AUTO_INCREMENT"].ToString() == "1";

                    tbl.Fields.Add(f);
                }

                // indices
                tbl.Indices = new IndexCollection(tbl);
                DataTable dtKeys = db.GetDataTable("SHOW INDEXES FROM `" + tbl.Name + "`");
                if (dtKeys != null)
                    for (int i = 0; i < dtKeys.Rows.Count; i++)
                    {
                        DataRow drKey = dtKeys.Rows[i];
                        Index index = tbl.Indices[drKey["Key_name"].ToString()] ?? new Index();
                        index.Name = drKey["Key_name"].ToString();
                        if (index.FieldNames == null)
                            index.FieldNames = new List<string>();
                        index.FieldNames.Add(drKey["Column_name"].ToString());
                        index.IsUnique = drKey["Non_unique"].ToString() == "0";
                        index.IsPrimary = index.Name == "PRIMARY";

                        if (tbl.Indices[index.Name] == null)
                            tbl.Indices.Add(index);
                    }
            }

            // indices
            //DataTable dtKeys = db.GetDataTable(String.Format(this.SQLPrimaryKeys, con.Database));
            //foreach (DataRow drKey in dtKeys.Rows)
            //    db.Tables[drKey["TABLE_NAME"].ToString()].Fields[drKey["COLUMN_NAME"].ToString()].IsPrimaryKey = true;
            
            // foreign indices
            DataTable dtForeigns = db.GetDataTable(String.Format(this.SQLForeignKeys, con.Database));
            foreach (DataRow drForeign in dtForeigns.Rows)
                db.Tables[drForeign["TABLE_NAME_1"].ToString()]
                    .Fields[drForeign["COLUMN_NAME_1"].ToString()].ReferenceFieldName =
                        drForeign["TABLE_NAME_2"] + "." + drForeign["COLUMN_NAME_2"];

        }

        #region string <=> dbType conversion

        /// <summary>
        /// Veritabanýndan string olarak gelen field tip bilgisini DbType enum'una dönüþtürür.
        /// </summary>
        public DbType StringToDbType(string typeName)
        {
            if (DEFStringToDbType.ContainsKey(typeName.ToUpperInvariant()))
                return DEFStringToDbType[typeName];
            return DbType.Undefined;
        }

        /// <summary>
        /// DbType olarak elimizde bulunan field tip bilgisini veritabanýn anlayacaðý stringe dönüþtürür. 
        /// </summary>
        public string DbTypeToString(DbType dbType)
        {
            if (DEFDbTypeToString.ContainsKey(dbType))
                return DEFDbTypeToString[dbType];
            return "???";
        }

        public static Dictionary<DbType, string> DEFDbTypeToString = new Dictionary<DbType, string>() 
        { 
            {DbType.Binary, "BINARY"},
            {DbType.Blob , "BLOB"},
            {DbType.BlobLong , "LONGBLOB"},
            {DbType.BlobMedium, "MEDIUMBLOB"},
            {DbType.BlobTiny, "TINYBLOB"},
            {DbType.Boolean, "BOOL"},
            {DbType.Byte, "TINYINT"},
            {DbType.Char, "CHAR"},
            {DbType.Currency, "DECIMAL"},
            {DbType.CurrencySmall, "DECIMAL"},
            {DbType.Date, "DATE"},
            {DbType.DateTime, "DATETIME"},
            {DbType.DateTimeSmall, "SMALLDATETIME"},
            {DbType.Decimal, "DECIMAL"},
            {DbType.Double, "DOUBLE"},
            {DbType.Enum, "VARCHAR"},
            {DbType.Float, "FLOAT"},
            {DbType.Guid, "VARCHAR"},
            {DbType.Image, "BLOB"},
            {DbType.Int16, "SMALLINT"},
            {DbType.Int32, "INT"},
            {DbType.Int64, "BIGINT"},
            {DbType.NChar, "CHAR"},
            {DbType.NText, "TEXT"},
            {DbType.Numeric, "NUMERIC"},
            {DbType.NVarChar, "VARCHAR"},
            {DbType.Real, "REAL"},
            {DbType.Set, "SET"},
            {DbType.Text, "TEXT"},
            {DbType.TextLong, "LONGTEXT"},
            {DbType.TextMedium, "MEDIUMTEXT"},
            {DbType.TextTiny, "TINYTEXT"},
            {DbType.Time, "TIME"},
            {DbType.Timestamp, "TIMESTAMP"},
            {DbType.Timestamptz, "TIMESTAMP"},
            {DbType.Timetz, "TIME"},
            {DbType.Undefined, "???"},
            {DbType.VarBinary, "BINARY"},
            {DbType.VarChar, "VARCHAR"},
            {DbType.Variant, "BLOB"},
            {DbType.Xml, "TEXT"}
        };

        public static Dictionary<string, DbType> DEFStringToDbType = new Dictionary<string, DbType>() 
        { 
            {"BINARY", DbType.Binary},
            {"BLOB", DbType.Blob},
            {"LONGBLOB", DbType.BlobLong},
            {"MEDIUMBLOB", DbType.BlobMedium},
            {"TINYBLOB", DbType.BlobTiny},
            {"BIT", DbType.Binary},
            {"BOOL", DbType.Boolean},
            {"TINYINT", DbType.Byte},
            {"CHAR", DbType.Char},
            {"DATE", DbType.Date},
            {"DATETIME", DbType.DateTime},
            {"SMALLDATETIME", DbType.DateTimeSmall},
            {"DECIMAL", DbType.Decimal},
            {"ENUM", DbType.VarChar},
            {"FLOAT", DbType.Float},
            {"DOUBLE", DbType.Double},
            {"SMALLINT", DbType.Int16},
            {"MEDIUMINT", DbType.Int32},
            {"INT", DbType.Int32},
            {"BIGINT", DbType.Int64},
            {"NUMERIC", DbType.Numeric},
            {"REAL", DbType.Real},
            {"SET", DbType.Set},
            {"TEXT", DbType.Text},
            {"LONGTEXT", DbType.TextLong},
            {"MEDIUMTEXT", DbType.TextMedium},
            {"TINYTEXT", DbType.TextTiny},
            {"TIME", DbType.Time},
            {"TIMESTAMP", DbType.Timestamp},
            {"VARCHAR", DbType.VarChar}
        };

        public string[] GetFieldTypes() {
            return DEFStringToDbType.Keys.ToArray();
        }

        #endregion

        #region INFORMATION_SCHEMA SQL ÝFADELERÝ
        // tablolar
        private string SQLTables = @"
											select
												TABLE_NAME, TABLE_TYPE
											from
												INFORMATION_SCHEMA.TABLES
                                            where
                                                TABLE_SCHEMA='{0}'";
        // bir tablonun fieldlarý
        private string SQLFields = @"
											select
												COLUMN_NAME,
                                                COLUMN_TYPE,
												DATA_TYPE,
												CHARACTER_MAXIMUM_LENGTH,
												IS_NULLABLE,
												COLUMN_DEFAULT,
                                                CASE WHEN EXTRA LIKE '%auto_increment%' THEN 1 ELSE 0 END AS IS_AUTO_INCREMENT
											from
												INFORMATION_SCHEMA.COLUMNS
											WHERE
												TABLE_NAME='{0}' and TABLE_SCHEMA='{1}'";
        // Foreyn kiyler
        private string SQLForeignKeys = @"
                                            SELECT
	                                            TBL1.CONSTRAINT_NAME,
	                                            TBL2.TABLE_NAME as TABLE_NAME_1,
	                                            TBL2.COLUMN_NAME as COLUMN_NAME_1,
	                                            TBL2.REFERENCED_TABLE_NAME as TABLE_NAME_2,
	                                            TBL2.REFERENCED_COLUMN_NAME as COLUMN_NAME_2
                                            FROM
	                                            (select *  from INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE TABLE_SCHEMA='{0}' AND CONSTRAINT_TYPE='FOREIGN KEY') AS TBL1,
	                                            (select * from INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_SCHEMA='{0}') AS TBL2
                                            WHERE
	                                            TBL2.CONSTRAINT_NAME = TBL1.CONSTRAINT_NAME";
        #endregion

        #region IDatabaseProvider Members

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(db.ConnectionString);
        }

        public DbDataAdapter CreateDataAdapter(System.Data.IDbCommand selectCommand)
        {
            return new MySqlDataAdapter((MySqlCommand) selectCommand);
        }

        public DbDataAdapter CreateDataAdapter(string selectCommandText, params object[] parameters)
        {
            MySqlCommand selectCommand = (MySqlCommand)this.CreateCommand(selectCommandText, parameters);
            return this.CreateDataAdapter(selectCommand);
        }

        public IDbCommand CreateCommand(string cmdText, params object[] parameters)
        {
            return this.CreateCommand(cmdText, (IDbTransaction)null, parameters);
        }

        public IDbCommand CreateCommand(string cmdText, System.Data.IDbTransaction transaction, params object[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
                cmdText = cmdText.Replace("{" + i + "}", "@_param" + i);

            MySqlCommand cmd = null;
            if (transaction != null)
                cmd = new MySqlCommand(cmdText, (MySqlConnection)transaction.Connection, (MySqlTransaction)transaction);
            else
                cmd = new MySqlCommand(cmdText, (MySqlConnection)this.Connection);

            for (int i = 0; i < parameters.Length; i++)
                cmd.Parameters.AddWithValue("@_param" + i, parameters[i]);

            return cmd;
        }

        public System.Data.IDbDataParameter CreateParameter(string parameterName, object value)
        {
            return new MySqlParameter(parameterName, value);
        }

        public string GetTableDDL(Table table)
        {
            checkTable(table);

            int len = ("," + Environment.NewLine).Length;

            StringBuilder sbFields = new StringBuilder();
            foreach (Field f in table.Fields.OrderBy(f=>f.Name))
                sbFields.Append("\t" + GetFieldDDL(f) + "," + Environment.NewLine);

            foreach (Index k in table.Indices.OrderBy(k=>k.Name))
                sbFields.Append("\t" + GetIndexKeyDDL(k) + "," + Environment.NewLine);

            sbFields = sbFields.Remove(sbFields.Length - len, len);

            return String.Format("CREATE {0} `{1}`(\n{2});" + Environment.NewLine,
                (table.IsView ? "VIEW" : "TABLE"),
                table.Name,
                sbFields.ToString());
        }

        private void checkTable(Table table)
        {
            Field autoIncrementField = table.Fields.Find(f => f.IsAutoIncrement);
            Index primaryKeyIndex = table.Indices == null ? null : table.Indices.Find(k => k.IsPrimary);
            if (autoIncrementField != null && (primaryKeyIndex == null || !primaryKeyIndex.FieldNames.Contains(autoIncrementField.Name)))
            {
                if (table.Indices == null) table.Indices = new IndexCollection(table);
                if (primaryKeyIndex == null)
                    table.Indices.Add(new Index { FieldNames = new List<string> { autoIncrementField.Name }, IsPrimary = true, IsUnique = true, Name = "pk_" + table.Name });
                else
                    primaryKeyIndex.FieldNames.Add(autoIncrementField.Name);
            }
        }

        public string GetIndexKeyDDL(Index index)
        {
            if (index.IsPrimary)
                return "PRIMARY KEY (`" + String.Join("`, `", index.Fields.ToStringArray()) + "`)";
            else if (index.IsUnique)
                return "UNIQUE KEY `" + index.Name + "` (`" + String.Join("`, `", index.Fields.ToStringArray()) + "`)";
            else
                return "KEY `" + index.Name + "` (`" + String.Join("`, `", index.Fields.ToStringArray()) + "`)";
        }

        public string GetFieldDDL(Field field)
        {
            StringBuilder fieldDDL = new StringBuilder();
            string fieldTypeName = DbTypeToString(field.FieldType).ToLowerInvariant();
            fieldDDL.Append("`" + field.Name + "` " + fieldTypeName);
            if (fieldTypeName == "char" || fieldTypeName == "varchar" || fieldTypeName == "nchar" || fieldTypeName == "nvarchar")
                fieldDDL.Append("(" + (field.Length == 0 ? 50 : field.Length) + ")");
            if (field.FieldType == DbType.VarChar || field.FieldType == DbType.Text)
                fieldDDL.Append(" CHARACTER SET utf8 COLLATE utf8_turkish_ci");
            if (field.IsAutoIncrement)
                fieldDDL.Append(" AUTO_INCREMENT");
            if (!field.IsNullable)
                fieldDDL.Append(" NOT NULL");
            //if (field.IsPrimaryKey)
            //    fieldDDL.Append(" PRIMARY KEY");
            if (!string.IsNullOrEmpty(field.DefaultValue))
                fieldDDL.Append(" DEFAULT '" + field.DefaultValue + "'");
            if (field.ReferenceField != null)
                fieldDDL.Append(" REFERENCES `" + field.ReferenceField.Table.Name + "`(`" + field.ReferenceField.Name + "`)");

            return fieldDDL.ToString();
        }

        #endregion
    }

}
