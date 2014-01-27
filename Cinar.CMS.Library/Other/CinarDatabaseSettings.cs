using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Cinar.CMS.Library.Other
{
    public class CinarDatabaseElement : ConfigurationElement
    {
        [ConfigurationProperty("siteName", IsKey = true, IsRequired = true)]
        public string SiteName
        {
            get { return (string)this["siteName"]; }
            set { this["siteName"] = value; }
        }

        [ConfigurationProperty("sqlProvider", IsRequired = true)]
        public DatabaseProvider SqlProvider
        {
            get { return (DatabaseProvider)this["sqlProvider"]; }
            set { this["sqlProvider"] = value; }
        }

        [ConfigurationProperty("sqlConnection", IsRequired = true)]
        public string SqlConnection
        {
            get { return (string)this["sqlConnection"]; }
            set { this["sqlConnection"] = value; }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        }
    }

    [ConfigurationCollection(typeof(CinarDatabaseElement))]
    public class CinarDatabaseElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CinarDatabaseElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CinarDatabaseElement)element).SiteName;
        }

        public CinarDatabaseElement this[string siteName]
        {
            get {
                foreach (CinarDatabaseElement item in this)
                    if (item.SiteName == siteName)
                        return item;
                return null;
            }
        }
    }

    public class CinarDatabaseSection : ConfigurationSection
    {
        [ConfigurationProperty("sites", IsDefaultCollection = true)]
        public CinarDatabaseElementCollection Sites
        {
            get { return (CinarDatabaseElementCollection)this["sites"]; }
            set { this["sites"] = value; }
        }
    }
}
