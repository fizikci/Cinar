using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using Cinar.Database;
using Cinar.DBTools.Tools;

namespace Cinar.DBTools
{
    public class ConnectionSettings
    {
        public const int TIMEOUT = 200;

        [ReadOnly(true), Description("Database vendor of the current connection")]
        public DatabaseProvider Provider { get; set; }
        [ReadOnly(true), Description("Host of the current connection")]
        public string Host { get; set; }
        [ReadOnly(true), Description("Database name of the current connection")]
        public string DbName { get; set; }
        [ReadOnly(true), Description("Username of the current connection")]
        public string UserName { get; set; }
        [PasswordPropertyText(true), ReadOnly(true), Description("User password of the current connection")]
        public string Password { get; set; }
        [XmlIgnore, Browsable(false)]
        public bool CreateDatabaseIfNotExist { get; set; }

        public Database.Database Database;
        public List<Diagram> Schemas = new List<Diagram>();

        public void InitializeDatabase()
        {
            if (Database == null)
            {
                Database = new Database.Database(Provider, Host, DbName, UserName, Password, TIMEOUT, null, CreateDatabaseIfNotExist);
            }
            else
            {
                Database.DefaultCommandTimeout = TIMEOUT;
                Database.SetConnectionString(Provider, Host, DbName, UserName, Password);
                Database.SetCollectionParents();
                Database.CreateDbProvider(false);
            }
        }

        public override string ToString()
        {
            if (Provider == DatabaseProvider.Cinar)
                return "Cinar SQL Engine";

            string toStr = DbName + " (";
            if (Host.Contains('.'))
            {
                string[] parts = Host.Split('.');
                toStr += parts[parts.Length - 2] + "." + parts[parts.Length - 1];
            }
            else
                toStr += (Host == "localhost" ? "local" : Host);
            toStr += " " + Provider + ")";

            return toStr;
        }

        public void RefreshDatabaseSchema()
        {
            Database = new Database.Database(Provider, Host, DbName, UserName, Password, TIMEOUT, null, CreateDatabaseIfNotExist);
            Cinar.DBTools.Provider.ConnectionsModified = true;
        }

        internal List<string> GetUIModules()
        {
            return this.Database.Tables.OrderBy(t => t.GenerateUIMetadata().DisplayOrder).Select(t => t.UIMetadata.ModuleName).Distinct().ToList();
        }

        internal IEnumerable<string> GetUIModuleTables(string module)
        {
            if (string.IsNullOrWhiteSpace(module))
                return Database.Tables.Where(c => string.IsNullOrWhiteSpace(c.GenerateUIMetadata().ModuleName)).OrderBy(c => c.GenerateUIMetadata().DisplayOrder).Select(c => c.Name).OrderBy(n=>n).ToList();
            else
                return Database.Tables.Where(c => c.GenerateUIMetadata().ModuleName == module).OrderBy(c => c.GenerateUIMetadata().DisplayOrder).Select(c => c.Name).OrderBy(n => n).ToList();
        }
    }
}