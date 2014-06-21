using Cinar.CMS.Library;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.CMS.Library.Entities
{
    public class GenericNotification : BaseEntity
    {
        /// <summary>
        /// Bu notification kime gösterilecek
        /// </summary>
        public int UserId { get; set; }

        public User User { get { return Provider.Database.Read<User>(UserId); } }

        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public string RelatedEntityName { get; set; }
        public int RelatedEntityId { get; set; }
    }
}
