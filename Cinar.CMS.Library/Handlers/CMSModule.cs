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
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            string fullOrigionalPath = HttpContext.Current.Request.Url.ToString().Replace("http://","");
            // sm.Main.Hakkımızda.1.aspx
            if (fullOrigionalPath.Contains(".aspx") && fullOrigionalPath.Split('/').Length == 4)
            {
                string[] parts = fullOrigionalPath.Split('/');
                string pageName = parts[1];

                if(parts[3].Contains(".aspx"))
                {
                    int item = int.Parse(parts[3].Replace(".aspx","").SplitAndGetLast('_'));
                    string restOfQueryString = fullOrigionalPath.Contains("aspx?") ? fullOrigionalPath.Split(new[] { "aspx?" }, StringSplitOptions.None)[1] : "";
                    string rewritePath = pageName + ".aspx?item=" + item + (restOfQueryString != "" ? "&" + restOfQueryString : "");
                    HttpContext.Current.RewritePath(rewritePath);
                }
            }

        }

    }
}
