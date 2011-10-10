using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;
using System.Collections;

namespace Cinar.Drawing
{
    public class Page
    {
        public Page()
        {
            //Zoom = 100;
            Background = new Background();
            Width = 864;
            Height = 1194;
        }

        public List<Element> Elements = new List<Element>();

        //public int Zoom { get; set; }

        public Element HitTest(Point point)
        {
            List<Element> elms = this.Elements.FindAll((Element elm) =>
            {
                return elm.Location.X <= point.X && elm.Location.Y <= point.Y && elm.Location.X + elm.Width >= point.X && elm.Location.Y + elm.Height >= point.Y;
            });

            if (elms.Count == 0)
                return null;
            else
            {
                elms.Sort();
                return elms[elms.Count - 1];
            }
        }

        public float ScaleFactor = 1.0f;

        public void SendBack(Element element)
        {
            int currZ = element.ZIndex;

            foreach (Element elm in this.Elements)
                if (elm == element)
                    elm.ZIndex = 0;
                else
                    elm.ZIndex = elm.ZIndex < currZ ? elm.ZIndex + 1 : elm.ZIndex;

            this.Elements.Sort();
        }

        public void BringToFront(Element element)
        {
            int currZ = element.ZIndex;

            foreach (Element elm in this.Elements)
                if (elm == element)
                    elm.ZIndex = this.Elements.Count - 1;
                else
                    elm.ZIndex = elm.ZIndex > currZ ? elm.ZIndex - 1 : elm.ZIndex;

            this.Elements.Sort();
        }

        [Category("Style")]
        public Background Background { get; set; }

        [Browsable(false)]
        public int Width { get; set; }
        [Browsable(false)]
        public int Height { get; set; }

        public void ScaleTransform(float sx, float sy)
        {
            if (sx == ScaleFactor && sy == ScaleFactor)
                return; //***

            this.Width = Convert.ToInt32(this.Width * sx);
            this.Height = Convert.ToInt32(this.Height * sy);

            foreach (Element elm in this.Elements)
            {
                elm.Location = new Point(Convert.ToInt32(elm.Location.X * sx), Convert.ToInt32(elm.Location.Y * sy));

                elm.Width = Convert.ToInt32(elm.Width * sx);
                elm.Height = Convert.ToInt32(elm.Height * sy);

                if (elm is Text)
                {
                    Text txt = elm as Text;
                    txt.Font.Size = Convert.ToInt32(txt.Font.Size * (sx + sy) / 2);
                }
            }
        }

        public void Draw(Graphics graphics)
        {
            //float zoom = (float)Zoom / (float)100;
            graphics.ScaleTransform(ScaleFactor, ScaleFactor);

            Page.DrawBackgroundStyle(graphics, new Point(0, 0), new Size(this.Width, this.Height), this.Background);

            foreach (Element elm in this.Elements)
                elm.PaintBackground(graphics);

            foreach (Element elm in this.Elements)
                elm.Paint(graphics);
        }

        public static void DrawBackgroundStyle(Graphics g, Point location, Size size, Background background)
        {
            // draw background color
            g.FillRectangle(
                new SolidBrush(System.Drawing.Color.FromKnownColor(background.Color)),
                location.X,
                location.Y,
                size.Width,
                size.Height);

            // draw background image
            if (background.Image != null)
            {
                Image bg = background.Image; // Image.FromFile(this.Background.Image);
                switch (background.Repeat)
                {
                    case BackgroundRepeat.Undefined:
                    case BackgroundRepeat.Tile:
                        for (int i = 0; i < size.Width / bg.Width; i++)
                            for (int j = 0; j < size.Height / bg.Height; j++)
                                g.DrawImageUnscaled(bg, location.X + i * bg.Width, location.Y + j * bg.Height);
                        break;
                    case BackgroundRepeat.Strech:
                        g.DrawImage(bg, location.X, location.Y, size.Width, size.Height);
                        break;
                    case BackgroundRepeat.Center:
                        int posX = location.X + size.Width / 2 - bg.Width / 2;
                        int posY = location.Y + size.Height / 2 - bg.Height / 2;
                        g.DrawImageUnscaled(bg, posX, posY);
                        break;
                    default:
                        break;
                }
            }
        }

        public void SetParameters(Hashtable parameters)
        {
            if (parameters == null)
                return;

            foreach(Element elm in this.Elements)
            {
                Text t = elm as Text;
                if (t == null) continue;

                t.Parameters = parameters;
            }
        }

        internal string ToHTML()
        {
            string html = string.Format("<div style=\"position:relative;width:{0}px;height:{1}px;background-color:{2}\">",
                this.Width,
                this.Height,
                this.Background.Color);

            foreach (Element elm in this.Elements)
                html += elm.ToHTML();

            html += "</div>";

            return html;
        }

        public Page Clone()
        {
            Page p = null;

            XmlSerializer ser = new XmlSerializer(typeof(Page));
            using (StringWriter sw = new StringWriter())
            {
                ser.Serialize(sw, this);
                string data = sw.ToString();
                using (StringReader sr = new StringReader(data))
                {
                    p = (Page)ser.Deserialize(sr);
                }
            }

            return p;
        }
    }
}
