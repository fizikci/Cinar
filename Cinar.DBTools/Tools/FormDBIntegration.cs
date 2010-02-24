using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Database;

namespace Cinar.DBTools.Tools
{
    public partial class FormDBIntegration : Form
    {
        public FormDBIntegration()
        {
            InitializeComponent();

            BindDatabases();
        }

        private void BindDatabases()
        {
            cbDstDb.Items.Clear();
            cbSrcDb.Items.Clear();
            foreach (ConnectionSettings cs in Provider.Connections)
            {
                cbDstDb.Items.Add(cs);
                cbSrcDb.Items.Add(cs);
            }
            cbDstDb.SelectedIndex = 0;
            cbSrcDb.SelectedIndex = 0;
        }

        private void BindDstTables()
        {
            cbDstTable.Items.Clear();
            foreach (Table tbl in ((ConnectionSettings)cbDstDb.SelectedItem).Database.Tables)
                cbDstTable.Items.Add(tbl);
            cbDstTable.SelectedIndex = 0;
        }


        private void cbDstDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDstTables();
        }
    }
}
