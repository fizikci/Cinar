using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Database;
using Cinar.DBTools.Controls;
using ForeignKeyConstraint = Cinar.Database.ForeignKeyConstraint;

namespace Cinar.DBTools.Tools
{
    public partial class FormCheckDatabaseSchema : Form, IDBToolsForm
    {
        public FormMain MainForm { get; set; }

        public FormCheckDatabaseSchema()
        {
            InitializeComponent();

            btnFix.Image = FamFamFam.database_gear;
            btnFix.TextAlign = ContentAlignment.MiddleLeft;
            btnFix.TextImageRelation = TextImageRelation.ImageBeforeText;

            btnClose.Image = FamFamFam.cancel;
            btnClose.TextAlign = ContentAlignment.MiddleLeft;
            btnClose.TextImageRelation = TextImageRelation.ImageBeforeText;

            checkDatabaseSchema();
        }

        private void checkDatabaseSchema()
        {
            flowPanel.Controls.Clear();

            foreach (Table tbl in Provider.Database.Tables)
            {
                if (tbl.IsView)
                    continue;
                Problem p = null;
                if (tbl.PrimaryColumn == null)
                    p = new PrimaryKeyDoesntExist() { Table = tbl };
                else if (!tbl.PrimaryColumn.IsAutoIncrement)
                    p = new PrimaryKeyIsNotAutoIncrement() { Column = tbl.PrimaryColumn };

                foreach (Column f in tbl.Columns)
                    if (f.Name.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase) && !f.IsPrimaryKey)
                        if (f.ReferenceColumn == null)
                        {
                            string tblName = f.Name.Substring(0, f.Name.Length - 2).Trim('_');
                            if (tblName == "") continue;

                            Table fTbl = Provider.Database.Tables[tblName];
                            if (fTbl == null && tblName.Length > 1) fTbl = Provider.Database.Tables[tblName.Substring(0, tblName.Length - 1)];
                            if (fTbl == null && tblName.Length > 2) fTbl = Provider.Database.Tables[tblName.Substring(0, tblName.Length - 2)];
                            if (fTbl == null) fTbl = Provider.Database.Tables[tblName + "s"];
                            if (fTbl == null) fTbl = Provider.Database.Tables[tblName + "es"];
                            p = new PossibleForeignKey() { Column = f, Table = fTbl };
                            Panel pnl = p.GetUI(); pnl.Tag = p;
                            flowPanel.Controls.Add(pnl);
                            p = null;
                        }

                if (p != null)
                {
                    Panel pnl = p.GetUI(); pnl.Tag = p;
                    flowPanel.Controls.Add(pnl);
                }
            }
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Panel pnl in flowPanel.Controls)
            {
                string sql = (pnl.Tag as Problem).Fix(pnl);
                if (sql != "")
                    sb.AppendLine(sql + ";");
            }
            if (sb.Length > 0)
            {
                if (MainForm.CurrEditor is SQLEditorAndResults && string.IsNullOrEmpty(MainForm.CurrEditor.Content))
                    MainForm.CurrEditor.Content = sb.ToString();
                else
                    MainForm.addSQLEditor("", sb.ToString());
            }
            checkDatabaseSchema();
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnApplyToMetadata_Click(object sender, EventArgs e)
        {
            foreach (Panel pnl in flowPanel.Controls)
                (pnl.Tag as Problem).ApplyToMetadata(pnl);
            Close();
        }
    }

    public abstract class Problem
    {
        public Table Table { get; set; }
        public Column Column { get; set; }

        public abstract string Fix(Panel p);
        public abstract Panel GetUI();
        public abstract void ApplyToMetadata(Panel p);
    }

    public class PrimaryKeyDoesntExist : Problem
    {
        public override Panel GetUI()
        {
            Panel p = new Panel() { Width = 520, Height = 24};
            Label lbl = new Label() {Width=300, TextAlign = ContentAlignment.MiddleRight};
            lbl.Text = "Select primary key field for " + Table + " :";
            ComboBox cb = new ComboBox() { Width=200, Left = 300, DropDownStyle = ComboBoxStyle.DropDownList};
            cb.Items.Add("");
            foreach (Column f in Table.Columns)
                cb.Items.Add(f);
            p.Controls.Add(lbl);
            p.Controls.Add(cb);
            return p;
        }
        public override string Fix(Panel p)
        {
            ComboBox cb = p.Controls[1] as ComboBox;
            if (cb.SelectedItem is Column)
            {
                Column f = (Column)cb.SelectedItem;
                var pk = new PrimaryKeyConstraint() { ColumnNames = new List<string>() { f.Name }, Name = "PK_" + Table.Name};
                return Provider.Database.GetSQLConstraintAdd(pk);
            }
            return "";
        }
        public override void ApplyToMetadata(Panel p)
        {
            ComboBox cb = p.Controls[1] as ComboBox;
            if (cb.SelectedItem is Column)
            {
                Column f = (Column)cb.SelectedItem;
                var pk = new PrimaryKeyConstraint() { ColumnNames = new List<string>() { f.Name }, Name = "PK_" + Table.Name };
                f.Table.Constraints.Add(pk);
            }
        }
    }

    public class PrimaryKeyIsNotAutoIncrement : Problem
    {
        public override Panel GetUI()
        {
            Panel p = new Panel() { Width = 520, Height = 24 };
            Label lbl = new Label() { Width = 300, TextAlign = ContentAlignment.MiddleRight };
            lbl.Text = String.Format("Set {0} as auto increment:", Column);
            CheckBox cb = new CheckBox() { Width = 100, Left = 300 };
            p.Controls.Add(lbl);
            p.Controls.Add(cb);
            return p;
        }
        public override string Fix(Panel p)
        {
            CheckBox cb = p.Controls[1] as CheckBox;
            if (cb.Checked)
                return Provider.Database.GetSQLColumnSetAutoIncrement(Column);
            return "";
        }
        public override void ApplyToMetadata(Panel p)
        {
            CheckBox cb = p.Controls[1] as CheckBox;
            if (cb.Checked)
                Column.IsAutoIncrement = true;
        }
    }

    public class PossibleForeignKey : Problem
    {
        public override Panel GetUI()
        {
            Panel p = new Panel() { Width = 520, Height = 24 };
            Label lbl = new Label() { Width = 300, TextAlign = ContentAlignment.MiddleRight };
            lbl.Text = String.Format("Possible FK from {0}.{1} to: ", Column.Table.Name, Column.Name);
            ComboBox cb = new ComboBox() { Width = 200, Left = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            cb.Items.Add("");
            foreach (Table t in Provider.Database.Tables)
                cb.Items.Add(t);
            if (Table != null) cb.SelectedItem = Table;
            p.Controls.Add(lbl);
            p.Controls.Add(cb);
            return p;
        }
        public override string Fix(Panel p)
        {
            ComboBox cb = p.Controls[1] as ComboBox;
            if (cb.SelectedItem is Table)
            {
                Table = cb.SelectedItem as Table;
                ForeignKeyConstraint fk = new ForeignKeyConstraint()
                {
                    ColumnNames = new List<string>() { Column.Name },
                    Name = string.Format("FK_{0}_{1}_{2}", Column.Table.Name, Column.Name, Table.Name),
                    RefConstraintName = Table.Constraints.Find(c => c is PrimaryKeyConstraint).Name,
                    RefTableName = Table.Name
                };
                Column.Table.Constraints.Add(fk);
                string sql = Provider.Database.GetSQLConstraintAdd(fk);
                Column.Table.Constraints.Remove(fk);
                return sql;
            }
            return "";
        }
        public override void ApplyToMetadata(Panel p)
        {
            ComboBox cb = p.Controls[1] as ComboBox;
            if (cb.SelectedItem is Table)
            {
                Table = cb.SelectedItem as Table;
                ForeignKeyConstraint fk = new ForeignKeyConstraint()
                {
                    ColumnNames = new List<string>() { Column.Name },
                    Name = string.Format("FK_{0}_{1}_{2}", Column.Table.Name, Column.Name, Table.Name),
                    RefConstraintName = Table.Constraints.Find(c => c is PrimaryKeyConstraint).Name,
                    RefTableName = Table.Name
                };
                Column.Table.Constraints.Add(fk);
            }
        }
    }
}