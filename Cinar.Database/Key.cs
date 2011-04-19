using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml.Serialization;

namespace Cinar.Database
{
    [Serializable]
    public class Key
    {
        internal KeyCollection parent;

        private string name;
        /// <summary>
        /// Key adı.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Key ile ilgili fieldlar
        /// </summary>
        [XmlIgnore]
        public FieldCollection Fields
        {
            get
            {
                FieldCollection fields = new FieldCollection(parent.table);
                foreach (string strField in fieldNames)
                    fields.Add(parent.table.Fields[strField]);
                return fields;
            }
            //set { fields = value; }
        }

        private List<string> fieldNames;
        /// <summary>
        /// Key ile ilgili fieldlar
        /// </summary>
        public List<string> FieldNames
        {
            get { return fieldNames; }
            set { fieldNames = value; }
        }

        private bool isPrimary;
        /// <summary>
        /// Primary Key midir?
        /// </summary>
        public bool IsPrimary
        {
            get { return isPrimary; }
            set { isPrimary = value; }
        }

        private bool isUnique;
        /// <summary>
        /// Unique key midir? (Primary Key ise unique olmak zorundadır zaten)
        /// </summary>
        public bool IsUnique
        {
            get { return isUnique; }
            set { isUnique = value; }
        }

        public Key() {
            this.fieldNames = new List<string>();
        }
    }

    [Serializable]
    public class KeyCollection : List<Key>
    {
        internal Table table;
        public KeyCollection()
        {
        }
        public KeyCollection(Table table)
        {
            this.table = table;
        }
        public new int Add(Key key)
        {
            key.parent = this;
            base.Add(key);
            return base.Count;
        }
        public Key this[string name]
        {
            get
            {
                foreach (Key key in this)
                    if (key.Name == name)
                        return key;
                return null;
            }
        }
        //public Field Find(DbType fieldType)
        //{
        //    foreach (Field fld in this)
        //        if (fld.FieldType == fieldType)
        //            return fld;
        //    return null;
        //}

        public string[] ToStringArray()
        {
            string[] res = new string[this.Count];
            for (int i = 0; i < this.Count; i++)
                res[i] = this[i].Name;
            return res;
        }
    }

}
