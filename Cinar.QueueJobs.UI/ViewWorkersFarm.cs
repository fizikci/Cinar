using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using db = Cinar.Database;
using System.Threading;
using Cinar.QueueJobs.Entities;

namespace Cinar.QueueJobs.UI
{
    public partial class ViewWorkersFarm : UserControl
    {
        private db.Database db;
        public Type WorkerProcessType { get; set; }
        public WorkerProcess WorkerProcess;

        public ViewWorkersFarm()
        {
            InitializeComponent();
        }

        private bool running = false;
        public bool Running { get { return running; } }

        public void Start()
        {
            if (WorkerProcessType == null)
            {
                MessageBox.Show("ViewWorkersFarm.WorkerProcess property cannot be NULL!");
                return;
            }

            WorkerProcess = (WorkerProcess)Activator.CreateInstance(WorkerProcessType);

            db = WorkerProcess.GetNewDatabaseInstance();

            // ensure tables existance
            db.Read(WorkerProcess.GetQueueDataType(), 1);
            db.Read(WorkerProcess.GetQueueType(), 1);
            db.Read(WorkerProcess.GetWorkerType(), 1);

            refreshWorkers(true);

            running = true;
        }

        int counter = 0;

        private void refreshWorkers(bool first)
        {
            if (!first && !running)
                return;

            try
            {
                timer.Enabled = false;

                if (counter % 60 == 0)
                {
                    var predefinedJobsSql = @"select
	                                                jd.Id,
	                                                jd.Name,
	                                                jd.CommandName,
	                                                jd.Request,
	                                                jd.RepeatInSeconds - TIME_TO_SEC(TIMEDIFF(now(), max(j.InsertDate))) as RepeatInSeconds
                                                from
	                                                JobDefinition jd
	                                                LEFT JOIN Job j ON j.Command = jd.CommandName
                                                group by
	                                                jd.Id, jd.Name, jd.CommandName, jd.Request, jd.RepeatInSeconds";

                    List<int> workerIds = db.GetList<int>("select Id from Worker order by Id");

                    int c = 0;
                    foreach (var jobDef in db.ReadList<JobDefinition>(predefinedJobsSql).Where(jd => jd.RepeatInSeconds <= 0))
                        WorkerProcess.AddJob(db, workerIds[c++ % workerIds.Count], jobDef.Name, jobDef.CommandName, jobDef.Request, 0, jobDef.Id);
                    
                }

                counter++;

                //önce disabled olmadığı halde 2 dakikadır çalışmayan socketleri modifiye edelim
                //if (!first)
                //    db.ExecuteNonQuery("UPDATE "+WorkerProcess.GetWorkerType().Name+" SET Modified=1 WHERE Disabled=0 AND LastExecution<{0}", DateTime.Now.AddSeconds(-120d));

                List<int> availableWorkers = new List<int>();

                if (first)
                    availableWorkers = db.GetList<int>("SELECT Id FROM " + WorkerProcess.GetWorkerType().Name + " WHERE Disabled=0");
                else
                {
                    Dictionary<int, int> modifiedWorkers = db.GetDictionary<int, int>("SELECT Id, Disabled FROM " + WorkerProcess.GetWorkerType().Name + " WHERE Modified=1");

                    if (modifiedWorkers == null || modifiedWorkers.Count == 0)
                        return;

                    foreach (var modifiedWorker in modifiedWorkers)
                    {
                        var ctrl = panelWorkers.Controls.Cast<ViewWorker>().FirstOrDefault(vs => vs.WorkerId == modifiedWorker.Key);
                        if (ctrl != null)
                        {
                            ctrl.BackgroundWorker.CancelAsync();
                            panelWorkers.Controls.Remove(ctrl);
                        }

                        if (modifiedWorker.Value == 0)
                            availableWorkers.Add(modifiedWorker.Key);
                    }
                    db.ExecuteNonQuery("UPDATE " + WorkerProcess.GetWorkerType().Name + " SET Modified=0 WHERE Modified=1");
                }

                foreach (var workerId in availableWorkers)
                {
                    ViewWorker vs = new ViewWorker(workerId, db.GetString("SELECT Name FROM " + WorkerProcess.GetWorkerType().Name + " WHERE Id={0}", workerId), (WorkerProcess)Activator.CreateInstance(WorkerProcessType));
                    panelWorkers.Controls.Add(vs);
                    vs.Run();

                    Thread.Sleep(20);
                    Refresh();
                }

                timer.Enabled = true;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "ERR: " + ex.Message;
                if (Log != null)
                    Log(lblStatus.Text);
            }
        }

        public Action<string> Log;

        private void timer_Tick(object sender, EventArgs e)
        {
            refreshWorkers(false);
        }

        public void Stop()
        {
            this.running = false;

            var count = panelWorkers.Controls.Count;

            for (int i = 0; i < count; i++)
            {
                ViewWorker viewWorker = (ViewWorker)panelWorkers.Controls[0];
                viewWorker.BackgroundWorker.CancelSync();
                panelWorkers.Controls.Remove(viewWorker);
                Refresh();
            }
        }

        public void OrderByName()
        {
            int i = 0;
            foreach (ViewWorker vs in panelWorkers.Controls.Cast<ViewWorker>().OrderBy(el => el.lblTitle.Text))
            {
                panelWorkers.Controls.SetChildIndex(vs, i);
                i++;
            }
        }
        public void OrderByRequestCount()
        {
            int i = 0;
            foreach (ViewWorker vs in panelWorkers.Controls.Cast<ViewWorker>().OrderBy(el => int.Parse(el.lblOKCount.Text)))
            {
                panelWorkers.Controls.SetChildIndex(vs, i);
                i++;
            }
        }
        public void OrderByActivationDate()
        {
            int i = 0;
            foreach (ViewWorker vs in panelWorkers.Controls.Cast<ViewWorker>().OrderBy(el => DateTime.Parse(el.lblActiveSince.Text)))
            {
                panelWorkers.Controls.SetChildIndex(vs, i);
                i++;
            }
        }
    }
}
