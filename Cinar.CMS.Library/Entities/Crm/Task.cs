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
        [ColumnDetail(References = typeof(Event))]
        public int EventId { get; set; }

        [ColumnDetail(References = typeof(Contact))]
        public int ContactId { get; set; }

        [ColumnDetail(References = typeof(User))]
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

        public void Finish(bool completed)
        {
            this.EndDate = DateTime.Now;
            if (completed)
                this.TaskState = TaskStates.Completed;
            else
                this.TaskState = TaskStates.Closed;
            this.Save();
        }
    }

    public enum TaskStates
    {
        New,
        Started,
        Completed,
        Closed
    }
}
