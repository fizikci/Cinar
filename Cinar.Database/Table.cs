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
    public class Table
    {
        internal TableCollection parent;

        private string name;
        /// <summary>
        /// Tablonun adı
        /// </summary>
        [Description("Name of the table"), Category("DDL")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Hangi veritabanına ait?
        /// </summary>
        [XmlIgnore, Browsable(false)]
        public Database Database
        {
            get { return this.parent.db; }
        }
	
        private FieldCollection fields;
        /// <summary>
        /// Tablonun fieldlarını listeler
        /// </summary>
        [Browsable(false)]
        public FieldCollection Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        /// <summary>
        /// Bu tablonun (varsa) tek alan üzerinde tanımlanmış Primary Index alanı.
        /// </summary>
        [XmlIgnore, Description("Primary field if exists"), Category("Extra Info")]
        public Field PrimaryField
        {
            get
            {
                if(this.Indices!=null)
                    foreach(Index index in this.Indices)
                        if(index.IsPrimary && index.FieldNames.Count==1)
                            return this.Fields[index.FieldNames[0]];
                return null;
            }
        }

        [XmlIgnore, Browsable(false)]
        public TableTypes DiscoveredTableType
        {
            get
            {
                return this.ReferencedByTables.Count > 0 ? TableTypes.Account : TableTypes.Transaction;
            }
        }

        private string stringFieldName;
        [Description("Name of the string representation field"), Category("Extra Info")]
        public string StringFieldName
        {
            get { return stringFieldName; }
            set { stringFieldName = value; }
        }

        /// <summary>
        /// Bu tablonun sahip olduğu string tipindeki ilk alan
        /// Human readable birşeyler lazım olduğunda kullanılabilir.
        /// </summary>
        [XmlIgnore, Browsable(false)]
        public Field StringField
        {
            get
            {
                if (String.IsNullOrEmpty(stringFieldName))
                {
                    Field res = this.Fields.Find(DbType.VarChar);
                    if (res == null) res = this.Fields.Find(DbType.Char);
                    if (res == null) res = this.Fields.Find(DbType.NVarChar);
                    if (res == null) res = this.Fields.Find(DbType.NChar);
                    return res;
                }
                else
                    return this.Fields[stringFieldName];
            }
        }

        private TableCollection referenceTables;
        /// <summary>
        /// Bu tablonun fieldlarının bağımlı olduğu tabloların listesi.
        /// Dolayısıyla bu tabloya ait bir kayıt, bu özellik tarafından listelenen tablolardaki ilişkili kayıtların child'ı olmuş olur.
        /// </summary>
        [XmlIgnore, Browsable(false)]
        public TableCollection ReferenceTables
        {
            get
            {
                if (referenceTables == null)
                {
                    referenceTables = new TableCollection(this.parent.db);
                    foreach (Field f in this.Fields)
                        if (f.ReferenceField != null)
                            referenceTables.Add(f.ReferenceField.Table);
                }
                return referenceTables;
            }
        }

        private TableCollection referencedByTables;
        /// <summary>
        /// Bu tabloya bağımlı başka tablolardaki fieldlar.
        /// </summary>
        [XmlIgnore, Browsable(false)]
        public TableCollection ReferencedByTables
        {
            get
            {
                if (referencedByTables == null)
                {
                    referencedByTables = new TableCollection(this.Database);
                    foreach (Table tbl in this.Database.Tables)
                        foreach (Field f in tbl.Fields)
                            if (f.ReferenceField!=null && f.ReferenceField == this.PrimaryField)
                                referencedByTables.Add(f.Table);
                }
                return referencedByTables;
            }
        }


        private IndexCollection indices;
        /// <summary>
        /// Tablonun keylerini listeler
        /// </summary>
        [Browsable(false)]
        public IndexCollection Indices
        {
            get { return indices; }
            set { indices = value; }
        }



        private bool isView;
        /// <summary>
        /// Yoksa bu bir View mi? View'ler tablolara çok benzedikleri için ayrıca bir View sınıfı tanımlamadık.
        /// Bu alan size elinizdeki tablonun view mi, yoksa table mı olduğunu söyleyecektir.
        /// </summary>
        [Description("Is just a view?"), Category("DDL")]
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
                    delimitL = "["; delimitR = "]";
                    break;
                default:
                    break;
            }

            string fields = delimitL + String.Join(delimitR + ", " + delimitL, this.Fields.ToStringArray()) + delimitR;
            string sql = String.Format("insert into {2}{0}{3} ({1}) values ({{0}});", this.Name, fields, delimitL, delimitR);

            StringBuilder sb = new StringBuilder();
            DataTable dt = this.Database.GetDataTable("select * from [" + this.Name + "]");
            foreach (DataRow dr in dt.Rows)
            {
                string[] values = new string[this.Fields.Count];
                for (int i = 0; i < this.Fields.Count; i++)
                {
                    string fieldName = this.Fields[i].Name;
                    if (dt.Columns[i].DataType == typeof(bool))
                        values[i] = dr.IsNull(fieldName) ? "null" : "'" + (dr[this.Fields[i].Name].Equals(true) ? 1 : 0) + "'";
                    else if (dt.Columns[i].DataType == typeof(DateTime))
                        values[i] = dr.IsNull(fieldName) ? "null" : "'" + ((DateTime)dr[this.Fields[i].Name]).ToString("yyyy-MM-dd HH:mm") + "'";
                    else
                        values[i] = dr.IsNull(fieldName) ? "null" : "'" + dr[this.Fields[i].Name].ToString().Replace("'", "''").Replace("\n", "\\n").Replace("\r", "\\r") + "'";
                }
                sb.AppendFormat(sql, String.Join(", ", values));
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public Table CloneForDatabase(Database dbDst, string tableName)
        {
            Table newTable = new Table();
            newTable.parent = dbDst.Tables;
            newTable.Fields = new FieldCollection(newTable);
            foreach (Field f in this.Fields)
            {
                Field newField = new Field();
                //newField.DefaultValue = f.DefaultValue;
                newField.FieldType = f.FieldType;
                if (f.FieldType == DbType.Timestamp && dbDst.Provider == DatabaseProvider.SQLServer)
                    newField.FieldType = DbType.DateTime;
                newField.IsAutoIncrement = f.IsAutoIncrement;
                newField.IsNullable = f.IsNullable;
                newField.Length = f.Length <= 0 ? 1000 : f.Length;
                newField.Name = f.Name;
                newField.parent = newTable.Fields;
                newTable.Fields.Add(newField);
            }
            newTable.Indices = new IndexCollection(newTable);
            foreach (Index k in this.Indices)
            {
                Index newIndex = new Index();
                newIndex.Name = k.Name;
                if (k.Name == "PRIMARY")
                    newIndex.Name = "PK_" + tableName;
                newIndex.IsUnique = k.IsUnique;
                newIndex.IsPrimary = k.IsPrimary;
                newIndex.FieldNames = new List<string>();
                foreach (Field fk in k.Fields)
                    newIndex.FieldNames.Add(fk.Name);
                newTable.Indices.Add(newIndex);
            }
            newTable.IsView = this.IsView;
            newTable.Name = tableName;

            return newTable;
        }

        public bool HasAutoIncrementField()
        {
            foreach (Field f in this.Fields)
                if (f.IsAutoIncrement)
                    return true;
            return false;
        }

        public Field FindFieldWhichRefersTo(Table tblSrc)
        {
            foreach (Field f in this.Fields)
                if (f.ReferenceField != null && f.ReferenceField.Table == tblSrc)
                    return f;
            return null;
        }

        public void GenerateUIMetadata()
        {
            UIMetadata = new TableUIMetadata();
            UIMetadata.DisplayName = Name;
            UIMetadata.DisplayOrder = parent.IndexOf(this);
            UIMetadata.ShortDisplayName = Name;
            UIMetadata.TableType = this.ReferencedByTables.Count > 0 ? TableTypes.Account : TableTypes.Transaction;
            UIMetadata.ModuleName = "";
            UIMetadata.ShowInMainMenu = UIMetadata.TableType == TableTypes.Account;
            UIMetadata.DefaultSortField = this.StringField == null ? null : this.StringField.Name;
            UIMetadata.DefaultSortType = SortTypes.Ascending;
            UIMetadata.ShowDetailGrids = "";
            for (int i = 0; i < this.ReferencedByTables.Count; i++)
                UIMetadata.ShowDetailGrids += this.ReferencedByTables[i].Name + ",";
            UIMetadata.ShowDetailGrids = UIMetadata.ShowDetailGrids.Trim(',');

            foreach (Field f in this.Fields)
                f.GenerateUIMetadata();
        }

        public TableUIMetadata UIMetadata { get; set; }
    }

    [Serializable]
    public class TableCollection : List<Table>
    {
        internal Database db;

        public TableCollection()
        {
        }
        public TableCollection(Database db)
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

        //public override string ToString()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach (Table table in this) sb.Append(table.Name + " ");
        //    return sb.ToString();
        //}
        //public string ToDDL()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach (Table tbl in this)
        //        sb.Append(tbl.ToDDL() + Environment.NewLine);
        //    return sb.ToString();
        //}
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
        public string DefaultSortField { get; set; }
        public SortTypes DefaultSortType { get; set; }
        public string ShowDetailGrids { get; set; }
    }

    public enum SortTypes
    {
        Ascending,
        Descending
    }
}
