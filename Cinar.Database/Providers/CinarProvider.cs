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
using Cinar.SQLEngine;

namespace Cinar.Database.Providers
{
    /// <summary>
    /// Cinar'dan metadata okuyan sýnýf
    /// </summary>
    internal class CinarProvider : BaseProvider, IDatabaseProvider
    {
        public CinarProvider(Database db, bool createDatabaseIfNotExist)
        {
            this.db = db;
            this.connection = new CinarConnection("cinar");
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
                    Column f = new Column();
                    f.DefaultValue = drColumn["COLUMN_DEFAULT"].ToString();
                    if (f.DefaultValue == "\0") f.DefaultValue = "";
                    f.ColumnTypeOriginal = drColumn["DATA_TYPE"].ToString();
                    f.ColumnType = Column.GetDbTypeOf(Type.GetType("System."+f.ColumnTypeOriginal));
                    f.Length = drColumn.IsNull("CHARACTER_MAXIMUM_LENGTH") ? 0 : Convert.ToInt64(drColumn["CHARACTER_MAXIMUM_LENGTH"]);
                    f.IsNullable = drColumn["IS_NULLABLE"].ToString() != "NO";
                    f.Name = drColumn["COLUMN_NAME"].ToString();
                    f.IsAutoIncrement = drColumn["IS_AUTO_INCREMENT"].ToString() == "1";

                    tbl.Columns.Add(f);
                }
            }
            
            /*
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
                        index.ColumnNames.Add(drKey["Column_name"].ToString());

                        if (tbl.Indices[index.Name] == null)
                            tbl.Indices.Add(index);
                    }
            }
            */
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
            return new CinarConnection(db.ConnectionString);
        }

        public DbDataAdapter CreateDataAdapter(IDbCommand selectCommand)
        {
            return new CinarDataAdapter((CinarCommand) selectCommand);
        }

        public DbDataAdapter CreateDataAdapter(string selectCommandText, params object[] parameters)
        {
            return this.CreateDataAdapter(CreateCommand(selectCommandText, parameters));
        }

        public IDbCommand CreateCommand(string cmdText, params object[] parameters)
        {
            return this.CreateCommand(cmdText, null, parameters);
        }

        public IDbCommand CreateCommand(string cmdText, IDbTransaction transaction, params object[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
                cmdText = cmdText.Replace("{" + i + "}", "@_param" + i);

            CinarCommand cmd = null;
            if (transaction != null)
                cmd = new CinarCommand(cmdText, (CinarConnection)transaction.Connection, (CinarTransaction)transaction);
            else
                cmd = new CinarCommand(cmdText, (CinarConnection)this.Connection);

            for (int i = 0; i < parameters.Length; i++)
                cmd.Parameters.AddWithValue("@_param" + i, parameters[i]);

            return cmd;
        }

        public IDbDataParameter CreateParameter(string parameterName, object value)
        {
            return new CinarParameter(parameterName, value);
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
            if (column.IsAutoIncrement)
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
                sbCons.Append(GetSQLIndexAdd(i) + ";" + Environment.NewLine);


            return String.Format("CREATE {0} `{1}`(\r\n{2});\r\n{3}" + Environment.NewLine,
                (table.IsView ? "VIEW" : "TABLE"),
                table.Name,
                sbColumns,
                sbCons);
        }

        #endregion

        #region
        public string GetSQLTableList()
        {
            return string.Format(@"
					    SELECT 
                            table_name, 
                            table_type 
                        FROM 
                            information_schema.tables", db.Name);
        }

        public string GetSQLTableRename(string oldName, string newName)
        {
            throw new NotImplementedException();
        }

        public string GetSQLTableDrop(Table table)
        {
            throw new NotImplementedException();
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
                            IS_AUTO_INCREMENT
						from
							INFORMATION_SCHEMA.COLUMNS
						WHERE
							TABLE_NAME='{0}'
                        ORDER BY ORDINAL_POSITION", tableName);
        }

        public string GetSQLColumnAdd(string toTable, Column column)
        {
            throw new NotImplementedException();
        }

        public string GetSQLColumnRemove(Column column)
        {
            throw new NotImplementedException();
        }

        public string GetSQLColumnRename(string oldColumnName, Column column)
        {
            throw new NotImplementedException();
        }

        public string GetSQLColumnChangeDataType(Column column)
        {
            throw new NotImplementedException();
        }

        public string GetSQLColumnChangeDefault(Column column)
        {
            throw new NotImplementedException();
        }

        public string GetSQLConstraintList()
        {
            throw new NotImplementedException();
        }

        public string GetSQLConstraintRemove(Constraint constraint)
        {
            throw new NotImplementedException();
        }

        public string GetSQLConstraintAdd(Constraint constraint)
        {
            throw new NotImplementedException();
        }

        public string GetSQLConstraintAdd(CheckConstraint constraint)
        {
            throw new NotImplementedException();
        }

        public string GetSQLConstraintAdd(UniqueConstraint constraint)
        {
            throw new NotImplementedException();
        }

        public string GetSQLConstraintAdd(ForeignKeyConstraint constraint)
        {
            throw new NotImplementedException();
        }

        public string GetSQLConstraintAdd(PrimaryKeyConstraint constraint)
        {
            throw new NotImplementedException();
        }

        public string GetSQLConstraintRemove(PrimaryKeyConstraint constraint)
        {
            throw new NotImplementedException();
        }

        public string GetSQLColumnAddNotNull(Column column)
        {
            throw new NotImplementedException();
        }

        public string GetSQLColumnRemoveNotNull(Column column)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public string GetSQLIndexRemove(Index index)
        {
            throw new NotImplementedException();
        }

        public string GetSQLViewCreate(Table view)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
