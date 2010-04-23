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
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Cinar.Database.Providers
{
    /// <summary>
    /// Microsoft SQL Server'dan metadata okuyan sınıf
    /// </summary>
    internal class SQLServerProvider : IDatabaseProvider
    {
        private System.Data.IDbConnection con;
        public System.Data.IDbConnection Connection
        {
            get { return con; }
        }

        private Database db = null;

        public SQLServerProvider(Database db, bool createDatabaseIfNotExist)
        {
            this.db = db;
            try
            {
                con = new SqlConnection(db.ConnectionString);
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
                    // ihtimal, veritabanı create edilmemiş. create edelim o zaman:
                    string newConnStr = "";
                    string dbName = "";
                    foreach (string param in db.ConnectionString.Split(';'))
                    {
                        if (param.StartsWith("Initial Catalog=", StringComparison.InvariantCultureIgnoreCase))
                            dbName = param.Split('=')[1];
                        else
                            newConnStr += param + ";";
                    }
                    con = new SqlConnection(newConnStr);
                    try
                    {
                        con.Open();
                        IDbCommand cmd = con.CreateCommand();
                        cmd.CommandText = "create database " + dbName + ";";
                        cmd.ExecuteNonQuery();
                        con.ChangeDatabase(dbName);
                        con.Close();
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Metada okuma işini yapan asıl metod. Sırayla bütün veritabanı nesnelerini okur.
        /// </summary>
        /// <param name="db"></param>
        public void ReadDatabaseMetadata()
        {
            // tables and views
            db.Tables = new TableCollection(db);

            // columns
            DataTable dtTables = db.GetDataTable(this.SQLTables);
            foreach (DataRow drTable in dtTables.Rows)
            {
                Table tbl = new Table();
                tbl.Name = drTable["TABLE_NAME"].ToString();
                tbl.IsView = drTable["TABLE_TYPE"].ToString() == "VIEW";
                db.Tables.Add(tbl);


                tbl.Fields = new FieldCollection(tbl);

                DataTable dtColumns = db.GetDataTable(String.Format(this.SQLFields, tbl.Name));
                foreach (DataRow drColumn in dtColumns.Rows)
                {
                    Field f = new Field();
                    f.DefaultValue = drColumn["COLUMN_DEFAULT"].ToString();
                    f.FieldType = StringToDbType(drColumn["DATA_TYPE"].ToString());
                    f.Length = drColumn.IsNull("CHARACTER_MAXIMUM_LENGTH") ? 0 : Convert.ToInt32(drColumn["CHARACTER_MAXIMUM_LENGTH"]);
                    f.IsNullable = drColumn["IS_NULLABLE"].ToString() != "NO";
                    f.Name = drColumn["COLUMN_NAME"].ToString();
                    f.IsAutoIncrement = drColumn["IS_AUTO_INCREMENT"].ToString() == "1";

                    tbl.Fields.Add(f);
                }

                // keys
                tbl.Keys = new KeyCollection(tbl);
                DataTable dtKeys = db.GetDataTable("EXEC sp_helpindex " + tbl.Name);
                if(dtKeys!=null)
                    foreach (DataRow drKey in dtKeys.Rows)
                    {
                        Key key = new Key();
                        key.Name = drKey["index_name"].ToString();
                        key.IsUnique = drKey["index_description"].ToString().Contains("unique");
                        key.IsPrimary = drKey["index_description"].ToString().Contains("primary key");

                        key.FieldNames = new List<string>();
                        foreach (string fieldName in drKey["index_keys"].ToString().Split(','))
                            key.FieldNames.Add(fieldName.Trim().Replace("(-)", ""));

                        tbl.Keys.Add(key);
                    }

            }

            // primary keys
            //DataTable dtKeys = db.GetDataTable(this.SQLPrimaryKeys);
            //foreach (DataRow drKey in dtKeys.Rows)
            //    db.Tables[drKey["TABLE_NAME"].ToString()].Fields[drKey["COLUMN_NAME"].ToString()].IsPrimaryKey = true;

            // foreign keys
            DataTable dtForeigns = db.GetDataTable(this.SQLForeignKeys);
            foreach (DataRow drForeign in dtForeigns.Rows)
                db.Tables[drForeign["TABLE_NAME_1"].ToString()]
                    .Fields[drForeign["COLUMN_NAME_1"].ToString()].ReferenceFieldName =
                        drForeign["TABLE_NAME_2"] + "." + drForeign["COLUMN_NAME_2"];

        }

        /// <summary>
        /// Veritabanından string olarak gelen field tip bilgisini DbType enum'una dönüştürür.
        /// </summary>
        public DbType StringToDbType(string typeName)
        {
            foreach (var elm in DEFStringToDbType)
                if (elm.Key.ToLowerInvariant().Equals(typeName.ToLowerInvariant()))
                    return elm.Value;
            return DbType.Undefined;
        }

        /// <summary>
        /// DbType olarak elimizde bulunan field tip bilgisini veritabanın anlayacağı stringe dönüştürür. 
        /// </summary>
        public string DbTypeToString(DbType dbType)
        {
            foreach (var elm in DEFDbTypeToString)
                if (elm.Key == dbType)
                    return elm.Value;
            return "???";
        }

        public static Dictionary<DbType, string> DEFDbTypeToString = new Dictionary<DbType, string>() 
        { 
            {DbType.Binary, "Binary"},
            {DbType.Blob , "Blob"},
            {DbType.BlobLong , "Image"},
            {DbType.BlobMedium, "BlobMedium"},
            {DbType.BlobTiny, "BlobTiny"},
            {DbType.Boolean, "Bit"},
            {DbType.Byte, "Tinyint"},
            {DbType.Char, "Char"},
            {DbType.Currency, "Money"},
            {DbType.CurrencySmall, "SmallMoney"},
            {DbType.Date, "Date"},
            {DbType.DateTime, "DateTime"},
            {DbType.DateTimeSmall, "SmallDateTime"},
            {DbType.Decimal, "Decimal"},
            {DbType.Double, "Decimal"},
            {DbType.Enum, "Varchar"},
            {DbType.Float, "Float"},
            {DbType.Guid, "Uniqueidentifier"},
            {DbType.Image, "Image"},
            {DbType.Int16, "SmallInt"},
            {DbType.Int32, "Int"},
            {DbType.Int64, "Bigint"},
            {DbType.NChar, "NChar"},
            {DbType.NText, "NText"},
            {DbType.Numeric, "Numeric"},
            {DbType.NVarChar, "NVarChar"},
            {DbType.Real, "Real"},
            {DbType.Set, "Varchar"},
            {DbType.Text, "Text"},
            {DbType.TextLong, "Text"},
            {DbType.TextMedium, "Text"},
            {DbType.TextTiny, "Text"},
            {DbType.Time, "Time"},
            {DbType.Timestamp, "timestamp"},
            {DbType.Timestamptz, "Timestamp"},
            {DbType.Timetz, "Time"},
            {DbType.Undefined, "???"},
            {DbType.VarBinary, "Varbinary"},
            {DbType.VarChar, "VarChar"},
            {DbType.Variant, "Sql_variant"},
            {DbType.Xml, "Xml"}
        };

        public static Dictionary<string, DbType> DEFStringToDbType = new Dictionary<string, DbType>()
        {
            {"Binary", DbType.Binary},
            {"Blob", DbType.Blob},
            {"BlobLong", DbType.BlobLong},
            {"BlobMedium", DbType.BlobMedium},
            {"BlobTiny", DbType.BlobTiny},
            {"Bit", DbType.Boolean},
            {"Tinyint", DbType.Byte},
            {"Char", DbType.Char},
            {"Money", DbType.Currency},
            {"SmallMoney", DbType.CurrencySmall},
            {"Date", DbType.Date},
            {"DateTime", DbType.DateTime},
            {"SmallDateTime", DbType.DateTimeSmall},
            {"Decimal", DbType.Decimal},
            {"Float", DbType.Float},
            {"Uniqueidentifier", DbType.Guid},
            {"Image", DbType.Image},
            {"SmallInt", DbType.Int16},
            {"Int", DbType.Int32},
            {"Bigint", DbType.Int64},
            {"NChar", DbType.NChar},
            {"NText", DbType.NText},
            {"Numeric", DbType.Numeric},
            {"NVarChar", DbType.NVarChar},
            {"Real", DbType.Real},
            {"Text", DbType.Text},
            {"Time", DbType.Time},
            {"Timestamp", DbType.Timestamp},
            {"Varbinary", DbType.VarBinary},
            {"VarChar", DbType.VarChar},
            {"Sql_variant", DbType.Variant},
            {"Xml", DbType.Xml}
        };

        #region INFORMATION_SCHEMA SQL İFADELERİ
        // tablolar
        private string SQLTables = @"
											select
												TABLE_NAME, TABLE_TYPE
											from
												INFORMATION_SCHEMA.TABLES";
        // bir tablonun fieldları
        private string SQLFields = @"
											select
												COLUMN_NAME,
												DATA_TYPE,
												CHARACTER_MAXIMUM_LENGTH,
												IS_NULLABLE,
												COLUMN_DEFAULT,
                                                columnproperty(OBJECT_ID('{0}'), COLUMN_NAME,'IsIdentity') as IS_AUTO_INCREMENT
											from
												INFORMATION_SCHEMA.COLUMNS
											WHERE
												TABLE_NAME='{0}'";
        // Tablolara Primary Key Alanları
//        private string SQLPrimaryKeys = @"
//											SELECT
//												TBL1.CONSTRAINT_NAME,
//												TBL2.TABLE_NAME,
//												TBL2.COLUMN_NAME
//											FROM
//												(select CONSTRAINT_NAME from INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_TYPE='PRIMARY KEY' AND TABLE_NAME<>'dtproperties') AS TBL1,
//												(select TABLE_NAME, COLUMN_NAME, CONSTRAINT_NAME from INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE) AS TBL2
//											WHERE
//												TBL2.CONSTRAINT_NAME = TBL1.CONSTRAINT_NAME";
        // Foreyn kiyler
        private string SQLForeignKeys = @"
											SELECT
												TBL1.CONSTRAINT_NAME,
												TBL2.TABLE_NAME as TABLE_NAME_1,
												TBL2.COLUMN_NAME as COLUMN_NAME_1,
												TBL3.TABLE_NAME as TABLE_NAME_2,
												TBL3.COLUMN_NAME as COLUMN_NAME_2,
												TBL1.UPDATE_RULE,
												TBL1.DELETE_RULE
											FROM
												(select CONSTRAINT_NAME, UNIQUE_CONSTRAINT_NAME, UPDATE_RULE, DELETE_RULE  from INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS) AS TBL1,
												(select TABLE_NAME, COLUMN_NAME, CONSTRAINT_NAME from INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE) AS TBL2,
												(select TABLE_NAME, COLUMN_NAME, CONSTRAINT_NAME from INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE) AS TBL3
											WHERE
												TBL2.CONSTRAINT_NAME = TBL1.CONSTRAINT_NAME AND
												TBL3.CONSTRAINT_NAME = UNIQUE_CONSTRAINT_NAME";
        #endregion


        #region IDatabaseProvider Members
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(db.ConnectionString);
        }

        public DbDataAdapter CreateDataAdapter(IDbCommand selectCommand)
        {
            return new SqlDataAdapter((SqlCommand)selectCommand);
        }
        
        public DbDataAdapter CreateDataAdapter(string selectCommandText, params object[] parameters)
        {
            SqlCommand selectCommand = (SqlCommand)this.CreateCommand(selectCommandText, parameters);
            return this.CreateDataAdapter(selectCommand);
        }
        
        public IDbCommand CreateCommand(string cmdText, params object[] parameters)
        {
            return this.CreateCommand(cmdText, (IDbTransaction)null, parameters);
        }
        
        public IDbCommand CreateCommand(string cmdText, IDbTransaction transaction, params object[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
                cmdText = cmdText.Replace("{" + i + "}", "@_param" + i);

            SqlCommand cmd = null;
            if (transaction != null)
                cmd = new SqlCommand(cmdText, (SqlConnection)this.Connection, (SqlTransaction)transaction);
            else
                cmd = new SqlCommand(cmdText, (SqlConnection)this.Connection);

            for (int i = 0; i < parameters.Length; i++)
                cmd.Parameters.AddWithValue("@_param" + i, parameters[i]);

            return cmd;
        }
        
        public IDbDataParameter CreateParameter(string parameterName, object value)
        {
            return new SqlParameter(parameterName, value);
        }

        public string GetTableDDL(Table table)
        {
            int len = ("," + Environment.NewLine).Length;

            StringBuilder sbFields = new StringBuilder();
            foreach (Field f in table.Fields)
                sbFields.Append("\t" + GetFieldDDL(f) + "," + Environment.NewLine);

            sbFields = sbFields.Remove(sbFields.Length - len, len);

            StringBuilder sbKeys = new StringBuilder();
            foreach (Key k in table.Keys)
                sbKeys.Append(GetIndexKeyDDL(k) + ";" + Environment.NewLine);

            return String.Format("CREATE {0} [{1}]({3}{2});{3}{4}",
                (table.IsView ? "VIEW" : "TABLE"),
                table.Name,
                sbFields.ToString(),
                Environment.NewLine,
                sbKeys.ToString());
        }

        private string GetIndexKeyDDL(Key key)
        {
            if (key.IsPrimary)
                return String.Format("ALTER TABLE [{0}] ADD CONSTRAINT [{1}] PRIMARY KEY CLUSTERED ([{2}])",
                    key.parent.table.Name,
                    key.Name,
                    String.Join("], [", key.Fields.ToStringArray()));
            else if (key.IsUnique)
                return String.Format("CREATE UNIQUE NONCLUSTERED INDEX [{0}] ON {1} ([{2}])",
                    key.Name,
                    key.parent.table.Name,
                    String.Join("], [", key.Fields.ToStringArray()));
            else
                return String.Format("CREATE NONCLUSTERED INDEX [{0}] ON {1} ([{2}])",
                    key.Name,
                    key.parent.table.Name,
                    String.Join("], [", key.Fields.ToStringArray()));
        }

        public string GetFieldDDL(Field f)
        {
            string fieldDDL = "[" + f.Name + "] " + f.Table.Database.dbProvider.DbTypeToString(f.FieldType);
            if (f.Length>0 && (f.FieldType == DbType.Char || f.FieldType == DbType.VarChar || f.FieldType == DbType.NChar || f.FieldType == DbType.NVarChar))
                fieldDDL += "(" + (f.Length == 0 ? 50 : f.Length) + ")";
            if (f.IsAutoIncrement)
                fieldDDL += " IDENTITY";
            if (!f.IsNullable)
                fieldDDL += " NOT NULL";
            //if (f.IsPrimaryKey && f == f.Table.PrimaryField)
            //    fieldDDL += " PRIMARY KEY";
            if (!string.IsNullOrEmpty(f.DefaultValue) && f.FieldType != DbType.Timestamp)
                fieldDDL += " DEFAULT " + getDefaultValue(f);
            if (f.ReferenceField != null)
                fieldDDL += " REFERENCES [" + f.ReferenceField.Table.Name + "](" + f.ReferenceField.Name + ")";
            return fieldDDL;
        }

        private string getDefaultValue(Field field)
        {
            if (field.IsStringType())
                return encloseWithQuote(field.DefaultValue.Trim());
            else if (field.IsNumericType())
                return field.DefaultValue.Trim().Trim('\'');
            else
                return encloseWithQuote(field.DefaultValue.Trim());
        }

        private string encloseWithQuote(string str)
        {
            return "'" + str.Trim().Trim('(', ')', '\'') + "'";
        }

        private string encloseWithParanthesis(string str)
        {
            return "(" + str.Trim().Trim('(', ')') + ")";
        }
        #endregion
    }
}
