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

            this.AutoSelectOptions.Add("Select", null);
            this.AutoSelectOptions.Add("All", (obj) => { return true; });
            this.AutoSelectOptions.Add("None", (obj) => { return false; });
        }

        public ListBox ListBox { get{ return lbTables;} }

        public string Message { get { return label1.Text; } set { label1.Text = value; } }

        public List<T> GetSelectedItems<T>() 
        {
            return lbTables.SelectedItems.Cast<T>().ToList();
        }

        public Dictionary<string, Func<object, bool>> AutoSelectOptions = new Dictionary<string, Func<object, bool>>();

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            foreach (var item in AutoSelectOptions) {
                cbSelectionOptions.Items.Add(item.Key);
            }
        }

        private void cbSelectionOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func<object, bool> function = AutoSelectOptions[cbSelectionOptions.SelectedItem.ToString()];

            if (function == null)
                return;

            for (int i = 0; i < ListBox.Items.Count; i++)
                ListBox.SetSelected(i, function(ListBox.Items[i]));
        }
    }
}
