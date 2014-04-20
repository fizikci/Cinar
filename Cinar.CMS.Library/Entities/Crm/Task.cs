using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    public class Task : BaseEntity
    {
        public int EventId { get; set; }
        public int ContactId { get; set; }
        public int AssignedToUserId { get; set; }
        public string Details { get; set; }
        public TaskStates TaskState { get; set; }
    }

    public enum TaskStates
    {
        New,
        Resolved,
        Closed
    }
}
