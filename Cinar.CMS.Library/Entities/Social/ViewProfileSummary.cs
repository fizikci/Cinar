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
        Id, 
        Nick as NickName, 
        concat(Name,' ',Surname) as FullName, 
        Avatar as UserAvatar,
        About as Summary, 
        Web as Website,
        (select count(Id) from UserContact where InsertUserId=u.Id) as IsFollowing,
        (select count(Id) from UserContact where UserId=u.Id) as IsFollower,
        (select count(Id) from Post where InsertUserId=u.Id) as PaylasimCount,
        (select count(Id) from UserContact where InsertUserId=u.Id) as FollowingCount,
        (select count(Id) from UserContact where UserId=u.Id) as FollowerCount
    from 
	    user as u")]
    public class ViewProfileSummary : DatabaseEntity
    {
        public int Id { get; set; }
        public string NickName {get; set;}
        public string FullName {get; set;}
        public string UserAvatar { get; set; }
        public string Summary {get; set;}
        public string Website {get; set;}
        public bool IsFollowing { get; set; }
        public bool IsFollower { get; set; }
        public bool IsBlocked { get; set; }
        public int PaylasimCount { get; set; }
        public int FollowingCount   { get; set; }
        public int FollowerCount { get; set; }
        public List<ViewMiniUserInfo> FollowersIFollow { get; set; }
    }
}
