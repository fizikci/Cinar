using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Cinar.Database;
using Cinar.DBTools.Controls;

namespace Cinar.DBTools.Tools
{
    public partial class FormGenerateTablesFromClasses : Form, IDBToolsForm
    {
        public FormGenerateTablesFromClasses()
        {
            InitializeComponent();
        }

        public FormMain MainForm
        {
            get;
            set;
        }

        private void btnOpenAssembly_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ".Net Assemblies|*.dll";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtAssembly.Text = ofd.FileName;

                Assembly asm = Assembly.LoadFrom(txtAssembly.Text);
                tree.Nodes.Clear();
                foreach (Type type in asm.GetTypes().Where(t=>t.IsClass && !t.IsAbstract && t.IsPublic && !t.IsSealed && !t.IsNested).OrderBy(t=>t.FullName))
                {
                    if (!string.IsNullOrEmpty(type.Namespace))
                    {
                        string[] nsList = type.Namespace.Split('.');
                        TreeNode tn = tree.Nodes[nsList[0]] ?? tree.Nodes.Add(nsList[0], nsList[0]);
                        for (int i = 1; i < nsList.Length; i++)
                            tn = tn.Nodes[tn.Name + "." + nsList[i]] ?? tn.Nodes.Add(tn.Name + "." + nsList[i], nsList[i]);
                        tn.Nodes.Add(type.FullName, type.Name).Tag = type;
                    }
                    else
                        tree.Nodes.Add(type.FullName, type.Name).Tag = type;
                }
            }
        }

        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Type type = (Type)e.Node.Tag;
            if (type == null)
                return;

            Table table = Provider.Database.CreateTableMetadataForType(type);
            editor.Text = Provider.Database.GetTableDDL(table);
            editor.Refresh();
        }

        private void tree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is Type)
            {
                if (e.Node.Checked)
                    selectedTypes.Add(e.Node.Tag as Type);
                else
                    selectedTypes.Remove(e.Node.Tag as Type);
            }

            foreach (TreeNode tn in e.Node.Nodes)
                tn.Checked = e.Node.Checked;
        }

        private List<Type> selectedTypes = new List<Type>();
        public List<Type> SelectedTypes {
            get { return selectedTypes; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Type type in selectedTypes)
                sb.AppendLine(Provider.Database.GetTableDDL(Provider.Database.CreateTableMetadataForType(type)));
            if (sb.Length > 0)
            {
                if (MainForm.CurrEditor is SQLEditorAndResults && string.IsNullOrEmpty(MainForm.CurrEditor.Content))
                    MainForm.CurrEditor.Content = sb.ToString();
                else
                    MainForm.addSQLEditor("", sb.ToString());
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
