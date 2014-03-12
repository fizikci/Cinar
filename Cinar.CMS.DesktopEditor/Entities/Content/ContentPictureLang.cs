using Cinar.Database;
using System.Xml.Serialization;

//using System.IO;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public class ContentPictureLang : BaseEntity
    {
        private int contentPictureId;
        public int ContentPictureId
        {
            get { return contentPictureId; }
            set { contentPictureId = value; }
        }

        private int langId;
        public int LangId
        {
            get { return langId; }
            set { langId = value; }
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
    }

}
