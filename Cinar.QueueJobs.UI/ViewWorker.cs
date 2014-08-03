using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace Cinar.QueueJobs.UI
{
    public partial class ViewWorker : UserControl
    {
        public int WorkerId;
        public string WorkerName;
        private WorkerProcess workerProcess;

        private BackgroundWorkerEx backgroundWorker;

        public BackgroundWorkerEx BackgroundWorker {
            get {
                if (backgroundWorker == null)
                {
                    this.backgroundWorker = new BackgroundWorkerEx();
                    this.backgroundWorker.WorkerReportsProgress = true;
                    this.backgroundWorker.WorkerSupportsCancellation = true;
                    this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
                    this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
                }
                return backgroundWorker; 
            }
        }

        public ViewWorker(int workerId, string workerName, WorkerProcess workerProcess)
        {
            InitializeComponent();
            lblTitle.Text = WorkerName = workerName;
            WorkerId = workerId;
            this.workerProcess = workerProcess;
        }

        public void Run()
        {
            lblActiveSince.Text = DateTime.Now.ToString("yyyy.MM.dd HH:mm");
            if (!BackgroundWorker.IsBusy)
            {
                lblTitle.Text += ".";
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            InternalWorkerProcess process = new InternalWorkerProcess(backgroundWorker, workerProcess);

            process.Init(WorkerId);
            process.Run();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this.Parent == null) // eğer durum buysa bu ViewWorker panelden çıkarılmış demektir, ölmeyi bekliyor, buraya gelmesinin bir anlamı yok zaten.
                return;

            try
            {
                string[] parts = e.UserState.ToString().Split(':');
                progressBar1.Value = e.ProgressPercentage;

                switch (parts[0])
                {
                    case "job":
                        lblCommand.Text = parts[2];
                        lblName.Text = parts[3];
                        ((ViewWorkersFarm) this.Parent.Parent).lblStatus.Text = lblCommand.Text;
                        lblTitle.BackColor = Color.YellowGreen;
                        lblOKCount.BackColor = Color.YellowGreen;
                        break;
                    case "jobend":
                        lblTitle.BackColor = Color.LightSteelBlue;
                        lblOKCount.BackColor = Color.LightSteelBlue;
                        lblOKCount.Text = (int.Parse(lblOKCount.Text) + 1).ToString();
                        break;
                    case "hello":
                        lblTitle.Text = parts[1];
                        lblActiveSince.ForeColor = Color.Red;
                        setTimeout(lblActiveSince, () => { lblActiveSince.ForeColor = Color.Black; }, 200);
                        break;
                    case "progress":
                        progressBar1.Value = int.Parse(parts[1]);
                        break;
                    case "error":
                        ((ViewWorkersFarm)this.Parent.Parent).lblStatus.Text = "ERROR on " + parts[2] + " : " + parts[1];
                        if (((ViewWorkersFarm)this.Parent.Parent).Log != null)
                            ((ViewWorkersFarm)this.Parent.Parent).Log("ERROR on " + parts[2] + " : " + parts[1]);
                        break;
                }
            }
            catch(Exception ex)
            {
                if (((ViewWorkersFarm)this.Parent.Parent).Log != null)
                    ((ViewWorkersFarm)this.Parent.Parent).Log(ex.ToStringBetter());
            }
        }

        private void lblLastCommand_Click(object sender, EventArgs e)
        {
            MessageBox.Show(lblCommand.Text, "Queue Manager");
        }

        public void setTimeout(Control control, Action action, int timeout)
        {
            Thread t = new Thread(
                () =>
                {
                    Thread.Sleep(timeout);
                    control.Invoke(action);
                }
            );
            t.Start();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
            //Process.Start("http://address/Logs.aspx?SocketId=" + this.WorkerId);
        }
    }
}
