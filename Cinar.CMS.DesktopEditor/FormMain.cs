using Cinar.CMS.DesktopEditor.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
