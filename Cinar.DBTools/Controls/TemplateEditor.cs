using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.UICommands;
using Cinar.DBTools.Tools;
using Cinar.Database;
using System.IO;

namespace Cinar.DBTools.Controls
{
    public partial class TemplateEditor : UserControl, IEditor
    {
        public string filePath;
        public string FilePath { get { return filePath; } }
        public bool Modified { get { return txt.Text != InitialText; } }
        public string InitialText;

        public TemplateEditor(string filePath)
        {
            InitializeComponent();

            this.filePath = filePath;
            if (!string.IsNullOrEmpty(filePath))
            {
                txt.LoadFile(filePath);
                InitialText = txt.Text;
            }
        }

        public bool Save()
        {
            if (string.IsNullOrEmpty(filePath))
                return SaveAs();

            InitialText = txt.Text;
            txt.SaveFile(filePath);
            return true;
        }

        public bool SaveAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All Files|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filePath = sfd.FileName;
                InitialText = txt.Text;
                txt.SaveFile(filePath);
                return true;
            }
            return false;
        }

        public string GetName()
        {
            return Path.GetFileName(this.FilePath);
        }


        public void OnClose()
        {
        }


        public string Content
        {
            get { return txt.Text; }
            set { txt.Text = value; }
        }
    }
}
