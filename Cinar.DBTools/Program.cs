using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Cinar.DBTools
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form f = null;
            if (args != null && args.Length > 0)
            {
                switch (args[0])
                {
                    case "help":
                        Console.WriteLine("");
                        break;
                    case "dbintegration":
                        f = new Tools.FormDBIntegration();
                        if (args.Length > 1)
                            (f as Tools.FormDBIntegration).StartIntegration(args[1]);
                        break;
                }
            }
            else
                f = new FormMain();

            Application.Run(f);
        }
    }
}