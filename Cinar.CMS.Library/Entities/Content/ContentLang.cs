using System;
using Cinar.Database;
using System.Drawing;
using System.Xml.Serialization;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select ContentLang.Id, ContentLang.Title as [ContentLang.Title], TLangId.Name as [Lang], ContentLang.Visible as [BaseEntity.Visible] from [ContentLang] left join [Lang] as TLangId ON TLangId.Id = [ContentLang].LangId")]
    public class ContentLang : BaseEntity
    {
        private int contentId = 1;
        [ColumnDetail(IsNotNull = true, References = typeof(Content))]
        [EditFormFieldProps(ControlType = ControlType.LookUp)]
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

        private int langId;
        [ColumnDetail(IsNotNull = true, References = typeof(Lang))]
        [EditFormFieldProps(ControlType = ControlType.LookUp)]
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
        [ColumnDetail(ColumnType = DbType.Text)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string metin;
        [ColumnDetail(ColumnType = DbType.Text)]
        public string Metin
        {
            get { return metin; }
            set { metin = value; }
        }

        public override string GetNameValue()
        {
            return this.title;
        }
        public override string GetNameColumn()
        {
            return "Title";
        }

        private string picture;
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        [PictureFieldProps(SpecialFolder = "uploadDir", SpecialNameField = "Title", AddRandomNumber = true, UseYearMonthDayFolders = true)]
        public string Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        private string spotTitle;
        [ColumnDetail(Length = 200)]
        public string SpotTitle
        {
            get { return spotTitle; }
            set { spotTitle = value; }
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            // başlıksız içerik mi olur! kontrolü
            if (String.IsNullOrEmpty(this.Title))
                throw new Exception(Provider.GetResource("Required field: Title"));

            // resim gelmişse kaydedelim
            if (Provider.Request.Files["PictureFile"]!=null && Provider.Request.Files["PictureFile"].ContentLength > 0)
            {
                string picFileName = Provider.Request.Files["PictureFile"].FileName;
                if (!String.IsNullOrEmpty(picFileName))
                {
                    string imgUrl = Provider.AppSettings["uploadDir"] + "/" + System.IO.Path.GetFileName(picFileName);
                    Image bmp = Image.FromStream(Provider.Request.Files["PictureFile"].InputStream);
                    if (bmp.Width > Provider.Configuration.ImageUploadMaxWidth)
                    {
                        Image bmp2 = bmp.ScaleImage(Provider.Configuration.ImageUploadMaxWidth, 0);
                        imgUrl = imgUrl.Substring(0, imgUrl.LastIndexOf('.')) + ".jpg";
                        bmp2.SaveJpeg(Provider.MapPath(imgUrl), Provider.Configuration.ThumbQuality);
                    }
                    else
                        Provider.Request.Files["PictureFile"].SaveAs(Provider.MapPath(imgUrl));
                    this.Picture = imgUrl;
                }
            }
        }
    }

}
