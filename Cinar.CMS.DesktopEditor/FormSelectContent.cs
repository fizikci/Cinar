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

            var db = Provider.GetDb(index);
            if (db == null)
            {
                MessageBox.Show((index + 1) + ". kutu için veritabanı ayarları geçersiz.", "Çınar CMS Desktop Editor");
                return;
            }

            grid.DataSource = db.GetDataTable("select top 100 c.Id, cat.Title as Kategori, c.Title as [Başlık] from Content c, Content cat where cat.Id=c.CategoryId order by c.Id desc");
        }

        private void grid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public int SelectedContentId
        {
            get
            {
                DataRow dr = ((DataRowView)grid.CurrentRow.DataBoundItem).Row;
                return (int)dr["Id"];

            }
        }
    }
}
