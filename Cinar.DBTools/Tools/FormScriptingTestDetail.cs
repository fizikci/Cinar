using System;
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
    public partial class FormScriptingTestDetail : Form
    {
        private ScriptingTest test;
        public ScriptingTest Test { get { return test; } }

        public FormScriptingTestDetail(ScriptingTest test)
        {
            InitializeComponent();

            this.test = (ScriptingTest)test.Clone();

            showTask();
        }

        private void showTask()
        {
            txtCategory.Text = test.Category;
            txtName.Text = test.Name;
            txtCode.Text = test.Code;
        }
        private void updateTask()
        {
            test.Category = txtCategory.Text;
            test.Name = txtName.Text;
            test.Code = txtCode.Text;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            updateTask();
        }
    }
}
