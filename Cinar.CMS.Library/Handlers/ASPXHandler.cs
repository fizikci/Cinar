﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Configuration;
using System.Collections;
using System.Web;
using System.Web.SessionState;
using System.Diagnostics;
using Cinar.CMS.Library.Modules;
using Cinar.Scripting;


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
            // TODO: aşağıdaki kod performansız, zaman içerisinde statik hale getirmek lazım.
            SortedDictionary<string, ArrayList> grups = new SortedDictionary<string, ArrayList>();
            foreach (Type type in Provider.GetModuleTypes())
            {
                ModuleInfoAttribute mInfo = (ModuleInfoAttribute)Utility.GetAttribute(type, typeof(ModuleInfoAttribute));
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
            // TODO: aşağıdaki kod performansız, zaman içerisinde statik hale getirmek lazım.
            ArrayList al = new ArrayList();
            foreach (Type type in Provider.GetEntityTypes())
            {
                ListFormPropsAttribute attr = (ListFormPropsAttribute)Utility.GetAttribute(type, typeof(ListFormPropsAttribute));
                //if (attr.VisibleAtMainMenu) 
                al.Add(String.Format("['{0}','{1}',{2}]", type.Name, Provider.GetResource(type.Name), Utility.ToJS(attr.VisibleAtMainMenu)));
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
                }
            }
        }

        public string HeadSection
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<meta name=\"description\" content=\"" + (Provider.Content != null ? Utility.HtmlEncode(Provider.Content.Description) + " " : "") + Utility.HtmlEncode(Provider.Configuration.SiteDescription) + "\"/>\n");
                sb.Append("<meta name=\"keywords\" content=\"" + (Provider.Content != null ? Utility.HtmlEncode(Provider.Content.Keywords) + " " + Utility.HtmlEncode(Provider.Content.Tags) + "," : "") + Utility.HtmlEncode(Provider.Configuration.SiteKeywords) + "\"/>\n");
                sb.Append("<META HTTP-EQUIV=\"Content-Type\" CONTENT=\"text/html; charset=utf-8\">\n");
                sb.Append("<META HTTP-EQUIV=\"Content-Language\" CONTENT=\"TR\">\n");
                if (Provider.Configuration.SiteIcon.Trim() != "")
                    sb.Append("<LINK href=\"http://" + Provider.Configuration.SiteAddress + "/" + Provider.Configuration.SiteIcon + "\" rel=\"SHORTCUT ICON\">\n");
                sb.Append("<LINK href=\"RSS.ashx?item=" + (Provider.Content == null ? 1 : Provider.Content.Id) + "\" rel=\"alternate\" title=\"" + Provider.Configuration.SiteName + "\" type=\"application/rss+xml\" />\n");

                if (Provider.DesignMode)
                    sb.Append("<style title=\"moduleStyles\">\n" + Provider.Configuration.DefaultStyleSheet + "\n" + Provider.ReadStyles(modules) + "\n</style>\n");
                else
                {
                    sb.Append("<link href=\"DefaultStyleSheet.ashx\" rel=\"stylesheet\" type=\"text/css\"/>\n");
                    sb.Append("<style title=\"moduleStyles\">\n" + Provider.ReadStyles(modules) + "\n</style>\n");
                }
                sb.Append("<link href=\"/external/default.css\" rel=\"stylesheet\" type=\"text/css\"/>\n");

                sb.AppendFormat("<script type='text/javascript'>var useHTMLEditor={0};</script>", Provider.Configuration.UseHTMLEditor.ToString().ToLower());
                sb.Append("<script type=\"text/javascript\" src=\"external/javascripts/prototype.js\"></script>\n");
                sb.Append("<script type=\"text/javascript\" src=\"external/default.js\"></script>\n");
                sb.Append("<script type=\"text/javascript\" src=\"external/message.js\"> </script>\n");
                if(Provider.Configuration.UseHTMLEditor)
                    sb.Append("<script type=\"text/javascript\">alert('FCK removed and there is no html editor yet!');</script>\n");
                sb.Append("<script type=\"text/javascript\" src=\"external/lang/" + Provider.CurrentCulture.Split('-')[0] + ".js\"></script>\n");
                //sb.Append("<script type=\"text/javascript\" src=\"external/javascripts/swfobject.js\"></script>\n");

                sb.Append("<script type=\"text/javascript\" src=\"external/javascripts/effects.js\"></script>\n");
                if (this.HasModule(typeof(ContentGallery)))
                {
                    sb.Append("<script type=\"text/javascript\" src=\"external/javascripts/lightbox.js\"> </script>\n");
                    sb.Append("<link rel=\"stylesheet\" href=\"external/javascripts/lightbox.css\" type=\"text/css\" media=\"screen\" />\n");
                }

                if (Provider.DesignMode)
                {
                    sb.Append("<link href=\"external/themes/default.css\" rel=\"stylesheet\" type=\"text/css\"/>\n");
                    sb.Append("<link href=\"external/themes/alphacube.css\" rel=\"stylesheet\" type=\"text/css\"/>\n");
                    //sb.Append("<link href=\"external/themes/spread.css\" rel=\"stylesheet\" type=\"text/css\"/>\n");
                    sb.Append("<link href=\"external/cinar.cms.css\" rel=\"stylesheet\" type=\"text/css\"/>\n");

                    sb.Append("<script type=\"text/javascript\" src=\"external/javascripts/window.js\"> </script>\n");
                    //sb.Append("<script type=\"text/javascript\" src=\"external/javascripts/dragdrop.js\"> </script>\n");
                    //sb.Append("<script type=\"text/javascript\" src=\"external/javascripts/rico.js\"> </script>\n");

                    sb.Append("<script type=\"text/javascript\" src=\"external/controls.js\"></script>\n");
                    sb.Append(@"
<script type='text/javascript'>
    Event.observe(document, 'contextmenu', function(e){Event.stop(e);}, false);
    Event.observe(document, 'mousedown', function(e){showPopupMenu(e);}, false);
    var currTemplate = '" + template.FileName + @"';
    var virtualDir = '" + WebConfigurationManager.AppSettings["virtualDir"] + @"';
    var moduleTypes = " + getModuleTypesJSON() + @";
    var entityTypes = " + getEntityTypesJSON() + @";
    var templates = " + getTemplatesJSON() + @";
</script>
                ");
                    sb.Append("<script type=\"text/javascript\" src=\"external/cinar.cms.js\"></script>\n");
                }

                if (!Provider.DesignMode)
                {
                        sb.Append(@"
<script type='text/javascript'>
    document.observe('dom:loaded', function(e){
        //if(!useHTMLEditor) return;
        $$('.editWithFCK').each(function(elm){
//            var oFCKeditor = new FCKeditor(elm.id);
//            oFCKeditor.BasePath = 'FCKEditor/';
//            oFCKeditor.Height = '300' ;
//            oFCKeditor.ReplaceTextarea();
        });
    }, false);
</script>
                ");

                }

                string title = Provider.Configuration.SiteName;
                if (Provider.Content != null && Provider.Content.Id != 1)
                    title += " - " + Provider.Content.Title;

                sb.Append("<title>" + Provider.Server.HtmlEncode(title) + "</title>");

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
                    if (module.Region == regionName && module.Template.ToLower() == template.FileName.ToLower())
                    {
                        Stopwatch stopWatch = new Stopwatch();
                        stopWatch.Start();
                        //sb.Append(module.Show());
                        Provider.Response.Write(module.Show());
                        stopWatch.Stop();
                        Provider.Response.Write(String.Format("<!-- {0} ms  ({1}) -->", stopWatch.ElapsedMilliseconds, module.CacheHint));
                        moduleCount++;
                    }

                if (moduleCount == 0 && Provider.DesignMode)
                    Provider.Response.Write(Provider.GetResource("Empty region") + ": " + regionName);

                return "";
            }
        }

        private Template template;


        public void ProcessRequest(HttpContext context)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            string path = context.Request.Url.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped);
            string fileName = path.Substring(path.LastIndexOf('/') + 1);

            template = (Template)Provider.Database.Read(typeof(Template), "FileName={0}", fileName);
            if (template == null)
                throw new Exception("Template doesn't exist: " + fileName);

            Provider.OnBeginRequest();

            Provider.Response.BufferOutput = Provider.Configuration.BufferOutput;

            string strExecutionTimes = "";
            strExecutionTimes = "<!-- Page read finished at " + stopWatch.ElapsedMilliseconds + " ms -->";

            if (Provider.DevelopmentMode)
            {
                Provider.Database.EnableSQLLog = true;
                Provider.Database.ClearSQLLog();
            }

            if (Provider.Request["currentCulture"] != null)
                Provider.CurrentCulture = Provider.Request["currentCulture"];

            Provider.SetHttpContextUser();
            if (Provider.User.IsInRole("Designer") && !String.IsNullOrEmpty(Provider.Request["DesignMode"]))
                Provider.DesignMode = Provider.Request["DesignMode"] == "On";

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
                    Provider.Response.Redirect(redirectPage, true);
                    return;
                }
                if (!Provider.ContextUser.IsInRole(ps.RoleToChange))
                {
                    Provider.DesignMode = false;
                }

            }
            #endregion

            strExecutionTimes += "<!-- All modules read finished at " + stopWatch.ElapsedMilliseconds + " ms -->";

            try
            {
                modules.ForEach(delegate(Module m) { m.beforeShow(); });
            }
            catch { }

            Provider.Response.Write(strExecutionTimes + "<!-- All modules beforeShow finished at " + stopWatch.ElapsedMilliseconds + " ms -->");

            #region BScript and attributes
            string pageContent = template.HTMLCode;//.Replace("!#","$=").Replace("#!","$");
            Interpreter engine = Provider.GetInterpreter(pageContent, this);
            engine.Parse();
            engine.Execute(Provider.Response.Output);
            //pageContent = engine.Output;
            #endregion

            //Provider.Response.Write(pageContent);

            Provider.Response.Write("<!-- Page output finished at " + stopWatch.ElapsedMilliseconds + " ms -->");

            try
            {
                modules.ForEach(delegate(Module m) { m.afterShow(); });
            }
            catch { }

            Provider.Response.Write("<!-- All modules afterShow finished at " + stopWatch.ElapsedMilliseconds + " ms -->");

            // bu hiti kaydedelim
            //if (Provider.Request.Browser!=null && !Provider.Request.Browser.Crawler)
            //    new Hit().Save();

            stopWatch.Stop();
            Provider.Response.Write("<!-- TOTAL: " + stopWatch.ElapsedMilliseconds + " ms -->");

            Provider.Database.Connection.Close();
        }
    }
}