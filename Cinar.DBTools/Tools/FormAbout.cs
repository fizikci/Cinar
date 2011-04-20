using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cinar.DBTools.Tools
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:bulentkeskin@gmail.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.cinarteknoloji.com");
        }

        private void linkLabel1_MouseEnter(object sender, EventArgs e)
        {
            (sender as LinkLabel).LinkColor = Color.Gold;
        }

        private void linkLabel1_MouseLeave(object sender, EventArgs e)
        {
            (sender as LinkLabel).LinkColor = Color.White;
        }
    }
}
