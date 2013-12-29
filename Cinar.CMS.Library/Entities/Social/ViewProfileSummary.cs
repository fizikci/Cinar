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
    [TableDetail(IsView = true, ViewSQL = @"select 
	    u.Id, 
	    u.Nick as NickName, 
	    concat(u.Name,' ',u.Surname) as FullName, 
	    u.Avatar as UserAvatar,
	    u.About as Summary, 
	    u.Web as Website,
        us.CoverPicture,
        us.AllowedMessageSenders,
        (select count(Id) from UserContact where InsertUserId=u.Id) as IsFollowing,
        (select count(Id) from UserContact where InsertUserId=u.Id) as IsFollowing,
        (select count(Id) from BlockedUser where InsertUserId=u.Id) as IsBlocked,
        (select count(Id) from UserContact where UserId=u.Id) as IsFollower,
        (select count(Id) from Post where InsertUserId=u.Id) as PaylasimCount,
        (select count(Id) from UserContact where InsertUserId=u.Id) as FollowingCount,
        (select count(Id) from UserContact where UserId=u.Id) as FollowerCount
    from 
	    user as u left join usersettings us on us.UserId=u.Id")]
    public class ViewProfileSummary : DatabaseEntity
    {
        public int Id { get; set; }
        public string NickName {get; set;}
        public string FullName {get; set;}
        public string UserAvatar { get; set; }
        public string Summary {get; set;}
        public string Website {get; set;}
        public string CoverPicture { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsFollower { get; set; }
        public bool IsBlocked { get; set; }
        public int PaylasimCount { get; set; }
        public int FollowingCount   { get; set; }
        public int FollowerCount { get; set; }
        public List<ViewMiniUserInfo> FollowersIFollow { get; set; }
        public string AllowedMessageSenders { get; set; }
    }
}
