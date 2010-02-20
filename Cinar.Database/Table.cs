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
using System.Collections;
using System.Data;
using System.Xml.Serialization;

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
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Hangi veritabanına ait?
        /// </summary>
        [XmlIgnore]
        public Database Database
        {
            get { return this.parent.db; }
        }
	
        private FieldCollection fields;
        /// <summary>
        /// Tablonun fieldlarını listeler
        /// </summary>
        public FieldCollection Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        /// <summary>
        /// Bu tablonun (varsa) tek alan üzerinde tanımlanmış Primary Key alanı.
        /// </summary>
        [XmlIgnore]
        public Field PrimaryField
        {
            get
            {
                foreach(Key key in this.Keys)
                    if(key.IsPrimary && key.FieldNames.Count==1)
                        return this.Fields[key.FieldNames[0]];
                return null;
            }
        }

        private string stringFieldName;
        public string StringFieldName
        {
            get { return stringFieldName; }
            set { stringFieldName = value; }
        }

        /// <summary>
        /// Bu tablonun sahip olduğu string tipindeki ilk alan
        /// Human readable birşeyler lazım olduğunda kullanılabilir.
        /// </summary>
        [XmlIgnore]
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
        [XmlIgnore]
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
        [XmlIgnore]
        public TableCollection ReferencedByTables
        {
            get
            {
                if (referencedByTables == null)
                {
                    referencedByTables = new TableCollection(this.Database);
                    foreach (Table tbl in this.Database.Tables)
                        foreach (Field f in tbl.Fields)
                            if (f.ReferenceField == this.PrimaryField)
                                referencedByTables.Add(f.Table);
                }
                return referencedByTables;
            }
        }


        private KeyCollection keys;
        /// <summary>
        /// Tablonun keylerini listeler
        /// </summary>
        public KeyCollection Keys
        {
            get { return keys; }
            set { keys = value; }
        }



        private bool isView;
        /// <summary>
        /// Yoksa bu bir View mi? View'ler tablolara çok benzedikleri için ayrıca bir View sınıfı tanımlamadık.
        /// Bu alan size elinizdeki tablonun view mi, yoksa table mı olduğunu söyleyecektir.
        /// </summary>
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

        public Table CloneForDatabase(Database dbDst, string tableName)
        {
            Table newTable = new Table();
            newTable.parent = dbDst.Tables;
            newTable.Fields = new FieldCollection(newTable);
            foreach (Field f in this.Fields)
            {
                Field newField = new Field();
                newField.DefaultValue = f.DefaultValue;
                newField.FieldType = f.FieldType;
                if (f.FieldType == DbType.Timestamp && dbDst.Provider == DatabaseProvider.SQLServer)
                    newField.FieldType = DbType.DateTime;
                newField.IsAutoIncrement = f.IsAutoIncrement;
                newField.IsNullable = f.IsNullable;
                newField.Length = f.Length;
                newField.Name = f.Name;
                newField.parent = newTable.Fields;
                newTable.Fields.Add(newField);
            }
            newTable.Keys = new KeyCollection(newTable);
            foreach (Key k in this.Keys)
            {
                Key newKey = new Key();
                newKey.Name = k.Name;
                if (k.Name == "PRIMARY")
                    newKey.Name = "PK_" + tableName;
                newKey.IsUnique = k.IsUnique;
                newKey.IsPrimary = k.IsPrimary;
                newKey.FieldNames = new List<string>();
                foreach (Field fk in k.Fields)
                    newKey.FieldNames.Add(fk.Name);
                newTable.Keys.Add(newKey);
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

}
