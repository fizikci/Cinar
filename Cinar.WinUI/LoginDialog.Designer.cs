namespace Cinar.WinUI
{
    partial class LoginDialog
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
            this.editUserName = new DevExpress.XtraEditors.TextEdit();
            this.editPassword = new DevExpress.XtraEditors.TextEdit();
            this.lblUsername = new DevExpress.XtraEditors.LabelControl();
            this.lblPassword = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.editUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // editUserName
            // 
            this.editUserName.Location = new System.Drawing.Point(89, 16);
            this.editUserName.Name = "editUserName";
            this.editUserName.Size = new System.Drawing.Size(161, 20);
            this.editUserName.TabIndex = 0;
            this.editUserName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editUserName_KeyUp);
            // 
            // editPassword
            // 
            this.editPassword.EditValue = "";
            this.editPassword.Location = new System.Drawing.Point(89, 51);
            this.editPassword.Name = "editPassword";
            this.editPassword.Properties.PasswordChar = '*';
            this.editPassword.Size = new System.Drawing.Size(161, 20);
            this.editPassword.TabIndex = 1;
            this.editPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editUserName_KeyUp);
            // 
            // lblUsername
            // 
            this.lblUsername.Location = new System.Drawing.Point(19, 19);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Kullanýcý Adý";
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(19, 54);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(22, 13);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Þifre";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(89, 94);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "Tamam";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(175, 94);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Vazgeç";
            // 
            // LoginDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 129);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.editPassword);
            this.Controls.Add(this.editUserName);
            this.Name = "LoginDialog";
            this.Text = "Oturum";
            ((System.ComponentModel.ISupportInitialize)(this.editUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editPassword.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit editUserName;
        private DevExpress.XtraEditors.TextEdit editPassword;
        private DevExpress.XtraEditors.LabelControl lblUsername;
        private DevExpress.XtraEditors.LabelControl lblPassword;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}