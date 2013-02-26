using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Cinar.Database
{
    [Serializable]
    public class Index : BaseIndexConstraint
    {
        [XmlIgnore, Browsable(false)]
        public IndexCollection parent;

        [Browsable(false)]
        public override Table Table { get { return parent.Table; } }

        public Index() {
            this.ColumnNames = new List<string>();
        }
    }

    public abstract class BaseIndexConstraint : Attribute, IMetadata
    {
        public abstract Table Table { get; }

        /// <summary>
        /// Index adı.
        /// </summary>
        [Category("Base"), ReadOnly(true)]
        public string Name { get; set; }

        /// <summary>
        /// Index ile ilgili columnlar
        /// </summary>
        [XmlIgnore, Browsable(false)]
        public ColumnCollection Columns
        {
            get
            {
                ColumnCollection columns = new ColumnCollection(Table);
                foreach (string strColumn in ColumnNames)
                    columns.Add(Table.Columns[strColumn]);
                return columns;
            }
            //set { columns = value; }
        }

        /// <summary>
        /// Index ile ilgili columnlar
        /// </summary>
        [Category("Base"), ReadOnly(true)]
        public List<string> ColumnNames { get; set; }

        public string ConstraintColumnNames 
        {
            get { return string.Join(",", this.ColumnNames); }
            set { this.ColumnNames = value.Split(',').Select(s=>s.Trim()).ToList(); }
        }

        public override string ToString()
        {
            return Table.Name + "." + this.Name + " (" + string.Join(", ", this.ColumnNames.ToArray()) + ")";
        }
    }

    [Serializable]
    public class IndexCollection : List<Index>
    {
        [XmlIgnore, Browsable(false)]
        public Table table;
        public Table Table { get { return table; } }

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
            foreach (string colName in index.ColumnNames)
                if (index.parent.Table.Columns[colName] == null)
                    Debug.WriteLine(string.Format("Index için kullanılan {0} alanı {1} tablosuna ait değil.", colName, index.parent.Table));
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

        public string[] ToStringArray()
        {
            string[] res = new string[this.Count];
            for (int i = 0; i < this.Count; i++)
                res[i] = this[i].Name;
            return res;
        }

        public override string ToString()
        {
            return table + " Indexes";
        }

    }

}
