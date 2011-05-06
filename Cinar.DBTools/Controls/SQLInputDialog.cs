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
        public SQLInputDialog(string sql, bool readOnly, string message = "To complete your process, the following query is going to be executed on database {0}. Please be sure that it doesn\'t harm your data.")
        {
            InitializeComponent();

            txtCode.Text = sql;
            txtCode.Document.ReadOnly = readOnly;
            label1.Text = string.Format(message, Provider.ActiveConnection);
        }

        public string SQL
        {
            get { return txtCode.Text; }
            set { txtCode.Text = value; }
        }
    }
}
