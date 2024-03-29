﻿using Cinar.CMS.Library;
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

        public override void BeforeSave()
        {
            base.BeforeSave();

            if (Id>0)
                return;

            switch (NotificationType)
            {
                case NotificationTypes.Shared:
                    var sp = this.Post.OriginalPost;
                    sp.ShareCount++;
                    sp.Save();
                    break;
                case NotificationTypes.Liked:
                    var lp = this.Post.OriginalPost ?? this.Post;
                    lp.LikeCount++;
                    lp.Save();
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

        protected override bool beforeDelete()
        {
            var res = base.beforeDelete();

            switch (NotificationType)
            {
                case NotificationTypes.Shared:
                    var sp = this.Post.OriginalPost;
                    sp.ShareCount--;
                    sp.Save();
                    break;
                case NotificationTypes.Liked:
                    var lp = this.Post.OriginalPost ?? this.Post;
                    lp.LikeCount--;
                    lp.Save();
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

            return res;
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
