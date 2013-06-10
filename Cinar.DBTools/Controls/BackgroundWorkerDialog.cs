using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cinar.DBTools.Controls
{
    public partial class BackgroundWorkerDialog : Form
    {
        public BackgroundWorkerDialog()
        {
            InitializeComponent();

            backgroundWorker.WorkerSupportsCancellation = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            label.Text = string.Format(MessageFormat, "");

            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (DoWork != null)
                Result = DoWork();
        }

        public string Result { get; set; }

        public void ReportProgress(int percentProgress, object userState)
        {
            backgroundWorker.ReportProgress(percentProgress, userState);
        }

        public Func<string> DoWork;
        public Action Completed;

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(progressBar.Value != e.ProgressPercentage)
                progressBar.Value = e.ProgressPercentage;
            if (e.UserState != null)
                label.Text = string.Format(MessageFormat, e.UserState);
        }

        public string MessageFormat { get; set; }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
            if(Completed!=null)
                Completed();
        }

        public bool Canceled
        {
            get { return backgroundWorker.CancellationPending; }
        }

        public int Maximum
        {
            get { return progressBar.Maximum; }
            set { progressBar.Maximum = value; }
        }

        public int Minimum
        {
            get { return progressBar.Minimum; }
            set { progressBar.Minimum = value; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
        }
    }
}