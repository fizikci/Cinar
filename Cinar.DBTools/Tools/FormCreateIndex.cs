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

            List<KeyColumn> list = new List<KeyColumn>();
            foreach (Column f in table.Columns)
            {
                list.Add(new KeyColumn()
                {
                    ColumnType = string.IsNullOrEmpty(f.ColumnTypeOriginal) ? Provider.Database.DbTypeToString(f.ColumnType) : f.ColumnTypeOriginal,
                    Length = (int)f.Length,
                    Name = f.Name
                });
            }
            keyColumnBindingSource.DataSource = list;

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

            pnlForeignTableColumns.SuspendLayout();
            pnlForeignTableColumns.Controls.Clear();

            foreach (string foreignColumnName in cs.ColumnNames)
            {
                Label lbl = new Label(){Text = "Map " + cs.Table.Name + "." + foreignColumnName + " column to:", Width = 200};
                ComboBox cb = new ComboBox {Width = 200, DropDownStyle = ComboBoxStyle.DropDownList, Left = 205};
                foreach (Column column in table.Columns)
                    cb.Items.Add(column);
                Panel p = new Panel() {Height = 25, Width = 405};
                p.Controls.Add(lbl);
                p.Controls.Add(cb);
                p.Tag = foreignColumnName;
                pnlForeignTableColumns.Controls.Add(p);
            }
            pnlForeignTableColumns.PerformLayout();
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

            index.ColumnNames = new List<string>();
            index.Name = txtKeyName.Text.MakeFileName();
            if (index is ForeignKeyConstraint)
            {
                ForeignKeyConstraint fk = index as ForeignKeyConstraint;
                foreach (Panel panel in pnlForeignTableColumns.Controls)
                    fk.ColumnNames.Add(((panel.Controls[1] as ComboBox).SelectedItem as Column).Name);
                fk.RefConstraintName = (comboForeignTableKeys.SelectedItem as BaseIndexConstraint).Name;
                fk.RefTableName = (comboForeignTableKeys.SelectedItem as BaseIndexConstraint).Table.Name;
            }
            else
            {
                foreach (KeyColumn fd in keyColumnBindingSource.DataSource as List<KeyColumn>)
                    if (fd.Selected)
                        index.ColumnNames.Add(fd.Name);
            }
            return index;
        }

        public void SetKey(BaseIndexConstraint index)
        {
            txtKeyName.Text = index.Name;
            rbUnique.Checked = index is UniqueConstraint;
            rbPrimaryKey.Checked = index is PrimaryKeyConstraint;
            rbIndex.Checked = index is Index;
            List<KeyColumn> columns = new List<KeyColumn>();
            foreach (Column f in table.Columns)
            {
                columns.Add(new KeyColumn()
                {
                    Selected = index.ColumnNames.Contains(f.Name),
                    ColumnType = string.IsNullOrEmpty(f.ColumnTypeOriginal) ? Provider.Database.DbTypeToString(f.ColumnType) : f.ColumnTypeOriginal,
                    Length = (int)f.Length,
                    Name = f.Name
                });
            }
            keyColumnBindingSource.DataSource = columns;

            if (index is ForeignKeyConstraint)
            {
                comboForeignTableKeys.SelectedItem = index.Table.Database.GetConstraint((index as ForeignKeyConstraint).RefConstraintName);
                tabControl.SelectedTab = tabPageFK;
            }
            else
                tabControl.SelectedTab = tabPageLocal;
        }
    }
    public class KeyColumn
    {
        public bool Selected { get; set; }
        public string Name { get; set; }
        public string ColumnType { get; set; }
        public int Length { get; set; }
    }
    public class KeyDef
    {
        public List<KeyColumn> Columns { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsUnique { get; set; }

        public KeyDef()
        {
            Columns = new List<KeyColumn>();
        }
    }

}
