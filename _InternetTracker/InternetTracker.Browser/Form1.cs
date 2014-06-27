using Skybound.Gecko;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternetTracker.Browser
{
    public partial class Form1 : Form
    {
        GeckoWebBrowser browser;

        public Form1()
        {
            InitializeComponent();

            Xpcom.Initialize(@"C:\xulrunner");

            browser = new GeckoWebBrowser();
            browser.Top = 50;
            browser.Left = 20;
            browser.Width = 600;
            browser.Height = 600;
            this.Controls.Add(browser);

            browser.DocumentCompleted += browser_DocumentCompleted;
        }

        void browser_DocumentCompleted(object sender, EventArgs e)
        {
            //browser.Navigate(@"javascript: var jq = document.createElement('script');jq.src = ""http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"";document.getElementsByTagName('head')[0].appendChild(jq);jQuery.noConflict();");

            foreach (GeckoElement elm in browser.Document.GetElementsByTagName("img"))
            {
                //XPCOM elm.DomObject
            }
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            browser.Navigate(txtUrl.Text);
        }
    }
}
