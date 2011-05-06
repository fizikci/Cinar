using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Collections;
using System.Xml.Serialization;

namespace Cinar.Database
{
    [Serializable]
    public class Index : BaseIndexConstraint
    {
        internal IndexCollection parent;

        [Browsable(false)]
        public override Table Table { get { return parent.Table; } }

        public Index() {
            this.FieldNames = new List<string>();
        }
    }

    public abstract class BaseIndexConstraint
    {
        public abstract Table Table { get; }

        /// <summary>
        /// Index adı.
        /// </summary>
        [Category("Base"), ReadOnly(true)]
        public string Name { get; set; }

        /// <summary>
        /// Index ile ilgili fieldlar
        /// </summary>
        [XmlIgnore, Browsable(false)]
        public FieldCollection Fields
        {
            get
            {
                FieldCollection fields = new FieldCollection(Table);
                foreach (string strField in FieldNames)
                    fields.Add(Table.Fields[strField]);
                return fields;
            }
            //set { fields = value; }
        }

        /// <summary>
        /// Index ile ilgili fieldlar
        /// </summary>
        [Category("Base"), ReadOnly(true)]
        public List<string> FieldNames { get; set; }
    }

    [Serializable]
    public class IndexCollection : List<Index>
    {
        internal Table table;
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
    }

}
