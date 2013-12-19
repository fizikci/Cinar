using System;
using Cinar.Database;
using System.Drawing;
using System.Xml.Serialization;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    public class NamedEntityLang : BaseEntity
    {
        [ColumnDetail(IsNotNull = true, References = typeof(Lang)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int LangId { get; set; }

        private Lang _lang;
        [XmlIgnore]
        public Lang Lang
        {
            get { return _lang ?? (_lang = (Lang) Provider.Database.Read(typeof (Lang), this.LangId)); }
        }

        [ColumnDetail(IsNotNull = true, Length = 200)]
        public string Name { get; set; }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Description { get; set; }

        public override string GetNameValue()
        {
            return this.Name;
        }
        public override string GetNameColumn()
        {
            return "Name";
        }

        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit), PictureFieldProps(SpecialFolder = "uploadDir", SpecialNameField = "Name", AddRandomNumber = true, UseYearMonthDayFolders = true)]
        public string Picture { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit), PictureFieldProps(SpecialFolder = "uploadDir", SpecialNameField = "Name", AddRandomNumber = true, UseYearMonthDayFolders = true)]
        public string Picture2 { get; set; }

        public override void BeforeSave()
        {
            base.BeforeSave();

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
