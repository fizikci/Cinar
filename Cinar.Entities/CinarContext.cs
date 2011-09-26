using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Cinar.Entities.Standart;

namespace Cinar.Entities
{
    public class CinarContext
    {
        public static Database.IDatabase Db;

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
