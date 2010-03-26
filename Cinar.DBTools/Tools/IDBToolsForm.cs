using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cinar.DBTools.Tools
{
    public interface IDBToolsForm
    {
        FormMain MainForm { get; set; }
        void Show();
    }
}
