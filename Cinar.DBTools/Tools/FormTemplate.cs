using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cinar.DBTools.Tools
{
    public partial class FormTemplate : Form
    {
        Template tmp = null;

        public FormTemplate(Template tmp)
        {
            this.tmp = (Template)tmp.Clone();
            InitializeComponent();

            txtName.Text = tmp.Name;
            txtFileNameFormat.Text = tmp.FileNameFormat;
            txtCode.Text = tmp.Code;
            txtCategory.Text = tmp.Category;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            tmp.Name = txtName.Text;
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            tmp.Code = txtCode.Text;
        }

        private void txtFileNameFormat_TextChanged(object sender, EventArgs e)
        {
            tmp.FileNameFormat = txtFileNameFormat.Text;
        }

        public Template Template { get { return this.tmp; } }

        private void txtCategory_TextChanged(object sender, EventArgs e)
        {
            tmp.Category = txtCategory.Text;
        }
    }
}