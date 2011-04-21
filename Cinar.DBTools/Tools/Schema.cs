﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Cinar.Database;

namespace Cinar.DBTools.Tools
{
    public class Schema
    {
        public static int Def_TableWidth = 150;
        public static int Def_TitleHeight = 20;
        public static int Def_FieldHeight = 20;
        public static ImageList ImageList;
        [XmlIgnore]
        public ConnectionSettings conn;

        public string Name { get; set; }
        public List<TableView> Tables { get; set; }
        public List<ConnectionLine> ConnectionLines { get; set; }

        public TableView GetTableView(string tableName)
        {
            foreach (TableView tv in Tables)
                if (tv.TableName == tableName)
                    return tv;
            return null;
        }

        public Schema()
        {
            Tables = new List<TableView>();
            ConnectionLines = new List<ConnectionLine>();
        }

        public Size CalculateTotalSize()
        {
            int maxX1 = Tables.Max(tv => tv.Position.X + tv.RealSize.Width);
            int maxY1 = Tables.Max(tv => tv.Position.Y + tv.RealSize.Height);

            int maxX2 = ConnectionLines.Count > 0 ? ConnectionLines.Max(cl => new int[] { cl.StartPoint.X, cl.EndPoint.X }.Max()) : 0;
            int maxY2 = ConnectionLines.Count > 0 ? ConnectionLines.Max(cl => new int[] { cl.StartPoint.Y, cl.EndPoint.Y }.Max()) : 0;

            return new Size(maxX1 > maxX2 ? maxX1 : maxX2, maxY1 > maxY2 ? maxY1 : maxY2) + new Size(20, 20);
        }

        internal void Draw(Graphics graphics, int width, int height)
        {

            Bitmap offScreenBmp = new Bitmap(width, height);
            Graphics offScreenDC = Graphics.FromImage(offScreenBmp);

            foreach (ConnectionLine cl in this.ConnectionLines)
                cl.Draw(offScreenDC);
            foreach (TableView tv in this.Tables)
                tv.Draw(offScreenDC, conn);

            graphics.DrawImage(offScreenBmp, new Point(0, 0));
        }

        public TableView HitTestTable(Point point)
        {
            foreach (TableView tv in this.Tables)
                if (tv.Rectangle.Contains(point))
                    return tv;
            return null;
        }
        public Field HitTestField(Point point)
        {
            foreach (TableView tv in this.Tables)
                if (tv.Rectangle.Contains(point))
                {
                    Table table = conn.Database.Tables[tv.TableName];
                    if (point.Y - tv.Position.Y > Schema.Def_TitleHeight)
                    {
                        int i = (point.Y - tv.Position.Y - Schema.Def_TitleHeight) / Schema.Def_FieldHeight;
                        if (i < table.Fields.Count)
                            return table.Fields[i];
                    }
                }
            return null;
        }
    }

    public class TableView
    {
        public string TableName { get; set; }
        public Point Position { get; set; }
        public Size Size { get; set; }
        internal bool Selected;
        public bool ShowFull;
        public string SelectedField;
        public bool Modified = false;

        public Size RealSize
        {
            get
            {
                return ShowFull ? Size : new Size(Size.Width, Schema.Def_TitleHeight + 1);
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(this.Position, this.RealSize);
            }
        }

        private Rectangle lastDrawRect;

        internal void Draw(Graphics graphics, ConnectionSettings conn)
        {
            Size size = RealSize;
            // bir önceki çizim alanını temizle (flicker engellemek için)
            graphics.FillRectangle(Brushes.White, new Rectangle(lastDrawRect.Location, lastDrawRect.Size + new Size(1, 1)));

            // siyah çerçeve
            graphics.DrawRectangle(Pens.Black, new Rectangle(Position, size));
            // çerçeve içinde beyaz zemin
            graphics.FillRectangle(Brushes.White, new Rectangle(Position + new Size(1, 1), size - new Size(2, 2)));

            Rectangle rectTitle = new Rectangle(Position + new Size(1, 1), new Size(size.Width - 1, Schema.Def_TitleHeight));
            graphics.FillRectangle(Selected ? SystemBrushes.ActiveCaption : SystemBrushes.InactiveCaption, rectTitle);
            graphics.DrawString(this.TableName, Control.DefaultFont, Selected ? SystemBrushes.ActiveCaptionText : SystemBrushes.InactiveCaptionText, rectTitle.ToRectangleF(), GetTitleStringFormat());

            if (ShowFull)
            {
                StringFormat sf = GetFieldStringFormat();
                Table table = conn.Database.Tables[this.TableName];
                for (int i = 0; i < table.Fields.Count; i++)
                {
                    Field field = table.Fields[i];
                    Rectangle rectField = new Rectangle(Position + new Size(1, 1 + Schema.Def_TitleHeight + Schema.Def_FieldHeight * i), new Size(Size.Width - 2, Schema.Def_FieldHeight));
                    if (field.Name == SelectedField)
                        graphics.FillRectangle(SystemBrushes.Highlight, rectField);
                    rectField.Size -= new Size(20, 0);
                    rectField.Location += new Size(20, 0);
                    graphics.DrawImageUnscaled(Schema.ImageList.Images[field.IsPrimaryKey ? "key" : "field"], rectField.Location + new Size(-18, 2));
                    graphics.DrawString(field.Name, Control.DefaultFont, field.Name == SelectedField ? SystemBrushes.HighlightText : SystemBrushes.ControlText, rectField.ToRectangleF(), sf);
                }
            }

            lastDrawRect = new Rectangle(Position, size);
        }

        private StringFormat GetTitleStringFormat()
        {
            return new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.NoWrap
            };
        }

        private StringFormat GetFieldStringFormat()
        {
            return new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.NoWrap
            };
        }
    }

    public class ConnectionLine
    {
        public string FromTable { get; set; }
        public string FromField { get; set; }
        public string ToTable { get; set; }
        private Point startPoint, lastStartPoint;
        public Point StartPoint
        {
            get { return startPoint; }
            set
            {
                lastStartPoint = startPoint;
                if (lastStartPoint == new Point(0, 0)) lastStartPoint = value;
                startPoint = value;
            }
        }
        private Point endPoint, lastEndPoint;
        public Point EndPoint
        {
            get { return endPoint; }
            set
            {
                lastEndPoint = endPoint;
                if (lastEndPoint == new Point(0, 0)) lastEndPoint = value;
                endPoint = value;
            }
        }

        internal void Draw(Graphics graphics)
        {
            Pen p = new Pen(Color.White, 12f);
            graphics.DrawLine(p, lastStartPoint, lastEndPoint);

            p = new Pen(Color.Gray, 4f);
            graphics.DrawLine(p, startPoint, endPoint);

            Pair<Point> centerLine = startPoint.GetLinePart(endPoint, 20, LinePart.Center);
            p = new Pen(Color.Red, 4f);
            p.SetLineCap(LineCap.Flat, LineCap.ArrowAnchor, DashCap.Flat);
            p.DashStyle = DashStyle.Solid;
            graphics.DrawLine(p, centerLine.First, centerLine.Second);

        }
    }
}