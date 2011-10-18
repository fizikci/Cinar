using Cinar.Database;
using System.Xml.Serialization;

//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select ContentTag.Id as [BaseEntity.Id], Content.Title as [Content.Title], Tag.Name as [Tag], ContentTag.Visible as [BaseEntity.Visible] from [ContentTag] left join [Content] ON Content.Id = [ContentTag].ContentId left join [Tag] ON Tag.Id = [ContentTag].TagId", QueryOrderBy = "[BaseEntity.Id] desc")]
    public class ContentTag : BaseEntity
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

        private int tagId;
        [ColumnDetail(IsNotNull = true, References = typeof(Tag)), EditFormFieldProps(ControlType = ControlType.LookUp, Options = "readOnly:true")]
        public int TagId
        {
            get { return tagId; }
            set { tagId = value; }
        }

        private Tag _tag;
        [XmlIgnore]
        public Tag Tag
        {
            get
            {
                if (_tag == null)
                    _tag = (Tag)Provider.Database.Read(typeof(Tag), this.tagId);
                return _tag;
            }
        }


        private int rank = 0;
        [ColumnDetail(IsNotNull = true, DefaultValue = "0"), EditFormFieldProps(Options = "readOnly:true")]
        public int Rank
        {
            get { return rank; }
            set { rank = value; }
        }


        // DİKKAT: bu entity için kasten beforeDelete ve beforeSave
        // ile işlem yapmıyoruz. Çünkü bu işler Content'in Save'inde yapılıyor.
        // TODO: gene de burada bir problem var:
        // eğer bu entity Content üzerinden değil de başka bir yerden insert veya update edilirse data integrity bozulur
    }

}
