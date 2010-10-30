namespace Cinar.WinApp.DefaultPlugIn.Forms
{
    partial class FormRoleUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRoleUser));
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.editRole = new Cinar.WinUI.LookUp();
            this.editUser = new Cinar.WinUI.LookUp();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutDbtUser = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutRole = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDbtUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutRole)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutControl.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutControl.Controls.Add(this.editRole);
            this.layoutControl.Controls.Add(this.editUser);
            this.layoutControl.Location = new System.Drawing.Point(4, 3);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Root = this.layoutControlGroup1;
            this.layoutControl.Size = new System.Drawing.Size(517, 67);
            this.layoutControl.TabIndex = 0;
            this.layoutControl.Text = "layoutControl";
            // 
            // editRole
            // 
            this.editRole.DependentFieldName = null;
            this.editRole.DependsOn = null;
            this.editRole.Filter = null;
            this.editRole.Location = new System.Drawing.Point(70, 7);
            this.editRole.Name = "editRole";
            this.editRole.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.editRole.ShowComboButton = true;
            this.editRole.Size = new System.Drawing.Size(441, 20);
            this.editRole.TabIndex = 7;
            // 
            // editUser
            // 
            this.editUser.DependentFieldName = null;
            this.editUser.DependsOn = null;
            this.editUser.Filter = null;
            this.editUser.Location = new System.Drawing.Point(70, 38);
            this.editUser.Name = "editUser";
            this.editUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.editUser.ShowComboButton = true;
            this.editUser.Size = new System.Drawing.Size(441, 20);
            this.editUser.TabIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutDbtUser,
            this.layoutRole});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(517, 67);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutDbtUser
            // 
            this.layoutDbtUser.Control = this.editUser;
            this.layoutDbtUser.CustomizationFormText = "Kullanýcý";
            this.layoutDbtUser.Image = ((System.Drawing.Image)(resources.GetObject("layoutDbtUser.Image")));
            this.layoutDbtUser.Location = new System.Drawing.Point(0, 31);
            this.layoutDbtUser.Name = "layoutDbtUser";
            this.layoutDbtUser.Size = new System.Drawing.Size(515, 34);
            this.layoutDbtUser.Text = "Kullanýcý";
            this.layoutDbtUser.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutDbtUser.TextSize = new System.Drawing.Size(58, 16);
            // 
            // layoutRole
            // 
            this.layoutRole.Control = this.editRole;
            this.layoutRole.CustomizationFormText = "Rol";
            this.layoutRole.Image = ((System.Drawing.Image)(resources.GetObject("layoutRole.Image")));
            this.layoutRole.Location = new System.Drawing.Point(0, 0);
            this.layoutRole.Name = "layoutRole";
            this.layoutRole.Size = new System.Drawing.Size(515, 31);
            this.layoutRole.Text = "Rol";
            this.layoutRole.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutRole.TextSize = new System.Drawing.Size(58, 16);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(425, 76);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Vazgeç";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(323, 76);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Tamam";
            // 
            // FormRoleUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 103);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.layoutControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormRoleUser";
            this.Text = "Kullanýcý - Rol Tanýmý";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDbtUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutRole)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private Cinar.WinUI.LookUp editUser;
        private DevExpress.XtraLayout.LayoutControlItem layoutDbtUser;
        private Cinar.WinUI.LookUp editRole;
        private DevExpress.XtraLayout.LayoutControlItem layoutRole;
    }
}