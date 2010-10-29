using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Cinar.WinUI
{
    public class CustomDesignForm : DevExpress.XtraReports.UserDesigner.XRDesignFormExBase
    {
        private DevExpress.XtraReports.UserDesigner.XRDesignBarManager xrDesignBarManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraReports.UserDesigner.RecentlyUsedItemsComboBox ricbFontName;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox ricbFontSize;
        private DevExpress.XtraReports.UserDesigner.DesignBar designBar1;
        private DevExpress.XtraReports.UserDesigner.DesignBar designBar2;
        private DevExpress.XtraReports.UserDesigner.DesignBar designBar3;
        private DevExpress.XtraReports.UserDesigner.DesignBar designBar4;
        private DevExpress.XtraReports.UserDesigner.DesignBar designBar5;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraBars.BarEditItem barEditItem2;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem1;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem2;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem3;
        private DevExpress.XtraReports.UserDesigner.CommandColorBarItem commandColorBarItem1;
        private DevExpress.XtraReports.UserDesigner.CommandColorBarItem commandColorBarItem2;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem4;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem5;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem6;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem7;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem8;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem9;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem10;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem11;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem12;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem13;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem14;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem15;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem16;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem17;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem18;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem19;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem20;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem21;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem22;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem23;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem24;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem25;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem26;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem27;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem28;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem29;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem30;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem31;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem32;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem33;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem34;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem35;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem36;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem37;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem38;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarSubItem barSubItem3;
        private DevExpress.XtraReports.UserDesigner.BarReportTabButtonsListItem barReportTabButtonsListItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem4;
        private DevExpress.XtraReports.UserDesigner.XRBarToolbarsListItem xrBarToolbarsListItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem5;
        private DevExpress.XtraReports.UserDesigner.BarDockPanelsListItem barDockPanelsListItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem6;
        private DevExpress.XtraBars.BarSubItem barSubItem7;
        private DevExpress.XtraBars.BarSubItem barSubItem8;
        private DevExpress.XtraBars.BarSubItem barSubItem9;
        private DevExpress.XtraBars.BarSubItem barSubItem10;
        private DevExpress.XtraBars.BarSubItem barSubItem11;
        private DevExpress.XtraBars.BarSubItem barSubItem12;
        private DevExpress.XtraBars.BarSubItem barSubItem13;
        private DevExpress.XtraBars.BarSubItem barSubItem14;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem39;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem40;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem41;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem42;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem43;
        private DevExpress.XtraBars.BarSubItem bsiLookAndFeel;
        private DevExpress.XtraReports.UserDesigner.XRDesignDockManager xrDesignDockManager1;
        private DevExpress.XtraReports.UserDesigner.DesignRepositoryItemComboBox designRepositoryItemComboBox1;
        private DevExpress.XtraReports.UserDesigner.DesignBar designBar6;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem44;
        private DevExpress.XtraReports.UserDesigner.XRZoomBarEditItem xrZoomBarEditItem1;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem45;
        private DevExpress.XtraReports.UserDesigner.GroupAndSortDockPanel groupAndSortDockPanel1;
        private DevExpress.XtraReports.UserDesigner.DesignControlContainer groupAndSortDockPanel1_Container;
        private DevExpress.XtraReports.UserDesigner.ToolBoxDockPanel toolBoxDockPanel1;
        private DevExpress.XtraReports.UserDesigner.DesignControlContainer toolBoxDockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer2;
        private DevExpress.XtraReports.UserDesigner.ReportExplorerDockPanel reportExplorerDockPanel1;
        private DevExpress.XtraReports.UserDesigner.DesignControlContainer reportExplorerDockPanel1_Container;
        private DevExpress.XtraReports.UserDesigner.FieldListDockPanel fieldListDockPanel1;
        private DevExpress.XtraReports.UserDesigner.DesignControlContainer fieldListDockPanel1_Container;
        private DevExpress.XtraReports.UserDesigner.PropertyGridDockPanel propertyGridDockPanel1;
        private DevExpress.XtraReports.UserDesigner.DesignControlContainer propertyGridDockPanel1_Container;
        private System.ComponentModel.Container components = null;

        public CustomDesignForm()
        {
            InitializeComponent();
            //BarItem item = new XtraReportsDemos.BarLookAndFeelListItem(DevExpress.LookAndFeel.UserLookAndFeel.Default);
            //xrDesignBarManager1.Items.Add(item);
            //bsiLookAndFeel.AddItem(item);
        }

        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if(components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomDesignForm));
			this.xrDesignBarManager1 = new DevExpress.XtraReports.UserDesigner.XRDesignBarManager();
			this.designBar1 = new DevExpress.XtraReports.UserDesigner.DesignBar();
			this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
			this.commandBarItem31 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem39 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem32 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem33 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem40 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem41 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
			this.commandBarItem37 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem38 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem34 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem35 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem36 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem42 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem43 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
			this.barReportTabButtonsListItem1 = new DevExpress.XtraReports.UserDesigner.BarReportTabButtonsListItem();
			this.barSubItem4 = new DevExpress.XtraBars.BarSubItem();
			this.xrBarToolbarsListItem1 = new DevExpress.XtraReports.UserDesigner.XRBarToolbarsListItem();
			this.barSubItem5 = new DevExpress.XtraBars.BarSubItem();
			this.barDockPanelsListItem1 = new DevExpress.XtraReports.UserDesigner.BarDockPanelsListItem();
			this.barSubItem6 = new DevExpress.XtraBars.BarSubItem();
			this.commandColorBarItem1 = new DevExpress.XtraReports.UserDesigner.CommandColorBarItem();
			this.commandColorBarItem2 = new DevExpress.XtraReports.UserDesigner.CommandColorBarItem();
			this.barSubItem7 = new DevExpress.XtraBars.BarSubItem();
			this.commandBarItem1 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem2 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem3 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.barSubItem8 = new DevExpress.XtraBars.BarSubItem();
			this.commandBarItem4 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem5 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem6 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem7 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.barSubItem9 = new DevExpress.XtraBars.BarSubItem();
			this.commandBarItem9 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem10 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem11 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem12 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem13 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem14 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem8 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.barSubItem10 = new DevExpress.XtraBars.BarSubItem();
			this.commandBarItem15 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem16 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem17 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem18 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.barSubItem11 = new DevExpress.XtraBars.BarSubItem();
			this.commandBarItem19 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem20 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem21 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem22 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.barSubItem12 = new DevExpress.XtraBars.BarSubItem();
			this.commandBarItem23 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem24 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem25 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem26 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.barSubItem13 = new DevExpress.XtraBars.BarSubItem();
			this.commandBarItem27 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem28 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.barSubItem14 = new DevExpress.XtraBars.BarSubItem();
			this.commandBarItem29 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.commandBarItem30 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.bsiLookAndFeel = new DevExpress.XtraBars.BarSubItem();
			this.designBar2 = new DevExpress.XtraReports.UserDesigner.DesignBar();
			this.designBar3 = new DevExpress.XtraReports.UserDesigner.DesignBar();
			this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
			this.ricbFontName = new DevExpress.XtraReports.UserDesigner.RecentlyUsedItemsComboBox();
			this.barEditItem2 = new DevExpress.XtraBars.BarEditItem();
			this.ricbFontSize = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.designBar4 = new DevExpress.XtraReports.UserDesigner.DesignBar();
			this.designBar5 = new DevExpress.XtraReports.UserDesigner.DesignBar();
			this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
			this.designBar6 = new DevExpress.XtraReports.UserDesigner.DesignBar();
			this.commandBarItem44 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.xrZoomBarEditItem1 = new DevExpress.XtraReports.UserDesigner.XRZoomBarEditItem();
			this.designRepositoryItemComboBox1 = new DevExpress.XtraReports.UserDesigner.DesignRepositoryItemComboBox();
			this.commandBarItem45 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.xrDesignDockManager1 = new DevExpress.XtraReports.UserDesigner.XRDesignDockManager();
			this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
			this.panelContainer2 = new DevExpress.XtraBars.Docking.DockPanel();
			this.reportExplorerDockPanel1 = new DevExpress.XtraReports.UserDesigner.ReportExplorerDockPanel();
			this.reportExplorerDockPanel1_Container = new DevExpress.XtraReports.UserDesigner.DesignControlContainer();
			this.fieldListDockPanel1 = new DevExpress.XtraReports.UserDesigner.FieldListDockPanel();
			this.fieldListDockPanel1_Container = new DevExpress.XtraReports.UserDesigner.DesignControlContainer();
			this.propertyGridDockPanel1 = new DevExpress.XtraReports.UserDesigner.PropertyGridDockPanel();
			this.propertyGridDockPanel1_Container = new DevExpress.XtraReports.UserDesigner.DesignControlContainer();
			this.toolBoxDockPanel1 = new DevExpress.XtraReports.UserDesigner.ToolBoxDockPanel();
			this.toolBoxDockPanel1_Container = new DevExpress.XtraReports.UserDesigner.DesignControlContainer();
			this.groupAndSortDockPanel1 = new DevExpress.XtraReports.UserDesigner.GroupAndSortDockPanel();
			this.groupAndSortDockPanel1_Container = new DevExpress.XtraReports.UserDesigner.DesignControlContainer();
			((System.ComponentModel.ISupportInitialize)(this.xrDesignPanel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xrDesignBarManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ricbFontName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ricbFontSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.designRepositoryItemComboBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xrDesignDockManager1)).BeginInit();
			this.panelContainer1.SuspendLayout();
			this.panelContainer2.SuspendLayout();
			this.reportExplorerDockPanel1.SuspendLayout();
			this.fieldListDockPanel1.SuspendLayout();
			this.propertyGridDockPanel1.SuspendLayout();
			this.toolBoxDockPanel1.SuspendLayout();
			this.groupAndSortDockPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// xrDesignPanel
			// 
			this.xrDesignPanel.Location = new System.Drawing.Point(165, 76);
			this.xrDesignPanel.ShowComponentTray = false;
			this.xrDesignPanel.Size = new System.Drawing.Size(417, 312);
			// 
			// xrDesignBarManager1
			// 
			this.xrDesignBarManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.designBar1,
            this.designBar2,
            this.designBar3,
            this.designBar4,
            this.designBar5,
            this.designBar6});
			this.xrDesignBarManager1.DockControls.Add(this.barDockControlTop);
			this.xrDesignBarManager1.DockControls.Add(this.barDockControlBottom);
			this.xrDesignBarManager1.DockControls.Add(this.barDockControlLeft);
			this.xrDesignBarManager1.DockControls.Add(this.barDockControlRight);
			this.xrDesignBarManager1.DockManager = this.xrDesignDockManager1;
			this.xrDesignBarManager1.FontNameBox = this.ricbFontName;
			this.xrDesignBarManager1.FontNameEdit = this.barEditItem1;
			this.xrDesignBarManager1.FontSizeBox = this.ricbFontSize;
			this.xrDesignBarManager1.FontSizeEdit = this.barEditItem2;
			this.xrDesignBarManager1.Form = this;
			this.xrDesignBarManager1.FormattingToolbar = this.designBar3;
			this.xrDesignBarManager1.HintStaticItem = this.barStaticItem1;
			this.xrDesignBarManager1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("xrDesignBarManager1.ImageStream")));
			this.xrDesignBarManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barEditItem1,
            this.barEditItem2,
            this.commandBarItem1,
            this.commandBarItem2,
            this.commandBarItem3,
            this.commandColorBarItem1,
            this.commandColorBarItem2,
            this.commandBarItem4,
            this.commandBarItem5,
            this.commandBarItem6,
            this.commandBarItem7,
            this.commandBarItem8,
            this.commandBarItem9,
            this.commandBarItem10,
            this.commandBarItem11,
            this.commandBarItem12,
            this.commandBarItem13,
            this.commandBarItem14,
            this.commandBarItem15,
            this.commandBarItem16,
            this.commandBarItem17,
            this.commandBarItem18,
            this.commandBarItem19,
            this.commandBarItem20,
            this.commandBarItem21,
            this.commandBarItem22,
            this.commandBarItem23,
            this.commandBarItem24,
            this.commandBarItem25,
            this.commandBarItem26,
            this.commandBarItem27,
            this.commandBarItem28,
            this.commandBarItem29,
            this.commandBarItem30,
            this.commandBarItem31,
            this.commandBarItem32,
            this.commandBarItem33,
            this.commandBarItem34,
            this.commandBarItem35,
            this.commandBarItem36,
            this.commandBarItem37,
            this.commandBarItem38,
            this.barStaticItem1,
            this.barSubItem1,
            this.barSubItem2,
            this.barSubItem3,
            this.barReportTabButtonsListItem1,
            this.barSubItem4,
            this.xrBarToolbarsListItem1,
            this.barSubItem5,
            this.barDockPanelsListItem1,
            this.barSubItem6,
            this.barSubItem7,
            this.barSubItem8,
            this.barSubItem9,
            this.barSubItem10,
            this.barSubItem11,
            this.barSubItem12,
            this.barSubItem13,
            this.barSubItem14,
            this.commandBarItem39,
            this.commandBarItem40,
            this.commandBarItem41,
            this.commandBarItem42,
            this.commandBarItem43,
            this.bsiLookAndFeel,
            this.commandBarItem44,
            this.xrZoomBarEditItem1,
            this.commandBarItem45});
			this.xrDesignBarManager1.LayoutToolbar = this.designBar4;
			this.xrDesignBarManager1.MainMenu = this.designBar1;
			this.xrDesignBarManager1.MaxItemId = 69;
			this.xrDesignBarManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ricbFontName,
            this.ricbFontSize,
            this.designRepositoryItemComboBox1});
			this.xrDesignBarManager1.StatusBar = this.designBar5;
			this.xrDesignBarManager1.Toolbar = this.designBar2;
			this.xrDesignBarManager1.XRDesignPanel = this.xrDesignPanel;
			this.xrDesignBarManager1.ZoomItem = this.xrZoomBarEditItem1;
			// 
			// designBar1
			// 
			this.designBar1.BarName = "MainMenu";
			this.designBar1.DockCol = 0;
			this.designBar1.DockRow = 0;
			this.designBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.designBar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem6),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsiLookAndFeel)});
			this.designBar1.OptionsBar.MultiLine = true;
			this.designBar1.OptionsBar.UseWholeRow = true;
			this.designBar1.Text = "Ana Menü";
			// 
			// barSubItem1
			// 
			this.barSubItem1.Caption = "&Dosya";
			this.barSubItem1.Id = 43;
			this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem31),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem39),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem32),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem33, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem40),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem41, true)});
			this.barSubItem1.Name = "barSubItem1";
			// 
			// commandBarItem31
			// 
			this.commandBarItem31.Caption = "&Yeni";
			this.commandBarItem31.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.NewReport;
			this.commandBarItem31.Hint = "Create a new blank report";
			this.commandBarItem31.Id = 34;
			this.commandBarItem31.ImageIndex = 9;
			this.commandBarItem31.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N));
			this.commandBarItem31.Name = "commandBarItem31";
			// 
			// commandBarItem39
			// 
			this.commandBarItem39.Caption = "New with &Wizard...";
			this.commandBarItem39.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.NewReportWizard;
			this.commandBarItem39.Hint = "Create a new report using the Wizard";
			this.commandBarItem39.Id = 60;
			this.commandBarItem39.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W));
			this.commandBarItem39.Name = "commandBarItem39";
			// 
			// commandBarItem32
			// 
			this.commandBarItem32.Caption = "&Aç...";
			this.commandBarItem32.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.OpenFile;
			this.commandBarItem32.Hint = "Open a report";
			this.commandBarItem32.Id = 35;
			this.commandBarItem32.ImageIndex = 10;
			this.commandBarItem32.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O));
			this.commandBarItem32.Name = "commandBarItem32";
			// 
			// commandBarItem33
			// 
			this.commandBarItem33.Caption = "&Kaydet";
			this.commandBarItem33.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SaveFile;
			this.commandBarItem33.Enabled = false;
			this.commandBarItem33.Hint = "Save a report";
			this.commandBarItem33.Id = 36;
			this.commandBarItem33.ImageIndex = 11;
			this.commandBarItem33.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
			this.commandBarItem33.Name = "commandBarItem33";
			// 
			// commandBarItem40
			// 
			this.commandBarItem40.Caption = "Save &As...";
			this.commandBarItem40.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SaveFileAs;
			this.commandBarItem40.Enabled = false;
			this.commandBarItem40.Hint = "Save a report with a new name";
			this.commandBarItem40.Id = 61;
			this.commandBarItem40.Name = "commandBarItem40";
			// 
			// commandBarItem41
			// 
			this.commandBarItem41.Caption = "Back to the Reports Main Demo";
			this.commandBarItem41.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Exit;
			this.commandBarItem41.Hint = "Close the designer";
			this.commandBarItem41.Id = 62;
			this.commandBarItem41.Name = "commandBarItem41";
			// 
			// barSubItem2
			// 
			this.barSubItem2.Caption = "D&üzenle";
			this.barSubItem2.Id = 44;
			this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem37, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem38),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem34, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem35),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem36),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem42),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem43, true)});
			this.barSubItem2.Name = "barSubItem2";
			// 
			// commandBarItem37
			// 
			this.commandBarItem37.Caption = "&Geri Al";
			this.commandBarItem37.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Undo;
			this.commandBarItem37.Enabled = false;
			this.commandBarItem37.Hint = "Undo the last operation";
			this.commandBarItem37.Id = 40;
			this.commandBarItem37.ImageIndex = 15;
			this.commandBarItem37.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z));
			this.commandBarItem37.Name = "commandBarItem37";
			// 
			// commandBarItem38
			// 
			this.commandBarItem38.Caption = "&Ýleri Al";
			this.commandBarItem38.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Redo;
			this.commandBarItem38.Enabled = false;
			this.commandBarItem38.Hint = "Redo the last operation";
			this.commandBarItem38.Id = 41;
			this.commandBarItem38.ImageIndex = 16;
			this.commandBarItem38.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y));
			this.commandBarItem38.Name = "commandBarItem38";
			// 
			// commandBarItem34
			// 
			this.commandBarItem34.Caption = "Kes";
			this.commandBarItem34.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Cut;
			this.commandBarItem34.Enabled = false;
			this.commandBarItem34.Hint = "Delete the control and copy it to the clipboard";
			this.commandBarItem34.Id = 37;
			this.commandBarItem34.ImageIndex = 12;
			this.commandBarItem34.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X));
			this.commandBarItem34.Name = "commandBarItem34";
			// 
			// commandBarItem35
			// 
			this.commandBarItem35.Caption = "Kopyala";
			this.commandBarItem35.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Copy;
			this.commandBarItem35.Enabled = false;
			this.commandBarItem35.Hint = "Copy the control to the clipboard";
			this.commandBarItem35.Id = 38;
			this.commandBarItem35.ImageIndex = 13;
			this.commandBarItem35.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C));
			this.commandBarItem35.Name = "commandBarItem35";
			// 
			// commandBarItem36
			// 
			this.commandBarItem36.Caption = "Yapýþtýr";
			this.commandBarItem36.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Paste;
			this.commandBarItem36.Enabled = false;
			this.commandBarItem36.Hint = "Add the control from the clipboard";
			this.commandBarItem36.Id = 39;
			this.commandBarItem36.ImageIndex = 14;
			this.commandBarItem36.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V));
			this.commandBarItem36.Name = "commandBarItem36";
			// 
			// commandBarItem42
			// 
			this.commandBarItem42.Caption = "&Delete";
			this.commandBarItem42.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Delete;
			this.commandBarItem42.Enabled = false;
			this.commandBarItem42.Hint = "Delete the control";
			this.commandBarItem42.Id = 63;
			this.commandBarItem42.Name = "commandBarItem42";
			// 
			// commandBarItem43
			// 
			this.commandBarItem43.Caption = "Select &All";
			this.commandBarItem43.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SelectAll;
			this.commandBarItem43.Enabled = false;
			this.commandBarItem43.Hint = "Select all the controls in the document";
			this.commandBarItem43.Id = 64;
			this.commandBarItem43.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A));
			this.commandBarItem43.Name = "commandBarItem43";
			// 
			// barSubItem3
			// 
			this.barSubItem3.Caption = "&Görünüm";
			this.barSubItem3.Id = 45;
			this.barSubItem3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barReportTabButtonsListItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem4, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem5, true)});
			this.barSubItem3.Name = "barSubItem3";
			// 
			// barReportTabButtonsListItem1
			// 
			this.barReportTabButtonsListItem1.Caption = "Tab Buttons";
			this.barReportTabButtonsListItem1.Id = 46;
			this.barReportTabButtonsListItem1.Name = "barReportTabButtonsListItem1";
			// 
			// barSubItem4
			// 
			this.barSubItem4.Caption = "&Toolbars";
			this.barSubItem4.Id = 47;
			this.barSubItem4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.xrBarToolbarsListItem1)});
			this.barSubItem4.Name = "barSubItem4";
			// 
			// xrBarToolbarsListItem1
			// 
			this.xrBarToolbarsListItem1.Caption = "&Toolbars";
			this.xrBarToolbarsListItem1.Id = 48;
			this.xrBarToolbarsListItem1.Name = "xrBarToolbarsListItem1";
			// 
			// barSubItem5
			// 
			this.barSubItem5.Caption = "&Windows";
			this.barSubItem5.Id = 49;
			this.barSubItem5.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barDockPanelsListItem1)});
			this.barSubItem5.Name = "barSubItem5";
			// 
			// barDockPanelsListItem1
			// 
			this.barDockPanelsListItem1.Caption = "&Windows";
			this.barDockPanelsListItem1.Id = 50;
			this.barDockPanelsListItem1.Name = "barDockPanelsListItem1";
			this.barDockPanelsListItem1.ShowCustomizationItem = false;
			this.barDockPanelsListItem1.ShowDockPanels = true;
			this.barDockPanelsListItem1.ShowToolbars = false;
			// 
			// barSubItem6
			// 
			this.barSubItem6.Caption = "&Biçim";
			this.barSubItem6.Id = 51;
			this.barSubItem6.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandColorBarItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandColorBarItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem7, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem8),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem9, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem10),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem11, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem12),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem13, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem14, true)});
			this.barSubItem6.Name = "barSubItem6";
			// 
			// commandColorBarItem1
			// 
			this.commandColorBarItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			this.commandColorBarItem1.Caption = "Yazý Rengi";
			this.commandColorBarItem1.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.ForeColor;
			this.commandColorBarItem1.Enabled = false;
			this.commandColorBarItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("commandColorBarItem1.Glyph")));
			this.commandColorBarItem1.Hint = "Set the foreground color of the control";
			this.commandColorBarItem1.Id = 5;
			this.commandColorBarItem1.Name = "commandColorBarItem1";
			// 
			// commandColorBarItem2
			// 
			this.commandColorBarItem2.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			this.commandColorBarItem2.Caption = "Zemin Rengi";
			this.commandColorBarItem2.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.BackColor;
			this.commandColorBarItem2.Enabled = false;
			this.commandColorBarItem2.Glyph = ((System.Drawing.Image)(resources.GetObject("commandColorBarItem2.Glyph")));
			this.commandColorBarItem2.Hint = "Set the background color of the control";
			this.commandColorBarItem2.Id = 6;
			this.commandColorBarItem2.Name = "commandColorBarItem2";
			// 
			// barSubItem7
			// 
			this.barSubItem7.Caption = "&Font";
			this.barSubItem7.Id = 52;
			this.barSubItem7.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem1, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem3)});
			this.barSubItem7.Name = "barSubItem7";
			// 
			// commandBarItem1
			// 
			this.commandBarItem1.Caption = "&Kalýn";
			this.commandBarItem1.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.FontBold;
			this.commandBarItem1.Enabled = false;
			this.commandBarItem1.Hint = "Make the font bold";
			this.commandBarItem1.Id = 2;
			this.commandBarItem1.ImageIndex = 0;
			this.commandBarItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B));
			this.commandBarItem1.Name = "commandBarItem1";
			// 
			// commandBarItem2
			// 
			this.commandBarItem2.Caption = "&Eðik";
			this.commandBarItem2.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.FontItalic;
			this.commandBarItem2.Enabled = false;
			this.commandBarItem2.Hint = "Make the font italic";
			this.commandBarItem2.Id = 3;
			this.commandBarItem2.ImageIndex = 1;
			this.commandBarItem2.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I));
			this.commandBarItem2.Name = "commandBarItem2";
			// 
			// commandBarItem3
			// 
			this.commandBarItem3.Caption = "&Altý Çizgili";
			this.commandBarItem3.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.FontUnderline;
			this.commandBarItem3.Enabled = false;
			this.commandBarItem3.Hint = "Underline the font";
			this.commandBarItem3.Id = 4;
			this.commandBarItem3.ImageIndex = 2;
			this.commandBarItem3.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U));
			this.commandBarItem3.Name = "commandBarItem3";
			// 
			// barSubItem8
			// 
			this.barSubItem8.Caption = "&Justify";
			this.barSubItem8.Id = 53;
			this.barSubItem8.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem4, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem6),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem7)});
			this.barSubItem8.Name = "barSubItem8";
			// 
			// commandBarItem4
			// 
			this.commandBarItem4.Caption = "&Sola Yasla";
			this.commandBarItem4.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.JustifyLeft;
			this.commandBarItem4.Enabled = false;
			this.commandBarItem4.Hint = "Align the control’s text to the left";
			this.commandBarItem4.Id = 7;
			this.commandBarItem4.ImageIndex = 5;
			this.commandBarItem4.Name = "commandBarItem4";
			// 
			// commandBarItem5
			// 
			this.commandBarItem5.Caption = "&Ortala";
			this.commandBarItem5.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.JustifyCenter;
			this.commandBarItem5.Enabled = false;
			this.commandBarItem5.Hint = "Align the control’s text to the center";
			this.commandBarItem5.Id = 8;
			this.commandBarItem5.ImageIndex = 6;
			this.commandBarItem5.Name = "commandBarItem5";
			// 
			// commandBarItem6
			// 
			this.commandBarItem6.Caption = "&Saða Yasla";
			this.commandBarItem6.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.JustifyRight;
			this.commandBarItem6.Enabled = false;
			this.commandBarItem6.Hint = "Align the control’s text to the right";
			this.commandBarItem6.Id = 9;
			this.commandBarItem6.ImageIndex = 7;
			this.commandBarItem6.Name = "commandBarItem6";
			// 
			// commandBarItem7
			// 
			this.commandBarItem7.Caption = "&Ýki tarafa yasla";
			this.commandBarItem7.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.JustifyJustify;
			this.commandBarItem7.Enabled = false;
			this.commandBarItem7.Hint = "Justify the control’s text";
			this.commandBarItem7.Id = 10;
			this.commandBarItem7.ImageIndex = 8;
			this.commandBarItem7.Name = "commandBarItem7";
			// 
			// barSubItem9
			// 
			this.barSubItem9.Caption = "&Align";
			this.barSubItem9.Id = 54;
			this.barSubItem9.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem9, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem10),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem11),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem12, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem13),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem14),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem8, true)});
			this.barSubItem9.Name = "barSubItem9";
			// 
			// commandBarItem9
			// 
			this.commandBarItem9.Caption = "&Lefts";
			this.commandBarItem9.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignLeft;
			this.commandBarItem9.Enabled = false;
			this.commandBarItem9.Hint = "Left align the selected controls";
			this.commandBarItem9.Id = 12;
			this.commandBarItem9.ImageIndex = 18;
			this.commandBarItem9.Name = "commandBarItem9";
			// 
			// commandBarItem10
			// 
			this.commandBarItem10.Caption = "&Centers";
			this.commandBarItem10.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignVerticalCenters;
			this.commandBarItem10.Enabled = false;
			this.commandBarItem10.Hint = "Align the centers of the selected controls vertically";
			this.commandBarItem10.Id = 13;
			this.commandBarItem10.ImageIndex = 19;
			this.commandBarItem10.Name = "commandBarItem10";
			// 
			// commandBarItem11
			// 
			this.commandBarItem11.Caption = "&Rights";
			this.commandBarItem11.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignRight;
			this.commandBarItem11.Enabled = false;
			this.commandBarItem11.Hint = "Right align the selected controls";
			this.commandBarItem11.Id = 14;
			this.commandBarItem11.ImageIndex = 20;
			this.commandBarItem11.Name = "commandBarItem11";
			// 
			// commandBarItem12
			// 
			this.commandBarItem12.Caption = "&Tops";
			this.commandBarItem12.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignTop;
			this.commandBarItem12.Enabled = false;
			this.commandBarItem12.Hint = "Align the tops of the selected controls";
			this.commandBarItem12.Id = 15;
			this.commandBarItem12.ImageIndex = 21;
			this.commandBarItem12.Name = "commandBarItem12";
			// 
			// commandBarItem13
			// 
			this.commandBarItem13.Caption = "&Middles";
			this.commandBarItem13.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignHorizontalCenters;
			this.commandBarItem13.Enabled = false;
			this.commandBarItem13.Hint = "Align the centers of the selected controls horizontally";
			this.commandBarItem13.Id = 16;
			this.commandBarItem13.ImageIndex = 22;
			this.commandBarItem13.Name = "commandBarItem13";
			// 
			// commandBarItem14
			// 
			this.commandBarItem14.Caption = "&Bottoms";
			this.commandBarItem14.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignBottom;
			this.commandBarItem14.Enabled = false;
			this.commandBarItem14.Hint = "Align the bottoms of the selected controls";
			this.commandBarItem14.Id = 17;
			this.commandBarItem14.ImageIndex = 23;
			this.commandBarItem14.Name = "commandBarItem14";
			// 
			// commandBarItem8
			// 
			this.commandBarItem8.Caption = "to &Grid";
			this.commandBarItem8.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignToGrid;
			this.commandBarItem8.Enabled = false;
			this.commandBarItem8.Hint = "Align the positions of the selected controls to the grid";
			this.commandBarItem8.Id = 11;
			this.commandBarItem8.ImageIndex = 17;
			this.commandBarItem8.Name = "commandBarItem8";
			// 
			// barSubItem10
			// 
			this.barSubItem10.Caption = "&Make Same Size";
			this.barSubItem10.Id = 55;
			this.barSubItem10.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem15, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem16),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem17),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem18)});
			this.barSubItem10.Name = "barSubItem10";
			// 
			// commandBarItem15
			// 
			this.commandBarItem15.Caption = "&Width";
			this.commandBarItem15.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SizeToControlWidth;
			this.commandBarItem15.Enabled = false;
			this.commandBarItem15.Hint = "Make the selected controls have the same width";
			this.commandBarItem15.Id = 18;
			this.commandBarItem15.ImageIndex = 24;
			this.commandBarItem15.Name = "commandBarItem15";
			// 
			// commandBarItem16
			// 
			this.commandBarItem16.Caption = "Size to Gri&d";
			this.commandBarItem16.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SizeToGrid;
			this.commandBarItem16.Enabled = false;
			this.commandBarItem16.Hint = "Size the selected controls to the grid";
			this.commandBarItem16.Id = 19;
			this.commandBarItem16.ImageIndex = 25;
			this.commandBarItem16.Name = "commandBarItem16";
			// 
			// commandBarItem17
			// 
			this.commandBarItem17.Caption = "&Height";
			this.commandBarItem17.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SizeToControlHeight;
			this.commandBarItem17.Enabled = false;
			this.commandBarItem17.Hint = "Make the selected controls have the same height";
			this.commandBarItem17.Id = 20;
			this.commandBarItem17.ImageIndex = 26;
			this.commandBarItem17.Name = "commandBarItem17";
			// 
			// commandBarItem18
			// 
			this.commandBarItem18.Caption = "&Both";
			this.commandBarItem18.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SizeToControl;
			this.commandBarItem18.Enabled = false;
			this.commandBarItem18.Hint = "Make the selected controls the same size";
			this.commandBarItem18.Id = 21;
			this.commandBarItem18.ImageIndex = 27;
			this.commandBarItem18.Name = "commandBarItem18";
			// 
			// barSubItem11
			// 
			this.barSubItem11.Caption = "&Horizontal Spacing";
			this.barSubItem11.Id = 56;
			this.barSubItem11.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem19, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem20),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem21),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem22)});
			this.barSubItem11.Name = "barSubItem11";
			// 
			// commandBarItem19
			// 
			this.commandBarItem19.Caption = "Make &Equal";
			this.commandBarItem19.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.HorizSpaceMakeEqual;
			this.commandBarItem19.Enabled = false;
			this.commandBarItem19.Hint = "Make the spacing between the selected controls equal";
			this.commandBarItem19.Id = 22;
			this.commandBarItem19.ImageIndex = 28;
			this.commandBarItem19.Name = "commandBarItem19";
			// 
			// commandBarItem20
			// 
			this.commandBarItem20.Caption = "&Increase";
			this.commandBarItem20.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.HorizSpaceIncrease;
			this.commandBarItem20.Enabled = false;
			this.commandBarItem20.Hint = "Increase the spacing between the selected controls";
			this.commandBarItem20.Id = 23;
			this.commandBarItem20.ImageIndex = 29;
			this.commandBarItem20.Name = "commandBarItem20";
			// 
			// commandBarItem21
			// 
			this.commandBarItem21.Caption = "&Decrease";
			this.commandBarItem21.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.HorizSpaceDecrease;
			this.commandBarItem21.Enabled = false;
			this.commandBarItem21.Hint = "Decrease the spacing between the selected controls";
			this.commandBarItem21.Id = 24;
			this.commandBarItem21.ImageIndex = 30;
			this.commandBarItem21.Name = "commandBarItem21";
			// 
			// commandBarItem22
			// 
			this.commandBarItem22.Caption = "&Remove";
			this.commandBarItem22.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.HorizSpaceConcatenate;
			this.commandBarItem22.Enabled = false;
			this.commandBarItem22.Hint = "Remove the spacing between the selected controls";
			this.commandBarItem22.Id = 25;
			this.commandBarItem22.ImageIndex = 31;
			this.commandBarItem22.Name = "commandBarItem22";
			// 
			// barSubItem12
			// 
			this.barSubItem12.Caption = "&Vertical Spacing";
			this.barSubItem12.Id = 57;
			this.barSubItem12.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem23, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem24),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem25),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem26)});
			this.barSubItem12.Name = "barSubItem12";
			// 
			// commandBarItem23
			// 
			this.commandBarItem23.Caption = "Make &Equal";
			this.commandBarItem23.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.VertSpaceMakeEqual;
			this.commandBarItem23.Enabled = false;
			this.commandBarItem23.Hint = "Make the spacing between the selected controls equal";
			this.commandBarItem23.Id = 26;
			this.commandBarItem23.ImageIndex = 32;
			this.commandBarItem23.Name = "commandBarItem23";
			// 
			// commandBarItem24
			// 
			this.commandBarItem24.Caption = "&Increase";
			this.commandBarItem24.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.VertSpaceIncrease;
			this.commandBarItem24.Enabled = false;
			this.commandBarItem24.Hint = "Increase the spacing between the selected controls";
			this.commandBarItem24.Id = 27;
			this.commandBarItem24.ImageIndex = 33;
			this.commandBarItem24.Name = "commandBarItem24";
			// 
			// commandBarItem25
			// 
			this.commandBarItem25.Caption = "&Decrease";
			this.commandBarItem25.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.VertSpaceDecrease;
			this.commandBarItem25.Enabled = false;
			this.commandBarItem25.Hint = "Decrease the spacing between the selected controls";
			this.commandBarItem25.Id = 28;
			this.commandBarItem25.ImageIndex = 34;
			this.commandBarItem25.Name = "commandBarItem25";
			// 
			// commandBarItem26
			// 
			this.commandBarItem26.Caption = "&Remove";
			this.commandBarItem26.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.VertSpaceConcatenate;
			this.commandBarItem26.Enabled = false;
			this.commandBarItem26.Hint = "Remove the spacing between the selected controls";
			this.commandBarItem26.Id = 29;
			this.commandBarItem26.ImageIndex = 35;
			this.commandBarItem26.Name = "commandBarItem26";
			// 
			// barSubItem13
			// 
			this.barSubItem13.Caption = "&Center in Form";
			this.barSubItem13.Id = 58;
			this.barSubItem13.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem27, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem28)});
			this.barSubItem13.Name = "barSubItem13";
			// 
			// commandBarItem27
			// 
			this.commandBarItem27.Caption = "&Horizontally";
			this.commandBarItem27.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.CenterHorizontally;
			this.commandBarItem27.Enabled = false;
			this.commandBarItem27.Hint = "Horizontally center the selected controls within a band";
			this.commandBarItem27.Id = 30;
			this.commandBarItem27.ImageIndex = 36;
			this.commandBarItem27.Name = "commandBarItem27";
			// 
			// commandBarItem28
			// 
			this.commandBarItem28.Caption = "&Vertically";
			this.commandBarItem28.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.CenterVertically;
			this.commandBarItem28.Enabled = false;
			this.commandBarItem28.Hint = "Vertically center the selected controls within a band";
			this.commandBarItem28.Id = 31;
			this.commandBarItem28.ImageIndex = 37;
			this.commandBarItem28.Name = "commandBarItem28";
			// 
			// barSubItem14
			// 
			this.barSubItem14.Caption = "&Order";
			this.barSubItem14.Id = 59;
			this.barSubItem14.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem29, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem30)});
			this.barSubItem14.Name = "barSubItem14";
			// 
			// commandBarItem29
			// 
			this.commandBarItem29.Caption = "&Bring to Front";
			this.commandBarItem29.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.BringToFront;
			this.commandBarItem29.Enabled = false;
			this.commandBarItem29.Hint = "Bring the selected controls to the front";
			this.commandBarItem29.Id = 32;
			this.commandBarItem29.ImageIndex = 38;
			this.commandBarItem29.Name = "commandBarItem29";
			// 
			// commandBarItem30
			// 
			this.commandBarItem30.Caption = "&Send to Back";
			this.commandBarItem30.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SendToBack;
			this.commandBarItem30.Enabled = false;
			this.commandBarItem30.Hint = "Move the selected controls to the back";
			this.commandBarItem30.Id = 33;
			this.commandBarItem30.ImageIndex = 39;
			this.commandBarItem30.Name = "commandBarItem30";
			// 
			// bsiLookAndFeel
			// 
			this.bsiLookAndFeel.Caption = "&Look and Feel";
			this.bsiLookAndFeel.Id = 65;
			this.bsiLookAndFeel.Name = "bsiLookAndFeel";
			// 
			// designBar2
			// 
			this.designBar2.BarName = "ToolBar";
			this.designBar2.DockCol = 0;
			this.designBar2.DockRow = 1;
			this.designBar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.designBar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem31),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem32),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem33),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem34, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem35),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem36),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem37, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem38)});
			this.designBar2.Text = "Standart";
			// 
			// designBar3
			// 
			this.designBar3.BarName = "FormattingToolBar";
			this.designBar3.DockCol = 1;
			this.designBar3.DockRow = 1;
			this.designBar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.designBar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barEditItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEditItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandColorBarItem1, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandColorBarItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem4, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem6),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem7)});
			this.designBar3.Text = "Biçimlendirme";
			// 
			// barEditItem1
			// 
			this.barEditItem1.Caption = "Font Ýsmi";
			this.barEditItem1.Edit = this.ricbFontName;
			this.barEditItem1.Hint = "Font Name";
			this.barEditItem1.Id = 0;
			this.barEditItem1.Name = "barEditItem1";
			this.barEditItem1.Width = 120;
			// 
			// ricbFontName
			// 
			this.ricbFontName.AutoHeight = false;
			this.ricbFontName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.ricbFontName.DropDownRows = 12;
			this.ricbFontName.Name = "ricbFontName";
			// 
			// barEditItem2
			// 
			this.barEditItem2.Caption = "Font Boyutu";
			this.barEditItem2.Edit = this.ricbFontSize;
			this.barEditItem2.Hint = "Font Size";
			this.barEditItem2.Id = 1;
			this.barEditItem2.Name = "barEditItem2";
			this.barEditItem2.Width = 55;
			// 
			// ricbFontSize
			// 
			this.ricbFontSize.AutoHeight = false;
			this.ricbFontSize.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.ricbFontSize.Items.AddRange(new object[] {
            ((byte)(8)),
            ((byte)(9)),
            ((byte)(10)),
            ((byte)(11)),
            ((byte)(12)),
            ((byte)(14)),
            ((byte)(16)),
            ((byte)(18)),
            ((byte)(20)),
            ((byte)(22)),
            ((byte)(24)),
            ((byte)(26)),
            ((byte)(28)),
            ((byte)(36)),
            ((byte)(48)),
            ((byte)(72))});
			this.ricbFontSize.Name = "ricbFontSize";
			// 
			// designBar4
			// 
			this.designBar4.BarName = "LayoutToolBar";
			this.designBar4.DockCol = 0;
			this.designBar4.DockRow = 2;
			this.designBar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.designBar4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem8),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem9, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem10),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem11),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem12, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem13),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem14),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem15, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem16),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem17),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem18),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem19, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem20),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem21),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem22),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem23, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem24),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem25),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem26),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem27, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem28),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem29, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem30)});
			this.designBar4.Text = "Yerleþim";
			// 
			// designBar5
			// 
			this.designBar5.BarName = "StatusBar";
			this.designBar5.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			this.designBar5.DockCol = 0;
			this.designBar5.DockRow = 0;
			this.designBar5.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			this.designBar5.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1)});
			this.designBar5.OptionsBar.AllowQuickCustomization = false;
			this.designBar5.OptionsBar.DrawDragBorder = false;
			this.designBar5.OptionsBar.UseWholeRow = true;
			this.designBar5.Text = "Durum Çubuðu";
			// 
			// barStaticItem1
			// 
			this.barStaticItem1.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
			this.barStaticItem1.Id = 42;
			this.barStaticItem1.Name = "barStaticItem1";
			this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
			this.barStaticItem1.Width = 32;
			// 
			// designBar6
			// 
			this.designBar6.BarName = "Zoom Bar";
			this.designBar6.DockCol = 1;
			this.designBar6.DockRow = 2;
			this.designBar6.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.designBar6.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem44),
            new DevExpress.XtraBars.LinkPersistInfo(this.xrZoomBarEditItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem45)});
			this.designBar6.Text = "Yakýnlaþtýrma";
			// 
			// commandBarItem44
			// 
			this.commandBarItem44.Caption = "Uzaklaþtýr";
			this.commandBarItem44.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.ZoomOut;
			this.commandBarItem44.Enabled = false;
			this.commandBarItem44.Hint = "Zoom out the design surface";
			this.commandBarItem44.Id = 66;
			this.commandBarItem44.ImageIndex = 40;
			this.commandBarItem44.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Subtract));
			this.commandBarItem44.Name = "commandBarItem44";
			// 
			// xrZoomBarEditItem1
			// 
			this.xrZoomBarEditItem1.Caption = "Zoom";
			this.xrZoomBarEditItem1.Edit = this.designRepositoryItemComboBox1;
			this.xrZoomBarEditItem1.Enabled = false;
			this.xrZoomBarEditItem1.Hint = "Select or input the zoom factor";
			this.xrZoomBarEditItem1.Id = 67;
			this.xrZoomBarEditItem1.Name = "xrZoomBarEditItem1";
			this.xrZoomBarEditItem1.Width = 70;
			// 
			// designRepositoryItemComboBox1
			// 
			this.designRepositoryItemComboBox1.AutoComplete = false;
			this.designRepositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.designRepositoryItemComboBox1.Name = "designRepositoryItemComboBox1";
			// 
			// commandBarItem45
			// 
			this.commandBarItem45.Caption = "Yakýnlaþtýr";
			this.commandBarItem45.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.ZoomIn;
			this.commandBarItem45.Enabled = false;
			this.commandBarItem45.Hint = "Zoom in the design surface";
			this.commandBarItem45.Id = 68;
			this.commandBarItem45.ImageIndex = 41;
			this.commandBarItem45.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Add));
			this.commandBarItem45.Name = "commandBarItem45";
			// 
			// xrDesignDockManager1
			// 
			this.xrDesignDockManager1.Form = this;
			this.xrDesignDockManager1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("xrDesignDockManager1.ImageStream")));
			this.xrDesignDockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.panelContainer1,
            this.toolBoxDockPanel1,
            this.groupAndSortDockPanel1});
			this.xrDesignDockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar"});
			this.xrDesignDockManager1.XRDesignPanel = this.xrDesignPanel;
			// 
			// panelContainer1
			// 
			this.panelContainer1.Controls.Add(this.panelContainer2);
			this.panelContainer1.Controls.Add(this.propertyGridDockPanel1);
			this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
			this.panelContainer1.ID = new System.Guid("c924e71d-fc07-4f71-80db-150e46515172");
			this.panelContainer1.Location = new System.Drawing.Point(582, 76);
			this.panelContainer1.Name = "panelContainer1";
			this.panelContainer1.Size = new System.Drawing.Size(250, 312);
			this.panelContainer1.Text = "panelContainer1";
			// 
			// panelContainer2
			// 
			this.panelContainer2.ActiveChild = this.reportExplorerDockPanel1;
			this.panelContainer2.Controls.Add(this.reportExplorerDockPanel1);
			this.panelContainer2.Controls.Add(this.fieldListDockPanel1);
			this.panelContainer2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
			this.panelContainer2.ID = new System.Guid("f590a4cc-96cf-4036-aaa1-6a23922194fc");
			this.panelContainer2.ImageIndex = 2;
			this.panelContainer2.Location = new System.Drawing.Point(0, 0);
			this.panelContainer2.Name = "panelContainer2";
			this.panelContainer2.Size = new System.Drawing.Size(250, 355);
			this.panelContainer2.Tabbed = true;
			this.panelContainer2.Text = "panelContainer2";
			// 
			// reportExplorerDockPanel1
			// 
			this.reportExplorerDockPanel1.Controls.Add(this.reportExplorerDockPanel1_Container);
			this.reportExplorerDockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
			this.reportExplorerDockPanel1.ID = new System.Guid("fb3ec6cc-3b9b-4b9c-91cf-cff78c1edbf1");
			this.reportExplorerDockPanel1.ImageIndex = 2;
			this.reportExplorerDockPanel1.Location = new System.Drawing.Point(3, 25);
			this.reportExplorerDockPanel1.Name = "reportExplorerDockPanel1";
			this.reportExplorerDockPanel1.Size = new System.Drawing.Size(244, 304);
			this.reportExplorerDockPanel1.Text = "Rapor Gezgini";
			this.reportExplorerDockPanel1.XRDesignPanel = this.xrDesignPanel;
			// 
			// reportExplorerDockPanel1_Container
			// 
			this.reportExplorerDockPanel1_Container.Location = new System.Drawing.Point(0, 0);
			this.reportExplorerDockPanel1_Container.Name = "reportExplorerDockPanel1_Container";
			this.reportExplorerDockPanel1_Container.Size = new System.Drawing.Size(244, 304);
			this.reportExplorerDockPanel1_Container.TabIndex = 0;
			// 
			// fieldListDockPanel1
			// 
			this.fieldListDockPanel1.Controls.Add(this.fieldListDockPanel1_Container);
			this.fieldListDockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
			this.fieldListDockPanel1.ID = new System.Guid("faf69838-a93f-4114-83e8-d0d09cc5ce95");
			this.fieldListDockPanel1.ImageIndex = 0;
			this.fieldListDockPanel1.Location = new System.Drawing.Point(3, 25);
			this.fieldListDockPanel1.Name = "fieldListDockPanel1";
			this.fieldListDockPanel1.Size = new System.Drawing.Size(244, 304);
			this.fieldListDockPanel1.Text = "Alan Listesi";
			this.fieldListDockPanel1.XRDesignPanel = this.xrDesignPanel;
			// 
			// fieldListDockPanel1_Container
			// 
			this.fieldListDockPanel1_Container.Location = new System.Drawing.Point(0, 0);
			this.fieldListDockPanel1_Container.Name = "fieldListDockPanel1_Container";
			this.fieldListDockPanel1_Container.Size = new System.Drawing.Size(244, 304);
			this.fieldListDockPanel1_Container.TabIndex = 0;
			// 
			// propertyGridDockPanel1
			// 
			this.propertyGridDockPanel1.Controls.Add(this.propertyGridDockPanel1_Container);
			this.propertyGridDockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
			this.propertyGridDockPanel1.ID = new System.Guid("b38d12c3-cd06-4dec-b93d-63a0088e495a");
			this.propertyGridDockPanel1.ImageIndex = 1;
			this.propertyGridDockPanel1.Location = new System.Drawing.Point(0, 355);
			this.propertyGridDockPanel1.Name = "propertyGridDockPanel1";
			this.propertyGridDockPanel1.Size = new System.Drawing.Size(250, 117);
			this.propertyGridDockPanel1.Text = "Özellikler";
			this.propertyGridDockPanel1.XRDesignPanel = this.xrDesignPanel;
			// 
			// propertyGridDockPanel1_Container
			// 
			this.propertyGridDockPanel1_Container.Location = new System.Drawing.Point(3, 25);
			this.propertyGridDockPanel1_Container.Name = "propertyGridDockPanel1_Container";
			this.propertyGridDockPanel1_Container.Size = new System.Drawing.Size(244, 89);
			this.propertyGridDockPanel1_Container.TabIndex = 0;
			// 
			// toolBoxDockPanel1
			// 
			this.toolBoxDockPanel1.Controls.Add(this.toolBoxDockPanel1_Container);
			this.toolBoxDockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
			this.toolBoxDockPanel1.ID = new System.Guid("161a5a1a-d9b9-4f06-9ac4-d0c3e507c54f");
			this.toolBoxDockPanel1.ImageIndex = 3;
			this.toolBoxDockPanel1.Location = new System.Drawing.Point(0, 76);
			this.toolBoxDockPanel1.Name = "toolBoxDockPanel1";
			this.toolBoxDockPanel1.Size = new System.Drawing.Size(165, 472);
			this.toolBoxDockPanel1.Text = "Araç Kutusu";
			this.toolBoxDockPanel1.XRDesignPanel = this.xrDesignPanel;
			// 
			// toolBoxDockPanel1_Container
			// 
			this.toolBoxDockPanel1_Container.Location = new System.Drawing.Point(3, 25);
			this.toolBoxDockPanel1_Container.Name = "toolBoxDockPanel1_Container";
			this.toolBoxDockPanel1_Container.Size = new System.Drawing.Size(159, 444);
			this.toolBoxDockPanel1_Container.TabIndex = 0;
			// 
			// groupAndSortDockPanel1
			// 
			this.groupAndSortDockPanel1.Controls.Add(this.groupAndSortDockPanel1_Container);
			this.groupAndSortDockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
			this.groupAndSortDockPanel1.ID = new System.Guid("4bab159e-c495-4d67-87dc-f4e895da443e");
			this.groupAndSortDockPanel1.ImageIndex = 4;
			this.groupAndSortDockPanel1.Location = new System.Drawing.Point(165, 388);
			this.groupAndSortDockPanel1.Name = "groupAndSortDockPanel1";
			this.groupAndSortDockPanel1.Size = new System.Drawing.Size(667, 160);
			this.groupAndSortDockPanel1.Text = "Gruplama ve Sýralama";
			this.groupAndSortDockPanel1.XRDesignPanel = this.xrDesignPanel;
			// 
			// groupAndSortDockPanel1_Container
			// 
			this.groupAndSortDockPanel1_Container.Location = new System.Drawing.Point(3, 25);
			this.groupAndSortDockPanel1_Container.Name = "groupAndSortDockPanel1_Container";
			this.groupAndSortDockPanel1_Container.Size = new System.Drawing.Size(411, 132);
			this.groupAndSortDockPanel1_Container.TabIndex = 0;
			// 
			// CustomDesignForm
			// 
			this.ClientSize = new System.Drawing.Size(832, 571);
			this.Controls.Add(this.panelContainer1);
			this.Controls.Add(this.groupAndSortDockPanel1);
			this.Controls.Add(this.toolBoxDockPanel1);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Name = "CustomDesignForm";
			this.Controls.SetChildIndex(this.barDockControlTop, 0);
			this.Controls.SetChildIndex(this.barDockControlBottom, 0);
			this.Controls.SetChildIndex(this.barDockControlRight, 0);
			this.Controls.SetChildIndex(this.barDockControlLeft, 0);
			this.Controls.SetChildIndex(this.toolBoxDockPanel1, 0);
			this.Controls.SetChildIndex(this.groupAndSortDockPanel1, 0);
			this.Controls.SetChildIndex(this.panelContainer1, 0);
			this.Controls.SetChildIndex(this.xrDesignPanel, 0);
			((System.ComponentModel.ISupportInitialize)(this.xrDesignPanel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xrDesignBarManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ricbFontName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ricbFontSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.designRepositoryItemComboBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xrDesignDockManager1)).EndInit();
			this.panelContainer1.ResumeLayout(false);
			this.panelContainer2.ResumeLayout(false);
			this.reportExplorerDockPanel1.ResumeLayout(false);
			this.fieldListDockPanel1.ResumeLayout(false);
			this.propertyGridDockPanel1.ResumeLayout(false);
			this.toolBoxDockPanel1.ResumeLayout(false);
			this.groupAndSortDockPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }
        #endregion

        protected override void SaveLayout() { }
        protected override void RestoreLayout() { }
    }
}