using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Cinar.Database;
using Cinar.UICommands;

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
                    Execute = cmdSaveSchema,
                    Trigger = new CommandTrigger{ Control = menuSave}
                },
                new Command {
                    Execute = (arg)=>{Close();},
                    Trigger = new CommandTrigger{Control=menuExit}
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
                    Execute = cmdRemoveTable,
                    Triggers = new List<CommandTrigger>{ 
                        new CommandTrigger{Control=menuRemove},
                        new CommandTrigger{Control=panel, Event="PreviewKeyDown", Predicate=(e)=>(e as PreviewKeyDownEventArgs).KeyValue == 46} //Keys.Delete
                    },
                    IsVisible = () => selectedTVs.Count > 0
                },
                new Command {
                    Execute = cmdCreateTable,
                    Trigger = new CommandTrigger{Control=menuCreateTable}
                },
                new Command {
                    Execute = cmdSetAsDisplayField,
                    Trigger = new CommandTrigger{Control=menuSetAsDisplayField},
                    IsVisible = () => selectedTVs.Count > 0 && !string.IsNullOrEmpty(selectedTVs[0].SelectedField)
                },
                new Command {
                    Execute = cmdSetAsPrimaryKey,
                    Trigger = new CommandTrigger{Control=menuSetAsPrimaryKey},
                    IsVisible = () => selectedTVs.Count > 0 && !string.IsNullOrEmpty(selectedTVs[0].SelectedField)
                },
                new Command {
                    Execute = cmdAddField,
                    Trigger = new CommandTrigger{Control=menuAddField},
                    IsVisible = () => selectedTVs.Count > 0
                },
            };
            cmdMan.SetCommandTriggers();
            //cmdMan.SetCommandControlsVisibility();
            cmdMan.SetCommandControlsEnable();

            this.Text = "Çınar ER Diagram for database: " + conn;
            Schema.ImageList = imageListTree;
        }

        protected override void OnLoad(EventArgs e)
        {
            panel.Width = splitContainer.Panel1.Width;
            panel.Height = splitContainer.Panel1.Height;

            if (this.CurrentSchema == null)
                cmdNewSchema(null);

            base.OnLoad(e);

        }

        public Schema CurrentSchema;

        private void cmdNewSchema(string arg)
        {
            ListBoxDialog std = new ListBoxDialog();
            foreach (var item in conn.Database.Tables.OrderBy(t=>t.Name))
                std.ListBox.Items.Add(item);
            if (std.ShowDialog() == DialogResult.OK)
            {
                createNewSchema(std.GetSelectedItems<Table>());
                correctPanelSize();
                panelPaint(true);
            }
        }
        public FormMain Opener;
        private void cmdSaveSchema(string arg)
        {
            if (string.IsNullOrEmpty(CurrentSchema.Name))
            {
                MessageBox.Show("Please enter a name for the current schema", "Çınar Database Tools");
                return;
            }

            if (conn.Schemas.IndexOf(CurrentSchema) == -1)
                conn.Schemas.Add(CurrentSchema);
            Opener.SaveConnections();
        }
        private void createNewSchema(List<Table> tables)
        {
            CurrentSchema = new Schema();
            CurrentSchema.conn = conn;
            CurrentSchema.Name = "";
            addTablesToSchema(tables, true);
            arrangeTables(true);
            correctConnectionLinesPositions(CurrentSchema.Tables);
        }
        private void addTablesToSchema(List<Table> tables, bool circularLayout)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                Table tbl = tables[i];
                TableView tv = new TableView();
                tv.Size = new Size(Schema.Def_TableWidth, Schema.Def_TitleHeight + tbl.Fields.Count * Schema.Def_FieldHeight);
                tv.TableName = tbl.Name;

                CurrentSchema.Tables.Add(tv);
            }

            CurrentSchema.ConnectionLines.Clear();

            for (int i = 0; i < conn.Database.Tables.Count; i++)
            {
                Table tbl = conn.Database.Tables[i];
                if (CurrentSchema.GetTableView(tbl.Name) == null)
                    continue;

                foreach (Field fld in tbl.Fields)
                    if (fld.ReferenceField != null)
                    {
                        TableView tv = CurrentSchema.GetTableView(fld.ReferenceField.Table.Name);
                        if(tv==null)
                            continue;

                        ConnectionLine cl = new ConnectionLine();
                        cl.FromField = fld.Name;
                        cl.FromTable = tbl.Name;
                        cl.ToTable = tv.TableName;
                        CurrentSchema.ConnectionLines.Add(cl);
                    }
            }
        }
        private void arrangeTables(bool circularLayout)
        {
            if (circularLayout)
            {
                Point center = new Point(400, 400);
                Size caplar = new Size(400, 300);
                for (int i = 0; i < CurrentSchema.Tables.Count; i++)
                {
                    TableView tv = CurrentSchema.Tables[i];
                    double aci = 360d / (double)CurrentSchema.Tables.Count * i * Math.PI / 180d;
                    tv.Position = center + new Size(Convert.ToInt32(caplar.Width * Math.Cos(aci)), Convert.ToInt32(caplar.Height * Math.Sin(aci)));
                }
            }
            else
            {
                for (int i = 0; i < CurrentSchema.Tables.Count; i++)
                {
                    TableView tv = CurrentSchema.Tables[i];
                    tv.Position = new Point(20, 20 + i * (Schema.Def_TitleHeight + 20));
                }
            }
        }
        private void correctPanelSize()
        {
            if (CurrentSchema != null)
            {
                Size size = CurrentSchema.CalculateTotalSize();
                
                if (size.Width > panel.Parent.Size.Width)
                    panel.Size = new Size(size.Width, panel.Size.Height);

                if (size.Height > panel.Parent.Size.Height)
                    panel.Size = new Size(panel.Size.Width, size.Height);
            }
        }
        private void correctConnectionLinesPositions(List<TableView> tables)
        {
            tables
                .ForEach(tv =>
                        CurrentSchema.ConnectionLines
                            .FindAll(cl => cl.FromTable == tv.TableName || cl.ToTable == tv.TableName)
                                .ForEach(correctConnectionLinePosition));
        }
        private void correctConnectionLinePosition(ConnectionLine cl)
        {
            TableView tv1 = CurrentSchema.GetTableView(cl.FromTable);
            TableView tv2 = CurrentSchema.GetTableView(cl.ToTable);
            Pair<Point> pair = tv1.Rectangle.FindClosestSideCenters(tv2.Rectangle);
            cl.StartPoint = pair.First;
            cl.EndPoint = pair.Second;
        }

        private void panelPaint(bool cls)
        {
            if (CurrentSchema != null)
            {
                Graphics g = panel.CreateGraphics();
                if (cls)
                    g.FillRectangle(Brushes.White, new Rectangle(0, 0, panel.Width, panel.Height));

                // draw whole schema
                CurrentSchema.Draw(g, panel.Width, panel.Height);
            }
        }

        List<TableView> _selectedTvs = new List<TableView>();
        List<TableView> selectedTVs 
        {
            get {
                return _selectedTvs;
            }
            set 
            {
                _selectedTvs.ForEach(tv => { tv.Selected = false; if (value != null && !value.Contains(tv)) tv.SelectedField = null; });
                _selectedTvs = value;
                if (_selectedTvs != null)
                    _selectedTvs.ForEach(tv => tv.Selected = true);
                else
                    _selectedTvs = new List<TableView>();

                if (selectedTVs.Count == 1)
                {
                    if (!string.IsNullOrEmpty(selectedTVs[0].SelectedField))
                        propertyGrid.SelectedObject = conn.Database.Tables[selectedTVs[0].TableName].Fields[selectedTVs[0].SelectedField];
                    else
                        propertyGrid.SelectedObject = conn.Database.Tables[selectedTVs[0].TableName];
                }
                else if (selectedTVs.Count == 0)
                    propertyGrid.SelectedObject = CurrentSchema;
                else
                    propertyGrid.SelectedObject = null;

            }
        }
        bool dragging = false;
        Size dragDelta;

        private void panelOnPaint(object sender, PaintEventArgs e)
        {
            panelPaint(true);
        }

        private void panelMouseDown(object sender, MouseEventArgs e)
        {
            panel.Focus();

            if (CurrentSchema != null)
            {
                TableView tv = CurrentSchema.HitTestTable(e.Location);
                if (tv != null)
                {
                    dragDelta = (Size)(e.Location - (Size)tv.Position);
                    dragging = true;
                    CurrentSchema.Tables.Remove(tv);
                    CurrentSchema.Tables.Add(tv);

                    Field selectedField = CurrentSchema.HitTestField(e.Location);
                    tv.SelectedField = selectedField == null ? null : selectedField.Name;
                    selectedTVs = new List<TableView> { tv };
                }
                else
                {
                    selectedTVs = null;
                    panelPaint(false);
                }

                cmdMan.SetCommandControlsVisibility(typeof(ToolStripMenuItem));
            }
        }

        private void panelMouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                selectedTVs.ForEach(tv => tv.Position = e.Location - dragDelta);
                correctConnectionLinesPositions(selectedTVs);
                correctPanelSize();
                panelPaint(false);
            }
        }

        Point lastMousePos;
        private void panelMouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            lastMousePos = e.Location;
        }

        private void cmdToggleTableView(string arg)
        {
            selectedTVs.ForEach(tv=>tv.ShowFull = !tv.ShowFull);
            correctConnectionLinesPositions(selectedTVs);
            correctPanelSize();
            panelPaint(false);
        }

        private void cmdAddTables(string arg)
        {
            ListBoxDialog std = new ListBoxDialog();
            foreach (var item in conn.Database.Tables.Except(CurrentSchema.Tables.Select<TableView, Table>(tv => conn.Database.Tables[tv.TableName])).OrderBy(t => t.Name))
                std.ListBox.Items.Add(item);
            if (std.ShowDialog() == DialogResult.OK)
            {
                addTablesToSchema(std.GetSelectedItems<Table>(), false);
                correctConnectionLinesPositions(CurrentSchema.Tables);
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
            selectedTVs.ForEach(tv =>
            {
                CurrentSchema.Tables.Remove(tv);
                CurrentSchema.ConnectionLines
                    .RemoveAll(cl => cl.FromTable == tv.TableName || cl.ToTable == tv.TableName);
            });
            correctConnectionLinesPositions(selectedTVs);
            correctPanelSize();
            panelPaint(true);
        }

        private void cmdCreateTable(string arg)
        {
            Table tbl = new Table {
                Name = "new_table",
            };
            tbl.Fields = new FieldCollection(tbl) {new Field
                            {
                                Name = "Id",
                                FieldType = Cinar.Database.DbType.Int32,
                                IsAutoIncrement = true,
                                IsNullable = false
                            }};

            tbl.Keys = new KeyCollection(tbl) {new Key
                          {
                              IsPrimary = true,
                              FieldNames = new List<string> {"Id"},
                              Name = "PK_new_table_" + DateTime.Now.Millisecond
                          }};

            conn.Database.Tables.Add(tbl);

            try
            {
                conn.Database.CreateTable(tbl, null, false);

                TableView tv = new TableView();
                tv.Modified = true;
                tv.Selected = true;
                tv.ShowFull = true;
                tv.TableName = tbl.Name;
                tv.Position = lastMousePos;
                tv.Size = new Size(Schema.Def_TableWidth, Schema.Def_TitleHeight + tbl.Fields.Count * Schema.Def_FieldHeight);
                CurrentSchema.Tables.Add(tv);

                selectedTVs = new List<TableView> { tv };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Çınar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Database.Tables.Remove(tbl);
            }

            correctPanelSize();
            panelPaint(true);
        }

        private void cmdAddField(string arg)
        {
            if (selectedTVs.Count != 1)
                return;

            Field f = new Field();
            f.Name = "new_field";
            f.FieldType = Cinar.Database.DbType.VarChar;
            f.Length = 20;
            f.IsNullable = true;
            conn.Database.Tables[selectedTVs[0].TableName].Fields.Add(f);

            try
            {
                conn.Database.AlterTableAddColumn(f);

                selectedTVs[0].Size += new Size(0, Schema.Def_FieldHeight);
                selectedTVs[0].Modified = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Çınar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Database.Tables[selectedTVs[0].TableName].Fields.Remove(f);
            }

            correctPanelSize();
            panelPaint(true);
        }

        private void cmdSetAsDisplayField(string arg)
        {
            if (selectedTVs.Count != 1)
                return;

            conn.Database.Tables[selectedTVs[0].TableName].StringFieldName = selectedTVs[0].SelectedField;
            panelPaint(false);
        }

        private void cmdSetAsPrimaryKey(string arg)
        {
            if (selectedTVs.Count != 1)
                return;

            Key key = conn.Database.Tables[selectedTVs[0].TableName].Keys.Find(k => k.IsPrimary);
            if (key == null)
            {
                key = new Key();
                key.IsPrimary = true;
                key.FieldNames = new List<string>() { selectedTVs[0].SelectedField };
                key.IsUnique = true;
                key.Name = "PRIMARY";
            }
            else
            {
                key.FieldNames = new List<string>() { selectedTVs[0].SelectedField };
            }
            panelPaint(false);
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Name")
            {
                if (propertyGrid.SelectedObject is Table && MessageBox.Show("The table name will be changed on database! Continue?", "Çınar Database Tools", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    try
                    {
                        conn.Database.AlterTableRename(e.OldValue.ToString(), e.ChangedItem.Value.ToString());
                        CurrentSchema.GetTableView(e.OldValue.ToString()).TableName = e.ChangedItem.Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Çınar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (propertyGrid.SelectedObject is Field && MessageBox.Show("The field name will be changed on database! Continue?", "Çınar Database Tools", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    try
                    {
                        conn.Database.AlterTableRenameColumn(selectedTVs[0].TableName, e.OldValue.ToString(), e.ChangedItem.Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Çınar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (propertyGrid.SelectedObject is Field && MessageBox.Show("The field definition will be changed on database! Continue?", "Çınar Database Tools", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                try
                {
                    conn.Database.AlterTableChangeColumn(propertyGrid.SelectedObject as Field);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Çınar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            panelPaint(false);
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
        internal bool Selected;
        public bool ShowFull;
        public string SelectedField;
        public bool Modified = false;

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
            graphics.FillRectangle(Brushes.White, new Rectangle(lastDrawRect.Location, lastDrawRect.Size+new Size(1,1)));
            
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
