using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Cinar.Extensions
{
    public static class Drawing
    {
        public static PointF ToPointF(this Point p)
        {
            return new PointF(p.X, p.Y);
        }
        public static Point ToPoint(this PointF p)
        {
            return new Point((int)p.X, (int)p.Y);
        }
        public static SizeF ToSizeF(this Size p)
        {
            return new SizeF(p.Width, p.Height);
        }
        public static Size ToSize(this SizeF p)
        {
            return new Size((int)p.Width, (int)p.Height);
        }
        public static RectangleF ToRectangleF(this Rectangle p)
        {
            return new RectangleF(p.Location.ToPointF(), p.Size.ToSizeF());
        }
        public static Rectangle ToRectangle(this RectangleF p)
        {
            return new Rectangle(p.Location.ToPoint(), p.Size.ToSize());
        }
    }
}
