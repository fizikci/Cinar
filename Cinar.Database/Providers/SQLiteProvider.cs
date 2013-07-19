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
using System.Data.SQLite;
using System.Data.Common;

namespace Cinar.Database.Providers
{
    /// <summary>
    /// Sqlite'den metadata okuyan sýnýf
    /// </summary>
    internal class SQLiteProvider : BaseProvider, IDatabaseProvider
    {
        public SQLiteProvider(IDatabase db, bool createDatabaseIfNotExist)
        {
            this.db = db;
            try
            {
                connection = new SQLiteConnection(db.ConnectionString);
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
                    string newConnStr = db.ConnectionString;
                    if(!newConnStr.Trim().EndsWith(";")) newConnStr += ";";
                    connection = new SQLiteConnection(newConnStr + "New=True;");
                    try
                    {
                        connection.Open();
                        CreatedNow = true;
                        connection.Close();
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

            bool connectionAlreadyOpen = connection.State == ConnectionState.Open;

            // columns
            if(!connectionAlreadyOpen) connection.Open();
            DataTable dtTables = ((DbConnection)connection).GetSchema("Tables");
            if (!connectionAlreadyOpen) connection.Close();
            foreach (DataRow drTable in dtTables.Rows)
            {
                Table tbl = new Table();
                tbl.Name = drTable["TABLE_NAME"].ToString();
                tbl.IsView = drTable["TABLE_TYPE"].ToString() == "VIEW";
                db.Tables.Add(tbl);
            }

            if (!connectionAlreadyOpen) connection.Open();
            DataTable dtColumns = ((DbConnection)connection).GetSchema("Columns");
            if (!connectionAlreadyOpen) connection.Close();
            foreach (DataRow drColumn in dtColumns.Rows)
            {
                Table tbl = db.Tables[drColumn["TABLE_NAME"].ToString()];
                if (tbl==null)
                    continue;

                Column f = new Column();
                f.DefaultValue = drColumn["COLUMN_DEFAULT"].ToString();
                if (f.DefaultValue == "\0") f.DefaultValue = "";
                f.ColumnTypeOriginal = drColumn["DATA_TYPE"].ToString().ToUpperInvariant();
                f.ColumnType = StringToDbType(f.ColumnTypeOriginal);
                f.Length = drColumn.IsNull("CHARACTER_MAXIMUM_LENGTH") ? 0 : Convert.ToInt64(drColumn["CHARACTER_MAXIMUM_LENGTH"]);
                f.IsNullable = drColumn["IS_NULLABLE"].ToString() != "NO";
                f.Name = drColumn["COLUMN_NAME"].ToString();
                f.IsAutoIncrement = drColumn["AUTOINCREMENT"].ToString() == "True";

                tbl.Columns.Add(f);
            }

            if (!connectionAlreadyOpen) connection.Open();
            DataTable dtIndexes = ((DbConnection)connection).GetSchema("Indexes");
            if (!connectionAlreadyOpen) connection.Close();
            foreach (DataRow drIndex in dtIndexes.Rows)
            {
                bool primary = (bool)drIndex["PRIMARY_KEY"];
                bool unique = (bool)drIndex["UNIQUE"];

                if(primary && unique)
                {
                    PrimaryKeyConstraint pk = new PrimaryKeyConstraint();
                    pk.Name = drIndex["INDEX_NAME"].ToString();
                    db.Tables[drIndex["TABLE_NAME"].ToString()].Constraints.Add(pk);
                }
                else if (unique)
                {
                    UniqueConstraint uc = new UniqueConstraint();
                    uc.Name = drIndex["INDEX_NAME"].ToString();
                    db.Tables[drIndex["TABLE_NAME"].ToString()].Constraints.Add(uc);
                }
                else
                {
                    Index index = new Index();
                    index.Name = drIndex["INDEX_NAME"].ToString();
                    db.Tables[drIndex["TABLE_NAME"].ToString()].Indices.Add(index);
                }
            }

            if (!connectionAlreadyOpen) connection.Open();
            DataTable dtIndexColumns = ((DbConnection)connection).GetSchema("IndexColumns");
            if (!connectionAlreadyOpen) connection.Close();
            foreach (DataRow drCol in dtIndexColumns.Rows)
            {
                BaseIndexConstraint index = db.GetConstraint(drCol["CONSTRAINT_NAME"].ToString());
                if(index==null)
                    index = db.Tables[drCol["TABLE_NAME"].ToString()].Indices[drCol["CONSTRAINT_NAME"].ToString()];

                if (index == null) continue; //***

                index.ColumnNames.Add(drCol["COLUMN_NAME"].ToString());
            }
        }

        #region string <=> dbType conversion

        public override Dictionary<DbType, string> DEFDbTypeToString { get { return _DEFDbTypeToString; } }
        private Dictionary<DbType, string> _DEFDbTypeToString = new Dictionary<DbType, string>() 
        { 
            {DbType.Binary, "BLOB"},
            {DbType.Blob , "BLOB"},
            {DbType.BlobLong , "BLOB"},
            {DbType.BlobMedium, "BLOB"},
            {DbType.BlobTiny, "BLOB"},
            {DbType.Boolean, "BOOLEAN"},
            {DbType.Byte, "INT2"},
            {DbType.Char, "CHARACTER"},
            {DbType.Currency, "DECIMAL"},
            {DbType.CurrencySmall, "DECIMAL"},
            {DbType.Date, "DATE"},
            {DbType.DateTime, "DATETIME"},
            {DbType.DateTimeSmall, "DATETIME"},
            {DbType.Decimal, "DECIMAL"},
            {DbType.Double, "DOUBLE"},
            {DbType.Enum, "VARCHAR"},
            {DbType.Float, "FLOAT"},
            {DbType.Guid, "VARCHAR"},
            {DbType.Image, "BLOB"},
            {DbType.Int16, "INT2"},
            {DbType.Int32, "INTEGER"},
            {DbType.Int64, "INT8"},
            {DbType.NChar, "NCHAR"},
            {DbType.NText, "TEXT"},
            {DbType.Numeric, "NUMERIC"},
            {DbType.NVarChar, "NVARCHAR"},
            {DbType.Real, "REAL"},
            {DbType.Set, "???"},
            {DbType.Text, "TEXT"},
            {DbType.TextLong, "TEXT"},
            {DbType.TextMedium, "TEXT"},
            {DbType.TextTiny, "TEXT"},
            {DbType.Time, "INTEGER"},
            {DbType.Timestamp, "INTEGER"},
            {DbType.Timestamptz, "INTEGER"},
            {DbType.Timetz, "INTEGER"},
            {DbType.Undefined, "???"},
            {DbType.VarBinary, "BLOB"},
            {DbType.VarChar, "VARCHAR"},
            {DbType.Variant, "BLOB"},
            {DbType.Xml, "TEXT"}
        };

        public override Dictionary<string, DbType> DEFStringToDbType { get { return _DEFStringToDbType; } }
        private Dictionary<string, DbType> _DEFStringToDbType = new Dictionary<string, DbType>() 
        { 
            {"INT", DbType.Int32},
            {"INTEGER", DbType.Int32},
            {"TINYINT", DbType.Int16},
            {"SMALLINT", DbType.Int16},
            {"MEDIUMINT", DbType.Int32},
            {"BIGINT", DbType.Int64},
            {"UNSIGNED BIG INT", DbType.Int64},
            {"INT2", DbType.Int16},
            {"INT8", DbType.Int64},
            {"CHARACTER", DbType.Char},
            {"VARCHAR", DbType.VarChar},
            {"VARYING CHARACTER", DbType.VarChar},
            {"NCHAR", DbType.NChar},
            {"NATIVE CHARACTER", DbType.NChar},
            {"NVARCHAR", DbType.NVarChar},
            {"TEXT", DbType.Text},
            {"CLOB", DbType.Blob},
            {"BLOB", DbType.Blob},
            {"REAL", DbType.Real},
            {"DOUBLE", DbType.Double},
            {"DOUBLE PRECISION", DbType.Double},
            {"FLOAT", DbType.Float},
            {"NUMERIC", DbType.Numeric},
            {"DECIMAL", DbType.Decimal},
            {"BOOLEAN", DbType.Boolean},
            {"DATE", DbType.Date},
            {"DATETIME", DbType.DateTime}
        };


        public string[] GetColumnTypes() {
            return DEFStringToDbType.Keys.ToArray();
        }

        #endregion

        #region IDatabaseProvider Members

        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection(db.ConnectionString);
        }

        public DbDataAdapter CreateDataAdapter(System.Data.IDbCommand selectCommand)
        {
            return new SQLiteDataAdapter((SQLiteCommand) selectCommand);
        }

        public DbDataAdapter CreateDataAdapter(string selectCommandText, params object[] parameters)
        {
            return this.CreateDataAdapter(this.CreateCommand(selectCommandText, parameters));
        }

        public IDbCommand CreateCommand(string cmdText, params object[] parameters)
        {
            return this.CreateCommand(cmdText, (IDbTransaction)null, parameters);
        }

        public IDbCommand CreateCommand(string cmdText, System.Data.IDbTransaction transaction, params object[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
                cmdText = cmdText.Replace("{" + i + "}", "@_param" + i);

            SQLiteCommand cmd = null;
            if (transaction != null)
                cmd = new SQLiteCommand(cmdText, (SQLiteConnection)transaction.Connection, (SQLiteTransaction)transaction);
            else
                cmd = new SQLiteCommand(cmdText, (SQLiteConnection)this.Connection);

            for (int i = 0; i < parameters.Length; i++)
                cmd.Parameters.AddWithValue("@_param" + i, parameters[i]);

            return cmd;
        }

        public IDbDataParameter CreateParameter(string parameterName, object value)
        {
            return new SQLiteParameter(parameterName, value);
        }

        public string GetColumnDDL(Column column)
        {
            StringBuilder columnDDL = new StringBuilder();
            string columnTypeName = DbTypeToString(column.ColumnType).ToLowerInvariant();
            columnDDL.Append("[" + column.Name + "] " + columnTypeName);
            if (columnTypeName == "char" || columnTypeName == "varchar" || columnTypeName == "nchar" || columnTypeName == "nvarchar")
                columnDDL.Append("(" + (column.Length == 0 ? 50 : column.Length) + ")");
            //if (column.ColumnType == DbType.VarChar || column.ColumnType == DbType.Text)
            //    fieldDDL.Append(" CHARACTER SET utf8 COLLATE utf8_turkish_ci");
            columnDDL.Append(column.IsNullable ? "" : " NOT NULL");
            if (column.IsPrimaryKey)
                columnDDL.Append(" PRIMARY KEY");
            if (column.IsAutoIncrement)
                columnDDL.Append(" AUTOINCREMENT");
            if (!string.IsNullOrEmpty(column.DefaultValue))
                columnDDL.Append(" DEFAULT " + getDefaultValue(column));
            if (column.ReferenceColumn != null)
                columnDDL.Append(" REFERENCES [" + column.ReferenceColumn.Table.Name + "]([" + column.ReferenceColumn.Name + "])");

            if (column.IsStringType())
                columnDDL.Append(" COLLATE NOCASE");

            return columnDDL.ToString();
        }
        private string getSimpleColumnDDL(Column column)
        {
            StringBuilder columnDDL = new StringBuilder();
            string columnTypeName = DbTypeToString(column.ColumnType).ToLowerInvariant();
            columnDDL.Append("`" + column.Name + "` " + columnTypeName);
            if (columnTypeName == "char" || columnTypeName == "varchar" || columnTypeName == "nchar" || columnTypeName == "nvarchar")
                columnDDL.Append("(" + (column.Length == 0 ? 50 : column.Length) + ")");
            columnDDL.Append(column.IsNullable ? " NULL" : " NOT NULL");
            if (!string.IsNullOrEmpty(column.DefaultValue))
                columnDDL.Append(" DEFAULT " + getDefaultValue(column));

            return columnDDL.ToString();
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
            {
                if (c is PrimaryKeyConstraint && c.ColumnNames.Count == 1)
                    continue;
                sbCons.Append(GetSQLConstraintAdd(c) + ";" + Environment.NewLine);
            }
            foreach (Index i in table.Indices)
                sbCons.Append(GetSQLIndexAdd(i) + ";" + Environment.NewLine);

            if (table.IsView)
                return String.Format("CREATE VIEW [{0}] AS {1}" + Environment.NewLine,
                    table.Name,
                    table.ViewSQL);

            return String.Format("CREATE TABLE [{0}](\r\n{1});\r\n{2}" + Environment.NewLine,
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
						    INFORMATION_SCHEMA.TABLES
                        where
                            TABLE_SCHEMA='{0}'", db.Name);
        }

        public string GetSQLTableRename(string oldName, string newName)
        {
            return string.Format("ALTER TABLE `{0}` RENAME TO `{1}`", oldName, newName);
        }

        public string GetSQLTableDrop(Table table)
        {
            return string.Format("DROP {0} `{1}`", table.IsView ? "VIEW" : "TABLE", table.Name);
        }

        public string GetSQLColumnList(string tableName)
        {
            return string.Format(@"
					    SELECT
							COLUMN_NAME,
                            COLUMN_TYPE,
							DATA_TYPE,
							CHARACTER_MAXIMUM_LENGTH,
							IS_NULLABLE,
							COLUMN_DEFAULT,
                            CASE WHEN EXTRA LIKE '%auto_increment%' THEN 1 ELSE 0 END AS IS_AUTO_INCREMENT
						FROM
							INFORMATION_SCHEMA.COLUMNS
						WHERE
							TABLE_NAME='{0}' AND TABLE_SCHEMA='{1}'
                        ORDER BY ORDINAL_POSITION", tableName, db.Name);
        }

        public string GetSQLColumnAdd(string toTable, Column column)
        {
            return string.Format("ALTER TABLE `{0}` ADD {1}", toTable, GetColumnDDL(column));
        }

        public string GetSQLColumnRemove(Column column)
        {
            return string.Format("ALTER TABLE `{0}` DROP COLUMN `{1}`", column.Table.Name, column.Name);
        }

        public string GetSQLColumnRename(string oldColumnName, Column column)
        {
            return string.Format("ALTER TABLE `{0}` CHANGE `{1}` `{2}` {3}", column.Table.Name, oldColumnName, column.Name, getSimpleColumnDDL(column));
        }

        public string GetSQLColumnChangeDataType(Column column)
        {
            return string.Format("ALTER TABLE `{0}` MODIFY COLUMN `{1}` {2}{3}", column.Table.Name, column.Name, column.ColumnTypeOriginal, column.SimpleColumnType == SimpleDbType.String ? "(" + column.Length + ")" : "");
        }

        public string GetSQLColumnChangeDefault(Column column)
        {
            return string.Format("ALTER TABLE `{0}` ALTER COLUMN `{1}` SET DEFAULT {2}", column.Table.Name, column.Name, column.DefaultValue);
        }

        public string GetSQLConstraintList()
        {
            return string.Format(@"select distinct
	Con.Name, Con.Type, Con.TableName, Col.ColumnName, Col.Position, Con.RefTableName, Con.RefConstraintName, Con.UpdateRule, Con.DeleteRule
from
	(select c.CONSTRAINT_NAME as Name, c.TABLE_NAME as TableName, c.CONSTRAINT_TYPE as Type, r.REFERENCED_TABLE_NAME as RefTableName, r.UNIQUE_CONSTRAINT_NAME as RefConstraintName, r.UPDATE_RULE AS UpdateRule, R.DELETE_RULE as DeleteRule from INFORMATION_SCHEMA.TABLE_CONSTRAINTS c left join INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS r ON r.CONSTRAINT_NAME=c.CONSTRAINT_NAME where c.table_schema='{0}') as Con,
	(select CONSTRAINT_NAME as ConstraintName, TABLE_NAME as TableName, COLUMN_NAME as ColumnName, ORDINAL_POSITION as Position from INFORMATION_SCHEMA.KEY_COLUMN_USAGE where table_schema='{0}') as Col
where
	Con.Name = Col.ConstraintName
order by Con.Name, Con.Type, Con.TableName, Col.ColumnName, Col.Position", db.Name);
        }

        public string GetSQLConstraintRemove(Constraint constraint)
        {
            if (constraint is PrimaryKeyConstraint)
                return GetSQLConstraintRemove(constraint as PrimaryKeyConstraint);
            if (constraint is UniqueConstraint)
                return string.Format("DROP INDEX `{0}` ON `{1}`", constraint.Name, constraint.Table.Name);
            if (constraint is CheckConstraint)
                throw new NotImplementedException("SQLite doesn't support check constraints. Use trigger.");
            if (constraint is ForeignKeyConstraint)
                return string.Format("ALTER TABLE `{0}` DROP FOREIGN KEY `{1}`", constraint.Table.Name, constraint.Name);

            throw new Exception("Unknown constraint type: " + constraint.GetType().Name);
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
            throw new NotImplementedException("SQLite doesn't support check constraints. Use trigger.");
        }

        public string GetSQLConstraintAdd(UniqueConstraint constraint)
        {
            return string.Format("CREATE UNIQUE INDEX `{0}` ON `{1}` (`{2}`)", constraint.Name, constraint.Table.Name, string.Join("`,`", constraint.ColumnNames.ToArray()));
        }

        public string GetSQLConstraintAdd(ForeignKeyConstraint constraint)
        {
            return string.Format("ALTER TABLE `{0}` ADD CONSTRAINT `{1}` FOREIGN KEY (`{2}`) REFERENCES `{3}`(`{4}`)", constraint.Table.Name, constraint.Name, string.Join("`,`", constraint.ColumnNames.ToArray()), constraint.RefTableName, string.Join("`,`", db.Tables[constraint.RefTableName].Constraints[constraint.RefConstraintName].ColumnNames.ToArray()));
        }

        public string GetSQLConstraintAdd(PrimaryKeyConstraint constraint)
        {
            return string.Format("ALTER TABLE `{0}` ADD PRIMARY KEY (`{1}`)", constraint.Table.Name, string.Join("`,`", constraint.ColumnNames.ToArray()));
        }

        public string GetSQLConstraintRemove(PrimaryKeyConstraint constraint)
        {
            return string.Format("ALTER TABLE `{0}` DROP PRIMARY KEY", constraint.Table.Name);
        }

        public string GetSQLColumnAddNotNull(Column column)
        {
            return string.Format("ALTER TABLE `{0}` MODIFY COLUMN {1}", column.Table.Name, GetColumnDDL(column));
        }

        public string GetSQLColumnRemoveNotNull(Column column)
        {
            return string.Format("ALTER TABLE `{0}` CHANGE `{1}` `{1}` {2}{3} NULL", column.Table.Name, column.Name, column.ColumnTypeOriginal, column.SimpleColumnType == SimpleDbType.String ? "(" + column.Length + ")" : "");
        }

        public string GetSQLColumnSetAutoIncrement(Column column)
        {
            return string.Format("ALTER TABLE `{0}` CHANGE `{1}` {2}", column.Table.Name, column.Name, GetColumnDDL(column));
        }

        public string GetSQLColumnRemoveAutoIncrement(Column column)
        {
            return string.Format("ALTER TABLE `{0}` CHANGE `{1}` {2}", column.Table.Name, column.Name, GetColumnDDL(column));
        }

        public string GetSQLIndexAdd(Index index)
        {
            return string.Format("CREATE INDEX `{0}` ON `{1}` (`{2}`)", index.Name, index.Table.Name, string.Join("`,`", index.ColumnNames.ToArray()));
        }

        public string GetSQLIndexRemove(Index index)
        {
            return string.Format("DROP INDEX `{0}` ON `{1}`", index.Name, index.Table.Name);
        }

        public string GetSQLViewCreate(Table view)
        {
            return (string)db.GetDataRow("show create table `" + db.Name + "`.`" + view.Name + "`;")[1];
        }

        public string GetSQLDateYearMonthPart(string columnName)
        {
            return string.Format("strftime('%Y.%m', [{0}])", columnName);
        }

        #endregion

    }
}
