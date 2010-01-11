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
        public static double FindDistanceTo(this Point p, Point point)
        {
            return Math.Sqrt((double)Math.Abs((point.X - p.X) * (point.X - p.X) - (point.Y - p.Y) * (point.Y - p.Y)));
        }
        public static Point FindMidPointTo(this Point p, Point point)
        {
            return new Point((p.X + point.X) / 2, (p.Y + point.Y) / 2);
        }
        public static SizeF ToSizeF(this PointF p)
        {
            return new SizeF(p.X, p.Y);
        }

        public static SizeF ToSizeF(this Size p)
        {
            return new SizeF(p.Width, p.Height);
        }
        public static Size ToSize(this SizeF p)
        {
            return new Size((int)p.Width, (int)p.Height);
        }
        public static Size Multiply(this Size p, int number)
        {
            return new Size(p.Width * number, p.Height * number);
        }
        public static Size Multiply(this Size p, Size numbers)
        {
            return new Size(p.Width * numbers.Width, p.Height * numbers.Height);
        }
        
        public static RectangleF ToRectangleF(this Rectangle p)
        {
            return new RectangleF(p.Location.ToPointF(), p.Size.ToSizeF());
        }
        public static Rectangle ToRectangle(this RectangleF p)
        {
            return new Rectangle(p.Location.ToPoint(), p.Size.ToSize());
        }
        public static Point[] GetCorners(this Rectangle rect)
        {
            return new Point[]{
                                new Point(rect.Left, rect.Top),
                                new Point(rect.Right, rect.Top),
                                new Point(rect.Left, rect.Bottom),
                                new Point(rect.Right, rect.Bottom)
                            };
        }
        public static Point GetMidPoint(this Rectangle rect)
        {
            return rect.Location.FindMidPointTo(rect.Location + rect.Size);
        }
        public static Point FindClosestSideCenterTo(this Rectangle rect, Point point)
        {
            List<Point> closestTwo = rect.GetCorners().OrderBy(p => p.FindDistanceTo(point)).Take(2).ToList();

            return closestTwo[0].FindMidPointTo(closestTwo[1]);
        }
        public static Point FindClosestSideCenterTo(this Rectangle rect, Rectangle rectangle)
        {
            List<Point> closestTwo = rect.GetCorners().OrderBy(p => p.FindDistanceTo(rectangle.GetMidPoint())).Take(2).ToList();

            return closestTwo[0].FindMidPointTo(closestTwo[1]);
        }
    }
}
