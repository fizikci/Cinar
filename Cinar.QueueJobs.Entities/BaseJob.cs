using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.QueueJobs.Entities
{
    public class BaseJob : BaseEntity
    {
        public BaseJob()
        {
            InsertDate = DateTime.Now;
        }

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 24)]
        public string Command { get; set; }

        public string Name { get; set; }

        public int WorkerId { get; set; }

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 10)]
        public JobStatuses Status { get; set; }

        public DateTime InsertDate { get; set; }
        public int ProcessTime { get; set; }
    }

    public enum JobStatuses
    {
        New,
        Processing,
        Failed,
        Done
    }
}
