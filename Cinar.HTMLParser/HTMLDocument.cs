using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Cinar.HTMLParser
{
    public partial class HTMLDocument : UserControl
    {
        public HTMLDocument()
        {
            InitializeComponent();
        }

        private Parser parser;

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
                parser = new Parser(new StringReader(htmlCode));
                parser.Parse();
                parser.RootNode.Layout.Width = this.Width;
                parser.RootNode.Draw(this.CreateGraphics());
            }
        }
    }
}
