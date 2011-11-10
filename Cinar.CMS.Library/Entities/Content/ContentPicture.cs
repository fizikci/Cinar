using System;
using Cinar.Database;
using System.Xml.Serialization;

//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select ContentPicture.Id, ContentPicture.FileName as [ContentPicture.FileName], ContentPicture.Visible as [BaseEntity.Visible] from [ContentPicture] left join [Content] as TContentId ON TContentId.Id = [ContentPicture].ContentId")]
    [EditFormDetails(DetailType = typeof(ContentPictureLang), RelatedFieldName = "ContentPictureId")]
    public class ContentPicture : BaseEntity
    {
        private int contentId;
        [ColumnDetail(References = typeof(Content)), EditFormFieldProps(ControlType = ControlType.LookUp)]
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

        private string title = "";
        [ColumnDetail(Length = 200)]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string description = "";
        [ColumnDetail(Length = 300)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string tagData = "";
        [ColumnDetail(ColumnType = DbType.Text)]//, EditFormFieldProps(Options="hidden:true")]
        public string TagData
        {
            get { return tagData; }
            set { tagData = value; }
        }

        private int like = 0;
        public int Like
        {
            get { return like; }
            set { like = value; }
        }

        private string fileName;
        [ColumnDetail(IsNotNull=true, Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
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

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            // resim gelmişse kaydedelim
            if (Provider.Request.Files["FileNameFile"] != null && Provider.Request.Files["FileNameFile"].ContentLength > 0)
            {
                string picFileName = Provider.Request.Files["FileNameFile"].FileName;
                if (!String.IsNullOrEmpty(picFileName))
                {
                    string imgUrl = Provider.AppSettings["uploadDir"] + "/" + System.IO.Path.GetFileName(picFileName);
                    Provider.Request.Files["FileNameFile"].SaveAs(Provider.Server.MapPath(imgUrl));
                    this.FileName = imgUrl;
                }
            }
        }
    }

}
