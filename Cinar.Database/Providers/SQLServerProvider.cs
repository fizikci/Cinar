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
    internal class SQLServerProvider : BaseProvider, IDatabaseProvider
    {
        public SQLServerProvider(IDatabase db, bool createDatabaseIfNotExist)
        {
            this.db = db;
            try
            {
                connection = new SqlConnection(db.ConnectionString);
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
                    connection = new SqlConnection(newConnStr);
                    try
                    {
                        connection.Open();
                        IDbCommand cmd = connection.CreateCommand();
                        cmd.CommandText = "create database " + dbName + ";";
                        cmd.ExecuteNonQuery();
                        connection.ChangeDatabase(dbName);
                        CreatedNow = true;
                        connection.Close();
                        connection = new SqlConnection(db.ConnectionString);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Metadata okuma işini yapan asıl metod. Sırayla bütün veritabanı nesnelerini okur.
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
                //if (drTable["TABLE_TYPE"].ToString() == "VIEW") continue;

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
                    f.Length = drColumn.IsNull("CHARACTER_MAXIMUM_LENGTH") ? 0 : Convert.ToInt32(drColumn["CHARACTER_MAXIMUM_LENGTH"]);
                    f.IsNullable = drColumn["IS_NULLABLE"].ToString() != "NO";
                    f.Name = drColumn["COLUMN_NAME"].ToString();
                    f.IsAutoIncrement = drColumn["IS_AUTO_INCREMENT"].ToString() == "1";

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
                DataTable dtKeys = db.GetDataTable("EXEC sp_helpindex [" + tbl.Name + "]");
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
        private Dictionary<DbType, string> _DEFDbTypeToString = new Dictionary<DbType, string>() 
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

        public override Dictionary<string, DbType> DEFStringToDbType { get { return _DEFStringToDbType; } }
        private Dictionary<string, DbType> _DEFStringToDbType = new Dictionary<string, DbType>()
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

        public string[] GetColumnTypes()
        {
            return DEFStringToDbType.Keys.ToArray();
        }

        #endregion

        #region IDatabaseProvider Members
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(db.ConnectionString);
        }

        public DbDataAdapter CreateDataAdapter(IDbCommand selectCommand)
        {
            selectCommand.CommandTimeout = db.DefaultCommandTimeout;
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

            cmd.CommandTimeout = db.DefaultCommandTimeout;

            for (int i = 0; i < parameters.Length; i++)
                cmd.Parameters.AddWithValue("@_param" + i, parameters[i] ?? DBNull.Value);

            return cmd;
        }

        public IDbDataParameter CreateParameter(string parameterName, object value)
        {
            return new SqlParameter(parameterName, value);
        }

        public string GetColumnDDL(Column f)
        {
            string columnDDL = "[" + f.Name + "] " + (string.IsNullOrEmpty(f.ColumnTypeOriginal) ? DbTypeToString(f.ColumnType) : f.ColumnTypeOriginal);
            if (f.Length > 0 && (f.ColumnType == DbType.Char || f.ColumnType == DbType.VarChar || f.ColumnType == DbType.NChar || f.ColumnType == DbType.NVarChar))
                columnDDL += "(" + (f.Length == 0 ? 50 : f.Length) + ")";
            if (!f.IsNullable)
                columnDDL += " NOT NULL";
            if (f.IsAutoIncrement)
                columnDDL += " IDENTITY";
            //if (f.IsPrimaryKey && f == f.Table.PrimaryColumn)
            //    fieldDDL += " PRIMARY KEY";
            if (!string.IsNullOrEmpty(f.DefaultValue) && f.ColumnType != DbType.Timestamp)
                columnDDL += " DEFAULT " + getDefaultValue(f);
            //if (f.ReferenceColumn != null)
            //    fieldDDL += " REFERENCES [" + f.ReferenceColumn.Table.Name + "]([" + f.ReferenceColumn.Name + "])";
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


            return String.Format("CREATE {0} [{1}](\r\n{2});\r\n{3}" + Environment.NewLine,
                (table.IsView ? "VIEW" : "TABLE"),
                table.Name,
                sbColumns,
                sbCons);
        }

        #endregion

        #region GetSQL

        public string GetSQLTableList()
        {
            return string.Format(@"
					    select
							TABLE_NAME, TABLE_TYPE
						from
							INFORMATION_SCHEMA.TABLES", db.Name);
        }

        public string GetSQLTableRename(string oldName, string newName)
        {
            return string.Format("EXEC sp_rename [{0}], [{1}]", oldName, newName);
        }

        public string GetSQLTableDrop(Table table)
        {
            return string.Format("DROP {0} [{1}]", table.IsView ? "VIEW" : "TABLE", table.Name);
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
                            columnproperty(OBJECT_ID('{0}'), COLUMN_NAME,'IsIdentity') as IS_AUTO_INCREMENT
						from
							INFORMATION_SCHEMA.COLUMNS
						WHERE
							TABLE_NAME='{0}'
                        ORDER BY ORDINAL_POSITION", tableName);
        }

        public string GetSQLColumnAdd(string toTable, Column column)
        {
            return string.Format("ALTER TABLE [{0}] ADD {1}", toTable, GetColumnDDL(column));
        }

        public string GetSQLColumnRemove(Column column)
        {
            return string.Format("ALTER TABLE [{0}] DROP COLUMN [{1}]", column.Table.Name, column.Name);
        }

        public string GetSQLColumnRename(string oldColumnName, Column column)
        {
            return string.Format("EXEC sp_rename @objname = '{0}.{1}', @newname = '{2}', @objtype = 'COLUMN'", column.Table.Name, oldColumnName, column.Name);
        }

        public string GetSQLColumnChangeDataType(Column column)
        {
            return string.Format("ALTER TABLE [{0}] ALTER COLUMN [{1}] {2}{3}", column.Table.Name, column.Name, column.ColumnTypeOriginal, column.SimpleColumnType == SimpleDbType.String ? "(" + column.Length + ")" : "");
        }

        public string GetSQLColumnChangeDefault(Column column)
        {
            throw new NotImplementedException();
        }

        public string GetSQLConstraintList()
        {
            return string.Format(@"select distinct
	Con.Name, Con.TableName, Con.Type, Col.ColumnName, Col.Position, Con.RefConstraintName, Con.UpdateRule, Con.DeleteRule
from
	(select c.CONSTRAINT_NAME as Name, c.TABLE_NAME as TableName, c.CONSTRAINT_TYPE as Type, r.UNIQUE_CONSTRAINT_NAME as RefConstraintName, r.UPDATE_RULE AS UpdateRule, r.DELETE_RULE as DeleteRule from INFORMATION_SCHEMA.TABLE_CONSTRAINTS c left join INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS r ON r.CONSTRAINT_NAME=c.CONSTRAINT_NAME where c.table_catalog='{0}') as Con,
	(select CONSTRAINT_NAME as ConstraintName, TABLE_NAME as TableName, COLUMN_NAME as ColumnName, ORDINAL_POSITION as Position from INFORMATION_SCHEMA.KEY_COLUMN_USAGE where table_catalog='{0}') as Col
where
	Con.Name = Col.ConstraintName
order by Con.Name, Con.TableName, Con.Type, Col.ColumnName, Col.Position", db.Name);
        }

        public string GetSQLConstraintRemove(Constraint constraint)
        {
            return string.Format("ALTER TABLE [{0}] DROP CONSTRAINT [{1}]", constraint.Table.Name, constraint.Name);
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
            return string.Format("ALTER TABLE [{0}] WITH NOCHECK ADD CONSTRAINT [{1}] CHECK ({2})", constraint.Table.Name, constraint.Name, constraint.Expression);
        }

        public string GetSQLConstraintAdd(UniqueConstraint constraint)
        {
            return string.Format("ALTER TABLE [{0}] ADD CONSTRAINT [{1}] UNIQUE ([{2}])", constraint.Table.Name, constraint.Name, string.Join("],[", constraint.ColumnNames.ToArray()));
        }

        public string GetSQLConstraintAdd(ForeignKeyConstraint constraint)
        {
            return string.Format("ALTER TABLE [{0}] ADD CONSTRAINT [{1}] FOREIGN KEY ([{2}]) REFERENCES [{3}]([{4}])", constraint.Table.Name, constraint.Name, string.Join("],[", constraint.ColumnNames.ToArray()), constraint.RefTableName, string.Join("],[", db.Tables[constraint.RefTableName].Constraints[constraint.RefConstraintName].ColumnNames.ToArray()));
        }

        public string GetSQLConstraintAdd(PrimaryKeyConstraint constraint)
        {
            return string.Format("ALTER TABLE [{0}] ADD CONSTRAINT [{1}] PRIMARY KEY ([{2}])", constraint.Table.Name, constraint.Name, string.Join("],[", constraint.ColumnNames.ToArray()));
        }

        public string GetSQLConstraintRemove(PrimaryKeyConstraint constraint)
        {
            return GetSQLConstraintRemove(constraint);
        }

        public string GetSQLColumnAddNotNull(Column column)
        {
            return string.Format("ALTER TABLE [{0}] ALTER COLUMN [{1}] {2}{3} NOT NULL", column.Table.Name, column.Name, column.ColumnTypeOriginal, column.SimpleColumnType == SimpleDbType.String ? "(" + column.Length + ")" : "");
        }

        public string GetSQLColumnRemoveNotNull(Column column)
        {
            return string.Format("ALTER TABLE [{0}] ALTER COLUMN [{1}] {2}{3} NULL", column.Table.Name, column.Name, column.ColumnTypeOriginal, column.SimpleColumnType == SimpleDbType.String ? "(" + column.Length + ")" : "");
        }

        public string GetSQLColumnSetAutoIncrement(Column column)
        {
            throw new NotImplementedException();
        }

        public string GetSQLColumnRemoveAutoIncrement(Column column)
        {
            throw new NotImplementedException();
        }

        public string GetSQLIndexAdd(Index index)
        {
            return string.Format("CREATE INDEX [{0}] ON [{1}] ([{2}])", index.Name, index.Table.Name, string.Join("],[", index.ColumnNames.ToArray()));
        }

        public string GetSQLIndexRemove(Index index)
        {
            return string.Format("DROP INDEX [{0}] ON [{1}]", index.Name, index.Table.Name);
        }

        public string GetSQLViewCreate(Table view)
        {
            return db.GetString("sp_helptext '" + view.Name + "';");
        }

        #endregion
    }
}
