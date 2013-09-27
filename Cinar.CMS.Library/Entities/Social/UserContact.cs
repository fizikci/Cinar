using Cinar.Database;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    public class UserContact : BaseEntity
    {
        private int userId;
        [ColumnDetail(References = typeof(User)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        [XmlIgnore]
        public User User
        {
            get
            {
                return (User)Provider.Database.Read(typeof(User), this.userId);
            }
        }

    }
}
