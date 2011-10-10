using System;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Xml.Serialization;
using System.IO;

namespace Cinar.Drawing
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class BorderComplex
    {
        public BorderComplex()
        {
            All = new Border();
            Top = new Border();
            Right = new Border();
            Bottom = new Border();
            Left = new Border();
        }

        public Border All { get; set; }
        public Border Top { get; set; }
        public Border Right { get; set; }
        public Border Bottom { get; set; }
        public Border Left { get; set; }

        internal Border getBorder(Side side)
        {
            Border border = All;

            switch (side)
            {
                case Side.Top:
                    border = Top;
                    break;
                case Side.Right:
                    border = Right;
                    break;
                case Side.Bottom:
                    border = Bottom;
                    break;
                case Side.Left:
                    border = Left;
                    break;
            }
            
            Border b = new Border();
            b.Color = border.Color == KnownColor.Transparent ? All.Color : border.Color;
            b.Width = border.Width == 0 ? All.Width : border.Width;
            b.Style = border.Style == BorderStyles.Undefined ? All.Style : border.Style;

            return b;
        }

        public override string ToString()
        {
            string res = "";
            if (!Left.IsEmpty())
                res += "border-left:" + Left + ";";
            if (!Right.IsEmpty())
                res += "border-right:" + Right + ";";
            if (!Top.IsEmpty())
                res += "border-top:" + Top + ";";
            if (!Bottom.IsEmpty())
                res += "border-bottom:" + Bottom + ";";
            if (!All.IsEmpty())
                res += "border:" + All + ";";
            return res;
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Border
    {
        public Border()
        {
            Width = 0;
            Color = KnownColor.Transparent;
            Style = BorderStyles.Undefined;
        }

        public bool IsEmpty()
        {
            return Width == 0 && Color == KnownColor.Transparent && Style == BorderStyles.Undefined;
        }

        [DefaultValue(0)]
        public int Width { get; set; }
        [DefaultValue(KnownColor.Transparent)]
        public KnownColor Color { get; set; }
        [DefaultValue(BorderStyles.Undefined)]
        public BorderStyles Style { get; set; }

        public Pen GetPen()
        {
            Brush borderBrush = new SolidBrush(System.Drawing.Color.FromKnownColor(this.Color));
            Pen p = new Pen(borderBrush, this.Width);
            p.DashCap = DashCap.Round;
            switch (this.Style)
            {
                case BorderStyles.Solid:
                    p.DashStyle = DashStyle.Solid;
                    break;
                case BorderStyles.Dashed:
                    p.DashStyle = DashStyle.Dash;
                    break;
                case BorderStyles.Dotted:
                    p.DashStyle = DashStyle.Dot;
                    break;
            }
            return p;
        }

        public override string ToString()
        {
            string width = Width > -1 ? Width + "px" : "";
            string style = Style != BorderStyles.Undefined ? " " + Style.ToString().ToLowerInvariant() : "";
            string color = Color != KnownColor.Transparent ? " " + Color.ToString().ToLowerInvariant() : "";
            return String.Format("{0}{1}{2}", width, style, color);
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Background
    {
        public Background()
        {
            Color = KnownColor.Transparent;
            Image = null;
            //PositionAlignX = AlignX.Left;
            //PositionAlignY = AlignY.Top;
            //PositionX = 0;
            //PositionY = 0;
            Repeat = BackgroundRepeat.Undefined;
        }
        [DefaultValue(KnownColor.Transparent)]
        public KnownColor Color { get; set; }
        private Image image;
        [XmlIgnore]
        public Image Image {
            get
            {
                if (image == null && !String.IsNullOrEmpty(ImageData))
                    image = Image.FromStream(new MemoryStream(Convert.FromBase64String(ImageData)));
                return image;
            }
            set
            {
                image = value;
                //if (image != null)
                //{
                //    TypeConverter converter = TypeDescriptor.GetConverter(image.GetType());
                //    imageData = Convert.ToBase64String((byte[])converter.ConvertTo(image, typeof(byte[])));
                //}
                //else
                //    imageData = null;
            }
        }
        private string imageData;
        [Browsable(false)]
        public string ImageData {
            get
            {
                if (image != null && imageData == null)
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(image.GetType());
                    imageData = Convert.ToBase64String((byte[])converter.ConvertTo(image, typeof(byte[])));
                }
                return imageData;
            }
            set
            {
                imageData = value;
            }
        }
        public void SetBackgroundImage(Image image)
        {
            this.image = image;
        }
        [DefaultValue(BackgroundRepeat.Undefined)]
        public BackgroundRepeat Repeat { get; set; }
        //[DefaultValue(AlignX.Left)]
        //public AlignX PositionAlignX { get; set; }
        //[DefaultValue(AlignY.Top)]
        //public AlignY PositionAlignY { get; set; }
        //[DefaultValue(0)]
        //public int PositionX { get; set; }
        //[DefaultValue(0)]
        //public int PositionY { get; set; }

        public override string ToString()
        {
            string color = Color != KnownColor.Transparent ? " " + Color.ToString().ToLowerInvariant() : "";
            //string image = Image != "" ? " url(" + Image + ")" : "";
            string repeat = Repeat != BackgroundRepeat.Undefined ? " " + Repeat.ToString().ToLowerInvariant() : "";
            //string posX = PositionX > 0 ? PositionX.ToString() + "px" : PositionAlignX.ToString().ToLowerInvariant();
            //string posY = PositionY > 0 ? PositionY.ToString() + "px" : PositionAlignY.ToString().ToLowerInvariant();

            return String.Format("{0}{1}", //{2} {3} {4}
                color, 
                //image, 
                repeat); 
                //posX, posY);
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MarginPadding
    {
        public MarginPadding()
        {
            All = 0;
        }

        private int all;
        [DefaultValue(0)]
        public int All { 
            get { return all; }
            set
            {
                if (value < 0) return;
                if (Top == all) Top = value;
                if (Right == all) Right = value;
                if (Bottom == all) Bottom = value;
                if (Left == all) Left = value;
                all = value;
            }
        }

        private int top;
        [DefaultValue(0)]
        public int Top
        {
            get { return top; }
            set
            {
                if (value < 0) return;
                top = value;
            }
        }
        
        private int right;
        [DefaultValue(0)]
        public int Right
        {
            get { return right; }
            set
            {
                if (value < 0) return;
                right = value;
            }
        }
        
        private int bottom;
        [DefaultValue(0)]
        public int Bottom
        {
            get { return bottom; }
            set
            {
                if (value < 0) return;
                bottom = value;
            }
        }

        private int left;
        [DefaultValue(0)]
        public int Left
        {
            get { return left; }
            set
            {
                if (value < 0) return;
                left = value;
            }
        }

        public override string ToString()
        {
            if (top == all && left == all && right == all && bottom == all)
                return all + "px";
            else
                return String.Format("{0}px {1}px {2}px {3}px", top, right, bottom, left);
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FontClass
    {
        public FontClass()
        {
            Family = "Tahoma";
            Size = 16;
            Color = KnownColor.Black;
        }
        [DefaultValue("Tahoma")]
        [TypeConverter(typeof(FontConverter.FontNameConverter)), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Editor("System.Drawing.Design.FontNameEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string Family { get; set; }
        [DefaultValue(KnownColor.Black)]
        public KnownColor Color { get; set; }
        [DefaultValue(16)]
        public int Size { get; set; }
        [DefaultValue(false)]
        public bool Bold { get; set; }
        [DefaultValue(false)]
        public bool Italic { get; set; }
        [DefaultValue(false)]
        public bool Underline { get; set; }
        [DefaultValue(false)]
        public bool LineThrough { get; set; }
        [DefaultValue(GraphicsUnit.Pixel)]
        public GraphicsUnit SizeUnit { get; set; }

        public override string ToString()
        {
            return Family;
        }

        public FontStyle GetStyle()
        {
            return 
                (Bold ? FontStyle.Bold : 0) | 
                (Italic ? FontStyle.Italic : 0) | 
                (LineThrough ? FontStyle.Strikeout : 0) | 
                (Underline ? FontStyle.Underline : 0);
        }

        public Font GetFont()
        {
            try
            {
                return new Font(Family, Size, GetStyle(), SizeUnit);
            }
            catch 
            {
                return new Font("Tahoma", 16, GetStyle(), SizeUnit);
            }
        }
    }

    public enum BorderStyles
    {
        Undefined,
        None,
        Solid,
        Dashed,
        Dotted
    }
    public enum BackgroundRepeat
    {
        Undefined,
        Tile,
        Strech,
        Center
    }
    //public enum AlignX
    //{
    //    None,
    //    Left,
    //    Center,
    //    Right
    //}
    //public enum AlignY
    //{
    //    None,
    //    Top,
    //    Center,
    //    Bottom
    //}
    public enum Side
    {
        All,
        Top,
        Right,
        Bottom,
        Left
    }
}
