using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cinar.DBTools.Controls
{
    public partial class SQLInputDialog : Form
    {
        public SQLInputDialog(string sql, bool readOnly)
        {
            InitializeComponent();

            txtCode.Text = sql;
            txtCode.Document.ReadOnly = readOnly;
            label1.Text = string.Format(label1.Text, Provider.ActiveConnection);
        }

        public string SQL
        {
            get { return txtCode.Text; }
            set { txtCode.Text = value; }
        }
    }
}
