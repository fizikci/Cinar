using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Database;

namespace Cinar.DBTools.Controls
{
    public partial class TableFormPreview : UserControl
    {
        public Table Table { get; set; }
        public PropertyGrid propertyGrid;

        private List<Panel> _activePanels = new List<Panel>();

        public TableFormPreview(PropertyGrid propertyGrid)
        {
            InitializeComponent();
            
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            this.SetStyle(ControlStyles.Opaque, false);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            this.propertyGrid = propertyGrid;
            this.propertyGrid.PropertyValueChanged += delegate(object o, PropertyValueChangedEventArgs args) { this.Preview(); };
            this.ContextMenuStrip = contextMenu;
            this.ContextMenuStrip.Opening += ContextMenuStrip_Opening;

            foreach (var val in Enum.GetNames(typeof(EditorTypes)))
            {
                ToolStripMenuItem item = new ToolStripMenuItem(val);
                item.Click += (sndr, args) => { setEditorType(val); };
                contextMenu.Items.Add(item);
            }
        }

        void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (_activePanels.Count == 0)
                e.Cancel = true;
        }

        public void Preview() {
            FlowLayoutPanel mainPanel = new FlowLayoutPanel() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight };
            mainPanel.AutoScroll = true;
            mainPanel.DoubleClick += mainPanel_DoubleClick;
            mainPanel.Click += mainPanel_Click;

            int i = 0;

            foreach (string group in Table.GetUIGroups())
            {
                GroupBox gb = new GroupBox() { Text = group, AutoSize = true};
                mainPanel.Controls.Add(gb);

                FlowLayoutPanel groupPanel = new FlowLayoutPanel() { AutoSize = true, FlowDirection = FlowDirection.TopDown, Left = 16, Top = 22};
                gb.Controls.Add(groupPanel);

                foreach (string column in Table.GetUIGroupColumns(group))
                {
                    Table.Columns[column].GenerateUIMetadata().DisplayOrder = i++;
                    var c = getControl(Table.Columns[column]);
                    if (c != null)
                        groupPanel.Controls.Add(c);
                }
            }

            this.Controls.Clear();
            this.Controls.Add(mainPanel);
        }

        void mainPanel_Click(object sender, EventArgs e)
        {
            _activePanels.Clear();
            markActivePanels();
        }

        void mainPanel_DoubleClick(object sender, EventArgs e)
        {
            this.Preview();
        }

        private Control getControl(Column column)
        {
            Control c = null;
            switch (column.GenerateUIMetadata().EditorType)
            {
                case EditorTypes.Undefined:
                case EditorTypes.Hidden:
                    break;
                case EditorTypes.TextEdit:
                    c = new TextBox();
                    break;
                case EditorTypes.MemoEdit:
                case EditorTypes.HTMLEdit:
                    c = new TextBox() { Multiline = true };
                    c.Height = 50;
                    break;
                case EditorTypes.CheckBox:
                    c = new CheckBox();
                    break;
                case EditorTypes.ComboBox:
                    c = new ComboBox();
                    (c as ComboBox).Items.Add("One");
                    (c as ComboBox).Items.Add("Two");
                    (c as ComboBox).Items.Add("Three");
                    break;
                case EditorTypes.LookUp:
                    c = new TextBox();
                    c.Text = "LookUp";
                    break;
                case EditorTypes.DateEdit:
                    c = new DateTimePicker();
                    break;
                case EditorTypes.NumberEdit:
                    c = new NumericUpDown();
                    break;
                case EditorTypes.TimeEdit:
                    c = new TextBox();
                    c.Text = "TimeEdit";
                    break;
                case EditorTypes.TagEdit:
                    c = new TextBox();
                    c.Text = "TagEdit";
                    break;
                default:
                    break;
            }

            if (c == null) return null;

            Panel p = new Panel() { Width = 400, Height = c.Height+4};
            Label l = new Label() { Width=160, TextAlign = ContentAlignment.MiddleRight, Text = column.GenerateUIMetadata().DisplayName};
            c.Width = 240;
            c.Left = 160;
            p.Tag = c.Tag = column;
            p.Controls.Add(l);
            p.Controls.Add(c);

            p.MouseDown += p_Click;
            l.MouseDown += p_Click;
            c.MouseDown += p_Click;

            return p;
        }

        void p_Click(object sender, MouseEventArgs e)
        {
            if (!(sender is Panel))
                sender = (sender as Control).Parent;

            var panel = sender as Panel;

            if (Control.ModifierKeys != Keys.Control && e.Button != MouseButtons.Right)
                _activePanels.Clear();

            if (Control.ModifierKeys == Keys.Shift)
            {
                //_activePanels.OrderBy(p=>(p.Tag as Column).GenerateUIMetadata().DisplayOrder)
            }

            if (_activePanels.Contains(panel))
                _activePanels.Remove(panel);
            else
                _activePanels.Add(panel);
            markActivePanels();
        }

        private void markActivePanels()
        {
            foreach (GroupBox gb in this.Controls[0].Controls)
                foreach (Panel panel in gb.Controls[0].Controls)
                    panel.BorderStyle = _activePanels.Contains(panel) ? BorderStyle.FixedSingle : BorderStyle.None;

            propertyGrid.SelectedObject = _activePanels.Count > 0 ? _activePanels.Last().Tag : Table;
        }

        private void setEditorType(string name) {
            if (_activePanels.Count == 0)
                return;

            EditorTypes et = (EditorTypes)Enum.Parse(typeof(EditorTypes), name);
            foreach (var activePanel in _activePanels)
                (activePanel.Tag as Column).GenerateUIMetadata().EditorType = et;


            Provider.ConnectionsModified = true;
            Preview();
        }

        private void menuRename_Click(object sender, EventArgs e)
        {
            if (_activePanels.Count == 0)
                return;

            var name = Provider.Prompt("Enter new display name", "Cinar Database Tools", (_activePanels[0].Tag as Column).GenerateUIMetadata().DisplayName);
            foreach (var activePanel in _activePanels)
                (activePanel.Tag as Column).GenerateUIMetadata().DisplayName = name;

            Provider.ConnectionsModified = true;
            Preview();
        }

        private void menuUp_Click(object sender, EventArgs e)
        {
            if (_activePanels.Count == 0)
                return;

            foreach (var activePanel in _activePanels)
            {
                var col = activePanel.Tag as Column;
                var prevCol = col.Table.Columns.Find(c => c.GenerateUIMetadata().DisplayOrder == col.GenerateUIMetadata().DisplayOrder - 1);
                if (prevCol != null)
                    prevCol.GenerateUIMetadata().DisplayOrder += 1;
                col.GenerateUIMetadata().DisplayOrder -= 1;
            }

            Provider.ConnectionsModified = true;
            Preview();
        }

        private void menuDown_Click(object sender, EventArgs e)
        {
            if (_activePanels.Count == 0)
                return;
            foreach (var activePanel in _activePanels)
            {
                var col = activePanel.Tag as Column;
                var nextCol = col.Table.Columns.Find(c => c.GenerateUIMetadata().DisplayOrder == col.GenerateUIMetadata().DisplayOrder + 1);
                if (nextCol != null)
                    nextCol.GenerateUIMetadata().DisplayOrder -= 1;
                col.GenerateUIMetadata().DisplayOrder += 1;
            }

            Provider.ConnectionsModified = true;
            Preview();
        }

        private void menuRefresh_Click(object sender, EventArgs e)
        {
            Preview();
        }

        private void menuRenameGroupName_Click(object sender, EventArgs e)
        {
            if (_activePanels.Count == 0)
                return;

            var name = Provider.Prompt("Enter new group name", "Cinar Database Tools", (_activePanels[0].Tag as Column).GenerateUIMetadata().GroupName);
            foreach (var activePanel in _activePanels)
                (activePanel.Tag as Column).GenerateUIMetadata().GroupName = name;

            Provider.ConnectionsModified = true;
            Preview();
        }
    }
}
