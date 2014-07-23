using System;
using System.Web;
using System.Reflection;
using System.IO;
using System.Text;
using System.Web.Routing;

namespace Cinar.CMS.Library.Handlers
{
    public class CMSModule : IHttpModule
    {
        public void Dispose()
        {
            
        }
        
        private static bool HasAppStarted = false;
        private readonly static object _syncObject = new object();

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.PreRequestHandlerExecute += context_PreRequestHandlerExecute; 
        }

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            if (!HasAppStarted)
            {
                lock (_syncObject)
                {
                    if (!HasAppStarted)
                    {
                        HasAppStarted = true;

                        Provider.RegenerateScripts();
                    }
                }
            }
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                string fullOrigionalPath = HttpContext.Current.Request.RawUrl.Replace("http://", "");
                // sm.Main.Hakkımızda.1.aspx
                if (fullOrigionalPath.Contains(".aspx") && fullOrigionalPath.Split('/').Length == 4)
                {
                    string[] parts = fullOrigionalPath.Split('/');
                    string pageName = parts[1];

                    if (parts[3].Contains(".aspx"))
                    {
                        int item = int.Parse(parts[3].Substring(0, parts[3].IndexOf(".aspx")).SplitAndGetLast('_'));
                        string restOfQueryString = fullOrigionalPath.Contains("aspx?") ? fullOrigionalPath.Split(new[] { "aspx?" }, StringSplitOptions.None)[1] : "";
                        string rewritePath = pageName + ".aspx?item=" + item + (restOfQueryString != "" ? "&" + restOfQueryString : "");
                        HttpContext.Current.RewritePath(rewritePath);
                    }
                }

                string[] pathParts = HttpContext.Current.Request.RawUrl.Split('?');
                string res = Provider.GetRewritePath(pathParts[0]);
                if (res != pathParts[0])
                {
                    if (pathParts.Length > 1)
                        res = res + (res.Contains("?") ? "&" : "?") + HttpContext.Current.Server.UrlEncode(pathParts[1]);
                    HttpContext.Current.RewritePath(res);
                }
            } catch{}
        }

    }
}
