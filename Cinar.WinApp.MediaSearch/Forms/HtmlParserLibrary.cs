using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Entities.MediaSearch;

namespace Cinar.WinApp.MediaSearch.Forms
{
    public class HtmlParserLibrary
    {
        WebBrowser webBrowser;
        Timer timer;

        public HtmlParserLibrary(WebBrowser browser)
        {
            if (browser == null)
            {
                 webBrowser = new WebBrowser();
                 webBrowser.AllowWebBrowserDrop = false;
                 //this.webBrowser.Dock = DockStyle.Fill;
                 //this.webBrowser.Location = new Point(0, 0);
                 //this.webBrowser.MinimumSize = new Size(20, 20);
                 //this.webBrowser.Name = "webBrowser";
                 webBrowser.ScriptErrorsSuppressed = true;
                 //this.webBrowser.Size = new Size(626, 213);
                 //this.webBrowser.TabIndex = 0;
            }
            else
            {
                webBrowser = browser;
            }

            timer = new Timer();
            timer.Interval = 1000;

            webBrowser.Navigated += new WebBrowserNavigatedEventHandler(webBrowser_Navigated);
            timer.Tick += new EventHandler(timer_Tick);
        }

        private ContentDefinition contentDef;

        public void Parse(ContentDefinition contentDef, string url)
        {
            this.contentDef = contentDef;
            webBrowser.Navigate(url);
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

                if (Parsed != null)
                    Parsed(content);
            }
        }

        public Action<Content> Parsed;

        private HtmlElement findElementBySelector(string selector)
        {
            HtmlElement elm = webBrowser.Document.Body;
            foreach (string node in selector.Split(new[] { " > " }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (elm == null)
                    break;

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
