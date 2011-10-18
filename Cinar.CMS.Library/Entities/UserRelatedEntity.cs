using Cinar.Database;
using System.Xml.Serialization;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    public abstract class UserRelatedEntity : NamedEntity
    {
        private int userId;
        [ColumnDetail(References = typeof(User)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private User _userId;
        [XmlIgnore]
        public User User
        {
            get
            {
                if (_userId == null)
                    _userId = (User)Provider.Database.Read(typeof(User), this.userId);
                return _userId;
            }
        }

    }
}
