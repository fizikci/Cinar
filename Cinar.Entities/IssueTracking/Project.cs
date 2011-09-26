using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Entities.Standart;
using Cinar.Database;

namespace Cinar.Entities.IssueTracking
{
    public class Project : NamedEntity
    {
        public List<User> GetTeamMembers()
        {
            return CinarContext.Db.ReadList<User>("SELECT Id, Name FROM User WHERE Id in (SELECT UserId FROM ProjectUser WHERE ProjectId = {0}) ORDER BY Name", Id);
        }
    }
}
