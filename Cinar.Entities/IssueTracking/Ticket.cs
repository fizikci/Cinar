using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Entities.Standart;
using Cinar.Database;

namespace Cinar.Entities.IssueTracking
{
    public class Ticket : NamedEntity, ICriticalEntity
    {
        [ColumnDetail(ColumnType = DbType.VarChar, Length = 10)]
        public TicketType Type { get; set; }

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 10)]
        public TicketStatus Status { get; set; }

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 10)]
        public TicketPriority Priority { get; set; }

        public int ProjectId { get; set; }

        [ColumnDetail(Length = 50)]
        public string Component { get; set; }

        public int ReportedById { get; set; }
        public int AssignedToId { get; set; }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Description { get; set; }

        public int EstimatedMinutes { get; set; }
        public int RealMinutes { get; set; }

        public Ticket()
        {
            Type = TicketType.Bug;
            Status = TicketStatus.New;
            Priority = TicketPriority.Normal;
            ReportedById = CinarContext.ClientUser.Id;
        }

        public string Project { 
            get {
                if (ProjectId <= 0)
                    return "";

                return CinarContext.Db.Read<Project>(ProjectId).Name;
            }
        }

        public Project GetProject()
        {
            if (ProjectId <= 0)
                return null;

            return CinarContext.Db.Read<Project>(ProjectId);
        }

        public User ReportedBy
        {
            get
            {
                if (ReportedById > 0)
                    return CinarContext.Db.Read<User>(ReportedById);

                return new User();
            }
        }

        public User AssignedTo
        {
            get
            {
                if (AssignedToId > 0)
                    return CinarContext.Db.Read<User>(AssignedToId);

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
        Resolved
    }
    public enum TicketPriority
    {
        Low,
        Normal,
        High
    }
}
