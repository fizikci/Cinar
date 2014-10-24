using Cinar.Database;
using Cinar.QueueJobs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.QueueJobs.Test
{
    public class SiteUrlFilter : BaseEntity
    {
        public int JobDefinitionId { get; set; }

        [ColumnDetail(ColumnType = DbType.VarChar, Length=20)]
        public RuleTypes RuleType { get; set; }

        [ColumnDetail(Length = 500)]
        public string Url { get; set; }
        
        public bool Skip { get; set; }
        
        public bool IsIndex { get; set; }

        public string RemoveFromTitle { get; set; }
        public string RemoveFromMetin { get; set; }
    }

    public enum RuleTypes
    {
        StartsWith,
        Contains
    }
}
