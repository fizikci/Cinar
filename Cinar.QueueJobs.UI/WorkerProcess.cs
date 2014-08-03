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
        private Worker worker;
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
            workerProcess.Log = this.Log;
        }

        public void Init(int workerId)
        {
            worker = (Worker)db.Read(workerProcess.GetWorkerType(), workerId);

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

            //db.FillEntity(worker);
            //worker.LastExecution = DateTime.Now;
            //db.Save(worker);
            backgroundWorker.ReportProgress(percent, "progress:" + percent);
        }

        public void Log(string message)
        {
            backgroundWorker.ReportProgress(-1, "log:" + message);
        }

        public void Run()
        {
            backgroundWorker.ReportProgress(1, "hello:" + worker.Name);

            while (true)
            {
                Job _job = null;
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

                    _job = getWaitingJob();

                    if (_job == null)
                    {
                        Thread.Sleep(1000);
                        db.FillEntity(worker);
                        worker.LastExecution = DateTime.Now;
                        db.Save(worker);
                        continue; //***
                    }

                    backgroundWorker.ReportProgress(1, string.Format("job:{0}:{1}:{2}", _job.Id, _job.Command, _job.Name.Replace("http://", "").Replace("www.", "")));

                    executeCommand(_job);

                    backgroundWorker.ReportProgress(100, "jobend");
                }
                catch (Exception ex)
                {
                    backgroundWorker.ReportProgress(100, "jobend");

                    backgroundWorker.ReportProgress(100, string.Format("error:{0}:{1}", ex.Message + (ex.InnerException != null ? " (" + ex.InnerException.Message + ")" : ""), worker.Name));

                    if (_job != null) {
                        _job.Status = JobStatuses.Failed;
                        db.Save(_job);
                        if (workerProcess.UseJobData)
                        {
                            JobData _jobData = (JobData)db.Read(workerProcess.GetQueueDataType(), "JobId = {0}", _job.Id);
                            _jobData.Response = ex.Message + (ex.InnerException != null ? " (" + ex.InnerException.Message + ")" : "");
                            db.Save(_jobData);
                        }
                    }

                }
                if(db.Connection.State != System.Data.ConnectionState.Closed) db.Connection.Close();
                Thread.Sleep(100);
            }
        }

        private void executeCommand(Job job)
        {
            db.FillEntity(worker);
            worker.LastExecution = DateTime.Now;
            worker.LastExecutionInfo = job.Command + ": " + job.Name;
            db.Save(worker);

            JobData jobData = null;
            if (workerProcess.UseJobData)
            {
                jobData = (JobData)db.Read(workerProcess.GetQueueDataType(), "JobId = {0}", job.Id) ?? new JobData();
                if (jobData == null)
                {
                    jobData = new JobData
                    {
                        JobId = job.Id,
                        Response = "Job related data not found!"
                    };
                    db.Save(jobData);
                    job.Status = JobStatuses.Failed;
                    db.Save(job);
                    backgroundWorker.ReportProgress(100, string.Format("error:{0}:{1}", "Job related data not found!", worker.Name));
                    return;
                }
            }

            Stopwatch sw = new Stopwatch();
            try
            {
                sw.Start();
                var res = workerProcess.ExecuteJob(job, jobData);
                if (workerProcess.UseJobData)
                    jobData.Response = res;
                sw.Stop();
            }
            catch (Exception ex)
            {
                if (workerProcess.UseJobData)
                {
                    jobData.Response = ex.Message + "\n" + (ex.InnerException != null ? "- " + ex.InnerException.Message : "");
                    db.Save(jobData);
                }

                job.Status = JobStatuses.Failed;

                db.Save(job);
                return;
            }

            if (workerProcess.UseJobData)
                db.Save(jobData);

            job.Status = JobStatuses.Done;
            job.ProcessTime = (int)sw.ElapsedMilliseconds;
            db.Save(job);
        }

        private Job getWaitingJob()
        {
            Job job = (Job)db.Read(workerProcess.GetQueueType(), "select top 1 * from " + workerProcess.GetQueueType().Name + " where WorkerId={0} AND Status='New' order by Id", worker.Id);
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
        public abstract string ExecuteJob(Job job, JobData jobData);
        public abstract Database.Database GetNewDatabaseInstance();
        public virtual bool UseJobData
        {
            get { return true; }
        }

        public Action<int> ReportProgress;
        public Action<string> Log;

        public void AddJob(Database.Database db, int workerId, string name, string command, string request, int parentJobId, int jobDefId)
        {
            Job que = new Job();
            que.Command = command;
            que.Name = name.StrCrop(497);
            que.WorkerId = workerId;
            que.ParentJobId = parentJobId;
            que.JobDefinitionId = jobDefId;
            db.Save(que);

            if (this.UseJobData)
            {
                JobData qD = new JobData
                {
                    JobId = que.Id,
                    Request = request
                };
                db.Save(qD);
            }
        }
    }
}
