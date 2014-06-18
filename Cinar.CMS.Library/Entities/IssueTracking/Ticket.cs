using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true)]
    public class Ticket : NamedEntity, ICriticalEntity
    {
        [ColumnDetail(ColumnType = DbType.VarChar, Length = 10)]
        public TicketType Type { get; set; }

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 10)]
        public TicketStatus Status { get; set; }

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 10)]
        public TicketPriority Priority { get; set; }

        [ColumnDetail(References = typeof(Project))]
        public int ProjectId { get; set; }

        [ColumnDetail(Length = 50)]
        public string Component { get; set; }

        [ColumnDetail(References = typeof(User))]
        public int ReportedById { get; set; }
        [ColumnDetail(References = typeof(User))]
        public int AssignedToId { get; set; }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }

        public Ticket()
        {
            Type = TicketType.Bug;
            Status = TicketStatus.New;
            Priority = TicketPriority.Normal;
            ReportedById = Provider.User.Id;
        }

        public string Project { 
            get {
                if (ProjectId <= 0)
                    return "";

                return Provider.Database.Read<Project>(ProjectId).Name;
            }
        }

        public Project GetProject()
        {
            if (ProjectId <= 0)
                return null;

            return Provider.Database.Read<Project>(ProjectId);
        }

        public User ReportedBy
        {
            get
            {
                if (ReportedById > 0)
                    return Provider.Database.Read<User>(ReportedById);

                return new User();
            }
        }

        public User AssignedTo
        {
            get
            {
                if (AssignedToId > 0)
                    return Provider.Database.Read<User>(AssignedToId);

                return new User();
            }
        }
    }

    public enum TicketType
    {
        Bug,
        Task
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
