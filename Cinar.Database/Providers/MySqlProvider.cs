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
    internal class MySqlProvider : BaseProvider, IDatabaseProvider
    {
        public MySqlProvider(IDatabase db, bool createDatabaseIfNotExist)
        {
            this.db = db;
            try
            {
                connection = new MySqlConnection(db.ConnectionString);
                //connection.Open();
                //IDbCommand cmd = connection.CreateCommand();
                //cmd.CommandText = "SET NAMES 'utf8';";
                //cmd.ExecuteNonQuery();
                //connection.Close();
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
                    connection = new MySqlConnection(newConnStr);
                    try
                    {
                        connection.Open();
                        IDbCommand cmd = connection.CreateCommand();
                        cmd.CommandText = "create database " + dbName + " default charset utf8 collate utf8_turkish_ci";
                        cmd.ExecuteNonQuery();
                        connection.ChangeDatabase(dbName);
                        CreatedNow = true;
                        connection.Close();
                        connection = new MySqlConnection(db.ConnectionString);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Metadata okuma iþini yapan asýl metod. Sýrayla bütün veritabaný nesnelerini okur.
        /// </summary>
        /// <param name="db"></param>
        public void ReadDatabaseMetadata(bool readAllMetadata)
        {
            #region tables and views
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
                    if (f.DefaultValue == "\0") f.DefaultValue = "";
                    f.ColumnTypeOriginal = drColumn["COLUMN_TYPE"].ToString()=="tinyint(1)" ? "BOOL" : drColumn["DATA_TYPE"].ToString().ToUpperInvariant();
                    f.ColumnType = StringToDbType(f.ColumnTypeOriginal);
                    f.Length = drColumn.IsNull("CHARACTER_MAXIMUM_LENGTH") ? 0 : Convert.ToInt64(drColumn["CHARACTER_MAXIMUM_LENGTH"]);
                    f.IsNullable = drColumn["IS_NULLABLE"].ToString() != "NO";
                    f.Name = drColumn["COLUMN_NAME"].ToString();
                    f.IsAutoIncrement = drColumn["IS_AUTO_INCREMENT"].ToString() == "1";

                    if (drColumn["COLUMN_KEY"].ToString() == "PRI")
                    {
                        var pk = new PrimaryKeyConstraint();
                        pk.Name = "PRIMARY";
                        pk.ColumnNames.Add(f.Name);
                        tbl.Constraints.Add(pk);
                    }

                    tbl.Columns.Add(f);
                }
            }
            #endregion

            #region constraints
            try
            {
                // Con.Name, Con.TableName, Con.Type, Col.ColumnName, Con.RefConstraintName, Con.UpdateRule, Con.DeleteRule
                DataTable dtCons = db.GetDataTable(this.GetSQLConstraintList());
                foreach (DataRow drCon in dtCons.Rows)
                {
                    Constraint con = db.Tables[drCon["TableName"].ToString()].Constraints[drCon["Name"].ToString()];
                    if (con != null)
                    {
                        if (!con.ColumnNames.Any(cn => cn.ToLowerInvariant() == drCon["ColumnName"].ToString().ToLowerInvariant()))
                            con.ColumnNames.Add(drCon["ColumnName"].ToString());
                        continue;
                    }

                    switch (drCon["Type"].ToString())
                    {
                        case "FOREIGN KEY":
                            con = new ForeignKeyConstraint();
                            (con as ForeignKeyConstraint).RefConstraintName = drCon["RefConstraintName"].ToString();
                            (con as ForeignKeyConstraint).RefTableName = drCon["RefTableName"].ToString();
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
            }
            catch (MySqlException ex) // demek ki MySQL versiyonu < 5.1.16
            {
                // foreign keys
                string sql = @" SELECT
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
                DataTable dtForeigns = db.GetDataTable(String.Format(sql, connection.Database));
                foreach (DataRow drForeign in dtForeigns.Rows)
                {
                    try
                    {
                        ForeignKeyConstraint con = new ForeignKeyConstraint();
                        con.Name = drForeign["CONSTRAINT_NAME"].ToString();
                        con.RefConstraintName = db.Tables[drForeign["TABLE_NAME_1"].ToString()].Constraints.FirstOrDefault(c => c is PrimaryKeyConstraint).Name;
                        con.RefTableName = drForeign["TABLE_NAME_2"].ToString();
                        //con.DeleteRule = drForeign["DeleteRule"].ToString();
                        //con.UpdateRule = drForeign["UpdateRule"].ToString();
                        con.ColumnNames.Add(drForeign["COLUMN_NAME_1"].ToString());
                        db.Tables[drForeign["TABLE_NAME_1"].ToString()].Constraints.Add(con);
                    }
                    catch { }
                }
            }
            #endregion

            if (!readAllMetadata) return; //***

            #region indices
            foreach (Table tbl in db.Tables)
            {
                DataTable dtKeys = db.GetDataTable("SHOW INDEXES FROM `" + tbl.Name + "`");
                if (dtKeys != null)
                    for (int i = 0; i < dtKeys.Rows.Count; i++)
                    {
                        DataRow drKey = dtKeys.Rows[i];
                        
                        if (db.GetConstraint(drKey["Key_name"].ToString()) != null)
                            continue;

                        Index index = tbl.Indices[drKey["Key_name"].ToString()] ?? new Index();
                        index.Name = drKey["Key_name"].ToString();
                        index.ColumnNames.Add(drKey["Column_name"].ToString());

                        if (tbl.Indices[index.Name] == null)
                            tbl.Indices.Add(index);
                    }
            }
            #endregion
        }

        #region string <=> dbType conversion

        public override Dictionary<DbType, string> DEFDbTypeToString { get { return _DEFDbTypeToString; } }
        private Dictionary<DbType, string> _DEFDbTypeToString = new Dictionary<DbType, string>() 
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

        public override Dictionary<string, DbType> DEFStringToDbType { get { return _DEFStringToDbType; } }
        private Dictionary<string, DbType> _DEFStringToDbType = new Dictionary<string, DbType>() 
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

        public string[] GetColumnTypes() {
            return DEFStringToDbType.Keys.ToArray();
        }

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

            MySqlCommand cmd = null;
            if (transaction != null)
                cmd = new MySqlCommand(cmdText, (MySqlConnection)transaction.Connection, (MySqlTransaction)transaction);
            else
                cmd = new MySqlCommand(cmdText, (MySqlConnection)this.Connection);

            for (int i = 0; i < parameters.Length; i++)
                cmd.Parameters.AddWithValue("@_param" + i, parameters[i]);

            return cmd;
        }

        public IDbDataParameter CreateParameter(string parameterName, object value)
        {
            return new MySqlParameter(parameterName, value);
        }

        public string GetColumnDDL(Column column)
        {
            StringBuilder columnDDL = new StringBuilder();
            string columnTypeName = DbTypeToString(column.ColumnType).ToLowerInvariant();
            columnDDL.Append("`" + column.Name + "` " + columnTypeName);
            if (columnTypeName == "char" || columnTypeName == "varchar" || columnTypeName == "nchar" || columnTypeName == "nvarchar")
                columnDDL.Append("(" + (column.Length == 0 ? 50 : column.Length) + ")");
            //if (column.ColumnType == DbType.VarChar || column.ColumnType == DbType.Text)
            //    fieldDDL.Append(" CHARACTER SET utf8 COLLATE utf8_turkish_ci");
            columnDDL.Append(column.IsNullable ? " NULL" : " NOT NULL");
            if (column.IsAutoIncrement && column.IsPrimaryKey)
                columnDDL.Append(" AUTO_INCREMENT");
            if (column.IsPrimaryKey)
                columnDDL.Append(" PRIMARY KEY");
            if (!string.IsNullOrEmpty(column.DefaultValue))
                columnDDL.Append(" DEFAULT " + getDefaultValue(column));
            if (column.ReferenceColumn != null)
                columnDDL.Append(" REFERENCES `" + column.ReferenceColumn.Table.Name + "`(`" + column.ReferenceColumn.Name + "`)");

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
            {
                if (i.Name=="PRIMARY" && i.ColumnNames.Count == 1)
                    continue;
                sbCons.Append(GetSQLIndexAdd(i) + ";" + Environment.NewLine);
            }

            if(table.IsView)
                return String.Format("CREATE VIEW `{0}` AS {1}" + Environment.NewLine,
                    table.Name,
                    table.ViewSQL);

            return String.Format("CREATE TABLE `{0}`(\r\n{1});\r\n{2}" + Environment.NewLine,
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

        public string GetSQLTableDrop(Table table, bool addIfExists)
        {
            return string.Format("DROP {0} {2}`{1}`", table.IsView ? "VIEW" : "TABLE", table.Name, addIfExists ? "IF EXISTS ":"");
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
                            CASE WHEN EXTRA LIKE '%auto_increment%' THEN 1 ELSE 0 END AS IS_AUTO_INCREMENT,
                            COLUMN_KEY
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
            return string.Format("ALTER TABLE `{0}` CHANGE `{1}` {2}", column.Table.Name, oldColumnName, getSimpleColumnDDL(column));
        }

        public string GetSQLColumnChangeDataType(Column column)
        {
            return string.Format("ALTER TABLE `{0}` MODIFY COLUMN `{1}` {2}{3}{4}", 
                column.Table.Name, 
                column.Name, 
                column.ColumnTypeOriginal, 
                column.SimpleColumnType == SimpleDbType.String ? "(" + column.Length + ")" : "",
                column.IsNullable ? "" : " NOT NULL");
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
                throw new NotImplementedException("MySQL doesn't support check constraints. Use trigger.");
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
            throw new NotImplementedException("MySQL doesn't support check constraints. Use trigger.");
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
            return string.Format("DATE_FORMAT([{0}], '%Y.%m')", columnName);
        }

        public string GetSQLDateYearMonthDayPart(string columnName)
        {
            return string.Format("DATE_FORMAT([{0}], '%Y.%m.%d')", columnName);
        }

        public string GetSQLDateYearMonthDayHourPart(string columnName)
        {
            return string.Format("DATE_FORMAT([{0}], '%Y.%m.%d %H')", columnName);
        }

        #endregion

    }
}
