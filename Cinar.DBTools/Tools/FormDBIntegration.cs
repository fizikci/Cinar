﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Cinar.Database;
using Cinar.Scheduler;
using Cinar.Scripting;
using Cinar.DBTools.Controls;

namespace Cinar.DBTools.Tools
{
    public partial class FormDBIntegration : Form
    {
        DBIntegration integData;
        string path;
        SchedulerEngine scheduler;

        public FormDBIntegration()
        {
            InitializeComponent();


            integData = new DBIntegration();

            scheduler = new SchedulerEngine();
            scheduler.SetForm(this);

            path = Path.GetDirectoryName(Application.ExecutablePath) + "\\dbintegration.xml";
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(DBIntegration));
                    integData = (DBIntegration)ser.Deserialize(sr);
                }
            }

            showCategories(null);
        }

        private void showCategories(string selectedCat)
        {
            cbCategories.Items.Clear();

            foreach (string cat in integData.Tasks.Select(c => c.Category).Distinct().OrderBy(s => s))
                cbCategories.Items.Add(cat ?? "");

            if (!string.IsNullOrEmpty(selectedCat))
                cbCategories.SelectedIndex = cbCategories.Items.IndexOf(selectedCat);

            showList();
        }

        private void cbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            showList();
        }

        private void showList()
        {
            int oldIndex = lbTasks.SelectedIndex;

            string selectedCat = (cbCategories.SelectedItem == null || cbCategories.SelectedItem.ToString() == "") ? null : cbCategories.SelectedItem.ToString();
            lbTasks.Items.Clear();
            foreach (DBIntegrationTask task in integData.Tasks.Where(t => t.Category == selectedCat))
                lbTasks.Items.Add(task);

            if (lbTasks.Items.Count > oldIndex)
                lbTasks.SelectedIndex = oldIndex;
            else
                lbTasks.SelectedIndex = lbTasks.Items.Count - 1;
        }

        private void btnDeleteSelectedTask_Click(object sender, EventArgs e)
        {
            DBIntegrationTask task = (DBIntegrationTask)lbTasks.SelectedItem;
            if (MessageBox.Show("Are you sure to delete " + task, "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                integData.Tasks.Remove(task);
                save();
                showCategories(task.Category);
            }
        }

        private void save()
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                XmlSerializer ser = new XmlSerializer(typeof(DBIntegration));
                ser.Serialize(sw, integData);
            }
        }

        private void btnAddNewTask_Click(object sender, EventArgs e)
        {
            DBIntegrationTask tmp = new DBIntegrationTask();
            tmp.Name = "New Task";
            tmp.Code = "Code...";

            FormDBIntegrationTask form = new FormDBIntegrationTask(tmp);
            if (form.ShowDialog() == DialogResult.OK)
            {
                integData.Tasks.Add(form.Task);
                showCategories(form.Task.Category);
                save();
            }
        }

        private void btnEditSelectedTask_Click(object sender, EventArgs e)
        {
            DBIntegrationTask tmp = (DBIntegrationTask)lbTasks.SelectedItem;
            FormDBIntegrationTask form = new FormDBIntegrationTask(tmp);
            if (form.ShowDialog() == DialogResult.OK)
            {
                integData.Tasks[integData.Tasks.IndexOf(tmp)] = form.Task;
                showCategories(form.Task.Category);
                save();
            }
        }

        private void btnStart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            scheduler.Interval = 100;

            for (int i = 0; i < lbTasks.Items.Count; i++)
            {
                DBIntegrationTask task = lbTasks.Items[i] as DBIntegrationTask;

                Job job = new Job
                  {
                      Interval = task.ExecInterval * 1000,
                      Type = JobType.PeriodicMS,
                      Handler = schedulerEngine => executeTask(task)
                  };
                scheduler.Jobs.Add(job);
            }

            scheduler.Start();
            
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void executeTask(DBIntegrationTask task)
        {
            ConnectionSettings csSrc = Provider.GetConnection(task.SourceDB);
            ConnectionSettings csDst = Provider.GetConnection(task.DestDB);
            if(csSrc==null)
            {
                Log(task, task.SourceDB + " doesnt exist!");
                return;
            }
            if(csDst==null)
            {
                Log(task, task.DestDB + " doesnt exist!");
                return;
            }

            string code = integData.ScriptIncludeCode + Environment.NewLine + task.Code;

            Interpreter pret = new Interpreter(code, null);
            pret.SetAttribute("dbSrc", csSrc.Database);
            pret.SetAttribute("dbDst", csDst.Database);
            pret.SetAttribute("this", task);
            pret.SetAttribute("form", this);
            pret.Parse();
            pret.Execute();

            if (!pret.LastExecutionSuccessful)
                Log(task, "Error: " + pret.Output);
            else
                Log(task, "Executed successfully in " + pret.ExecutingTime + " ms.");
        }

        public void Log(DBIntegrationTask task, string message)
        {
            lbLog.Items.Insert(0, string.Format("{0} {1} : {2}", DateTime.Now.ToString("HH:mm:ss"), task.Name, message));

            if(lbLog.Items.Count>200)
            {
                string logPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\dbintegration.log";
                using (StreamWriter sw = new StreamWriter(logPath, true, Encoding.UTF8))
                {
                    while (lbLog.Items.Count > 100)
                    {
                        sw.WriteLine(lbLog.Items[lbLog.Items.Count - 1].ToString());
                        lbLog.Items.RemoveAt(lbLog.Items.Count - 1);
                    }
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            scheduler.Stop();
            scheduler.Clear();

            string logPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\dbintegration.log";
            using (StreamWriter sw = new StreamWriter(logPath, true, Encoding.UTF8))
            {
                while (lbLog.Items.Count > 0)
                {
                    sw.WriteLine(lbLog.Items[lbLog.Items.Count - 1].ToString());
                    lbLog.Items.RemoveAt(lbLog.Items.Count - 1);
                }
            }
        }

        private void btnStop_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            scheduler.Stop();
            scheduler.Clear();

            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void lbTasks_DoubleClick(object sender, EventArgs e)
        {
            btnEditSelectedTask_Click(sender, e);
        }

        private void btnScriptInclude_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TextInputDialog tid = new TextInputDialog();
            tid.TextInput = integData.ScriptIncludeCode;

            if (tid.ShowDialog() == DialogResult.OK)
            {
                integData.ScriptIncludeCode = tid.TextInput;
                save();
            }
        }

    }
}
