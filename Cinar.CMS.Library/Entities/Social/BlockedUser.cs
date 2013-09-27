using Cinar.CMS.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select BlockedUser.Id, Blocker.Nick as [Blocker.Nick], Blocked.Nick as [Blocked.Nick] from [BlockedUser] INNER JOIN [User] as Blocker ON Blocker.Id=BlockedUser.InsertUserId INNER JOIN [User] as Blocked ON Blocked.Id=BlockedUser.UserId", QueryOrderBy = "BlockedUser.Id desc")]
    public class BlockedUser : BaseEntity
    {
        public int UserId { get; set; }
    }
}
