using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Task.Id as [Task.Id], Task.Name as [NamedEntity.Name], Task.TaskState as [Task.TaskState] from Task", QueryOrderBy = "Task.Id desc")]
    public class Task : NamedEntity, ICriticalEntity
    {
        public int EventId { get; set; }
        public int ContactId { get; set; }
        public int AssignedToUserId { get; set; }
        [ColumnDetail(ColumnType=DbType.Text)]
        public string Details { get; set; }
        [ColumnDetail(ColumnType = DbType.VarChar, Length = 16)]
        public TaskStates TaskState { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public void Start()
        {
            if (this.TaskState == TaskStates.New)
            {
                this.StartDate = DateTime.Now;
                this.TaskState = TaskStates.Started;
                this.Save();
            }
        }

        public void Finish(bool resolved)
        {
            this.EndDate = DateTime.Now;
            if (resolved)
                this.TaskState = TaskStates.Resolved;
            else
                this.TaskState = TaskStates.Closed;
            this.Save();
        }
    }

    public enum TaskStates
    {
        New,
        Started,
        Resolved,
        Closed
    }
}
