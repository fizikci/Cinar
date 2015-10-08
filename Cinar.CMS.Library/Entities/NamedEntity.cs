using System;
using Cinar.Database;
using System.Drawing;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [Serializable]
    public abstract class NamedEntity : BaseEntity
    {
        [ColumnDetail(IsNotNull=true, Length=100)]
        public string Name { get; set; }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Description { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit), PictureFieldProps(SpecialFolder = "uploadDir", SpecialNameField = "Name", AddRandomNumber = true, UseYearMonthDayFolders = true)]
        public string Picture { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit), PictureFieldProps(SpecialFolder = "uploadDir", SpecialNameField = "Name", AddRandomNumber = true, UseYearMonthDayFolders = true)]
        public string Picture2 { get; set; }

        public override string GetNameValue()
        {
            return this.Name;
        }
        public override string GetNameColumn()
        {
            return "Name";
        }

        public override void BeforeSave()
        {
            base.BeforeSave();

            // resim gelmişse kaydedelim
            if (Provider.Request.Files["Picture"] != null && Provider.Request.Files["Picture"].ContentLength > 0)
            {
                string picFileName = Provider.Request.Files["Picture"].FileName;
                if (!String.IsNullOrEmpty(picFileName))
                {
                    string imgUrl = Provider.AppSettings[this.GetType().Name.ToLower() + "Dir"] + "/" + System.IO.Path.GetFileName(picFileName);
                    Image bmp = Image.FromStream(Provider.Request.Files["Picture"].InputStream);
                    if (bmp.Width > Provider.Configuration.ImageUploadMaxWidth)
                    {
                        Image bmp2 = bmp.ScaleImage(Provider.Configuration.ImageUploadMaxWidth, 0);
                        //imgUrl = imgUrl.Substring(0, imgUrl.LastIndexOf('.')) + ".jpg";
                        //bmp2.SaveJpeg(Provider.MapPath(imgUrl), Provider.Configuration.ThumbQuality);
                        bmp2.SaveImage(Provider.MapPath(imgUrl), Provider.Configuration.ThumbQuality);
                    }
                    else
                        Provider.Request.Files["Picture"].SaveAs(Provider.MapPath(imgUrl));
                    this.Picture = imgUrl;

                    Provider.DeleteThumbFiles(imgUrl);
                }
            }
            if (Provider.Request.Files["Picture2"] != null && Provider.Request.Files["Picture2"].ContentLength > 0)
            {
                string picFileName = Provider.Request.Files["Picture2"].FileName;
                if (!String.IsNullOrEmpty(picFileName))
                {
                    string imgUrl = Provider.AppSettings[this.GetType().Name.ToLower() + "Dir"] + "/" + System.IO.Path.GetFileName(picFileName);
                    Image bmp = Image.FromStream(Provider.Request.Files["Picture2"].InputStream);
                    if (bmp.Width > Provider.Configuration.ImageUploadMaxWidth)
                    {
                        Image bmp2 = bmp.ScaleImage(Provider.Configuration.ImageUploadMaxWidth, 0);
                        //imgUrl = imgUrl.Substring(0, imgUrl.LastIndexOf('.')) + ".jpg";
                        //bmp2.SaveJpeg(Provider.MapPath(imgUrl), Provider.Configuration.ThumbQuality);
                        bmp2.SaveImage(Provider.MapPath(imgUrl), Provider.Configuration.ThumbQuality);
                    }
                    else
                        Provider.Request.Files["Picture2"].SaveAs(Provider.MapPath(imgUrl));
                    this.Picture2 = imgUrl;

                    Provider.DeleteThumbFiles(imgUrl);
                }
            }
        }


        public string GetThumbPicture(int width, int height, bool cropPicture)
        {
            return Provider.GetThumbPath(this.Picture, width, height, cropPicture);
        }
        public string GetThumbPicture2(int width, int height, bool cropPicture)
        {
            return Provider.GetThumbPath(this.Picture2, width, height, cropPicture);
        }

    }
}
