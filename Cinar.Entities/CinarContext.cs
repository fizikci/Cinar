using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Cinar.Database;
using Cinar.Entities.Standart;

namespace Cinar.Entities
{
    public class CinarContext
    {
        private static Database.IDatabase db;
        public static Database.IDatabase Db
        {
            get {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items["db"]==null)
                        return null;
                    return (IDatabase)HttpContext.Current.Items["db"];
                }

                return db;
            }
            set {
                if (HttpContext.Current != null)
                    HttpContext.Current.Items["db"] = value;
                else
                    db = value;
            }
        }

        private static User user;
        public static User ClientUser
        {
            get 
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Session["user"] == null)
                        HttpContext.Current.Session["user"] = new User { Name = "Anonim" };
                    return (User)HttpContext.Current.Session["user"];
                }
                else
                {
                    if(user==null)
                        user = new User { Name = "Anonim" };
                    return user;
                }
            }
            set 
            {
                if (HttpContext.Current != null)
                   HttpContext.Current.Session["user"] = value;
                else
                    user = value;
            }
        }

        //public static T ReadFromWebCache<T>(int id)
        //{
        //    if (System.Web.HttpContext.Current != null)
        //    {
        //        if(System.Web.HttpContext.Current.Items[typeof(T).FullName+id]==null)
        //            System.Web.HttpContext.Current.Items[typeof(T).FullName + id] = CinarContext.Db.Read<T>(id);
        //        return (T)System.Web.HttpContext.Current.Items[typeof(T).FullName + id];
        //    }
        //    return default(T);
        //}
    }
}
