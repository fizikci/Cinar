using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.Scheduler
{
    public delegate void JobHandler(SchedulerEngine engine);

    public enum JobType
    {
        Once,
        PeriodicMS,
        PeriodicMinute,
        PeriodicHour,
        TimeSpecific
    }

    public class Job
    {
        public JobType Type { get; set; }
        public DateTime ExecutionTime { get; set; }
        public double Interval { get; set; }
        public JobHandler Handler { get; set; }
        /// <summary>
        /// delays for specified miliseconds before the job executed
        /// </summary>
        public int Delay { get; set; }

        internal DateTime LastExecution { get; set; }
    }
}
