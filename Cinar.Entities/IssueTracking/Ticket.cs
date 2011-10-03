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
        [ColumnDetail(Length = 10)]
        public string Type { get; set; } // Bug, Task

        [ColumnDetail(Length = 10)]
        public string Status { get; set; } // New, Accepted, Rejected, Resolved

        [ColumnDetail(Length = 10)]
        public string Priority { get; set; } // Low, Normal, High

        public int ProjectId { get; set; }

        [ColumnDetail(Length = 50)]
        public string Component { get; set; }

        public int ReportedById { get; set; }
        public int AssignedToId { get; set; }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Description { get; set; }

        public Ticket()
        {
            Type = "Bug";
            Status = "New";
            Priority = "Normal";
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

        public string CreatedOn
        {
            get {
                return InsertDate.ToString("dd MMMM yyyy");
            }
        }
    }
}
