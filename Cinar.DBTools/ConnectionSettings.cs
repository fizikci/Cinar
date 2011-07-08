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
                Database = new Database.Database(Provider, Host, DbName, UserName, Password, 30, null, CreateDatabaseIfNotExist);
            }
            else
            {
                Database.SetConnectionString(Provider, Host, DbName, UserName, Password, 30);
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
            Database = new Database.Database(Provider, Host, DbName, UserName, Password, 30, null, CreateDatabaseIfNotExist);
            Cinar.DBTools.Provider.ConnectionsModified = true;
        }
    }
}