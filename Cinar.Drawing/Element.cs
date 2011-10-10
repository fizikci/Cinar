using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.ComponentModel;
using Rectangle = Cinar.Drawing.Rectangle;
//using System.Windows.Forms;

namespace Cinar.Drawing
{
    [XmlInclude(typeof(Text))]
    [XmlInclude(typeof(Picture))]
    [XmlInclude(typeof(Line))]
    [XmlInclude(typeof(Rectangle))]
    [XmlInclude(typeof(Circle))]
    public class Element : IComparable
    {
        [Category("Layout")]
        public Point Location { get; set; }
        [XmlIgnore]
        [Category("Layout")]
        public Size Size
        { 
            get { return new Size(this.Width, this.Height); } 
            set { this.Width = value.Width; this.Height = value.Height; } 
        }

        [Browsable(false)]
        public int ZIndex { get; set; }
        [Browsable(false)]
        public int Width { get; set; }
        [Browsable(false)]
        public int Height { get; set; }

        //[DefaultValue(KnownColor.Black)]
        //public KnownColor Color { get; set; }
        [Category("Style")]
        public Background Background { get; set; }
        [Category("Style")]
        public BorderComplex Border { get; set; }
        [Category("Style")]
        [DefaultValue("0px")]
        public MarginPadding Padding { get; set; }

        public Element()
        {
            //Color = KnownColor.Black;
            Background = new Background();
            Border = new BorderComplex();
            Padding = new MarginPadding();
        }

        //public Element(PageDesigner parent) : this()
        //{
        //    this.parent = parent;
        //}

        //private PageDesigner parent;
        //[XmlIgnore, Browsable(false)]
        //public PageDesigner Parent
        //{
        //    get
        //    {
        //        return this.parent;
        //    }
        //    set 
        //    {
        //        this.parent = value;
        //    }
        //}

        private bool selected;
        [XmlIgnore, Browsable(false)]
        public bool Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                this.selected = value;
            }
        }

        private Hashtable tags = new Hashtable();
        [XmlIgnore, Browsable(false)]
        public Hashtable Tags
        {
            get { return tags; }
        }

        private Cursor cursor = Cursors.Default;
        [XmlIgnore, Browsable(false)]
        public Cursor Cursor
        {
            get { return cursor; }
            set { cursor = value; }
        }

        public Action OnClick;

        public virtual void Paint(Graphics g)
        {
            int x0 = this.Location.X;
            int y0 = this.Location.Y;

            Pen borderPen = null;
            if ((borderPen = getBorderPen(Side.Top)) != null)
                g.DrawLine(borderPen, x0, y0, x0 + this.Width, y0);
            if ((borderPen = getBorderPen(Side.Right)) != null)
                g.DrawLine(borderPen, x0 + this.Width, y0, x0 + this.Width, y0 + this.Height);
            if ((borderPen = getBorderPen(Side.Bottom)) != null)
                g.DrawLine(borderPen, x0, y0 + this.Height, x0 + this.Width, y0 + this.Height);
            if ((borderPen = getBorderPen(Side.Left)) != null)
                g.DrawLine(borderPen, x0, y0, x0, y0 + this.Height);

            if (this.Selected)
            {
                Pen pen = new Pen(Color.Silver);
                pen.DashStyle = DashStyle.Dash;
                g.DrawRectangle(pen, x0, y0, this.Width, this.Height);
            }
        }

        private Pen getBorderPen(Side whichSide)
        {
            Border b = this.Border.getBorder(whichSide);
            if (b.Width > 0 && b.Style != BorderStyles.None)
            {
                return b.GetPen();
            }
            else
                return null;
        }


        public virtual void PaintBackground(Graphics g)
        {
            Page.DrawBackgroundStyle(g, this.Location, this.Size, this.Background);
        }


        #region IComparable Members

        public int CompareTo(object obj)
        {
            Element elm = (Element)obj;
            return this.ZIndex.CompareTo(elm.ZIndex);
        }

        #endregion

        public string ToHTML()
        {
            string background = Background.ToString();
            string border = Border.ToString();
            string padding = Padding.ToString();

            return string.Format(@"
                <div style=""
                    position:absolute;
                    left:{0}px;
                    top:{1}px;
                    width:{2}px;
                    height:{3}px;
                    z-index:{4};
                    {5}{6}{7}{8}"">
                    {9}
                </div>
                ",
                 Location.X,
                 Location.Y,
                 Width,
                 Height,
                 ZIndex,
                 !string.IsNullOrEmpty(background) ? "background:" + background + ";" : "",
                 !string.IsNullOrEmpty(border) ? border : "",
                 !string.IsNullOrEmpty(padding) ? "padding:" + padding + ";" : "",
                 this.getExtraCSS(),
                 this.toHTML());
        }

        protected virtual string toHTML()
        {
            return "";
        }
        protected virtual string getExtraCSS()
        {
            return "";
        }
    }
}
