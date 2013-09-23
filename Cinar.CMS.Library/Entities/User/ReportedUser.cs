using Cinar.Database;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    public class ReportedUser : BaseEntity
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

        [ColumnDetail(Length=10)]
        public string Reason { get; set; }
        
        [ColumnDetail(ColumnType = DbType.Text)]
        public string ReasonText { get; set; }
    }
}
