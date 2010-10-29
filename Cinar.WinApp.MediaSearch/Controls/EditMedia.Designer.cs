namespace Cinar.WinApp.MediaSearch.Controls
{
    partial class EditMedia
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
            this.layoutEntity = new DevExpress.XtraLayout.LayoutControl();
            this.editName = new Cinar.WinUI.LookUp();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnAddMedia = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveMedia = new DevExpress.XtraEditors.SimpleButton();
            this.panelDetail = new DevExpress.XtraEditors.PanelControl();
            this.listEntityContentDefinition = new Cinar.WinUI.ListEntitySimple();
            this.listEntityAuthor = new Cinar.WinUI.ListEntitySimple();
            this.btnDeleteMedia = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutEntity)).BeginInit();
            this.layoutEntity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelDetail)).BeginInit();
            this.panelDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutEntity
            // 
            this.layoutEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutEntity.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutEntity.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutEntity.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutEntity.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutEntity.Controls.Add(this.editName);
            this.layoutEntity.Location = new System.Drawing.Point(3, 3);
            this.layoutEntity.Name = "layoutEntity";
            this.layoutEntity.Root = this.layoutControlGroup1;
            this.layoutEntity.Size = new System.Drawing.Size(553, 34);
            this.layoutEntity.TabIndex = 0;
            // 
            // editName
            // 
            this.editName.DependentFieldName = null;
            this.editName.DependsOn = null;
            this.editName.Filter = null;
            this.editName.Location = new System.Drawing.Point(62, 7);
            this.editName.Name = "editName";
            this.editName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.editName.ShowEllipsisButton = false;
            this.editName.Size = new System.Drawing.Size(485, 20);
            this.editName.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemName});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(553, 34);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.Control = this.editName;
            this.layoutControlItemName.CustomizationFormText = "Medya Adý";
            this.layoutControlItemName.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemName.Name = "layoutControlItemName";
            this.layoutControlItemName.Size = new System.Drawing.Size(551, 32);
            this.layoutControlItemName.Text = "Medya Adý";
            this.layoutControlItemName.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItemName.TextSize = new System.Drawing.Size(50, 13);
            // 
            // btnAddMedia
            // 
            this.btnAddMedia.ImageIndex = 1;
            this.btnAddMedia.Location = new System.Drawing.Point(3, 43);
            this.btnAddMedia.Name = "btnAddMedia";
            this.btnAddMedia.Size = new System.Drawing.Size(114, 22);
            this.btnAddMedia.TabIndex = 13;
            this.btnAddMedia.Text = "Yeni Medya Ekle";
            // 
            // btnSaveMedia
            // 
            this.btnSaveMedia.ImageIndex = 0;
            this.btnSaveMedia.Location = new System.Drawing.Point(132, 43);
            this.btnSaveMedia.Name = "btnSaveMedia";
            this.btnSaveMedia.Size = new System.Drawing.Size(82, 22);
            this.btnSaveMedia.TabIndex = 14;
            this.btnSaveMedia.Text = "Kaydet";
            // 
            // panelDetail
            // 
            this.panelDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetail.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelDetail.Controls.Add(this.listEntityContentDefinition);
            this.panelDetail.Controls.Add(this.listEntityAuthor);
            this.panelDetail.Location = new System.Drawing.Point(3, 74);
            this.panelDetail.Margin = new System.Windows.Forms.Padding(0);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(554, 493);
            this.panelDetail.TabIndex = 23;
            // 
            // listEntityContentDefinition
            // 
            this.listEntityContentDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listEntityContentDefinition.DeleteWarningMessage = "Seçilen yetki ile rol arasýndaki iliþki silinecek. Onaylýyor musunuz?";
            this.listEntityContentDefinition.EntityType = null;
            this.listEntityContentDefinition.ImageForEntity = null;
            this.listEntityContentDefinition.Location = new System.Drawing.Point(3, 3);
            this.listEntityContentDefinition.Name = "listEntityContentDefinition";
            this.listEntityContentDefinition.PageSize = 0;
            this.listEntityContentDefinition.ShowAddButton = true;
            this.listEntityContentDefinition.ShowDeleteButton = true;
            this.listEntityContentDefinition.ShowEditButton = true;
            this.listEntityContentDefinition.ShowExcelButton = false;
            this.listEntityContentDefinition.ShowHistoryButton = false;
            this.listEntityContentDefinition.ShowTotalCount = true;
            this.listEntityContentDefinition.Size = new System.Drawing.Size(548, 225);
            this.listEntityContentDefinition.TabIndex = 27;
            this.listEntityContentDefinition.Title = "Bu medyadan takip edilen RSS adresleri";
            // 
            // listEntityAuthor
            // 
            this.listEntityAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listEntityAuthor.DeleteWarningMessage = "Seçilen kullanýcý ile rol arasýndaki iliþki silinecek. Onaylýyor musunuz?";
            this.listEntityAuthor.EntityType = null;
            this.listEntityAuthor.ImageForEntity = null;
            this.listEntityAuthor.Location = new System.Drawing.Point(3, 234);
            this.listEntityAuthor.Name = "listEntityAuthor";
            this.listEntityAuthor.PageSize = 0;
            this.listEntityAuthor.ShowAddButton = true;
            this.listEntityAuthor.ShowDeleteButton = true;
            this.listEntityAuthor.ShowEditButton = true;
            this.listEntityAuthor.ShowExcelButton = false;
            this.listEntityAuthor.ShowHistoryButton = false;
            this.listEntityAuthor.ShowTotalCount = true;
            this.listEntityAuthor.Size = new System.Drawing.Size(548, 256);
            this.listEntityAuthor.TabIndex = 26;
            this.listEntityAuthor.Title = "Bu medyada takip edilen yazarlar";
            // 
            // btnDeleteMedia
            // 
            this.btnDeleteMedia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteMedia.ImageIndex = 2;
            this.btnDeleteMedia.Location = new System.Drawing.Point(458, 43);
            this.btnDeleteMedia.Name = "btnDeleteMedia";
            this.btnDeleteMedia.Size = new System.Drawing.Size(98, 22);
            this.btnDeleteMedia.TabIndex = 24;
            this.btnDeleteMedia.Text = "Bu Medyayý Sil";
            // 
            // EditMedia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDeleteMedia);
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.layoutEntity);
            this.Controls.Add(this.btnAddMedia);
            this.Controls.Add(this.btnSaveMedia);
            this.Name = "EditMedia";
            this.Size = new System.Drawing.Size(581, 574);
            ((System.ComponentModel.ISupportInitialize)(this.layoutEntity)).EndInit();
            this.layoutEntity.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelDetail)).EndInit();
            this.panelDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutEntity;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Cinar.WinUI.LookUp editName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemName;
        private DevExpress.XtraEditors.SimpleButton btnSaveMedia;
        private DevExpress.XtraEditors.SimpleButton btnAddMedia;
        private DevExpress.XtraEditors.PanelControl panelDetail;
        private DevExpress.XtraEditors.SimpleButton btnDeleteMedia;
        private Cinar.WinUI.ListEntitySimple listEntityAuthor;
        private Cinar.WinUI.ListEntitySimple listEntityContentDefinition;
    }
}