using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternetTracker.SiteMap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            List<TreeNode> nodes = new List<TreeNode>();

            foreach (string file in Directory.GetFiles(@"C:\Users\Administrator\Desktop\OUTPUT\FoundLinks\20140627"))
            {
                TreeNode tn = new TreeNode("http://" + Path.GetFileNameWithoutExtension(file));
                tn.Tag = tn.Text;
                string text = File.ReadAllText(file);
                addNodesTo(tn, text);
                nodes.Add(tn);
            }

            treeView1.Nodes.AddRange(nodes.ToArray());
        }

        private void addNodesTo(TreeNode tn, string text)
        {
            var list = text.Split('\n','?').Where(u=>u.StartsWith(tn.Tag.ToString())).Select(u => u.Replace(tn.Tag.ToString(), "").Trim('/').Split('/').FirstOrDefault()).Distinct().ToList();

            foreach (var part in list) 
            {
                if (string.IsNullOrWhiteSpace(part))
                    continue;
                var n = new TreeNode(part);
                n.Tag = tn.Tag + "/" + part;
                tn.Nodes.Add(n);
                addNodesTo(n, text);
            }
        }
    }
}
