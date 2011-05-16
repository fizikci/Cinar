using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Menees.DiffUtils.Controls;
using Menees.DiffUtils;
using Cinar.Database;

namespace Cinar.DBTools.Tools
{
    public partial class FormCompareDatabases : Form, IDBToolsForm
    {
        public FormMain MainForm { get; set; }

        public FormCompareDatabases()
        {
            InitializeComponent();

            BindDatabases();
        }

        private void BindDatabases()
        {
            cbDstDb.Items.Clear();
            cbSrcDb.Items.Clear();
            foreach (ConnectionSettings cs in Provider.Connections)
            {
                cbDstDb.Items.Add(cs);
                cbSrcDb.Items.Add(cs);
            }
            cbDstDb.SelectedIndex = 0;
            cbSrcDb.SelectedIndex = 0;
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            DiffControl diffControl = new DiffControl();
            diffControl.Dock = DockStyle.Fill;
            panel.Controls.Clear();
            panel.Controls.Add(diffControl);

            bool chkIgnoreCase = false;
            bool chkIgnoreWhitespace = true;
            bool chkSupportChangeEditType = false;

            IList<string> code1, code2;

            code1 = GetDbDDL((cbSrcDb.SelectedItem as ConnectionSettings).Database);
            code2 = GetDbDDL((cbDstDb.SelectedItem as ConnectionSettings).Database);

            try
            {
                TextDiff diff = new TextDiff(HashType.CRC32, chkIgnoreCase, chkIgnoreWhitespace, 0, chkSupportChangeEditType);

                EditScript script = diff.Execute(code1, code2);

                diffControl.SetData(code1, code2, script);//, strA, strB);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private IList<string> GetDbDDL(Database.Database db1)
        {
            StringBuilder sb1 = new StringBuilder();
            foreach (Table tbl in db1.Tables.OrderBy(t => t.Name))
            {
                sb1.AppendLine(tbl.Name);
                foreach (Column f in tbl.Columns.OrderBy(f => f.Name))
                {
                    string fStr = "";
                    if (cbName.Checked)
                        fStr += f.Name + " ";
                    if (cbType.Checked)
                        fStr += f.ColumnType + " ";
                    if (cbLength.Checked && f.IsStringType())
                        fStr += f.Length + " ";
                    if (cbNullable.Checked)
                        fStr += f.IsNullable ? "Nullable " : "NotNull ";
                    if (fStr != "")
                        sb1.AppendLine("\t" + fStr);
                }
            }
            return sb1.ToString().Replace("\r", "").Split('\n');
        }
    }
}
