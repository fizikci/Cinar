using Cinar.Database;
using System.Xml.Serialization;

//using System.IO;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public class ContentContent : BaseEntity
    {
        private int parentContentId;
        public int ParentContentId
        {
            get { return parentContentId; }
            set { parentContentId = value; }
        }

        private int childContentId;
        public int ChildContentId
        {
            get { return childContentId; }
            set { childContentId = value; }
        }

        public string RelationType { get; set; }
    }

}
