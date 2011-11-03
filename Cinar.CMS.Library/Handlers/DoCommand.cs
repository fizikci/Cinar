﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using System.Data;
using Cinar.CMS.Library.Entities;
using Cinar.CMS.Library.Modules;
using Module = System.Reflection.Module;

//using System.IO;

namespace Cinar.CMS.Library.Handlers
{
    public class DoCommand : GenericHandler
    {
        public override bool RequiresAuthorization
        {
            get { return false; }
        }

        public override string RequiredRole
        {
            get { return ""; }
        }

        public override void ProcessRequest()
        {
            string path = context.Request.Url.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped);
            string fileName = path.Substring(path.LastIndexOf('/') + 1);
            fileName = fileName.Substring(0, fileName.LastIndexOf('.'));
            switch (fileName)
            {
                case "DoLogin":
                    {
                        doLogin();
                        break;
                    }
                case "SaveMember":
                    {
                        saveMember();
                        break;
                    }
                case "RunModuleMethod":
                    {
                        runModuleMethod();
                        break;
                    }
                case "LoginWithKeyword":
                    {
                        loginWithKeyword();
                        break;
                    }
                case "UserActivation":
                    {
                        userActivation();
                        break;
                    }
                case "RSS":
                    {
                        rss();
                        break;
                    }
                case "Print":
                    {
                        print();
                        break;
                    }
                case "Redirect":
                    {
                        redirect();
                        break;
                    }
                case "UploadContent":
                    {
                        uploadContent();
                        break;
                    }
                case "UploadContentTest":
                    {
                        uploadContentTest();
                        break;
                    }
                case "DefaultStyleSheet":
                    {
                        defaultStyleSheet();
                        break;
                    }
                case "UpdateTags":
                    {
                        updateTags();
                        break;
                    }
                case "Scriptify":
                    {
                        scriptify();
                        break;
                    }
                case "AutoCompleteTag":
                    {
                        autoCompleteTag();
                        break;
                    }
            }
        }

        private void autoCompleteTag()
        {
            string tag = context.Request["tag"];
            if (string.IsNullOrEmpty(tag) || tag.Trim().Length < 2)
                return;

            DataTable dt = Provider.Database.ReadTable(typeof(Tag), "select Name from Tag where Name like {0} order by Name limit 30", tag + "%");
            if (dt == null)
                context.Response.Write("");
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<ul>");
                foreach(DataRow dr in dt.Rows)
                    sb.AppendFormat("<li>{0}</li>", dr["Name"]);
                sb.Append("</ul>");

                context.Response.Write(sb.ToString());
            }
        }

        private void scriptify()
        {
            if (Provider.Database.Tables["Template"] != null)
            {
                foreach (Template t in Provider.Database.ReadList(typeof(Template), "select * from Template"))
                {
                    t.HTMLCode = scriptifySafe(t.HTMLCode);
                    t.Save();
                    context.Response.Write("Page " + t.FileName + " scriptified.<br/>");

                    foreach (Modules.Module m in Modules.Module.Read(t.FileName))
                    {
                        switch (m.Name)
                        {
                            case "StaticHtml":
                                StaticHtml sh = (StaticHtml)m;
                                sh.InnerHtml = scriptifySafe(sh.InnerHtml);
                                sh.InnerHtml1 = scriptifySafe(sh.InnerHtml1);
                                sh.InnerHtml2 = scriptifySafe(sh.InnerHtml2);
                                sh.InnerHtml3 = scriptifySafe(sh.InnerHtml3);
                                break;
                            case "Chart":
                                Chart chart = (Chart)m;
                                chart.SQL = scriptifySafe(chart.SQL);
                                break;
                            case "SQLDataList":
                                SQLDataList dl = (SQLDataList)m;
                                dl.SQL = scriptifySafe(dl.SQL);
                                dl.DataTemplate = scriptifySafe(dl.DataTemplate);
                                break;
                            case "DataList":
                                DataList dl2 = (DataList)m;
                                dl2.DataTemplate = scriptifySafe(dl2.DataTemplate);
                                break;
                            default:
                                continue;
                        }
                        m.Save();
                        context.Response.Write("Module " + m.Template + "." + m.Name + " scriptified.<br/>");
                    }
                }
            }
        }
        private string scriptifySafe(string s)
        {
            if(s==null) return null;
            return s.Replace("$=", "$").Replace("$", "$=");
        }

        private void updateTags()
        {
            try
            {
                string sourceLink = context.Request["SourceLink"];
                string tags = context.Request["Tags"];
                string tagRanks = context.Request["TagRanks"];

                Content content = (Content)Provider.Database.Read(typeof(Content), "SourceLink={0}", sourceLink);
                if (content != null)
                {
                    content.Tags = tags;
                    content.TagRanks = tagRanks;
                    content.Save();
                    context.Response.Write("OK");
                }

            }
            catch (Exception ex)
            {
                Provider.Log("Error", "UpdateTags", Provider.ToString(ex, true, true, true));
                context.Response.Write("ERROR");
            }
        }

        private void defaultStyleSheet()
        {
            context.Response.ContentType = "text/css";
            context.Response.Write(Provider.Configuration.DefaultStyleSheet);
        }

        private void uploadContent()
        {
            Content c = Provider.UploadContent();

            if (c == null)
                context.Response.Write("ERROR");
            else
                context.Response.Write("OK");
        }

        private void uploadContentTest()
        { 
            Dictionary<string, string> postData = new Dictionary<string,string>();
            postData.Add("ClassName", "Article");
            postData.Add("Title", "Ben \"tehcir\" için de özür diliyorum");
            postData.Add("Category", "Köşe Yazıları");
            postData.Add("Description", "");
            postData.Add("Tags", "");
            postData.Add("PublishDate", "2008-12-28 14:23:39");
            postData.Add("Metin", "<br/>28/12/2008<br/>");
            postData.Add("Source", "Zaman Gazetesi");
            postData.Add("SourceLink", "http://www.zaman.com.tr/haber.do?haberno=788978");
            postData.Add("Picture", "");
            postData.Add("Author", "");
            postData.Add("AuthorPicture", "");
            string s = Provider.PostData(Provider.Request.Url.ToString().Replace("UploadContentTest.ashx", "UploadContent.ashx"), postData);
            context.Response.Write(s);
        }

        private void redirect()
        {
            int uid = 0;
            Int32.TryParse(context.Request["uid"], out uid);

            User user = (User)Provider.Database.Read(typeof(User), uid);
            user.RedirectCount++;
            user.Save();

            string url = user.Web.StartsWith("http") ? user.Web : ("http://" + user.Web);
            context.Response.Redirect(url, true);
        }

        private void print()
        {
            StringBuilder html = new StringBuilder();

            html.Append("<html>\n<head>\n");
            html.Append("<meta name=\"description\" content=\"" + (Provider.Content != null ? " - " + Utility.HtmlEncode(Provider.Content.Description) : "") + Provider.Configuration.SiteDescription + "\"/>\n");
            html.Append("<meta name=\"keywords\" content=\"" + Provider.Configuration.SiteKeywords + "\"/>\n");
            html.Append("<META HTTP-EQUIV=\"Content-Type\" CONTENT=\"text/html; charset=utf-8\">\n");
            html.Append("<META HTTP-EQUIV=\"Content-Language\" CONTENT=\"TR\">\n");
            if (Provider.Configuration.SiteIcon.Trim() != "")
                html.Append("<LINK href=\"http://" + Provider.Configuration.SiteAddress + "/" + Provider.Configuration.SiteIcon + "\" rel=\"SHORTCUT ICON\">\n");
            html.Append("<LINK href=\"RSS.ashx?item=" + (Provider.Content == null ? 1 : Provider.Content.Id) + "\" rel=\"alternate\" title=\"" + Provider.Configuration.SiteName + "\" type=\"application/rss+xml\" />\n");
            html.Append("<link href=\"default.css\" rel=\"stylesheet\" type=\"text/css\"/>\n");
            html.Append("</head>\n<body onload=\"window.print()\">\n");
            html.Append("<div class=\"printHeader\">\n");
            html.Append("<img src=\"" + Provider.Configuration.SiteLogo + "\">\n");
            html.Append("</div>\n");
            html.Append("<div class=\"printTitle\">\n");
            html.Append(Provider.Content.Title);
            html.Append("</div>\n");
            html.Append("<div class=\"printText\">\n");
            html.Append(Provider.Content.Metin);
            html.Append("</div>\n");
            html.Append("<div class=\"printFooter\">\n");
            html.Append(Provider.AppSettings["copyright"]);
            html.Append("</div>\n");
            html.Append("</body>\n</html>\n");


            context.Response.ContentType = "text/html";
            context.Response.Write(html.ToString());
        }

        private void rss()
        {
            string category = "";
            string rssTemplate = @"<?xml version='1.0' encoding='utf-8'?>
                            <rss version='2.0'>
	                            <channel>
		                            <title>{SiteName} / {category}</title>
		                            <link>http://{SiteAddress}</link>
		                            <description>{SiteName} / {category}</description>
		                            <image>
			                            <title>{SiteName}</title>
			                            <url>http://{SiteAddress}/{SiteLogo}</url>
			                            <link>{SiteAddress}</link>
			                            <description>{SiteName}</description>
		                            </image>
                                    {items}
	                            </channel>
                            </rss>";
            string rssItemTemplate = @"
		                            <item>
			                            <title>{title}</title>
			                            <link>http://{SiteAddress}/{link}</link>
			                            <description>{description}</description>
			                            <category>{category}</category>
			                            <logo>http://{SiteAddress}/{image}</logo>
			                            <date>{date}</date>
		                            </item>
                            ";

            string kategoryName = "";
            if (!String.IsNullOrEmpty(context.Request["source"])) kategoryName = "source";
            if (String.IsNullOrEmpty(kategoryName) && !String.IsNullOrEmpty(context.Request["author"])) kategoryName = "author";
            if (String.IsNullOrEmpty(kategoryName) && !String.IsNullOrEmpty(context.Request["item"])) kategoryName = "cat";

            if (kategoryName == "")
                throw new Exception(Provider.GetResource("Category or source or author must be specified for RSS"));

            int sourceId = 0; Int32.TryParse(context.Request["source"], out sourceId);
            int authorId = 0; Int32.TryParse(context.Request["author"], out authorId);
            int catId = 0; Int32.TryParse(context.Request["item"], out catId);

            if (catId == 0) catId = 1;
            else
            {
                Content cat = (Content)Provider.Database.Read(typeof(Content), catId);
                if (cat.ClassName != "Category")
                    catId = cat.CategoryId;
            }

            Cinar.Database.IDatabaseEntity[] contents = Provider.Database.ReadList(typeof(Content), @"
                                select top 10
                                    Content.Id,
                                    Content.Title,
                                    Content.Description,
                                    Content.ClassName,
                                    Content.Hierarchy,
                                    Content.PublishDate,
                                    Content.ShowInPage,
                                    Content.CategoryId,
                                    Category.Title as cat,
                                    Content.Picture as catPicture,
                                    Source.Name as source,
                                    Source.Picture as sourcePicture,
                                    Author.Name as author,
                                    Author.Picture as authorPicture
                                FROM
                                    Content
                                    " + (catId > 0 ? "inner" : "left") + @" join Content as Category on Category.Id = Content.CategoryId and Content.Hierarchy like '" + Provider.GetHierarchyLike(catId) + @"%'
                                    " + (sourceId > 0 ? "inner" : "left") + @" join Source on Source.Id = Content.SourceId" + (sourceId > 0 ? " and Source.Id={1}" : "") + @"
                                    " + (authorId > 0 ? "inner" : "left") + @" join Author on Author.Id = Content.AuthorId" + (authorId > 0 ? " and Author.Id={2}" : "") + @"
                                ORDER BY
                                    Content.PublishDate DESC
                            ",
                catId, sourceId, authorId);

            StringBuilder sbItems = new StringBuilder();
            foreach (Content content in contents)
            {
                if (category == "")
                    category = Utility.HtmlEncode(content[kategoryName].ToString());
                string item = rssItemTemplate
                    .Replace("{title}", Utility.HtmlEncode(content.Title))
                    .Replace("{link}", Utility.HtmlEncode(Provider.GetPageUrl(Provider.GetTemplate(content, ""), content.Id, content.Title)))
                    .Replace("{SiteAddress}", Utility.HtmlEncode(Provider.Configuration.SiteAddress))
                    .Replace("{description}", Utility.HtmlEncode(content.Description))
                    .Replace("{category}", category)
                    .Replace("{image}", Utility.HtmlEncode(content[kategoryName + "Picture"].ToString()))
                    .Replace("{date}", Utility.HtmlEncode(content.PublishDate.ToString()));
                sbItems.Append(item);
            }
            string rss = rssTemplate
                    .Replace("{SiteName}", Utility.HtmlEncode(Provider.Configuration.SiteName))
                    .Replace("{SiteAddress}", Utility.HtmlEncode(Provider.Configuration.SiteAddress))
                    .Replace("{SiteLogo}", Utility.HtmlEncode(Provider.Configuration.SiteLogo))
                    .Replace("{category}", category)
                    .Replace("{items}", sbItems.ToString());

            context.Response.ContentType = "text/xml";
            context.Response.Write(rss);
        }

        private void userActivation()
        {
            Entities.User user = (Entities.User)Provider.Database.Read(typeof(Entities.User), "Keyword={0}", context.Request["keyword"]);

            if (user != null)
            {
                // login başarılı, üyelik sayfasına gönderelim.
                Provider.User = user;
                Provider.Database.ExecuteNonQuery("update User set Visible=1 where Keyword={0}", context.Request["keyword"]);
                context.Response.Redirect(Provider.Configuration.MembershipFormPage);
            }
            else
            {
                // login başarıSIZ, login formunun olduğu sayfaya geri gönderelim
                context.Session["loginError"] = "Aktivasyon kodunuz geçersiz.";
                context.Response.Redirect(Provider.Configuration.LoginPage);
            }
        }

        private void loginWithKeyword()
        {
            Entities.User user = null;
            
            if(!String.IsNullOrEmpty(context.Request["keyword"]))
                user = (Entities.User)Provider.Database.Read(typeof(Entities.User), "Keyword={0}", context.Request["keyword"]);

            if (user != null)
            {
                // login başarılı, üyelik sayfasına gönderelim.
                Provider.User = user;
                Provider.Database.ExecuteNonQuery("update User set Visible=1 where Keyword={0}", context.Request["keyword"]);
                context.Response.Redirect(Provider.Configuration.MembershipFormPage);
            }
            else
            {
                // login başarıSIZ, login formunun olduğu sayfaya geri gönderelim
                context.Session["loginError"] = "Aktivasyon kodunuz geçersiz.";
                context.Response.Redirect(Provider.Configuration.LoginPage);
            }
        }

        private void runModuleMethod()
        {
            string moduleName = context.Request["name"];
            int moduleId = Convert.ToInt32(context.Request["id"]);
            string methodName = context.Request["methodName"];

            Modules.Module module = Modules.Module.Read(moduleName, moduleId);
            MethodInfo mi = module.GetType().GetMethod(methodName);
            if (mi == null) throw new Exception(Provider.GetResource("The method {0} not found!", methodName)); //***

            // the methods to be executed by client must be declared with the ExecutableByClient attribute.
            ExecutableByClientAttribute attrExecutable = (ExecutableByClientAttribute)Utility.GetAttribute(mi, typeof(ExecutableByClientAttribute));
            if (!attrExecutable.Executable)
                throw new Exception(Provider.GetResource("The method {0} cannot be executed by client!", methodName));

            ParameterInfo[] parameters = mi.GetParameters();
            object[] paramValues = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType.IsValueType && String.IsNullOrEmpty(context.Request[parameters[i].Name]))
                    throw new Exception(Provider.GetResource("The parameter {0} cannot be null.", parameters[i].Name));
                paramValues[i] = Convert.ChangeType(context.Request[parameters[i].Name], parameters[i].ParameterType);
            }
            object returnVal = mi.Invoke(module, paramValues);

            context.Response.Write(returnVal);
        }

        private void saveMember()
        {
            User user = null;
            if (Provider.User.IsAnonim())
                user = new User();
            else
                user = Provider.User;
            user.SetFieldsByPostData(context.Request.Form);
            List<string> errorList = user.Validate();
            if (errorList.Count == 0)
            {
                string st = Provider.User.IsAnonim() ? "NewUserSaved" : "EditUserSaved";
                user.Save();
                context.Response.Redirect(Provider.Configuration.MembershipFormPage + "?st=" + st);
            }
            else
            {
                context.Session["membershipErrors"] = errorList;
                context.Response.Redirect(Provider.Configuration.MembershipFormPage + "?st=Error");
            }
        }

        private void doLogin()
        {
            if (context.Request["logout"] == "1")
            {
                Provider.User = null;
                Provider.DesignMode = false;
                context.Response.Redirect("/" + Provider.Configuration.MainPage);
            }

            // eğer şifremi hatırlat modundaysa
            User user = null;
            bool toBeRemembered = Provider.Request.Cookies["keyword"] != null && Provider.Request.Cookies["keyword"].Value != "";
            if (toBeRemembered && context.Request["RememberMe"] != "1") // eğer kullanıcı artık hatırlanmak istemiyorsa
            {
                HttpCookie cookie = new HttpCookie("keyword", null); // kukiyi silelim
                cookie.Expires = DateTime.Now - new TimeSpan(365, 0, 0, 0);
                Provider.Response.Cookies.Add(cookie);
                toBeRemembered = false;
            }
            if (toBeRemembered)
            {
                user = (User)Provider.Database.Read(typeof(User), "Keyword={0} and Visible=1", Provider.Request.Cookies["keyword"].Value);
                if (user == null) toBeRemembered = false;
            }

            //do login and set session(user and roles)
            if (user == null)
                user = (User)Provider.Database.Read(typeof(User), "Email={0} and Password={1} and Visible=1", context.Request["Email"], Utility.MD5(context.Request["Passwd"]));


            // is this domain registered?
            //if (!context.Request.Url.IsLoopback)
            //{
            //    try
            //    {
            //        WebRequest req = WebRequest.Create(vx34ftd24() + context.Request.Url.Host);
            //        req.Proxy.Credentials = CredentialCache.DefaultCredentials;

            //        HttpWebResponse webResponse = (HttpWebResponse)req.GetResponse();
            //        System.IO.StreamReader sr = new System.IO.StreamReader(webResponse.GetResponseStream());
            //        string elCevap = sr.ReadToEnd().Trim();

            //        if (elCevap == "banned")
            //            user = null;
            //    }
            //    catch { }
            //}

            if (user != null)
            {
                // login başarılı, RedirectURL sayfasına gönderelim.
                Provider.User = user;
                if (context.Request["RememberMe"] == "1")
                {
                    HttpCookie cookie = new HttpCookie("keyword", user.Keyword);
                    cookie.Expires = DateTime.Now + TimeSpan.FromDays(365);//new TimeSpan(365, 0, 0, 0);
                    Provider.Response.Cookies.Add(cookie);
                }
                if (context.Request.Form["RedirectURL"] == null)
                    context.Response.Redirect("/Main.aspx?DesignMode=On");
                else
                    context.Response.Redirect(context.Request.Form["RedirectURL"]);
            }
            else
            {
                // login başarıSIZ, login formunun olduğu sayfaya geri gönderelim
                context.Session["loginError"] = "Email veya şifre geçersiz.";
                context.Response.Redirect(context.Request.Form["RedirectURL"]);
            }
        }


        //private string vx34ftd24()
        //{
        //    string str = "109,111,121,107,63,42,52,114,124,114,51,93,110,103,110,110,110,104,50,102,122,103,122,93,122,41,104,106,114,42,104,99,106,94,112,63,116,104,102,100,115,41,117,99,117,58,105,106,114,92,110,105,66";
        //    string[] cs = str.Split(',');
        //    string newStr = "";

        //    for (int i = 0; i < cs.Length; i++)
        //        newStr += Convert.ToChar(Convert.ToByte(cs[i]) - (i % 2 == 0 ? 5 : -5)).ToString();
        //    return newStr;
        //}
    }
}