using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.QueueJobs.Entities
{
    public class BaseJobData : BaseEntity
    {
        public int JobId { get; set; }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Request { get; set; }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Response { get; set; }
    }
}
