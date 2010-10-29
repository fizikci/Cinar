using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cinar.WinUI
{
    public partial class LoginDialog : DevExpress.XtraEditors.XtraForm
    {
        public LoginDialog()
        {
            InitializeComponent();

            if (ConfigurationSettings.AppSettings["defaultUserName"] != null)
                editUserName.Text = ConfigurationSettings.AppSettings["defaultUserName"];
            if (ConfigurationSettings.AppSettings["defaultPassword"] != null)
                editPassword.Text = ConfigurationSettings.AppSettings["defaultPassword"];

            editUserName.Focus();
        }

        public string Username
        {
            get { return editUserName.Text; }
            set { editUserName.Text = value; }
        }

        public string Password
        {
            get { return editPassword.Text; }
            set { editPassword.Text = value; }
        }

        private void editUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                this.DialogResult = DialogResult.OK;
            else if (e.KeyCode == Keys.Escape)
                this.DialogResult = DialogResult.Cancel;
        }
    }
}