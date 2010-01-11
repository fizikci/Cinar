using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Database;
using Cinar.Extensions;
using Cinar.UICommands;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;

namespace Cinar.DBTools.Tools
{
    public partial class FormERDiagram : Form
    {
        CommandManager cmdMan = new CommandManager();
        ConnectionSettings conn = null;

        public FormERDiagram()
        {
            InitializeComponent();

            conn = Provider.ActiveConnection;

            cmdMan.Commands = new CommandCollection(){
                new Command {
                    Execute = cmdNewSchema,
                    Trigger = new CommandTrigger{ Control = menuNew}
                },
                new Command {
                    Execute = cmdToggleTableView,
                    Trigger = new CommandTrigger{Control=panel, Event="DoubleClick"}
                },
                new Command {
                    Execute = cmdAddTables,
                    Trigger = new CommandTrigger{Control=menuAddTables}
                },
                new Command {
                    Execute = cmdArrangeTables,
                    Trigger = new CommandTrigger{Control=menuArrangeTables}
                },
                new Command {
                    Execute = (arg)=>{Close();},
                    Trigger = new CommandTrigger{Control=menuExit}
                },
                new Command {
                    Execute = cmdRemoveTable,
                    Trigger = new CommandTrigger{Control=menuRemove}
                },
                new Command {
                    Execute = cmdSetAsDisplayField,
                    Trigger = new CommandTrigger{Control=menuSetAsDisplayField}
                },
                new Command {
                    Execute = cmdSetAsPrimaryKey,
                    Trigger = new CommandTrigger{Control=menuSetAsPrimaryKey}
                },
            };
            cmdMan.SetCommandTriggers();
            //cmdMan.SetCommandControlsVisibility();
            cmdMan.SetCommandControlsEnable();

            this.Text = "Çınar ER Diagram for database: " + conn;
            Schema.ImageList = imageListTree;
        }

        Schema currentSchema;

        private void cmdNewSchema(string arg)
        {
            SelectTableDialog std = new SelectTableDialog();
            if (std.ShowDialog() == DialogResult.OK)
            {
                createNewSchema(std.SelectedTables);
                correctPanelSize();
                panelPaint(true);
            }
        }

        private void createNewSchema(List<Table> tables)
        {
            currentSchema = new Schema();
            currentSchema.conn = conn;
            currentSchema.Name = conn.Database.Name + " Schema";
            addTablesToSchema(tables, true);
        }

        private void addTablesToSchema(List<Table> tables, bool circularLayout)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                Table tbl = tables[i];
                TableView tv = new TableView();
                tv.Size = new Size(Schema.Def_TableWidth, Schema.Def_TitleHeight + tbl.Fields.Count * Schema.Def_FieldHeight);
                tv.TableName = tbl.Name;

                currentSchema.Tables.Add(tv);
            }

            arrangeTables(circularLayout);

            currentSchema.ConnectionLines.Clear();

            for (int i = 0; i < conn.Database.Tables.Count; i++)
            {
                Table tbl = conn.Database.Tables[i];
                if (currentSchema.GetTableView(tbl.Name) == null)
                    continue;

                foreach (Field fld in tbl.Fields)
                    if (fld.ReferenceField != null)
                    {
                        TableView tv = currentSchema.GetTableView(fld.ReferenceField.Table.Name);
                        if(tv==null)
                            continue;

                        ConnectionLine cl = new ConnectionLine();
                        cl.FromField = fld.Name;
                        cl.FromTable = tbl.Name;
                        cl.ToTable = tv.TableName;
                        TableView fromTV = currentSchema.GetTableView(cl.FromTable);
                        cl.StartPoint = fromTV.Rectangle.FindClosestSideCenterTo(tv.Rectangle);
                        cl.EndPoint = tv.Rectangle.FindClosestSideCenterTo(fromTV.Rectangle);
                        currentSchema.ConnectionLines.Add(cl);
                    }
            }
        }

        private void arrangeTables(bool circularLayout)
        {
            if (circularLayout)
            {
                Point center = new Point(400, 400);
                Size caplar = new Size(400, 300);
                for (int i = 0; i < currentSchema.Tables.Count; i++)
                {
                    TableView tv = currentSchema.Tables[i];
                    double aci = 360d / (double)currentSchema.Tables.Count * i * Math.PI / 180d;
                    tv.Position = center + new Size(Convert.ToInt32(caplar.Width * Math.Cos(aci)), Convert.ToInt32(caplar.Height * Math.Sin(aci)));
                }
            }
            else
            {
                for (int i = 0; i < currentSchema.Tables.Count; i++)
                {
                    TableView tv = currentSchema.Tables[i];
                    tv.Position = new Point(20, 20 + i * (Schema.Def_TitleHeight + 20));
                }
            }
        }

        private void correctPanelSize()
        {
            if(currentSchema!=null)
                panel.Size = currentSchema.CalculateTotalSize();
        }

        private void panelPaint(bool cls)
        {
            if (currentSchema != null)
            {
                Graphics g = panel.CreateGraphics();
                if (cls)
                    g.FillRectangle(Brushes.White, new Rectangle(0, 0, panel.Width, panel.Height));
                currentSchema.Draw(g, panel.Width, panel.Height);
            }
        }

        TableView selectedTV = null;
        bool dragging = false;
        Size dragDelta;

        private void panelOnPaint(object sender, PaintEventArgs e)
        {
            panelPaint(true);
        }

        private void panelMouseDown(object sender, MouseEventArgs e)
        {
            if (currentSchema != null)
            {
                TableView tv = currentSchema.HitTestTable(e.Location);
                if (tv != null)
                {
                    if (selectedTV != null)
                    {
                        selectedTV.Selected = false;
                        selectedTV.SelectedField = null;
                    }
                    selectedTV = tv;
                    dragDelta = (Size)(e.Location - (Size)selectedTV.Position);
                    dragging = true;
                    selectedTV.Selected = true;
                    currentSchema.Tables.Remove(selectedTV);
                    currentSchema.Tables.Add(selectedTV);
                    Field selectedField = currentSchema.HitTestField(e.Location);
                    selectedTV.SelectedField = selectedField == null ? null : selectedField.Name;
                }
            }
        }

        private void panelMouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                selectedTV.Position = e.Location - dragDelta;
                currentSchema.ConnectionLines.FindAll(cl => cl.FromTable == selectedTV.TableName || cl.ToTable == selectedTV.TableName).ForEach(cl =>
                {
                    TableView tv1 = currentSchema.GetTableView(cl.FromTable);
                    TableView tv2 = currentSchema.GetTableView(cl.ToTable);
                    cl.StartPoint = tv1.Rectangle.FindClosestSideCenterTo(tv2.Rectangle);
                    cl.EndPoint = tv2.Rectangle.FindClosestSideCenterTo(tv1.Rectangle);
                });
                correctPanelSize();
                panelPaint(false);
            }
        }

        private void panelMouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            if (selectedTV != null)
            {
                if(selectedTV.SelectedField!=null)
                    propertyGrid.SelectedObject = conn.Database.Tables[selectedTV.TableName].Fields[selectedTV.SelectedField];
                else
                    propertyGrid.SelectedObject = conn.Database.Tables[selectedTV.TableName];
                panelPaint(false);
            }
        }

        private void cmdToggleTableView(string arg)
        {
            selectedTV.ShowFull = !selectedTV.ShowFull;
            panelPaint(false);
            correctPanelSize();
        }

        private void cmdAddTables(string arg)
        {

            SelectTableDialog std = new SelectTableDialog();
            if (std.ShowDialog() == DialogResult.OK)
            {
                addTablesToSchema(std.SelectedTables.Except(currentSchema.Tables.ConvertAll<Table>(tv => conn.Database.Tables[tv.TableName])).ToList(), false);
                correctPanelSize();
                panelPaint(true);
            }
        }

        private void cmdArrangeTables(string arg)
        {
            arrangeTables(true);
            correctPanelSize();
            panelPaint(true);
        }

        private void cmdRemoveTable(string arg)
        {
            currentSchema.Tables.Remove(selectedTV);
            correctPanelSize();
            panelPaint(true);
        }

        private void cmdSetAsDisplayField(string arg)
        {
            conn.Database.Tables[selectedTV.TableName].StringFieldName = selectedTV.SelectedField;
            panelPaint(false);
        }

        private void cmdSetAsPrimaryKey(string arg)
        {
            Key key = conn.Database.Tables[selectedTV.TableName].Keys.Find(k=>k.IsPrimary);
            if (key == null)
            {
                key = new Key();
                key.IsPrimary = true;
                key.FieldNames = new List<string>() { selectedTV.SelectedField };
                key.IsUnique = true;
                key.Name = "PRIMARY";
            }
            else
            {
                key.FieldNames = new List<string>() { selectedTV.SelectedField };
            }
            panelPaint(false);
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Name")
            {
                currentSchema.GetTableView(e.OldValue.ToString()).TableName = e.ChangedItem.Value.ToString();
                panelPaint(false);
            }
        }
    }

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

            int maxX2 = ConnectionLines.Count > 0 ? ConnectionLines.Max(cl => new int[]{cl.StartPoint.X,cl.EndPoint.X}.Max()) : 0;
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
        public bool Selected;
        public bool ShowFull;
        public string SelectedField;

        public Size RealSize {
            get {
                return ShowFull ? Size : new Size(Size.Width, Schema.Def_TitleHeight + 1);
            }
        }

        public Rectangle Rectangle {
            get {
                return new Rectangle(this.Position, this.RealSize);
            }
        }

        private Rectangle lastDrawRect;

        internal void Draw(Graphics graphics, ConnectionSettings conn)
        {
            Size size = RealSize;
            // bir önceki çizim alanını temizle (flicker engellemek için)
            graphics.FillRectangle(Brushes.White, lastDrawRect);
            
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

        public ConnectionLine()
        {
        }

        internal void Draw(Graphics graphics)
        {
            Pen p = new Pen(Color.White, 8f);
            graphics.DrawLine(p, lastStartPoint, lastEndPoint);
            
            p = new Pen(Color.Gray, 4f);
            p.SetLineCap(LineCap.Round, LineCap.ArrowAnchor, DashCap.Flat);
            p.DashStyle = DashStyle.Solid;
            graphics.DrawLine(p, startPoint, endPoint);
        }
    }
}
