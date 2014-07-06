using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.QueueJobs.Entities
{
    public class BaseWorker : BaseEntity
    {
        public string Name { get; set; }
        public int Disabled { get; set; }
        public int Modified { get; set; }

        public DateTime ActiveSince { get; set; }
        public DateTime LastExecution { get; set; }
        public string LastExecutionInfo { get; set; }

        public bool IsActive
        {
            get { return Disabled == 0 && LastExecution > DateTime.Now.AddSeconds(-120); }
        }
    }
}
