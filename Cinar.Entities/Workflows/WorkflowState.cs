using Cinar.Entities.Standart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.Entities.Workflows
{
    public class WorkflowState<T> : NamedEntity where T: struct
    {
        public int WorkflowTypeId { get; set; }
        public T FromState { get; set; }
        public T ToState { get; set; }
        public int RoleId { get; set; }
        public string NotificationMessageTemplate { get; set; }

    }
}
