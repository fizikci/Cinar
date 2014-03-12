using System;
using System.Collections.Generic;
using System.Text;
using Cinar.Database;
using System.Web;
using System.Collections.Specialized;
using System.Drawing;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public class UserSettings : BaseEntity
    {
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

        [ColumnDetail(ColumnType = DbType.VarChar, Length = 16)]
        public AllowedMessageSenders AllowedMessageSenders { get; set; }
    }

    public enum AllowedMessageSenders
    {
        Everybody,
        Nobody,
        Followers
    }
}
