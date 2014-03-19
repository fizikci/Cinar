using Cinar.CMS.Library;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cinar.CMS.Library.Entities
{
    public class Post : BaseEntity
    {
        [ColumnDetail(Length=200)]
        public string Metin { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string Picture { get; set; }

        /// <summary>
        /// Eğer bu post paylaşarak oluşturulmuş ise, paylaşılan post'un idsi.
        /// </summary>
        [ColumnDetail(References = typeof(Post))]
        public int OriginalPostId { get; set; }

        public Post OriginalPost
        {
            get { return Provider.Database.Read<Post>(OriginalPostId); }
        }

        /// <summary>
        /// Bu post hangi post'a cevap olarak yazıldı?
        /// </summary>
        [ColumnDetail(References = typeof(Post))]
        public int ReplyToPostId { get; set; }

        public Post ReplyToPost {
            get {
                return Provider.Database.Read<Post>(this.ReplyToPostId);
            }
        }

        [ColumnDetail(References = typeof(Post))]
        public int ThreadId { get; set; }

        public Post ThreadPost
        {
            get
            {
                return Provider.Database.Read<Post>(this.ThreadId);
            }
        }

        [ColumnDetail(References=typeof(Lang))]
        public int LangId { get; set; }

        public int Lat { get; set; }
        public int Lng { get; set; }
        public string VideoId { get; set; }
        public VideoTypes VideoType { get; set; }

        /// <summary>
        /// Kaç kişi bunu paylaştı
        /// </summary>
        public int ShareCount { get; set; }
        
        /// <summary>
        /// Kaç kişi beğendi
        /// </summary>
        public int LikeCount { get; set; }

        protected override bool beforeDelete()
        {
            if (Provider.Database.GetInt("select count(*) from PostAd where PostId={0}", this.Id) > 0)
                throw new Exception(Provider.TR("Reklamı olan paylaşım silinemez."));

            Provider.Database.ExecuteNonQuery("delete from Notification where PostId={0}", this.Id);
            foreach (var post in Provider.Database.ReadList<Post>("select * from Post where OriginalPostId={0}", this.Id))
                post.Delete();
            foreach (var post in Provider.Database.ReadList<Post>("select * from Post where ReplyToPostId={0}", this.Id))
                post.Delete();
            Provider.Database.ExecuteNonQuery("delete from PostHashTag where PostId={0}", this.Id);
            return true;
        }

        protected override void afterDelete()
        {
            base.afterDelete();

            // paylaşıma ait resmi silelim
            if(!string.IsNullOrWhiteSpace(this.Picture) && this.OriginalPostId==0)
                File.Delete(Provider.MapPath(this.Picture));

            // bu paylaşıma yazılan cevapları silelim
            foreach (Post p in Provider.Database.ReadList<Post>("select * from Post where ReplyToPostId={0}", this.Id))
                p.Delete();
        }

        public override void BeforeSave()
        {
            base.BeforeSave();

            if (Id==0) { 
                // aynı paylaşımı yapıyorsa kaydetmeyelim
                var lastPost = Provider.Database.Read<Post>("select top 1 * from Post where InsertUserId={0} order by Id desc", Provider.User.Id) ?? new Post() { Metin = ""};
                if ((Provider.Request.Files["Picture"] == null || Provider.Request.Files["Picture"].ContentLength == 0) && lastPost.Metin.Trim() == this.Metin.Trim())
                    throw new Exception(Provider.TR("Bunu daha önce zaten paylaşmıştınız"));

                if (lastPost.InsertDate.AddSeconds(5) > DateTime.Now)
                    throw new Exception(Provider.TR("5 saniye içinde iki paylaşım yapılamaz"));

                if (this.ReplyToPostId > 0)
                    this.ThreadId = this.ReplyToPost.ThreadId > 0 ? this.ReplyToPost.ThreadId : this.ReplyToPost.Id;
            }

            try
            {
                var urls = new Regex(@"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)");

                foreach (Match m in urls.Matches(this.Metin))
                {
                    if (m.Value.Contains("youtube.com"))
                    {
                        string video_id = m.Value.SplitWithTrim("v=")[1];
                        var ampersandPosition = video_id.IndexOf('&');
                        if (ampersandPosition != -1)
                        {
                            video_id = video_id.Substring(0, ampersandPosition);
                        }
                        this.VideoId = video_id;
                        this.VideoType = VideoTypes.Youtube;
                    }
                    if (m.Value.Contains("vimeo.com"))
                    {
                        string video_id = m.Value.SplitAndGetLast('/');
                        this.VideoId = video_id;
                        this.VideoType = VideoTypes.Vimeo;
                    }
                    if (m.Value.Contains("dailymotion.com"))
                    {
                        string video_id = m.Value.SplitAndGetLast('/').Split('_')[0];
                        this.VideoId = video_id;
                        this.VideoType = VideoTypes.DailyMotion;
                    }
                }
            }
            catch { } // video şart değil, hata olursa es geç.

            if (Id==0)
            {
                // resim gelmişse kaydedelim
                if (Provider.Request.Files["Picture"] != null && Provider.Request.Files["Picture"].ContentLength > 0)
                {
                    string picFileName = Provider.Request.Files["Picture"].FileName;
                    if (!String.IsNullOrEmpty(picFileName) && (".jpg.png.gif.jpeg".Contains(picFileName.Substring(picFileName.LastIndexOf('.')).ToLowerInvariant())))
                    {
                        string imgUrl = Provider.BuildPath("p_" + (new Random().Next(1000000)+1), "uploadDir", true) + picFileName.Substring(picFileName.LastIndexOf('.'));
                        Image bmp = Image.FromStream(Provider.Request.Files["Picture"].InputStream);
                        if (bmp.Width > Provider.Configuration.ImageUploadMaxWidth)
                        {
                            Image bmp2 = bmp.ScaleImage(Provider.Configuration.ImageUploadMaxWidth, 0);
                            bmp2.SaveJpeg(Provider.MapPath(imgUrl), Provider.Configuration.ThumbQuality);
                        }
                        else
                            Provider.Request.Files["Picture"].SaveAs(Provider.MapPath(imgUrl));
                        this.Picture = imgUrl;

                        Provider.DeleteThumbFiles(imgUrl);
                    }
                }
            }
        }

        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            if (!isUpdate)
            {
                if (this.OriginalPostId > 0)
                {
                    new Notification
                    {
                        NotificationType = NotificationTypes.Shared,
                        PostId = this.Id,
                        UserId = this.OriginalPost.InsertUserId
                    }.Save();
                }
                if (this.ReplyToPostId > 0 && this.ReplyToPost.InsertUserId != Provider.User.Id) // eğer kendi paylaşımı dışında bir paylaşıma cevap ise, o paylaşımı yazan kişiye haber ver
                {
                    new Notification
                    {
                        NotificationType = NotificationTypes.Reply,
                        PostId = this.ReplyToPostId,
                        UserId = this.ReplyToPost.InsertUserId
                    }.Save();

                    if (Provider.User.Settings.MailAfterPostReply)
                    {
                        string msg = String.Format(@"
                                Merhaba {0},<br/><br/>
                                {1} paylaşımına cevap yazdı:<br/><br/>
                                <i>{2}</i><br/><br/>
                                <a href=""http://{3}"">http://{3}</a>",
                                    this.ReplyToPost.InsertUser.FullName,
                                    Provider.User.FullName,
                                    this.Metin,
                                    Provider.Configuration.SiteAddress);
                        Provider.SendMail(this.ReplyToPost.InsertUser.Email, Provider.User.FullName + " paylaşımına cevap yazdı", msg);
                    }
                }

                // mention
                List<string> mentions = new List<string>();
                foreach (Match m in Regex.Matches(this.Metin, @"@([\w\d]+)"))
                {
                    string nick = m.Value.Substring(1);
                    if (mentions.Contains(nick))
                        continue; //***
                    mentions.Add(nick);

                    User u = Provider.Database.Read<User>("Nick = {0}", nick);
                    if (u != null)
                    {
                        // kendi nickini yazmışsa bildirim gönderme
                        if (u.Id == Provider.User.Id)
                            continue;

                        // cevap verilen paylaşımı yazan kişinin nicki ise mention bildirimi gönderme, çünkü zaten reply bildirimi gönderiliyor
                        if (this.ReplyToPost != null && this.ReplyToPost.InsertUserId == u.Id)
                            continue;

                        new Notification
                        {
                            NotificationType = NotificationTypes.Mention,
                            PostId = this.Id,
                            UserId = u.Id
                        }.Save();
                    }
                }

                // HashTags
                foreach (Match m in Regex.Matches(this.Metin, @"#([\w\d]+)"))
                {
                    HashTag ht = Provider.Database.Read<HashTag>("Name = {0} AND LangId={1}", m.Value.Substring(1), this.LangId);
                    if (ht == null)
                        ht = new HashTag { Name = m.Value.Substring(1), MentionCount = 1, LangId = this.LangId };
                    else
                        ht.MentionCount++;

                    ht.Save();

                    new PostHashTag { HashTagId = ht.Id, PostId = this.Id }.Save();
                }

                // Blacklist
                foreach (var badWord in Provider.Database.ReadList<WordBlacklist>())
                    if (this.Metin.Contains(badWord.Name))
                    {
                        new PostBlacklist { PostId = this.Id, WordBlacklistId = badWord.Id }.Save();
                    }

            }
        }
    }

    public enum VideoTypes
    {
        None,
        Youtube,
        Vimeo,
        DailyMotion
    }
}
