using Cinar.Database;
using System.Xml.Serialization;

//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select ContentPictureLang.Id as [ContentPictureLang.Id], ContentPictureLang.Title as [ContentPictureLang.Title], Lang.Name as [Lang], ContentPictureLang.Visible as [ContentPictureLang.Visible] from [ContentPictureLang] left join [Lang] ON Lang.Id = [ContentPictureLang].LangId")]
    public class ContentPictureLang : BaseEntity
    {
        private int contentPictureId;
        [ColumnDetail(IsNotNull = true, References = typeof(ContentPicture)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int ContentPictureId
        {
            get { return contentPictureId; }
            set { contentPictureId = value; }
        }

        private ContentPicture _contentPicture;
        [XmlIgnore]
        public ContentPicture ContentPicture
        {
            get
            {
                if (_contentPicture == null)
                    _contentPicture = (ContentPicture)Provider.Database.Read(typeof(ContentPicture), this.contentPictureId);
                return _contentPicture;
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

        private string tagData = "";
        [ColumnDetail(ColumnType = DbType.Text)]//, EditFormFieldProps(Options="hidden:true")]
        public string TagData
        {
            get { return tagData; }
            set { tagData = value; }
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
