using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Cinar.WinApp.MediaSearch.Forms
{
    public partial class FormCSSSelector : DevExpress.XtraEditors.XtraForm
    {
        private string url;
        private FormCSSSelector(string rssUrl)
        {
            InitializeComponent();
            webBrowser.Navigating += new WebBrowserNavigatingEventHandler(webBrowser_Navigating);
            webBrowser.Navigated += new WebBrowserNavigatedEventHandler(webBrowser_Navigated);

            RSSReader.RSS rss = new Cinar.RSSReader.RSS(rssUrl);
            if (rss.Items.Count > 0)
            {
                url = rss.Items[0].Link;
                webBrowser.Navigate(url);
            }
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            e.Cancel = e.Url.ToString() != url;
        }
        HtmlElement marker;
        void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.ToString() == url)
            {
                webBrowser.Document.MouseOver += new HtmlElementEventHandler(Document_MouseOver);
                webBrowser.Document.Click += new HtmlElementEventHandler(Document_Click);
            }
        }

        void Document_Click(object sender, HtmlElementEventArgs e)
        {
            cbMark.Checked = false;
        }

        void Document_MouseOver(object sender, HtmlElementEventArgs e)
        {
            if (!cbMark.Checked)
                return;

            if (marker == null)
            {
                marker = webBrowser.Document.CreateElement("DIV");
                marker.Style = "position:absolute; top:10px; left:10px; width:100px; height:100px; border:1px solid red;";
                webBrowser.Document.Body.AppendChild(marker);
            }
            
            HtmlElement elm = e.ToElement;
            Size size = elm.ClientRectangle.Size;
            Point pos = elm.ClientRectangle.Location;

            string s = "";
            while (elm != null)
            {
                string cls = elm.GetAttribute("classname");
                if(!string.IsNullOrEmpty(cls))
                    s = elm.TagName + "." + cls + " > " + s;

                pos.X += elm.OffsetRectangle.Left;
                pos.Y += elm.OffsetRectangle.Top;

                elm = elm.OffsetParent;
            }
            label.Text = s;

            marker.Style = string.Format("position:absolute; left:{0}px; top:{1}px; width:{2}px; height:{3}px; border:1px solid blue;",pos.X, pos.Y, size.Width, size.Height);
        }

        public static string GetSelector(string formTitle, string rssUrl)
        {
            FormCSSSelector f = new FormCSSSelector(rssUrl);
            f.Text = formTitle;
            if (f.ShowDialog() == DialogResult.OK)
                return f.label.Text;
            else
                return null;
        }


    }
}