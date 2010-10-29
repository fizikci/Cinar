namespace Cinar.WinUI
{
    partial class ListEntitySimple
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListEntitySimple));
            this.btnEditEntity = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddEntity = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteEntity = new DevExpress.XtraEditors.SimpleButton();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddEntity = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditEntity = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteEntity = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEditEntity
            // 
            this.btnEditEntity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditEntity.ImageIndex = 0;
            this.btnEditEntity.Location = new System.Drawing.Point(103, 197);
            this.btnEditEntity.Name = "btnEditEntity";
            this.btnEditEntity.Size = new System.Drawing.Size(121, 22);
            this.btnEditEntity.TabIndex = 29;
            this.btnEditEntity.Text = "Seçileni Düzenle";
            // 
            // btnAddEntity
            // 
            this.btnAddEntity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddEntity.ImageIndex = 1;
            this.btnAddEntity.Location = new System.Drawing.Point(0, 197);
            this.btnAddEntity.Name = "btnAddEntity";
            this.btnAddEntity.Size = new System.Drawing.Size(93, 22);
            this.btnAddEntity.TabIndex = 27;
            this.btnAddEntity.Text = "Yeni Ekle";
            // 
            // btnDeleteEntity
            // 
            this.btnDeleteEntity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteEntity.ImageIndex = 2;
            this.btnDeleteEntity.Location = new System.Drawing.Point(466, 197);
            this.btnDeleteEntity.Name = "btnDeleteEntity";
            this.btnDeleteEntity.Size = new System.Drawing.Size(98, 22);
            this.btnDeleteEntity.TabIndex = 28;
            this.btnDeleteEntity.Text = "Seçileni Sil";
            // 
            // pnlTitle
            // 
            this.pnlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(116)))), ((int)(((byte)(100)))));
            this.pnlTitle.Controls.Add(this.btnPrev);
            this.pnlTitle.Controls.Add(this.btnNext);
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Location = new System.Drawing.Point(0, 3);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(564, 22);
            this.pnlTitle.TabIndex = 25;
            // 
            // btnPrev
            // 
            this.btnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrev.Location = new System.Drawing.Point(502, 1);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(24, 19);
            this.btnPrev.TabIndex = 2;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(535, 1);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(24, 19);
            this.btnNext.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.Location = new System.Drawing.Point(3, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(64, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "     Baþlýk";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.ContextMenuStrip = this.contextMenu;
            this.grid.Location = new System.Drawing.Point(1, 30);
            this.grid.MainView = this.gridView;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(561, 162);
            this.grid.TabIndex = 26;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView,
            this.gridView4});
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCopy,
            this.menuAddEntity,
            this.menuEditEntity,
            this.menuDeleteEntity,
            this.menuHistory,
            this.menuExcel});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(162, 158);
            // 
            // menuCopy
            // 
            this.menuCopy.Image = ((System.Drawing.Image)(resources.GetObject("menuCopy.Image")));
            this.menuCopy.Name = "menuCopy";
            this.menuCopy.Size = new System.Drawing.Size(161, 22);
            this.menuCopy.Text = "Kopyala";
            // 
            // menuAddEntity
            // 
            this.menuAddEntity.Name = "menuAddEntity";
            this.menuAddEntity.Size = new System.Drawing.Size(161, 22);
            this.menuAddEntity.Text = "Yeni Ekle";
            // 
            // menuEditEntity
            // 
            this.menuEditEntity.Name = "menuEditEntity";
            this.menuEditEntity.Size = new System.Drawing.Size(161, 22);
            this.menuEditEntity.Text = "Seçileni Düzenle";
            // 
            // menuDeleteEntity
            // 
            this.menuDeleteEntity.Name = "menuDeleteEntity";
            this.menuDeleteEntity.Size = new System.Drawing.Size(161, 22);
            this.menuDeleteEntity.Text = "Seçileni Sil";
            // 
            // menuHistory
            // 
            this.menuHistory.Name = "menuHistory";
            this.menuHistory.Size = new System.Drawing.Size(161, 22);
            this.menuHistory.Text = "Tarihçe";
            // 
            // menuExcel
            // 
            this.menuExcel.Name = "menuExcel";
            this.menuExcel.Size = new System.Drawing.Size(161, 22);
            this.menuExcel.Text = "Excel\'de Aç";
            // 
            // gridView
            // 
            this.gridView.GridControl = this.grid;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsSelection.MultiSelect = true;
            this.gridView.OptionsView.ColumnAutoWidth = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.StartSorting += new System.EventHandler(this.gridView_StartSorting);
            this.gridView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView_RowStyle);
            // 
            // gridView4
            // 
            this.gridView4.GridControl = this.grid;
            this.gridView4.Name = "gridView4";
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.Location = new System.Drawing.Point(360, 197);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(100, 22);
            this.btnExcel.TabIndex = 34;
            this.btnExcel.Text = "Excel\'de Aç";
            // 
            // ListEntitySimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnEditEntity);
            this.Controls.Add(this.btnAddEntity);
            this.Controls.Add(this.btnDeleteEntity);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.grid);
            this.Name = "ListEntitySimple";
            this.Size = new System.Drawing.Size(564, 225);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.contextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnEditEntity;
        private DevExpress.XtraEditors.SimpleButton btnAddEntity;
        private DevExpress.XtraEditors.SimpleButton btnDeleteEntity;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblTitle;
        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuCopy;
        private System.Windows.Forms.ToolStripMenuItem menuHistory;
        private System.Windows.Forms.ToolStripMenuItem menuExcel;
        private System.Windows.Forms.ToolStripMenuItem menuAddEntity;
        private System.Windows.Forms.ToolStripMenuItem menuEditEntity;
        private System.Windows.Forms.ToolStripMenuItem menuDeleteEntity;
    }
}