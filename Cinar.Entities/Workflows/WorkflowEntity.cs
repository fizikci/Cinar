using Cinar.Database;
using Cinar.Entities.Standart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.Entities.Workflows
{
    public class WorkflowEntity<T> : BaseEntity where T : struct
    {
        public int WorkflowTypeId { get; set; }

        [ColumnDetail(ColumnType=DbType.VarChar, Length=100)]
        public T State { get; set; }
    }
}
