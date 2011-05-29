using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Collections;
using System.Xml.Serialization;

namespace Cinar.Database
{
    [Serializable]
    [XmlInclude(typeof(PrimaryKeyConstraint))]
    [XmlInclude(typeof(UniqueConstraint))]
    [XmlInclude(typeof(ForeignKeyConstraint))]
    [XmlInclude(typeof(CheckConstraint))]
    public class Constraint : BaseIndexConstraint
    {
        public ConstraintCollection parent;
        [Browsable(false)]
        public override Table Table { get { return parent.Table; } }

        public Constraint() {
            this.ColumnNames = new List<string>();
        }
    }

    [Serializable]
    public class PrimaryKeyConstraint : Constraint
    {

    }

    [Serializable]
    public class UniqueConstraint : Constraint
    {

    }

    [Serializable]
    public class ForeignKeyConstraint : Constraint
    {
        [Category("Ref"), ReadOnly(true)]
        public string RefTableName { get; set; }
        [Category("Ref"), ReadOnly(true)]
        public string RefConstraintName { get; set; }
        [Category("Rules"), ReadOnly(true)]
        public string UpdateRule { get; set; }
        [Category("Rules"), ReadOnly(true)]
        public string DeleteRule { get; set; }
    }

    [Serializable]
    public class CheckConstraint : Constraint
    {
        [Category("Check")]
        public string Expression { get; set; }
    }


    [Serializable]
    public class ConstraintCollection : List<Constraint>
    {
        public Table table;
        public Table Table { get { return table; } }

        public ConstraintCollection()
        {
        }
        public ConstraintCollection(Table table)
        {
            this.table = table;
        }
        public new int Add(Constraint index)
        {
            index.parent = this;
            base.Add(index);
            return base.Count;
        }
        public Constraint this[string name]
        {
            get
            {
                foreach (Constraint index in this)
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
            return table + " Constraints";
        }
    }

}
