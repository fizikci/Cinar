using System;
using System.Text;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Content")]
    public class ContentPicture : Module
    {
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

        protected override string show()
        {
            Entities.Content content = Provider.Content;

            if (content == null)
            {
                if (Provider.DesignMode)
                    return Provider.GetResource("There is no picture to show because there is no related content");
                else
                    return String.Empty;
            }

            return Provider.GetThumbImgHTML(content.Picture, this.width, this.height, content.Title, null, null);
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} img {{}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }

}
