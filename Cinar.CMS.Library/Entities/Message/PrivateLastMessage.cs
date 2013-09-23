using Cinar.Database;
using System;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    public class PrivateLastMessage : BaseEntity
    {
        [ColumnDetail(Length=35)]
        public string Summary { get; set; }

        public int MailBoxOwnerId { get; set; }
        public int UserId { get; set; }

        public override string GetNameColumn()
        {
            return "Summary";
        }
    }

    public class ViewPrivateLastMessage : DatabaseEntity
    {
        public int MailBoxOwnerId { get; set; }
        public string FullName { get; set; }
        public string Nick { get; set; }
        public string Avatar { get; set; }
        public string Summary { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
