using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.CMS.Library.Entities
{
    public class JobData : SimpleBaseEntity
    {
        public int JobId { get; set; }

        [ColumnDetail(ColumnType = DbType.TextMedium)]
        public string Request { get; set; }

        [ColumnDetail(ColumnType = DbType.TextMedium)]
        public string Response { get; set; }
    }
}
