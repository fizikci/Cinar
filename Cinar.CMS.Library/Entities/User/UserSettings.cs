using System;
using System.Collections.Generic;
using System.Text;
using Cinar.Database;
using System.Web;
using System.Collections.Specialized;
using System.Drawing;

namespace Cinar.CMS.Library.Entities
{
    public class UserSettings : BaseEntity
    {
        public UserSettings() {
            CoverPicture = "";
            BackgroundPicture = "";
            BackgroundColor = "";
            LinkColor = "";
            BackgroundAlign = "";
            BackgroundLayout = "";
        }

        [ColumnDetail(References=typeof(User), IsUnique=true)]
        public int UserId { get; set; }

        [ColumnDetail(Length = 100)]
        public string CoverPicture { get; set; }

        [ColumnDetail(Length = 100)]
        public string BackgroundPicture { get; set; }

        [ColumnDetail(Length = 20)]
        public string BackgroundColor { get; set; }

        [ColumnDetail(Length = 20)]
        public string LinkColor { get; set; }

        [ColumnDetail(Length = 100)]
        public string BackgroundAlign { get; set; }

        [ColumnDetail(Length = 100)]
        public string BackgroundLayout { get; set; }

        public DateTime LastNotificationCheck { get; set; }

        public DateTime LastPrivateMessageCheck { get; set; }

        public bool NeedsConfirmation { get; set; }

        public bool IsInfoHidden { get; set; }

        public bool MailAfterFollow { get; set; }

        public bool MailAfterMessage { get; set; }

        public bool MailAfterPostReply { get; set; }

        public override void SetFieldsByPostData(NameValueCollection postData)
        {
            base.SetFieldsByPostData(postData);

            string avatarDir = Provider.AppSettings["avatarDir"];
            if (String.IsNullOrEmpty(avatarDir))
                throw new Exception(Provider.GetResource("Avatar folder is not specified in config file."));
            if (!avatarDir.EndsWith("/")) avatarDir += "/";

            HttpPostedFile postedFile = Provider.Request.Files["BackgroundPicture"];
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                string bgUrlPath = avatarDir + Provider.User.Email + "_bg" + System.IO.Path.GetExtension(postedFile.FileName);
                string avatarFilePath = Provider.MapPath(bgUrlPath);
                postedFile.SaveAs(avatarFilePath);
                this.BackgroundPicture = bgUrlPath;

                Provider.DeleteThumbFiles(bgUrlPath);
            }

            postedFile = Provider.Request.Files["CoverPicture"];
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                string cvUrlPath = avatarDir + Provider.User.Email + "_cv" + System.IO.Path.GetExtension(postedFile.FileName);
                string avatarFilePath = Provider.MapPath(cvUrlPath);

                Image bmp = Image.FromStream(postedFile.InputStream);
                if (bmp.Width > 715)
                {
                    Image bmp2 = bmp.ScaleImage(715, 0);
                    bmp2.SaveImage(avatarFilePath, Provider.Configuration.ThumbQuality);
                }
                else
                    postedFile.SaveAs(avatarFilePath);


                this.CoverPicture = cvUrlPath;

                Provider.DeleteThumbFiles(cvUrlPath);
            }
        }
    }
}
