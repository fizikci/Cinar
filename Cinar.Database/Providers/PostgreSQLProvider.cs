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

        public PostgreSQLProvider(Database db, bool createConnection)
        {
            this.db = db;
            if (createConnection)
            {
                try
                {
                    con = new NpgsqlConnection(db.ConnectionString);
                    con.Open();
                    con.Close();
                }
                catch
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
                    con.Open();
                    IDbCommand cmd = con.CreateCommand();
                    cmd.CommandText = "create database " + dbName + ";";
                    cmd.ExecuteNonQuery();
                    con.ChangeDatabase(dbName);
                    con.Close();
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
            db.Tables = new TableCollection(db);

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
                        f.FieldType = StringToDbType(drColumn["DATA_TYPE"].ToString());
                        f.Length = drColumn.IsNull("CHARACTER_MAXiMUM_LENGTH") ? 0 : Convert.ToInt64(drColumn["CHARACTER_MAXiMUM_LENGTH"]);
                        f.IsNullable = drColumn["iS_NULLABLE"].ToString() != "NO";
                        f.Name = drColumn["COLUMN_NAME"].ToString();
                        f.IsAutoIncrement = drColumn["IS_AUTO_INCREMENT"].ToString() == "1";

                        tbl.Fields.Add(f);
                    }
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }

            // primary keys
            //TODO: postgreSQL bulursak bu kodu MySQL ve SQLServer'da olduðu gibi düzeltelim
            DataTable dtKeys = db.GetDataTable(this.SQLPrimaryKeys);
            if(dtKeys!=null)
                foreach (DataRow drKey in dtKeys.Rows)
                {
                    Table tbl = db.Tables[drKey["TABLE_NAME"].ToString()];
                    Key key = new Key();
                    key.FieldNames = new List<string>();
                    key.FieldNames.Add(drKey["COLUMN_NAME"].ToString());
                    key.IsPrimary = true;
                    key.IsUnique = true;
                    if (tbl.Keys == null) tbl.Keys = new KeyCollection(tbl);
                    tbl.Keys.Add(key);
                }
            
            // foreign keys
            DataTable dtForeigns = db.GetDataTable(this.SQLForeignKeys);
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
            typeName = typeName[0].ToString().ToUpperInvariant() + typeName.Substring(1);
            object ob = Enum.Parse(typeof(DbType4Postgres), typeName.Replace(" ", "_"));
            object ob2 = Enum.ToObject(typeof(DbType), ob);
            return (DbType)ob2;
        }

        /// <summary>
        /// DbType olarak elimizde bulunan field tip bilgisini veritabanýn anlayacaðý stringe dönüþtürür. 
        /// </summary>
        public string DbTypeToString(DbType dbType)
        {
            DbType4Postgres dbType4Postgres = (DbType4Postgres)dbType;
            switch (dbType4Postgres)
            {
                case DbType4Postgres.Boolean:
                    return "boolean";
                case DbType4Postgres.Bigint:
                    return "int4";
                case DbType4Postgres.Smallint:
                    return "int2";
                case DbType4Postgres.Integer:
                    return "int4";
                case DbType4Postgres.Text:
                    return "text";
                case DbType4Postgres.Real:
                    return "float4";
                case DbType4Postgres.Double_precision:
                    return "float8";
                case DbType4Postgres.Money:
                    return "money";
                case DbType4Postgres.Character:
                    return "char";
                case DbType4Postgres.Character_varying:
                    return "varchar";
                case DbType4Postgres.Date:
                    return "date";
                case DbType4Postgres.Time_without_time_zone:
                    return "time";
                case DbType4Postgres.Timestamp_without_time_zone:
                    return "timestamp";
                case DbType4Postgres.Timestamp_with_time_zone:
                    return "timestamptz";
                case DbType4Postgres.Time_with_time_zone:
                    return "timetz";
                case DbType4Postgres.Numeric:
                    return "numeric";
            }
            return "???";
        }


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
        // Tablolara Primary Key Alanlarý
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
            StringBuilder sbFields = new StringBuilder();
            foreach (Field f in table.Fields)
                sbFields.Append("\t" + GetFieldDDL(f) + ",\n");

            sbFields = sbFields.Remove(sbFields.Length - 2, 2);


            return String.Format("CREATE {0} \"{1}\"(\n{2});\n",
                (table.IsView ? "VIEW" : "TABLE"),
                table.Name,
                sbFields.ToString());
        }

        public string GetFieldDDL(Field f)
        {
            string fieldDDL = "\"" + f.Name + "\" ";
            if(!f.IsAutoIncrement)
                fieldDDL += f.Table.Database.dbProvider.DbTypeToString(f.FieldType);
            if (f.FieldType == DbType.Char || f.FieldType == DbType.VarChar || f.FieldType == DbType.NChar || f.FieldType == DbType.NVarChar)
                fieldDDL += "(" + (f.Length == 0 ? 50 : f.Length) + ")";
            if (f.IsAutoIncrement)
                fieldDDL += " SERIAL";
            if (!f.IsNullable)
                fieldDDL += " NOT NULL";
            if (f.IsPrimaryKey)
                fieldDDL += " PRIMARY KEY";
            if (!string.IsNullOrEmpty(f.DefaultValue))
                fieldDDL += " DEFAULT " + f.DefaultValue;
            if (f.ReferenceField != null)
                fieldDDL += " REFERENCES [" + f.ReferenceField.Table.Name + "](" + f.ReferenceField.Name + ")";
            return fieldDDL;
        }

        #endregion
    }

    internal enum DbType4Postgres
    {
        Boolean = DbType.Boolean,
        Bigint = DbType.Int64,
        Smallint = DbType.Int16,
        Integer = DbType.Int32,
        Text = DbType.Text,
        Real = DbType.Real,
        Double_precision = DbType.Double,
        Money = DbType.Currency,
        Character = DbType.Char,
        Character_varying = DbType.VarChar,
        Date = DbType.Date,
        Time_without_time_zone = DbType.Time,
        Timestamp_without_time_zone = DbType.Timestamp,
        Timestamp_with_time_zone = DbType.Timestamptz,
        Time_with_time_zone = DbType.Timetz,
        //Bit = DbType.Bit,
        Numeric = DbType.Numeric
    }

}
