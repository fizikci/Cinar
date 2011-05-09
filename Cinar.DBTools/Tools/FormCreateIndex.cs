using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Database;
using Constraint = Cinar.Database.Constraint;
using ForeignKeyConstraint = Cinar.Database.ForeignKeyConstraint;
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

            foreach (Table fkTable in Provider.Database.Tables)
            {
                Constraint cs = fkTable.Constraints.Find(c => c is PrimaryKeyConstraint || c is UniqueConstraint);
                if (cs != null)
                    comboForeignTableKeys.Items.Add(cs);
            }
            comboForeignTableKeys.SelectedIndexChanged += new EventHandler(comboForeignTableKeys_SelectedIndexChanged);
        }

        void comboForeignTableKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            Constraint cs = comboForeignTableKeys.SelectedItem as Constraint;
            if (cs == null)
                return;

            pnlForeignTableFields.SuspendLayout();
            pnlForeignTableFields.Controls.Clear();

            foreach (string foreignFieldName in cs.FieldNames)
            {
                Label lbl = new Label(){Text = "Map " + cs.Table.Name + "." + foreignFieldName + " column to:", Width = 200};
                ComboBox cb = new ComboBox {Width = 200, DropDownStyle = ComboBoxStyle.DropDownList, Left = 205};
                foreach (Field field in table.Fields)
                    cb.Items.Add(field);
                Panel p = new Panel() {Height = 25, Width = 405};
                p.Controls.Add(lbl);
                p.Controls.Add(cb);
                p.Tag = foreignFieldName;
                pnlForeignTableFields.Controls.Add(p);
            }
            pnlForeignTableFields.PerformLayout();
        }

        public BaseIndexConstraint GetCreatedKey()
        {
            BaseIndexConstraint index = null;

            if (tabControl.SelectedTab == tabPageFK)
                index = new ForeignKeyConstraint();
            else if (rbPrimaryKey.Checked)
                index = new PrimaryKeyConstraint();
            else if (rbUnique.Checked)
                index = new UniqueConstraint();
            else if (rbIndex.Checked)
                index = new Index();
            else
                throw new Exception("Please select an index/constraint type");

            index.FieldNames = new List<string>();
            index.Name = txtKeyName.Text.MakeFileName();
            if (index is ForeignKeyConstraint)
            {
                ForeignKeyConstraint fk = index as ForeignKeyConstraint;
                foreach (Panel panel in pnlForeignTableFields.Controls)
                    fk.FieldNames.Add(((panel.Controls[1] as ComboBox).SelectedItem as Field).Name);
                fk.RefConstraintName = (comboForeignTableKeys.SelectedItem as BaseIndexConstraint).Name;
                fk.RefTableName = (comboForeignTableKeys.SelectedItem as BaseIndexConstraint).Table.Name;
            }
            else
            {
                foreach (KeyField fd in keyFieldBindingSource.DataSource as List<KeyField>)
                    if (fd.Selected)
                        index.FieldNames.Add(fd.Name);
            }
            return index;
        }

        public void SetKey(BaseIndexConstraint index)
        {
            txtKeyName.Text = index.Name;
            rbUnique.Checked = index is UniqueConstraint;
            rbPrimaryKey.Checked = index is PrimaryKeyConstraint;
            rbIndex.Checked = index is Index;
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

            if (index is ForeignKeyConstraint)
            {
                comboForeignTableKeys.SelectedItem = index.Table.Database.GetConstraint((index as ForeignKeyConstraint).RefConstraintName);
                tabControl.SelectedTab = tabPageFK;
            }
            else
                tabControl.SelectedTab = tabPageLocal;
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
