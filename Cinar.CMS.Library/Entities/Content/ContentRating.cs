using Cinar.Database;
using System.Xml.Serialization;

//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select ContentRating.Id as [ContentRating.Id], Content.Title as [Content.Title] from [ContentRating] left join [Content] ON Content.Id = [ContentRating].ContentId", QueryOrderBy = "ContentRating.Id desc")]
    public class ContentRating : SimpleBaseEntity
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

        [ColumnDetail(IsNotNull = true, DefaultValue = "0"), EditFormFieldProps(Options = "readOnly:true")]
        public int Rating { get; set; }
    }

}
