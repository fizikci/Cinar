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
        HtmlParserLibrary lib;

        public FormTest(ContentDefinition contentDef)
        {
            this.contentDef = contentDef;
            InitializeComponent();
            lib = new HtmlParserLibrary(webBrowser);

            rss = new RSS(contentDef.RSSUrl);
            if (rss.Items.Count > 0)
                navigate();
            lib.Parsed = (content) => {
                list.Add(content);

                index++;
                navigate();
            };
        }

        private void navigate()
        {
            if (index < rss.Items.Count)
                lib.Parse(contentDef, rss.Items[index].Link);
            //else
            grid.DataSource = null;
            grid.DataSource = list;
        }
    }

}