﻿using System;
using Cinar.Database;
using System.Xml.Serialization;
using System.Drawing;

//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select ContentPicture.Id as [ContentPicture.Id], ContentPicture.Title as [ContentPicture.Title], ContentPicture.FileName as [ContentPicture.FileName], ContentPicture.Tag as [ContentPicture.Tag], ContentPicture.Visible as [ContentPicture.Visible] from [ContentPicture] left join [Content] ON Content.Id = [ContentPicture].ContentId", QueryOrderBy = "ContentPicture.OrderNo")]
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
        [ColumnDetail(ColumnType=DbType.Text)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string tag = "";
        [ColumnDetail(Length = 200)]
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        private string tagData = "";
        [ColumnDetail(ColumnType = DbType.Text), EditFormFieldProps(Visible = false)]
        public string TagData
        {
            get { return tagData; }
            set { tagData = value; }
        }

        [ColumnDetail(IsNotNull = true, DefaultValue = "0"), EditFormFieldProps(Options = "readOnly:true")]
        public int LikeIt
        {
            get;
            set;
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

        public override void BeforeSave()
        {
            base.BeforeSave();

            // resim gelmişse kaydedelim
            if (Provider.Request.Files["FileName"] != null && Provider.Request.Files["FileName"].ContentLength > 0)
            {
                string picFileName = Provider.Request.Files["FileName"].FileName;
                if (!String.IsNullOrEmpty(picFileName))
                {
                    string imgUrl = Provider.AppSettings["uploadDir"] + "/" + System.IO.Path.GetFileName(picFileName);
                    Image bmp = Image.FromStream(Provider.Request.Files["FileName"].InputStream);
                    if (bmp.Width > Provider.Configuration.ImageUploadMaxWidth)
                    {
                        Image bmp2 = bmp.ScaleImage(Provider.Configuration.ImageUploadMaxWidth, 0);
                        imgUrl = imgUrl.Substring(0, imgUrl.LastIndexOf('.')) + ".jpg";
                        bmp2.SaveJpeg(Provider.MapPath(imgUrl), Provider.Configuration.ThumbQuality);
                    }
                    else
                        Provider.Request.Files["FileName"].SaveAs(Provider.MapPath(imgUrl));
                    this.FileName = imgUrl;

                    Provider.DeleteThumbFiles(imgUrl);
                }
            }
        }


        public string GetThumbPicture(int width, int height, bool cropPicture)
        {
            return Provider.GetThumbPath(this.FileName, width, height, cropPicture);
        }

    }

}
