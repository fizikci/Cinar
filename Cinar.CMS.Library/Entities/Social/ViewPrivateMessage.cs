using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinar.CMS.Library;
using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    [TableDetail(IsView = true, ViewSQL = @"select 
	        (us.LastPrivateMessageCheck < n.InsertDate) AS New,
	        u.Nick AS UserName,
	        u.Avatar AS UserPicture,
	        n.InsertDate AS InsertDate,
	        n.UserId AS UserId,
	        n.Summary AS Message 
        from 
	        PrivateLastMessage n 
	        join User u
	        join UserSettings us
        where 
	        u.Id = n.MailBoxOwnerId and 
	        n.UserId = us.UserId")]
    public class ViewPrivateMessage : DatabaseEntity
    {
        public bool New { get; set; }
        public string UserName { get; set; }
        public string UserPicture { get; set; }
        public DateTime InsertDate { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
    }
}
