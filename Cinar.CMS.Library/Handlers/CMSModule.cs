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
            context.BeginRequest += context_BeginRequest;
            context.PreRequestHandlerExecute += context_PreRequestHandlerExecute;
            context.AcquireRequestState += context_AcquireRequestState;
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

        void context_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                string fullOrigionalPath = HttpContext.Current.Request.RawUrl.Replace("http://", "");
                // falanca.com/en/Content/Urunler/Bilgisayar_3.aspx
                if (fullOrigionalPath.Contains(".aspx") && fullOrigionalPath.Split('/').Length == 5)
                {
                    string[] parts = fullOrigionalPath.Split('/');
                    string lang = parts[1];

                    if (!Provider.CurrentCulture.StartsWith(lang))
                        Provider.CurrentCulture = lang;
                }
            }
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                string fullOrigionalPath = HttpContext.Current.Request.RawUrl.Replace("http://", "");

                if (fullOrigionalPath.Contains(".aspx"))
                {
                    // falanca.com/[en/]Content/Urunler/Bilgisayar_3.aspx
                    string[] parts = fullOrigionalPath.Split('/');
                    if (parts.Length >= 4)
                    {
                        var i = 0;
                        if (parts.Length == 5) 
                            i = 1;

                        string pageName = parts[1 + i];

                        if (parts[3 + i].Contains(".aspx"))
                        {
                            int item = int.Parse(parts[3 + i].Substring(0, parts[3 + i].IndexOf(".aspx")).SplitAndGetLast('_'));
                            string restOfQueryString = fullOrigionalPath.Contains("aspx?") ? fullOrigionalPath.Split(new[] { "aspx?" }, StringSplitOptions.None)[1] : "";
                            string rewritePath = pageName + ".aspx?item=" + item + (restOfQueryString != "" ? "&" + restOfQueryString : "");
                            HttpContext.Current.RewritePath(rewritePath);
                        }
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
