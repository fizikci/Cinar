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

namespace Cinar.DBTools.Tools
{
    public partial class FormDBTransfer : Form, IDBToolsForm
    {
        public FormMain MainForm { get; set; }

        public FormDBTransfer()
        {
            InitializeComponent();

            if (Provider.Connections.Count < 2)
                throw new Exception("Add one more connection for transfer target");

            foreach (ConnectionSettings cs in Provider.Connections)
            {
                cbDbSrc.Items.Add(cs);
                cbDbDest.Items.Add(cs);
            }
            cbDbSrc.SelectedIndex = cbDbSrc.Items.IndexOf(Provider.ActiveConnection);
            cbDbDest.SelectedIndex = cbDbSrc.SelectedIndex < cbDbSrc.Items.Count - 1 ? cbDbSrc.SelectedIndex + 1 : cbDbSrc.SelectedIndex - 1;
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
                new DBTransfer(
                    (cbDbSrc.SelectedItem as ConnectionSettings).Database,
                    (cbDbDest.SelectedItem as ConnectionSettings).Database,
                    lbTables.SelectedItems.Cast<string>().ToList(),
                    Convert.ToInt32(editPageSize.Value),
                    Convert.ToInt32(editLimit.Value),
                    txtPrefix.Text,
                    log));
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DBTransfer dbTranfer = (DBTransfer)e.Argument;
                dbTranfer.Transfer(cbTransferData.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void log(string msg)
        {
            backgroundWorker.ReportProgress(0, msg);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStart.Enabled = true;
            btnCancel.Enabled = false;

            if (e.Error != null)
                MessageBox.Show(e.Error.Message, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtLog.Text += e.UserState.ToString();
        }

        private void cbTransferData_CheckedChanged(object sender, EventArgs e)
        {
            groupTransferData.Enabled = cbTransferData.Checked;
        }
    }
}
