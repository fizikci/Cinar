using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.CMS.Library.Entities;

namespace Cinar.CMS.Library.Handlers
{
    public class SocialAPI
    {
        /// <summary>
        /// Kullanıcı bugüne kadar kaç paylaşım yapmış?
        /// </summary>
        public static int GetUserPostCount(int userId)
        {
            return Provider.Database.GetInt("select count(Id) from Post where InsertUserId={0}", userId);
        }

        /// <summary>
        /// Kullanıcı kaç kişiyi takip ediyor?
        /// </summary>
        public static int GetUserFollowingCount(int userId)
        {
            return Provider.Database.GetInt("select count(Id) from UserContact where InsertUserId={0}", userId);
        }

        /// <summary>
        /// Kaç kişi bu kullanıcıyı takip ediyor?
        /// </summary>
        public static int GetUserFollowerCount(int userId)
        {
            return Provider.Database.GetInt("select count(Id) from UserContact where UserId={0}", userId);
        }

        /// <summary>
        /// Kullanıcı login olduğunda açılan ilk sayfada göreceği paylaşımlar
        /// </summary>
        public static List<ViewPost> GetUserHomePosts(int userId, int lessThanId, int greaterThanId, int pageSize)
        {
            string idPart = null;
            if (lessThanId > 0)
                idPart = "p.Id<{1}";
            if (greaterThanId > 0)
                idPart = "p.Id>{1}";
            if (idPart == null)
                throw new Exception("lessThanId or greaterThanId expected");

            List<ViewPost> list = Provider.Database.ReadList<ViewPost>(@"
                SELECT 
                    p.Id,
                    u.Avatar as UserAvatar,
                    u.Nick as UserNick,
                    concat(u.Name,' ',u.Surname) as UserFullName,
                    p.Metin,
                    p.InsertDate,
                    p.ShareCount,
                    p.LikeCount,
                    p.OriginalPostId,
                    p.ReplyToPostId
                FROM 
                    Post p, User u
                WHERE 
                    p.InsertUserId = u.Id AND
                    (p.InsertUserId in (select UserId from UserContact where InsertUserId={0}) OR p.InsertUserId = {0}) AND
                    " + idPart + @" 
                ORDER BY 
                    p.Id DESC
                LIMIT {2}", userId, lessThanId > 0 ? lessThanId : greaterThanId, pageSize);

            foreach (var viewPost in list)
            {
                if (viewPost.OriginalPostId > 0)
                {
                    var post = Provider.Database.Read<Post>(viewPost.OriginalPostId);
                    viewPost.SharerNick = viewPost.UserNick;
                    viewPost.UserAvatar = post.InsertUser.Avatar;
                    viewPost.UserNick = post.InsertUser.Nick;
                    viewPost.UserFullName = post.InsertUser.FullName;
                }
                viewPost.UserAvatar = Provider.GetThumbPath(viewPost.UserAvatar, 48, 48, false);
            }

            return list;
        }

        /// <summary>
        /// Şehir meydanında listelenen paylaşımlar
        /// </summary>
        public static List<ViewPost> GetTownSquarePosts(int userId, int lessThanId, int greaterThanId, int pageSize)
        {
            string idPart = null;
            if (lessThanId > 0)
                idPart = "p.Id<{1}";
            if (greaterThanId > 0)
                idPart = "p.Id>{1}";
            if (idPart == null)
                throw new Exception("lessThanId or greaterThanId expected");

            List<ViewPost> list = Provider.Database.ReadList<ViewPost>(@"
                SELECT 
                    p.Id,
                    u.Avatar as UserAvatar,
                    u.Nick as UserNick,
                    concat(u.Name,' ',u.Surname) as UserFullName,
                    p.Metin,
                    p.InsertDate,
                    p.ShareCount,
                    p.LikeCount,
                    p.OriginalPostId,
                    p.ReplyToPostId
                FROM 
                    Post p
                    inner join User u ON p.InsertUserId = u.Id
                    left join UserSettings us ON u.Id = us.UserId
                WHERE 
                    us.IsInfoHidden <> 1 AND
                    " + idPart + @" 
                ORDER BY 
                    p.Id DESC
                LIMIT {0}", pageSize, lessThanId > 0 ? lessThanId : greaterThanId);

            foreach (var viewPost in list)
            {
                if (viewPost.OriginalPostId > 0)
                {
                    var post = Provider.Database.Read<Post>(viewPost.OriginalPostId);
                    viewPost.SharerNick = viewPost.UserNick;
                    viewPost.UserAvatar = post.InsertUser.Avatar;
                    viewPost.UserNick = post.InsertUser.Nick;
                    viewPost.UserFullName = post.InsertUser.FullName;
                }
                viewPost.UserAvatar = Provider.GetThumbPath(viewPost.UserAvatar, 48, 48, false);
            }

            return list;
        }

        /// <summary>
        /// Tex paylaşım
        /// </summary>
        public static ViewPost GetPost(int postId)
        {
            ViewPost post = Provider.Database.Read<ViewPost>(@"
                SELECT 
                    p.Id,
                    u.Avatar as UserAvatar,
                    u.Nick as UserNick,
                    concat(u.Name,' ',u.Surname) as UserFullName,
                    p.Metin,
                    p.InsertDate,
                    p.ShareCount,
                    p.LikeCount,
                    p.OriginalPostId,
                    p.ReplyToPostId
                FROM 
                    Post p, User u
                WHERE 
                    p.InsertUserId = u.Id AND
                    p.Id = {0}", postId);

            if (post.OriginalPostId > 0)
            {
                var orginalPost = Provider.Database.Read<Post>(post.OriginalPostId);
                post.SharerNick = post.UserNick;
                post.UserAvatar = orginalPost.InsertUser.Avatar;
                post.UserNick = orginalPost.InsertUser.Nick;
                post.UserFullName = orginalPost.InsertUser.FullName;
            }
            post.UserAvatar = Provider.GetThumbPath(post.UserAvatar, 48, 48, false);

            return post;
        }

        /// <summary>
        /// En popüler paylaşımlar
        /// </summary>
        public static List<ViewPost> GetPopularPosts(int lessThanId, int greaterThanId, int pageSize)
        {
            string idPart = null;
            if (lessThanId > 0)
                idPart = "p.Id<{1}";
            if (greaterThanId > 0)
                idPart = "p.Id>{1}";
            if (idPart == null)
                throw new Exception("lessThanId or greaterThanId expected");

            List<ViewPost> list = Provider.Database.ReadList<ViewPost>(@"
                SELECT 
                    p.Id,
                    u.Avatar as UserAvatar,
                    u.Nick as UserNick,
                    concat(u.Name,' ',u.Surname) as UserFullName,
                    p.Metin,
                    p.InsertDate,
                    p.ShareCount,
                    p.LikeCount,
                    p.OriginalPostId,
                    p.ReplyToPostId
                FROM 
                    Post p, User u
                WHERE 
                    p.InsertUserId = u.Id AND
                    " + idPart + @" AND
                    p.InsertDate > DATE_SUB(curdate(), INTERVAL 1 MONTH)
                ORDER BY 
                    (p.ShareCount+1) * (p.ShareCount+1) * (p.LikeCount+1) DESC
                LIMIT {0}", pageSize, lessThanId > 0 ? lessThanId : greaterThanId);

            foreach (var viewPost in list)
            {
                if (viewPost.OriginalPostId > 0)
                {
                    var post = Provider.Database.Read<Post>(viewPost.OriginalPostId);
                    viewPost.SharerNick = viewPost.UserNick;
                    viewPost.UserAvatar = post.InsertUser.Avatar;
                    viewPost.UserNick = post.InsertUser.Nick;
                    viewPost.UserFullName = post.InsertUser.FullName;
                }
                viewPost.UserAvatar = Provider.GetThumbPath(viewPost.UserAvatar, 48, 48, false);
            }

            return list;
        }

        /// <summary>
        /// Arama sonuçları
        /// </summary>
        public static object GetSearchResults(string query, int lessThanId, int greaterThanId, int pageSize)
        {
            string idPart = null;
            if (lessThanId > 0)
                idPart = "p.Id<{1}";
            if (greaterThanId > 0)
                idPart = "p.Id>{1}";
            if (idPart == null)
                throw new Exception("lessThanId or greaterThanId expected");

            if (string.IsNullOrWhiteSpace(query) || query.Length < 3)
                throw new Exception("min 3 characters");

            List<ViewPost> list = Provider.Database.ReadList<ViewPost>(@"
                SELECT 
                    p.Id,
                    u.Avatar as UserAvatar,
                    u.Nick as UserNick,
                    concat(u.Name,' ',u.Surname) as UserFullName,
                    p.Metin,
                    p.InsertDate,
                    p.ShareCount,
                    p.LikeCount,
                    p.OriginalPostId,
                    p.ReplyToPostId
                FROM 
                    Post p, User u
                WHERE 
                    p.InsertUserId = u.Id AND
                    p.Metin like {0} AND
                    " + idPart + @" 
                ORDER BY 
                    p.Id DESC
                LIMIT {2}", "%" + query + "%", lessThanId > 0 ? lessThanId : greaterThanId, pageSize);

            foreach (var viewPost in list)
            {
                if (viewPost.OriginalPostId > 0)
                {
                    var post = Provider.Database.Read<Post>(viewPost.OriginalPostId);
                    viewPost.SharerNick = viewPost.UserNick;
                    viewPost.UserAvatar = post.InsertUser.Avatar;
                    viewPost.UserNick = post.InsertUser.Nick;
                    viewPost.UserFullName = post.InsertUser.FullName;
                }
                viewPost.UserAvatar = Provider.GetThumbPath(viewPost.UserAvatar, 48, 48, false);
            }

            return list;
        }

        /// <summary>
        /// Kullanıcnın adına tıklanınca açılan profil popup'ında gösterilecek bilgiler
        /// </summary>
        public static ViewProfileSummary GetUserProfileSummary(string userNick)
        {
            int userId = Provider.User.Id;
            User user = Provider.Database.Read<User>("Nick={0}", userNick);
            if (user != null)
                userId = user.Id;

            ViewProfileSummary profileSummary = Provider.Database.Read<ViewProfileSummary>(@"
            select 
	            Id, 
	            Nick as NickName, 
	            concat(Name,' ',Surname) as FullName, 
	            Avatar as UserAvatar,
	            About as Summary, 
	            Web as Website,
	            (select count(Id) from UserContact where UserId=u.Id and InsertUserId={1}) as IsFollowing,
	            (select count(Id) from UserContact where UserId={1} and InsertUserId=u.Id) as IsFollower,
                (select count(Id) from BlockedUser where UserId=u.Id and InsertUserId={1}) as IsBlocked,
	            (select count(Id) from Post where InsertUserId=u.Id) as PaylasimCount,
	            (select count(Id) from UserContact where InsertUserId=u.Id) as FollowingCount,
	            (select count(Id) from UserContact where UserId=u.Id) as FollowerCount
            from user as u
            where Id = {0}", userId, Provider.User.Id);

            profileSummary.FollowersIFollow = Provider.Database.ReadList<ViewMiniUserInfo>(@"
            select Nick, concat(Name,' ',Surname) as FullName
		        from UserContact as fu, user as u
		        where u.Id = fu.UserId and fu.InsertUserId = {1} and fu.UserId in
	                (select fu.InsertUserId
		                from UserContact as fu, user u2
		                where u2.Id = fu.UserId and fu.UserId = {0}) limit 4", profileSummary.Id, Provider.User.Id);

            profileSummary.UserAvatar = Provider.GetThumbPath(profileSummary.UserAvatar, 73, 73, false);

            return profileSummary;
        }

        /// <summary>
        /// Bir paylaşımla ilgili diğer bilgiler: RelatedUsers, Replies gibi.
        /// </summary>
        public static ViewPostRelatedData GetPostRelatedData(int pid)
        {
            var res = new ViewPostRelatedData();

            res.RelatedUsers = Provider.Database.ReadList<ViewMiniUserInfo>(@"
                select 
                    * 
                from 
                    ViewMiniUserInfo 
                where 
                    Id in (
                        select 
                            InsertUserId 
                        from 
                            Notification 
                        where 
                            PostId={0} AND 
                            (NotificationType={1} OR NotificationType={2})
                    )",
                pid,
                NotificationTypes.Shared,
                NotificationTypes.Liked);

            foreach (var u in res.RelatedUsers)
                u.Avatar = Provider.GetThumbPath(u.Avatar, 24, 24, false);


            res.Replies = Provider.Database.ReadList<ViewPost>(@"
                SELECT TOP 5
                    p.Id,
                    u.Avatar as UserAvatar,
                    u.Nick as UserNick,
                    concat(u.Name,' ',u.Surname) as UserFullName,
                    p.Metin,
                    p.InsertDate,
                    p.ShareCount,
                    p.LikeCount
                FROM 
                    Post p, User u
                WHERE 
                    p.InsertUserId = u.Id AND
                    p.ReplyToPostId = {0} 
                ORDER BY 
                    p.Id DESC"
                , pid);

            foreach (var viewPost in res.Replies)
                viewPost.UserAvatar = Provider.GetThumbPath(viewPost.UserAvatar, 32, 32, false);

            return res;
        }

        /// <summary>
        /// Kullanıcının kendi paylaşımları. Profil sayfasına girdiği zaman bunları görür
        /// </summary>
        public static List<ViewPost> GetUserProfilePosts(int userId, int lessThanId, int greaterThanId, int pageSize)
        {
            //pageSize = 5; //***

            string idPart = null;
            if (lessThanId > 0)
                idPart = "p.Id<{1}";
            if (greaterThanId > 0)
                idPart = "p.Id>{1}";
            if (idPart == null)
                throw new Exception("lessThanId or greaterThanId expected");

            List<ViewPost> list = Provider.Database.ReadList<ViewPost>(@"
                SELECT 
                    p.Id,
                    u.Avatar as UserAvatar,
                    u.Nick as UserNick,
                    concat(u.Name,' ',u.Surname) as UserFullName,
                    p.Metin,
                    p.InsertDate,
                    p.ShareCount,
                    p.LikeCount,
                    p.OriginalPostId,
                    p.ReplyToPostId
                FROM 
                    Post p, User u
                WHERE 
                    p.InsertUserId = u.Id AND
                    p.InsertUserId = {0} AND
                    " + idPart + @" 
                ORDER BY 
                    p.Id DESC
                LIMIT {2}", userId, lessThanId > 0 ? lessThanId : greaterThanId, pageSize);

            foreach (var viewPost in list)
            {
                if (viewPost.OriginalPostId > 0)
                {
                    var post = Provider.Database.Read<Post>(viewPost.OriginalPostId);
                    viewPost.SharerNick = viewPost.UserNick;
                    viewPost.UserAvatar = post.InsertUser.Avatar;
                    viewPost.UserNick = post.InsertUser.Nick;
                    viewPost.UserFullName = post.InsertUser.FullName;
                }
                viewPost.UserAvatar = Provider.GetThumbPath(viewPost.UserAvatar, 48, 48, false);
            }

            return list;
        }

        /// <summary>
        /// Bu kullanıcıyı takip eden kullanıcıların listesi
        /// </summary>
        public static List<ViewMiniUserInfo> GetUserFollowers(int userId, int lessThanId, int greaterThanId, int pageSize)
        {
            string idPart = null;
            if (lessThanId > 0)
                idPart = "fu.Id<{1}";
            if (greaterThanId > 0)
                idPart = "fu.Id>{1}";
            if (idPart == null)
                throw new Exception("lessThanId or greaterThanId expected");

            List<ViewMiniUserInfo> list = Provider.Database.ReadList<ViewMiniUserInfo>(@"
                SELECT 
                    fu.Id,
                    u.Avatar,
                    u.Nick,
                    concat(u.Name,' ',u.Surname) as FullName,
                    u.About
                FROM 
                    UserContact fu, User u
                WHERE 
                    fu.UserId = {0} AND
                    fu.InsertUserId = u.Id AND
                    " + idPart + @" 
                ORDER BY 
                    fu.Id DESC
                LIMIT {2}", userId, lessThanId > 0 ? lessThanId : greaterThanId, pageSize);

            foreach (var item in list)
                item.Avatar = Provider.GetThumbPath(item.Avatar, 48, 48, false);

            return list;
        }

        /// <summary>
        /// Bu kullanıcının bildirimleri
        /// </summary>
        public static List<ViewNotification> GetUserNotifications(int limit)
        {
            List<ViewNotification> list = Provider.Database.ReadList<ViewNotification>(@"
                SELECT 
                    *
                FROM 
                    ViewNotification
                WHERE 
                    NotifiedUserId = {0}
                Order By InsertDate desc
                LIMIT {1}", Provider.User.Id, limit);

            foreach (ViewNotification v in list)
                v.UserPicture = Provider.GetThumbPath(v.UserPicture, 32, 32, false);

            return list;
        }

        /// <summary>
        /// Bu kullanının takip ettiği kullanıcıların listesi
        /// </summary>
        public static List<ViewMiniUserInfo> GetUserFollowings(int userId, int lessThanId, int greaterThanId, int pageSize)
        {
            string idPart = null;
            if (lessThanId > 0)
                idPart = "fu.Id<{1}";
            if (greaterThanId > 0)
                idPart = "fu.Id>{1}";
            if (idPart == null)
                throw new Exception("lessThanId or greaterThanId expected");

            List<ViewMiniUserInfo> list = Provider.Database.ReadList<ViewMiniUserInfo>(@"
                SELECT 
                    fu.Id,
                    u.Avatar,
                    u.Nick,
                    concat(u.Name,' ',u.Surname) as FullName,
                    u.About
                FROM 
                    UserContact fu, User u
                WHERE 
                    fu.InsertUserId = {0} AND
                    fu.UserId = u.Id AND
                    " + idPart + @" 
                ORDER BY 
                    fu.Id DESC
                LIMIT {2}", userId, lessThanId > 0 ? lessThanId : greaterThanId, pageSize);

            foreach (var item in list)
                item.Avatar = Provider.GetThumbPath(item.Avatar, 48, 48, false);

            return list;
        }

        /// <summary>
        /// Kullanıcının beğendiği paylaşımlar. Profil sayfasında gösterilir.
        /// </summary>
        public static List<ViewPost> GetUserProfileLikes(int userId, int lessThanId, int greaterThanId, int pageSize)
        {
            string idPart = null;
            if (lessThanId > 0)
                idPart = "p.Id<{1}";
            if (greaterThanId > 0)
                idPart = "p.Id>{1}";
            if (idPart == null)
                throw new Exception("lessThanId or greaterThanId expected");

            List<ViewPost> list = Provider.Database.ReadList<ViewPost>(@"
                SELECT 
                    p.Id,
                    u.Avatar as UserAvatar,
                    u.Nick as UserNick,
                    concat(u.Name,' ',u.Surname) as UserFullName,
                    p.Metin,
                    p.InsertDate,
                    p.ShareCount,
                    p.LikeCount,
                    p.OriginalPostId,
                    p.ReplyToPostId
                FROM 
                    Post p, User u, Notification n
                WHERE 
                    p.InsertUserId = u.Id AND
                    p.Id = n.PostId AND
                    n.InsertUserId = {0} AND
                    n.NotificationType = {3} AND
                    " + idPart + @" 
                ORDER BY 
                    p.Id DESC
                LIMIT {2}", userId, lessThanId > 0 ? lessThanId : greaterThanId, pageSize, NotificationTypes.Liked);

            foreach (var viewPost in list)
            {
                if (viewPost.OriginalPostId > 0)
                {
                    var post = Provider.Database.Read<Post>(viewPost.OriginalPostId);
                    viewPost.SharerNick = viewPost.UserNick;
                    viewPost.UserAvatar = post.InsertUser.Avatar;
                    viewPost.UserNick = post.InsertUser.Nick;
                    viewPost.UserFullName = post.InsertUser.FullName;
                }
                viewPost.UserAvatar = Provider.GetThumbPath(viewPost.UserAvatar, 48, 48, false);
            }

            return list;
        }
    }

}
