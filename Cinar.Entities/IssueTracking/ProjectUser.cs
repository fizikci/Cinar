using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Entities.Standart;
using Cinar.Database;

namespace Cinar.Entities.IssueTracking
{
    public class ProjectUser : BaseEntity
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }

        public Project Project
        {
            get {
                return CinarContext.Db.Read<Project>(ProjectId);
            }
        }
    }
}
