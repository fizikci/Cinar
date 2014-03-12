using Cinar.Database;
using System.Xml.Serialization;
//using System.IO;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public abstract class UserRelatedEntity : NamedEntity
    {
        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

    }
}
