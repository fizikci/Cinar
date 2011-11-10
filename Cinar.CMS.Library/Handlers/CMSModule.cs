using System;
using System.Web;
using System.Reflection;

namespace Cinar.CMS.Library.Handlers
{
    public class CMSModule : IHttpModule
    {
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);

            // StopMonitoring app folders deletion
            //TODO: uçur bunu bi şekilde..
            PropertyInfo p = typeof(System.Web.HttpRuntime).GetProperty("FileChangesMonitor", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            object o = p.GetValue(null, null);
            FieldInfo f = o.GetType().GetField("_dirMonSubdirs", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
            object monitor = f.GetValue(o);
            MethodInfo m = monitor.GetType().GetMethod("StopMonitoring", BindingFlags.Instance | BindingFlags.NonPublic);
            m.Invoke(monitor, new object[] { }); 
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            string fullOrigionalpath = HttpContext.Current.Request.Url.ToString();
            // sm.Main.Hakkımızda.1.aspx
            if (fullOrigionalpath.Contains("/sm/"))
            {
                string url = fullOrigionalpath.Substring(fullOrigionalpath.IndexOf("/sm/") + 4);
                string pageName = url.Split('/')[0];

                if(url.Contains(".aspx"))
                {
                    int item = int.Parse(url.Split('/')[1]);
                    string restOfQueryString = fullOrigionalpath.Contains("aspx?") ? fullOrigionalpath.Split(new[] { "aspx?" }, StringSplitOptions.None)[1] : "";
                    string rewritePath = pageName + ".aspx?item=" + item + (restOfQueryString != "" ? "&" + restOfQueryString : "");
                    HttpContext.Current.RewritePath(rewritePath);
                }
            }

        }

    }
}
