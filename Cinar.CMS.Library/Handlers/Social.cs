using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using System.Data;
using Cinar.CMS.Library.Entities;
using Cinar.CMS.Library.Modules;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Cinar.CMS.Library.Handlers
{
    public class Social : GenericHandler
    {
        public override bool RequiresAuthorization
        {
            get { return false; }
        }

        public override string RequiredRole
        {
            get { return ""; }
        }

        HttpContext context;

        private int loadPostsPageSize = 20;

        public override void ProcessRequest()
        {
            context = HttpContext.Current;
            try
            {
                context.Response.ContentType = "application/json";

                string method = context.Request["method"];

                switch (method)
                {
                    case "sendMessage":
                        sendMessage();
                        break;
                    case "updateLastOpenNotification":
                        updateLastOpenNotification();
                        break;
                    case "confirmFollower":
                        confirmFollower();
                        break;
                    case "deleteUserAvatar":
                        deleteUserAvatar();
                        break;
                    case "deleteUserBackgroundPicture":
                        deleteUserBackgroundPicture();
                        break;
                    case "deleteUserCoverPicture":
                        deleteUserCoverPicture();
                        break;
                    case "updateLastOpenPrivateMessage":
                        updateLastOpenPrivateMessage();
                        break;
                    case "getMessageCount":
                        getMessageCount();
                        break;
                    case "getLastMessages":
                        getLastMessages();
                        break;
                    case "getMessages":
                        getMessages();
                        break;
                    case "setMessageRead":
                        throw new NotImplementedException();
                    case "deleteMessage":
                        throw new NotImplementedException();
                    case "reportUser":
                        reportUser();
                        break;
                    case "getUserHomePosts":
                        getUserHomePosts();
                        break;
                    case "getPost":
                        getPost();
                        break;
                    case "getMaxHashTags":
                        getMaxHashTags();
                        break;
                    case "getCitySquarePosts":
                        getCitySquarePosts();
                        break;
                    case "getPopularPosts":
                        getPopularPosts();
                        break;
                    case "getSearchResults":
                        getSearchResults();
                        break;
                    case "getUserProfileSummary":
                        getUserProfileSummary();
                        break;
                    case "getUserProfilePosts":
                        getUserProfilePosts();
                        break;
                    case "getUserFollowers":
                        getUserFollowers();
                        break;
                    case "getUserFollowings":
                        getUserFollowings();
                        break;
                    case "getUserProfileLikes":
                        getUserProfileLikes();
                        break;
                    case "getUserNotifications":
                        getUserNotifications();
                        break;
                    case "getPrivateMessages":
                        getPrivateMessages();
                        break;
                    case "follow":
                        follow();
                        break;
                    case "unfollow":
                        unfollow();
                        break;
                    case "block":
                        block();
                        break;
                    case "unblock":
                        unblock();
                        break;
                    case "spam":
                        spam();
                        break;
                    case "post":
                        post();
                        break;
                    case "like":
                        like();
                        break;
                    case "share":
                        share();
                        break;
                    case "delete":
                        delete();
                        break;
                    case "updatePassword":
                        updatePassword();
                        break;
                    case "updateEmail":
                        updateEmail();
                        break;
                    case "getPostRelatedData":
                        getPostRelatedData();
                        break;
                    case "followContacts":
                        followContacts();
                        break;
                    case "inviteContacts":
                        inviteContacts();
                        break;
                    case "createPostAd":
                        createPostAd();
                        break;
                    case "searchPeopleAndTopics":
                        searchPeopleAndTopics();
                        break;
                    case "getAllNotifications":
                        getAllNotifications();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(new Result
                {
                    IsError = true,
                    ErrorMessage = ex.Message
                }.ToJSON());
            }
        }

        private void getAllNotifications()
        {
            if(Provider.User.IsAnonim())
                return;

            List<ViewNotification> nots = Provider.Database.ReadList<ViewNotification>(@"
                select 
                    n.UserId AS NotifiedUserId,
                    0 AS New,
                    u.Nick AS UserName,
                    u.Avatar AS UserPicture,
                    n.InsertDate AS InsertDate,
                    n.InsertUserId AS InsertUserId,
                    n.NotificationType AS NotificationType,
                    n.PostId AS PostId 
                from 
                    notification n, user u
                where 
                    u.Id = n.InsertUserId and
                    n.UserId = {0}
                Order By n.InsertDate desc", Provider.User.Id);

            if (nots.Count != 0)
            {
                context.Response.Write(new Result { Data = nots }.ToJSON());
            }
        }

        private void confirmFollower()
        {
            if (Provider.User.IsAnonim())
                return;

            int rId = 0;
            string requesterId = context.Request["rId"];
            if (int.TryParse(requesterId, out rId))
            {
                Notification n = Provider.Database.Read<Notification>(@"select top 1 * from Notification 
                                                                    where InsertUserId = {0} and UserId = {1} 
                                                                    and NotificationType = 4 order by Id desc"
                                                                        , rId, Provider.User.Id);
                if (n != null)
                {
                    new UserContact { UserId = Provider.User.Id, InsertUserId = rId }.Save();
                    context.Response.Write(new Result { Data = true }.ToJSON());
                }
                else
                {
                    context.Response.Write(new Result { IsError = true, ErrorMessage = Provider.TR("Böyle bir talep bulunamadı. Sayfanızı yenileyin.") }.ToJSON());
                }
            }
            else
            {
                context.Response.Write(new Result { IsError = true, ErrorMessage = Provider.TR("Şu anda takip işlemi yapılamıyor. Lütfen daha sonra tekrar deneyin.") }.ToJSON());
            }
        }

        private void deleteUserAvatar()
        {
            if (Provider.User.IsAnonim())
                return;

            Provider.User.Avatar = "";
            Provider.User.Save();
            if (Provider.User.Avatar == "")
            {
                context.Response.Write(new Result { Data = true }.ToJSON());
            }
            else
            {
                context.Response.Write(new Result { IsError = true, ErrorMessage = "Avatar was not deleted." }.ToJSON());
            }
        }
        private void deleteUserBackgroundPicture()
        {
            if (Provider.User.IsAnonim())
                return;

            Provider.User.Settings.BackgroundPicture = "";
            Provider.User.Settings.Save();
            if (Provider.User.Settings.BackgroundPicture == "")
            {
                context.Response.Write(new Result { Data = true }.ToJSON());
            }
            else
            {
                context.Response.Write(new Result { IsError = true, ErrorMessage = "Background picture was not deleted." }.ToJSON());
            }
        }
        private void deleteUserCoverPicture()
        {
            if (Provider.User.IsAnonim())
                return;

            Provider.User.Settings.CoverPicture = "";
            Provider.User.Settings.Save();
            if (Provider.User.Settings.CoverPicture == "")
            {
                context.Response.Write(new Result { Data = true }.ToJSON());
            }
            else
            {
                context.Response.Write(new Result { IsError = true, ErrorMessage = "Cover picture was not deleted." }.ToJSON());
            }
        }

        private void sendMessage()
        {
            if (Provider.User.IsAnonim())
                return;

            string toUserNick = context.Request["toUserNick"];

            new PrivateMessage
            {
                ToUserId = Provider.Database.Read<User>("Nick={0}", toUserNick).Id,
                Message = Provider.Request["message"],
            }.Save();

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void updateLastOpenNotification()
        {
            if (Provider.User.IsAnonim())
                return;

            Provider.User.Settings.LastNotificationCheck = DateTime.Now;
            Provider.User.Settings.Save();

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void getMessageCount()
        {
            if (Provider.User.IsAnonim())
                return;

            Result res = new Result();
            res.Data = Provider.Database.GetInt("select count(*) from PrivateMessage where InsertDate>{0} AND ToUserId={1}", Provider.User.Settings.LastPrivateMessageCheck, Provider.User.Id);

            context.Response.Write(res.ToJSON());
        }

        private void getLastMessages()
        {
            if (Provider.User.IsAnonim())
                return;

            context.Response.Write(new Result
            {
                Data = Provider.Database.ReadList<ViewPrivateLastMessage>(@"
                                                                                select 
                                                                                    plm.MailBoxOwnerId AS MailBoxOwnerId,
                                                                                    plm.UserId AS UserId,
                                                                                    concat(u.Name,' ',u.Surname) AS FullName,
                                                                                    u.Nick AS Nick,
                                                                                    u.Avatar AS Avatar,
                                                                                    plm.Summary AS Summary,
                                                                                    plm.UpdateDate AS UpdateDate 
                                                                                from 
                                                                                    privatelastmessage plm, user u
                                                                                where 
                                                                                    plm.UserId = u.Id and
                                                                                    plm.MailBoxOwnerId={0}", Provider.User.Id)
            }.ToJSON());
        }

        private void getMessages()
        {
            if (Provider.User.IsAnonim())
                return;

            int UserId = 0;
            int.TryParse(context.Request["UserId"], out UserId);

            List<PrivateMessage> pm =
                Provider.Database.ReadList<PrivateMessage>(@"select 
	                                                            Message,
	                                                            ToUserId,
	                                                            [Read],
	                                                            InsertDate,
	                                                            InsertUserId
                                                            from
	                                                            PrivateMessage
                                                            where
	                                                            (ToUserId = {0} AND InsertUserId = {1}) OR
	                                                            (ToUserId = {1} AND InsertUserId = {0})
                                                            order by InsertDate desc", Provider.User.Id, UserId);
            context.Response.Write(new Result { Data = pm }.ToJSON());
        }

        private void updateLastOpenPrivateMessage()
        {
            if (Provider.User.IsAnonim())
                return;

            Provider.User.Settings.LastPrivateMessageCheck = DateTime.Now;
            Provider.User.Settings.Save();

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void reportUser()
        {
            if (Provider.User.IsAnonim())
                return;

            string nick = context.Request["nick"];
            string reason = context.Request["reason"];
            string reasonText = context.Request["reasonText"];

            new ReportedUser
            {
                UserId = Provider.Database.Read<User>("Nick={0}", nick).Id,
                Reason = reason,
                ReasonText = reasonText
            }.Save();

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void getUserHomePosts()
        {
            if (Provider.User.IsAnonim())
                return;

            int lessThanId = 0;
            int.TryParse(context.Request["lessThanId"], out lessThanId);
            int greaterThanId = 0;
            int.TryParse(context.Request["greaterThanId"], out greaterThanId);

            context.Response.Write(new Result { Data = SocialAPI.GetUserHomePosts(Provider.User.Id, lessThanId, greaterThanId, loadPostsPageSize) }.ToJSON());
        }

        private void getMaxHashTags()
        {
            int limit = 10;
            int.TryParse(context.Request["limit"], out limit);

            int langId = 1;
            int.TryParse(context.Request["langId"], out langId);

            string[] names = Provider.Database.ReadList<HashTag>(@"
select 
    Name 
from 
    hashtag 
where
    LangId = {0}
order by 
    MentionCount desc 
limit 
    {1}", langId, limit).OrderByDescending(x => x.MentionCount).Select(x => x.Name).ToArray();

            context.Response.Write(new Result { Data = names }.ToJSON());
        }

        private void getCitySquarePosts()
        {
            int lessThanId = 0;
            int.TryParse(context.Request["lessThanId"], out lessThanId);
            int greaterThanId = 0;
            int.TryParse(context.Request["greaterThanId"], out greaterThanId);

            context.Response.Write(new Result { Data = SocialAPI.GetTownSquarePosts(Provider.User.Id, lessThanId, greaterThanId, loadPostsPageSize) }.ToJSON());
        }

        private void getPost()
        {
            int postId = 0;
            int.TryParse(context.Request["postId"], out postId);

            context.Response.Write(new Result { Data = SocialAPI.GetPost(postId) }.ToJSON());
        }

        private void getPopularPosts()
        {
            int lessThanId = 0;
            int.TryParse(context.Request["lessThanId"], out lessThanId);
            int greaterThanId = 0;
            int.TryParse(context.Request["greaterThanId"], out greaterThanId);

            context.Response.Write(new Result { Data = SocialAPI.GetPopularPosts(lessThanId, greaterThanId, loadPostsPageSize) }.ToJSON());
        }

        private void getSearchResults()
        {
            int lessThanId = 0;
            int.TryParse(context.Request["lessThanId"], out lessThanId);
            int greaterThanId = 0;
            int.TryParse(context.Request["greaterThanId"], out greaterThanId);

            context.Response.Write(new Result { Data = SocialAPI.GetSearchResults(Provider.Request["q"], lessThanId, greaterThanId, loadPostsPageSize) }.ToJSON());
        }

        private void getUserProfileSummary()
        {
            context.Response.Write(new Result { Data = SocialAPI.GetUserProfileSummary(Provider.Request["userNick"]) }.ToJSON());
        }

        private void getUserProfilePosts()
        {
            int lessThanId = 0;
            int.TryParse(context.Request["lessThanId"], out lessThanId);
            int greaterThanId = 0;
            int.TryParse(context.Request["greaterThanId"], out greaterThanId);

            int userId = Provider.User.Id;
            User user = Provider.Database.Read<User>("Nick={0}", context.Request["user"]);
            if (user != null)
                userId = user.Id;

            context.Response.Write(new Result { Data = SocialAPI.GetUserProfilePosts(userId, lessThanId, greaterThanId, loadPostsPageSize) }.ToJSON());
        }

        private void searchPeopleAndTopics()
        {
            int userId = Provider.User.Id;
            var users = Provider.Database.ReadList<ViewMiniUserInfo>(@"SELECT 
                    u.Id,
                    u.Avatar,
                    u.Nick,
                    concat(u.Name,' ',u.Surname) as FullName,
                    u.About
                FROM 
                    UserContact fu, User u
                WHERE 
                    u.Visible = 1 AND
                    fu.UserId = {0} AND
                    fu.InsertUserId = u.Id AND
                    (u.Nick like {1} OR u.Name like {1})
                LIMIT {2}", userId, "%"+Provider.Request["q"]+"%", 4);

            if (users.Count < 4) {
                users.AddRange(Provider.Database.ReadList<ViewMiniUserInfo>(@"SELECT 
                    u.Id,
                    u.Avatar,
                    u.Nick,
                    concat(u.Name,' ',u.Surname) as FullName,
                    u.About
                FROM 
                    User u
                WHERE 
                    (u.Nick like {0} OR u.Name like {0}) AND
                    u.Id not in (1,3) AND
                    u.Visible = 1 AND
                    " + (users.Count > 0 ? "u.Id not in (" + users.Select(u => u.Id).StringJoin(",") + ")" : "1=1") + @"
                LIMIT {1}", "%" + Provider.Request["q"] + "%", 4));
            }

            context.Response.Write(new Result { Data = users }.ToJSON());
        }

        private void getUserFollowers()
        {
            int lessThanId = 0;
            int.TryParse(context.Request["lessThanId"], out lessThanId);
            int greaterThanId = 0;
            int.TryParse(context.Request["greaterThanId"], out greaterThanId);

            int userId = Provider.User.Id;
            User user = Provider.Database.Read<User>("Nick={0}", context.Request["user"]);
            if (user != null)
                userId = user.Id;

            context.Response.Write(new Result { Data = SocialAPI.GetUserFollowers(userId, lessThanId, greaterThanId, loadPostsPageSize) }.ToJSON());
        }

        private void getUserFollowings()
        {
            int lessThanId = 0;
            int.TryParse(context.Request["lessThanId"], out lessThanId);
            int greaterThanId = 0;
            int.TryParse(context.Request["greaterThanId"], out greaterThanId);

            int userId = Provider.User.Id;
            User user = Provider.Database.Read<User>("Nick={0}", context.Request["user"]);
            if (user != null)
                userId = user.Id;

            context.Response.Write(new Result { Data = SocialAPI.GetUserFollowings(userId, lessThanId, greaterThanId, loadPostsPageSize) }.ToJSON());
        }

        private void getUserProfileLikes()
        {
            int lessThanId = 0;
            int.TryParse(context.Request["lessThanId"], out lessThanId);
            int greaterThanId = 0;
            int.TryParse(context.Request["greaterThanId"], out greaterThanId);

            int userId = Provider.User.Id;
            User user = Provider.Database.Read<User>("Nick={0}", context.Request["user"]);
            if (user != null)
                userId = user.Id;

            context.Response.Write(new Result { Data = SocialAPI.GetUserProfileLikes(userId, lessThanId, greaterThanId, loadPostsPageSize) }.ToJSON());
        }

        private void getUserNotifications()
        {
            if (Provider.User.IsAnonim())
                return;

            int limit = 10;
            int.TryParse(context.Request["limit"], out limit);

            var list = SocialAPI.GetUserNotifications(limit);

            context.Response.Write(new Result { Data = list }.ToJSON());
        }

        private void getPrivateMessages()
        {
            if (Provider.User.IsAnonim())
                return;

            int limit = 10;
            int.TryParse(context.Request["limit"], out limit);

            var list = SocialAPI.GetPrivateMessages(limit);

            context.Response.Write(new Result { Data = list }.ToJSON());
        }

        private void follow()
        {
            if (Provider.User.IsAnonim())
                return;

            SocialAPI.FollowUser(Provider.User.Id, context.Request["user"], null);

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void unfollow()
        {
            if (Provider.User.IsAnonim())
                return;

            User user = Provider.Database.Read<User>("Nick={0}", context.Request["user"]);
            if (user == null)
                throw new Exception("User unknown");

            UserContact uc = Provider.Database.Read<UserContact>("UserId={0} and InsertUserId={1}", user.Id, Provider.User.Id);
            if (uc != null) uc.Delete();

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void unblock()
        {
            if (Provider.User.IsAnonim())
                return;

            User user = Provider.Database.Read<User>("Nick={0}", context.Request["user"]);
            if (user == null)
                throw new Exception("User unknown");

            Provider.Database.ExecuteNonQuery("delete from BlockedUser where UserId={0} and InsertUserId={1}", user.Id, Provider.User.Id);
            Provider.Database.ExecuteNonQuery("delete from SpammerUser where UserId={0} and InsertUserId={1}", user.Id, Provider.User.Id);

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void block()
        {
            if (Provider.User.IsAnonim())
                return;

            User user = Provider.Database.Read<User>("Nick={0}", context.Request["user"]);
            if (user == null)
                throw new Exception("User unknown");

            BlockedUser bu = Provider.Database.Read<BlockedUser>("select * from blockeduser where InsertUserId = {0} and UserId = {1}", Provider.User.Id, user.Id);

            if (bu == null)
                bu = new BlockedUser { UserId = user.Id };

            bu.Save();

            UserContact uc = Provider.Database.Read<UserContact>("UserId={0} and InsertUserId={1}", user.Id, Provider.User.Id);
            uc.Delete();

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void spam()
        {
            if (Provider.User.IsAnonim())
                return;

            User user = Provider.Database.Read<User>("Nick={0}", context.Request["user"]);
            if (user == null)
                throw new Exception("User unknown");

            new BlockedUser { UserId = user.Id }.Save();
            new SpammerUser { UserId = user.Id }.Save();
            UserContact uc = Provider.Database.Read<UserContact>("UserId={0} and InsertUserId={1}", user.Id, Provider.User.Id);
            uc.Delete();

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void post()
        {
            if (Provider.User.IsAnonim())
                return;

            int lat = 0;
            int.TryParse(context.Request["lat"], out lat);
            int lng = 0;
            int.TryParse(context.Request["lng"], out lng);
            string metin = context.Request["metin"];
            if (string.IsNullOrWhiteSpace(metin) && (Provider.Request.Files["Picture"] == null || Provider.Request.Files["Picture"].ContentLength == 0))
                throw new Exception("Post empty");
            int replyToPostId = 0;
            int.TryParse(context.Request["replyToPostId"], out replyToPostId);

            Post replyToPost = null; 
            if(replyToPostId>0) replyToPost = Provider.Database.Read<Post>(replyToPostId);

            context.Response.ContentType = "text/html";

            try
            {
                Post p = new Post
                {
                    LangId = Provider.CurrentLanguage.Id,
                    Lat = lat,
                    Lng = lng,
                    Metin = metin==null ? "" : metin,
                    ReplyToPostId = replyToPostId>0 ? (replyToPost.OriginalPost == null ? replyToPostId : replyToPost.OriginalPost.Id) : 0
                };

                p.Save();

                ViewPost vp = new ViewPost()
                    {
                        SharerNick = "",
                        UserAvatar = Provider.GetThumbPath(p.InsertUser.Avatar, 48, 48, false),
                        UserFullName = p.InsertUser.FullName,
                        UserNick = p.InsertUser.Nick                        
                    };
                p.CopyPropertiesWithSameName(vp);

                context.Response.Write("<html><head></head><body><script>window.parent.paylas(" + vp.ToJSON() + ");</script></body></html>");
            }
            catch(Exception ex)
            {
                context.Response.Write("<html><head></head><body><script>window.parent.niceAlert(" + ex.Message.ToJS() + ");</script></body></html>");
            }
        }

        private void like()
        {
            if (Provider.User.IsAnonim())
                return;

            int pid = 0;
            int.TryParse(context.Request["pid"], out pid);
            if (pid == 0)
                throw new Exception("Post not found");

            Post post = Provider.Database.Read<Post>(pid);
            post = post.OriginalPost ?? post;

            Notification n = Provider.Database.Read<Notification>
                ("UserId = {0} and NotificationType = {1} and PostId = {2} and InsertUserId = {3}", post.InsertUserId, NotificationTypes.Liked, post.Id, Provider.User.Id);

            if (n == null)
            {
                new Notification
                    {
                        NotificationType = NotificationTypes.Liked,
                        PostId = post.Id,
                        UserId = post.InsertUserId
                    }.Save();
                context.Response.Write(new Result { Data = true }.ToJSON());
            }
            else
                context.Response.Write(new Result { Data = false }.ToJSON());
        }

        private void share()
        {
            if (Provider.User.IsAnonim())
                return;

            int pid = 0;
            int.TryParse(context.Request["pid"], out pid);
            if (pid == 0)
                throw new Exception("Post not found");

            Post post = Provider.Database.Read<Post>(pid);

            if ((Provider.Database.Read<User>(post.InsertUserId)).Settings.NeedsConfirmation)
                throw new Exception("This user does not let his posts to be shared.");

            Post p = new Post
            {
                LangId = Provider.CurrentLanguage.Id,
                Metin = post.Metin,
                OriginalPostId = post.OriginalPostId > 0 ? post.OriginalPostId : post.Id,
                Picture = post.Picture
            };
            p.Save();

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void delete()
        {
            if (Provider.User.IsAnonim())
                return;

            int pid = 0;
            int.TryParse(context.Request["pid"], out pid);

            if (pid == 0)
                throw new Exception("pid expected");

            Post p = Provider.Database.Read<Post>(pid);

            if (p.InsertUserId != Provider.User.Id)
                throw new Exception("access denied");

            p.Delete();

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void updatePassword()
        {
            if (Provider.User.IsAnonim())
                return;

            //string oldPass = Provider.Request["oldPass"];
            string newPass = Provider.Request["newPass"];
            User u = Provider.User;
            //string currPass = ((string)Provider.Database.GetValue("select Password from user where Nick = {0}", Provider.User.Nick)).Substring(0, 16).ToLower();

            //if (string.IsNullOrWhiteSpace(oldPass) || string.IsNullOrWhiteSpace(newPass))
            //    throw new Exception("password expected");

            //oldPass = Utility.MD5(oldPass).Substring(0, 16);

            //if (oldPass != currPass)
            //    throw new Exception("password didn't match: " + oldPass + " =/= " + currPass);

            u.Password = Utility.MD5(newPass);
            u.Save();

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void getPostRelatedData()
        {
            int pid = 0;
            int limit = 0; 
            int.TryParse(context.Request["pid"], out pid);
            int.TryParse(context.Request["limit"], out limit);
            if (pid == 0)
                throw new Exception("pid expected");

            context.Response.Write(new Result { Data = SocialAPI.GetPostRelatedData(pid, limit) }.ToJSON());
        }

        private void followContacts()
        {
            if (Provider.User.IsAnonim())
                return;

            string[] emails = Provider.Request.Form["emails"].SplitWithTrim('&');
            foreach (string emailParts in emails)
            {
                try
                {
                    var email = emailParts.SplitWithTrim('=')[1].Replace("%40", "@");
                    SocialAPI.FollowUser(Provider.User.Id, null, email);
                }
                catch { }
            }
            Provider.Response.Write("ok");
        }

        private void inviteContacts()
        {
            if (Provider.User.IsAnonim())
                return;

            string[] emails = Provider.Request.Form["emails"].SplitWithTrim('&');
            foreach (string emailParts in emails)
            {
                try
                {
                    var email = emailParts.SplitWithTrim('=')[1].Replace("%40", "@");
                    Provider.SendMail(Provider.User.Email, email, Provider.User.FullName + " isimli kişiden davetiye", string.Format(@"
Merhaba,<br/>
<br/>
Arkadaşın {0} seni bu siteye davet ediyor:<br/>
<br/>
http://{1}
", Provider.User.FullName, Provider.Configuration.SiteAddress));
                }
                catch { }
            }
            Provider.Response.Write("ok");
        }

        private void createPostAd()
        {
            if (Provider.User.IsAnonim())
                return;

            int postId = 0;
            int.TryParse(Provider.Request["postId"], out postId);
            if (postId == 0)
            {
                context.Response.Write(new Result { IsError = true, ErrorMessage = Provider.TR("Paylaşım seçiniz") }.ToJSON());
                return;
            }

            PostAd pa = new PostAd();
            pa.PostId = postId;
            pa.Save();

            context.Response.Write(new Result { Data = pa.Id }.ToJSON());
        }

        private void updateEmail()
        {
            if (Provider.User.IsAnonim())
                return;

            string newEmail = Provider.Request["newEmail"];
            Provider.Log("Notice", "updateEmail", newEmail);

            string msg = String.Format(@"
                                Merhaba {0},<br/><br/>
                                Aşağıdaki linki kullanarak yeni email adresinizi aktif hale getirebilirsiniz:<br/><br/>
                                <a href=""http://{1}/ValidateNewEmail.ashx?keyword={2}"">http://{1}/LoginWithKeyword.ashx?keyword={2}</a>",
                            Provider.User.FullName,
                            Provider.Configuration.SiteAddress,
                            Provider.User.Keyword);
            Provider.SendMail(newEmail, "Yeni email adresinizi onaylayınız", msg);

            context.Response.Write(new Result { Data = true }.ToJSON());
        }
    }

    public class Result
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
    }

}