using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cinar.CMS.DesktopEditor
{
    public partial class FormSelectContent : Form
    {
        private int index;

        public FormSelectContent(int index)
        {
            InitializeComponent();
            this.index = index;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            bindGrid();
        }

        private void bindGrid()
        {
            var db = Provider.GetDb(index);
            if (db == null)
            {
                MessageBox.Show((index + 1) + ". kutu için veritabanı ayarları geçersiz.", "Çınar CMS Desktop Editor");
                return;
            }

            grid.DataSource = db.GetDataTable("select top 100 c.Id, cat.Title as Kategori, c.Title as [Başlık] from Content c, Content cat where cat.Id=c.CategoryId order by c.Id desc");

            grid.Columns["Başlık"].Width = 400;
        }

        private void grid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public int SelectedContentId
        {
            get
            {
                if (grid.CurrentRow == null)
                    return 0;

                DataRow dr = ((DataRowView)grid.CurrentRow.DataBoundItem).Row;
                return (int)dr["Id"];

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string ids = "";
            foreach (DataGridViewRow row in grid.SelectedRows)
                ids += "," + ((DataRowView)row.DataBoundItem).Row["Id"];

            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                var s = Settings.Load();
                string res = wc.DownloadString(s.SiteAddress[index].Trim('/') + string.Format("/DoCommand.ashx?method=deleteContents&Email={0}&Passwd={1}&ids={2}", s.Emails[index], s.Passwords[index], ids.Trim(',')));
                MessageBox.Show(res, "Cinar CMS Desktop Editor");
                bindGrid();
            }
        }
    }
}
