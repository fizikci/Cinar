using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using Cinar.Scripting;

namespace Cinar.DBTools.Tools
{
    public partial class FormScriptingTest : Form, IDBToolsForm
    {
        List<ScriptingTest> data;
        string path;

        public FormMain MainForm { get; set; }
        
        public FormScriptingTest()
        {
            InitializeComponent();

            data = new List<ScriptingTest>();

            path = Path.GetDirectoryName(Application.ExecutablePath) + "\\scriptingtests.xml";
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(List<ScriptingTest>));
                    data = (List<ScriptingTest>)ser.Deserialize(sr);
                }
            }

            showCategories("All");
        }

        private void showCategories(string selectedCat)
        {
            cbCategories.Items.Clear();

            cbCategories.Items.Add("All");

            foreach (string cat in data.Select(c => c.Category).Distinct().OrderBy(s => s))
                cbCategories.Items.Add(cat ?? "");

            if (!string.IsNullOrEmpty(selectedCat))
                cbCategories.SelectedIndex = cbCategories.Items.IndexOf(selectedCat);

            showList();
        }

        private void cbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            showList();
        }

        private void showList()
        {
            string selectedCat = (cbCategories.SelectedItem == null || cbCategories.SelectedItem.ToString() == "") ? null : cbCategories.SelectedItem.ToString();

            if (selectedCat == "All")
            {
                grid.DataSource = data.OrderBy(t => t.Name).ToList();
                foreach (DataGridViewColumn col in grid.Columns)
                    col.Visible = col.HeaderText == "Name" || col.HeaderText == "Category" || col.HeaderText == "Result";
            }
            else
            {
                grid.DataSource = data.Where(t => t.Category == selectedCat).OrderBy(t=>t.Name).ToList();
                foreach (DataGridViewColumn col in grid.Columns)
                    col.Visible = col.HeaderText == "Name" || col.HeaderText == "Result";
            }
        }

        private void btnDeleteSelectedTask_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a test.");
                return;
            }

            ScriptingTest task = (ScriptingTest)grid.SelectedRows[0].DataBoundItem;
            if (MessageBox.Show("Are you sure to delete " + task, "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                data.Remove(task);
                save();
                showCategories(task.Category);
            }
        }

        private void save()
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                XmlSerializer ser = new XmlSerializer(typeof(List<ScriptingTest>));
                ser.Serialize(sw, data);
            }
        }

        private void btnAddNewTask_Click(object sender, EventArgs e)
        {
            ScriptingTest tmp = new ScriptingTest();
            tmp.Name = "New Test";
            tmp.Code = "Code...";
            tmp.Category = (cbCategories.SelectedItem == null || cbCategories.SelectedItem.ToString() == "") ? null : cbCategories.SelectedItem.ToString();

            FormScriptingTestDetail form = new FormScriptingTestDetail(tmp);
            if (form.ShowDialog() == DialogResult.OK)
            {
                data.Add(form.Test);
                showCategories(form.Test.Category);
                save();
            }
        }

        private void btnEditSelectedTask_Click(object sender, EventArgs e)
        {
            ScriptingTest tmp = (ScriptingTest)grid.CurrentRow.DataBoundItem;
            FormScriptingTestDetail form = new FormScriptingTestDetail(tmp);
            if (form.ShowDialog() == DialogResult.OK)
            {
                data[data.IndexOf(tmp)] = form.Test;
                showCategories(form.Test.Category);
                save();
            }
        }

        private void btnRunTests_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                ScriptingTest test = (ScriptingTest) row.DataBoundItem;

                Interpreter pret = new Interpreter(test.Code, null);
                pret.AddAssembly(typeof(POP3.Pop3Client).Assembly);
                pret.SetAttribute("db", Provider.Database);
                pret.Parse();
                pret.Execute();

                test.Result = pret.Output;

                if (pret.Successful && test.Result.Trim() == "OK")
                    row.Cells["Result"].Style.BackColor = Color.Green;
                else
                    row.Cells["Result"].Style.BackColor = Color.Red;

                row.Cells["Result"].Style.ForeColor = Color.White;
            }
        }

    }

    public class ScriptingTest
    {
        public ScriptingTest()
        {
            Result = "Unknown";
        }

        public string Name { get; set; }
        public string Category { get; set; }
        public string Code { get; set; }
        [XmlIgnore]
        public string Result { get; set; }
    }
}
