using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections;

namespace Cinar.Drawing
{
    public class Template
    {
        public Template()
        { 
        }

        public List<Page> Pages = new List<Page>();

        public static Template Open(string fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Template));
            Template t = null;
            using (StreamReader sr = new StreamReader(fileName))
            {
                t = (Template)ser.Deserialize(sr);
            }
            return t;
        }
        public static Template Open(byte[] data)
        {
            if (data == null)
                return null;

            XmlSerializer ser = new XmlSerializer(typeof(Template));
            Template t = null;
            using (StringReader sr = new StringReader(Encoding.UTF8.GetString(data)))
            {
                t = (Template)ser.Deserialize(sr);
            }
            return t;
        }

        //public Image GetTiffImage(Hashtable parameters)
        //{
        //    return TiffUtil.Save(GetImagesOfPages(parameters, 96, 96), null);
        //}

        public byte[] GetTiffData(Hashtable parameters, int width, int height, int resolutionWidth, int resolutionHeight)
        {
            return TiffUtil2.SaveData(GetImagesOfPages(parameters, width, height, resolutionWidth, resolutionHeight).ToArray());
        }

        public List<Image> ApplyTemplateToTiffFile(Hashtable parameters, string fileName, int width, int height, int resolutionWidth, int resolutionHeight)
        {
            Image[] tiffPages = TiffUtil2.SplitTiffPages(Image.FromFile(fileName));

            Page layoutPage = this.Pages[0];
            float sx = (float)width / (float)layoutPage.Width;
            float sy = (float)height / (float)layoutPage.Height;
            layoutPage.ScaleTransform(sx, sy);
            layoutPage.SetParameters(parameters);

            List<Image> resImages = new List<Image>();
            for (int i = 0; i < tiffPages.Length; i++)
            {
                Bitmap bmp = (Bitmap)tiffPages[i];
                Image resImage = new Bitmap(width, height);
                ((Bitmap)resImage).SetResolution(resolutionWidth, resolutionHeight);

                Graphics g = Graphics.FromImage(resImage);
                g.FillRectangle(new SolidBrush(Color.FromKnownColor(layoutPage.Background.Color)), 0, 0, width, height);

                bmp.SetResolution(resolutionWidth, resolutionHeight);
                bmp = (Bitmap)TiffUtil2.ScaleImage(bmp, width, height);

                //System.Drawing.Imaging.ImageAttributes attr = new System.Drawing.Imaging.ImageAttributes();
                //attr.SetColorKey(bmp.GetPixel(0, 0), bmp.GetPixel(0, 0)); // set transperancy
                //System.Drawing.Rectangle dstRect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
                g.DrawImage(bmp, 0, 0);//dstRect, 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attr);

                layoutPage.Draw(g);

                resImages.Add(resImage);
            }

            return resImages;
        }

        public void Save(string fileName)
        {
            string data = "";
            XmlSerializer ser = new XmlSerializer(typeof(Template));
            using (StringWriter sw = new StringWriter())
            {
                ser.Serialize(sw, this);
                data = sw.ToString();
            }

            File.WriteAllText(fileName, data, Encoding.UTF8);
        }

        public byte[] Save()
        {
            byte[] data = null;
            XmlSerializer ser = new XmlSerializer(typeof(Template));
            using (StringWriter sw = new StringWriter())
            {
                ser.Serialize(sw, this);
                data = Encoding.UTF8.GetBytes(sw.ToString());
            }

            return data;
        }

        public void SaveAsTiff(string fileName)
        {
            List<Image> images = GetImagesOfPages(null, 1728, 2403, 204, 196);

            TiffUtil.Save(images.ToArray(), fileName);
        }

        public List<Image> GetImagesOfPages(Hashtable parameters, int width, int height, int resolutionWidth, int resolutionHeight)
        {
            List<Image> images = new List<Image>();

            for (int i = 0; i < this.Pages.Count; i++)
            {
                Page page = Pages[i];

                float sx = (float)width / (float)page.Width;
                float sy = (float)height / (float)page.Height;

                page.ScaleTransform(sx, sy);

                page.SetParameters(parameters);
                Image image = new Bitmap(width, height);
                ((Bitmap)image).SetResolution(resolutionWidth, resolutionHeight);

                Graphics g = Graphics.FromImage(image);
                g.FillRectangle(new SolidBrush(Color.FromKnownColor(page.Background.Color)), 0, 0, page.Width, page.Height);
                page.Draw(g);

                images.Add(image);
            }
            return images;
        }

        public byte[] GetHtmlData(Hashtable parameters)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < this.Pages.Count; i++)
            {
                Page page = Pages[i];
                page.SetParameters(parameters);
                sb.Append(page.ToHTML());
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        public byte[] GetTextData(Hashtable parameters)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < this.Pages.Count; i++)
            {
                Page page = Pages[i];
                page.SetParameters(parameters);
                foreach(Element elm in page.Elements)
                    if(elm is Text)
                        sb.Append((elm as Text).ToText() + Environment.NewLine);
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        public void SaveAsHTML(string fileName)
        {
            File.WriteAllText(fileName, Encoding.UTF8.GetString(GetHtmlData(null)), Encoding.UTF8);
        }

        public void SaveAsText(string fileName)
        {
            File.WriteAllText(fileName, Encoding.UTF8.GetString(GetTextData(null)), Encoding.UTF8);
        }

        public void PrependTemplate(Template template)
        {
            if (template == null || template.Pages.Count == 0)
                return;

            List<Page> pages = this.Pages;
            this.Pages = new List<Page>();
            this.Pages.AddRange(template.Pages);
            this.Pages.AddRange(pages);
        }

        public void AppendTemplate(Template template)
        {
            if (template == null || template.Pages.Count == 0)
                return;

            this.Pages.AddRange(template.Pages);
        }
    }
}
