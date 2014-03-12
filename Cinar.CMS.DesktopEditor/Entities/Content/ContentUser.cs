using Cinar.Database;
using System.Xml.Serialization;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public class ContentUser : BaseEntity
    {
        private int contentId;
        public int ContentId
        {
            get { return contentId; }
            set { contentId = value; }
        }

        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        [ColumnDetail(Length = 300)]
        public string Category
        {
            get;
            set;
        }

        public bool IsOwner { get; set; }

    }

}
