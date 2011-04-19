using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml.Serialization;

namespace Cinar.Database
{
    [Serializable]
    public class Index
    {
        internal IndexCollection parent;

        private string name;
        /// <summary>
        /// Index adı.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Index ile ilgili fieldlar
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
        /// Index ile ilgili fieldlar
        /// </summary>
        public List<string> FieldNames
        {
            get { return fieldNames; }
            set { fieldNames = value; }
        }

        private bool isPrimary;
        /// <summary>
        /// Primary Index midir?
        /// </summary>
        public bool IsPrimary
        {
            get { return isPrimary; }
            set { isPrimary = value; }
        }

        private bool isUnique;
        /// <summary>
        /// Unique Index midir? (Primary Index ise unique olmak zorundadır zaten)
        /// </summary>
        public bool IsUnique
        {
            get { return isUnique; }
            set { isUnique = value; }
        }

        public Index() {
            this.fieldNames = new List<string>();
        }

        public string ToDDL()
        {
            if (parent.table.Database.Provider == DatabaseProvider.SQLServer)
            {
                if (IsPrimary)
                    return "ALTER TABLE [" + parent.table.Name + "] WITH NOCHECK ADD CONSTRAINT " + Name + " PRIMARY KEY CLUSTERED (" + string.Join(", ", FieldNames.ToArray()) + ")";
                else if (IsUnique)
                    return "ALTER TABLE [" + parent.table.Name + "] WITH NOCHECK ADD CONSTRAINT " + Name + " UNIQUE (" + string.Join(", ", FieldNames.ToArray()) + ")";
            }

            return "CREATE " + (IsPrimary ? "PRIMARY KEY" : (IsUnique ? "UNIQUE" : "")) + " INDEX " + Name + " ON " + parent.table.Name + " (" + string.Join(", ", FieldNames.ToArray()) + ")";
        }
    }

    [Serializable]
    public class IndexCollection : List<Index>
    {
        internal Table table;
        public IndexCollection()
        {
        }
        public IndexCollection(Table table)
        {
            this.table = table;
        }
        public new int Add(Index index)
        {
            index.parent = this;
            base.Add(index);
            return base.Count;
        }
        public Index this[string name]
        {
            get
            {
                foreach (Index index in this)
                    if (index.Name == name)
                        return index;
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
