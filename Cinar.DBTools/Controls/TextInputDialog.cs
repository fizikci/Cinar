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
    public partial class TextInputDialog : Form
    {
        public TextInputDialog()
        {
            InitializeComponent();
        }

        public string TextInput
        {
            get { return txtCode.Text; }
            set { txtCode.Text = value; }
        }
    }
}
