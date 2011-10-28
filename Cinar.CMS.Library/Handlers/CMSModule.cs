using System;
using System.Web;

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
