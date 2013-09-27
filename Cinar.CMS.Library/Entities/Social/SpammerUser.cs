using Cinar.CMS.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select SpammerUser.Id, Spammer.Nick as [Spammer.Nick], Spammed.Nick as [Spammed.Nick] from [SpammerUser] INNER JOIN [User] as Spammer ON Spammer.Id=SpammerUser.UserId INNER JOIN [User] as Spammed ON Spammed.Id=SpammerUser.InsertUserId", QueryOrderBy = "SpammerUser.Id desc")]
    public class SpammerUser : BaseEntity
    {
        public int UserId { get; set; }
    }
}
