using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Menees.DiffUtils;
using Menees.DiffUtils.Controls;

namespace Cinar.DBTools.Tools
{
    public partial class FormGeneratedCode : Form
    {
        List<GeneratedCode> generatedCodes = null;

        public FormGeneratedCode(List<GeneratedCode> generatedCodes)
        {
            InitializeComponent();

            this.generatedCodes = generatedCodes;

            tabControl1.TabPages.Clear();
            foreach (GeneratedCode gc in generatedCodes)
            {
                TabPage tp = new TabPage(Path.GetFileName(gc.Path));
                tabControl1.TabPages.Add(tp);
                tp.Tag = gc;

                RichTextBox tb = new RichTextBox();
                tb.AcceptsTab = true;
                tb.DetectUrls = false;
                tb.Dock = DockStyle.Fill;
                tb.Font = new Font("Lucida Console", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 162);
                tb.Location = new System.Drawing.Point(3, 3);
                tb.Text = gc.Code;
                tb.WordWrap = false;

                tb.TextChanged += delegate {
                    (tp.Tag as GeneratedCode).Code = tb.Text;
                };

                tp.Controls.Add(tb);
            }

            tabControl1.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GeneratedCode gc = (GeneratedCode)tabControl1.SelectedTab.Tag;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = gc.Path;
            if (sfd.ShowDialog() == DialogResult.OK)
                File.WriteAllText(sfd.FileName, gc.Code, Encoding.UTF8);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDiff_Click(object sender, EventArgs e)
        {
            GeneratedCode gc = (GeneratedCode)tabControl1.SelectedTab.Tag;

            Form f = new Form();
            f.WindowState = FormWindowState.Maximized;
            f.Text = "[Existing File] vs. [Generated Code]";
            ComponentResourceManager resources = new ComponentResourceManager(this.GetType());
            f.Icon = (Icon)(resources.GetObject("$this.Icon"));

            DiffControl diffControl = new DiffControl();
            diffControl.Dock = DockStyle.Fill;
            f.Controls.Add(diffControl);

            bool chkIgnoreCase = false;
            bool chkIgnoreWhitespace = true;
            bool chkSupportChangeEditType = false;
            bool chkXML = false;

            string strA = gc.Path;

            try
            {
                //edtTextOne.Text = strA;
                //edtTextTwo.Text = strB;

                TextDiff diff = new TextDiff(HashType.CRC32, chkIgnoreCase, chkIgnoreWhitespace, 0, chkSupportChangeEditType);

                IList<string> code1, code2;
                if (chkXML)
                {
                    code1 = Functions.GetXMLTextLines(strA, WhitespaceHandling.All);
                    code2 = gc.Code.Replace("\r","").Split('\n');//Functions.GetXMLTextLines(strB, eWS);
                }
                else
                {
                    code1 = Functions.GetFileTextLines(strA);
                    code2 = gc.Code.Replace("\r","").Split('\n');//Functions.GetFileTextLines(strB);
                }

                EditScript script = diff.Execute(code1, code2);

                diffControl.SetData(code1, code2, script);//, strA, strB);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            f.Show();
        }
    }
}