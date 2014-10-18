using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Cinar.Database
{
    public enum ConstraintTypes
    {
        None,
        PrimaryKey,
        Unique,
        ForeignKey,
        Check
    }

    [Serializable]
    [XmlInclude(typeof(PrimaryKeyConstraint))]
    [XmlInclude(typeof(UniqueConstraint))]
    [XmlInclude(typeof(ForeignKeyConstraint))]
    [XmlInclude(typeof(CheckConstraint))]
    public class Constraint : BaseIndexConstraint
    {
        [XmlIgnore, Browsable(false)]
        public ConstraintCollection parent;
        [Browsable(false)]
        public override Table Table { get { return parent.Table; } }

        public ConstraintTypes Type { get; set; }

        public Constraint() {
            this.ColumnNames = new List<string>();
        }
    }

    [Serializable]
    public class PrimaryKeyConstraint : Constraint
    {
        public PrimaryKeyConstraint() : base()
        {
            this.Type = ConstraintTypes.PrimaryKey;
        }
    }

    [Serializable]
    public class UniqueConstraint : Constraint
    {
        public UniqueConstraint() : base()
        {
            this.Type = ConstraintTypes.Unique;
        }

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

        public ForeignKeyConstraint() : base()
        {
            this.Type = ConstraintTypes.ForeignKey;
        }
}

    [Serializable]
    public class CheckConstraint : Constraint
    {
        [Category("Check")]
        public string Expression { get; set; }

        public CheckConstraint() : base()
        {
            this.Type = ConstraintTypes.Check;
        }    
    }


    [Serializable]
    public class ConstraintCollection : List<Constraint>
    {
        [XmlIgnore, Browsable(false)]
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
            foreach (string colName in index.ColumnNames)
                if (index.parent.Table.Columns[colName] == null)
                    Debug.WriteLine(string.Format("{2} için kullanılan {0} alanı {1} tablosuna ait değil.", colName, index.parent.Table, index.GetType().Name));
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
