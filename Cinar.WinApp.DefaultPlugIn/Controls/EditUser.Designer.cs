namespace Cinar.WinApp.DefaultPlugIn.Controls
{
    partial class EditUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditUser));
            this.layoutEntity = new DevExpress.XtraLayout.LayoutControl();
            this.editName = new Cinar.WinUI.LookUp();
            this.editEmail = new DevExpress.XtraEditors.TextEdit();
            this.editPassword = new DevExpress.XtraEditors.TextEdit();
            this.editUserName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnAddDbtUser = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveDbtUser = new DevExpress.XtraEditors.SimpleButton();
            this.panelDetail = new DevExpress.XtraEditors.PanelControl();
            this.listEntityRoleUser = new Cinar.WinUI.ListEntitySimple();
            this.btnDeleteDbtUser = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutEntity)).BeginInit();
            this.layoutEntity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
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
            this.layoutEntity.Controls.Add(this.editEmail);
            this.layoutEntity.Controls.Add(this.editPassword);
            this.layoutEntity.Controls.Add(this.editUserName);
            this.layoutEntity.Location = new System.Drawing.Point(3, 3);
            this.layoutEntity.Name = "layoutEntity";
            this.layoutEntity.Root = this.layoutControlGroup1;
            this.layoutEntity.Size = new System.Drawing.Size(884, 97);
            this.layoutEntity.TabIndex = 0;
            // 
            // editName
            // 
            this.editName.DependentFieldName = null;
            this.editName.DependsOn = null;
            this.editName.DisplayFieldName = "NameSurname";
            this.editName.Filter = null;
            this.editName.Location = new System.Drawing.Point(67, 7);
            this.editName.Name = "editName";
            this.editName.ShowEllipsisButton = false;
            this.editName.Size = new System.Drawing.Size(811, 20);
            this.editName.TabIndex = 26;
            // 
            // editEmail
            // 
            this.editEmail.Location = new System.Drawing.Point(67, 69);
            this.editEmail.Name = "editEmail";
            this.editEmail.Size = new System.Drawing.Size(811, 20);
            this.editEmail.TabIndex = 23;
            // 
            // editPassword
            // 
            this.editPassword.EditValue = "";
            this.editPassword.Location = new System.Drawing.Point(508, 38);
            this.editPassword.Name = "editPassword";
            this.editPassword.Properties.Appearance.Font = new System.Drawing.Font("Wingdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.editPassword.Properties.Appearance.Options.UseFont = true;
            this.editPassword.Properties.PasswordChar = 'l';
            this.editPassword.Size = new System.Drawing.Size(370, 19);
            this.editPassword.TabIndex = 22;
            // 
            // editUserName
            // 
            this.editUserName.Location = new System.Drawing.Point(67, 38);
            this.editUserName.Name = "editUserName";
            this.editUserName.Size = new System.Drawing.Size(370, 20);
            this.editUserName.TabIndex = 21;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem2,
            this.layoutControlItemName,
            this.layoutControlItem7});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(884, 97);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.editUserName;
            this.layoutControlItem6.CustomizationFormText = "Kullanýcý Adý";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(441, 31);
            this.layoutControlItem6.Text = "Kullanýcý Adý";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(55, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.editEmail;
            this.layoutControlItem2.CustomizationFormText = "E-Posta";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 62);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(882, 33);
            this.layoutControlItem2.Text = "E-Posta";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(55, 13);
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.Control = this.editName;
            this.layoutControlItemName.CustomizationFormText = "Ad Soyad";
            this.layoutControlItemName.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemName.Name = "layoutControlItemName";
            this.layoutControlItemName.Size = new System.Drawing.Size(882, 31);
            this.layoutControlItemName.Text = "Ad Soyad";
            this.layoutControlItemName.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItemName.TextSize = new System.Drawing.Size(55, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.editPassword;
            this.layoutControlItem7.CustomizationFormText = "Þifre";
            this.layoutControlItem7.Location = new System.Drawing.Point(441, 31);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(441, 31);
            this.layoutControlItem7.Text = "Þifre";
            this.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(55, 13);
            // 
            // btnAddDbtUser
            // 
            this.btnAddDbtUser.Image = ((System.Drawing.Image)(resources.GetObject("btnAddDbtUser.Image")));
            this.btnAddDbtUser.ImageIndex = 1;
            this.btnAddDbtUser.Location = new System.Drawing.Point(6, 106);
            this.btnAddDbtUser.Name = "btnAddDbtUser";
            this.btnAddDbtUser.Size = new System.Drawing.Size(114, 22);
            this.btnAddDbtUser.TabIndex = 13;
            this.btnAddDbtUser.Text = "Yeni Kullanýcý Ekle";
            // 
            // btnSaveDbtUser
            // 
            this.btnSaveDbtUser.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveDbtUser.Image")));
            this.btnSaveDbtUser.ImageIndex = 0;
            this.btnSaveDbtUser.Location = new System.Drawing.Point(135, 106);
            this.btnSaveDbtUser.Name = "btnSaveDbtUser";
            this.btnSaveDbtUser.Size = new System.Drawing.Size(82, 22);
            this.btnSaveDbtUser.TabIndex = 14;
            this.btnSaveDbtUser.Text = "Kaydet";
            // 
            // panelDetail
            // 
            this.panelDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetail.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelDetail.Controls.Add(this.listEntityRoleUser);
            this.panelDetail.Location = new System.Drawing.Point(2, 131);
            this.panelDetail.Margin = new System.Windows.Forms.Padding(0);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(885, 284);
            this.panelDetail.TabIndex = 23;
            // 
            // listEntityRoleUser
            // 
            this.listEntityRoleUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listEntityRoleUser.DeleteWarningMessage = "Seçilen rol ile kullanýcý arasýndaki iliþki silinecek. Onaylýyor musunuz?";
            this.listEntityRoleUser.EntityType = typeof(Cinar.Entities.Standart.RoleUser);
            this.listEntityRoleUser.ImageForEntity = ((System.Drawing.Bitmap)(resources.GetObject("listEntityRoleUser.ImageForEntity")));
            this.listEntityRoleUser.Location = new System.Drawing.Point(4, 3);
            this.listEntityRoleUser.Name = "listEntityRoleUser";
            this.listEntityRoleUser.PageSize = 0;
            this.listEntityRoleUser.ShowAddButton = true;
            this.listEntityRoleUser.ShowDeleteButton = true;
            this.listEntityRoleUser.ShowEditButton = true;
            this.listEntityRoleUser.ShowExcelButton = false;
            this.listEntityRoleUser.ShowHistoryButton = false;
            this.listEntityRoleUser.ShowTotalCount = true;
            this.listEntityRoleUser.Size = new System.Drawing.Size(879, 278);
            this.listEntityRoleUser.TabIndex = 26;
            this.listEntityRoleUser.Title = "Kullanýcýnýn sahip olduðu roller";
            // 
            // btnDeleteDbtUser
            // 
            this.btnDeleteDbtUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteDbtUser.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteDbtUser.Image")));
            this.btnDeleteDbtUser.ImageIndex = 2;
            this.btnDeleteDbtUser.Location = new System.Drawing.Point(792, 106);
            this.btnDeleteDbtUser.Name = "btnDeleteDbtUser";
            this.btnDeleteDbtUser.Size = new System.Drawing.Size(98, 22);
            this.btnDeleteDbtUser.TabIndex = 24;
            this.btnDeleteDbtUser.Text = "Kullanýcýyý Sil";
            // 
            // EditUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDeleteDbtUser);
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.layoutEntity);
            this.Controls.Add(this.btnAddDbtUser);
            this.Controls.Add(this.btnSaveDbtUser);
            this.Name = "EditUser";
            this.Size = new System.Drawing.Size(912, 432);
            ((System.ComponentModel.ISupportInitialize)(this.layoutEntity)).EndInit();
            this.layoutEntity.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelDetail)).EndInit();
            this.panelDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutEntity;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnSaveDbtUser;
        private DevExpress.XtraEditors.SimpleButton btnAddDbtUser;
        private DevExpress.XtraEditors.PanelControl panelDetail;
        private DevExpress.XtraEditors.SimpleButton btnDeleteDbtUser;
        private Cinar.WinUI.ListEntitySimple listEntityRoleUser;
        private DevExpress.XtraEditors.TextEdit editUserName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.TextEdit editPassword;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.TextEdit editEmail;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Cinar.WinUI.LookUp editName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemName;
    }
}