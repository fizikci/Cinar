using Cinar.CMS.DesktopEditor.Controls;
using Krystalware.UploadHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Cinar.CMS.DesktopEditor
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            var s = Settings.Load();
            foreach (int index in s.Providers.Keys)
            {
                var menu = new ToolStripMenuItem(s.SiteAddress[index]);
                menu.Tag = index;
                menu.Click += menu_Click;
                menuOpenSite.DropDownItems.Add(menu);
            }
        }

        void menu_Click(object sender, EventArgs e)
        {
            showSiteForm((int)(sender as ToolStripMenuItem).Tag);
        }

        private void menuYeniSite_Click(object sender, EventArgs e)
        {
            var s = Settings.Load();
            var index = s.SiteAddress.Count;

            showNewSiteForm(s, index);
        }

        private void showNewSiteForm(Settings s, int index)
        {
            FormSettings f = new FormSettings(index);
            if (f.ShowDialog() == DialogResult.OK)
            {
                s.SiteAddress[index] = f.SiteAddress;
                s.Providers[index] = f.ConnectionProvider;
                s.ConnectionStrings[index] = f.ConnectingString;
                s.Emails[index] = f.Email;
                s.Passwords[index] = f.Password;
                s.Save();

                showSiteForm(index);
            }
        }

        private void showSiteForm(int index)
        {
            var s = Settings.Load();

            Form form = new Form();
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            form.MinimumSize = new System.Drawing.Size(427, 310);
            form.Size = new System.Drawing.Size(630, 450);
            form.Text = s.SiteAddress[index];
            var vc = new ViewContent();
            
            vc.Index = index;
            vc.Dock = DockStyle.Fill;
            form.Controls.Add(vc);
            form.MdiParent = this;

            form.Show();
        }

        private void menuCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }

        private void menuTileHoriz_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
        }

        private void menuTileVert_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
        }

        private void menuKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = 10 * 60 * 1000;
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var s = Settings.Load();
            foreach (var item in s.Feed) 
            {
                if (string.IsNullOrWhiteSpace(item.Value))
                    continue;

                var siteIndex = item.Key;
                var site = s.SiteAddress[siteIndex];

                foreach (string rssItem in item.Value.Replace("\r", "").SplitWithTrim('\n'))
                {
                    try
                    {
                        if (!rssItem.Contains("|")) continue;
                        var rssUrl = rssItem.SplitWithTrim("|")[0];
                        var categoryId = int.Parse(rssItem.SplitWithTrim("|")[1]);

                        XElement rss = XElement.Parse(rssUrl.DownloadPage());
                        XNamespace mediaNS = "http://search.yahoo.com/mrss/";

                        var haberler = rss.Element("channel").Elements("item").ToList();
                        if (haberler.Count == 0)
                            continue;

                        backgroundWorker.ReportProgress(0, rss.Element("channel").Element("title").Value + "|" + haberler.Count);
                        for (int i = 0; i < haberler.Count; i++)
                        {
                            var media = haberler[i].Element(mediaNS + "content");
                            var picture = media==null ? "" : media.Attribute("url").Value;
                            var title = haberler[i].Element("title").Value;
                            var sourceLink = haberler[i].Element("link").Value;

                            if (Provider.GetDb(siteIndex).GetInt("select Id from Content where SourceLink={0}", sourceLink) > 0)
                            {
                                backgroundWorker.ReportProgress(((i + 1) * 100) / haberler.Count);
                                continue;
                            }

                            var metin = haberler[i].Element("description").Value;
                            if (metin.Contains(")- "))
                                metin = metin.Substring(metin.IndexOf(")- ") + 3);
                            var pubDate = DateTime.Parse(haberler[i].Element("pubDate").Value);

                            NameValueCollection postData = new NameValueCollection();
                            postData.Add("Id", "0");
                            postData.Add("ClassName", "Content");
                            postData.Add("Title", title);
                            postData.Add("SourceLink", sourceLink);
                            postData.Add("CategoryId", categoryId.ToString());
                            try
                            {
                                postData.Add("Description", metin.SplitWithTrim(".").First());
                            }
                            catch { }
                            postData.Add("Picture", picture);
                            postData.Add("PublishDate", pubDate.ToString("dd.MM.yyyy hh:mm"));
                            postData.Add("Metin", metin);

                            string res = HttpUploadHelper.Upload(site.Trim('/') + "/UploadContent.ashx", new UploadFile[0], postData);

                            backgroundWorker.ReportProgress(((i+1) * 100) / haberler.Count);
                        }
                    }
                    catch { }
                }
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                var data = e.UserState.ToString().SplitWithTrim("|");
                statusLabel.Text = data[0] + " kaynağından " + data[1] + " haber yükleniyor ";
            }
            statusProgress.Value = e.ProgressPercentage;
        }
    }
}
