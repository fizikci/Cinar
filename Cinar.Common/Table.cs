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
using System.Linq;
using System.Collections;
using System.Data;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Cinar.Database
{
    /// <summary>
    /// Bir tabloya ait yapı bilgisini modelleyen sınıf.
    /// </summary>
    [Serializable]
    public class Table : IMetadata
    {
        public Table()
        {
            this.Columns = new ColumnCollection(this);
            this.Indices = new IndexCollection(this);
            this.Constraints = new ConstraintCollection(this);
        }

        [XmlIgnore, Browsable(false)]
        public TableCollection parent;

        private string name;
        /// <summary>
        /// Tablonun adı
        /// </summary>
        [Description("Name of the table"), Category("Definition")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Hangi veritabanına ait?
        /// </summary>
        [XmlIgnore, Browsable(false)]
        public IDatabase Database
        {
            get { return this.parent.db; }
        }
	
        private ColumnCollection columns;
        /// <summary>
        /// Tablonun columnlarını listeler
        /// </summary>
        [Browsable(false)]
        public ColumnCollection Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        /// <summary>
        /// Bu tablonun (varsa) tek alan üzerinde tanımlanmış Primary Index alanı.
        /// </summary>
        [XmlIgnore, Description("Primary column if exists"), Category("Extra Info")]
        public Column PrimaryColumn
        {
            get
            {
                if(this.Constraints!=null)
                    foreach(Constraint con in this.Constraints)
                        if(con is PrimaryKeyConstraint && con.ColumnNames.Count==1)
                            return this.Columns[con.ColumnNames[0]];

                #region mysql'e özgü bir saçmalık nedeniyle
                if (this.Indices!=null)
                    foreach (Index ind in this.Indices)
                        if (ind.Name == "PRIMARY")
                            return this.Columns[ind.ColumnNames[0]];
                #endregion

                return null;
            }
        }

        [XmlIgnore, Browsable(false)]
        public TableTypes DiscoveredTableType
        {
            get
            {
                return this.ReferencedByTables.Count > ReferenceTables.Count ? TableTypes.Account : TableTypes.Transaction;
            }
        }

        private string stringColumnName;
        [Description("Name of the string representation column"), Category("Extra Info")]
        public string StringColumnName
        {
            get { return stringColumnName; }
            set { stringColumnName = value; }
        }

        /// <summary>
        /// Bu tablonun sahip olduğu string tipindeki ilk alan
        /// Human readable birşeyler lazım olduğunda kullanılabilir.
        /// </summary>
        [XmlIgnore, Browsable(false)]
        public Column StringColumn
        {
            get
            {
                if (String.IsNullOrEmpty(stringColumnName))
                {
                    Column res = this.Columns.Find(DbType.VarChar);
                    if (res == null) res = this.Columns.Find(DbType.Char);
                    if (res == null) res = this.Columns.Find(DbType.NVarChar);
                    if (res == null) res = this.Columns.Find(DbType.NChar);
                    return res;
                }
                else
                    return this.Columns[stringColumnName];
            }
        }

        //private TableCollection referenceTables;
        /// <summary>
        /// Bu tablonun columnlarının bağımlı olduğu tabloların listesi.
        /// Dolayısıyla bu tabloya ait bir kayıt, bu özellik tarafından listelenen tablolardaki ilişkili kayıtların child'ı olmuş olur.
        /// </summary>
        [XmlIgnore, Browsable(false)]
        public TableCollection ReferenceTables
        {
            get
            {
                //if (referenceTables == null)
                //{
                    TableCollection referenceTables = new TableCollection(this.parent.db);
                    foreach (ForeignKeyConstraint fk in this.Constraints.Where(c=>c is ForeignKeyConstraint))
                        referenceTables.Add(fk.Table);
                //}
                return referenceTables;
            }
        }

        //private TableCollection referencedByTables;
        /// <summary>
        /// Bu tabloya bağımlı başka tablolardaki columnlar.
        /// </summary>
        [XmlIgnore, Browsable(false)]
        public TableCollection ReferencedByTables
        {
            get
            {
                //if (referencedByTables == null)
                //{
                    TableCollection referencedByTables = new TableCollection(this.Database);
                    foreach (Table tbl in this.Database.Tables)
                        foreach (ForeignKeyConstraint fk in tbl.Constraints.Where(c => c is ForeignKeyConstraint))
                            if (fk.RefTableName == this.Name)
                                referencedByTables.Add(fk.Table);
                //}
                return referencedByTables;
            }
        }


        private IndexCollection indices;
        /// <summary>
        /// Tablonun indexlerini listeler
        /// </summary>
        [Browsable(false)]
        public IndexCollection Indices
        {
            get { return indices; }
            set { indices = value; }
        }

        private ConstraintCollection constraints;
        /// <summary>
        /// Tablonun constraintlerini listeler
        /// </summary>
        [Browsable(false)]
        public ConstraintCollection Constraints
        {
            get { return constraints; }
            set { constraints = value; }
        }



        private bool isView;
        /// <summary>
        /// Yoksa bu bir View mi? View'ler tablolara çok benzedikleri için ayrıca bir View sınıfı tanımlamadık.
        /// Bu alan size elinizdeki tablonun view mi, yoksa table mı olduğunu söyleyecektir.
        /// </summary>
        [Description("Is just a view?"), Category("Definition"), ReadOnly(true)]
        public bool IsView
        {
            get { return isView; }
            set { isView = value; }
        }

        public override string ToString()
        {
            return this.Name;
        }
        /// <summary>
        /// Bu tabloya oluşturmak için yazılması gereken CREATE TABLE sorgusunu döndürür.
        /// </summary>
        public string ToDDL()
        {
            if (this.parent == null)
                throw new Exception("Bu tablo tanımı bir veritabanına ait değil. dbInstance.GetDataTable(table) şeklinde kullanın.");
            return this.parent.db.GetTableDDL(this);
        }

        public string Dump(DatabaseProvider dbProvider)
        {
            string delimitL = "[", delimitR = "]";
            switch (dbProvider)
            {
                case DatabaseProvider.PostgreSQL:
                    delimitL = "\""; delimitR = "\"";
                    break;
                case DatabaseProvider.MySQL:
                    delimitL = "`"; delimitR = "`";
                    break;
                case DatabaseProvider.SQLServer:
                case DatabaseProvider.SQLite:
                    delimitL = "["; delimitR = "]";
                    break;
                default:
                    break;
            }

            string columns = delimitL + String.Join(delimitR + ", " + delimitL, this.Columns.ToStringArray()) + delimitR;
            string sql = String.Format("insert into {2}{0}{3} ({1}) values ({{0}});", this.Name, columns, delimitL, delimitR);

            StringBuilder sb = new StringBuilder();
            DataTable dt = this.Database.GetDataTable("select * from [" + this.Name + "]");
            foreach (DataRow dr in dt.Rows)
            {
                string[] values = new string[this.Columns.Count];
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    string columnName = this.Columns[i].Name;
                    if (dt.Columns[i].DataType == typeof(bool))
                        values[i] = dr.IsNull(columnName) ? "null" : "'" + (dr[this.Columns[i].Name].Equals(true) ? 1 : 0) + "'";
                    else if (dt.Columns[i].DataType == typeof(DateTime))
                        values[i] = dr.IsNull(columnName) ? "null" : "'" + ((DateTime)dr[this.Columns[i].Name]).ToString("yyyy-MM-dd HH:mm") + "'";
                    else
                        values[i] = dr.IsNull(columnName) ? "null" : "'" + dr[this.Columns[i].Name].ToString().Replace("'", "''").Replace("\\", "\\\\").Replace("\n", "\\n").Replace("\r", "\\r") + "'";
                }
                sb.AppendFormat(sql, String.Join(", ", values));
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public Table CloneForDatabase(IDatabase dbDst, string tableName)
        {
            Table newTable = new Table();
            newTable.parent = dbDst.Tables;
            foreach (Column f in this.Columns)
            {
                Column newColumn = new Column();
                //newColumn.DefaultValue = f.DefaultValue;
                newColumn.ColumnType = f.ColumnType;
                if (f.ColumnType == DbType.Timestamp && dbDst.Provider == DatabaseProvider.SQLServer)
                    newColumn.ColumnType = DbType.DateTime;
                newColumn.IsAutoIncrement = f.IsAutoIncrement;
                newColumn.IsNullable = f.IsNullable;
                newColumn.Length = f.Length <= 0 ? 1000 : f.Length;
                newColumn.Name = f.Name;
                newColumn.parent = newTable.Columns;
                newTable.Columns.Add(newColumn);
            }
            foreach (Index k in this.Indices)
            {
                Index newIndex = new Index();
                newIndex.Name = k.Name;
                if (k.Name == "PRIMARY")
                    newIndex.Name = "PK_" + tableName;
                newIndex.ColumnNames = new List<string>();
                foreach (Column fk in k.Columns)
                    newIndex.ColumnNames.Add(fk.Name);
                newTable.Indices.Add(newIndex);
            }
            newTable.IsView = this.IsView;
            newTable.Name = tableName;

            foreach (Constraint c in this.Constraints)
            {
                if (c is PrimaryKeyConstraint) {
                    PrimaryKeyConstraint p = new PrimaryKeyConstraint();
                    p.Name = c.Name;
                    p.ColumnNames = c.ColumnNames;
                    newTable.Constraints.Add(p);
                }
            }

            return newTable;
        }

        public bool HasAutoIncrementColumn()
        {
            foreach (Column f in this.Columns)
                if (f.IsAutoIncrement)
                    return true;
            return false;
        }

        public Column FindColumnWhichRefersTo(Table tblSrc)
        {
            foreach (Column f in this.Columns)
                if (f.ReferenceColumn != null && f.ReferenceColumn.Table == tblSrc)
                    return f;
            return null;
        }

        public void GenerateUIMetadata()
        {
            if (UIMetadata != null)
                return;

            UIMetadata = new TableUIMetadata();
            UIMetadata.DisplayName = Name;
            UIMetadata.DisplayOrder = parent.IndexOf(this);
            UIMetadata.ShortDisplayName = Name;
            UIMetadata.TableType = this.ReferencedByTables.Count > 0 ? TableTypes.Account : TableTypes.Transaction;
            UIMetadata.ModuleName = this.Database.Name;
            UIMetadata.ShowInMainMenu = UIMetadata.TableType == TableTypes.Account;
            UIMetadata.DefaultSortColumn = this.StringColumn == null ? null : this.StringColumn.Name;
            UIMetadata.DefaultSortType = SortTypes.Ascending;
            UIMetadata.ShowDetailGrids = "";
            for (int i = 0; i < this.ReferencedByTables.Count; i++)
                UIMetadata.ShowDetailGrids += this.ReferencedByTables[i].Name + ",";
            UIMetadata.ShowDetailGrids = UIMetadata.ShowDetailGrids.Trim(',');

            foreach (Column f in this.Columns)
                f.GenerateUIMetadata();
        }

        [Description("The definitions for this table to be used generating of UI code"), Category("Extra Info")]
        public TableUIMetadata UIMetadata { get; set; }
    }

    [Serializable]
    public class TableCollection : List<Table>
    {
        [XmlIgnore, Browsable(false)]
        public IDatabase db;

        public TableCollection()
        {
        }
        public TableCollection(IDatabase db)
        {
            this.db = db;
        }

        public new int Add(Table table)
        {
            table.parent = this;
            base.Add(table);
            return base.Count;
        }
        public Table this[string name]
        {
            get
            {
                foreach(Table tbl in this)
                    if(tbl.Name.ToLowerInvariant() == name.ToLowerInvariant())
                        return tbl;
                return null;
            }
        }


        public override string ToString()
        {
            return db.Name + " Columns";
        }
    }

    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class TableUIMetadata
    {
        public string DisplayName { get; set; }
        public string ShortDisplayName { get; set; }
        public int DisplayOrder { get; set; }
        public TableTypes TableType { get; set; }
        public string ModuleName { get; set; }
        public bool ShowInMainMenu { get; set; }
        public string DefaultSortColumn { get; set; }
        public SortTypes DefaultSortType { get; set; }
        public string ShowDetailGrids { get; set; }
    }

    public enum SortTypes
    {
        Ascending,
        Descending
    }
}
