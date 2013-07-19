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
    internal class PostgreSQLProvider : BaseProvider, IDatabaseProvider
    {
        public PostgreSQLProvider(IDatabase db, bool createDatabaseIfNotExist)
        {
            this.db = db;
            try
            {
                connection = new NpgsqlConnection(db.ConnectionString);
                if (createDatabaseIfNotExist)
                {
                    connection.Open();
                    connection.Close();
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
                    connection = new NpgsqlConnection(newConnStr);
                    try
                    {
                        connection.Open();
                        IDbCommand cmd = connection.CreateCommand();
                        cmd.CommandText = "create database " + dbName + ";";
                        cmd.ExecuteNonQuery();
                        connection.ChangeDatabase(dbName);
                        CreatedNow = true;
                        connection.Close();
                        connection = new NpgsqlConnection(db.ConnectionString);
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
            DataTable dtTables = db.GetDataTable(GetSQLTableList());
            foreach (DataRow drTable in dtTables.Rows)
            {
                Table tbl = new Table();
                tbl.Name = drTable["TABLE_NAME"].ToString();
                tbl.IsView = drTable["TABLE_TYPE"].ToString() == "VIEW";
                db.Tables.Add(tbl);

                DataTable dtColumns = db.GetDataTable(GetSQLColumnList(tbl.Name));
                foreach (DataRow drColumn in dtColumns.Rows)
                {
                    Column f = new Column();
                    f.DefaultValue = drColumn["COLUMN_DEFAULT"].ToString();
                    f.ColumnTypeOriginal = drColumn["DATA_TYPE"].ToString().ToUpperInvariant();
                    f.ColumnType = StringToDbType(f.ColumnTypeOriginal);
                    f.Length = drColumn.IsNull("CHARACTER_MAXiMUM_LENGTH") ? 0 : Convert.ToInt64(drColumn["CHARACTER_MAXiMUM_LENGTH"]);
                    f.IsNullable = drColumn["iS_NULLABLE"].ToString() != "NO";
                    f.Name = drColumn["COLUMN_NAME"].ToString();
                    f.IsAutoIncrement = !string.IsNullOrEmpty(drColumn["iS_AUTO_iNCREMENT"].ToString());

                    tbl.Columns.Add(f);
                }
            }

            // constraints
            // Con.Name, Con.TableName, Con.Type, Col.ColumnName, Con.RefConstraintName, Con.UpdateRule, Con.DeleteRule
            DataTable dtCons = db.GetDataTable(this.GetSQLConstraintList());
            foreach (DataRow drCon in dtCons.Rows)
            {
                Constraint con = db.Tables[drCon["TableName"].ToString()].Constraints[drCon["Name"].ToString()];
                if (con != null)
                {
                    con.ColumnNames.Add(drCon["ColumnName"].ToString());
                    continue;
                }

                switch (drCon["Type"].ToString())
                {
                    case "FOREIGN KEY":
                        con = new ForeignKeyConstraint();
                        (con as ForeignKeyConstraint).RefConstraintName = drCon["RefConstraintName"].ToString();
                        //(con as ForeignKeyConstraint).RefTableName = drCon["RefConstraintName"].ToString();
                        (con as ForeignKeyConstraint).DeleteRule = drCon["DeleteRule"].ToString();
                        (con as ForeignKeyConstraint).UpdateRule = drCon["UpdateRule"].ToString();
                        break;
                    case "PRIMARY KEY":
                        con = new PrimaryKeyConstraint();
                        break;
                    case "UNIQUE":
                        con = new UniqueConstraint();
                        break;
                    default:
                        throw new Exception("Unknown constraint type: " + drCon["Type"].ToString());
                }
                con.Name = drCon["Name"].ToString();
                con.ColumnNames.Add(drCon["ColumnName"].ToString());

                db.Tables[drCon["TableName"].ToString()].Constraints.Add(con);
            }
            foreach (Table tbl in db.Tables)
                foreach (ForeignKeyConstraint fk in tbl.Constraints.Where(c => c is ForeignKeyConstraint))
                    fk.RefTableName = db.GetConstraint(fk.RefConstraintName).Table.Name;

            // indices
            foreach (Table tbl in db.Tables)
            {
                DataTable dtKeys = db.GetDataTable(@"select
                                                        t.relname as table_name,
                                                        i.relname as index_name,
                                                        array_to_string(array_agg(a.attname), ', ') as index_keys
                                                    from
                                                        pg_class t,
                                                        pg_class i,
                                                        pg_index ix,
                                                        pg_attribute a
                                                    where
                                                        t.oid = ix.indrelid
                                                        and i.oid = ix.indexrelid
                                                        and a.attrelid = t.oid
                                                        and a.attnum = ANY(ix.indkey)
                                                        and t.relkind = 'r'
                                                        and t.relname like '" + tbl.Name + @"%'
                                                    group by
                                                        t.relname,
                                                        i.relname
                                                    order by
                                                        t.relname,
                                                        i.relname;
                                                    ");
                if (dtKeys != null)
                    foreach (DataRow drKey in dtKeys.Rows)
                    {
                        if (db.GetConstraint(drKey["index_name"].ToString()) != null)
                            continue;

                        Index index = new Index();
                        index.Name = drKey["index_name"].ToString();

                        index.ColumnNames = new List<string>();
                        foreach (string columnName in drKey["index_keys"].ToString().Split(','))
                            index.ColumnNames.Add(columnName.Trim().Replace("(-)", ""));

                        tbl.Indices.Add(index);
                    }
            }

        }

        #region string <=> dbType conversion

        public override Dictionary<DbType, string> DEFDbTypeToString { get { return _DEFDbTypeToString; } }
        private Dictionary<string, DbType> _DEFStringToDbType = new Dictionary<string, DbType>() 
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
        public override Dictionary<string, DbType> DEFStringToDbType { get { return _DEFStringToDbType; } }
        private Dictionary<DbType, string> _DEFDbTypeToString = new Dictionary<DbType, string>() 
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

        public string[] GetColumnTypes()
        {
            return DEFStringToDbType.Keys.ToArray();
        }

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

        public string GetColumnDDL(Column f)
        {
            string columnDDL = "\"" + f.Name + "\" ";
            //if (!f.IsAutoIncrement)
                columnDDL += DbTypeToString(f.ColumnType);
            if (f.ColumnType == DbType.Char || f.ColumnType == DbType.VarChar || f.ColumnType == DbType.NChar || f.ColumnType == DbType.NVarChar)
                columnDDL += "(" + (f.Length == 0 ? 50 : f.Length) + ")";
            //if (f.IsAutoIncrement)
            //    columnDDL += " SERIAL";
            if (!f.IsNullable)
                columnDDL += " NOT NULL";
            //if (f.IsPrimaryKey)
            //    columnDDL += " PRIMARY KEY";
            if (!string.IsNullOrEmpty(f.DefaultValue) /*&& !f.DefaultValue.StartsWith("nextval(")*/)
                columnDDL += " DEFAULT " + getDefaultValue(f);
            //if (f.ReferenceColumn != null)
            //    columnDDL += " REFERENCES \"" + f.ReferenceColumn.Table.Name + "\"(\"" + f.ReferenceColumn.Name + "\")";
            return columnDDL;
        }
        public string GetTableDDL(Table table)
        {
            if (table.Columns.Count == 0)
                return "";

            int len = ("," + Environment.NewLine).Length;

            StringBuilder sbColumns = new StringBuilder();
            foreach (Column f in table.Columns)
                sbColumns.Append("\t" + GetColumnDDL(f) + "," + Environment.NewLine);
            sbColumns = sbColumns.Remove(sbColumns.Length - len, len);

            StringBuilder sbCons = new StringBuilder();
            foreach (Constraint c in table.Constraints)
                sbCons.Append(GetSQLConstraintAdd(c) + ";" + Environment.NewLine);
            foreach (Index i in table.Indices)
                sbCons.Append(GetSQLIndexAdd(i) + ";" + Environment.NewLine);

            if (table.IsView)
                return String.Format("CREATE VIEW \"{0}\" AS {1}" + Environment.NewLine,
                    table.Name,
                    table.ViewSQL);

            return String.Format("CREATE TABLE \"{0}\"(\r\n{1});\r\n{2}" + Environment.NewLine,
                table.Name,
                sbColumns,
                sbCons);
        }

        #endregion

        #region GetSQL

        public string GetSQLTableList()
        {
            return string.Format(@"
					    SELECT 
                            table_name, 
                            table_type 
                        FROM 
                            information_schema.tables 
                        WHERE 
                            (table_type = 'BASE TABLE' or table_type = 'VIEW') AND 
                            table_schema NOT IN ('pg_catalog', 'information_schema')", db.Name);
        }

        public string GetSQLTableRename(string oldName, string newName)
        {
            return string.Format("ALTER TABLE \"{0}\" RENAME TO \"{1}\"", oldName, newName);
        }

        public string GetSQLTableDrop(Table table)
        {
            return string.Format("DROP {0} \"{1}\"", table.IsView ? "VIEW" : "TABLE", table.Name);
        }

        public string GetSQLColumnList(string tableName)
        {
            return string.Format(@"
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
							TABLE_NAME='{0}'
                        ORDER BY ORDINAL_POSITION", tableName);
        }

        public string GetSQLColumnAdd(string toTable, Column column)
        {
            return string.Format("ALTER TABLE \"{0}\" ADD {1}", toTable, GetColumnDDL(column));
        }

        public string GetSQLColumnRemove(Column column)
        {
            return string.Format("ALTER TABLE \"{0}\" DROP COLUMN \"{1}\"", column.Table.Name, column.Name);
        }

        public string GetSQLColumnRename(string oldColumnName, Column column)
        {
            return string.Format("ALTER TABLE \"{0}\" RENAME COLUMN \"{1}\" TO \"{2}\"", column.Table.Name, oldColumnName, column.Name);
        }

        public string GetSQLColumnChangeDataType(Column column)
        {
            return string.Format("ALTER TABLE \"{0}\" ALTER COLUMN \"{1}\" TYPE {2}{3}", column.Table.Name, column.Name, column.ColumnTypeOriginal, column.SimpleColumnType == SimpleDbType.String ? "(" + column.Length + ")" : "");
        }

        public string GetSQLColumnChangeDefault(Column column)
        {
            return string.Format("ALTER TABLE \"{0}\" ALTER COLUMN \"{1}\" SET DEFAULT {2}", column.Table.Name, column.Name, column.DefaultValue);
        }

        public string GetSQLConstraintList()
        {
            return string.Format(@"select distinct
	Con.Name, Con.TableName, Con.Type, Col.ColumnName, Col.Position, Con.RefConstraintName, Con.UpdateRule, Con.DeleteRule
from
	(select c.CONSTRAINT_NAME as Name, c.TABLE_NAME as TableName, c.CONSTRAINT_TYPE as Type, r.UNIQUE_CONSTRAINT_NAME as RefConstraintName, r.UPDATE_RULE AS UpdateRule, R.DELETE_RULE as DeleteRule from INFORMATION_SCHEMA.TABLE_CONSTRAINTS c left join INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS r ON r.CONSTRAINT_NAME=c.CONSTRAINT_NAME where c.table_catalog='{0}') as Con,
	(select CONSTRAINT_NAME as ConstraintName, TABLE_NAME as TableName, COLUMN_NAME as ColumnName, ORDINAL_POSITION as Position from INFORMATION_SCHEMA.KEY_COLUMN_USAGE where table_catalog='{0}') as Col
where
	Con.Name = Col.ConstraintName
order by Con.Name, Con.TableName, Con.Type, Col.ColumnName, Col.Position", db.Name);
        }

        public string GetSQLConstraintRemove(Constraint constraint)
        {
            return string.Format("ALTER TABLE \"{0}\" DROP CONSTRAINT \"{1}\"", constraint.Table.Name, constraint.Name);
        }

        public string GetSQLConstraintAdd(Constraint constraint)
        {
            if (constraint == null)
                return "";

            if (constraint is PrimaryKeyConstraint)
                return GetSQLConstraintAdd(constraint as PrimaryKeyConstraint);
            if (constraint is UniqueConstraint)
                return GetSQLConstraintAdd(constraint as UniqueConstraint);
            if (constraint is CheckConstraint)
                return GetSQLConstraintAdd(constraint as CheckConstraint);
            if (constraint is ForeignKeyConstraint)
                return GetSQLConstraintAdd(constraint as ForeignKeyConstraint);

            throw new Exception("Unknown constraint type: " + constraint.GetType().Name);
        }

        public string GetSQLConstraintAdd(CheckConstraint constraint)
        {
            return string.Format("ALTER TABLE \"{0}\" ADD CONSTRAINT \"{1}\" CHECK ({2})", constraint.Table.Name, constraint.Name, constraint.Expression);
        }

        public string GetSQLConstraintAdd(UniqueConstraint constraint)
        {
            return string.Format("ALTER TABLE \"{0}\" ADD CONSTRAINT \"{1}\" UNIQUE (\"{2}\")", constraint.Table.Name, constraint.Name, string.Join("\",\"", constraint.ColumnNames.ToArray()));
        }

        public string GetSQLConstraintAdd(ForeignKeyConstraint constraint)
        {
            return string.Format("ALTER TABLE \"{0}\" ADD CONSTRAINT \"{1}\" FOREIGN KEY (\"{2}\") REFERENCES \"{3}\"(\"{4}\")", constraint.Table.Name, constraint.Name, string.Join("\",\"", constraint.ColumnNames.ToArray()), constraint.RefTableName, string.Join("\",\"", db.Tables[constraint.RefTableName].Constraints[constraint.RefConstraintName].ColumnNames.ToArray()));
        }

        public string GetSQLConstraintAdd(PrimaryKeyConstraint constraint)
        {
            return string.Format("ALTER TABLE \"{0}\" ADD CONSTRAINT \"{1}\" PRIMARY KEY (\"{2}\")", constraint.Table.Name, constraint.Name, string.Join("\",\"", constraint.ColumnNames.ToArray()));
        }

        public string GetSQLConstraintRemove(PrimaryKeyConstraint constraint)
        {
            return GetSQLConstraintRemove(constraint);
        }

        public string GetSQLColumnAddNotNull(Column column)
        {
            return string.Format("ALTER TABLE \"{0}\" ALTER COLUMN \"{1}\" SET NOT NULL", column.Table.Name, column.Name);
        }

        public string GetSQLColumnRemoveNotNull(Column column)
        {
            return string.Format("ALTER TABLE \"{0}\" ALTER COLUMN \"{1}\" DROP NOT NULL", column.Table.Name, column.Name);
        }

        public string GetSQLColumnSetAutoIncrement(Column column)
        {
            return string.Format(@"DECLARE
    seq_name VARCHAR(100);
BEGIN
    SELECT pg_get_serial_sequence('{0}', '{1}') INTO seq_name;
    IF seq_name IS NOT NULL THEN RAISE '{0}.{1} is already auto increment'; END IF;

    CREATE SEQUENCE ""seq_{0}_{1}"";
    SELECT setval('seq_{0}_{1}', max(""{1}"")) FROM ""{0}"";
    ALTER TABLE ""{0}"" ALTER COLUMN ""{1}"" DROP DEFAULT;
    ALTER TABLE ""{0}"" ALTER COLUMN ""{1}"" SET DEFAULT NEXTVAL('seq_{0}_{1}');
END;", column.Table.Name, column.Name);
        }

        public string GetSQLColumnRemoveAutoIncrement(Column column)
        {
            return string.Format(@"DECLARE
    seq_name VARCHAR(100);
BEGIN
    SELECT pg_get_serial_sequence('{0}', '{1}') INTO seq_name;
    IF seq_name IS NULL THEN RAISE '{0}.{1} is already NOT auto increment'; END IF;

    ALTER TABLE ""{0}"" ALTER COLUMN ""{1}"" DROP DEFAULT;
    EXECUTE 'DROP SEQUENCE ""$1""' USING seq_name;;
END;", column.Table.Name, column.Name);
        }

        public string GetSQLIndexAdd(Index index)
        {
            return string.Format("CREATE INDEX \"{0}\" ON \"{1}\" (\"{2}\")", index.Name, index.Table.Name, string.Join("\",\"", index.ColumnNames.ToArray()));
        }

        public string GetSQLIndexRemove(Index index)
        {
            return string.Format("DROP INDEX \"{0}\"", index.Name);
        }

        public string GetSQLViewCreate(Table view)
        {
            return "CREATE VIEW \"" + view.Name + "\" AS " + db.GetString("select definition from pg_catalog.pg_views where viewname = '"+view.Name+"'");
        }

        public string GetSQLDateYearMonthPart(string columnName)
        {
            return string.Format("to_char([{0}], 'YYYY.MM')", columnName);
        }

        #endregion
    }
}
