using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.ComponentModel;

namespace Cinar.Scheduler
{
    public class SchedulerEngine
    {
        private ISynchronizeInvoke formObject;
        private Timer timer;
        private int index;

        public double Interval { get; set; }
        public List<Job> Jobs { get; set; }

        public SchedulerEngine()
        {
            Jobs = new List<Job>();
        }

        public void SetForm(ISynchronizeInvoke form)
        {
            formObject = form;
            if (timer != null)
                timer.SynchronizingObject = formObject;
        }

        public void Start()
        {
            timer = new Timer(Interval);
            if (formObject != null)
                timer.SynchronizingObject = formObject;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        public void Stop()
        {
            if (timer == null) return;
            timer.Enabled = false;
            timer.Elapsed -= timer_Elapsed;
            timer = null;
        }

        public void Clear()
        {
            Jobs.Clear();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (timer == null)
                return;

            timer.Enabled = false;
            try
            {
                //There is no job to execute
                if (Jobs.Count == 0) return;

                //We keep this value to detect infinite loop
                int orjinalValue = index;

                //Find a job apropriate to execute
                do
                {
                    //We reached the end of the job list, start from beginning
                    if (index >= Jobs.Count) index = 0;

                    Job job = Jobs[index];

                    //Flag
                    bool executeJob = false;

                    //Determine if job will be executed
                    switch (job.Type)
                    {
                        case JobType.Once:
                            executeJob = true;
                            break;
                        case JobType.PeriodicMS:
                            TimeSpan sp = DateTime.Now.Subtract(job.LastExecution);
                            if (sp.TotalMilliseconds >= job.Interval) executeJob = true;
                            break;
                        case JobType.PeriodicMinute:
                            TimeSpan sp2 = DateTime.Now.Subtract(job.LastExecution);
                            if (sp2.TotalMinutes >= job.Interval) executeJob = true;
                            break;
                        case JobType.PeriodicHour:
                            TimeSpan sp3 = DateTime.Now.Subtract(job.LastExecution);
                            if (sp3.TotalHours >= job.Interval) executeJob = true;
                            break;
                        case JobType.TimeSpecific:
                            if (job.ExecutionTime > DateTime.Now) executeJob = true;
                            break;
                    }

                    if (executeJob)
                    {
                        execute(job);
                        index++;
                        break;
                    }
                    else
                        index++; //Try next job


                } while (index != orjinalValue);
            }
            catch (Exception exception)
            {
                //todo: log..
            }
            timer.Enabled = true;
        }

        private void execute(Job job)
        {
            try
            {
                System.Threading.Thread.Sleep(job.Delay);
                job.Handler(this);
            }
            catch (Exception exception)
            {
                //TODO: log...
            }

            switch (job.Type)
            {
                case JobType.Once:
                case JobType.TimeSpecific:
                    Jobs.RemoveAt(index);
                    break;
                default:
                    job.LastExecution = DateTime.Now;
                    break;
            }
        }
    }
}
