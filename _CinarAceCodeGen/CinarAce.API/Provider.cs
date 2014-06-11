using Cinar.Database;
using Membership.API.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace $=db.Name$.API
{
    public static class Provider
    {
        private static Database db;

        public static Database Database
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items["db"] == null)
                    {
                        Database db = GetNewDatabaseInstance();
                        db.DefaultCommandTimeout = 1000;
                        db.CreateTablesAutomatically = true;
                        HttpContext.Current.Items["db"] = db;
                    }
                    return (Database)HttpContext.Current.Items["db"];
                }
                else
                {
                    if (db == null)
                        db = GetNewDatabaseInstance();
                    return db;
                }
            }
        }

        public static Database GetNewDatabaseInstance()
        {

            return new Database(ConfigurationManager.AppSettings["dbConnStr"], DatabaseProvider.SQLServer);
        }

        public static List<T> ReadListWithCache<T>(this IDatabase db) where T : IDatabaseEntity
        {
            if (HttpContext.Current.Cache[typeof(T).FullName] == null)
                HttpContext.Current.Cache.Insert(typeof(T).FullName, db.ReadList<T>(), null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 10, 0));

            return (List<T>)HttpContext.Current.Cache[typeof(T).FullName];
        }

        public static Member CurrentMember {
            get
            {
                if (HttpContext.Current.Session["Member"] != null)
                    return (Member)HttpContext.Current.Session["Member"];


                return new Member() { Id = 0, UserName = "Anonim" };
            }
            set { HttpContext.Current.Session["Member"] = value; }
        }

        // Provider.TR("{0} records of total {1}", recCount, total);
        public static string TR(string text, params object[] parameters)
        {
            return string.Format(text, parameters);
        }

    }
}