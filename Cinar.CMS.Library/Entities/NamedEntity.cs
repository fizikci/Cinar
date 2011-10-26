﻿using System;
using Cinar.Database;
using System.Drawing;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    public abstract class NamedEntity : BaseEntity
    {
        [ColumnDetail(IsNotNull=true, Length=100)]
        public string Name { get; set; }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Description { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit), PictureFieldProps(SpecialFolder = "uploadDir", SpecialNameField = "Name", AddRandomNumber = true, UseYearMonthDayFolders = true)]
        public string Picture { get; set; }

        public override string GetNameValue()
        {
            return this.Name;
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
                    Bitmap bmp = (Bitmap)Image.FromStream(Provider.Request.Files["PictureFile"].InputStream);
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
