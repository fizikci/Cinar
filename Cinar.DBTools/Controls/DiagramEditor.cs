using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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

            if (CurrentSchema == null)
            {
                CurrentSchema = new Diagram();
                CurrentSchema.conn = conn;
                CurrentSchema.Name = "";
            }

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
                    Execute = cmdRemoveObject,
                    Triggers = new List<CommandTrigger>{ 
                        new CommandTrigger{Control=menuRemove},
                        new CommandTrigger{Control=this, Event="PreviewKeyDown", Predicate=(e)=>(e as PreviewKeyDownEventArgs).KeyValue == 46} //Keys.Delete
                    },
                    IsVisible = () => CurrentSchema.SelectedObject is TableView
                },
                new Command {
                    Execute = cmdCreateTable,
                    Trigger = new CommandTrigger{Control=menuCreateTable}
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
            panelPaint(true);
            MainForm.ObjectChanged += new EventHandler<DbObjectChangedArgs>(MainForm_ObjectChanged);
            MainForm.ObjectAdded += new EventHandler<DbObjectAddedArgs>(MainForm_ObjectAdded);
            MainForm.ObjectRemoved += new EventHandler<DbObjectRemovedArgs>(MainForm_ObjectRemoved);
        }

        void MainForm_ObjectRemoved(object sender, DbObjectRemovedArgs e)
        {
            if (e.Object is Table && (e.Object as Table).Database == conn.Database)
            {
                TableView tv = CurrentSchema.GetTableView((e.Object as Table).Name);
                CurrentSchema.Tables.Remove(tv);
                CurrentSchema.ConnectionLines.RemoveAll(cl => cl.ToTable == tv.TableName || cl.FromTable == tv.TableName);
            }
            if (e.Object is ForeignKeyConstraint && (e.Object as ForeignKeyConstraint).Table.Database == conn.Database)
            {
                CurrentSchema.ConnectionLines.RemoveAll(cl => cl.ForeignKeyName == (e.Object as ForeignKeyConstraint).RefConstraintName);
            }
            panelPaint(true);
        }

        void MainForm_ObjectAdded(object sender, DbObjectAddedArgs e)
        {
            
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

                foreach (ForeignKeyConstraint fk in tbl.Constraints.Where(c => c is ForeignKeyConstraint))
                {
                    TableView tv = CurrentSchema.GetTableView(fk.RefTableName);
                    if (tv == null)
                        continue;
                    ConnectionLine cl = new ConnectionLine();
                    cl.FromTable = tbl.Name;
                    cl.ToTable = tv.TableName;
                    cl.ForeignKeyName = fk.Name;
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
                    tv.Position = new Point(20 + i * (Diagram.Def_TitleHeight + 20), 20 + i * (Diagram.Def_TitleHeight + 20));
                }
            }
        }
        private void correctPanelSize()
        {
            if (CurrentSchema != null)
            {
                Size size = CurrentSchema.CalculateTotalSize();
                
                if (size.Width > this.MinimumSize.Width)
                    this.Size = new Size(size.Width, this.Size.Height);

                if (size.Height > this.MinimumSize.Height)
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
                    CurrentSchema.SelectedObject = tv;
                    propertyGrid.SelectedObject = conn.Database.Tables[tv.TableName];

                    Field selectedField = CurrentSchema.HitTestField(e.Location);
                    if (selectedField != null)
                    {
                        CurrentSchema.SelectedObject = selectedField;
                        propertyGrid.SelectedObject = selectedField;
                    }
                }
                else
                {
                    ConnectionLine cl = CurrentSchema.HitTestConnectionLine(e.Location);
                    if (cl != null) {
                        cl.Selected = true;
                        CurrentSchema.SelectedObject = cl;
                        propertyGrid.SelectedObject = conn.Database.GetConstraint(cl.ForeignKeyName);
                    }
                }

                panelPaint(false);

                MainForm.cmdMan.SetCommandControlsVisibility(typeof(ToolStripMenuItem));
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (dragging)
            {
                if (CurrentSchema.SelectedObject is TableView)
                {
                    TableView tv = CurrentSchema.SelectedObject as TableView;
                    tv.Position = e.Location - dragDelta;
                    correctConnectionLinesPositions(new List<TableView> { tv });
                    correctPanelSize();
                    panelPaint(false);
                    modified = true;
                }
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
            if (CurrentSchema.SelectedObject is TableView)
            {
                TableView tv = CurrentSchema.SelectedObject as TableView;
                tv.ShowFull = !tv.ShowFull;
                correctConnectionLinesPositions(new List<TableView> { tv });
                correctPanelSize();
                panelPaint(false);
                modified = true;
            }
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

        private void cmdRemoveObject(string arg)
        {
            if (CurrentSchema.SelectedObject is TableView)
            {
                TableView tv = CurrentSchema.SelectedObject as TableView;
                CurrentSchema.Tables.Remove(tv);
                CurrentSchema.ConnectionLines.RemoveAll(cl => cl.FromTable == tv.TableName || cl.ToTable == tv.TableName);

                CurrentSchema.SelectedObject = null;
                correctConnectionLinesPositions(new List<TableView> { tv });
            }
            if (CurrentSchema.SelectedObject is Field)
            {
                Field f = CurrentSchema.SelectedObject as Field;
                SQLInputDialog sid = new SQLInputDialog(conn.Database.GetSQLColumnRemove(f), true, "Do you really want to drop this column?");
                if (sid.ShowDialog() == DialogResult.OK)
                {
                    conn.Database.ExecuteNonQuery(sid.SQL);
                    TreeNode tn = MainForm.findNode(f);
                    if(tn!=null) tn.Remove();
                    CurrentSchema.SelectedObject = null;
                }
            }
            if (CurrentSchema.SelectedObject is ConnectionLine)
            {
                Constraint c = conn.Database.GetConstraint((CurrentSchema.SelectedObject as ConnectionLine).ForeignKeyName);
                SQLInputDialog sid = new SQLInputDialog(conn.Database.GetSQLConstraintRemove(c), true, "Do you really want to drop this constraint?");
                if (sid.ShowDialog() == DialogResult.OK)
                {
                    conn.Database.ExecuteNonQuery(sid.SQL);
                    CurrentSchema.ConnectionLines.Remove(CurrentSchema.SelectedObject as ConnectionLine);
                    TreeNode tn = MainForm.findNode(c);
                    if (tn != null) tn.Remove();
                    CurrentSchema.SelectedObject = null;
                }
            }

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
                CurrentSchema.Tables.Add(tv);

                CurrentSchema.SelectedObject = tv;

                modified = true;
                correctPanelSize();
                panelPaint(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cinar Database Tools", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            Field fld = e.Data.GetData(typeof(Field)) as Field;

            if (tbl != null)
            {
                TableView tv = CurrentSchema.GetTableView(tbl.Name);
                if (tbl != null && tv == null)
                    e.Effect = DragDropEffects.Move;
                else
                    e.Effect = DragDropEffects.None;
            }

            if (fld != null)
            {
                    e.Effect = DragDropEffects.Move;
            }
        }
        protected override void OnDragDrop(DragEventArgs e)
        {
            Table tbl = e.Data.GetData(typeof(Table)) as Table;
            Field fld = e.Data.GetData(typeof(Field)) as Field;

            if (tbl != null)
            {
                Point pos = this.PointToClient(new Point(e.X, e.Y));
                if (tbl.Database == conn.Database)
                    addTablesToSchema(new List<Table>() { tbl }, false, pos.X, pos.Y);
                else
                {
                    Table newTable = null;
                    try
                    {
                        newTable = (Table)tbl.Clone();
                        conn.Database.Tables.Add(newTable);
                        conn.Database.SetCollectionParents();
                        if(MainForm.CreateTable(conn.Database, newTable))
                            addTablesToSchema(new List<Table>() { newTable }, false, pos.X, pos.Y);
                        else
                            conn.Database.Tables.Remove(newTable);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Cinar Database Tools");
                        conn.Database.Tables.Remove(newTable);
                    }
                }
            }

            if (fld != null)
            {
                TableView tv = CurrentSchema.HitTestTable(this.PointToClient(new Point(e.X, e.Y)));
                if (tv != null)
                {
                    fld = (Field)fld.Clone();
                    conn.Database.Tables[tv.TableName].Fields.Add(fld);

                    try
                    {
                        string sql = conn.Database.GetSQLColumnAdd(tv.TableName, fld);
                        SQLInputDialog sid = new SQLInputDialog(sql, false);
                        if (sid.ShowDialog() == DialogResult.OK)
                        {
                            conn.Database.ExecuteNonQuery(sid.SQL);
                            MainForm.populateTreeNodesFor(null, fld);
                        }
                        else
                            conn.Database.Tables[tv.TableName].Fields.Remove(fld);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Cinar Database Tools");
                        conn.Database.Tables[tv.TableName].Fields.Remove(fld);
                    }
                }
            }

            panelPaint(true);
        }

        public void OnClose()
        {
            
        }
    }
}
