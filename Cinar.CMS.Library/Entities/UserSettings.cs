using System;
using System.Collections.Generic;
using System.Text;
using Cinar.Database;
using System.Web;
using System.Collections.Specialized;

namespace Cinar.CMS.Library.Entities
{
    public class UserSettings : BaseEntity
    {
        public UserSettings() {
        }

        [ColumnDetail(References=typeof(User), IsUnique=true)]
        public int UserId { get; set; }

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

        public bool NeedsConfirmation { get; set; }

        public bool IsInfoHidden { get; set; }

        public override void SetFieldsByPostData(NameValueCollection postData)
        {
            base.SetFieldsByPostData(postData);

            HttpPostedFile postedFile = Provider.Request.Files["BackgroundPicture"];
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                string avatarDir = Provider.AppSettings["avatarDir"];
                if (String.IsNullOrEmpty(avatarDir))
                    throw new Exception(Provider.GetResource("Avatar folder is not specified in config file."));
                if (!avatarDir.EndsWith("/")) avatarDir += "/";
                string avatarUrlPath = avatarDir + Provider.User.Email + "_bg" + System.IO.Path.GetExtension(postedFile.FileName);
                string avatarFilePath = Provider.MapPath(avatarUrlPath);
                postedFile.SaveAs(avatarFilePath);
                this.BackgroundPicture = avatarUrlPath;
            }
        }
    }
}
