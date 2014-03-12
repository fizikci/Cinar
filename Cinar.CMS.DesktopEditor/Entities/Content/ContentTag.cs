using Cinar.Database;
using System.Xml.Serialization;

//using System.IO;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public class ContentTag : BaseEntity
    {
        private int contentId;
        public int ContentId
        {
            get { return contentId; }
            set { contentId = value; }
        }

        private int tagId;
        public int TagId
        {
            get { return tagId; }
            set { tagId = value; }
        }

        private int rank = 0;
        public int Rank
        {
            get { return rank; }
            set { rank = value; }
        }
    }

}
