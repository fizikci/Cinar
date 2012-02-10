using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Database;
using Cinar.DBTools.Tools;

namespace Cinar.DBTools
{
    public partial class FormConnect : Form
    {
        public FormConnect()
        {
            InitializeComponent();
        }

        private void btnShowDatabases_Click(object sender, EventArgs e)
        {
            DatabaseProvider provider = cbProvider.Text.ToEnum<DatabaseProvider>();

            if (provider == DatabaseProvider.SQLite)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.CheckFileExists = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                    txtDBName.Text = ofd.FileName;
            }
            else
            {
                Database.Database db = new Database.Database();
                db.SetConnectionString(provider, txtHost.Text, null, txtUserName.Text, txtPassword.Text, 30);
                db.CreateDbProvider(false);

                ListBoxDialog lbd = new ListBoxDialog();
                lbd.ListBox.DataSource = db.GetDatabases();
                lbd.ListBox.SelectionMode = SelectionMode.One;
                lbd.Message = "Select a database";
                if (lbd.ShowDialog() == DialogResult.OK)
                    txtDBName.Text = lbd.GetSelectedItems<string>()[0];
            }
        }
    }
}
