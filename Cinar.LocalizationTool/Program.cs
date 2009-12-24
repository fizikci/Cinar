using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using Cinar.Database;

namespace Cinar.LocalizationTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static Database.Database db;
        public static Database.Database Database
        {
            get
            {
                if (db == null)
                {
                    string sqlCon = ConfigurationSettings.AppSettings["sqlConnection"];
                    DatabaseProvider sqlPro = (DatabaseProvider)Enum.Parse(typeof(DatabaseProvider), ConfigurationSettings.AppSettings["sqlProvider"]);
                    db = new Database.Database(sqlCon, sqlPro);
                }
                return db;
            }
        }
    }
}
