using Cinar.Database;
using System.Xml.Serialization;

//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select EntityAttachmentLang.Id as [EntityAttachmentLang.Id], EntityAttachmentLang.Title as [EntityAttachmentLang.Title], Lang.Name as [Lang], EntityAttachmentLang.Visible as [EntityAttachmentLang.Visible] from [EntityAttachmentLang] left join [Lang] ON Lang.Id = [EntityAttachmentLang].LangId", QueryOrderBy = "EntityAttachmentLang.Id desc")]
    public class EntityAttachmentLang : BaseEntity
    {
        [ColumnDetail(IsNotNull = true, References = typeof(EntityAttachment)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int EntityAttachmentId { get; set; }

        private EntityAttachment _EntityAttachment;
        [XmlIgnore]
        public EntityAttachment EntityAttachment
        {
            get
            {
                if (_EntityAttachment == null)
                    _EntityAttachment = (EntityAttachment)Provider.Database.Read(typeof(EntityAttachment), this.EntityAttachmentId);
                return _EntityAttachment;
            }
        }

        private int langId;
        [ColumnDetail(IsNotNull = true, References = typeof(Lang)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int LangId
        {
            get { return langId; }
            set { langId = value; }
        }
        private Lang _lang;
        [XmlIgnore]
        public Lang Lang
        {
            get
            {
                if (_lang == null)
                    _lang = (Lang)Provider.Database.Read(typeof(Lang), this.langId);
                return _lang;
            }
        }

        private string title;
        [ColumnDetail(IsNotNull = true, Length = 200)]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string description;
        [ColumnDetail(Length = 300)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string fileName;
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        [PictureFieldProps(SpecialFolder = "uploadDir", SpecialNameField = "Title", AddRandomNumber = true, UseYearMonthDayFolders = true)]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public override string GetNameColumn()
        {
            return "Title";
        }
        public override string GetNameValue()
        {
            return this.title;
        }
    }

}
