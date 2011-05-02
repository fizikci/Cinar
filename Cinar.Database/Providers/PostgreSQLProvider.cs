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
using Npgsql;
using System.Data.Common;

namespace Cinar.Database.Providers
{
    /// <summary>
    /// Postgre'den metadata okuyan sýnýf
    /// </summary>
    internal class PostgreSQLProvider : IDatabaseProvider
    {
        private System.Data.IDbConnection con;
        public System.Data.IDbConnection Connection
        {
            get { return con; }
        }

        private Database db = null;

        public PostgreSQLProvider(Database db, bool createDatabaseIfNotExist)
        {
            this.db = db;
            try
            {
                con = new NpgsqlConnection(db.ConnectionString);
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
                    con = new NpgsqlConnection(newConnStr);
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
        /// Metada okuma iþini yapan asýl metod. Sýrayla bütün veritabaný nesnelerini okur.
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
                try
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
                        f.Length = drColumn.IsNull("CHARACTER_MAXiMUM_LENGTH") ? 0 : Convert.ToInt64(drColumn["CHARACTER_MAXiMUM_LENGTH"]);
                        f.IsNullable = drColumn["iS_NULLABLE"].ToString() != "NO";
                        f.Name = drColumn["COLUMN_NAME"].ToString();
                        f.IsAutoIncrement = !string.IsNullOrEmpty(drColumn["iS_AUTO_iNCREMENT"].ToString());

                        tbl.Fields.Add(f);
                    }
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }

            // primary indices
            DataTable dtKeys = db.GetDataTable(this.SQLPrimaryKeys);
            if (dtKeys != null)
                foreach (DataRow drKey in dtKeys.Rows)
                {
                    Table tbl = db.Tables[drKey["TABLE_NAME"].ToString()];
                    Index index = new Index();
                    index.Name = drKey["CONSTRAiNT_NAME"].ToString();
                    index.FieldNames = new List<string>();
                    index.FieldNames.Add(drKey["COLUMN_NAME"].ToString());
                    index.IsPrimary = true;
                    index.IsUnique = true;
                    if (tbl.Indices == null) tbl.Indices = new IndexCollection(tbl);
                    tbl.Indices.Add(index);
                }

            // foreign indices
            DataTable dtForeigns = db.GetDataTable(this.SQLForeignKeys);
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
            if(DEFStringToDbType.ContainsKey(typeName.ToUpperInvariant()))
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

        public static Dictionary<string, DbType> DEFStringToDbType = new Dictionary<string, DbType>() 
        { 
            {"BOOLEAN",                     DbType.Boolean},
            {"BOOL",                        DbType.Boolean},
            {"BIGINT",                      DbType.Int64},
            {"INT8",                        DbType.Int64},
            {"BIGSERIAL",                   DbType.Int64},
            {"SERIAL8",                     DbType.Int64},
            {"SERIAL",                      DbType.Int32},
            {"SERIAL4",                     DbType.Int32},
            {"BYTEA",                       DbType.Binary},
            {"BIT",                         DbType.Binary},
            {"BIT VARYING",                 DbType.VarBinary},
            {"INT2",                        DbType.Int16},
            {"SMALLINT",                    DbType.Int16},
            {"INT",                         DbType.Int32},
            {"INT4",                        DbType.Int32},
            {"INTEGER",                     DbType.Int32},
            {"TEXT",                        DbType.Text},
            {"REAL",                        DbType.Real},
            {"FLOAT4",                      DbType.Real},
            {"FLOAT8",                      DbType.Float},
            {"DOUBLE PRECISION",            DbType.Float},
            {"MONEY",                       DbType.Currency},
            {"CHAR",                        DbType.Char},
            {"CHARACTER",                   DbType.Char},
            {"VARCHAR",                     DbType.VarChar},
            {"CHARACTER VARYING",           DbType.VarChar},
            {"DATE",                        DbType.Date},
            {"TIME",                        DbType.Time},
            {"TIME WITHOUT TIME ZONE",      DbType.Time},
            {"TIMESTAMP",                   DbType.Timestamp},
            {"TIMESTAMP WITHOUT TIME ZONE", DbType.Timestamp},
            {"TIMESTAMP WITH TIME ZONE",    DbType.Timestamptz},
            {"TIMESTAMPZ",                  DbType.Timestamptz},
            {"TIME WITH TIME ZONE",         DbType.Timetz},
            {"TIMEZ",                       DbType.Timetz},
            {"XML",                         DbType.Xml},
            {"DECIMAL",                     DbType.Decimal},
            {"NUMERIC",                     DbType.Numeric}
        };
        public static Dictionary<DbType, string> DEFDbTypeToString = new Dictionary<DbType, string>() 
        { 
            {DbType.Binary,         "BYTEA"},
            {DbType.Blob ,          "BYTEA"},
            {DbType.BlobLong ,      "BYTEA"},
            {DbType.BlobMedium,     "BYTEA"},
            {DbType.BlobTiny,       "BYTEA"},
            {DbType.Boolean,        "BOOL"},
            {DbType.Byte,           "INT2"},
            {DbType.Char,           "CHAR"},
            {DbType.Currency,       "MONEY"},
            {DbType.CurrencySmall,  "MONEY"},
            {DbType.Date,           "DATE"},
            {DbType.DateTime,       "DATE"},
            {DbType.DateTimeSmall,  "DATE"},
            {DbType.Decimal,        "DECIMAL"},
            {DbType.Double,         "DECIMAL"},
            {DbType.Enum,           "VARCHAR"},
            {DbType.Float,          "REAL"},
            {DbType.Guid,           "UUID"},
            {DbType.Image,          "BYTEA"},
            {DbType.Int16,          "SMALLINT"},
            {DbType.Int32,          "INT"},
            {DbType.Int64,          "BIGINT"},
            {DbType.NChar,          "CHAR"},
            {DbType.NText,          "TEXT"},
            {DbType.Numeric,        "NUMERIC"},
            {DbType.NVarChar,       "VARCHAR"},
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
            {DbType.VarBinary,      "BYTEA"},
            {DbType.VarChar,        "VARCHAR"},
            {DbType.Variant,        "BYTEA"},
            {DbType.Xml,            "XML"}
        };

        public string[] GetFieldTypes()
        {
            return DEFStringToDbType.Keys.ToArray();
        }

        #endregion

        #region INFORMATION_SCHEMA SQL ÝFADELERÝ
        // tablolar
        private string SQLTables = @"
											SELECT 
                                                table_name, 
                                                table_type 
                                            FROM 
                                                information_schema.tables 
                                            WHERE 
                                                (table_type = 'BASE TABLE' or table_type = 'VIEW') AND 
                                                table_schema NOT IN ('pg_catalog', 'information_schema');";
        // bir tablonun fieldlarý
        private string SQLFields = @"
											select
												COLUMN_NAME,
												DATA_TYPE,
												CHARACTER_MAXIMUM_LENGTH,
												IS_NULLABLE,
												COLUMN_DEFAULT,
                                                pg_get_serial_sequence('{0}', COLUMN_NAME) as IS_AUTO_INCREMENT
											from
												INFORMATION_SCHEMA.COLUMNS
											WHERE
												TABLE_NAME='{0}'";
        // Tablolara Primary Index Alanlarý
        private string SQLPrimaryKeys = @"
											SELECT
												TBL1.CONSTRAINT_NAME,
												TBL2.TABLE_NAME,
												TBL2.COLUMN_NAME
											FROM
												(select CONSTRAINT_NAME from INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_TYPE='PRIMARY KEY' AND TABLE_NAME<>'dtproperties') AS TBL1,
												(select TABLE_NAME, COLUMN_NAME, CONSTRAINT_NAME from INFORMATION_SCHEMA.KEY_COLUMN_USAGE) AS TBL2
											WHERE
												TBL2.CONSTRAINT_NAME = TBL1.CONSTRAINT_NAME";
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
												(select *  from INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS) AS TBL1,
												(select TABLE_NAME, COLUMN_NAME, CONSTRAINT_NAME from INFORMATION_SCHEMA.KEY_COLUMN_USAGE) AS TBL2,
												(select TABLE_NAME, COLUMN_NAME, CONSTRAINT_NAME from INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE) AS TBL3
											WHERE
												TBL2.CONSTRAINT_NAME = TBL1.CONSTRAINT_NAME AND
												TBL3.CONSTRAINT_NAME = TBL1.CONSTRAINT_NAME";
        #endregion

        #region IDatabaseProvider Members
        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(db.ConnectionString);
        }

        public DbDataAdapter CreateDataAdapter(System.Data.IDbCommand selectCommand)
        {
            return new NpgsqlDataAdapter((NpgsqlCommand)selectCommand);
        }

        public DbDataAdapter CreateDataAdapter(string selectCommandText, params object[] parameters)
        {
            NpgsqlCommand selectCommand = (NpgsqlCommand)this.CreateCommand(selectCommandText, parameters);
            return (DbDataAdapter)this.CreateDataAdapter(selectCommand);
        }

        public IDbCommand CreateCommand(string cmdText, params object[] parameters)
        {
            return this.CreateCommand(cmdText, (IDbTransaction)null, parameters);
        }

        public IDbCommand CreateCommand(string cmdText, System.Data.IDbTransaction transaction, params object[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
                cmdText = cmdText.Replace("{" + i + "}", "@_param" + i);

            NpgsqlCommand cmd = null;
            if (transaction != null)
                cmd = new NpgsqlCommand(cmdText, (NpgsqlConnection)this.Connection, (NpgsqlTransaction)transaction);
            else
                cmd = new NpgsqlCommand(cmdText, (NpgsqlConnection)this.Connection);

            for (int i = 0; i < parameters.Length; i++)
                cmd.Parameters.Add("@_param" + i, parameters[i]);

            return cmd;
        }

        public IDbDataParameter CreateParameter(string parameterName, object value)
        {
            return new NpgsqlParameter(parameterName, value);
        }

        public string GetTableDDL(Table table)
        {
            int len = ("," + Environment.NewLine).Length;

            StringBuilder sbFields = new StringBuilder();
            foreach (Field f in table.Fields)
                sbFields.Append("\t" + GetFieldDDL(f) + "," + Environment.NewLine);

            foreach (Index k in table.Indices.OrderBy(k => k.Name))
                if(k.IsPrimary || k.IsUnique)
                    sbFields.Append("\t" + GetIndexKeyDDL(k) + "," + Environment.NewLine);

            sbFields = sbFields.Remove(sbFields.Length - len, len);

            return String.Format("CREATE {0} \"{1}\"(\n{2});" + Environment.NewLine,
                (table.IsView ? "VIEW" : "TABLE"),
                table.Name,
                sbFields);
        }

        public string GetIndexKeyDDL(Index index)
        {
            if (index.IsPrimary)
            {
                if (string.IsNullOrEmpty(index.Name) || index.Name.ToUpperInvariant() == "PRIMARY") 
                    index.Name = "pk_" + index.parent.table.Name;
                return "CONSTRAINT \"" + index.Name + "\" PRIMARY KEY (\"" + String.Join("\", \"", index.Fields.ToStringArray()) + "\")";
            }

            return "CONSTRAINT \"" + index.Name + "\" UNIQUE  (\"" + String.Join("\", \"", index.Fields.ToStringArray()) + "\")";
        }

        public string GetFieldDDL(Field f)
        {
            string fieldDDL = "\"" + f.Name + "\" ";
            //if (!f.IsAutoIncrement)
                fieldDDL += DbTypeToString(f.FieldType);
            if (f.FieldType == DbType.Char || f.FieldType == DbType.VarChar || f.FieldType == DbType.NChar || f.FieldType == DbType.NVarChar)
                fieldDDL += "(" + (f.Length == 0 ? 50 : f.Length) + ")";
            //if (f.IsAutoIncrement)
            //    fieldDDL += " SERIAL";
            if (!f.IsNullable)
                fieldDDL += " NOT NULL";
            //if (f.IsPrimaryKey)
            //    fieldDDL += " PRIMARY KEY";
            if (!string.IsNullOrEmpty(f.DefaultValue) /*&& !f.DefaultValue.StartsWith("nextval(")*/)
                fieldDDL += " DEFAULT " + f.DefaultValue;
            if (f.ReferenceField != null)
                fieldDDL += " REFERENCES \"" + f.ReferenceField.Table.Name + "\"(\"" + f.ReferenceField.Name + "\")";
            return fieldDDL;
        }

        #endregion
    }
}
