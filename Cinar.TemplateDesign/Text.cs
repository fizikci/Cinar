using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;
using System.Collections;

namespace Cinar.TemplateDesign
{
    public class Text : Element
    {
        public Text()
        {
            Font = new FontClass();
        }

        [Category("Text")]
        public FontClass Font { get; set; }

        private string innerText = "Sayın @acc.Name dikkatine!";
        [Category("Text"), DefaultValue("Sayın @acc.Name dikkatine!"), Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string InnerText
        {
            get
            {
                return innerText;
            }
            set
            {
                innerText = value;
            }
        }

        internal Hashtable Parameters = null;

        public override void Paint(Graphics g)
        {
            base.Paint(g);

            Font font = Font.GetFont();

            string text = getReplacedText();

            g.DrawString(
                text,
                font,
                new SolidBrush(Color.FromKnownColor(Font.Color)),
                new RectangleF(
                    this.Location.X + this.Border.Left.Width + this.Padding.Left, 
                    this.Location.Y + this.Border.Top.Width + this.Padding.Top, 
                    this.Width, 
                    this.Height));
        }

        private string getReplacedText()
        {
            string text = this.InnerText;
            if (this.Parameters != null)
            {
                foreach (string key in Parameters.Keys)
                {
                    string val = Parameters[key] == null ? "null" : Parameters[key].ToString();
                    text = text.Replace("@" + key, val);
                }
            }
            return text;
        }

        protected override string toHTML()
        {
            return getReplacedText();
        }

        public string ToText()
        {
            return getReplacedText();
        }

        protected override string getExtraCSS()
        {
            return string.Format("font-family:{0};font-size:{1}px;color:{2}",
                Font.Family,
                Font.Size,
                Font.Color);
        }
    }
}
