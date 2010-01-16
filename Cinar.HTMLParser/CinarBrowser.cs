using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using org.w3c.dom.html2;

namespace Cinar.HTMLParser
{
    public partial class CinarBrowser : UserControl
    {
        public CinarBrowser()
        {
            InitializeComponent();
        }

        private org.w3c.dom.html2.HTMLDocument doc;
        public org.w3c.dom.html2.HTMLDocument document
        {
            get {
                return doc;
            }
        }

        private string htmlCode;
        public string HTMLCode 
        {
            get {
                return htmlCode;
            }
            set {
                htmlCode = value;
                if (htmlCode == null)
                    return;

                HtmlAgilityPack.HtmlDocument parser = new HtmlAgilityPack.HtmlDocument();
                parser.Load(new StringReader(htmlCode));
                doc = new org.w3c.dom.html2.HTMLDocument(parser, this.Width, this.Height);
            }
        }
    }
}
