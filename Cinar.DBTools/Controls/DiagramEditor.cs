using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.UICommands;
using Cinar.DBTools.Tools;
using Cinar.Database;

namespace Cinar.DBTools.Controls
{
    public partial class DiagramEditor : UserControl, IEditor
    {
        CommandManager cmdMan = new CommandManager();
        ConnectionSettings conn = null;
        public PropertyGrid propertyGrid;

        public FormMain MainForm { get; set; }

        public DiagramEditor()
        {
            InitializeComponent();

            conn = Provider.ActiveConnection;

            cmdMan.Commands = new CommandCollection(){
                new Command {
                    Execute = cmdToggleTableView,
                    Trigger = new CommandTrigger{Control=this, Event="DoubleClick"}
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
                        new CommandTrigger{Control=this, Event="PreviewKeyDown", Predicate=(e)=>(e as PreviewKeyDownEventArgs).KeyValue == 46} //Keys.Delete
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
            };
            cmdMan.SetCommandTriggers();
            //cmdMan.SetCommandControlsVisibility();
            cmdMan.SetCommandControlsEnable();

            Diagram.ImageList = imageListTree;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            correctPanelSize();
            MainForm.ObjectChanged += new EventHandler<DbObjectChangedArgs>(MainForm_ObjectChanged);

            if (CurrentSchema == null)
            {
                CurrentSchema = new Diagram();
                CurrentSchema.conn = conn;
                CurrentSchema.Name = "";
                panelPaint(true);
            }
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            cmdAddTables(null);
        }

        void MainForm_ObjectChanged(object sender, DbObjectChangedArgs e)
        {
            if (e.Object is Table && e.PropertyName == "Name")
            {
                TableView tv = CurrentSchema.Tables.Find(t => t.TableName == e.OldValue.ToString());
                if (tv != null)
                    tv.TableName = e.NewValue.ToString();
                foreach (ConnectionLine cl in CurrentSchema.ConnectionLines)
                {
                    if (cl.FromTable == e.OldValue.ToString()) cl.FromTable = e.NewValue.ToString();
                    if (cl.ToTable == e.OldValue.ToString()) cl.ToTable = e.NewValue.ToString();
                }
            }
            if (e.Object is Field && e.PropertyName == "Name")
            {
                Field f = e.Object as Field;
                TableView tv = CurrentSchema.Tables.Find(t => t.TableName == f.Table.Name);
                if (tv != null)
                {
                    tv.SelectedField = e.NewValue.ToString();
                }
            }

            panelPaint(true);
        }

        public Diagram CurrentSchema;

        private void cmdNewSchema(string arg)
        {
            ListBoxDialog std = new ListBoxDialog();
            std.StartPosition = FormStartPosition.CenterScreen;
            foreach (var item in conn.Database.Tables.OrderBy(t=>t.Name))
                std.ListBox.Items.Add(item);
            if (std.ShowDialog() == DialogResult.OK)
            {
                modified = true;
                createNewSchema(std.GetSelectedItems<Table>());
                correctPanelSize();
                panelPaint(true);
            }
        }

        private bool saveSchema()
        {
            if (string.IsNullOrEmpty(CurrentSchema.Name))
                CurrentSchema.Name = "New Diagram";

            if (conn.Schemas.IndexOf(CurrentSchema) == -1)
                conn.Schemas.Add(CurrentSchema);
            modified = false;
            return true;
        }

        private void createNewSchema(List<Table> tables)
        {
            addTablesToSchema(tables, true);
            arrangeTables(true);
            correctConnectionLinesPositions(CurrentSchema.Tables);
        }
        private void addTablesToSchema(List<Table> tables, bool circularLayout, int left = 10, int top = 10)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                Table tbl = tables[i];
                TableView tv = new TableView();
                //tv.Size = new Size(Diagram.Def_TableWidth, Diagram.Def_TitleHeight + tbl.Fields.Count * Diagram.Def_FieldHeight);
                tv.Position = new Point(left, top);
                tv.TableName = tbl.Name;
                tv.ShowFull = !circularLayout;

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
                        //cl.FromField = fld.Name;
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
                    tv.Position = new Point(20, 20 + i * (Diagram.Def_TitleHeight + 20));
                }
            }
        }
        private void correctPanelSize()
        {
            if (CurrentSchema != null)
            {
                Size size = CurrentSchema.CalculateTotalSize();
                
                if (size.Width > this.Parent.Size.Width)
                    this.Size = new Size(size.Width, this.Size.Height);

                if (size.Height > this.Parent.Size.Height)
                    this.Size = new Size(this.Size.Width, size.Height);
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
            if (tv1 == null || tv2 == null)
                return;

            Pair<Point> pair = tv1.Rectangle.FindClosestSideCenters(tv2.Rectangle);
            cl.StartPoint = pair.First;
            cl.EndPoint = pair.Second;
        }

        private void panelPaint(bool cls)
        {
            if (CurrentSchema != null)
            {
                Graphics g = this.CreateGraphics();
                if (cls)
                    g.FillRectangle(Brushes.White, new Rectangle(0, 0, this.Width, this.Height));

                // draw whole schema
                CurrentSchema.Draw(g, this.Font, this.Width, this.Height);
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

                if (propertyGrid.SelectedObject != null)
                    MainForm.SelectedObject = propertyGrid.SelectedObject;
            }
        }
        bool dragging = false;
        Size dragDelta;

        protected override void OnPaint(PaintEventArgs e)
        {
            panelPaint(true);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();

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

                    ConnectionLine cl = CurrentSchema.HitTestConnectionLine(e.Location);
                    if (cl != null) {
                        ConnectionLine lastSelectedCL = CurrentSchema.ConnectionLines.Find(c => c.Selected);
                        if (lastSelectedCL != null) lastSelectedCL.Selected = false;
                        cl.Selected = true;
                    }

                    panelPaint(false);
                }

                MainForm.cmdMan.SetCommandControlsVisibility(typeof(ToolStripMenuItem));
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (dragging)
            {
                selectedTVs.ForEach(tv => tv.Position = e.Location - dragDelta);
                correctConnectionLinesPositions(selectedTVs);
                correctPanelSize();
                panelPaint(false);
                modified = true;
            }
        }

        Point lastMousePos;
        protected override void OnMouseUp(MouseEventArgs e)
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
            modified = true;
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
                modified = true;
            }
        }

        private void cmdArrangeTables(string arg)
        {
            arrangeTables(true);
            correctPanelSize();
            panelPaint(true);
            modified = true;
        }

        private void cmdRemoveTable(string arg)
        {
            selectedTVs.ForEach(tv =>
            {
                CurrentSchema.Tables.Remove(tv);
                CurrentSchema.ConnectionLines
                    .RemoveAll(cl => cl.FromTable == tv.TableName || cl.ToTable == tv.TableName);
            });

            if (CurrentSchema.Tables.Count > 0)
                selectedTVs = new List<TableView> { CurrentSchema.Tables[0] };

            correctConnectionLinesPositions(selectedTVs);
            correctPanelSize();
            panelPaint(true);
            modified = true;
        }

        private void cmdCreateTable(string arg)
        {
            try
            {
                Table tbl = MainForm.CreateTable();

                TableView tv = new TableView();
                tv.Modified = true;
                tv.Selected = true;
                tv.ShowFull = true;
                tv.TableName = tbl.Name;
                tv.Position = lastMousePos;
                //tv.Size = new Size(Diagram.Def_TableWidth, Diagram.Def_TitleHeight + tbl.Fields.Count * Diagram.Def_FieldHeight);
                CurrentSchema.Tables.Add(tv);

                selectedTVs = new List<TableView> { tv };

                modified = true;
                correctPanelSize();
                panelPaint(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdSetAsDisplayField(string arg)
        {
            if (selectedTVs.Count != 1)
                return;

            conn.Database.Tables[selectedTVs[0].TableName].StringFieldName = selectedTVs[0].SelectedField;
            panelPaint(false);
            modified = true;
        }
        private bool modified;
        public bool Modified
        {
            get
            {
                return modified;
            }
        }
        public bool Save()
        {
            return saveSchema();
        }
        public string GetName()
        {
            return CurrentSchema.Name;
        }

        public override bool AllowDrop
        {
            get
            {
                return true;
            }
            set
            {
            }
        }
        protected override void OnDragEnter(DragEventArgs e)
        {
            Table tbl = e.Data.GetData(typeof(Table)) as Table;
            TableView tv = CurrentSchema.GetTableView(tbl.Name);
            if (tbl != null && tv==null && tbl.Database == conn.Database)
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }
        protected override void OnDragDrop(DragEventArgs e)
        {
            Table tbl = e.Data.GetData(typeof(Table)) as Table;
            Point pos = this.PointToClient(new Point(e.X, e.Y));
            addTablesToSchema(new List<Table>() { tbl}, false, pos.X, pos.Y);
            panelPaint(false);
        }
    }
}
