using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternetTracker.DocumentView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string getNodeInnerText(string url, string xpath)
        {
            using (WebClient wc = new WebClient())
            {
                //wc.Headers["Connection"] = "keep-alive";
                wc.Headers["Cache-Control"] = "max-age=0";
                wc.Headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                wc.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36";
                //wc.Headers["Accept-Encoding"] = "gzip,deflate,sdch";
                wc.Headers["Accept-Language"] = "tr,en;q=0.8,en-US;q=0.6";

                string textOrj = wc.DownloadString(url);

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(textOrj);

                return doc.DocumentNode.SelectSingleNode(xpath).InnerHtml;
            }
        }

        private HtmlNode findContentNode(HtmlNode htmlNode)
        {
            for (int i = 0; i < 5; i++)
            {
                var enbuyuk = htmlNode.FirstChild;
                foreach (HtmlNode sub in htmlNode.ChildNodes)
                    if (sub.InnerText.Length > enbuyuk.InnerText.Length)
                        enbuyuk = sub;
                htmlNode = enbuyuk;
            }
            while (htmlNode.ChildNodes.Count == 1) htmlNode = htmlNode.FirstChild;
            return htmlNode;
        }

        private void clearNodes(HtmlAgilityPack.HtmlDocument doc, string p)
        {
            var bodyNode = doc.DocumentNode.SelectNodes(p);
            if(bodyNode!=null)
            foreach (HtmlNode node in bodyNode)
            {
                node.ParentNode.RemoveChild(node);
            }
        }

        private void addNodesToTree(TreeNode nodeRoot)
        {
            HtmlNode parentNode = nodeRoot.Tag as HtmlNode;
            foreach (HtmlNode node in parentNode.ChildNodes)
            {
                if (string.IsNullOrWhiteSpace(node.InnerText))
                    continue;

                if (node.Name == "#comment")
                    continue;

                TreeNode nodeElm = nodeRoot.Nodes.Add(node.Name + " ("+node.InnerText.Length+")");
                nodeElm.Tag = node;

                addNodesToTree(nodeElm);
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propertyGrid.SelectedObject = e.Node.Tag;
            txtToString.Text = (e.Node.Tag as HtmlNode).InnerHtml;
        }

        private void txtUrl_Click(object sender, EventArgs e)
        {
            txtUrl.SelectAll();
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            using (WebClient wc = new WebClient())
            {
                //wc.Headers["Connection"] = "keep-alive";
                wc.Headers["Cache-Control"] = "max-age=0";
                wc.Headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                wc.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36";
                //wc.Headers["Accept-Encoding"] = "gzip,deflate,sdch";
                wc.Headers["Accept-Language"] = "tr,en;q=0.8,en-US;q=0.6";

                string textOrj = wc.DownloadString(txtUrl.Text);
                string text = "";
                foreach (char c in textOrj)
                {
                    if (c != ' ' && Char.IsWhiteSpace(c))
                        continue;
                    text += c;
                }
                while (text.Contains("  ")) text = text.Replace("  ", " ");
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(text);

                clearNodes(doc, "//script");
                clearNodes(doc, "//style");
                clearNodes(doc, "//a");

                TreeNode nodeRoot = treeView.Nodes.Add(doc.DocumentNode.Name);
                nodeRoot.Tag = doc.DocumentNode;

                addNodesToTree(nodeRoot);
                HtmlNode contentNode = findContentNode(doc.DocumentNode.SelectSingleNode("//body"));
                txtXPath.Text = contentNode.XPath;

                txtToString.Text = getNodeInnerText(txtUrl.Text, contentNode.XPath);
            }
        }
    }
}
