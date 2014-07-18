using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.QueueJobs.Entities
{
    public class JobDefinition : BaseEntity
    {
        public string Name { get; set; }
        public string Data { get; set; }
        public string CommandName { get; set; }
        public int RepeatInSeconds { get; set; }
    }
}
