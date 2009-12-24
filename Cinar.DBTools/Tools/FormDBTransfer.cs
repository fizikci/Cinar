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
    public partial class FormDBTransfer : Form
    {
        public FormDBTransfer()
        {
            InitializeComponent();

            foreach (ConnectionSettings cs in Provider.Connections)
            {
                cbDbDest.Items.Add(cs);
                cbDbSrc.Items.Add(cs);
            }
            cbDbDest.SelectedIndex = 0;
            cbDbSrc.SelectedIndex = 0;
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
            DBTransfer dbTranfer = (DBTransfer)e.Argument;
            dbTranfer.Transfer(cbTransferData.Checked);
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

        private void cbTransferData_CheckedChanged(object sender, EventArgs e)
        {
            groupTransferData.Enabled = cbTransferData.Checked;
        }
    }
}
