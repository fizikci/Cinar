using System.Text;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Containers")]
    public class Frame : Container
    {
        protected string topLeft = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string TopLeft
        {
            get { return topLeft; }
            set { topLeft = value; }
        }

        protected string topCenter = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string TopCenter
        {
            get { return topCenter; }
            set { topCenter = value; }
        }

        protected string topRight = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string TopRight
        {
            get { return topRight; }
            set { topRight = value; }
        }

        protected string centerLeft = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string CenterLeft
        {
            get { return centerLeft; }
            set { centerLeft = value; }
        }

        protected string centerRight = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string CenterRight
        {
            get { return centerRight; }
            set { centerRight = value; }
        }

        protected string bottomLeft = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string BottomLeft
        {
            get { return bottomLeft; }
            set { bottomLeft = value; }
        }

        protected string bottomCenter = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string BottomCenter
        {
            get { return bottomCenter; }
            set { bottomCenter = value; }
        }

        protected string bottomRight = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string BottomRight
        {
            get { return bottomRight; }
            set { bottomRight = value; }
        }

        private string getImageHtml(string img) 
        {
            return img == "" ? "" : ("<img src=\"" + img + "\"/>");
        }

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
            sb.Append("<tr>");
            sb.AppendFormat("<td class=\"topLeft\" width=\"1%\">{0}</td>", getImageHtml(this.topLeft));
            sb.AppendFormat("<td class=\"topCenter\" width=\"98%\" style=\"background:url({0})\"></td>", this.topCenter);
            sb.AppendFormat("<td class=\"topRight\" width=\"1%\">{0}</td>", getImageHtml(this.topRight));
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.AppendFormat("<td class=\"centerLeft\" style=\"background:url({0})\"></td>", this.centerLeft);

            sb.Append("<td>");
            sb.Append(base.show());
            sb.Append("</td>");

            sb.AppendFormat("<td class=\"centerRight\" style=\"background:url({0})\"></td>", this.centerRight);
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.AppendFormat("<td class=\"bottomLeft\">{0}</td>", getImageHtml(this.bottomLeft));
            sb.AppendFormat("<td class=\"bottomCenter\" style=\"background:url({0})\"></td>", this.bottomCenter);
            sb.AppendFormat("<td class=\"bottomRight\">{0}</td>", getImageHtml(this.bottomRight));
            sb.Append("</tr>");
            sb.Append("</table>");

            return sb.ToString();
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0} table {{}}\n", getCSSId());
            sb.AppendFormat("#{0} td.topLeft {{}}\n", getCSSId());
            sb.AppendFormat("#{0} td.topCenter {{}}\n", getCSSId());
            sb.AppendFormat("#{0} td.topRight {{}}\n", getCSSId());
            sb.AppendFormat("#{0} td.centerLeft {{}}\n", getCSSId());
            sb.AppendFormat("#{0} td.centerRight {{}}\n", getCSSId());
            sb.AppendFormat("#{0} td.bottomLeft {{}}\n", getCSSId());
            sb.AppendFormat("#{0} td.bottomCenter {{}}\n", getCSSId());
            sb.AppendFormat("#{0} td.bottomRight {{}}\n", getCSSId());
            return sb.ToString();
        }
    }

}
