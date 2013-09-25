using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinar.CMS.Library;
using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    public class ViewNotification : DatabaseEntity
    {
        public int InsertUserId { get; set; }
        public int NotifiedUserId { get; set; }
        public string UserName { get; set; }
        public string UserPicture { get; set; }
        public DateTime InsertDate { get; set; }
        public NotificationTypes NotificationType { get; set; }
        public int PostId { get; set; }

    }
}
