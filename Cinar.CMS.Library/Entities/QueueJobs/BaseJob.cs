using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.CMS.Library.Entities
{
    public class BaseJob : SimpleBaseEntity
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
