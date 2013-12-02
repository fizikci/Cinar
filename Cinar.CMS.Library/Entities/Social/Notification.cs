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
    public class Notification : BaseEntity
    {
        /// <summary>
        /// Bu notification kime gösterilecek
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Tipi nedir?
        /// </summary>
        [ColumnDetail(ColumnType=DbType.Int16)]
        public NotificationTypes NotificationType { get; set; }

        /// <summary>
        /// Hangi post bu notification'a sebep oldu
        /// </summary>
        public int PostId { get; set; }

        public Post Post {
            get { return Provider.Database.Read<Post>(PostId); }
        }

        protected override void beforeSave(bool isUpdate)
        {
            if (isUpdate)
                return;

            switch (NotificationType)
            {
                case NotificationTypes.Shared:
                    this.Post.OriginalPost.ShareCount++;
                    this.Post.OriginalPost.Save();
                    break;
                case NotificationTypes.Liked:
                    this.Post.LikeCount++;
                    this.Post.Save();
                    break;
                case NotificationTypes.Mention:
                    break;
                case NotificationTypes.Reply:
                    break;
                case NotificationTypes.Followed:
                    break;
                case NotificationTypes.FollowerRequest:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum NotificationTypes : short
    { 
        /// <summary>
        /// Dostum, senin postun paylaşıldı
        /// </summary>
        Shared,
        /// <summary>
        /// Postunu beğendiler
        /// </summary>
        Liked,
        /// <summary>
        /// Biri postunda senden bahsetti
        /// </summary>
        Mention,
        /// <summary>
        /// Biri postuna cevap yazdı
        /// </summary>
        Reply,
        /// <summary>
        /// Biri seni takip etmek istedi.
        /// </summary>
        FollowerRequest,
        /// <summary>
        /// Biri seni takip etti.
        /// </summary>
        Followed
    }
}
