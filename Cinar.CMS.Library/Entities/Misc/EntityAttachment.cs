using System;
using Cinar.Database;
using System.Xml.Serialization;
using System.Drawing;

//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select EntityAttachment.Id as [EntityAttachment.Id], EntityAttachment.Title as [EntityAttachment.Title], EntityAttachment.FileName as [EntityAttachment.FileName], EntityAttachment.Tag as [EntityAttachment.Tag], EntityAttachment.EntityName as [EntityAttachment.EntityName], EntityAttachment.EntityId as [EntityAttachment.EntityId], EntityAttachment.Visible as [EntityAttachment.Visible] from [EntityAttachment]", QueryOrderBy = "EntityAttachment.OrderNo")]
    [EditFormDetails(DetailType = typeof(EntityAttachmentLang), RelatedFieldName = "EntityAttachmentId")]
    public class EntityAttachment : BaseEntity
    {
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.entityTypes")]
        public string EntityName { get; set; }
        public int EntityId { get; set; }

        private BaseEntity _entity;
        [XmlIgnore]
        public BaseEntity Entity
        {
            get
            {
                if (_entity == null)
                    _entity = (Content)Provider.Database.Read(Provider.GetEntityType(EntityName), this.EntityId);
                return _entity;
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

        private string fileName;
        [ColumnDetail(IsNotNull=true, Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
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
                    try
                    {
                        Image bmp = Image.FromStream(Provider.Request.Files["FileName"].InputStream);
                        if (bmp.Width > Provider.Configuration.ImageUploadMaxWidth)
                        {
                            Image bmp2 = bmp.ScaleImage(Provider.Configuration.ImageUploadMaxWidth, 0);
                            imgUrl = imgUrl.Substring(0, imgUrl.LastIndexOf('.')) + ".jpg";
                            bmp2.SaveJpeg(Provider.MapPath(imgUrl), Provider.Configuration.ThumbQuality);
                        }
                        else
                            Provider.Request.Files["FileName"].SaveAs(Provider.MapPath(imgUrl));
                        Provider.DeleteThumbFiles(imgUrl);
                    }
                    catch {
                        Provider.Request.Files["FileName"].SaveAs(Provider.MapPath(imgUrl));
                    }
                    this.FileName = imgUrl;
                }
            }
        }


        public string GetThumbPicture(int width, int height, bool cropPicture)
        {
            return Provider.GetThumbPath(this.FileName, width, height, cropPicture);
        }

    }

}
