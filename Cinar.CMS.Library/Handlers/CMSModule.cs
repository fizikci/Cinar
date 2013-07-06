using System;
using System.Web;
using System.Reflection;
using System.IO;
using System.Text;

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

                        foreach (string scriptName in new[] { "cinar_cms_css", "cinar_cms_js", "controls_js", "default_css", "default_js", "en_js", "message_js", "tr_js", "help_html" })
                        {
                            string s = Properties.Resources.ResourceManager.GetString(scriptName);
                            File.WriteAllText(Provider.Server.MapPath("/_thumbs/" + scriptName.Replace("_", ".")), s, Encoding.UTF8);
                        }

                        File.WriteAllText(Provider.Server.MapPath("/_thumbs/DefaultJavascript.js"), Provider.Configuration.DefaultJavascript, Encoding.UTF8);
                        File.WriteAllText(Provider.Server.MapPath("/_thumbs/DefaultStyleSheet.css"), Provider.Configuration.DefaultStyleSheet, Encoding.UTF8);
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
            } catch{}
        }

    }
}
