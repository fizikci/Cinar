using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Configuration;
using System.Collections;
using System.Web;
using System.Web.SessionState;
using System.Diagnostics;
using Cinar.CMS.Library.Modules;
using Cinar.Scripting;
using Cinar.CMS.Library.Entities;
using System.Text.RegularExpressions;


namespace Cinar.CMS.Library.Handlers
{
    public class ASPXHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return false; }
        }

        internal List<Module> modules = null;

        private string getModuleTypesJSON()
        {
            SortedDictionary<string, ArrayList> grups = new SortedDictionary<string, ArrayList>();
            foreach (Type type in Provider.GetModuleTypes())
            {
                ModuleInfoAttribute mInfo = (ModuleInfoAttribute)CMSUtility.GetAttribute(type, typeof(ModuleInfoAttribute));
                string grup = Provider.GetResource(mInfo.Grup);
                if (!mInfo.Visible) continue; //***
                if (!grups.ContainsKey(grup)) grups.Add(grup, new ArrayList());
                grups[grup].Add("{name:'" + Provider.GetResource(type.Name) + "',id:'" + type.Name + "'}");
            }
            ArrayList res = new ArrayList();
            foreach (string grup in grups.Keys)
            {
                //grups[grup].Sort();
                res.Add("{grup:'" + grup + "', items:[" + String.Join(",", (string[])grups[grup].ToArray(typeof(string))) + "]}");
            }
            return "[" + String.Join(",", (string[])res.ToArray(typeof(string))) + "]";
        }
        private string getEntityTypesJSON()
        {
            ArrayList al = new ArrayList();
            foreach (Type type in Provider.GetEntityTypes())
            {
                ListFormPropsAttribute attr = (ListFormPropsAttribute)CMSUtility.GetAttribute(type, typeof(ListFormPropsAttribute));
                //if (attr.VisibleAtMainMenu) 
                al.Add(String.Format("['{0}','{1}',{2}]", type.Name, Provider.GetResource(type.Name), attr.VisibleAtMainMenu.ToJS()));
            }
            //al.Sort();
            return "[" + String.Join(",", (string[])al.ToArray(typeof(string))) + "]";
        }
        private string getTemplatesJSON()
        {
            Template[] templates = (Template[])Provider.Database.ReadList(typeof(Template), "select FileName from Template order by FileName");
            string[] fileNames = new string[templates.Length];
            for (int i = 0; i < templates.Length; i++)
                fileNames[i] = "'" + templates[i].FileName + "'";
            return "[" + String.Join(", ", fileNames) + "]";
        }

        public bool HasModule(Type moduleType)
        {
            return modules.Exists(delegate(Module mdl) { return mdl.GetType() == moduleType; });
        }
        public Module GetModule(Type moduleType)
        {
            return modules.Find(delegate(Module mdl) { return mdl.GetType() == moduleType; });
        }
        private void readAllModules()
        {
            this.modules = new List<Module>(Module.Read(template.FileName));
            foreach (Module mdl in this.modules)
            {
                mdl.ContainerPage = this;
                this.readModulesRecursive(mdl);

                Stopwatch stopWatch = new Stopwatch(); stopWatch.Start();

                mdl.output = mdl.Show();

                stopWatch.Stop(); if (Provider.ShowExecutionTime) mdl.output += String.Format("<!-- {0} ms  ({1}) -->", stopWatch.ElapsedMilliseconds, mdl.CacheHint);
            }
        }
        private void readModulesRecursive(Module mdl)
        {
            if (mdl is ModuleContainer)
            {
                ModuleContainer container = (ModuleContainer)mdl;
                foreach (Module childModule in container.ChildModules)
                {
                    childModule.ContainerPage = this;
                    readModulesRecursive(childModule);

                    Stopwatch stopWatch = new Stopwatch(); stopWatch.Start();

                    childModule.output = childModule.Show();

                    stopWatch.Stop(); if (Provider.ShowExecutionTime) childModule.output += String.Format("<!-- {0} ms  ({1}) -->", stopWatch.ElapsedMilliseconds, childModule.CacheHint);
                }
            }
        }

        public bool AddDefaultJS = true;
        public bool AddDefaultCSS = true;

        public string CDN { get { return Provider.AppSettings["CDN"] ?? ""; } }

        private string _title = "";
        public string Title {
            get {
                if (_title == "")
                {
                    string title = "";
                    if (Provider.Content != null && Provider.Content.Id != 1)
                        title += Provider.Content.Title;
                    if (Provider.Content != null && Provider.Content.Id != 1 && !string.IsNullOrWhiteSpace(Provider.Configuration.SiteName))
                        title += " - ";
                    if (!string.IsNullOrWhiteSpace(Provider.Configuration.SiteName))
                        title += Provider.Configuration.SiteName;
                    return title;
                }
                return _title;
            }
            set {
                _title = value;
            }
        }

        private string _description = "";
        public string Description
        {
            get
            {
                if (_description == "")
                {
                    return (Provider.Content != null && !string.IsNullOrWhiteSpace(Provider.Content.Description)) ? CMSUtility.HtmlEncode(Provider.Content.Description) : CMSUtility.HtmlEncode(Provider.Configuration.SiteDescription);
                }
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        private string _image = "";
        public string Image
        {
            get
            {
                if (_image == "")
                {
                    return ((Provider.Content != null && !string.IsNullOrWhiteSpace(Provider.Content.Picture)) ? Provider.Content.Picture : Provider.Configuration.SiteLogo);
                }
                return _image;
            }
            set
            {
                _image = value;
            }
        }

        public string HeadSection
        {
            get
            {
                StringBuilder sb = new StringBuilder();


                if (Provider.Request["Id"] != null) {
                    int id = 0;
                    if (int.TryParse(Provider.Request["Id"], out id))
                    {
                        Post p = Provider.Database.Read<Post>(id);
                        if (p != null)
                        {
                            Title = p.InsertUser.Nick + " " + Provider.Configuration.SiteName + "'te paylaştı";
                            Description = p.Metin;
                        }
                    }
                }

                sb.Append("<title>" + Provider.Server.HtmlEncode(Title) + "</title>\n");
                sb.Append("<link rel =\"canonical\" href =\"//" + Provider.Request.Url.Authority + Provider.Request.RawUrl + "\" />\n");
                sb.Append("<meta name=\"description\" content=\"" + Provider.Server.HtmlEncode(Description) + "\"/>\n");
                sb.Append("<meta name=\"keywords\" content=\"" + (Provider.Content != null ? CMSUtility.HtmlEncode(Provider.Content.Keywords) + " " + CMSUtility.HtmlEncode(Provider.Content.Tags) + "," : "") + CMSUtility.HtmlEncode(Provider.Configuration.SiteKeywords) + "\"/>\n");
                sb.Append("<meta name=\"viewport\" content=\"width=device-width\"/>\n");
                sb.Append("<META HTTP-EQUIV=\"Content-Type\" CONTENT=\"text/html; charset=utf-8\"/>\n");
                sb.Append("<META HTTP-EQUIV=\"Content-Language\" CONTENT=\""+(Provider.CurrentCulture==null ? "EN" : Provider.CurrentCulture.Split('-')[0])+"\"/>\n");
                foreach (var lang in Provider.Database.GetList<string>("select Code from Lang where Id<>{0}", Provider.CurrentLanguage.Id))
                {
                    CinarUriParser uriParser = new CinarUriParser(Provider.Request.Url.Scheme + "://" + Provider.Request.Url.Authority + Provider.Request.RawUrl);
                    uriParser.QueryPart["currentCulture"] = lang;
                    sb.Append("<link rel=\"alternate\" href=\"" + uriParser + "\" hreflang=\"" + lang + "\" />\n");
                }

                if(!Provider.Configuration.FacebookAppId.IsEmpty())
                    sb.Append("<meta property=\"fb:app_id\" content=\""+Provider.Configuration.FacebookAppId+"\"/>\n");

                sb.Append("<meta property=\"og:title\" content=\"" + Provider.Server.HtmlEncode(Title) + "\"/>\n");
                sb.Append("<meta property=\"og:image\" content=\"" + Image + "\"/>\n");
                sb.Append("<meta property=\"og:site_name\" content=\"" + Provider.Server.HtmlEncode(Provider.Configuration.SiteName) + "\"/>\n");
                sb.Append("<meta property=\"og:description\" content=\"" + Provider.Server.HtmlEncode(Description) + "\"/>\n");

                sb.Append("<meta name=\"twitter:card\" content=\"summary\">\n");
                sb.Append("<meta name=\"twitter:title\" content=\"" + Provider.Server.HtmlEncode(Title) + "\"/>\n");
                sb.Append("<meta name=\"twitter:image\" content=\"" + Image + "\"/>\n");

                if (Provider.Configuration.SiteIcon.Trim() != "")
                    sb.Append("<link href=\"" + Provider.Configuration.SiteIcon + "\" rel=\"SHORTCUT ICON\"/>\n");
                sb.Append("<link href=\"/RSS.ashx?item=" + (Provider.Content == null ? 1 : Provider.Content.Id) + "\" rel=\"alternate\" title=\"" + Provider.Configuration.SiteName + "\" type=\"application/rss+xml\" />\n");

                ////////////////////////////////////// CSS

                sb.Append("<link href=\"" + (Provider.DesignMode ? "default.css.ashx" : "/_thumbs/default.css") + "\" rel=\"stylesheet\" type=\"text/css\"/>\n");
                if (Provider.Configuration.UseExternalLibrary.Contains("jQueryUI"))
                {
                    sb.Append("<link href=\"" + CDN + "/external/themes/ui-lightness/jquery-ui-1.10.3.custom.min.css\" rel=\"stylesheet\">\n");
                }
                if (Provider.Configuration.UseExternalLibrary.Contains("Bootstrap"))
                {
                    sb.Append("<link rel=\"stylesheet\" href=\"" + CDN + "/external/bootstrap/css/bootstrap.min.css\">\n");
                }
                sb.Append("<link href=\"" + (Provider.DesignMode ? "cinar.cms.css.ashx" : "/_thumbs/cinar.cms.css") + "\" rel=\"stylesheet\" type=\"text/css\"/>\n");
                sb.Append("<link href=\"" + (Provider.DesignMode ? "famfamfam.css.ashx" : "/_thumbs/famfamfam.css") + "\" rel=\"stylesheet\" type=\"text/css\"/>\n");

                sb.Append("<link href=\"" + CDN + "/external/themes/default.css\" rel=\"stylesheet\" type=\"text/css\"/>\n");
                sb.Append("<link href=\"" + CDN + "/external/themes/alphacube.css\" rel=\"stylesheet\" type=\"text/css\"/>\n");

                if (Provider.DesignMode)
                    sb.Append("<style title=\"moduleStyles\">\n" + (AddDefaultCSS ? Provider.Configuration.DefaultStyleSheet : "") + "\n" + Provider.ReadStyles(modules) + "\n</style>\n");
                else
                {
                    if (AddDefaultCSS)
                        sb.Append("<link href=\"" + (Provider.DesignMode ? "DefaultStyleSheet.css.ashx" : "/_thumbs/DefaultStyleSheet.css") + "\" rel=\"stylesheet\" type=\"text/css\"/>\n");
                    sb.Append("<style title=\"moduleStyles\">\n" + Provider.ReadStyles(modules) + "\n</style>\n");
                }

                sb.AppendFormat("<script type='text/javascript'>var designMode = {0}, defaultLangId = {1}; currLang = '{2}'; isDesigner={3};</script>\n", Provider.DesignMode.ToJS(), Provider.Configuration.DefaultLang, Provider.CurrentLanguage.Code.Split('-')[0], Provider.User.IsInRole("Designer").ToJS());

                ///////////////////////////////////// JS
                if (template == null || !template.HTMLCode.Contains("$=this.Scripts"))
                    sb.Append(Scripts);

                return sb.ToString();
            }
        }

        public string Scripts
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<script src=\"" + CDN + "/external/javascripts/jquery-1.10.2.min.js\"></script>\n");
                if (Provider.Configuration.UseExternalLibrary.Contains("jQuery"))
                {
                    sb.Append("<script src=\"" + CDN + "/external/javascripts/jquery-ui-1.10.3.custom.min.js\"></script>\n");
                }
                if (Provider.Configuration.UseExternalLibrary.Contains("Bootstrap"))
                {
                    sb.Append("<script src=\"" + CDN + "/external/bootstrap/js/bootstrap.min.js\"></script>\n");
                }

                sb.Append("<script type=\"text/javascript\" src=\"" + CDN + "/external/javascripts/prototype.js\"></script>\n");

                sb.Append("<script type=\"text/javascript\" src=\"" + (Provider.DesignMode ? "default.js.ashx" : "/_thumbs/default.js") + "\"></script>\n");
                sb.Append("<script type=\"text/javascript\" src=\"" + (Provider.DesignMode ? "message.js.ashx" : "/_thumbs/message.js") + "\"> </script>\n");
                sb.Append("<script type=\"text/javascript\" src=\"" + (Provider.DesignMode ? (Provider.CurrentCulture.Split('-')[0] + ".js.ashx") : ("/_thumbs/" + Provider.CurrentCulture.Split('-')[0] + ".js")) + "\"></script>\n");

                sb.Append("<script type=\"text/javascript\" src=\"" + CDN + "/external/javascripts/window.js\"></script>\n");


                if (Provider.DesignMode || Provider.User.IsInRole("Editor"))
                {
                    sb.Append("<script type=\"text/javascript\" src=\"" + (Provider.DesignMode ? "controls.js.ashx" : "/_thumbs/controls.js") + "\"></script>\n");
                    sb.Append(@"
<script type=""text/javascript"">
    var currTemplate = '" + template.FileName + @"';
    var moduleTypes = " + getModuleTypesJSON() + @";
    var entityTypes = " + getEntityTypesJSON() + @";
    var templates = " + getTemplatesJSON() + @";
</script>
                ");
                    sb.Append("<script type=\"text/javascript\" src=\"" + (Provider.DesignMode ? "cinar.cms.js.ashx" : "/_thumbs/cinar.cms.js") + "\"></script>\n");
                    sb.Append("<script src=\"" + CDN + "/external/javascripts/ace/ace.js\" type=\"text/javascript\" charset=\"utf-8\"></script>\n");
                }

                if (!Provider.User.IsAnonim())
                {
                    sb.Append(@"
<script type=""text/javascript"">
    var cc_sk = 0;
    var cc_sk_i = setInterval(function(){
        new Ajax.Request('KeepSession.ashx', {
            method: 'post',
            onComplete: function(req) {
                cc_sk++;
                if(cc_sk>50)
                    clearInterval(cc_sk_i);
            }
        });
    }, 60 * 1000);
</script>
");
                }

                if (AddDefaultJS)
                    sb.Append("<script type=\"text/javascript\" src=\"" + (Provider.DesignMode ? "DefaultJavascript.ashx" : "/_thumbs/DefaultJavascript.js") + "\"></script>\n");

                return sb.ToString();
            }
        }


        public string this[string regionName]
        {
            get
            {
                //StringBuilder sb = new StringBuilder();
                int moduleCount = 0;

                foreach (Module module in this.modules)
                {
                    if (module.Region == regionName && module.Template.ToLower() == template.FileName.ToLower())
                    {
                        //sb.Append(module.Show());
                        Provider.Response.Write(module.Show());
                        moduleCount++;
                    }
                }

                if (moduleCount == 0 && Provider.DesignMode)
                    Provider.Response.Write("<div class=\"cs_empty_reg\">" + Provider.GetResource("Empty region") + ": " + regionName + "<br/><span>" + Provider.GetResource("Right click and select \"Add Module\" to enrich this region.") + "</span></div>");

                return "";
            }
        }

        private Template template;


        public void ProcessRequest(HttpContext context)
        {
            #region control attacks
            if (!string.IsNullOrWhiteSpace(Provider.Request["item"]))
            {
                var idStr = Provider.Request["item"];
                var m = Regex.Match(idStr, "\\d+");
                if (!m.Success || m.Value != idStr)
                {
                    Provider.Response.Redirect("/Default.aspx", true);
                    return;
                }
            }
            if (Provider.Request.QueryString!=null)
                for (int i = 0; i < Provider.Request.QueryString.Count; i++)
                {
                    var key = Provider.Request.QueryString.GetKey(i);
                    var val = Provider.Request.QueryString[key];
                    if (key!=null && key.EndsWith("Id") && !string.IsNullOrWhiteSpace(val))
                    {
                        var m = Regex.Match(val, "[\\d\\w\\,\\-]+");
                        if (!m.Success || m.Value != val)
                        {
                            Provider.Response.Redirect("/Default.aspx", true);
                            return;
                        }
                    }
                }
            #endregion

            if (Provider.DevelopmentMode)
            {
                Provider.Database.EnableSQLLog = true;
                Provider.Database.ClearSQLLog();
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            string path = context.Request.Url.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped);
            string fileName = path.Substring(path.LastIndexOf('/') + 1);

            template = (Template)Provider.Database.Read(typeof(Template), "FileName={0}", fileName);
            if (template == null)
                throw new HttpException(404, "Template doesn't exist: " + fileName);

            if (template.HTMLCode.Trim().StartsWith("use:"))
            {
                var templateName = template.HTMLCode.Replace("use:", "").Trim();
                var htmlCode = Provider.Database.GetString("select HTMLCode from Template where FileName={0}", templateName);
                if(!string.IsNullOrWhiteSpace(htmlCode))
                    template.HTMLCode = htmlCode;
            }

            Provider.OnBeginRequest();

            Provider.Response.BufferOutput = Provider.Configuration.BufferOutput;

            if (!string.IsNullOrWhiteSpace(Provider.Configuration.DefaultPageViewRole) && template.FileName != Provider.Configuration.LoginPage && !Provider.User.IsInRole(Provider.Configuration.DefaultPageViewRole))
            {
                Provider.Response.Redirect("/" + Provider.Configuration.LoginPage + "?RedirectURL=" + Provider.Server.UrlEncode(Provider.Request.RawUrl), true);
                return;
            }

            string strExecutionTimes = "";
            if (Provider.ShowExecutionTime)
                strExecutionTimes = "<!-- Page read finished at " + stopWatch.ElapsedMilliseconds + " ms -->";


            if (Provider.Request["currentCulture"] != null)
                Provider.CurrentCulture = Provider.Request["currentCulture"];

            var urlParts = Provider.Request.Url.ToString().Replace("://", "").Split('/');
            if(urlParts.Length>0 && urlParts[1].Length==2)
                Provider.CurrentCulture = urlParts[1];

            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Provider.CurrentCulture);
            }
            catch {
                Provider.CurrentCulture = "en-US";
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            }

            Provider.SetHttpContextUser();
            if (Provider.User.IsInRole("Designer") && !String.IsNullOrEmpty(Provider.Request["DesignMode"]))
                Provider.DesignMode = Provider.Request["DesignMode"] == "On";

            // persistentSessionId cookie'si set edilmisse otomatik oturum acalim.
            if (Provider.User.IsAnonim() && Provider.Request.Cookies["persistentSessionId"] != null && !Provider.Request.Cookies["persistentSessionId"].Value.IsEmpty())
            {
                var user = Provider.Database.Read<User>("Keyword = {0}", Provider.Request.Cookies["persistentSessionId"].Value);
                if (user == null)
                    Provider.Response.Cookies["persistentSessionId"].Expires = DateTime.Now.AddDays(-1);
                else
                    Provider.User = user;
            }

            #region item (içerik) id'sini Session'a koyalım
            if (Provider.Request["item"] != null)
            {
                int itemId = 0;
                Int32.TryParse(Provider.Request["item"], out itemId);
                if (itemId > 0)
                    Provider.Session["item"] = itemId;
                else
                    Provider.Session["item"] = null; //Provider.Content = null;
            }
            else
                Provider.Session["item"] = 1; //Provider.Content = null;
            #endregion

            Provider.Session.Timeout = Provider.Configuration.SessionTimeout;

            this.readAllModules();

            #region page security (User can access/edit this page?)
            if (this.HasModule(typeof(PageSecurity)))
            {
                PageSecurity ps = (PageSecurity)this.GetModule(typeof(PageSecurity));
                if (!String.IsNullOrEmpty(ps.RoleToRead) && !Provider.ContextUser.IsInRole(ps.RoleToRead))
                {
                    string redirectPage = string.IsNullOrEmpty(ps.RedirectPage) ? Provider.Configuration.MainPage : ps.RedirectPage;
                    Provider.Response.Redirect("/" + redirectPage + "?RedirectURL=" + Provider.Server.UrlEncode(Provider.Request.RawUrl), true);
                    return;
                }
                if (!Provider.ContextUser.IsInRole(ps.RoleToChange))
                {
                    Provider.DesignMode = false;
                }

            }
            #endregion

            if (Provider.ShowExecutionTime)
                strExecutionTimes += "<!-- All modules read finished at " + stopWatch.ElapsedMilliseconds + " ms -->";

            try
            {
                modules.ForEach(delegate(Module m) { if (Provider.DesignMode || m.Visible) m.beforeShow(); });
            }
            catch { }

            if (Provider.ShowExecutionTime)
                Provider.Response.Write(strExecutionTimes + "<!-- All modules beforeShow() finished at " + stopWatch.ElapsedMilliseconds + " ms -->");

            #region Cinar Script and attributes
            string pageContent = string.IsNullOrWhiteSpace(Provider.Configuration.DefaultPageLoadScript) ? "" : (" $ try{ $ " + Provider.Configuration.DefaultPageLoadScript+"\r\n $ } catch(ex) {echo(ex);} $ ");
            pageContent += template.HTMLCode;
            Interpreter engine = Provider.GetInterpreter(pageContent, this);
            engine.Parse();
            engine.Execute(Provider.Response.Output);
            #endregion


            if (Provider.ShowExecutionTime)
                Provider.Response.Write("<!-- All modules Show() finished at " + stopWatch.ElapsedMilliseconds + " ms -->");

            try
            {
                modules.ForEach(delegate(Module m) { if (Provider.DesignMode || m.Visible) m.afterShow(); });
            }
            catch { }

            if (Provider.ShowExecutionTime)
                Provider.Response.Write("<!-- All modules afterShow() finished at " + stopWatch.ElapsedMilliseconds + " ms -->");

            // bu hiti kaydedelim
            if (Provider.Configuration.LogHit && Provider.Request.Browser!=null && !Provider.Request.Browser.Crawler)
                new Hit().Save();

            stopWatch.Stop();

            if (Provider.ShowExecutionTime)
                Provider.Response.Write("<!-- TOTAL: " + stopWatch.ElapsedMilliseconds + " ms -->");

            Provider.Database.Connection.Close();
        }
    }
}