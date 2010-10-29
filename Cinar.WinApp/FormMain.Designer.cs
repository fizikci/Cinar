namespace Cinar.WinApp
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.MainMenu = new DevExpress.XtraBars.Bar();
            this.menuFile = new DevExpress.XtraBars.BarSubItem();
            this.menuExit = new DevExpress.XtraBars.BarButtonItem();
            this.menuCommands = new DevExpress.XtraBars.BarSubItem();
            this.menuView = new DevExpress.XtraBars.BarSubItem();
            this.menuSkins = new DevExpress.XtraBars.BarSubItem();
            this.menuChangeView = new DevExpress.XtraBars.BarButtonItem();
            this.menuHelp = new DevExpress.XtraBars.BarSubItem();
            this.menuAbout = new DevExpress.XtraBars.BarButtonItem();
            this.StatusBar = new DevExpress.XtraBars.Bar();
            this.statusBarStaticItem = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnNewCommands = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.btnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.btnPrintCommands = new DevExpress.XtraBars.BarSubItem();
            this.btnPrintList = new DevExpress.XtraBars.BarButtonItem();
            this.btnPrintSelectedEntity = new DevExpress.XtraBars.BarButtonItem();
            this.lblBul = new DevExpress.XtraBars.BarStaticItem();
            this.txtBarItemBul = new DevExpress.XtraBars.BarEditItem();
            this.txtBul = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.navBar = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBul)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.MainMenu,
            this.StatusBar});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.statusBarStaticItem,
            this.menuFile,
            this.menuExit,
            this.menuHelp,
            this.menuAbout,
            this.menuView,
            this.menuSkins,
            this.menuCommands,
            this.menuChangeView,
            this.btnNewCommands,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.btnDelete,
            this.btnPrintCommands,
            this.btnPrintList,
            this.btnPrintSelectedEntity,
            this.lblBul,
            this.txtBarItemBul,
            this.barButtonItem4});
            this.barManager.MainMenu = this.MainMenu;
            this.barManager.MaxItemId = 25;
            this.barManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.txtBul});
            this.barManager.StatusBar = this.StatusBar;
            // 
            // MainMenu
            // 
            this.MainMenu.BarName = "Main menu";
            this.MainMenu.DockCol = 0;
            this.MainMenu.DockRow = 0;
            this.MainMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.MainMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuFile, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuCommands),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuView),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuHelp)});
            this.MainMenu.OptionsBar.AllowQuickCustomization = false;
            this.MainMenu.OptionsBar.DrawDragBorder = false;
            this.MainMenu.OptionsBar.MultiLine = true;
            this.MainMenu.OptionsBar.UseWholeRow = true;
            this.MainMenu.Text = "Main menu";
            // 
            // menuFile
            // 
            this.menuFile.Caption = "Dosya";
            this.menuFile.Id = 3;
            this.menuFile.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuExit)});
            this.menuFile.Name = "menuFile";
            // 
            // menuExit
            // 
            this.menuExit.Caption = "Çýkýþ";
            this.menuExit.Id = 4;
            this.menuExit.Name = "menuExit";
            // 
            // menuCommands
            // 
            this.menuCommands.Caption = "Ýþlemler";
            this.menuCommands.Id = 9;
            this.menuCommands.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4)});
            this.menuCommands.Name = "menuCommands";
            // 
            // menuView
            // 
            this.menuView.Caption = "Görünüm";
            this.menuView.Id = 7;
            this.menuView.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuSkins),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuChangeView)});
            this.menuView.Name = "menuView";
            // 
            // menuSkins
            // 
            this.menuSkins.Caption = "Temalar";
            this.menuSkins.Id = 8;
            this.menuSkins.Name = "menuSkins";
            // 
            // menuChangeView
            // 
            this.menuChangeView.Caption = "Görünümü Deðiþtir (dikey/yatay)";
            this.menuChangeView.Id = 10;
            this.menuChangeView.Name = "menuChangeView";
            // 
            // menuHelp
            // 
            this.menuHelp.Caption = "Yardým";
            this.menuHelp.Id = 5;
            this.menuHelp.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuAbout)});
            this.menuHelp.Name = "menuHelp";
            // 
            // menuAbout
            // 
            this.menuAbout.Caption = "Hakkýnda...";
            this.menuAbout.Id = 6;
            this.menuAbout.Name = "menuAbout";
            // 
            // StatusBar
            // 
            this.StatusBar.BarName = "Status bar";
            this.StatusBar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.StatusBar.DockCol = 0;
            this.StatusBar.DockRow = 0;
            this.StatusBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.StatusBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.statusBarStaticItem)});
            this.StatusBar.OptionsBar.AllowQuickCustomization = false;
            this.StatusBar.OptionsBar.DrawDragBorder = false;
            this.StatusBar.OptionsBar.UseWholeRow = true;
            this.StatusBar.Text = "Status bar";
            // 
            // statusBarStaticItem
            // 
            this.statusBarStaticItem.Id = 0;
            this.statusBarStaticItem.Name = "statusBarStaticItem";
            this.statusBarStaticItem.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // btnNewCommands
            // 
            this.btnNewCommands.Caption = "Yeni";
            this.btnNewCommands.Glyph = ((System.Drawing.Image)(resources.GetObject("btnNewCommands.Glyph")));
            this.btnNewCommands.Id = 13;
            this.btnNewCommands.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.btnNewCommands.Name = "btnNewCommands";
            this.btnNewCommands.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Müþteri";
            this.barButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.Glyph")));
            this.barButtonItem1.Id = 14;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Kategori";
            this.barButtonItem2.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.Glyph")));
            this.barButtonItem2.Id = 15;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Yazdýr";
            this.barButtonItem3.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.Glyph")));
            this.barButtonItem3.Id = 16;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // btnDelete
            // 
            this.btnDelete.Caption = "Sil";
            this.btnDelete.Glyph = ((System.Drawing.Image)(resources.GetObject("btnDelete.Glyph")));
            this.btnDelete.Id = 17;
            this.btnDelete.Name = "btnDelete";
            // 
            // btnPrintCommands
            // 
            this.btnPrintCommands.Caption = "Yazdýr";
            this.btnPrintCommands.Glyph = ((System.Drawing.Image)(resources.GetObject("btnPrintCommands.Glyph")));
            this.btnPrintCommands.Id = 18;
            this.btnPrintCommands.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnPrintList),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnPrintSelectedEntity)});
            this.btnPrintCommands.Name = "btnPrintCommands";
            this.btnPrintCommands.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // btnPrintList
            // 
            this.btnPrintList.Caption = "Listeyi Yazdýr";
            this.btnPrintList.Id = 19;
            this.btnPrintList.Name = "btnPrintList";
            // 
            // btnPrintSelectedEntity
            // 
            this.btnPrintSelectedEntity.Caption = "Seçilen Kaydý Yazdýr";
            this.btnPrintSelectedEntity.Id = 20;
            this.btnPrintSelectedEntity.Name = "btnPrintSelectedEntity";
            // 
            // lblBul
            // 
            this.lblBul.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lblBul.Caption = "Bul :";
            this.lblBul.Id = 21;
            this.lblBul.Name = "lblBul";
            this.lblBul.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // txtBarItemBul
            // 
            this.txtBarItemBul.Caption = "barEditItem1";
            this.txtBarItemBul.Edit = this.txtBul;
            this.txtBarItemBul.Id = 22;
            this.txtBarItemBul.Name = "txtBarItemBul";
            // 
            // txtBul
            // 
            this.txtBul.AutoHeight = false;
            this.txtBul.Name = "txtBul";
            // 
            // navBar
            // 
            this.navBar.ActiveGroup = this.navBarGroup1;
            this.navBar.AllowSelectedLink = true;
            this.navBar.ContentButtonHint = null;
            this.navBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBar.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
            this.navBar.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItem1});
            this.navBar.Location = new System.Drawing.Point(0, 25);
            this.navBar.MinimumSize = new System.Drawing.Size(30, 0);
            this.navBar.Name = "navBar";
            this.navBar.OptionsNavPane.ExpandedWidth = 269;
            this.navBar.OptionsNavPane.ShowOverflowPanel = false;
            this.navBar.OptionsNavPane.ShowSplitter = false;
            this.navBar.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBar.Size = new System.Drawing.Size(194, 691);
            this.navBar.TabIndex = 0;
            this.navBar.Text = "navBarControl1";
            this.navBar.View = new DevExpress.XtraNavBar.ViewInfo.SkinNavigationPaneViewInfoRegistrator();
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "Modül Adý (otomatik)";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem1)});
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarItem1
            // 
            this.navBarItem1.Caption = "Form Adý (bunlar otomatik oluþturulur, deðiþtirmeyiniz)";
            this.navBarItem1.Name = "navBarItem1";
            this.navBarItem1.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarItem1.SmallImage")));
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPagesAndTabControlHeader;
            this.xtraTabbedMdiManager1.HeaderButtons = ((DevExpress.XtraTab.TabButtons)((DevExpress.XtraTab.TabButtons.Close | DevExpress.XtraTab.TabButtons.Default)));
            this.xtraTabbedMdiManager1.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Always;
            this.xtraTabbedMdiManager1.MdiParent = this;
            // 
            // FormDMT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 740);
            this.Controls.Add(this.navBar);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IsMdiContainer = true;
            this.Name = "FormMain";
            this.Text = "Çýnar WinApp";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBul)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        protected DevExpress.XtraBars.Bar MainMenu;
        protected DevExpress.XtraBars.Bar StatusBar;
        private DevExpress.XtraBars.BarStaticItem statusBarStaticItem;
        private DevExpress.XtraBars.BarSubItem menuFile;
        private DevExpress.XtraBars.BarButtonItem menuExit;
        private DevExpress.XtraBars.BarSubItem menuHelp;
        private DevExpress.XtraBars.BarButtonItem menuAbout;
        private DevExpress.XtraBars.BarSubItem menuView;
        private DevExpress.XtraBars.BarSubItem menuSkins;
        private DevExpress.XtraBars.BarSubItem menuCommands;
        private DevExpress.XtraNavBar.NavBarControl navBar;
        private DevExpress.XtraBars.BarButtonItem menuChangeView;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarItem navBarItem1;
        private DevExpress.XtraBars.BarSubItem btnNewCommands;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem btnDelete;
        private DevExpress.XtraBars.BarSubItem btnPrintCommands;
        private DevExpress.XtraBars.BarButtonItem btnPrintList;
        private DevExpress.XtraBars.BarButtonItem btnPrintSelectedEntity;
        private DevExpress.XtraBars.BarStaticItem lblBul;
        private DevExpress.XtraBars.BarEditItem txtBarItemBul;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtBul;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;

    }
}