using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cinar.DBTools.Tools
{
    public partial class FormSearchProjectFolder : Form
    {
        public FormSearchProjectFolder()
        {
            InitializeComponent();
        }

        private void tbProjectFolder_Click(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = tb.Text;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tb.Text = fbd.SelectedPath;
                lbFileTypes.Items.Clear();
                lbFileTypes.Items.AddRange(
                    Directory.EnumerateFiles(fbd.SelectedPath, "*.*", SearchOption.AllDirectories)
                             .Select(path => (Path.GetExtension(path) ?? "").ToUpperInvariant())
                             .Distinct()
                             .OrderBy(ext => ext)
                             .ToArray<object>()
                    );
            }
        }

        public string ProjectFolder
        {
            get { return tbProjectFolder.Text; }
        }

        public List<string> SearchExtensions
        {
            get { return lbFileTypes.CheckedItems.Cast<string>().ToList(); }
        }

        public string WhatToSearch
        {
            get
            {
                return cbWhatToSearch.SelectedItem.ToString();
            }
        }
    }
}
