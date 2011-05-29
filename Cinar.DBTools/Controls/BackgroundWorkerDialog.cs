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
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (DoWork != null)
                DoWork(e);
        }

        public void ReportProgress(int percentProgress)
        {
            backgroundWorker.ReportProgress(percentProgress);
        }

        public Action<DoWorkEventArgs> DoWork;

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        public string Message
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
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
    }
}