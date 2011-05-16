using System;
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
    public class Diagram
    {
        public static int Def_TableWidth = 150;
        public static int Def_TitleHeight = 20;
        public static int Def_ColumnHeight = 20;
        public static ImageList ImageList;
        [XmlIgnore]
        public ConnectionSettings conn;
        [XmlIgnore]
        public object SelectedObject
        {
            get { return selectedObject; }
            set {
                selectedObject = value;
                Tables.ForEach(tv => { tv.Selected = tv == selectedObject; });
                ConnectionLines.ForEach(cl => { cl.Selected = cl == selectedObject; });
                if (selectedObject is Column)
                {
                    Column f = selectedObject as Column;
                    Tables.ForEach(tv => { if (tv.TableName == f.Table.Name) tv.SelectedColumn = f.Name; else tv.SelectedColumn = ""; });
                }
                else
                    Tables.ForEach(tv => { tv.SelectedColumn = ""; });
            }
        }
        private object selectedObject;

        public string Name { get; set; }
        public ListTableView Tables { get; set; }
        public List<ConnectionLine> ConnectionLines { get; set; }

        public TableView GetTableView(string tableName)
        {
            foreach (TableView tv in Tables)
                if (tv.TableName == tableName)
                    return tv;
            return null;
        }

        public Diagram()
        {
            Tables = new ListTableView(this);
            ConnectionLines = new List<ConnectionLine>();
        }

        public Size CalculateTotalSize()
        {
            if (Tables.Count == 0)
                return new Size(20, 20);

            int maxX1 = Tables.Max(tv => tv.Position.X + tv.RealSize.Width);
            int maxY1 = Tables.Max(tv => tv.Position.Y + tv.RealSize.Height);

            int maxX2 = ConnectionLines.Count > 0 ? ConnectionLines.Max(cl => new int[] { cl.StartPoint.X, cl.EndPoint.X }.Max()) : 0;
            int maxY2 = ConnectionLines.Count > 0 ? ConnectionLines.Max(cl => new int[] { cl.StartPoint.Y, cl.EndPoint.Y }.Max()) : 0;

            return new Size(maxX1 > maxX2 ? maxX1 : maxX2, maxY1 > maxY2 ? maxY1 : maxY2) + new Size(20, 20);
        }

        internal void Draw(Graphics graphics, Font font, int width, int height)
        {
            Bitmap offScreenBmp = new Bitmap(width, height);
            Graphics offScreenDC = Graphics.FromImage(offScreenBmp);

            if (this.Tables.Count == 0)
            { 
                offScreenDC.DrawString("double click (or drag and drop tables)", font, Brushes.Gray, 50, 50);
            }
            else
            {
                foreach (ConnectionLine cl in this.ConnectionLines)
                    cl.Draw(offScreenDC);
                foreach (TableView tv in this.Tables)
                    tv.Draw(offScreenDC, font, conn);
            }

            graphics.DrawImage(offScreenBmp, new Point(0, 0));
        }

        public TableView HitTestTable(Point point)
        {
            foreach (TableView tv in this.Tables)
                if (tv.Rectangle.Contains(point))
                    return tv;
            return null;
        }
        public Column HitTestColumn(Point point)
        {
            foreach (TableView tv in this.Tables)
                if (tv.Rectangle.Contains(point))
                {
                    Table table = conn.Database.Tables[tv.TableName];
                    if (table == null) return null;//***

                    if (point.Y - tv.Position.Y > Diagram.Def_TitleHeight)
                    {
                        int i = (point.Y - tv.Position.Y - Diagram.Def_TitleHeight) / Diagram.Def_ColumnHeight;
                        if (i < table.Columns.Count)
                            return table.Columns[i];
                    }
                }
            return null;
        }
        public ConnectionLine HitTestConnectionLine(Point point)
        {
            foreach (ConnectionLine cl in this.ConnectionLines)
            {
                int d = cl.FindDistanceTo(point);
                if (d < 16 && d > -1)
                    return cl;
            }
            return null;
        }
    }

    public class ListTableView : List<TableView>
    {
        [XmlIgnore]
        internal Diagram Diagram;

        public ListTableView(Diagram schema)
        {
            this.Diagram = schema;
        }

        public new int Add(TableView tv)
        {
            tv.Diagram = Diagram;
            base.Add(tv);
            return base.Count;
        }
    }

    public class TableView
    {
        public string TableName { get; set; }
        public Point Position { get; set; }
        [XmlIgnore]
        public Size Size { 
            get 
            {
                int columnCount = 0;
                if (Diagram.conn.Database.Tables[TableName] != null)
                    columnCount = Diagram.conn.Database.Tables[TableName].Columns.Count;
                return new Size(Diagram.Def_TableWidth, Diagram.Def_TitleHeight + columnCount * Diagram.Def_ColumnHeight); 
            } 
        }
        internal bool Selected;
        public bool ShowFull;
        public string SelectedColumn;
        public bool Modified = false;

        [XmlIgnore]
        internal Diagram Diagram { get; set; }

        public Size RealSize
        {
            get
            {
                return ShowFull ? Size : new Size(Size.Width, Diagram.Def_TitleHeight + 1);
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

        internal void Draw(Graphics graphics, Font font, ConnectionSettings conn)
        {
            Table table = conn.Database.Tables[this.TableName];
            if (table == null)
                return; //***

            Size size = RealSize;
            // bir önceki çizim alanını temizle (flicker engellemek için)
            graphics.FillRectangle(Brushes.White, new Rectangle(lastDrawRect.Location, lastDrawRect.Size + new Size(1, 1)));

            // siyah çerçeve
            graphics.DrawRectangle(Pens.Black, new Rectangle(Position, size));
            // çerçeve içinde beyaz zemin
            graphics.FillRectangle(Brushes.White, new Rectangle(Position + new Size(1, 1), size - new Size(2, 2)));

            Rectangle rectTitle = new Rectangle(Position + new Size(1, 1), new Size(size.Width - 1, Diagram.Def_TitleHeight));
            graphics.FillRectangle(Selected ? SystemBrushes.ActiveCaption : SystemBrushes.InactiveCaption, rectTitle);
            graphics.DrawString(this.TableName, font, Selected ? SystemBrushes.ActiveCaptionText : SystemBrushes.InactiveCaptionText, rectTitle.ToRectangleF(), GetTitleStringFormat());

            if (ShowFull)
            {
                StringFormat sf = GetColumnStringFormat();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Column column = table.Columns[i];
                    Rectangle rectColumn = new Rectangle(Position + new Size(1, 1 + Diagram.Def_TitleHeight + Diagram.Def_ColumnHeight * i), new Size(Size.Width - 2, Diagram.Def_ColumnHeight));
                    if (column.Name == SelectedColumn)
                        graphics.FillRectangle(SystemBrushes.Highlight, rectColumn);
                    rectColumn.Size -= new Size(20, 0);
                    rectColumn.Location += new Size(20, 0);
                    graphics.DrawImageUnscaled(Diagram.ImageList.Images[column.IsPrimaryKey ? "key" : "column"], rectColumn.Location + new Size(-18, 2));
                    graphics.DrawString(column.Name, font, column.Name == SelectedColumn ? SystemBrushes.HighlightText : SystemBrushes.ControlText, rectColumn.ToRectangleF(), sf);
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

        private StringFormat GetColumnStringFormat()
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

            p = new Pen(Selected ? Color.Blue : Color.Gray, 4f);
            graphics.DrawLine(p, startPoint, endPoint);

            Pair<Point> centerLine = startPoint.GetLinePart(endPoint, 20, LinePart.Center);
            p = new Pen(Color.Red, 4f);
            p.SetLineCap(LineCap.Flat, LineCap.ArrowAnchor, DashCap.Flat);
            p.DashStyle = DashStyle.Solid;
            graphics.DrawLine(p, centerLine.First, centerLine.Second);

        }

        public int FindDistanceTo(Point point)
        {
            bool isOutX = (point.X < StartPoint.X && point.X < EndPoint.X) || (point.X > StartPoint.X && point.X > EndPoint.X);
            bool isOutY = (point.Y < StartPoint.Y && point.Y < EndPoint.Y) || (point.Y > StartPoint.Y && point.Y > EndPoint.Y);
            if (isOutX || isOutY)
                return -1;

            int A = StartPoint.Y - EndPoint.Y;
            int B = EndPoint.X - StartPoint.X;
            int C = StartPoint.Y * (StartPoint.X - EndPoint.X) + StartPoint.X * (EndPoint.Y - StartPoint.Y);

            return Convert.ToInt32(Math.Abs(A * point.X + B * point.Y + C) / Math.Sqrt(A * A + B * B));
        }

        [XmlIgnore]
        internal bool Selected { get; set; }

        public string ForeignKeyName { get; set; }
    }
}
