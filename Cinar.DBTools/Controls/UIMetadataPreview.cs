using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cinar.DBTools.Controls
{
    public partial class UIMetadataPreview : UserControl, IEditor
    {
        public Database.Database Database { get; set; }
        public PropertyGrid propertyGrid;

        public UIMetadataPreview(PropertyGrid propertyGrid)
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
        }

        public void Preview() 
        {
            tabControl.TabPages.Clear();

            int i = 0;

            foreach (string module in Provider.GetUIModules())
            {
                tabControl.TabPages.Add(module);
                TabPage tp = tabControl.TabPages[tabControl.TabPages.Count-1];

                FlowLayoutPanel modulePanel = new FlowLayoutPanel() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, AutoScroll = true};
                tp.Controls.Add(modulePanel);

                foreach (string tableName in Database.GetUIModuleTables(module))
                {
                    Database.Tables[tableName].GenerateUIMetadata().DisplayOrder = i++;
                    var c = getControl(Database.Tables[tableName]);
                    if (c != null)
                        modulePanel.Controls.Add(c);
                }
            }
        }









        public bool Modified
        {
            get { return Provider.ConnectionsModified; }
        }

        public string GetName()
        {
            return "UI Metadata Preview";
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

    }
}
