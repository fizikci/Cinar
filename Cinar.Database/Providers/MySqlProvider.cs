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
    internal class MySqlProvider : IDatabaseProvider
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
            db.Tables = new TableCollection(db);

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
                    f.FieldType = StringToDbType(drColumn["DATA_TYPE"].ToString());
                    f.Length = drColumn.IsNull("CHARACTER_MAXIMUM_LENGTH") ? 0 : Convert.ToInt64(drColumn["CHARACTER_MAXIMUM_LENGTH"]);
                    f.IsNullable = drColumn["IS_NULLABLE"].ToString() != "NO";
                    f.Name = drColumn["COLUMN_NAME"].ToString();
                    f.IsAutoIncrement = drColumn["IS_AUTO_INCREMENT"].ToString() == "1";

                    tbl.Fields.Add(f);
                }

                // keys
                tbl.Keys = new KeyCollection(tbl);
                DataTable dtKeys = db.GetDataTable("SHOW INDEXES FROM `" + tbl.Name + "`");
                if (dtKeys != null)
                    for (int i = 0; i < dtKeys.Rows.Count; i++)
                    {
                        DataRow drKey = dtKeys.Rows[i];
                        Key key = tbl.Keys[drKey["Key_name"].ToString()] ?? new Key();
                        key.Name = drKey["Key_name"].ToString();
                        if (key.FieldNames == null)
                            key.FieldNames = new List<string>();
                        key.FieldNames.Add(drKey["Column_name"].ToString());
                        key.IsUnique = drKey["Non_unique"].ToString() == "0";
                        key.IsPrimary = key.Name == "PRIMARY";

                        if (tbl.Keys[key.Name] == null)
                            tbl.Keys.Add(key);
                    }
            }

            // keys
            //DataTable dtKeys = db.GetDataTable(String.Format(this.SQLPrimaryKeys, con.Database));
            //foreach (DataRow drKey in dtKeys.Rows)
            //    db.Tables[drKey["TABLE_NAME"].ToString()].Fields[drKey["COLUMN_NAME"].ToString()].IsPrimaryKey = true;
            
            // foreign keys
            DataTable dtForeigns = db.GetDataTable(String.Format(this.SQLForeignKeys, con.Database));
            foreach (DataRow drForeign in dtForeigns.Rows)
                db.Tables[drForeign["TABLE_NAME_1"].ToString()]
                    .Fields[drForeign["COLUMN_NAME_1"].ToString()].ReferenceFieldName =
                        drForeign["TABLE_NAME_2"] + "." + drForeign["COLUMN_NAME_2"];

        }

        /// <summary>
        /// Veritabanýndan string olarak gelen field tip bilgisini DbType enum'una dönüþtürür.
        /// </summary>
        public DbType StringToDbType(string typeName)
        {
            foreach (var elm in DEFStringToDbType)
                if (elm.Key.ToLowerInvariant().Equals(typeName.ToLowerInvariant()))
                    return elm.Value;
            return DbType.Undefined;
        }

        /// <summary>
        /// DbType olarak elimizde bulunan field tip bilgisini veritabanýn anlayacaðý stringe dönüþtürür. 
        /// </summary>
        public string DbTypeToString(DbType dbType)
        {
            if (dbType == DbType.Boolean) return "tinyint(1)";
            foreach (var elm in DEFDbTypeToString)
                if (elm.Key == dbType)
                    return elm.Value;
            return "???";
        }

        public static Dictionary<DbType, string> DEFDbTypeToString = new Dictionary<DbType, string>() 
        { 
            {DbType.Binary, "Binary"},
            {DbType.Blob , "Blob"},
            {DbType.BlobLong , "LongBlob"},
            {DbType.BlobMedium, "MediumBlob"},
            {DbType.BlobTiny, "TinyBlob"},
            {DbType.Boolean, "Bit"},
            {DbType.Byte, "Tinyint"},
            {DbType.Char, "Char"},
            {DbType.Currency, "Decimal"},
            {DbType.CurrencySmall, "Decimal"},
            {DbType.Date, "Date"},
            {DbType.DateTime, "DateTime"},
            {DbType.DateTimeSmall, "SmallDateTime"},
            {DbType.Decimal, "Decimal"},
            {DbType.Double, "Double"},
            {DbType.Enum, "varchar"},
            {DbType.Float, "Float"},
            {DbType.Guid, "varchar"},
            {DbType.Image, "Blob"},
            {DbType.Int16, "SmallInt"},
            {DbType.Int32, "Int"},
            {DbType.Int64, "Bigint"},
            {DbType.NChar, "Char"},
            {DbType.NText, "Text"},
            {DbType.Numeric, "Numeric"},
            {DbType.NVarChar, "VarChar"},
            {DbType.Real, "Real"},
            {DbType.Set, "Set"},
            {DbType.Text, "Text"},
            {DbType.TextLong, "LongText"},
            {DbType.TextMedium, "MediumText"},
            {DbType.TextTiny, "TinyText"},
            {DbType.Time, "Time"},
            {DbType.Timestamp, "timestamp"},
            {DbType.Timestamptz, "Timestamp"},
            {DbType.Timetz, "Time"},
            {DbType.Undefined, "???"},
            {DbType.VarBinary, "Binary"},
            {DbType.VarChar, "VarChar"},
            {DbType.Variant, "Blob"},
            {DbType.Xml, "text"}
        };

        public static Dictionary<string, DbType> DEFStringToDbType = new Dictionary<string, DbType>() 
        { 
            {"Binary", DbType.Binary},
            {"Blob", DbType.Blob},
            {"LongBlob", DbType.BlobLong},
            {"MediumBlob", DbType.BlobMedium},
            {"TinyBlob", DbType.BlobTiny},
            {"Bit", DbType.Boolean},
            {"Tinyint", DbType.Byte},
            {"Char", DbType.Char},
            {"Date", DbType.Date},
            {"DateTime", DbType.DateTime},
            {"SmallDateTime", DbType.DateTimeSmall},
            {"Decimal", DbType.Decimal},
            {"Enum", DbType.VarChar},
            {"Float", DbType.Float},
            {"Double", DbType.Double},
            {"SmallInt", DbType.Int16},
            {"MediumInt", DbType.Int32},
            {"Int", DbType.Int32},
            {"Bigint", DbType.Int64},
            {"Numeric", DbType.Numeric},
            {"Real", DbType.Real},
            {"Set", DbType.Set},
            {"Text", DbType.Text},
            {"LongText", DbType.TextLong},
            {"MediumText", DbType.TextMedium},
            {"TinyText", DbType.TextTiny},
            {"Time", DbType.Time},
            {"Timestamp", DbType.Timestamp},
            {"VarChar", DbType.VarChar}
        };


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
												DATA_TYPE,
												CHARACTER_MAXIMUM_LENGTH,
												IS_NULLABLE,
												COLUMN_DEFAULT,
                                                CASE WHEN EXTRA LIKE '%auto_increment%' THEN 1 ELSE 0 END AS IS_AUTO_INCREMENT
											from
												INFORMATION_SCHEMA.COLUMNS
											WHERE
												TABLE_NAME='{0}' and TABLE_SCHEMA='{1}'";
        // Tablolara Primary Key Alanlarý
        private string SQLPrimaryKeys = @"
											SELECT
												TBL1.CONSTRAINT_NAME,
												TBL2.TABLE_NAME,
												TBL2.COLUMN_NAME
											FROM
												(select CONSTRAINT_NAME, TABLE_NAME from INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_TYPE='PRIMARY KEY' AND CONSTRAINT_SCHEMA='{0}') AS TBL1,
												(select TABLE_NAME, COLUMN_NAME, CONSTRAINT_NAME from INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE CONSTRAINT_SCHEMA='{0}') AS TBL2
											WHERE
												TBL2.TABLE_NAME = TBL1.TABLE_NAME;";
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
                cmd.Parameters.Add("@_param" + i, parameters[i]);

            return cmd;
        }

        public System.Data.IDbDataParameter CreateParameter(string parameterName, object value)
        {
            return new MySqlParameter(parameterName, value);
        }

        public string GetTableDDL(Table table)
        {
            int len = ("," + Environment.NewLine).Length;

            StringBuilder sbFields = new StringBuilder();
            foreach (Field f in table.Fields.OrderBy(f=>f.Name))
                sbFields.Append("\t" + GetFieldDDL(f) + "," + Environment.NewLine);

            foreach (Key k in table.Keys.OrderBy(k=>k.Name))
                sbFields.Append("\t" + GetIndexKeyDDL(k) + "," + Environment.NewLine);

            sbFields = sbFields.Remove(sbFields.Length - len, len);

            return String.Format("CREATE {0} `{1}`(\n{2});" + Environment.NewLine,
                (table.IsView ? "VIEW" : "TABLE"),
                table.Name,
                sbFields.ToString());
        }

        public string GetIndexKeyDDL(Key key)
        {
            if (key.IsPrimary)
                return "PRIMARY KEY (`" + String.Join("`, `", key.Fields.ToStringArray()) + "`)";
            else if (key.IsUnique)
                return "UNIQUE KEY `" + key.Name + "` (`" + String.Join("`, `", key.Fields.ToStringArray()) + "`)";
            else
                return "KEY `" + key.Name + "` (`" + String.Join("`, `", key.Fields.ToStringArray()) + "`)";
        }

        public string GetFieldDDL(Field field)
        {
            StringBuilder fieldDDL = new StringBuilder();
            string fieldTypeName = field.Table.Database.dbProvider.DbTypeToString(field.FieldType).ToLowerInvariant();
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
                fieldDDL.Append(" REFERENCES `" + field.ReferenceField.Table.Name + "`(" + field.ReferenceField.Name + ")");

            return fieldDDL.ToString();
        }

        #endregion
    }

    internal enum DbType4MySQL
    {
        Bigint = DbType.Int64,
        Binary = DbType.Binary,
        Bit = DbType.Boolean,
        Blob = DbType.Blob,
        Tinyint = DbType.Boolean,
        Char = DbType.Char,
        Date = DbType.Date,
        Datetime = DbType.DateTime,
        Decimal = DbType.Decimal,
        Double = DbType.Double,
        Float = DbType.Float,
        Int = DbType.Int32,
        Longblob = DbType.BlobLong,
        Longtext = DbType.TextLong,
        Mediumblob = DbType.BlobMedium,
        Mediumint = DbType.Int32,
        Mediumtext = DbType.TextMedium,
        Smallint = DbType.Int16,
        Text = DbType.Text,
        Time = DbType.Time,
        Timestamp = DbType.Timestamp,
        Tinyblob = DbType.BlobTiny,
        Tinytext = DbType.TextTiny,
        Varchar = DbType.VarChar,
        Set = DbType.Set,
        Enum = DbType.Enum
    }

}
