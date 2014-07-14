using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinar.CMS.Library;
using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    [TableDetail(IsView = true, ViewSQL = @"
                SELECT
                    p.Id,
                    u.Avatar as UserAvatar,
                    u.Nick as UserNick,
                    concat(u.Name,' ',u.Surname) as UserFullName,
                    p.Metin,
                    p.Picture,
                    p.VideoId,
                    p.VideoType,
                    p.InsertDate,
                    p.ShareCount,
                    p.LikeCount,
                    p.OriginalPostId,
                    p.ReplyToPostId
                FROM 
                    Post p, User u
                WHERE 
                    p.InsertUserId = u.Id
                ORDER BY 
                    p.Id DESC")]
    public class ViewPost : DatabaseEntity
    {
        public int Id { get; set; }
        public string UserAvatar { get; set; }
        public string UserNick { get; set; }
        public string UserFullName { get; set; }
        public string Metin { get; set; }
        public string Picture { get; set; }
        public string VideoId { get; set; }
        public VideoTypes VideoType { get; set; }
        public DateTime InsertDate { get; set; }

        public int ShareCount { get; set; }
        public int LikeCount { get; set; }

        public int OriginalPostId { get; set; }
        public int ReplyToPostId { get; set; }
        public string SharerNick { get; set; }

        public int UserLikedThis { get; set; }

        public User Originator
        {
            get { return OriginalPostId > 0 ? Provider.Database.Read<Post>(OriginalPostId).InsertUser : Provider.Database.Read<Post>(Id).InsertUser; }
        }
    }
}
