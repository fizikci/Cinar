using System;
using Cinar.Database;
using System.Drawing;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    public abstract class NamedEntity : BaseEntity
    {
        private string name;
        [ColumnDetail(IsNotNull=true, Length=100)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string description;
        [ColumnDetail(ColumnType = DbType.Text)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string picture;
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        [PictureFieldProps(SpecialFolder = "uploadDir", SpecialNameField = "Name", AddRandomNumber = true, UseYearMonthDayFolders = true)]
        public string Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        public override string GetNameValue()
        {
            return this.name;
        }
        public override string GetNameColumn()
        {
            return "Name";
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            // resim gelmişse kaydedelim
            // TODO: bu yönetim paneli içindi. zamanla uçurulabilir.
            if (Provider.Request.Files["PictureFile"] != null && Provider.Request.Files["PictureFile"].ContentLength > 0)
            {
                string picFileName = Provider.Request.Files["PictureFile"].FileName;
                if (!String.IsNullOrEmpty(picFileName))
                {
                    string imgUrl = Provider.AppSettings[this.GetType().Name.ToLower() + "Dir"] + "/" + System.IO.Path.GetFileName(picFileName);
                    Bitmap bmp = (Bitmap)Bitmap.FromStream(Provider.Request.Files["PictureFile"].InputStream);
                    if (bmp.Width > Provider.Configuration.ImageUploadMaxWidth)
                    {
                        Bitmap bmp2 = Utility.ScaleImage(bmp, Provider.Configuration.ImageUploadMaxWidth, 0);
                        imgUrl = imgUrl.Substring(0, imgUrl.LastIndexOf('.')) + ".jpg";
                        Utility.SaveJpeg(Provider.Server.MapPath(imgUrl), bmp2, Provider.Configuration.ThumbQuality);
                    }
                    else
                        Provider.Request.Files["PictureFile"].SaveAs(Provider.Server.MapPath(imgUrl));
                    this.Picture = imgUrl;
                }
            }
        }
    }
}
