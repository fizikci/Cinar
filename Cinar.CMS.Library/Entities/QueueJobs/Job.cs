using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.CMS.Library.Entities
{
    public class Job : SimpleBaseEntity
    {
        public Job()
        {
            InsertDate = DateTime.Now;
        }

        public int JobDefinitionId { get; set; }

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 24)]
        public string Command { get; set; }

        [ColumnDetail(Length = 500)]
        public string Name { get; set; }

        public int WorkerId { get; set; }
        public int ParentJobId { get; set; }

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 10)]
        public JobStatuses Status { get; set; }

        public DateTime InsertDate { get; set; }
        public int ProcessTime { get; set; }

        public int ResLength { get; set; }
    }

    public enum JobStatuses
    {
        New,
        Processing,
        Failed,
        Done
    }
}
