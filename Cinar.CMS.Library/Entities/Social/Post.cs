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

        /// <summary>
        /// Bu post hangi post'a cevap olarak yazıldı?
        /// </summary>
        [ColumnDetail(References = typeof(Post))]
        public int ReplyToPostId { get; set; }

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

            if(!string.IsNullOrWhiteSpace(this.Picture))
                File.Delete(Provider.MapPath(this.Picture));
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

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

            if (!isUpdate)
            {
                // resim gelmişse kaydedelim
                if (Provider.Request.Files["Picture"] != null && Provider.Request.Files["Picture"].ContentLength > 0)
                {
                    string picFileName = Provider.Request.Files["Picture"].FileName;
                    if (!String.IsNullOrEmpty(picFileName))
                    {
                        string imgUrl = Provider.BuildPath("p_" + (DateTime.Now.Millisecond % 1000), "uploadDir", true) + picFileName.Substring(picFileName.LastIndexOf('.'));
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

        protected override void afterSave(bool isUpdate)
        {
            if (!isUpdate) {
                if (this.OriginalPostId > 0)
                {
                    new Notification
                    {
                        NotificationType = NotificationTypes.Shared,
                        PostId = this.Id,
                        UserId = Provider.Database.Read<Post>(this.OriginalPostId).InsertUserId
                    }.Save();
                }
                if (this.ReplyToPostId > 0)
                {
                    new Notification
                    {
                        NotificationType = NotificationTypes.Reply,
                        PostId = this.ReplyToPostId,
                        UserId = Provider.Database.Read<Post>(this.ReplyToPostId).InsertUserId
                    }.Save();
                }

                foreach (Match m in Regex.Matches(this.Metin, @"#([\w\d]+)"))
                {
                    HashTag ht = Provider.Database.Read<HashTag>("Name = {0} AND LangId={1}", m.Value.Substring(1), this.LangId);
                    if (ht == null)
                        ht = new HashTag { Name = m.Value.Substring(1), MentionCount = 1, LangId = this.LangId};
                    else
                        ht.MentionCount++;

                    ht.Save();

                    new PostHashTag { HashTagId = ht.Id, PostId = this.Id }.Save();
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
