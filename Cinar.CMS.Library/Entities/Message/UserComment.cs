using Cinar.Database;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select UserComment.Id as [BaseEntity.Id], Content.Title as [Content], UserComment.Nick as [UserComment.Nick], UserComment.Title as [UserComment.Title], UserComment.Visible as [BaseEntity.Visible] from UserComment left join Content on Content.Id = UserComment.ContentId", QueryOrderBy = "[BaseEntity.Id] desc")]
    public class UserComment : BaseEntity
    {
        private int contentId;
        [ColumnDetail(IsNotNull = true, References = typeof(Content)), EditFormFieldProps(ControlType = ControlType.LookUp)]
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

        private string nick;
        [ColumnDetail(IsNotNull = true, Length = 100)]
        public string Nick
        {
            get { return nick; }
            set { nick = value; }
        }

        private string web;
        [ColumnDetail(Length = 100), EditFormFieldProps(Options = @"regEx:'(((ht|f)tp(s?):\/\/)|(www\.[^ \[\]\(\)\n\r\t]+)|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})\/)([^ \[\]\(\),;&quot;\'&lt;&gt;\n\r\t]+)([^\. \[\]\(\),;&quot;\'&lt;&gt;\n\r\t])|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})'")]
        public string Web
        {
            get { return web; }
            set { web = value; }
        }

        private string email;
        [ColumnDetail(Length = 100), EditFormFieldProps(Options = @"regEx:'^[\w-]+@([\w-]+\.)+[\w-]+$'")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string ip;
        [ColumnDetail(IsNotNull = true, Length = 50)]
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        private string title;
        [ColumnDetail(IsNotNull = true, Length = 200)]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string commentText;
        [ColumnDetail(IsNotNull = true, ColumnType = DbType.Text)]
        public string CommentText
        {
            get { return commentText; }
            set { commentText = value; }
        }

        private int parentId = 0;
        [ColumnDetail(DefaultValue = "0", References = typeof(UserComment))]
        [EditFormFieldProps(Visible = false)]
        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }

        private UserComment _parent;
        [XmlIgnore]
        public UserComment Parent
        {
            get
            {
                if (_parent == null)
                    _parent = (UserComment)Provider.Database.Read(typeof(UserComment), this.parentId);
                return _parent;
            }
        }

        private int responseCount = 0;
        [ColumnDetail(IsNotNull = true, DefaultValue = "0")]
        [EditFormFieldProps(Visible = false)]
        public int ResponseCount
        {
            get { return responseCount; }
            set { responseCount = value; }
        }

        protected override void afterSave(bool isUpdate)
        {
            base.afterSave(isUpdate);

            // eğer parent'ı varsa parent'ın responseCount'unu arttıralım
            if (this.ParentId > 0)
                Provider.Database.ExecuteNonQuery("update UserComment set ResponseCount = {1} where Id={0}", 
                    this.parentId, 
                    Provider.Database.GetValue("select count(*) from UserComment where ParentId={0} and Visible=1", this.ParentId));

            Provider.Database.ExecuteNonQuery("update Content set CommentCount = {1} where Id={0}", 
                this.contentId, 
                Provider.Database.GetValue("select count(*) from UserComment where ContentId={0} and Visible=1", this.contentId));
        }

        public override string GetNameColumn()
        {
            return "Title";
        }
    }

}
