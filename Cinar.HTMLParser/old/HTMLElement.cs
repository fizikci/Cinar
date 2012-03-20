using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Cinar.Extensions;

namespace Cinar.HTMLParser
{
    public abstract class HTMLElement : IDisposable
    {
        public HTMLElement()
        {
            this.ChildNodes = new List<HTMLElement>();
        }

        public string TagName { get; internal set; }
        protected Dictionary<string, string> attributes = new Dictionary<string, string>();
        public void SetAttribute(string name, string val)
        {
            attributes.Add(name, val);
            if (name == "style")
            {
                foreach (string pair in val.Split(';'))
                {
                    string[] parts = pair.Split(':');
                    if (parts.Length == 2)
                        style.Add(parts[0], parts[1]);
                }
            }
        }

        protected Dictionary<string, string> style = new Dictionary<string, string>();

        public List<HTMLElement> ChildNodes { get; internal set; }
        public HTMLElement Parent { get; internal set; }

        private string innerText;
        public string InnerText 
        {
            get
            {
                if (innerText == null)
                {
                    string res = "";
                    foreach (HTMLElement elm in this.ChildNodes)
                        res += elm.InnerText;
                    return res;
                }
                else
                    return innerText;
            }
            set
            {
                if (this is InnerTextElement)
                    this.InnerText = value;
                else
                {
                    this.ChildNodes.Clear();
                    InnerTextElement elm = new InnerTextElement();
                    elm.TagName = "InnerText";
                    elm.innerText = value;
                    elm.Parent = this;
                    this.ChildNodes.Add(elm);
                }
            }
        }

        internal string GetRecursiveStyleValue(string styleName)
        {
            if (this.style.ContainsKey(styleName))
                return this.style[styleName];
            else if (this.Parent != null)
                return this.Parent.GetRecursiveStyleValue(styleName);
            else
                return getDefaultStyle(styleName);
        }
        internal void SetRecursiveStyleValue(string styleName, string styleValue)
        {
            if (GetRecursiveStyleValue(styleName) != styleValue)
                style[styleName] = styleValue;
        }

        private string getDefaultStyle(string styleName)
        {
            switch (styleName.ToLowerInvariant())
            {
                case "color":
                    return "black";
                case "font-family":
                    return "Verdana";
                case "font-size":
                    return "12px";
                case "background-color":
                    return "white";
                case "display":
                    return "inline";
            }
            return "";
        }

        private Font font = null;
        public Font GetFont()
        {
            if (font != null)
                return font;
            if (this.style.Keys.Any(key => new string[] { "font-family", "font-size", "font-weight", "font-style" }.Contains(key)))
            {
                font = new Font(
                    GetRecursiveStyleValue("font-family"),
                    GetRecursiveStyleValue("font-size").ToFloat(),
                    (GetRecursiveStyleValue("font-weight") == "bold" ? FontStyle.Bold : FontStyle.Regular) | (GetRecursiveStyleValue("font-style") == "italic" ? FontStyle.Italic : FontStyle.Regular) | (GetRecursiveStyleValue("text-decoration") == "underline" ? FontStyle.Underline : FontStyle.Regular),
                    GraphicsUnit.Pixel);
                return font;
            }
            else if (Parent != null)
                return Parent.GetFont();
            else
                return Control.DefaultFont;
        }

        public Color GetColor()
        {
            string strColor = GetRecursiveStyleValue("color");
            return strColor.ToUpperInvariant().ToColor();
        }

        //public CinarBrowser IDocument;
        internal float lastLineHeight;

        public RectangleF Layout;
        internal RectangleF InnerLayout
        {
            get {
                float paddingLeft = GetRecursiveStyleValue("padding-left").ToFloat();
                float paddingRight = GetRecursiveStyleValue("padding-right").ToFloat();
                float paddingBottom = GetRecursiveStyleValue("padding-bottom").ToFloat();
                float paddingTop = GetRecursiveStyleValue("padding-top").ToFloat();
                float marginLeft = GetRecursiveStyleValue("margin-left").ToFloat();
                float marginRight = GetRecursiveStyleValue("margin-right").ToFloat();
                float marginBottom = GetRecursiveStyleValue("margin-bottom").ToFloat();
                float marginTop = GetRecursiveStyleValue("margin-top").ToFloat();
                float borderLeftWidth = GetRecursiveStyleValue("border-left-width").ToFloat();
                float borderRightWidth = GetRecursiveStyleValue("border-right-width").ToFloat();
                float borderBottomWidth = GetRecursiveStyleValue("border-bottom-width").ToFloat();
                float borderTopWidth = GetRecursiveStyleValue("border-top-width").ToFloat();
                RectangleF innerLayout = new RectangleF();
                innerLayout.Location = new PointF(
                    Layout.Location.X + paddingLeft + marginLeft + borderLeftWidth,
                    Layout.Location.Y + paddingTop + marginTop + borderTopWidth);
                innerLayout.Width = Layout.Width - (paddingRight + marginRight + borderRightWidth);
                innerLayout.Height = Layout.Height - (paddingBottom + marginBottom + borderBottomWidth);
                return innerLayout;
            }
        }

        internal float AvailableWidth = 0f;
        internal PointF CursorPos;
        internal List<Line> Lines = new List<Line>();

        public HTMLElement FindBlockParent() {
            if (Parent == null) 
                return null;

            if (Parent.style["display"] == "block")
                return Parent;
            else
                return Parent.FindBlockParent();
        }

        public virtual void CalculateLayout(Graphics g)
        {
            //AvailableWidth = Parent == null ? IDocument.Width : Parent.InnerLayout.Width;
        }
        public virtual void Draw(Graphics g)
        {
            HTMLElement blockParent = this.FindBlockParent();
            if (blockParent != null)
            {
                this.Layout.Location = new PointF(blockParent.Layout.X, blockParent.CursorPos.Y + blockParent.lastLineHeight);
                this.Layout.Width = blockParent.Layout.Width;
            }
            foreach (HTMLElement elm in this.ChildNodes)
            {
                elm.Draw(g);
                elm.DrawPrivate(g);
            }
            if (blockParent != null)
            {
                blockParent.Layout.Height += this.Layout.Height;
                blockParent.CursorPos += new SizeF(0, this.Layout.Height);
            }
        }
        public virtual void DrawPrivate(Graphics g) { }

        #region IDisposable Members

        public void Dispose()
        {
            if (font != null)
                font.Dispose();
        }

        #endregion

        public class Line
        {
            public RectangleF Layout;
            public List<Word> Words = new List<Word>();
        }

        public class Word
        {
            public RectangleF Layout;
            public string WordString;
            public HtmlElement IElement;
        }
    }

    public class ImageElement : DivElement
    {
        public ImageElement()
        {
            style["display"] = "inline";
        }
        
        public string Src 
        {
            get { return attributes["src"]; }
            set { attributes["src"] = value; }
        }

        public override void DrawPrivate(Graphics g)
        {
            
        }
    }

    public class DivElement : HTMLElement
    {
        public DivElement()
        {
            style["display"] = "block";
        }
    }
    public class SpanElement : HTMLElement
    {
        public SpanElement()
        {
            style["display"] = "inline";
        }
    }

    public class InnerTextElement : HTMLElement
    {
        public InnerTextElement()
        {
            style["display"] = "inline";
        }

        public override void DrawPrivate(Graphics g)
        {
            Font font = this.GetFont();
            HTMLElement blockParent = this.FindBlockParent();
            PointF startCursor = blockParent.CursorPos;
            string str = this.InnerText;
            SolidBrush brush = new SolidBrush(this.GetColor());
            string[] words = str.Split(' ');
            float lineHeight = 0;
            for (int i = 0; i < words.Length; i++ )
            {
                SizeF charRect = g.MeasureString(words[i], font);
                if (charRect.Height > lineHeight) lineHeight = charRect.Height;
                if (blockParent.CursorPos.X + charRect.Width > blockParent.Layout.Width)
                {
                    blockParent.CursorPos = new PointF(0, blockParent.CursorPos.Y + lineHeight);
                    blockParent.Layout.Height += lineHeight;
                }
                g.DrawString(words[i], font, brush, blockParent.CursorPos + blockParent.Layout.Location.ToSizeF());
                blockParent.CursorPos.X += charRect.Width;
                //System.Threading.Thread.Sleep(100);
            }
            blockParent.lastLineHeight = lineHeight;
        }
    }

    public class ScriptElement : HTMLElement
    {
        public string Src
        {
            get { return attributes["src"]; }
            set { attributes["src"] = value; }
        }
        public string Type
        {
            get { return attributes["type"]; }
            set { attributes["type"] = value; }
        }
        public override void Draw(Graphics g)
        {
            // not drawn            
        }
    }
}
