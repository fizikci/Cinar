using System;
using System.Text;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Content")]
    public class PictureOfContent : StaticHtml
    {
        public PictureOfContent()
        {
            InnerHtml = @"$
    var pic = '';
    if(this.WhichPicture=='Picture')
        pic = Provider.Content.Picture;
    else if(this.WhichPicture=='Picture2')
        pic = Provider.Content.Picture2;
    else if(this.WhichPicture=='Category')
        pic = Provider.Content.Category.Picture;
    else if(this.WhichPicture=='Category2')
        pic = Provider.Content.Category.Picture2;
    else if(this.WhichPicture=='Author')
        pic = Provider.Content.Author.Picture;
    else if(this.WhichPicture=='Source')
        pic = Provider.Content.Source.Picture;
$
<img 
    src=""$=Provider.GetThumbPath(pic, this.Width, this.Height, this.CropPicture)$"" 
    $=this.Width>0 ? 'width=""'+this.Width+'""':''$  
    $=this.Height>0 ? 'height=""'+this.Height+'""':''$ 
    title=""$=Provider.Content.Title$""
    />
";
        }

        protected string whichPicture = "Picture";
        [ColumnDetail(Length = 30), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:_WHICHPICTURE2_")]
        public string WhichPicture
        {
            get { return whichPicture; }
            set { whichPicture = value; }
        }

        protected int width = 0;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        protected int height = 0;
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public bool CropPicture { get; set; }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0} img {{}}\n", getCSSId());
            return sb.ToString();
        }
    }

}
