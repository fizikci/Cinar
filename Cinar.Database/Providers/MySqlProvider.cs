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
        public MySqlProvider(Database db, bool createDatabaseIfNotExist)
        {
            this.db = db;
            try
            {
                connection = new MySqlConnection(db.ConnectionString);
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
            db.Tables = new TableCollection((Database)db);

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
            }

            // constraints
            // Con.Name, Con.TableName, Con.Type, Col.ColumnName, Con.RefConstraintName, Con.UpdateRule, Con.DeleteRule
            DataTable dtCons = db.GetDataTable(this.GetSQLConstraintList());
            ConstraintCollection conCol = new ConstraintCollection();
            foreach (DataRow drCon in dtCons.Rows)
            {
                Constraint con = db.Tables[drCon["TableName"].ToString()].Constraints[drCon["Name"].ToString()];
                if (con != null)
                {
                    con.FieldNames.Add(drCon["ColumnName"].ToString());
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
                }
                con.Name = drCon["Name"].ToString();
                con.FieldNames.Add(drCon["ColumnName"].ToString());

                db.Tables[drCon["TableName"].ToString()].Constraints.Add(con);
            }

            // indices
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
                        index.FieldNames.Add(drKey["Column_name"].ToString());

                        if (tbl.Indices[index.Name] == null)
                            tbl.Indices.Add(index);
                    }
            }
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

        public string[] GetFieldTypes() {
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

        public System.Data.IDbDataParameter CreateParameter(string parameterName, object value)
        {
            return new MySqlParameter(parameterName, value);
        }

        public string GetFieldDDL(Field field)
        {
            StringBuilder fieldDDL = new StringBuilder();
            string fieldTypeName = DbTypeToString(field.FieldType).ToLowerInvariant();
            fieldDDL.Append("`" + field.Name + "` " + fieldTypeName);
            if (fieldTypeName == "char" || fieldTypeName == "varchar" || fieldTypeName == "nchar" || fieldTypeName == "nvarchar")
                fieldDDL.Append("(" + (field.Length == 0 ? 50 : field.Length) + ")");
            //if (field.FieldType == DbType.VarChar || field.FieldType == DbType.Text)
            //    fieldDDL.Append(" CHARACTER SET utf8 COLLATE utf8_turkish_ci");
            fieldDDL.Append(field.IsNullable ? " NULL" : " NOT NULL");
            if (field.IsAutoIncrement)
                fieldDDL.Append(" AUTO_INCREMENT");
            if (field.IsPrimaryKey)
                fieldDDL.Append(" PRIMARY KEY");
            if (!string.IsNullOrEmpty(field.DefaultValue))
                fieldDDL.Append(" DEFAULT " + getDefaultValue(field));
            if (field.ReferenceField != null)
                fieldDDL.Append(" REFERENCES `" + field.ReferenceField.Table.Name + "`(`" + field.ReferenceField.Name + "`)");

            return fieldDDL.ToString();
        }
        private string getSimpleFieldDDL(Field field)
        {
            StringBuilder fieldDDL = new StringBuilder();
            string fieldTypeName = DbTypeToString(field.FieldType).ToLowerInvariant();
            fieldDDL.Append("`" + field.Name + "` " + fieldTypeName);
            if (fieldTypeName == "char" || fieldTypeName == "varchar" || fieldTypeName == "nchar" || fieldTypeName == "nvarchar")
                fieldDDL.Append("(" + (field.Length == 0 ? 50 : field.Length) + ")");
            fieldDDL.Append(field.IsNullable ? " NULL" : " NOT NULL");
            if (!string.IsNullOrEmpty(field.DefaultValue))
                fieldDDL.Append(" DEFAULT " + getDefaultValue(field));

            return fieldDDL.ToString();
        }

        public string GetTableDDL(Table table)
        {
            int len = ("," + Environment.NewLine).Length;

            StringBuilder sbFields = new StringBuilder();
            foreach (Field f in table.Fields)
                sbFields.Append("\t" + GetFieldDDL(f) + "," + Environment.NewLine);
            sbFields = sbFields.Remove(sbFields.Length - len, len);

            StringBuilder sbCons = new StringBuilder();
            foreach (Constraint c in table.Constraints)
            {
                if (c is PrimaryKeyConstraint && c.FieldNames.Count == 1)
                    continue;
                sbCons.Append(GetSQLConstraintAdd(c) + ";" + Environment.NewLine);
            }
            foreach (Index i in table.Indices)
                sbCons.Append(GetSQLIndexAdd(i) + ";" + Environment.NewLine);


            return String.Format("CREATE {0} `{1}`(\r\n{2});\r\n{3}" + Environment.NewLine,
                (table.IsView ? "VIEW" : "TABLE"),
                table.Name,
                sbFields,
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

        public string GetSQLColumnAdd(string toTable, Field column)
        {
            return string.Format("ALTER TABLE `{0}` ADD {1}", toTable, GetFieldDDL(column));
        }

        public string GetSQLColumnRemove(Field column)
        {
            return string.Format("ALTER TABLE `{0}` DROP COLUMN `{1}`", column.Table.Name, column.Name);
        }

        public string GetSQLColumnRename(string oldColumnName, Field column)
        {
            return string.Format("ALTER TABLE `{0}` CHANGE `{1}` `{2}` {3}", column.Table.Name, oldColumnName, column.Name, getSimpleFieldDDL(column));
        }

        public string GetSQLColumnChangeDataType(Field column)
        {
            return string.Format("ALTER TABLE `{0}` MODIFY COLUMN `{1}` {2}{3}", column.Table.Name, column.Name, column.FieldTypeOriginal, column.SimpleFieldType == SimpleDbType.String ? "(" + column.Length + ")" : "");
        }

        public string GetSQLColumnChangeDefault(Field column)
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
            throw new NotImplementedException();
            //return string.Format("ALTER TABLE `{0}` DROP CONSTRAINT `{1}`", constraint.Table.Name, constraint.Name);
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
            throw new NotImplementedException();
        }

        public string GetSQLConstraintAdd(UniqueConstraint constraint)
        {
            return string.Format("CREATE UNIQUE INDEX `{0}` ON `{1}` (`{2}`)", constraint.Name, constraint.Table.Name, string.Join("`,`", constraint.FieldNames.ToArray()));
        }

        public string GetSQLConstraintAdd(ForeignKeyConstraint constraint)
        {
            return string.Format("ALTER TABLE `{0}` ADD CONSTRAINT `{1}` FOREIGN KEY (`{2}`) REFERENCES `{3}`(`{4}`)", constraint.Table.Name, constraint.Name, string.Join("`,`", constraint.FieldNames.ToArray()), constraint.RefTableName, string.Join("`,`", db.Tables[constraint.RefTableName].Constraints[constraint.RefConstraintName].FieldNames.ToArray()));
        }

        public string GetSQLConstraintAdd(PrimaryKeyConstraint constraint)
        {
            return string.Format("ALTER TABLE `{0}` ADD PRIMARY KEY (`{1}`)", constraint.Table.Name, string.Join("`,`", constraint.FieldNames.ToArray()));
        }

        public string GetSQLConstraintRemove(PrimaryKeyConstraint constraint)
        {
            return string.Format("ALTER TABLE `{0}` DROP PRIMARY KEY", constraint.Table.Name);
        }

        public string GetSQLColumnAddNotNull(Field column)
        {
            return string.Format("ALTER TABLE `{0}` MODIFY COLUMN {1}", column.Table.Name, GetFieldDDL(column));
        }

        public string GetSQLColumnRemoveNotNull(Field column)
        {
            return string.Format("ALTER TABLE `{0}` CHANGE `{1}` `{1}` {2}{3} NULL", column.Table.Name, column.Name, column.FieldTypeOriginal, column.SimpleFieldType == SimpleDbType.String ? "(" + column.Length + ")" : "");
        }

        public string GetSQLColumnSetAutoIncrement(Field column)
        {
            return string.Format("ALTER TABLE `{0}` CHANGE `{1}` {2}", column.Table.Name, column.Name, GetFieldDDL(column));
        }

        public string GetSQLColumnRemoveAutoIncrement(Field column)
        {
            return string.Format("ALTER TABLE `{0}` CHANGE `{1}` {2}", column.Table.Name, column.Name, GetFieldDDL(column));
        }

        public string GetSQLIndexAdd(Index index)
        {
            return string.Format("CREATE INDEX `{0}` ON `{1}` (`{2}`)", index.Name, index.Table.Name, string.Join("`,`", index.FieldNames.ToArray()));
        }

        public string GetSQLIndexRemove(Index index)
        {
            return string.Format("DROP INDEX `{0}` ON `{1}`", index.Name, index.Table.Name);
        }

        #endregion
    }
}
