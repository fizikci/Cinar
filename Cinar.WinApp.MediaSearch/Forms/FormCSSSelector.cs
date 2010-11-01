using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Cinar.WinApp.MediaSearch.Forms
{
    public partial class FormCSSSelector : DevExpress.XtraEditors.XtraForm
    {
        private FormCSSSelector(string rssUrl)
        {
            InitializeComponent();
            webBrowser.Navigating += new WebBrowserNavigatingEventHandler(webBrowser_Navigating);
            webBrowser.Navigated += new WebBrowserNavigatedEventHandler(webBrowser_Navigated);

            RSSReader.RSS rss = new Cinar.RSSReader.RSS(rssUrl);
            comboUrls.Properties.Items.AddRange(rss.Items.Select(i => i.Link).ToArray());
            comboUrls.SelectedIndexChanged += new EventHandler(comboUrls_SelectedIndexChanged);

            cbMark.Click += new EventHandler(cbMark_Click);
            editSelector.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(editSelector_ButtonClick);
        }

        void editSelector_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string selector = editSelector.EditValue.ToString();
            HtmlElement elm = findElementBySelector(selector);
            markElement(elm);
        }

        private HtmlElement findElementBySelector(string selector)
        {
            HtmlElement elm = webBrowser.Document.Body;
            foreach (string node in selector.Split(new[]{" > "}, StringSplitOptions.RemoveEmptyEntries))
            {
                if (node.StartsWith("#"))
                    elm = webBrowser.Document.GetElementById(node.Substring(1));
                else if (node.Contains("."))
                {
                    string[] parts = node.Split('.');
                    elm = elm.GetElementsByTagName(parts[0]).Cast<HtmlElement>().First(e => e.GetAttribute("classname") == parts[1]);
                }
                else
                {
                    string[] parts = node.Split('[');
                    int index = int.Parse(parts[1].Trim(']'));
                    elm = elm.GetElementsByTagName(parts[0])[index];
                }
            }
            return elm;
        }

        private void markElement(HtmlElement elm)
        {
            ElementPositionAndSelector eps = getElementPositionAndSelector(elm);

            HtmlElement marker = webBrowser.Document.GetElementById("cinarMarker");
            if (marker == null)
            {
                marker = webBrowser.Document.CreateElement("DIV");
                marker.Id = "cinarMarker";
                marker.Style = "position:absolute; top:10px; left:10px; width:100px; height:100px; border:1px solid red;";
                webBrowser.Document.Body.AppendChild(marker);
            }
            Rectangle rect = eps.Position;
            editSelector.Text = eps.Selector;

            marker.Style = string.Format("position:absolute; left:{0}px; top:{1}px; width:{2}px; height:{3}px; border:1px solid blue;", rect.Left, rect.Top, rect.Width, rect.Height);
        }

        void cbMark_Click(object sender, EventArgs e)
        {
            if (cbMark.Checked)
                webBrowser.Focus();
        }

        private string url;
        void comboUrls_SelectedIndexChanged(object sender, EventArgs e)
        {
            url = comboUrls.EditValue.ToString();
            webBrowser.Navigate(url);
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            e.Cancel = e.Url.ToString() != url;
        }

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

            HtmlElement elm = e.ToElement;
            if (elm.Id == "cinarMarker")
                return;

            markElement(elm);
        }

        private ElementPositionAndSelector getElementPositionAndSelector(HtmlElement elm)
        {
            Size size = elm.ClientRectangle.Size;
            if (size.Width == 0)
                size = elm.ScrollRectangle.Size;
            Point pos = elm.ClientRectangle.Location;

            string sFull = "";
            while (elm != null)
            {
                if (elm.TagName == "HTML")
                    break;

                string cls = elm.GetAttribute("classname");
                string id = elm.GetAttribute("id");

                if (!string.IsNullOrEmpty(id))
                    sFull = "#" + id + " > " + sFull;
                else if (!string.IsNullOrEmpty(cls))
                    sFull = elm.TagName + "." + cls + " > " + sFull;
                else if(elm.OffsetParent!=null)
                {
                    int index = elm.OffsetParent.GetElementsByTagName(elm.TagName).OfType<HtmlElement>().IndexOf(e => e == elm);
                    sFull = elm.TagName + "[" + index + "] > " + sFull;
                }

                pos.X += elm.OffsetRectangle.Left;
                pos.Y += elm.OffsetRectangle.Top;

                elm = elm.OffsetParent;
            }
            return new ElementPositionAndSelector { 
                  Position = new Rectangle(pos, size),
                  Selector = sFull.Trim(' ','>')
              };
        }

        public static string GetSelector(string formTitle, string rssUrl, string selector)
        {
            FormCSSSelector f = new FormCSSSelector(rssUrl);
            f.Text = formTitle;
            f.editSelector.Text = selector;
            if (f.ShowDialog() == DialogResult.OK)
                return f.editSelector.Text;
            else
                return null;
        }


    }
    public class ElementPositionAndSelector
    {
        public string Selector { get; set; }
        public Rectangle Position { get; set; }
    }
}