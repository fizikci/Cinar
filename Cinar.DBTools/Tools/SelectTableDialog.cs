using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Database;

namespace Cinar.DBTools.Tools
{
    public partial class SelectTableDialog : Form
    {
        public SelectTableDialog()
        {
            InitializeComponent();

            if (Provider.Database != null)
                foreach (Table tbl in Provider.Database.Tables)
                    lbTables.Items.Add(tbl);
        }

        public List<Table> SelectedTables 
        {
            get
            {
                List<Table> list = new List<Table>();
                foreach (Table item in lbTables.SelectedItems)
                    list.Add(item);
                return list;
            }
        }
    }
}
