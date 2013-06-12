using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Cinar.Database
{
    public class CacheProvider
    {
        Hashtable desktopCache = new Hashtable();

        public object this[string name]
        {
            get
            {
                if (HttpContext.Current == null)
                    return desktopCache[name];
                else
                    return HttpContext.Current.Cache[name];
            }
            set
            {
                if (HttpContext.Current == null)
                    desktopCache[name] = value;
                else
                    HttpContext.Current.Cache[name] = value;
            }
        }

        public bool ContainsKey(string key)
        {
            if (HttpContext.Current == null)
                return desktopCache.ContainsKey(key);
            else
                return HttpContext.Current.Cache[key] != null;
        }

        public void Remove(string key)
        {
            if (HttpContext.Current == null)
                desktopCache.Remove(key);
            else
                HttpContext.Current.Cache.Remove(key);
        }
    }

}
