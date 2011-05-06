using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Database;
using UniqueConstraint = Cinar.Database.UniqueConstraint;

namespace Cinar.DBTools.Tools
{
    public partial class FormCreateIndex : Form
    {
        Table table;

        public FormCreateIndex(Table table)
        {
            this.table = table;

            InitializeComponent();

            List<KeyField> list = new List<KeyField>();
            foreach (Field f in table.Fields)
            {
                list.Add(new KeyField()
                {
                    FieldType = string.IsNullOrEmpty(f.FieldTypeOriginal) ? Provider.Database.DbTypeToString(f.FieldType) : f.FieldTypeOriginal,
                    Length = (int)f.Length,
                    Name = f.Name
                });
            }
            keyFieldBindingSource.DataSource = list;
        }
        public BaseIndexConstraint GetCreatedKey()
        {
            BaseIndexConstraint index = null;

            if (rbPrimaryKey.Checked)
                index = new PrimaryKeyConstraint();
            else if (rbUnique.Checked)
                index = new UniqueConstraint();
            else
                index = new Index();

            index.FieldNames = new List<string>();
            index.Name = txtKeyName.Text.MakeFileName();
            foreach (KeyField fd in keyFieldBindingSource.DataSource as List<KeyField>)
                if(fd.Selected)
                    index.FieldNames.Add(fd.Name);
            return index;
        }

        public void SetKey(BaseIndexConstraint index)
        {
            txtKeyName.Text = index.Name;
            rbPrimaryKey.Checked = index is PrimaryKeyConstraint;
            rbUnique.Checked = index is PrimaryKeyConstraint || index is UniqueConstraint;
            List<KeyField> fields = new List<KeyField>();
            foreach (Field f in table.Fields)
            {
                fields.Add(new KeyField()
                {
                    Selected = index.FieldNames.Contains(f.Name),
                    FieldType = string.IsNullOrEmpty(f.FieldTypeOriginal) ? Provider.Database.DbTypeToString(f.FieldType) : f.FieldTypeOriginal,
                    Length = (int)f.Length,
                    Name = f.Name
                });
            }
            keyFieldBindingSource.DataSource = fields;
        }
    }
    public class KeyField
    {
        public bool Selected { get; set; }
        public string Name { get; set; }
        public string FieldType { get; set; }
        public int Length { get; set; }
    }
    public class KeyDef
    {
        public List<KeyField> Fields { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsUnique { get; set; }

        public KeyDef()
        {
            Fields = new List<KeyField>();
        }
    }

}
