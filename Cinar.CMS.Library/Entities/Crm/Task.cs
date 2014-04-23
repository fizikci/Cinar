using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Task.Id as [Task.Id], Task.Name as [NamedEntity.Name], Task.TaskState as [Task.TaskState] from Task", QueryOrderBy = "Task.Id desc")]
    public class Task : NamedEntity
    {
        public int EventId { get; set; }
        public int ContactId { get; set; }
        public int AssignedToUserId { get; set; }
        [ColumnDetail(ColumnType=DbType.Text)]
        public string Details { get; set; }
        [ColumnDetail(ColumnType = DbType.VarChar, Length = 16)]
        public TaskStates TaskState { get; set; }
    }

    public enum TaskStates
    {
        New,
        Resolved,
        Closed
    }
}
