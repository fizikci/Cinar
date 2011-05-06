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
            checkDatabaseSchema();
        }

        private void checkDatabaseSchema()
        {
            lbProblems.Items.Clear();

            foreach (Table tbl in Provider.Database.Tables)
            {
                if (tbl.IsView)
                    continue;
                if (tbl.PrimaryField == null)
                    lbProblems.Items.Add(new PrimaryKeyDoesntExist() { Table = tbl });
                else if(!tbl.PrimaryField.IsAutoIncrement)
                    lbProblems.Items.Add(new PrimaryKeyIsNotAutoIncrement() { Field = tbl.PrimaryField });

                foreach (Field f in tbl.Fields)
                    if (f.Name.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase) && !f.IsPrimaryKey)
                    {
                        if (f.ReferenceField == null)
                        {
                            string tblName = f.Name.Substring(0, f.Name.Length - 2).Trim('_');
                            lbProblems.Items.Add(new PossibleForeignKey() { Field = f, Table = Provider.Database.Tables[tblName] });
                        }
                    }
            }
            for (int i = 0; i < lbProblems.Items.Count; i++)
                if (lbProblems.Items[i] is PossibleForeignKey && !lbProblems.Items[i].ToString().Contains("??"))
                    lbProblems.SetSelected(i, true);
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            foreach (Problem problem in lbProblems.SelectedItems)
                problem.Fix();
            checkDatabaseSchema();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            checkDatabaseSchema();
        }
    }

    public abstract class Problem
    {
        public Table Table { get; set; }
        public Field Field { get; set; }

        public abstract void Fix();
    }

    public class PrimaryKeyDoesntExist : Problem
    {
        public override string ToString()
        {
            return String.Format("Primary Key doesn't exist for the table {0}", (object) Table);
        }
        public override void Fix()
        {
            Form form = new Form();
            form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            form.Width = 400;
            form.Height = 70;
            form.Text = " Select Primary Key For " + Table;
            ComboBox cb = new ComboBox();
            cb.Left = 10;
            cb.Top = 10;
            cb.Items.Add("");
            foreach (Field f in Table.Fields)
                cb.Items.Add(f);
            Button btnOk = new Button();
            btnOk.Left = 10 + cb.Width + 10;
            btnOk.Top = 10;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Text = "OK";
            Button btnCancel = new Button();
            btnCancel.Left = 10 + cb.Width + 10 + btnOk.Width + 10;
            btnCancel.Top = 10;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Text = "Cancel";
            form.Controls.Add(cb);
            form.Controls.Add(btnOk);
            form.Controls.Add(btnCancel);
            if (form.ShowDialog() == DialogResult.OK && cb.SelectedItem is Field)
            {
                Field f = (Field)cb.SelectedItem;
                if (f != null)
                {
                    Table.Constraints.Add(new PrimaryKeyConstraint() { FieldNames = new List<string>() { f.Name }, Name = "PK" + Table.Name});
                }
            }
        }
    }

    public class PrimaryKeyIsNotAutoIncrement : Problem
    {
        public override string ToString()
        {
            return String.Format("Primary Key is not auto increment: {0}", (object) Field);
        }
        public override void Fix()
        {
            Field.IsAutoIncrement = true;
            try
            {
                string sql = Provider.Database.GetSQLColumnSetAutoIncrement(Field);
                SQLInputDialog sid = new SQLInputDialog(sql, true);
                if(sid.ShowDialog()==DialogResult.OK)
                    Provider.Database.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                Field.IsAutoIncrement = false;
                MessageBox.Show("Field cannot be set as auto increment.\n\n"+ex.Message, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class PossibleForeignKey : Problem
    {
        public override string ToString()
        {
            return String.Format("Possible foreign key: {0}.{1} -> {2}.{3}", Field.Table.Name, Field.Name, Table == null ? "??" : Table.Name, Table == null ? "??" : Table.PrimaryField == null ? "??" : Table.PrimaryField.Name);
        }
        public override void Fix()
        {
            if (Table != null)
            {
                createForeignKey();
            }
            else
            {
                Form form = new Form();
                form.Text = "Select Table for " + Field;
                form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                form.Width = 400;
                form.Height = 70;
                ComboBox cb = new ComboBox();
                cb.Left = 10;
                cb.Top = 10;
                cb.Items.Add("");
                foreach (Table t in Provider.Database.Tables)
                    cb.Items.Add(t);
                Button btnOk = new Button();
                btnOk.Left = 10 + cb.Width + 10;
                btnOk.Top = 10;
                btnOk.DialogResult = DialogResult.OK;
                btnOk.Text = "OK";
                Button btnCancel = new Button();
                btnCancel.Left = 10 + cb.Width + 10 + btnOk.Width + 10;
                btnCancel.Top = 10;
                btnCancel.DialogResult = DialogResult.Cancel;
                btnCancel.Text = "Cancel";
                form.Controls.Add(cb);
                form.Controls.Add(btnOk);
                form.Controls.Add(btnCancel);
                if (form.ShowDialog() == DialogResult.OK && cb.SelectedItem is Table)
                {
                    Table t = (Table)cb.SelectedItem;
                    if (t != null)
                    {
                        createForeignKey(t);
                    }
                }
            }
        }

        private void createForeignKey(Table foreignTable = null)
        {
            if (foreignTable != null)
                Table = foreignTable;

            ForeignKeyConstraint fk = new ForeignKeyConstraint()
                                          {
                                              FieldNames = new List<string>() {Field.Name },
                                              Name = string.Format("FK_{0}_{1}_{2}", Field.Table.Name, Field.Name, Table.Name ),
                                              RefConstraintName = Table.Constraints.Find(c=>c is PrimaryKeyConstraint).Name,
                                              RefTableName = Table.Name
                                          };
            Field.Table.Constraints.Add(fk);
            try
            {
                string sql = Provider.Database.GetSQLConstraintAdd(fk);
                SQLInputDialog sid = new SQLInputDialog(sql, true);
                if (sid.ShowDialog() == DialogResult.OK)
                    Provider.Database.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                Field.Table.Constraints.Remove(fk);
                MessageBox.Show("Foreign key cannot be created.\n\n" + ex.Message, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}