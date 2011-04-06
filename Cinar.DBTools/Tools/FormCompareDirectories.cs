using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Menees.DiffUtils.Controls;
using Menees.DiffUtils;
using Cinar.Database;
using System.IO;

namespace Cinar.DBTools.Tools
{
    public partial class FormCompareDirectories : Form, IDBToolsForm
    {
        public FormMain MainForm { get; set; }

        public FormCompareDirectories()
        {
            InitializeComponent();
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            DiffControl diffControl = new DiffControl();
            diffControl.Dock = DockStyle.Fill;
            panel.Controls.Clear();
            panel.Controls.Add(diffControl);

            bool chkIgnoreCase = false;
            bool chkIgnoreWhitespace = true;
            bool chkSupportChangeEditType = false;

            List<string> code1, code2;

            code1 = GetContent(tbSrcDir.Text);
            for (int i = 0; i < code1.Count; i++) code1[i] = code1[i].Substring(tbSrcDir.Text.Length);
            code2 = GetContent(tbDstDir.Text);
            for (int i = 0; i < code2.Count; i++) code2[i] = code2[i].Substring(tbDstDir.Text.Length);

            try
            {
                TextDiff diff = new TextDiff(HashType.CRC32, chkIgnoreCase, chkIgnoreWhitespace, 0, chkSupportChangeEditType);

                EditScript script = diff.Execute(code1, code2);

                diffControl.SetData(code1, code2, script);//, strA, strB);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private List<string> GetContent(string path)
        {
            List<string> list = new List<string>();
            if (!Directory.Exists(path))
                return list; //***

            foreach (string dir in Directory.GetDirectories(path))
            {
                list.Add(dir);
                list.AddRange(GetContent(dir));
            }
            foreach (string fileName in Directory.GetFiles(path))
            {
                string name = "";
                FileInfo fi = new FileInfo(fileName);

                if (cbName.Checked)
                    name += fileName + " ";

                if (cbLength.Checked)
                    name += fi.Length + " ";
                if (cbDate.Checked)
                    name += fi.LastWriteTime.ToString("dd.MM.yyyy hh:mm:ss");

                list.Add(name);
            }

            return list;
        }

        private void tbSrcDir_Click(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = tb.Text;
            if (fbd.ShowDialog() == DialogResult.OK)
                tb.Text = fbd.SelectedPath;
        }
    }
}
