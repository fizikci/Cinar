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
    public partial class FormDBIntegrationTask : Form
    {
        private DBIntegrationTask task;
        public DBIntegrationTask Task { get { return task; } }

        public FormDBIntegrationTask(DBIntegrationTask task)
        {
            InitializeComponent();

            this.task = (DBIntegrationTask)task.Clone();

            BindDatabases();

            showTask();
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

        private void showTask()
        {
            txtCategory.Text = task.Category;
            numInterval.Value = task.ExecInterval;
            txtName.Text = task.Name;
            cbSrcDb.SelectedIndex = cbSrcDb.Items.OfType<ConnectionSettings>().IndexOf(cs=>cs.ToString()==task.SourceDB);
            cbDstDb.SelectedIndex = cbDstDb.Items.OfType<ConnectionSettings>().IndexOf(cs => cs.ToString() == task.DestDB);
            txtCode.Text = task.Code;
        }
        private void updateTask()
        {
            task.Category = txtCategory.Text;
            task.ExecInterval = (int)numInterval.Value;
            task.Name = txtName.Text;
            task.SourceDB = cbSrcDb.SelectedItem.ToString();
            task.DestDB = cbDstDb.SelectedItem.ToString();
            task.Code = txtCode.Text;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            updateTask();
        }
    }
}
