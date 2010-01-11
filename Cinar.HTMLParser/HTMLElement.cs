using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Cinar.Extensions;

namespace Cinar.HTMLParser
{
    public abstract class HTMLElement
    {
        public HTMLElement()
        {
            this.ChildNodes = new List<HTMLElement>();
        }

        public RectangleF Layout;

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
                if (this is InnerText)
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

        internal PointF cursor;

        public abstract void Draw(Graphics g);
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

        public override void Draw(Graphics g)
        {
            throw new NotImplementedException();
        }
    }

    public class DivElement : HTMLElement
    {
        public DivElement()
        {
            style["display"] = "block";
        }

        public override void Draw(Graphics g)
        {
            foreach (HTMLElement elm in this.ChildNodes)
            {
                elm.Layout.Location = elm.Parent.cursor;
                elm.Layout.Width = elm.Parent.Layout.Width;
                elm.Draw(g);
            }
        }
    }
    public class SpanElement : HTMLElement
    {
        public SpanElement()
        {
            style["display"] = "inline";
        }

        public override void Draw(Graphics g)
        {
            foreach (HTMLElement elm in this.ChildNodes)
            {
                elm.Draw(g);
            }
        }
    }

    public class InnerTextElement : HTMLElement
    {
        public override void Draw(Graphics g)
        {
            Font font = this.GetFont();
            PointF startCursor = Parent.cursor;
            string str = this.InnerText;
            SolidBrush brush = new SolidBrush(this.GetColor());
            string[] words = str.Split(' ');
            for (int i = 0; i < words.Length; i++ )
            {
                SizeF charRect = g.MeasureString(words[i], font);
                if (Parent.cursor.X + charRect.Width > Parent.Layout.Width)
                    Parent.cursor = new PointF(0, Parent.cursor.Y + charRect.Height);
                g.DrawString(words[i], font, brush, Parent.cursor+Parent.Layout.Location.ToSizeF());
                Parent.cursor.X += charRect.Width;
                System.Threading.Thread.Sleep(100);
            }
            //this.Layout = new RectangleF(startCursor, 
            //this.Layout = new Rectangle(new Point(Parent.cursorLeft,  textSize.ToSize();
            //g.DrawString(this.InnerText, font, new SolidBrush(cl), this.Layout.Location.ToPointF());
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
