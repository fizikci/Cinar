using Cinar.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cinar.CMS.DesktopEditor
{
    public static class Provider
    {
        private static Dictionary<int, Database.Database> listDb = new Dictionary<int, Database.Database>();
        public static Database.Database GetDb(int index)
        {
            try
            {
                if (!listDb.ContainsKey(index))
                    listDb[index] = new Database.Database(Provider.GetConnectionString(index), Provider.GetDbProvider(index), Application.ExecutablePath.ToLowerInvariant().Replace("cinar.cms.desktopeditor.exe", "db" + index + ".config"));
                return listDb[index];
            }
            catch{
                return null;
            }
        }

        private static DatabaseProvider GetDbProvider(int index)
        {
            return Settings.Load().Providers[index];
        }

        private static string GetConnectionString(int index)
        {
            return Settings.Load().ConnectionStrings[index];
        }

        public static System.Collections.Specialized.NameValueCollection CopiedContent { get; set; }

        public static string CopiedPicturePath { get; set; }
    }

    public class Settings
    {
        public Dictionary<int, string> ConnectionStrings { get; set; }
        public Dictionary<int, DatabaseProvider> Providers { get; set; }
        public Dictionary<int, string> SiteAddress { get; set; }
        public Dictionary<int, string> Emails { get; set; }
        public Dictionary<int, string> Passwords { get; set; }
        public Dictionary<int, string> Feed { get; set; }

        private static string getJsonPath() {
            return Application.ExecutablePath.ToLowerInvariant().Replace(".exe", ".json");
        }

        public static Settings Load()
        {
            string jsonPath = getJsonPath();
            if (!File.Exists(jsonPath))
            {
                Settings sts = new Settings();
                sts.ConnectionStrings = new Dictionary<int, string> { };
                sts.Providers = new Dictionary<int, DatabaseProvider> { };
                sts.SiteAddress = new Dictionary<int, string> { };
                sts.Emails = new Dictionary<int, string> { };
                sts.Passwords = new Dictionary<int, string> { };
                sts.Feed = new Dictionary<int, string> { };

                string json = JsonConvert.SerializeObject(sts, Formatting.Indented);
                File.WriteAllText(jsonPath, json, Encoding.UTF8);
            }

            return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(jsonPath, Encoding.UTF8));
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(getJsonPath(), json, Encoding.UTF8);
        }
    }
}
