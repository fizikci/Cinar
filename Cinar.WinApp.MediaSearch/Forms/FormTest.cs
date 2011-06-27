using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Cinar.Entities.MediaSearch;
using Cinar.RSSReader;
using System.Net;

namespace Cinar.WinApp.MediaSearch.Forms
{
    public partial class FormTest : DevExpress.XtraEditors.XtraForm
    {
        List<Content> list = new List<Content>();
        RSS rss;
        int index = 0;
        ContentDefinition contentDef;

        public FormTest(ContentDefinition contentDef)
        {
            this.contentDef = contentDef;
            InitializeComponent();
            webBrowser.Navigated += new WebBrowserNavigatedEventHandler(webBrowser_Navigated);
            timer.Tick += new EventHandler(timer_Tick);

            rss = new RSS(contentDef.RSSUrl);
            if (rss.Items.Count > 0)
                navigate();
        }

        private void navigate()
        {
            if (index < rss.Items.Count)
                webBrowser.Navigate(rss.Items[index].Link);
            //else
            grid.DataSource = null;
                grid.DataSource = list;
        }

        void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            timer.Enabled = true;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (!webBrowser.IsBusy)
            {
                timer.Enabled = false;

                Content content = new Content();

                content.SourceUrl = webBrowser.Url.ToString();

                HtmlElement elm = findElementBySelector(contentDef.TitleSelector);
                content.Title = elm != null ? elm.InnerText : "";

                try
                {
                    elm = findElementBySelector(contentDef.ImageSelector);
                    content.ImageUrl = elm != null ? elm.GetAttribute("src") : "";
                }
                catch { }

                try
                {
                    elm = findElementBySelector(contentDef.DateSelector);
                    content.PublishDate = elm != null ? DateTime.Parse(elm.InnerText) : new DateTime();
                }
                catch { }

                try
                {
                    elm = findElementBySelector(contentDef.ContentSelector);
                    content.Text = elm != null ? elm.InnerText : "";
                }
                catch { }

                try
                {
                    elm = findElementBySelector(contentDef.AuthorSelector);
                    content.Author = elm != null ? elm.InnerText : "";
                }
                catch { }

                list.Add(content);

                index++;
                navigate();
            }
        }


        private HtmlElement findElementBySelector(string selector)
        {
            HtmlElement elm = webBrowser.Document.Body;
            foreach (string node in selector.Split(new[] { " > " }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (node.StartsWith("#"))
                    elm = webBrowser.Document.GetElementById(node.Substring(1));
                else if (node.Contains("."))
                {
                    string[] parts = node.Split('.');
                    if (parts[1].Contains(" ")) parts[1] = parts[1].Split(' ')[0];
                    elm = elm.GetElementsByTagName(parts[0]).Cast<HtmlElement>().First(e => e.GetAttribute("classname").Contains(parts[1]));
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

    }

    public class Content
    {
        public string SourceUrl { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public string Text { get; set; }
    }
}