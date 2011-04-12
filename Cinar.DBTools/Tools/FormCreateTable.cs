using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Database;

namespace Cinar.DBTools.Tools
{
    public partial class FormCreateTable : Form
    {
        public FormCreateTable()
        {
            InitializeComponent();

            foreach (object o in Enum.GetNames(typeof(Cinar.Database.DbType)))
                colFieldType.Items.Add(o);

            fieldCollectionBindingSource.DataSource = new List<FieldDef>();
        }

        public Table GetTable()
        {
            Table tbl = new Table();
            tbl.Fields = new FieldCollection(tbl);
            tbl.Keys = new KeyCollection(tbl);
            tbl.Name = txtTableName.Text.MakeFileName();
            if (!(fieldCollectionBindingSource.DataSource is Type))
                foreach (FieldDef fd in fieldCollectionBindingSource.DataSource as List<FieldDef>)
                {
                    Field f = new Field()
                    {
                        Name = fd.Name,
                        FieldType = (Cinar.Database.DbType)Enum.Parse(typeof(Cinar.Database.DbType), fd.FieldType),
                        Length = fd.Length,
                        DefaultValue = fd.DefaultValue,
                        IsNullable = fd.IsNullable,
                        IsAutoIncrement = fd.IsAutoIncrement
                    };
                    tbl.Fields.Add(f);
                    if (fd.IsPrimaryKey)
                    {
                        Key k = new Key();
                        k.Fields.Add(f);
                        k.IsPrimary = true;
                        k.IsUnique = true;
                        k.Name = "PK_" + tbl.Name;
                        tbl.Keys.Add(k);
                    }
                }
            return tbl;
        }
    }

    public class FieldDef
    {
        public string Name { get; set; }
        public string FieldType { get; set; }
        public int Length { get; set; }
        public string DefaultValue { get; set; }
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsAutoIncrement { get; set; }
    }
}
