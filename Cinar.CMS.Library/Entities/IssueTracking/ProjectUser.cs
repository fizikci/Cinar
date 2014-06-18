using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    public class ProjectUser : BaseEntity
    {
        [ColumnDetail(References = typeof(Project))]
        public int ProjectId { get; set; }

        [ColumnDetail(References = typeof(User))]
        public int UserId { get; set; }

        public Project Project
        {
            get
            {
                return Provider.Database.Read<Project>(ProjectId);
            }
        }

        public User User
        {
            get
            {
                return Provider.Database.Read<User>(UserId);
            }
        }
    }
}
