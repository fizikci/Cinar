using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Database;
using Cinar.Database.Tools;
using System.IO;

namespace Cinar.DBTools.Tools
{
    public partial class FormSQLDump : Form, IDBToolsForm
    {
        public FormMain MainForm { get; set; }

        public FormSQLDump()
        {
            InitializeComponent();

            foreach (ConnectionSettings cs in Provider.Connections)
                cbDbSrc.Items.Add(cs);
            cbDbSrc.SelectedIndex = cbDbSrc.Items.IndexOf(Provider.ActiveConnection);

            foreach (object o in Enum.GetValues(typeof(DatabaseProvider)))
                cbProvider.Items.Add(o);
            cbProvider.SelectedIndex = cbProvider.Items.IndexOf(Provider.Database.Provider);
        }

        private void cbDbSrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbTables.Items.Clear();
            ConnectionSettings cs = (ConnectionSettings)cbDbSrc.SelectedItem;
            foreach (Table tbl in cs.Database.Tables)
                lbTables.Items.Add(tbl.Name);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnCancel.Enabled = true;

            backgroundWorker.RunWorkerAsync(
                new DBDump()
                {
                    Database = (cbDbSrc.SelectedItem as ConnectionSettings).Database,
                    TableList = lbTables.SelectedItems.Cast<string>().ToList(),
                    FilePath = txtFileName.Text,
                    Provider = (DatabaseProvider)cbProvider.SelectedItem
                });
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DBDump dbDump = (DBDump)e.Argument;
            File.WriteAllText(dbDump.FilePath, "");
            foreach (var tableName in dbDump.TableList)
            {
                Table t = dbDump.Database.Tables[tableName];
                backgroundWorker.ReportProgress(0, "dumping " + t.Name + "...\r\n");
                File.AppendAllText(dbDump.FilePath, dbDump.Database.GetTableDDL(t, dbDump.Provider));
                File.AppendAllText(dbDump.FilePath, t.Dump(dbDump.Provider));
            }
            backgroundWorker.ReportProgress(100, "finished :)\r\n");
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStart.Enabled = true;
            btnCancel.Enabled = false;

            if (e.Error != null)
                MessageBox.Show(e.Error.Message, "Çınar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtLog.Text += e.UserState.ToString();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
                txtFileName.Text = sfd.FileName;
        }

    }

    internal class DBDump
    {
        public Database.Database Database { get; set; }
        public List<string> TableList { get; set; }
        public string FilePath { get; set; }
        public DatabaseProvider Provider { get; set; }
    }
}
