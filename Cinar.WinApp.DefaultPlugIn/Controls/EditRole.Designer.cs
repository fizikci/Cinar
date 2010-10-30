namespace Cinar.WinApp.DefaultPlugIn.Controls
{
    partial class EditRole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditRole));
            this.layoutEntity = new DevExpress.XtraLayout.LayoutControl();
            this.editName = new Cinar.WinUI.LookUp();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnAddRole = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveRole = new DevExpress.XtraEditors.SimpleButton();
            this.panelDetail = new DevExpress.XtraEditors.PanelControl();
            this.editRight = new Cinar.WinUI.LookUp();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnRemoveRight = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddRight = new DevExpress.XtraEditors.SimpleButton();
            this.listEntityRoleRight = new Cinar.WinUI.ListEntitySimple();
            this.listEntityRoleUser = new Cinar.WinUI.ListEntitySimple();
            this.btnDeleteRole = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutEntity)).BeginInit();
            this.layoutEntity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelDetail)).BeginInit();
            this.panelDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editRight.Properties)).BeginInit();
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
            this.editName.Location = new System.Drawing.Point(45, 7);
            this.editName.Name = "editName";
            this.editName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.editName.ShowEllipsisButton = false;
            this.editName.Size = new System.Drawing.Size(502, 20);
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
            this.layoutControlItemName.CustomizationFormText = "Rol Adý";
            this.layoutControlItemName.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemName.Name = "layoutControlItemName";
            this.layoutControlItemName.Size = new System.Drawing.Size(551, 32);
            this.layoutControlItemName.Text = "Rol Adý";
            this.layoutControlItemName.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItemName.TextSize = new System.Drawing.Size(33, 13);
            // 
            // btnAddRole
            // 
            this.btnAddRole.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRole.Image")));
            this.btnAddRole.ImageIndex = 1;
            this.btnAddRole.Location = new System.Drawing.Point(3, 43);
            this.btnAddRole.Name = "btnAddRole";
            this.btnAddRole.Size = new System.Drawing.Size(114, 22);
            this.btnAddRole.TabIndex = 13;
            this.btnAddRole.Text = "Yeni Rol Ekle";
            // 
            // btnSaveRole
            // 
            this.btnSaveRole.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveRole.Image")));
            this.btnSaveRole.ImageIndex = 0;
            this.btnSaveRole.Location = new System.Drawing.Point(132, 43);
            this.btnSaveRole.Name = "btnSaveRole";
            this.btnSaveRole.Size = new System.Drawing.Size(82, 22);
            this.btnSaveRole.TabIndex = 14;
            this.btnSaveRole.Text = "Kaydet";
            // 
            // panelDetail
            // 
            this.panelDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetail.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelDetail.Controls.Add(this.editRight);
            this.panelDetail.Controls.Add(this.labelControl2);
            this.panelDetail.Controls.Add(this.btnRemoveRight);
            this.panelDetail.Controls.Add(this.btnAddRight);
            this.panelDetail.Controls.Add(this.listEntityRoleRight);
            this.panelDetail.Controls.Add(this.listEntityRoleUser);
            this.panelDetail.Location = new System.Drawing.Point(3, 74);
            this.panelDetail.Margin = new System.Windows.Forms.Padding(0);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(554, 757);
            this.panelDetail.TabIndex = 23;
            // 
            // editRight
            // 
            this.editRight.DependentFieldName = null;
            this.editRight.DependsOn = null;
            this.editRight.Filter = null;
            this.editRight.Location = new System.Drawing.Point(47, 194);
            this.editRight.Name = "editRight";
            this.editRight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.editRight.ShowComboButton = true;
            this.editRight.ShowEllipsisButton = false;
            this.editRight.Size = new System.Drawing.Size(257, 20);
            this.editRight.TabIndex = 32;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 198);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(33, 13);
            this.labelControl2.TabIndex = 31;
            this.labelControl2.Text = "Yetki : ";
            // 
            // btnRemoveRight
            // 
            this.btnRemoveRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveRight.Location = new System.Drawing.Point(391, 193);
            this.btnRemoveRight.Name = "btnRemoveRight";
            this.btnRemoveRight.Size = new System.Drawing.Size(156, 23);
            this.btnRemoveRight.TabIndex = 30;
            this.btnRemoveRight.Text = "Seçilen yetkiyi KALDIR";
            // 
            // btnAddRight
            // 
            this.btnAddRight.Location = new System.Drawing.Point(310, 193);
            this.btnAddRight.Name = "btnAddRight";
            this.btnAddRight.Size = new System.Drawing.Size(44, 23);
            this.btnAddRight.TabIndex = 28;
            this.btnAddRight.Text = "Ekle";
            // 
            // listEntityRoleRight
            // 
            this.listEntityRoleRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listEntityRoleRight.DeleteWarningMessage = "Seçilen yetki ile rol arasýndaki iliþki silinecek. Onaylýyor musunuz?";
            this.listEntityRoleRight.EntityType = null;
            this.listEntityRoleRight.ImageForEntity = ((System.Drawing.Bitmap)(resources.GetObject("listEntityRoleRight.ImageForEntity")));
            this.listEntityRoleRight.Location = new System.Drawing.Point(3, 3);
            this.listEntityRoleRight.Name = "listEntityRoleRight";
            this.listEntityRoleRight.PageSize = 0;
            this.listEntityRoleRight.ShowAddButton = false;
            this.listEntityRoleRight.ShowDeleteButton = false;
            this.listEntityRoleRight.ShowEditButton = false;
            this.listEntityRoleRight.ShowExcelButton = false;
            this.listEntityRoleRight.ShowHistoryButton = false;
            this.listEntityRoleRight.ShowTotalCount = true;
            this.listEntityRoleRight.Size = new System.Drawing.Size(548, 189);
            this.listEntityRoleRight.TabIndex = 27;
            this.listEntityRoleRight.Title = "Bu rolün sahip olduðu yetkiler";
            // 
            // listEntityRoleUser
            // 
            this.listEntityRoleUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listEntityRoleUser.DeleteWarningMessage = "Seçilen kullanýcý ile rol arasýndaki iliþki silinecek. Onaylýyor musunuz?";
            this.listEntityRoleUser.EntityType = null;
            this.listEntityRoleUser.ImageForEntity = ((System.Drawing.Bitmap)(resources.GetObject("listEntityRoleUser.ImageForEntity")));
            this.listEntityRoleUser.Location = new System.Drawing.Point(3, 234);
            this.listEntityRoleUser.Name = "listEntityRoleUser";
            this.listEntityRoleUser.PageSize = 0;
            this.listEntityRoleUser.ShowAddButton = true;
            this.listEntityRoleUser.ShowDeleteButton = true;
            this.listEntityRoleUser.ShowEditButton = true;
            this.listEntityRoleUser.ShowExcelButton = false;
            this.listEntityRoleUser.ShowHistoryButton = false;
            this.listEntityRoleUser.ShowTotalCount = true;
            this.listEntityRoleUser.Size = new System.Drawing.Size(548, 508);
            this.listEntityRoleUser.TabIndex = 26;
            this.listEntityRoleUser.Title = "Bu role sahip olan kullanýcýlar";
            // 
            // btnDeleteRole
            // 
            this.btnDeleteRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteRole.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteRole.Image")));
            this.btnDeleteRole.ImageIndex = 2;
            this.btnDeleteRole.Location = new System.Drawing.Point(458, 43);
            this.btnDeleteRole.Name = "btnDeleteRole";
            this.btnDeleteRole.Size = new System.Drawing.Size(98, 22);
            this.btnDeleteRole.TabIndex = 24;
            this.btnDeleteRole.Text = "Bu Rolü Sil";
            // 
            // EditRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDeleteRole);
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.layoutEntity);
            this.Controls.Add(this.btnAddRole);
            this.Controls.Add(this.btnSaveRole);
            this.Name = "EditRole";
            this.Size = new System.Drawing.Size(581, 845);
            ((System.ComponentModel.ISupportInitialize)(this.layoutEntity)).EndInit();
            this.layoutEntity.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelDetail)).EndInit();
            this.panelDetail.ResumeLayout(false);
            this.panelDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editRight.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutEntity;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Cinar.WinUI.LookUp editName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemName;
        private DevExpress.XtraEditors.SimpleButton btnSaveRole;
        private DevExpress.XtraEditors.SimpleButton btnAddRole;
        private DevExpress.XtraEditors.PanelControl panelDetail;
        private DevExpress.XtraEditors.SimpleButton btnDeleteRole;
        private Cinar.WinUI.ListEntitySimple listEntityRoleUser;
        private Cinar.WinUI.ListEntitySimple listEntityRoleRight;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnRemoveRight;
        private DevExpress.XtraEditors.SimpleButton btnAddRight;
        private Cinar.WinUI.LookUp editRight;
    }
}