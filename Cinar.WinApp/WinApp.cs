using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils.Drawing.Helpers;
using DevExpress.XtraEditors;
using Cinar.WinUI;

namespace Cinar.WinApp
{
    public static class WinApp
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            bool oturumAcildi = false;
            while (!oturumAcildi)
            {
                try
                {
                    oturumAcildi = DMT.Provider.Login();
                }
                catch (Exception ex)
                {
                    if (ex.Message != "Cancel")
                        XtraMessageBox.Show(ex.Message + (ex.InnerException != null ? Environment.NewLine + ex.InnerException.Message : ""), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            }

            if(oturumAcildi)
                Application.Run(new FormMain());

        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            XtraMessageBox.Show(ex.Message + (ex.InnerException != null ? Environment.NewLine + ex.InnerException.Message : ""), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}