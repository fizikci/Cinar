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
    public partial class TableFormPreview : UserControl, IEditor
    {
        public Table Table { get; set; }
        public PropertyGrid propertyGrid;

        private Panel _activePanel;
        private Panel activePanel
        {
            get { return _activePanel; }
            set {
                if (_activePanel != null)
                    _activePanel.BorderStyle = System.Windows.Forms.BorderStyle.None;

                _activePanel = value;
                _activePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            }
        }

        public TableFormPreview(PropertyGrid propertyGrid)
        {
            InitializeComponent();

            this.propertyGrid = propertyGrid;

            this.ContextMenuStrip = contextMenu;

            foreach (var val in Enum.GetNames(typeof(EditorTypes)))
            {
                ToolStripMenuItem item = new ToolStripMenuItem(val);
                item.Click += (sndr, args) => { setEditorType(val); };
                contextMenu.Items.Add(item);
            }
        }

        public void Preview() {
            FlowLayoutPanel mainPanel = new FlowLayoutPanel() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight };
            mainPanel.AutoScroll = true;
            mainPanel.DoubleClick += mainPanel_DoubleClick;

            foreach (string group in Table.GetUIGroups())
            {
                GroupBox gb = new GroupBox() { Text = group, AutoSize = true};
                mainPanel.Controls.Add(gb);

                FlowLayoutPanel groupPanel = new FlowLayoutPanel() { AutoSize = true, FlowDirection = FlowDirection.TopDown, Left = 16, Top = 22};
                gb.Controls.Add(groupPanel);

                foreach (string column in Table.GetUIGroupColumns(group))
                {
                    var c = getControl(Table.Columns[column]);
                    if (c != null)
                        groupPanel.Controls.Add(c);
                }
            }

            this.Controls.Clear();
            this.Controls.Add(mainPanel);
        }

        void mainPanel_DoubleClick(object sender, EventArgs e)
        {
            this.Preview();
        }

        private Control getControl(Column column)
        {
            Control c = null;
            switch (column.UIMetadata.EditorType)
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
            Label l = new Label() { Width=160, TextAlign = ContentAlignment.MiddleRight, Text = column.UIMetadata.DisplayName};
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

            activePanel = sender as Panel;
            propertyGrid.SelectedObject = activePanel.Tag;
        }

        public bool Modified
        {
            get { return false; }
        }

        public string GetName()
        {
            return Table.Name + " UI View";
        }

        public bool Save()
        {
            return true;
        }

        public void OnClose()
        {
            
        }

        public string Content
        {
            get
            {
                return "";
            }
            set
            {
                
            }
        }

        public string FilePath
        {
            get { return ""; }
        }

        private void setEditorType(string name) {
            if (activePanel == null)
                return;

            EditorTypes et = (EditorTypes)Enum.Parse(typeof(EditorTypes), name);
            (activePanel.Tag as Column).UIMetadata.EditorType = et;

            Provider.ConnectionsModified = true;
            Preview();
        }

        private void menuRename_Click(object sender, EventArgs e)
        {
            if (activePanel == null)
                return;

            var name = Provider.Prompt("Enter new display name", "Cinar Database Tools", (activePanel.Tag as Column).UIMetadata.DisplayName);
            (activePanel.Tag as Column).UIMetadata.DisplayName = name;

            Provider.ConnectionsModified = true;
            Preview();
        }

        private void menuUp_Click(object sender, EventArgs e)
        {
            if (activePanel == null)
                return;

            var col = activePanel.Tag as Column;
            var prevCol = col.Table.Columns.Find(c => c.UIMetadata.DisplayOrder == col.UIMetadata.DisplayOrder - 1);
            if (prevCol != null)
                prevCol.UIMetadata.DisplayOrder += 1;
            col.UIMetadata.DisplayOrder -= 1;

            Provider.ConnectionsModified = true;
            Preview();
        }

        private void menuDown_Click(object sender, EventArgs e)
        {
            if (activePanel == null)
                return;

            var col = activePanel.Tag as Column;
            var nextCol = col.Table.Columns.Find(c => c.UIMetadata.DisplayOrder == col.UIMetadata.DisplayOrder + 1);
            if (nextCol != null)
                nextCol.UIMetadata.DisplayOrder -= 1;
            col.UIMetadata.DisplayOrder += 1;

            Provider.ConnectionsModified = true;
            Preview();
        }

        private void menuRefresh_Click(object sender, EventArgs e)
        {
            Preview();
        }
    }
}
