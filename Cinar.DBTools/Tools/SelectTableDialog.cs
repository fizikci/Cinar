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
    public partial class ListBoxDialog : Form
    {
        public ListBoxDialog()
        {
            InitializeComponent();
        }

        public ListBox ListBox { get{ return lbTables;} }

        public List<T> GetSelectedItems<T>() 
        {
            return lbTables.SelectedItems.Cast<T>().ToList();
        }
    }
}
