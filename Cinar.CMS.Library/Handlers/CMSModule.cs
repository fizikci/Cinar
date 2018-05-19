using System;
using System.Web;
using System.Reflection;
using System.IO;
using System.Text;
using System.Web.Routing;
using System.Linq;

namespace Cinar.CMS.Library.Handlers
{
    public class CMSModule : IHttpModule
    {
        public void Dispose()
        {
            
        }
        
        private static bool HasAppStarted = false;
        private readonly static object _syncObject = new object();
        private static object MapUrlMethod = null;

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
                    HasAppStarted = true;

                    Provider.RegenerateScripts();

                    if (Provider.DevelopmentMode && HttpContext.Current.Request.Url.IsLoopback)
                        HttpContext.Current.Response.Redirect("/DoLogin.ashx?Email=root@local&Passwd=root", false);
                }
            }
        }

        void context_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                string fullOrigionalPath = HttpContext.Current.Request.RawUrl.Replace("http://", "");
                // falanca.com/en/Content/Urunler/Bilgisayar_3.aspx
                if (fullOrigionalPath.Split('/').Length > 1)
                {
                    string[] parts = fullOrigionalPath.Split('/');
                    string lang = parts[1];

                    if (!Provider.CurrentCulture.StartsWith(lang))
                        if (lang == "tr" || lang == "en" || lang == "ru" || lang == "ar" || lang == "fr" || lang == "de")
                            Provider.CurrentCulture = lang;
                }
            }
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                string fullOrigionalPath = HttpContext.Current.Request.RawUrl.Replace("http://", "").Replace("https://", "");

                // check for any XSS attack
                string lowercaseUrl = fullOrigionalPath.ToLowerInvariant();
                if (
                    lowercaseUrl.Contains("onmouseover") ||
                    lowercaseUrl.Contains("onclick") ||
                    lowercaseUrl.Contains("onmouseup") ||
                    lowercaseUrl.Contains("onmousedown") ||
                    lowercaseUrl.Contains("javascript:") ||
                    lowercaseUrl.Contains("<script") ||
                    lowercaseUrl.Contains("%3cscript"))
                {
                    HttpContext.Current.RewritePath("404.aspx");
                    return;
                }

                //ashx'lere dokunma
                if (lowercaseUrl.Contains(".ashx"))
                    return;

                #region customAssembly Url Rewriting
                if (MapUrlMethod == null)
                {
                    if (!String.IsNullOrEmpty(Provider.AppSettings["customAssemblies"]))
                        foreach (string customAssembly in Provider.AppSettings["customAssemblies"].SplitWithTrim(','))
                        {
                            Assembly assembly = Provider.GetAssembly(customAssembly);
                            if (assembly == null)
                                continue;
                            var api = assembly.GetTypes().FirstOrDefault(t => t.GetInterface("IAPIProvider") != null);
                            if (api != null)
                                MapUrlMethod = api.GetMethod("MapUrl") ?? (object)false;
                        }
                    if (MapUrlMethod == null)
                        MapUrlMethod = (object)false;
                }

                if (MapUrlMethod is MethodInfo)
                {
                    object ret = ((MethodInfo)MapUrlMethod).Invoke(null, new object[] { HttpContext.Current.Request });
                    if (ret != null && !ret.ToString().IsEmpty())
                    {
                        HttpContext.Current.RewritePath(ret.ToString());
                        return;
                    }
                }
                #endregion


                // url rewrite
                if (fullOrigionalPath.Contains(".aspx"))
                {
                    #region default CMS url rewriting
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
                            return;
                        }
                    }
                    #endregion
                }

                #region CMS user defined url rewriting
                string[] pathParts = HttpContext.Current.Request.RawUrl.Split('?');
                string res = Provider.GetRewritePath(pathParts[0]);
                if (res != pathParts[0])
                {
                    if (pathParts.Length > 1)
                        res = res + (res.Contains("?") ? "&" : "?") + HttpContext.Current.Server.UrlEncode(pathParts[1]);
                    HttpContext.Current.RewritePath(res);
                }
                #endregion

            } catch{}
        }

    }
}
