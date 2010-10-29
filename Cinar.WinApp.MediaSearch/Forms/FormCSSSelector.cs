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
        private FormCSSSelector(string rssUrl)
        {
            InitializeComponent();

            RSSReader.RSS rss = new Cinar.RSSReader.RSS(rssUrl);
            if(rss.Items.Count>0)
                webBrowser.Navigate(rss.Items[0].Link); 
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