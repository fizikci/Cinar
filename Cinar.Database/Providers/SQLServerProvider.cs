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
            db.Tables = new TableCollection((Database)db);

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
                    f.FieldTypeOriginal = drColumn["DATA_TYPE"].ToString().ToUpperInvariant();
                    f.FieldType = StringToDbType(f.FieldTypeOriginal);
                    f.Length = drColumn.IsNull("CHARACTER_MAXIMUM_LENGTH") ? 0 : Convert.ToInt32(drColumn["CHARACTER_MAXIMUM_LENGTH"]);
                    f.IsNullable = drColumn["IS_NULLABLE"].ToString() != "NO";
                    f.Name = drColumn["COLUMN_NAME"].ToString();
                    f.IsAutoIncrement = drColumn["IS_AUTO_INCREMENT"].ToString() == "1";

                    tbl.Fields.Add(f);
                }

                // indices
                tbl.Indices = new IndexCollection(tbl);
                DataTable dtKeys = db.GetDataTable("EXEC sp_helpindex " + tbl.Name);
                if(dtKeys!=null)
                    foreach (DataRow drKey in dtKeys.Rows)
                    {
                        Index index = new Index();
                        index.Name = drKey["index_name"].ToString();
                        index.IsUnique = drKey["index_description"].ToString().Contains("unique");
                        index.IsPrimary = drKey["index_description"].ToString().Contains("primary key");

                        index.FieldNames = new List<string>();
                        foreach (string fieldName in drKey["index_keys"].ToString().Split(','))
                            index.FieldNames.Add(fieldName.Trim().Replace("(-)", ""));

                        tbl.Indices.Add(index);
                    }

            }

            // primary indices
            //DataTable dtKeys = db.GetDataTable(this.SQLPrimaryKeys);
            //foreach (DataRow drKey in dtKeys.Rows)
            //    db.Tables[drKey["TABLE_NAME"].ToString()].Fields[drKey["COLUMN_NAME"].ToString()].IsPrimaryKey = true;

            // foreign indices
            DataTable dtForeigns = db.GetDataTable(this.SQLForeignKeys);
            foreach (DataRow drForeign in dtForeigns.Rows)
                db.Tables[drForeign["TABLE_NAME_1"].ToString()]
                    .Fields[drForeign["COLUMN_NAME_1"].ToString()].ReferenceFieldName =
                        drForeign["TABLE_NAME_2"] + "." + drForeign["COLUMN_NAME_2"];

        }

        #region string <=> dbType conversion

        /// <summary>
        /// Veritabanından string olarak gelen field tip bilgisini DbType enum'una dönüştürür.
        /// </summary>
        public DbType StringToDbType(string typeName)
        {
            if (DEFStringToDbType.ContainsKey(typeName.ToUpperInvariant()))
                return DEFStringToDbType[typeName];
            return DbType.Undefined;
        }

        /// <summary>
        /// DbType olarak elimizde bulunan field tip bilgisini veritabanın anlayacağı stringe dönüştürür. 
        /// </summary>
        public string DbTypeToString(DbType dbType)
        {
            if (DEFDbTypeToString.ContainsKey(dbType))
                return DEFDbTypeToString[dbType];
            return "???";
        }

        public static Dictionary<DbType, string> DEFDbTypeToString = new Dictionary<DbType, string>() 
        { 
            {DbType.Binary,         "BINARY"},
            {DbType.Blob ,          "BLOB"},
            {DbType.BlobLong ,      "IMAGE"},
            {DbType.BlobMedium,     "BLOBMEDIUM"},
            {DbType.BlobTiny,       "BLOBTINY"},
            {DbType.Boolean,        "BIT"},
            {DbType.Byte,           "TINYINT"},
            {DbType.Char,           "CHAR"},
            {DbType.Currency,       "MONEY"},
            {DbType.CurrencySmall,  "SMALLMONEY"},
            {DbType.Date,           "DATE"},
            {DbType.DateTime,       "DATETIME"},
            {DbType.DateTimeSmall,  "SMALLDATETIME"},
            {DbType.Decimal,        "DECIMAL"},
            {DbType.Double,         "DECIMAL"},
            {DbType.Enum,           "VARCHAR"},
            {DbType.Float,          "FLOAT"},
            {DbType.Guid,           "UNIQUEIDENTIFIER"},
            {DbType.Image,          "IMAGE"},
            {DbType.Int16,          "SMALLINT"},
            {DbType.Int32,          "INT"},
            {DbType.Int64,          "BIGINT"},
            {DbType.NChar,          "NCHAR"},
            {DbType.NText,          "NTEXT"},
            {DbType.Numeric,        "NUMERIC"},
            {DbType.NVarChar,       "NVARCHAR"},
            {DbType.Real,           "REAL"},
            {DbType.Set,            "VARCHAR"},
            {DbType.Text,           "TEXT"},
            {DbType.TextLong,       "TEXT"},
            {DbType.TextMedium,     "TEXT"},
            {DbType.TextTiny,       "TEXT"},
            {DbType.Time,           "TIME"},
            {DbType.Timestamp,      "TIMESTAMP"},
            {DbType.Timestamptz,    "TIMESTAMP"},
            {DbType.Timetz,         "TIME"},
            {DbType.Undefined,      "???"},
            {DbType.VarBinary,      "VARBINARY"},
            {DbType.VarChar,        "VARCHAR"},
            {DbType.Variant,        "SQL_VARIANT"},
            {DbType.Xml,            "XML"}
        };

        public static Dictionary<string, DbType> DEFStringToDbType = new Dictionary<string, DbType>()
        {
            {"BINARY",              DbType.Binary},
            {"BLOB",                DbType.Blob},
            {"BLOBLONG",            DbType.BlobLong},
            {"BLOBMEDIUM",          DbType.BlobMedium},
            {"BLOBTINY",            DbType.BlobTiny},
            {"BIT",                 DbType.Boolean},
            {"TINYINT",             DbType.Byte},
            {"CHAR",                DbType.Char},
            {"MONEY",               DbType.Currency},
            {"SMALLMONEY",          DbType.CurrencySmall},
            {"DATE",                DbType.Date},
            {"DATETIME",            DbType.DateTime},
            {"SMALLDATETIME",       DbType.DateTimeSmall},
            {"DECIMAL",             DbType.Decimal},
            {"FLOAT",               DbType.Float},
            {"UNIQUEIDENTIFIER",    DbType.Guid},
            {"IMAGE",               DbType.Image},
            {"SMALLINT",            DbType.Int16},
            {"INT",                 DbType.Int32},
            {"BIGINT",              DbType.Int64},
            {"NCHAR",               DbType.NChar},
            {"NTEXT",               DbType.NText},
            {"NUMERIC",             DbType.Numeric},
            {"NVARCHAR",            DbType.NVarChar},
            {"REAL",                DbType.Real},
            {"TEXT",                DbType.Text},
            {"TIME",                DbType.Time},
            {"TIMESTAMP",           DbType.Timestamp},
            {"VARBINARY",           DbType.VarBinary},
            {"VARCHAR",             DbType.VarChar},
            {"SQL_VARIANT",         DbType.Variant},
            {"XML",                 DbType.Xml}
        };

        public string[] GetFieldTypes()
        {
            return DEFStringToDbType.Keys.ToArray();
        }

        #endregion

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

            foreach (Index k in table.Indices.OrderBy(k => k.Name))
                if (k.IsPrimary || k.IsUnique)
                    sbFields.Append("\t" + GetIndexKeyDDL(k) + "," + Environment.NewLine);

            sbFields = sbFields.Remove(sbFields.Length - len, len);

            return String.Format("CREATE {0} [{1}](\n{2});" + Environment.NewLine,
                (table.IsView ? "VIEW" : "TABLE"),
                table.Name,
                sbFields);
        }

        private string GetIndexKeyDDL(Index index)
        {
            if (index.IsPrimary)
            {
                if (string.IsNullOrEmpty(index.Name)) index.Name = "pk_" + index.parent.table.Name;
                return "CONSTRAINT [" + index.Name + "] PRIMARY KEY ([" + String.Join("], [", index.Fields.ToStringArray()) + "])";
            }

            return "CONSTRAINT [" + index.Name + "] UNIQUE  ([" + String.Join("], [", index.Fields.ToStringArray()) + "])";
        }
        /*
        private string GetIndexKeyDDL(Index Index)
        {
            if (Index.IsPrimary)
                return String.Format("ALTER TABLE [{0}] ADD CONSTRAINT [{1}] PRIMARY KEY CLUSTERED ([{2}])",
                    Index.parent.table.Name,
                    Index.Name,
                    String.Join("], [", Index.Fields.ToStringArray()));
            else if (Index.IsUnique)
                return String.Format("CREATE UNIQUE NONCLUSTERED INDEX [{0}] ON {1} ([{2}])",
                    Index.Name,
                    Index.parent.table.Name,
                    String.Join("], [", Index.Fields.ToStringArray()));
            else
                return String.Format("CREATE NONCLUSTERED INDEX [{0}] ON {1} ([{2}])",
                    Index.Name,
                    Index.parent.table.Name,
                    String.Join("], [", Index.Fields.ToStringArray()));
        }
        */
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
                fieldDDL += " REFERENCES [" + f.ReferenceField.Table.Name + "]([" + f.ReferenceField.Name + "])";
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
