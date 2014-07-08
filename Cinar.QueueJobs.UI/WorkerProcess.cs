using Cinar.QueueJobs.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DB = Cinar.Database;

namespace Cinar.QueueJobs.UI
{
    internal class InternalWorkerProcess
    {
        private BaseWorker worker;
        private DB.Database db;
        private WorkerProcess workerProcess;

        private BackgroundWorker backgroundWorker;
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);

        public InternalWorkerProcess(BackgroundWorker backgroundWorker, WorkerProcess workerProcess)
        {
            this.backgroundWorker = backgroundWorker;
            this.db = workerProcess.GetNewDatabaseInstance();
            this.workerProcess = workerProcess;
            workerProcess.ReportProgress = this.ReportProgress;
        }

        public void Init(int workerId)
        {
            worker = (BaseWorker)db.Read(workerProcess.GetWorkerType(), workerId);

            if (worker == null)
                throw new Exception("Worker with id " + workerId + " not defined in database");

            worker.LastExecution = DateTime.Now;
            worker.LastExecutionInfo = "start";
            worker.ActiveSince = DateTime.Now;
            db.Save(worker);
        }

        public void ReportProgress(int percent)
        {
            if (backgroundWorker.CancellationPending)
                throw new Exception("Manager is waiting for this to die");

            worker.LastExecution = DateTime.Now;
            worker.Save();
            backgroundWorker.ReportProgress(1, "progress:" + percent);
        }

        public void Run()
        {
            backgroundWorker.ReportProgress(1, "hello:" + worker.Name);

            while (true)
            {

                try
                {

                    if (backgroundWorker.CancellationPending)
                    {
                        backgroundWorker.ReportProgress(100, "finish");
                        db.FillEntity(worker);
                        worker.LastExecutionInfo = "finish";
                        db.Save(worker);
                        break;
                    }

                    BaseJob queue = getWaitingJob();

                    if (queue == null)
                    {
                        Thread.Sleep(1000);
                        continue; //***
                    }

                    backgroundWorker.ReportProgress(1, string.Format("job:{0}:{1}:{2}", queue.Id, queue.Command, queue.Name));

                    executeCommand(queue);

                    backgroundWorker.ReportProgress(1, "jobend");
                }
                catch (Exception ex)
                {
                    backgroundWorker.ReportProgress(1, "jobend");

                    backgroundWorker.ReportProgress(1, string.Format("error:{0}:{1}", ex.Message+(ex.InnerException!=null ? " ("+ex.InnerException.Message+")":""), worker.Name));

                    db.FillEntity(worker);
                    worker.Modified = 1;
                    db.Save(worker);
                    break;
                }

                Thread.Sleep(100);
            }
        }

        private void executeCommand(BaseJob job)
        {
            BaseJobData jobData = (BaseJobData)db.Read(workerProcess.GetQueueDataType(), "JobId = {0}", job.Id);
            if(jobData==null)
                throw new Exception("Queue job related data not found");

            Stopwatch sw = new Stopwatch();
            try
            {
                sw.Start();
                jobData.Response = workerProcess.ExecuteJob(job, jobData);
                sw.Stop();
            }
            catch (Exception ex)
            {
                jobData.Response = ex.Message + "\n" + (ex.InnerException != null ? "- " + ex.InnerException.Message : "");
                db.Save(jobData);

                job.Status = JobStatuses.Failed;

                db.Save(job);
                return;
            }

            db.Save(jobData);

            job.Status = JobStatuses.Done;
            job.ProcessTime = (int)sw.ElapsedMilliseconds;
            db.Save(job);

            db.FillEntity(worker);
            worker.LastExecution = DateTime.Now;
            worker.LastExecutionInfo = job.Command + ": " + job.Name;
            db.Save(worker);
        }

        private BaseJob getWaitingJob()
        {
            BaseJob job = (BaseJob)db.Read(workerProcess.GetQueueType(), "select top 1 * from " + workerProcess.GetQueueType().Name + " where WorkerId={0} AND Status='New' order by Id", worker.Id);
            if (job != null)
            {
                job.Status = JobStatuses.Processing;
                db.Save(job);
            }
            return job;
        }
    }

    public abstract class WorkerProcess
    {
        public abstract Type GetWorkerType();
        public abstract Type GetQueueType();
        public abstract Type GetQueueDataType();
        public abstract string ExecuteJob(BaseJob job, BaseJobData jobData);
        public abstract Database.Database GetNewDatabaseInstance();

        public Action<int> ReportProgress;
    }
}
