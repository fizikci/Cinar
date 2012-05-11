using Cinar.Database;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select ContentUser.Id as [BaseEntity.Id], Content.Title as [Content.Title], User.Email as [User], ContentUser.Visible as [BaseEntity.Visible] from [ContentUser] left join [Content] ON Content.Id = [ContentUser].ContentId left join [User] ON User.Id = [ContentUser].UserId", QueryOrderBy = "[BaseEntity.Id] desc")]
    public class ContentUser : BaseEntity
    {
        private int contentId;
        [ColumnDetail(IsNotNull = true, References = typeof(Content)), EditFormFieldProps(ControlType = ControlType.LookUp, Options = "readOnly:true")]
        public int ContentId
        {
            get { return contentId; }
            set { contentId = value; }
        }

        private Content _content;
        [XmlIgnore]
        public Content Content
        {
            get
            {
                if (_content == null)
                    _content = (Content)Provider.Database.Read(typeof(Content), this.contentId);
                return _content;
            }
        }

        private int userId;
        [ColumnDetail(IsNotNull = true, References = typeof(User)), EditFormFieldProps(ControlType = ControlType.LookUp, Options = "readOnly:true")]
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private User _user;
        [XmlIgnore]
        public User User
        {
            get
            {
                if (_user == null)
                    _user = (User)Provider.Database.Read(typeof(User), this.userId);
                return _user;
            }
        }
    }

}
