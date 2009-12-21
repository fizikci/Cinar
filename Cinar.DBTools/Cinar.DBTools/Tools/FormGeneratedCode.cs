using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Cinar.DBTools.Tools
{
    public partial class FormGeneratedCode : Form
    {
        public FormGeneratedCode(string code)
        {
            InitializeComponent();

            txtCode.Text = code;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
                File.WriteAllText(sfd.FileName, txtCode.Text, Encoding.UTF8);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}