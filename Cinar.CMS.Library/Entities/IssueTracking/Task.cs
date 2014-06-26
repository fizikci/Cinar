using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true)]
    public class Task : NamedEntity, ICriticalEntity
    {
        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=TaskType'")]
        public int TypeId { get; set; }

        [EditFormFieldProps(Category = "Status"), ColumnDetail(ColumnType = DbType.VarChar, Length = 10)]
        public TicketStatus Status { get; set; }

        [EditFormFieldProps(Category = "Status"), ColumnDetail(ColumnType = DbType.VarChar, Length = 10)]
        public TicketPriority Priority { get; set; }

        [EditFormFieldProps(Category = "Details"), ColumnDetail(References = typeof(Project))]
        public int ProjectId { get; set; }

        [EditFormFieldProps(Category = "Details"), ColumnDetail(Length = 50)]
        public string Component { get; set; }

        [EditFormFieldProps(Category = "Details"), ColumnDetail(References = typeof(User))]
        public int ReportedById { get; set; }
        [EditFormFieldProps(Category = "Details"), ColumnDetail(References = typeof(User))]
        public int AssignedToId { get; set; }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }

        public Task()
        {
            Status = TicketStatus.New;
            Priority = TicketPriority.Normal;
            ReportedById = Provider.User.Id;

            StartDate = DateTime.Now.Date;
            DueDate = DateTime.Now.AddDays(3).Date;
        }

        public Project Project { 
            get {
                return Provider.Database.Read<Project>(ProjectId);
            }
        }

        public User ReportedBy
        {
            get
            {
                return Provider.Database.Read<User>(ReportedById);
            }
        }

        public User AssignedTo
        {
            get
            {
                return Provider.Database.Read<User>(AssignedToId);
            }
        }

        public override void BeforeSave()
        {
            base.BeforeSave();

            if (this.StartDate == this.DueDate)
                this.DueDate = this.DueDate.AddHours(23).AddMinutes(59);
        }

        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            if (Provider.User.Id != this.AssignedToId)
            {
                new GenericNotification
                {
                    EntityName = "Task",
                    EntityId = this.Id,
                    RelatedEntityName = "Task",
                    RelatedEntityId = this.Id,
                    UserId = this.AssignedToId
                }.Save();
            }

            if (Provider.User.Id != this.Project.ManagerId && this.AssignedToId != this.Project.ManagerId)
            {
                new GenericNotification
                {
                    EntityName = "Task",
                    EntityId = this.Id,
                    RelatedEntityName = "Task",
                    RelatedEntityId = this.Id,
                    UserId = this.Project.ManagerId
                }.Save();
            }
        }
    }

    public enum TicketStatus
    {
        New,
        Accepted,
        Rejected,
        Completed
    }
    public enum TicketPriority
    {
        Low,
        Normal,
        High
    }
}
