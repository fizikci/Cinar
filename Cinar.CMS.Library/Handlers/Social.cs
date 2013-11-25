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
            get { return true; }
        }

        public override string RequiredRole
        {
            get { return "User"; }
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
                    case "updateProfile":
                        updateProfile();
                        break;
                    case "isNickAvailable":
                        isNickAvailable();
                        break;
                    case "getPostRelatedData":
                        getPostRelatedData();
                        break;
                    case "createPostAd":
                        createPostAd();
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

        private void sendMessage()
        {
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
            Provider.User.Settings.LastNotificationCheck = DateTime.Now;
            Provider.User.Settings.Save();

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void getMessageCount()
        {
            Result res = new Result();
            res.Data = Provider.Database.GetInt("select count(*) from PrivateMessage where InsertDate>{0} AND ToUserId={1}", Provider.User.Settings.LastPrivateMessageCheck, Provider.User.Id);

            context.Response.Write(res.ToJSON());
        }

        private void getLastMessages()
        {
            context.Response.Write(new Result
            {
                Data = Provider.Database.ReadList<ViewPrivateLastMessage>("select * from ViewPrivateLastMessage where MailBoxOwnerId={0}", Provider.User.Id)
            }.ToJSON());
        }

        private void getMessages()
        {
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

        private void setMessageRead() { 
            
        }

        private void reportUser()
        {
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
 /*
        private void sendMessage()
        {
            string toUserNick = context.Request["toUserNick"];

            new PrivateMessage
            {
                ToUserId = Provider.Database.Read<User>("Nick={0}", toUserNick).Id,
                Message = Provider.Request["message"],
            }.Save();

            context.Response.Write(new Result{Data=true}.ToJSON());
        }

        private void getMessageCount()
        {
            Result res = new Result();
            res.Data = Provider.Database.GetInt("select * from PrivateMessage where InsertDate>{0} AND ToUserId={1}", Provider.User.Settings.LastPrivateMessageCheck, Provider.User.Id);

            context.Response.Write(res.ToJSON());
        }

        private void getLastMessages()
        {
            var res = new Result{
                Data = Provider.Database.ReadList<ViewPrivateLastMessage>("select * from ViewPrivateLastMessage where MailBoxOwnerId={0}", Provider.User.Id)
            };
            context.Response.Write(res.ToJSON());
        }

        private void reportUser()
        {
            string nick = context.Request["nick"];
            string reason = context.Request["reason"];
            string reasonText = context.Request["reasonText"];

            new ReportedUser
            {
                UserId = Provider.Database.Read<User>("Nick={0}", nick).Id,
                Reason = reason,
                ReasonText = reasonText
            }.Save();

            context.Response.Write(new Result{Data=true}.ToJSON());
        }
*/

        private void getUserHomePosts()
        {
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
            int userId = Provider.User.Id;

            int limit = 10;
            int.TryParse(context.Request["limit"], out limit);

            var list = SocialAPI.GetUserNotifications(limit);

            context.Response.Write(new Result { Data = list }.ToJSON());
        }

        private void follow()
        {

            SocialAPI.FollowUser(Provider.User.Id, context.Request["user"], null);

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void unfollow()
        {
            User user = Provider.Database.Read<User>("Nick={0}", context.Request["user"]);
            if (user == null)
                throw new Exception("User unknown");

            UserContact uc = Provider.Database.Read<UserContact>("UserId={0} and InsertUserId={1}", user.Id, Provider.User.Id);
            if (uc != null) uc.Delete();

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void unblock()
        {
            User user = Provider.Database.Read<User>("Nick={0}", context.Request["user"]);
            if (user == null)
                throw new Exception("User unknown");

            Provider.Database.ExecuteNonQuery("delete from BlockedUser where UserId={0} and InsertUserId={1}", user.Id, Provider.User.Id);
            Provider.Database.ExecuteNonQuery("delete from SpammerUser where UserId={0} and InsertUserId={1}", user.Id, Provider.User.Id);

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void block()
        {
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
            int lat = 0;
            int.TryParse(context.Request["lat"], out lat);
            int lng = 0;
            int.TryParse(context.Request["lng"], out lng);
            string metin = context.Request["metin"];
            if (string.IsNullOrWhiteSpace(metin))
                throw new Exception("Post empty");
            int replyToPostId = 0;
            int.TryParse(context.Request["replyToPostId"], out replyToPostId);

            context.Response.ContentType = "text/html";

            try
            {
                Post p = new Post
                {
                    LangId = Provider.CurrentLanguage.Id,
                    Lat = lat,
                    Lng = lng,
                    Metin = metin,
                    ReplyToPostId = replyToPostId
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
            int pid = 0;
            int.TryParse(context.Request["pid"], out pid);
            if (pid == 0)
                throw new Exception("Post not found");

            int userId = Provider.Database.Read<Post>(pid).InsertUserId;

            Notification n = Provider.Database.Read<Notification>
                ("UserId = {0} and NotificationType = {1} and PostId = {2} and InsertUserId = {3}", userId, NotificationTypes.Liked, pid, Provider.User.Id);

            if (n == null)
            {
                new Notification
                    {
                        NotificationType = NotificationTypes.Liked,
                        PostId = pid,
                        UserId = userId
                    }.Save();
                context.Response.Write(new Result { Data = true }.ToJSON());
            }
            else
                context.Response.Write(new Result { Data = false }.ToJSON());
        }

        private void share()
        {
            int pid = 0;
            int.TryParse(context.Request["pid"], out pid);
            if (pid == 0)
                throw new Exception("Post not found");

            Post post = Provider.Database.Read<Post>(pid);

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

        private void updateProfile()
        {
            string oldPass = Provider.Request["oldPass"];

            User u = Provider.User;

            u.Save();

            context.Response.Write(new Result { Data = true }.ToJSON());
        }

        private void getPostRelatedData()
        {
            int pid = 0;
            int.TryParse(context.Request["pid"], out pid);
            if (pid == 0)
                throw new Exception("pid expected");

            context.Response.Write(new Result { Data = SocialAPI.GetPostRelatedData(pid) }.ToJSON());
        }

        private void isNickAvailable()
        {

            string nick = Provider.Request["nick"];

            if (!string.IsNullOrWhiteSpace(nick))
                context.Response.Write(new Result { Data = !Provider.Database.GetBool("select count(Nick) from user where Nick = {0}", nick) }.ToJSON());
            else
                context.Response.Write(new Result { Data = null }.ToJSON());

        }

        private void followContacts()
        {
            string[] emails = Provider.Request.Form["emails"].SplitWithTrim(',');
            foreach (string email in emails)
            {
                try
                {
                    SocialAPI.FollowUser(Provider.User.Id, null, email);
                }
                catch { }
            }
            Provider.Response.Write("ok");
        }

        private void inviteContacts()
        {
            string[] emails = Provider.Request.Form["emails"].SplitWithTrim(',');
            foreach (string email in emails)
            {
                try
                {
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
    }

        public class Result
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
    }

}