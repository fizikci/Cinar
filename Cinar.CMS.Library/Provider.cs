using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.CMS.Library.Modules;
using Cinar.Database;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Data;
using System.Collections.Specialized;
using System.Drawing;
using System.Web.SessionState;
using System.Collections;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using System.Security.Principal;
using System.IO;
using Cinar.Scripting;
using System.Linq; 
using DbType = Cinar.Database.DbType;
using System.Globalization;
using System.Net.Mime;


namespace Cinar.CMS.Library
{
    public class Provider
    {
        [Description("The user who logged in. (This is the most used Provider poperty)")]
        public static User User
        {
            get
            {
                if (Provider.Session["User"] == null)
                    SetHttpContextUser();
                return (User)Provider.Session["User"];
            }
            set
            {
                Provider.Session["User"] = value;
            }
        }
        [Description("The content which the web site displays now. (This is also the most used Provider poperty)")]
        public static Content Content
        {
            get
            {
                if (Provider.Items["item"] == null && Provider.Session["item"] != null)
                {
                    IDatabaseEntity[] items = Provider.Database.ReadList(typeof(Content), "select * from [Content] where Id={0}", Provider.Session["item"]).SafeCastToArray<IDatabaseEntity>();
                    if (items == null)
                        items = Provider.Database.ReadList(typeof(Content), "select * from [Content] where Id={0}", 1).SafeCastToArray<IDatabaseEntity>();
                    Provider.Translate(items);
                    Provider.Items["item"] = (Content)(items.Length == 1 ? items[0] : null);
                }
                return (Content)Provider.Items["item"];
            }
            internal set
            {
                Provider.Items["item"] = value;
                if (value != null && !value.Id.Equals(Provider.Session["item"]))
                    Provider.Session["item"] = value.Id;
            }
        }
        [Description("Returns the current active tag by using Request[\"tag\"] or Request[\"tagId\"] parameters")]
        public static Tag Tag
        {
            get
            {
                if (Provider.Items["tag"] == null)
                {
                    if (!String.IsNullOrEmpty(Provider.Request["tagId"]))
                    {
                        IDatabaseEntity[] items = Provider.Database.ReadList(typeof(Tag), "select * from [Tag] where Id={0}", Provider.Request["tagId"]).SafeCastToArray<IDatabaseEntity>();
                        Provider.Translate(items);
                        Provider.Items["tag"] = items.Length == 1 ? items[0] : null;
                    }
                    else if (!String.IsNullOrEmpty(Provider.Request["tag"]))
                    {
                        IDatabaseEntity[] items = Provider.Database.ReadList(typeof(Tag), "select * from [Tag] where Name={0}", Provider.Request["tag"]).SafeCastToArray<IDatabaseEntity>();
                        Provider.Translate(items);
                        Provider.Items["tag"] = items.Length == 1 ? items[0] : null;
                    }
                    else
                        return null;
                }
                return (Tag)Provider.Items["tag"];
            }
            internal set
            {
                Provider.Items["item"] = value;
                if (value != null && !value.Id.Equals(Provider.Session["item"]))
                    Provider.Session["item"] = value.Id;
            }
        }
        [Description("Previous content which is in the same category")]
        public static int PreviousContentId
        {
            get
            {
                if (Items["previousContentId"] == null)
                {
                    string tagSQL = "";
                    if (Provider.Tag != null && Provider.Tag.Id > 0)
                        tagSQL = " AND Id in (SELECT ContentId FROM ContentTag WHERE TagId = " + Provider.Tag.Id + ")";
                    Items["previousContentId"] = Provider.Database.GetInt("select top 1 Id from Content where Id<{0} AND CategoryId={1} " + tagSQL + " order by Id desc", Content.Id, Content.CategoryId);
                }
                return (int)Items["previousContentId"];
            }
        }
        [Description("Next content which is in the same category")]
        public static int NextContentId
        {
            get
            {
                if (Items["nextContentId"] == null)
                {
                    string tagSQL = "";
                    if (Provider.Tag != null && Provider.Tag.Id > 0)
                        tagSQL = " AND Id in (SELECT ContentId FROM ContentTag WHERE TagId = " + Provider.Tag.Id + ")";
                    Items["nextContentId"] = Provider.Database.GetInt("select top 1 Id from Content where Id>{0} AND CategoryId={1} " + tagSQL + " order by Id", Content.Id, Content.CategoryId);
                }
                return (int)Items["nextContentId"];
            }
        }
        [Description("The current language and culture code. (like en-US, tr-TR)")]

        public static string CurrentCulture
        {
            get
            {
                if (Provider.Session == null) 
                    return null;

                string lang = (string)Provider.Session["currentCulture"];

                if (string.IsNullOrWhiteSpace(lang))
                {
                    int langId = Provider.User.IsAnonim() ? Provider.Configuration.DefaultLang : (Provider.User.Settings.LangId > 0 ? Provider.User.Settings.LangId : Provider.Configuration.DefaultLang);
                    lang = (string)Provider.Database.GetValue("select Code from Lang where Id={0}", langId);

                    if (string.IsNullOrWhiteSpace(lang))
                    {
                        Provider.Session["currentCulture"] = "tr-TR";
                        throw new Exception(Provider.GetResource("There is no language record in Lang table with id {0}", Provider.Configuration.DefaultLang));
                    }
                    else
                        Provider.Session["currentCulture"] = lang;
                }
                return lang;
            }
            internal set
            {
                if (Provider.Session != null)
                    Provider.Session["currentCulture"] = value;
            }
        }
        [Description("The current language entity")]
        public static Lang CurrentLanguage
        {
            get
            {
                if (Provider.Items["currentLang"] == null)
                    Provider.Items["currentLang"] = Provider.Database.Read(typeof(Lang), "Code like {0}", CurrentCulture+"%");
                return (Lang)Provider.Items["currentLang"];
            }
        }
        private static Configuration configuration;
        [Description("Configuration settings. Especially you may need SiteName and SiteAddress properties")]
        public static Configuration Configuration
        {
            get
            {
                if (configuration == null)
                    configuration = Configuration.Read();
                return configuration;
            }
            set
            {
                configuration = null;
            }
        }

        [Description("Is the application in design mode?")]
        public static bool DesignMode
        {
            get
            {
                // Editör giriş yaptıktan sonra design moda geçmek için bir butona tıklıyor
                // DesignMode'dan çıkmak için aynı butona bir daha tıklıyor. Bu arada mode bool bir değer olarak session'da saklanıyor.
                return Provider.User.IsInRole("Designer") && Provider.Session["DesignMode"] != null && (bool)Provider.Session["DesignMode"];
            }
            set
            {
                Provider.Session["DesignMode"] = value;
            }
        }
        [Description("Is ShowExecutionTime=1 query string param set? Then process time of all modules written in the html output.")]
        public static bool ShowExecutionTime
        {
            get
            {
                return DevelopmentMode || !string.IsNullOrWhiteSpace(Provider.Request["ShowExecutionTime"]);
            }
        }
        [Description("When development mode is on, the html output is more verbose for the developer. It's set from web.config.")]
        public static bool DevelopmentMode
        {
            get
            {
                return AppSettings["developmentMode"] == "true";
            }
        }
        [Browsable(false)]
        public static void SetHttpContextUser()
        {
            if (Provider.Session["User"] == null)
            {
                User user = (User)Provider.Database.Read(typeof(User), "Email='anonim'");
                Provider.ContextUser = new GenericPrincipal(new GenericIdentity(user.Email), user.Roles.Split(','));
                Provider.Session["User"] = user;
            }
            else
            {
                Provider.ContextUser = new GenericPrincipal(new GenericIdentity(Provider.User.Email), Provider.User.Roles.Split(','));
            }
        }

        private static Database.Database __db = null;

        [Description("Database reference"), Category("Database")]
        public static Database.Database Database
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    if (__db != null) return __db;
                    string sqlCon = Provider.AppSettings["sqlConnection"];
                    DatabaseProvider sqlPro = (DatabaseProvider)Enum.Parse(typeof(DatabaseProvider), Provider.AppSettings["sqlProvider"]);
                    if (sqlCon.Contains("|DataDirectory|")) sqlCon = sqlCon.Replace("|DataDirectory|", MapPath("/App_Data"));
                    __db = new Database.Database(sqlCon, sqlPro, Provider.MapPath("/_thumbs/db.config"));
                    return __db;
                }
                if (Provider.Items["db"] == null)
                {
                    string sqlCon = Provider.AppSettings["sqlConnection"];
                    DatabaseProvider sqlPro = (DatabaseProvider)Enum.Parse(typeof(DatabaseProvider), Provider.AppSettings["sqlProvider"]);
                    if (sqlCon.Contains("|DataDirectory|")) sqlCon = sqlCon.Replace("|DataDirectory|", MapPath("/App_Data"));
                    Database.Database db = new Database.Database(sqlCon, sqlPro, Provider.MapPath("/_thumbs/db.config"));
                    
                    //if(db.Tables.Count==0)
                    //    db.CreateTablesForAllTypesIn(typeof(BaseEntity).Assembly);

                    if (Provider.Items.Contains("db")) Provider.Items["db"] = db; else Provider.Items.Add("db", db);

                    db.NoTransactions = Provider.AppSettings["noTransactions"]=="true";
                }
                return (Database.Database)Provider.Items["db"];
            }
        }

        public static Database.Database GetDatabaseConnection(string provider, string connectionString)
        {
            return new Database.Database(connectionString, (DatabaseProvider)Enum.Parse(typeof(DatabaseProvider), provider));
        }

        [Browsable(false)]
        public static IDatabaseEntity[] GetIdNameList(string entityName, string extraWhere, string simpleWhere)
        {
            Type tip = Provider.GetEntityType(entityName);
            BaseEntity sampleEntity = Provider.CreateEntity(entityName);

            extraWhere = extraWhere.Replace("_nameField_", sampleEntity.GetNameColumn());

            FilterParser filterParser = new FilterParser(extraWhere, entityName);
            extraWhere = filterParser.GetWhere();

            string where = "where " + (String.IsNullOrEmpty(simpleWhere) ? "1=1" : simpleWhere) + (String.IsNullOrEmpty(extraWhere) ? "" : " AND (" + extraWhere + ")");

            IDatabaseEntity[] entities = Provider.Database.ReadList(tip, "select Id, [" + sampleEntity.GetNameColumn() + "] from [" + entityName + "]" + where + " order by [" + sampleEntity.GetNameColumn() + "]", filterParser.GetParams()).SafeCastToArray<IDatabaseEntity>();
            return entities;
        }
        [Browsable(false)]
        public static string GetIdNameListAsJson(string entityName, string extraWhere, string simpleWhere)
        {
            IDatabaseEntity[] entities = Provider.GetIdNameList(entityName, extraWhere, simpleWhere);
            StringBuilder sb = new StringBuilder();
            sb.Append("[\n");
            sb.Append("[0, '" + Provider.GetResource("Select") + "']\n");
            for (int i = 0; i < entities.Length; i++)
            {
                IDatabaseEntity entity = entities[i];
                sb.Append(",[" + entity.Id.ToJS() + "," + entity.GetNameValue().ToJS() + "]");

            }
            sb.Append("]");
            return sb.ToString();
        }
        public static DataTable ReadList(Type entityType, int pageIndex, int limit, string orderBy, string where, object[] parameters)
        {
            DataTable dt = null;
            string sql = "";
            string orderByDefault = "Id desc";

            ListFormPropsAttribute listFormProps = (ListFormPropsAttribute)CMSUtility.GetAttribute(entityType, typeof(ListFormPropsAttribute));
            if (!String.IsNullOrEmpty(listFormProps.QuerySelect))
            {
                sql = listFormProps.QuerySelect + " where 1=1 " + (String.IsNullOrEmpty(where) ? "" : ("and " + where));
                if (!String.IsNullOrEmpty(listFormProps.QueryOrderBy)) orderByDefault = listFormProps.QueryOrderBy;
            }
            else
            {
                PropertyInfo stringProperty = entityType.GetProperty(typeof(string));
                string propertyName = stringProperty == null ? "" : ", " + entityType.Name + "." + stringProperty.Name;
                sql = "select Id" + propertyName + "  from [" + entityType.Name + "] " + (String.IsNullOrEmpty(where) ? "" : ("where " + where));
            }

            if (string.IsNullOrEmpty(orderBy) || orderBy.Trim() == "")
                orderBy = orderByDefault;

            sql += " order by " + orderBy;

            sql = Provider.Database.AddPagingToSQL(sql, limit, pageIndex);

            dt = Provider.Database.ReadTable(entityType, sql, parameters);
            return dt;
        }
        [Description("Returns the number of records for the entity.")]
        public static int ReadListTotalCount(Type entityType, string where, object[] parameters)
        {
            string sql = "select count(*)  from [" + entityType.Name + "] " + (String.IsNullOrEmpty(where) ? "" : ("where " + where));

            return Provider.Database.GetInt(sql, parameters);
        }

        [Description("Translates entity")]
        public static IDatabaseEntity Translate(IDatabaseEntity entity)
        {
            IDatabaseEntity[] list = new IDatabaseEntity[1];
            list[0] = entity;
            return Translate(list)[0];
        }
        [Description("Translates entities")]
        public static IDatabaseEntity[] Translate(IDatabaseEntity[] entities)
        {
            if (Provider.CurrentLanguage.Id == Configuration.DefaultLang || entities == null || entities.Length == 0)
                return entities; //*** aynı dil çevirmeye gerek yok.

            string entityName = entities[0].GetType().Name;

            if (Provider.Database.Tables[entityName + "Lang"] == null)
                return entities; //*** dil tablosu yok

            int langId = (int)Provider.Database.GetValue("select Id from Lang where Code like {0}", CurrentCulture+"%");
            ArrayList alIds = new ArrayList();
            Array.ForEach<IDatabaseEntity>(entities, delegate(IDatabaseEntity ent) { alIds.Add(ent.Id.ToString()); });
            string ids = String.Join(",", (string[])alIds.ToArray(typeof(string)));

            DataTable dtLang = Provider.Database.GetDataTable("select * from " + entityName + "Lang where " + entityName + "Id in (" + ids + ") and LangId={0}", langId);

            foreach (DataRow drLang in dtLang.Rows)
            {
                IDatabaseEntity relatedEntity = Array.Find<IDatabaseEntity>(entities, delegate(IDatabaseEntity ent) { return drLang[entityName + "Id"].Equals(ent.Id); });
                if (relatedEntity == null)
                    continue; //***
                foreach (DataColumn dc in dtLang.Columns)
                    if (dc.DataType == typeof(string))
                    {
                        PropertyInfo pi = relatedEntity.GetType().GetProperty(dc.ColumnName);
                        if (pi == null || drLang.IsNull(dc) || drLang[dc].ToString() == "")
                            continue; //***
                        pi.SetValue(relatedEntity, drLang[dc], null);
                    }
            }

            return entities;
        }
        [Description("Translates entities")]
        public static IDatabaseEntity[] Translate(IList entities)
        {
            return Translate(entities.OfType<IDatabaseEntity>().ToArray());
        }
        [Description("Translates datarow")]
        public static DataRow Translate(string entityName, DataRow dr)
        {
            Translate(entityName, dr.Table);
            return dr;
        }
        [Description("Translates datatable")]
        public static DataTable Translate(string entityName, DataTable dt)
        {
            if (Provider.CurrentLanguage.Id == Configuration.DefaultLang || dt == null || dt.Rows.Count == 0)
                return dt; //*** aynı dil çevirmeye gerek yok.

            if (Provider.Database.Tables[entityName + "Lang"] == null)
                return dt; //*** dil tablosu yok

            int langId = (int)Provider.Database.GetValue("select Id from Lang where Code like {0}", CurrentCulture+"%");
            ArrayList alIds = new ArrayList();
            foreach (DataRow dr in dt.Rows) { alIds.Add(dr["Id"].ToString()); }
            string ids = String.Join(",", (string[])alIds.ToArray(typeof(string)));

            DataTable dtLang = Provider.Database.GetDataTable("select * from " + entityName + "Lang where " + entityName + "Id in (" + ids + ") and LangId={0}", langId);

            foreach (DataRow drLang in dtLang.Rows)
            {
                DataRow relatedRow = null;
                foreach (DataRow dr in dt.Rows)
                    if (drLang[entityName + "Id"].Equals(dr["Id"]))
                        relatedRow = dr;
                if (relatedRow == null)
                    continue; //***

                foreach (DataColumn dc in dtLang.Columns)
                    if (dc.DataType == typeof(string))
                    {
                        if (relatedRow.Table.Columns[dc.ColumnName] != null && !drLang.IsNull(dc) && drLang[dc].ToString() != "")
                            relatedRow[dc.ColumnName] = drLang[dc];
                    }
            }
            return dt;
        }
        [Description("Returns the translation of phrase from default language to the current language. First looks at cache, if not found uses Google to translate and writes it to database and caches.")]
        public static string TR(string name)
        {
            if (Provider.Configuration.DefaultLang == Provider.CurrentLanguage.Id || string.IsNullOrWhiteSpace(name))
                return name;

            Dictionary<string, int> sr = null;
            Dictionary<string, string> srl = null;

            cacheResources();

            sr = (Dictionary<string, int>)HttpContext.Current.Cache["StaticResource"];
            srl = (Dictionary<string, string>)HttpContext.Current.Cache["StaticResourceLang"];

            if (!sr.ContainsKey(name))
            {
                StaticResource newSR = new StaticResource { Name = name };
                newSR.Save();
                sr[name] = newSR.Id;
                //HttpContext.Current.Cache["StaticResource"] = sr;

                try
                {
                    var defLang = Provider.Database.Read<Lang>(Provider.Configuration.DefaultLang);
                    foreach (Lang l in Provider.Database.ReadList<Lang>("select * from Lang where Id<>{0}", Provider.Configuration.DefaultLang))
                    {
                        string url = "http://translate.google.com/translate_a/t?client=t&sl=" + defLang.Code.Split('-')[0] + "&tl=" + l.Code.Split('-')[0] + "&hl=en&sc=2&ie=UTF-8&oe=UTF-8&oc=1&otf=1&ssel=4&tsel=0&q=" + Provider.Server.UrlEncode(name);
                        Encoding resolvedEncoding = Encoding.UTF8;
                        string translate = url.DownloadPage(ref resolvedEncoding).SplitWithTrim('"')[1];
                        if (char.IsUpper(name[0]) && char.IsLower(translate[0])) translate = translate.CapitalizeFirstLetterInvariant();
                        StaticResourceLang newSRL = new StaticResourceLang
                        {
                            LangId = l.Id,
                            StaticResourceId = newSR.Id,
                            Translation = translate
                        };
                        newSRL.Save();
                        srl[newSR.Id + "_" + newSRL.LangId] = newSRL.Translation;
                        //HttpContext.Current.Cache["StaticResourceLang"] = srl;

                        if (Provider.CurrentLanguage.Id == l.Id)
                            name = newSRL.Translation;
                    }
                }
                catch { }

                return name;
            }

            int srItem = sr[name];

            if (!srl.ContainsKey(srItem + "_" + Provider.CurrentLanguage.Id))
            {
                try
                {
                    var defLang = Provider.Database.Read<Lang>(Provider.Configuration.DefaultLang);
                    Lang l = Provider.CurrentLanguage;
                    string url = "http://translate.google.com/translate_a/t?client=t&sl=" + defLang.Code.Split('-')[0] + "&tl=" + l.Code.Split('-')[0] + "&hl=en&sc=2&ie=UTF-8&oe=UTF-8&oc=1&otf=1&ssel=4&tsel=0&q=" + Provider.Server.UrlEncode(name);
                    Encoding resolvedEncoding = Encoding.UTF8;
                    string translate = url.DownloadPage(ref resolvedEncoding).SplitWithTrim('"')[1];
                    if (char.IsUpper(name[0]) && char.IsLower(translate[0])) translate = translate.CapitalizeFirstLetterInvariant();
                    StaticResourceLang newSRL = new StaticResourceLang
                    {
                        LangId = l.Id,
                        StaticResourceId = srItem,
                        Translation = translate
                    };
                    newSRL.Save();
                    srl[srItem + "_" + newSRL.LangId] = newSRL.Translation;
                    name = newSRL.Translation;
                }
                catch { }

                HttpContext.Current.Cache["StaticResourceLang"] = srl;

                return name;

            }
            else
                return srl[srItem + "_" + Provider.CurrentLanguage.Id];
        }

        internal static void cacheResources()
        {
            if (HttpContext.Current.Cache["StaticResource"] == null)
            {
                //Create Table if does not exist!
                Provider.Database.Read<StaticResource>(1);
                HttpContext.Current.Cache["StaticResource"] = Provider.Database.GetDictionary<string, int>("select Name, Id from StaticResource");
            }
            if (HttpContext.Current.Cache["StaticResourceLang"] == null)
            {
                //Create Table if does not exist!
                Provider.Database.Read<StaticResourceLang>(1);
                Dictionary<string, string> dict = new Dictionary<string,string>();
                foreach (DataRow dr in Provider.Database.GetDataTable("select StaticResourceId, LangId, Translation from StaticResourceLang").Rows)
                    dict.Add(dr["StaticResourceId"] + "_" + dr["LangId"], dr["Translation"].ToString());

                HttpContext.Current.Cache["StaticResourceLang"] = dict;
            }
        }

        [Description("Translates entity column name")]
        public static string TranslateColumnName(string entityName, string columnName)
        {
            string columnTitle = "";
            if (columnName == "Id" || columnName.EndsWith(".Id"))
                columnTitle = "Id";
            else if (columnName == "Visible" || columnName.EndsWith(".Visible"))
                columnTitle = Provider.GetResource("BaseEntity.Visible");
            else if (columnName == "Name")
                columnTitle = Provider.GetResource("NamedEntity.Name");
            else if (columnName.EndsWith(".Name"))
                columnTitle = Provider.GetResource(columnName.Split('.')[0]);
            else
            {
                if (columnName.Contains("."))
                    columnTitle = Provider.GetResource(columnName);
                else
                    columnTitle = Provider.GetResource(entityName + "." + columnName);
                if (columnTitle.StartsWith("? "))
                {
                    if (columnName == "TCategoryId.Title")
                        columnTitle = Provider.GetResource("Content.CategoryId");
                    else
                        columnTitle = columnName;
                }
            }
            return columnTitle;
        }

        [Description("Returns default stylesheet for all the module types")]
        public static string GetAllModulesDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Type type in Provider.GetModuleTypes())
            {
                Modules.Module module = Provider.CreateModule(type);
                module.Id = 1;
                sb.Append(module.GetDefaultCSS().Replace("#", "div.").Replace("_1", "") + "\n");
            }
            return sb.ToString();
        }

        public static string GetTemplate(Content content, string moduleTemplate)
        {
            if (content == null) throw new ArgumentException(Provider.GetResource("Content parameter cannot be null!"));

            if (content.ShowInPage != "")
                return content.ShowInPage;

            if (string.IsNullOrWhiteSpace(content.Hierarchy))
                return Provider.Configuration.MainPage;

            string template = null;

            List<Content> dtCats = (List<Content>)Provider.Items["dtTemplate: " + content.Hierarchy];
            if (dtCats == null)
            {
                dtCats = Provider.Database.ReadList<Content>("select ShowContentsInPage, ShowCategoriesInPage from Content where Id in (" + content.Hierarchy + ")");
                Provider.Items["dtTemplate: " + content.Hierarchy] = dtCats;
            }
            for (int i = dtCats.Count - 1; i >= 0; i--)
            {
                Content drCat = dtCats[i];
                if (content.ClassName == "Category")
                {
                    if (!String.IsNullOrWhiteSpace(drCat.ShowCategoriesInPage))
                    {
                        template = drCat.ShowCategoriesInPage;
                        break;
                    }
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(drCat.ShowContentsInPage))
                    {
                        template = drCat.ShowContentsInPage;
                        break;
                    }
                }

            }

            if (String.IsNullOrEmpty(template)) template = moduleTemplate;
            if (String.IsNullOrEmpty(template)) template = content.ClassName == "Category" ? Provider.Configuration.CategoryPage : Provider.Configuration.ContentPage;
            return template;
        }
        public static string GetTemplateById(int id, string moduleTemplate)
        {
            return GetTemplate((Content)Provider.Database.Read(typeof(Content), id), moduleTemplate);
        }
        [Description("Deletes a template with the modules in it")]
        public static void DeleteTemplate(string template, bool clearTemplateReferences)
        {
            if (String.IsNullOrEmpty(template))
                throw new Exception("Silinecek dosya adı belirtilmemiş.");
            else if (template == "Default.aspx")
                throw new Exception("Default.aspx silinemez.");

            template = template.Trim();

            Provider.Database.Begin();

            // delete the page
            Template templateRec = (Template)Provider.Database.Read(typeof(Template), "FileName={0}", template);
            if(templateRec!=null)
                templateRec.Delete();

            // delete the modules
            try
            {
                foreach (Modules.Module module in Modules.Module.Read(template))
                    module.Delete();

                if (clearTemplateReferences)
                {
                    Provider.Database.ExecuteNonQuery("update Content set ShowInPage={0} where ShowInPage={1}", "", template);
                    Provider.Database.ExecuteNonQuery("update Content set ShowContentsInPage={0} where ShowContentsInPage={1}", "", template);
                }

                Provider.Database.Commit();
            }
            catch (Exception ex)
            {
                Provider.Database.Rollback();
                throw ex;
            }
        }
        [Description("Copies a templates with all the modules in it (Foo.aspx => Bar.aspx")]
        public static bool CopyTemplate(string template, string newName)
        {
            try
            {
                Provider.Database.Begin();
                Modules.Module[] modules = Modules.Module.Read(template);
                for (int i = 0; i < modules.Length; i++)
                {
                    modules[i].SaveACopyFor(newName);
                }

                Template templateRec = (Template)Provider.Database.Read(typeof(Template), "FileName={0}", template);
                templateRec.Id = 0;
                templateRec.FileName = newName;
                templateRec.Save();
                Provider.Database.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Provider.Database.Rollback();
                throw ex;
            }
        }
        [Description("Renames template (Foo.aspx => Bar.aspx")]
        public static bool RenameTemplate(string template, string newName)
        {
            try
            {
                Provider.Database.Begin();
                Provider.Database.ExecuteNonQuery("update Module set Template={0} where Template={1}", newName, template);
                Provider.Database.ExecuteNonQuery("update Content set ShowInPage={0} where ShowInPage={1}", newName, template);
                Provider.Database.ExecuteNonQuery("update Content set ShowContentsInPage={0} where ShowContentsInPage={1}", newName, template);
                Provider.Database.ExecuteNonQuery("update Content set ShowCategoriesInPage={0} where ShowCategoriesInPage={1}", newName, template);

                Template templateRec = (Template)Provider.Database.Read(typeof(Template), "FileName={0}", template);
                templateRec.FileName = newName;
                templateRec.Save();

                Provider.Database.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Provider.Database.Rollback();
                throw ex;
            }
        }


        public static string GetHierarchyLike(int parentCatId)
        {
            Content parentCat = (Content)Provider.Database.Read(typeof(Content), parentCatId);

            return Provider.GetHierarchyLike(parentCat);
        }
        public static string GetHierarchyLike(Content parentCat)
        {
            return parentCat.Hierarchy + (String.IsNullOrEmpty(parentCat.Hierarchy) ? "" : ",") + parentCat.Id.ToString().PadLeft(5, '0');
        }


        #region Resource
        [Browsable(false)]
        public static string GetResource(string code, params object[] args)
        {
            string lang = CurrentCulture.Split('-')[0];

            string str = lang == "tr" ? (StaticResources.tr.ContainsKey(code) ? StaticResources.tr[code] : null) : (StaticResources.en.ContainsKey(code) ? StaticResources.en[code] : null);

            return str == null ? String.Format(code, args) : String.Format(str, args);
        }
        //TODO: Bu resource stringleri veritabanına taşıyalım, değiştirilmesine izin verelim
        [Browsable(false)]
        public static string GetModuleResource(string code)
        {
            string lang = CurrentCulture.Split('-')[0];

            Hashtable ht = new Hashtable();

            if (lang == "tr")
            {
                // Anket
                ht["Vote"] = "Oyla";
                // Sepet
                ht["Product"] = "Ürün Adı";
                ht["List Price"] = "Liste Fiyatı";
                ht["Our Price"] = "Bizim Fiyatımız";
                ht["Amount"] = "Miktar";
                ht["Remove"] = "Sil";
                ht["Subtotal"] = "Ara Toplam";
                ht["Discount"] = "İndirim";
                ht["VAT"] = "KDV";
                ht["Grand Total"] = "Toplam Tutar";
                ht["Update Basket"] = "Sepeti Güncelle";
                ht["Empty Basket"] = "Sepeti Boşalt";
                ht["Proceed To Checkout"] = "Satın Al";
                // AuthorSourceDetailBase
                ht["To send email"] = "E-Posta göndermek için";
                ht["Work"] = "İş";
                ht["Cell"] = "Cep";
                // ContentTools
                ht["Comment"] = "Yorum yaz";
                ht["Feedback"] = "Editöre email";
                ht["Print"] = "Yazdır";
                ht["Recommend"] = "Tavsiye et";
                ht["Recommendation from [name]"] = "[name] isimli arkadaşınızdan tavsiye";
                ht["Your message has been sent"] = "Mesajınız iletildi. Teşekkür ederiz.";
                ht["Your message couldn't be sent"] = "Mesaj gönderilemedi. Lütfen site yöneticisine bildiriniz.";
                // Navigation
                ht["Home Page"] = "Ana Sayfa";
                // DataList
                ht["Previous Page"] = "Önceki Sayfa";
                ht["Next Page"] = "Sonraki Sayfa";
                // Form
                ht["Save"] = "Kaydet";
                // FormField
                ht["Delete"] = "Sil";
                // LoginForm
                ht["Sign up"] = "Üye olmak istiyorum";
                ht["My profile"] = "Üyelik bilgilerim.";
                ht["Forgot your password?"] = "Şifremi unuttum.";
                ht["Welcome"] = "Hoşgeldiniz";
                ht["E-Mail"] = "E-Posta";
                ht["Password"] = "Şifre";
                ht["Remember me"] = "Beni hatırla";
                ht["Send activation code"] = "Aktivasyon kodunu gönder";
                ht["Enter"] = "Giriş";
                // PasswordForm
                ht["Enter your email address and click send button. You will receive your special adress where you can change your password."] = "Şifrenizi sıfırlamak için üye olurken kullandığınız email adresinizi yazınız.";
                ht["There isn't any user with the email address you entered. Please check."] = "Bu email adresine sahip bir kullanıcı yok. Lütfen adresinizi doğru yazdığınızdan emin olunuz.";
                ht["Please change your password by using the address below"] = "Aşağıdaki linki kullanarak şifrenizi yenileyebilirsiniz.";
                ht["Your Password"] = "Şifre hatırlatma";
                ht["A message sent to your email address. Please read it."] = "Email adresinize mesaj gönderildi. Lütfen bu mesajda yazılanları uygulayınız.";
                ht["Send"] = "Gönder";
                // UserActivationForm
                ht["Enter your email address and click send button. You will receive your activation code."] = "E-Posta adresinizi girip tamama basınız. Aktivasyon kodunuz e-posta adresinize gönderilecektir.";
                ht["Please activate your membership by using the address below"] = "Aşağıdaki linki kullanarak üyeliğinizi aktifleştirebilirsiniz.";
                ht["Membership activation"] = "Üyelik aktivasyon";
                // ContactUs
                ht["Your message has been sent. Thank you."] = "Teşekkür ederiz. Mesajınız iletildi.";
                ht["Thank"] = "Teşekkür";
                ht["Complaint"] = "Şikayet";
                ht["Request"] = "Talep";
                ht["Recommendation"] = "Tavsiye";
                ht["Your Name"] = "Adınız";
                ht["Your Email Address"] = "E-Posta adresiniz";
                ht["Subject"] = "Konu";
                ht["Your Message"] = "Mesajınız";
                // GenericForm
                ht["Your form has been sent. Thank you."] = "Teşekkür ederiz. Mesajınız iletildi";
                ht["Date"] = "Tarih";
                ht["User"] = "Kullanıcı";
                ht["IP"] = "IP";
                ht["Referrer"] = "Referrer";
                ht["User Agent"] = "User Agent";
                ht["A form submitted by site visitor"] = "Site ziyaretçisinden form";
                // Comments
                ht["Total {1} comment, {2} reply"] = "Toplam {1} yorum, {2} cevap";
                ht["(Write comment)"] = "(Yorum yaz)";
                ht["(Reply)"] = "(Cevapla)";
                ht["reply"] = "cevap";

                if(ht[code]==null)
                    ht[code] = (Provider.DesignMode ? "?!! " :"") + code;
            }
            else if (lang == "en")
            {
                ht[code] = code;
            }
            else
            {
                ht[code] = "? " + code;
            }

            return (string)ht[code];
        }
        #endregion

        [Browsable(false)]
        public static string ReadStyles(List<Modules.Module> modules)
        {
            if (modules == null || modules.Count == 0)
                return String.Empty;

            StringBuilder sb = new StringBuilder();
            modules.ForEach(delegate(Modules.Module module)
            {
                sb.Append(module.CSS.Replace("}", "}\n") + "\n");
                if (module is ModuleContainer)
                    sb.Append(ReadStyles((module as ModuleContainer).ChildModules));
            });
            return sb.ToString();
        }


        #region GetThumbPath
        public static string GetThumbImgHTML(string imageUrl, int prefWidth, int prefHeight, string title, string className, string extraAttributes, bool cropPicture)
        {
            string path = Provider.GetThumbPath(imageUrl, prefWidth, prefHeight, cropPicture);

            if (path.StartsWith("ERR:"))
                return path.Substring(5);

            return String.Format("<img src=\"{0}\" border=\"0\"{1}{2}{3}{4}{5}/>",
                path,
                String.IsNullOrEmpty(title) ? "" : (" alt=\"" + CMSUtility.HtmlEncode(title) + "\""),
                prefWidth > 0 ? " width=\"" + prefWidth + "\"" : "",
                prefHeight > 0 ? " height=\"" + prefHeight + "\"" : "",
                String.IsNullOrEmpty(className) ? "" : (" class=\"" + className + "\""),
                String.IsNullOrEmpty(extraAttributes) ? "" : " " + extraAttributes);
        }
        [Description("Resizes image to specified dimensions and returns the thumb picture path")]
        public static string GetThumbPath(string imageUrl, int prefWidth, int prefHeight, bool cropPicture)
        {
            if (String.IsNullOrEmpty(imageUrl))
            {
                imageUrl = Provider.Configuration.NoPicture;
                if (String.IsNullOrEmpty(imageUrl))
                    return "/UserFiles/Avatars/heryere.jpg";
                    //return "https://cdn1.iconfinder.com/data/icons/DarkGlass_Reworked/128x128/apps/sodipodi.png";
                    //return Provider.DesignMode ? "ERR: " + Provider.GetResource("No picture. And NoPicture image not specified in configuration.") : "ERR: ";
            }

            if (!imageUrl.StartsWith("/")) imageUrl = "/" + imageUrl;
            //if (!imageUrl.StartsWith(".")) imageUrl = "." + imageUrl;

            if (prefWidth == 0 && prefHeight == 0)
            {
                prefWidth = 100;
                prefHeight = 0;
            }

            if (prefWidth == -1 && prefHeight == -1)
                return imageUrl;

            string path = null;
            try
            {
                path = Provider.MapPath(imageUrl);
                if (!File.Exists(path))
                {
                    path = Provider.MapPath(Provider.Configuration.NoPicture);
                    if (!File.Exists(path))
                        return imageUrl;
                }
            }
            catch (Exception ex) {
                return "ERR: " + ex.Message;
            }

            string thumbUrl = "/_thumbs/" + prefWidth + "x" + prefHeight + (cropPicture?"_cr":"") + "_" + imageUrl.Replace("/","_");
            string thumbPath = Provider.MapPath(thumbUrl);

            if (!File.Exists(thumbPath) || File.GetLastWriteTime(path) > File.GetLastWriteTime(thumbPath))
            {
                if (path.EndsWith(".gif") && System.Utility.IsGifAnimated(path))
                {
                    File.Copy(path, thumbPath);
                }
                else
                {
                    Image orjImg = null, imgDest = null;
                    try
                    {
                        // burada resize ediyoruz
                        orjImg = Image.FromFile(path);
                        if (prefWidth == 0)
                            imgDest = orjImg.ScaleImage(0, prefHeight);
                        else if (prefHeight == 0)
                            imgDest = orjImg.ScaleImage(prefWidth, 0);
                        else
                        {
                            double picRatio = (double)orjImg.Width / (double)orjImg.Height;
                            double prefRatio = (double)prefWidth / (double)prefHeight;
                            if (picRatio >= prefRatio)
                            {
                                imgDest = orjImg.ScaleImage(prefWidth, 0);
                                int y = Convert.ToInt32((double)(imgDest.Height - prefHeight) / 2d);
                                if (!cropPicture) // !cropPicture ifadesinde bir hata varmış gibi görünüyor ama böylesi doğru
                                    imgDest = imgDest.CropImage(0, y, prefWidth, prefHeight);
                            }
                            else
                            {
                                imgDest = orjImg.ScaleImage(0, prefHeight);
                                int x = Convert.ToInt32((double)(imgDest.Width - prefWidth) / 2d);
                                if (!cropPicture) // !cropPicture ifadesinde bir hata varmış gibi görünüyor ama böylesi doğru
                                    imgDest = imgDest.CropImage(x, 0, prefWidth, prefHeight);
                            }
                        }
                        imgDest.SaveImage(thumbPath, Provider.Configuration.ThumbQuality * 1L);
                    }
                    catch (Exception ex)
                    {
                        if (orjImg != null) orjImg.Dispose();
                        if (imgDest != null) imgDest.Dispose();
                        return "ERR: " + ex.Message;
                    }
                    if (orjImg != null) orjImg.Dispose();
                    if (imgDest != null) imgDest.Dispose();
                }
            }
            return thumbUrl;
        }
        #endregion

        #region wrappers
        [Description("Web config app settings")]
        public static NameValueCollection AppSettings
        {
            get
            {
                    return System.Web.Configuration.WebConfigurationManager.AppSettings;
            }
        }

        [Description("Returns HttpContext.Current")]
        public static HttpContext HttpContext
        {
            get
            {
                return HttpContext.Current;
            }
        }
        [Description("Returns HttpContext.Current.Server")]
        public static HttpServerUtility Server
        {
            get
            {
                return HttpContext.Current.Server;
            }
        }
        [Description("Returns HttpContext.Current.Request")]
        public static HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }
        [Description("Returns HttpContext.Current.Response")]
        public static HttpResponse Response
        {
            get
            {
                return HttpContext.Current.Response;
            }
        }
        [Description("Returns HttpContext.Current.Session")]
        public static HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }
        [Description("Returns HttpContext.Current.Items (item storage for request )")]
        public static IDictionary Items
        {
            get { return HttpContext.Current.Items; }
        }
        [Description("Returns HttpContext.Current.Application")]
        public static HttpApplicationState Application
        {
            get { return HttpContext.Current.Application; }
        }
        [Description("Returns HttpContext.Current.User")]
        public static IPrincipal ContextUser
        {
            get
            {
                return HttpContext.Current.User;
            }
            set
            {
                HttpContext.Current.User = value;
            }
        }
        [Description("Returns local path of the url")]
        public static string MapPath(string path)
        {
            if (AppSettings.AllKeys.Contains("MapPathPrefix"))
                path = AppSettings["MapPathPrefix"] + path;
            return Provider.Server.MapPath(path);
        }
        #endregion

        #region SendMail
        [Description("Sends mail with display names")]
        public static string SendMail(string from, string fromDisplayName, string to, string toDisplayName, string subject, string message)
        {
            try
            {
                MailAddress _from = new MailAddress(Provider.Configuration.AuthEmail, Provider.Configuration.SiteName);
                MailAddress _to = new MailAddress(to, toDisplayName);
                MailMessage mail = new MailMessage(_from, _to);
                if (!String.IsNullOrEmpty(from))
                    mail.ReplyToList.Add(new MailAddress(from, fromDisplayName));
                mail.Subject = subject;
                if (to != _from.Address)
                    mail.Bcc.Add(new MailAddress(Provider.Configuration.AuthEmail, Provider.Configuration.SiteName));
                mail.IsBodyHtml = true;
                mail.Body = message;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = Provider.Configuration.MailHost;
                smtp.Port = Provider.Configuration.MailPort;
                if (!String.IsNullOrEmpty(Provider.Configuration.MailUsername) && !String.IsNullOrEmpty(Provider.Configuration.MailPassword))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(Provider.Configuration.MailUsername, Provider.Configuration.MailPassword);
                }

                smtp.Send(mail);
                return "";
            }
            catch(Exception ex)
            {
                return ex.Message + (ex.InnerException == null ? "" : (" (" + ex.InnerException.Message + ")"));
            }
        }
        [Description("Sends mail from \"from\" to \"to\".")]
        public static string SendMail(string from, string to, string subject, string message)
        {
            return Provider.SendMail(from, from, to, to, subject, message);
        }
        [Description("Sends mail to the parameter \"to\".")]
        public static string SendMail(string to, string subject, string message)
        {
            return Provider.SendMail(null, null, to, to, subject, message);
        }
        [Description("Sends mail to Configuration.Email")]
        public static string SendMail(string subject, string message)
        {
            return Provider.SendMail(null, null, Provider.Configuration.AuthEmail, Provider.Configuration.SiteName, subject, message);
        }

        [Description("Sends mail to Configuration.Email with attachment")]
        public static string SendMailWithAttachment(string subject, string message)
        {
            try
            {
                MailAddress _from = new MailAddress(Provider.Configuration.AuthEmail, Provider.Configuration.SiteName);
                MailAddress _to = new MailAddress(Provider.Configuration.AuthEmail, Provider.Configuration.SiteName);
                MailMessage mail = new MailMessage(_from, _to);
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = message;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = Provider.Configuration.MailHost;
                smtp.Port = Provider.Configuration.MailPort;
                if (!String.IsNullOrEmpty(Provider.Configuration.MailUsername) && !String.IsNullOrEmpty(Provider.Configuration.MailPassword))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(Provider.Configuration.MailUsername, Provider.Configuration.MailPassword);
                }

                if (Provider.Request.Files.Count>0 && Provider.Request.Files[0].ContentLength>0)
                {
                    foreach (string key in Provider.Request.Files.Keys)
                    {
                        Attachment attachment = new Attachment(Provider.Request.Files[key].InputStream, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = attachment.ContentDisposition;
                        disposition.FileName = Path.GetFileName(Provider.Request.Files[key].FileName);
                        disposition.Size = Provider.Request.Files[key].ContentLength;
                        disposition.DispositionType = DispositionTypeNames.Attachment;
                        mail.Attachments.Add(attachment);
                    }
                }

                smtp.Send(mail);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message + (ex.InnerException == null ? "" : (" (" + ex.InnerException.Message + ")"));
            }
        }
        #endregion

        #region Osman's custom message (bu ne olm lan? derleme hatası veriyordu kapattım kodu.) :)
        /*
        public static string EmailCustomMessage(User to, string customSubject = "", string content = "")
        {
            string subject = "Custom Trial Mail";
            if (customSubject != "")
                subject = customSubject;


            if (content == "")
            {
                content = string.Format(EmailH1, "Hello" + (string.IsNullOrWhiteSpace(to.Name) ? "" : " " + to.Name) + ",");
                string p = "This is a custom message. You can send custom html messages with it. Such as <strong>this.</strong>";
                content += string.Format(EmailParagraph, p);
            }

            string msg = string.Format(EmailHTMLBody, content);

            string response = "";
            if (SendMail(to.Email, subject, msg) != "")
                response = "<h6> " + to.FullName + " &lt;" + to.Email + "&gt; </h6>";
            return response;
        }
        public static string EmailHTMLBody
        {
            get
            {
                return @"<div style=""margin-top:0;margin-bottom:0;margin-left:0;margin-right:0;padding-top:0;padding-bottom:0;padding-left:0;padding-right:0;min-width:100%"" bgcolor=""#ffffff"">
                            <center style=""background-color:#f2efe9;width:100%;min-width:620px"">
                              <table style=""border-collapse:collapse;border-spacing:0;background-color:#f2efe9;width:100%;min-width:620px;table-layout:fixed"">
                                <tbody>
                                <tr>
                                  <td style=""padding-top:0;padding-bottom:0;padding-left:0;padding-right:0;vertical-align:middle"">
                                    <table style=""border-collapse:collapse;border-spacing:0;width:100%;background-color:#ebe7e1;color:#808080;Margin-left:auto;Margin-right:auto"">
                                        <tbody>
                                          <tr><td style=""padding-top:0;padding-bottom:0;padding-left:0;padding-right:0;vertical-align:middle;font-size:20px;line-height:20px;min-height:20px;background-color:#f2efe9;background-image:none;background-repeat:repeat"">&nbsp;</td></tr>
                                        </tbody>
                                    </table>
                                  </td>
                                </tr>
                                <tr>
                                  <td style=""padding-top:0;padding-bottom:0;padding-left:0;padding-right:0;vertical-align:middle"">
                                    <center>
                                      <table style=""border-collapse:collapse;border-spacing:0;width:600px;Margin-left:auto;Margin-right:auto"">
                                        <tbody><tr>
                                          <td style=""padding-top:12px;padding-bottom:32px;padding-left:0;padding-right:0;vertical-align:middle;font-size:26px;line-height:36px;min-height:36px;Margin-bottom:0;font-weight:bold;color:#38434d;text-align:center;font-family:sans-serif"" align=""center"">
                                            <center>
                                                <div>
                                                    <a style=""color:#38434d;text-decoration:none"" href=""http://" + Configuration.SiteAddress + @""" target=""_blank"">" + Configuration.SiteName + @"</a>
                                                </div>
                                            </center>
                                          </td>
                                        </tr>
                                      </tbody></table>
                                    </center>
                                  </td>
                                </tr>
                              </tbody>
                            </table>
      
                                <table style=""border-collapse:collapse;border-spacing:0;background-color:#f2efe9;width:80%;min-width:620px;table-layout:fixed;"">
                                  <tbody>
                                    <tr>
                                        <td style=""padding-top:0;padding-bottom:0;padding-left:0;padding-right:0;text-align:left;vertical-align:middle;background-color:#ffffff;width:600px;"">
                                                        
                                            <div><div style=""font-size:54px;line-height:54px;min-height:54px;"">&nbsp;</div></div>
                                                        
                                            <table style=""border-collapse:collapse;border-spacing:0;width:100%;"">
                                                <tbody>
                                                <tr>
                                                    <td style=""padding-top:0;padding-bottom:0;;padding-left:48px;padding-right:48px;vertical-align:middle;text-align:left"">
                                                        {0}
                                                        <font size=""2"" style=""Margin-top:0;font-weight:normal;color:#38434d;font-family: sans-serif;font-size:14px;line-height:28px;min-height:28px;Margin-bottom:18px"">
                                                            <a style=""color:#38434d;text-decoration:none"" href=""http://" + Configuration.SiteAddress + @"/Team"" target=""_blank"">- " + Configuration.SiteName + @" Team</a>
                                                        </font><br><br>
                                                        <font size=""2"" style=""Margin-top:0;font-weight:normal;color:#38434d;font-family: sans-serif;font-size:14px;line-height:28px;min-height:28px;Margin-bottom:18px"">
                                                            <a style=""color:#38434d;text-decoration:none"" href=""http://" + Configuration.SiteAddress + @"/EditProfile"" target=""_blank""><font size=""1"">unsubscribe from emails</font></a>
                                                        </font><br><br>
                                                    </td>
                                                </tr>
                                                </tbody>
                                            </table>
                                                        
                                            <div style=""font-size:32px;line-height:32px;min-height:32px;"">&nbsp;</div>
                                                        
                                        </td>
                                    </tr>
                                  </tbody>
                                </table>

                                <table style=""border-collapse:collapse;border-spacing:0;background-color:#f2efe9;width:100%;min-width:620px;table-layout:fixed"">
                                <tbody>
                                <tr>
                                  <td style=""padding-top:0;padding-bottom:0;padding-left:0;padding-right:0;vertical-align:middle"">
                                    <table style=""border-collapse:collapse;border-spacing:0;width:100%;background-color:#ebe7e1;color:#808080;Margin-left:auto;Margin-right:auto"">
                                        <tbody>
                                          <tr><td style=""padding-top:0;padding-bottom:0;padding-left:0;padding-right:0;vertical-align:middle;font-size:20px;line-height:20px;min-height:20px;background-color:#f2efe9;background-image:none;background-repeat:repeat"">&nbsp;</td></tr>
                                        </tbody>
                                    </table>
                                  </td>
                                </tr>
                                <tr>
                                  <td style=""padding-top:0;padding-bottom:0;padding-left:0;padding-right:0;vertical-align:middle"">
                                    <center>
                                      <table style=""border-collapse:collapse;border-spacing:0;width:600px;Margin-left:auto;Margin-right:auto"">
                                        <tbody><tr>
                                          <td style=""padding: 0 30px 30px;vertical-align:middle;font-size:26px;Margin-bottom:0;font-weight:bold;color:#38434d;font-family:sans-serif"" align=""left"">

                                            <div>"
                                                + string.Format(EmailFooterBadge, "#", "Download Android App", "http://www.webso.it/wp-content/uploads/2013/04/logo_google_play_store_badge.png")
                                                + string.Format(EmailFooterBadge, "#", "Download iOS App", "http://www.jackenhack.com/wp-content/uploads/2014/07/Download_on_the_App_Store_Badge_US-UK_135x40_0824.png")
                                        + @"</div>
                                            
                                          </td>
                                        </tr>
                                      </tbody></table>
                                    </center>
                                  </td>
                                </tr>
                              </tbody>
                            </table>
                            </center>
                        </div>
                        ";
            }
        }
        public static string EmailParagraph
        {
            get
            {
                return @"<font size=""2"" style=""Margin-top:0;font-weight:normal;color:#38434d;font-family:Georgia,serif;font-size:14px;line-height:28px;min-height:28px;Margin-bottom:18px"">
                    {0}
                    </font><br><br>";
            }
        }
        public static string EmailLink
        {
            get
            {
                return @"
                        <a style=""color:#cab790;text-decoration:underline"" href=""{0}"" target=""_blank"">
                            {1}
                        </a>";
            }
        }
        public static string EmailH1
        {
            get
            {
                return @"
                    <font size=""3"" style=""Margin-top:0;font-weight:normal;color:#38434d;font-family:Georgia,serif;font-size:22px;line-height:28px;min-height:28px;Margin-bottom:18px"">
                        {0}
                    </font><br><br>";
            }
        }
         * */
        #endregion

        [Description("Builds url with the query string parameters")]
        public static string BuildUrl(string pageUrl, string paramName, string paramValue)
        {
            return BuildUrl(pageUrl, paramName, paramValue, "","");
        }
        [Description("Builds url with the query string parameters")]
        public static string BuildUrl(string pageUrl, string paramName1, string paramValue1, string paramName2, string paramValue2)
        {
            if (string.IsNullOrWhiteSpace(pageUrl))
                pageUrl = Provider.Request.RawUrl;

            if (!pageUrl.StartsWith("http"))
                pageUrl = "http://" + Provider.Configuration.SiteAddress + (pageUrl.StartsWith("/") ? "" : "/") + pageUrl;

            CinarUriParser uriParser = new CinarUriParser(pageUrl);
            uriParser.QueryPart[paramName1] = paramValue1;
            if(!string.IsNullOrWhiteSpace(paramName2))
                uriParser.QueryPart[paramName2] = paramValue2;

            return uriParser.ToString();
        }

        public static string PostData(string url, Dictionary<string, string> data)
        {
            // Create a request using a URL that can receive a post.
            WebRequest request = WebRequest.Create(url);
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            string postData = null;
            foreach (string key in data.Keys)
            {
                if (!string.IsNullOrEmpty(postData)) postData += "&";
                postData += key + "=" + HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(data[key] ?? ""));
            }
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            System.IO.Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            //Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }

        internal static IFormatProvider GetFormatProvider()
        {

            return System.Threading.Thread.CurrentThread.CurrentCulture;
        }

        public static string ToString(Exception ex, bool asHTML)
        {
            return Provider.ToString(ex, Provider.DesignMode, Provider.DevelopmentMode, asHTML);
        }
        public static string ToString(Exception ex, bool inDetail, bool withStackTrace, bool asHTML)
        {
            if (inDetail)
            {
                string message = ex.Message + "\n\n";
                string trace = ex.StackTrace + "\n\n";

                message = ex.InnerException == null ? message : ex.InnerException.Message + "\n\n";
                trace = ex.InnerException == null ? trace : ex.InnerException.StackTrace;

                string res = withStackTrace ? message + trace : message;
                if (asHTML) res = res.Replace("\n", "<br/>");

                return res;
            }
            else
                return "";
        }

        [Description("Creates and saves log entry")]
        public static void Log(string logType, string category, string description, string entityName="", int entityId = 0)
        {
            Log log = new Log();
            log.LogType = logType;
            log.Category = category;
            log.Description = description;
            log.EntityName = entityName;
            log.EntityId = entityId;
            log.InsertUserId = Provider.User.Id;
            log.Save();
        }

        [Description("Returns the interpreter for the given code by the template parameter")]
        public static Interpreter GetInterpreter(string template, object forThis)
        {
            Interpreter engine = new Interpreter(template, new List<string>() { "Cinar.CMS.Library", "Cinar.CMS.Library.Entities", "Cinar.CMS.Library.Modules", "Cinar.CMS.Library.Handlers" });
            engine.AddAssembly(typeof(Provider).Assembly);
            engine.AddAssembly(typeof(Utility).Assembly);
            engine.AddAssembly(typeof(Brickred.SocialAuth.NET.Core.ProviderFactory).Assembly);
            if (!String.IsNullOrEmpty(Provider.AppSettings["customAssemblies"]))
                foreach (string customAssembly in Provider.AppSettings["customAssemblies"].SplitWithTrim(','))
                {
                    Assembly assembly = GetAssembly(customAssembly);
                    if (assembly == null)
                        continue;
                    engine.AddAssembly(assembly);
                    var api = assembly.GetTypes().FirstOrDefault(t => t.GetInterface("IAPIProvider")!=null);
                    if(api!=null)
                        engine.SetAttribute(api.Name, Activator.CreateInstance(api));
                }

            engine.SetAttribute("Context", new ProviderWrapper());
            engine.SetAttribute("this", forThis);
            engine.SetAttribute("db", Provider.Database);

            return engine;
        }

        [Description("Returns human readable url of content")]
        public static string GetPageUrl(string template, int id, string categoryTitle, string contentTitle)
        {
            if (id == 1)
                return (Provider.Configuration.StartUrlsWithLangCode ? "/" + Provider.CurrentCulture.Split('-')[0] : "") + "/" + Provider.Configuration.MainPage;

            if (Provider.DesignMode)
                return string.Format(
                    "{0}/{1}?item={2}",
                    Provider.Configuration.StartUrlsWithLangCode ? "/" + Provider.CurrentCulture.Split('-')[0] : "",
                    template,
                    id);

            return string.Format(
                    "{0}/{1}/{2}/{3}_{4}.aspx",
                    Provider.Configuration.StartUrlsWithLangCode ? "/" + Provider.CurrentCulture.Split('-')[0] : "",
                    template.Replace(".aspx", "").ToLowerInvariant(),
                    categoryTitle.MakeFileName().ToLowerInvariant(),
                    contentTitle.MakeFileName().ToLowerInvariant(),
                    id);
        }
        [Description("Returns human readable url of content")]
        public static string GetPageUrl(string template, int contentId)
        {
            Content content = Provider.Database.Read<Entities.Content>(contentId);
            if (content != null)
                return Provider.GetPageUrl(Provider.GetTemplate(content, ""), content.Id, content.Category.Title, content.Title);
            else
                return "javascript:alert('No such content'); return false;";
        }

        [Description("Reads exchange rates from http://www.tcmb.gov.tr/kurlar/today.xml")]
        public static ExchangeRate GetExchangeRates()
        {
            ExchangeRate res = Provider.Database.Read<ExchangeRate>("InsertDate >= {0}", DateTime.Now.Date);
            if (res == null)
            {
                res = new ExchangeRate();

                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                string xml = wc.DownloadString("http://www.tcmb.gov.tr/kurlar/today.xml");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                foreach (XmlNode node in doc.SelectNodes("//Currency[ForexSelling>0]"))
                {
                    string name = node.Attributes["Kod"].Value;
                    int fiyat = (int)(decimal.Parse(node.SelectSingleNode("ForexSelling").InnerText, CultureInfo.InvariantCulture.NumberFormat) * 1000);
                    try
                    {
                        res.SetMemberValue(name, fiyat);
                    }catch{}
                }

                res.Save();
            }
            return res;
        }

        internal static Dictionary<Regex, string> routes = null;
        [Browsable(false)]
        public static Dictionary<Regex, string> Routes
        {
            get
            {
                if (routes == null)
                {
                    routes = new Dictionary<Regex, string>();
                    string[] lines = Provider.Configuration.Routes.Replace("\r", "").SplitWithTrim('\n');
                    foreach (var line in lines)
                    {
                        string[] parts = line.SplitWithTrim("=>");
                        if (!parts[0].StartsWith("^"))
                            parts[0] = "^" + parts[0];
                        if (!parts[0].EndsWith("$"))
                            parts[0] = parts[0] + "$";
                        routes.Add(new Regex(parts[0]), parts[1]);
                    }
                }
                return routes;
            }
        }

        public static string GetRewritePath(string url)
        {
            foreach (var item in Routes)
            {
                if (item.Key.IsMatch(url))
                    return item.Key.Replace(url, item.Value);
            }

            return url;
        }

        [Description("Deletes the thumb pictures of urlPath (/UserFiles/Images/foo/bar.jpg)")]
        public static void DeleteThumbFiles(string urlPath)
        {
            foreach (string thumbFilePath in Directory.GetFiles(Provider.MapPath("/_thumbs"), "*" + urlPath.Replace("/", "_")))
                File.Delete(thumbFilePath);
        }

        [Description("Returns visitor location in JSON format")]
        public static string GetVisitorLocation()
        {
            return ("http://freegeoip.net/json/"+GetIPAddress()).DownloadPage();
        }

        [Description("Returns visitor IP address")]
        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;

            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        #region reflection and other internal facility
        [Browsable(false)]
        public static ControlType GetDefaultControlType(Cinar.Database.DbType dbType, PropertyInfo pi, ColumnDetailAttribute columnProps)
        {
            ControlType ct = ControlType.Undefined;
            Type type = pi.PropertyType;

            switch (dbType)
            {
                case Cinar.Database.DbType.Boolean:
                    ct = ControlType.ComboBox;
                    break;
                case Cinar.Database.DbType.Byte:
                case Cinar.Database.DbType.Int16:
                case Cinar.Database.DbType.Int32:
                case Cinar.Database.DbType.Int64:
                    ct = ControlType.IntegerEdit;
                    if (columnProps.References != null)
                        ct = ControlType.LookUp;
                    break;
                case Cinar.Database.DbType.Real:
                case Cinar.Database.DbType.Float:
                case Cinar.Database.DbType.Decimal:
                case Cinar.Database.DbType.Double:
                case Cinar.Database.DbType.Numeric:
                case Cinar.Database.DbType.Currency:
                case Cinar.Database.DbType.CurrencySmall:
                    ct = ControlType.DecimalEdit;
                    break;
                case Cinar.Database.DbType.DateTimeSmall:
                case Cinar.Database.DbType.DateTime:
                case Cinar.Database.DbType.Date:
                    ct = ControlType.DateTimeEdit;
                    break;
                case Cinar.Database.DbType.Char:
                case Cinar.Database.DbType.VarChar:
                case Cinar.Database.DbType.NChar:
                case Cinar.Database.DbType.NVarChar:
                case Cinar.Database.DbType.Text:
                case Cinar.Database.DbType.NText:
                case Cinar.Database.DbType.TextTiny:
                case Cinar.Database.DbType.TextMedium:
                case Cinar.Database.DbType.TextLong:
                    if (type.IsEnum)
                        ct = ControlType.ComboBox;
                    else
                        ct = ControlType.StringEdit;
                    break;
                case Cinar.Database.DbType.Time:
                case Cinar.Database.DbType.Timetz:
                case Cinar.Database.DbType.Timestamp:
                case Cinar.Database.DbType.Timestamptz:
                case Cinar.Database.DbType.Binary:
                case Cinar.Database.DbType.VarBinary:
                case Cinar.Database.DbType.Image:
                case Cinar.Database.DbType.Blob:
                case Cinar.Database.DbType.BlobTiny:
                case Cinar.Database.DbType.BlobMedium:
                case Cinar.Database.DbType.BlobLong:
                case Cinar.Database.DbType.Variant:
                case Cinar.Database.DbType.Guid:
                case Cinar.Database.DbType.Xml:
                case Cinar.Database.DbType.Set:
                case Cinar.Database.DbType.Enum:
                case Cinar.Database.DbType.Undefined:
                default:
                    switch (type.Name)
                    {
                        case "String":
                            if (type.IsEnum)
                                ct = ControlType.ComboBox;
                            else
                                ct = ControlType.StringEdit;
                            break;
                        case "Int16":
                        case "Int32":
                        case "Int64":
                            if (type.IsEnum)
                                ct = ControlType.ComboBox;
                            else if (columnProps.References != null)
                                ct = ControlType.LookUp;
                            else
                                ct = ControlType.IntegerEdit;
                            break;
                        case "Decimal":
                        case "Double":
                        case "Single":
                            ct = ControlType.DecimalEdit;
                            break;
                        case "DateTime":
                            ct = ControlType.DateTimeEdit;
                            break;
                        case "Boolean":
                            ct = ControlType.ComboBox;
                            break;
                        default:
                            if (type.IsEnum)
                                ct = ControlType.ComboBox;
                            else
                                throw new Exception(Provider.GetResource("A control type cannot be acquired from the field type!"));
                            break;
                    }
                    break;
            }

            return ct;
        }
        public static string GetPropertyEditorJSON(object obj, bool addInvisibles)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[\n");
            ArrayList res = new ArrayList();
            int ctrlOrderNo = 0;
            foreach (PropertyInfo pi in obj.GetProperties().OrderBy(x => x.MetadataToken))
            {
                if (pi.Name == "Item") continue;
                if (pi.GetSetMethod() == null) continue;

                EditFormFieldPropsAttribute editProps = (EditFormFieldPropsAttribute)CMSUtility.GetAttribute(pi, typeof(EditFormFieldPropsAttribute));
                if (!addInvisibles && !editProps.Visible)
                    continue; //***

                editProps.Category = editProps.Category ?? Provider.GetResource((pi.DeclaringType == typeof(NamedEntity) ? obj.GetType() : pi.DeclaringType).Name);
                editProps.OrderNo = editProps.OrderNo > 0 ? editProps.OrderNo : ctrlOrderNo++;

                ColumnDetailAttribute columnProps = (ColumnDetailAttribute)CMSUtility.GetAttribute(pi, typeof(ColumnDetailAttribute));

                if (editProps.ControlType == ControlType.Undefined)
                    editProps.ControlType = Provider.GetDefaultControlType(columnProps.ColumnType, pi, columnProps);
                if (columnProps.ColumnType == Cinar.Database.DbType.Undefined)
                    columnProps.ColumnType = Column.GetDbTypeOf(pi.PropertyType);
                if (columnProps.ColumnType == Cinar.Database.DbType.Text)
                    columnProps.Length = Int32.MaxValue;
                if (pi.PropertyType.IsEnum && columnProps.ColumnType == DbType.Undefined)
                    columnProps.ColumnType = DbType.Int32;

                string caption = Provider.GetResource(pi.DeclaringType.Name + "." + pi.Name);
                if (caption.StartsWith(pi.DeclaringType.Name + "."))
                    caption = pi.Name;
                string description = Provider.GetResource(pi.DeclaringType.Name + "." + pi.Name + "Desc");
                if (description.StartsWith(pi.DeclaringType.Name + "."))
                    description = "";

                string options = "";
                if (columnProps.IsNotNull && !editProps.Options.Contains("required:")) options += ",required:true";

                switch (editProps.ControlType)
                {
                    case ControlType.StringEdit:
                        options += ",maxLength:" + columnProps.Length;
                        break;
                    case ControlType.IntegerEdit:
                        options += ",maxLength:10";
                        break;
                    case ControlType.DecimalEdit:
                        break;
                    case ControlType.DateTimeEdit:
                        break;
                    case ControlType.PictureEdit:
                        break;
                    case ControlType.CSSEdit:
                        break;
                    case ControlType.MemoEdit:
                        break;
                    case ControlType.FilterEdit:
                        break;
                    case ControlType.ComboBox:
                        if (pi.PropertyType.IsEnum)
                        {
                            if (Provider.Database.GetColumnForProperty(pi).IsStringType())
                                options += string.Format(",items:[{0}]", Enum.GetNames(pi.PropertyType).Select(s => "['" + s + "','" + s + "']").StringJoin(","));
                            else
                                options += string.Format(",items:[{0}]", Enum.GetNames(pi.PropertyType).Select((s, i) => "[" + i + ",'" + s + "']").StringJoin(","));
                        }
                        if (columnProps.ColumnType == Cinar.Database.DbType.Boolean && editProps.Options.IndexOf("items:") == -1)
                            options += String.Format(",items:[[false,'{0}'],[true,'{1}']]", Provider.GetResource("No"), Provider.GetResource("Yes"));
                        if (columnProps.References != null)
                            options += ",entityName:'" + columnProps.References.Name + "', itemsUrl:'EntityInfo.ashx'";
                        break;
                    case ControlType.LookUp:
                        if (columnProps.References != null)
                            options += ",entityName:'" + columnProps.References.Name + "', itemsUrl:'EntityInfo.ashx'";
                        break;
                    case ControlType.TagEdit:
                        options += ",entityName:'Tag', itemsUrl:'EntityInfo.ashx'";
                        break;
                    default:
                        throw new Exception(Provider.GetResource("{0} type of controls not supported yet.", editProps.ControlType));
                }
                if (!String.IsNullOrEmpty(editProps.Options)) options += ", " + editProps.Options;

                if (!String.IsNullOrEmpty(options))
                    options = ", options:{" + options.Substring(1) + "}";

                res.Add("\t{" + String.Format("label:{0}, description:{1}, type:'{2}', id:{3}, category:{4}, orderNo:{5}, value:{6}{7}",
                    caption.ToJS(),
                    description.ToJS(),
                    editProps.ControlType,
                    pi.Name.ToJS(),
                    editProps.Category.ToJS(),
                    editProps.OrderNo.ToJS(),
                    pi.GetValue(obj, null).ToJS(),
                    options) + "}\n,");
            }

            //res.Sort();
            foreach (string s in res)
                sb.Append(s);

            // bir info formun içinde list form olabilir (details)
            //object[] attrDetails = obj.GetType().GetCustomAttributes(typeof(EditFormDetailsAttribute), false);
            //foreach (EditFormDetailsAttribute detail in attrDetails)
            //{
            //    ListFormPropsAttribute listFormProps = (ListFormPropsAttribute)CMSUtility.GetAttribute(detail.DetailType, typeof(ListFormPropsAttribute));
            //    sb.Append("\t{");
            //    sb.AppendFormat("label:'{0}', description:'{1}', type:'ListForm', entityName:'{2}', relatedFieldName:'{3}'", CMSUtility.ToHTMLString(Provider.GetResource(detail.DetailType.Name)), CMSUtility.ToHTMLString(Provider.GetResource(detail.DetailType.Name + "Desc")), detail.DetailType.Name, detail.RelatedFieldName);
            //    sb.Append("}\n,");
            //}

            if (obj.GetType() != typeof(Lang))
            {
                foreach (Type entityType in Provider.GetEntityTypes())
                    foreach (PropertyInfo pi in entityType.GetProperties())
                    {
                        if (pi.DeclaringType == typeof(BaseEntity)) continue; //*** BaseEntity'deki UpdateUserId ve InsertUserId bütün entitilerde var, gereksiz bir ilişki kalabalığı oluşturuyor

                        ColumnDetailAttribute fda = (ColumnDetailAttribute)CMSUtility.GetAttribute(pi, typeof(ColumnDetailAttribute));
                        if (fda.References == obj.GetType())
                        {
                            sb.Append("\t{");
                            sb.AppendFormat("label:'{0}', description:'{1}', type:'ListForm', entityName:'{2}', relatedFieldName:'{3}'",
                                Provider.GetResource(entityType.Name).ToHTMLString(),
                                Provider.GetResource(entityType.Name + "Desc").ToHTMLString(),
                                entityType.Name,
                                pi.Name);
                            sb.Append("}\n,");
                        }
                    }
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");

            string propEditorJSON = ParseOptions(sb.ToString());

            return propEditorJSON;
        }
        [Browsable(false)]
        public static string ParseOptions(string options)
        {
            if (options.Contains("_LANGS_"))
                options = options.Replace("_LANGS_", Lang.GetLangsJSON());
            if (options.Contains("_CLASSNAMELIST_"))
                options = options.Replace("_CLASSNAMELIST_", GetResource("_CLASSNAMELIST_"));
            if (options.Contains("_SHOWPERCENT_"))
                options = options.Replace("_SHOWPERCENT_", GetResource("_SHOWPERCENT_"));
            if (options.Contains("_ORDERCONTENTSBY_"))
                options = options.Replace("_ORDERCONTENTSBY_", GetResource("_ORDERCONTENTSBY_"));
            if (options.Contains("_ASCENDING_"))
                options = options.Replace("_ASCENDING_", GetResource("_ASCENDING_"));
            if (options.Contains("_GROUPBY_"))
                options = options.Replace("_GROUPBY_", GetResource("_GROUPBY_"));
            if (options.Contains("_HORIZONTAL_"))
                options = options.Replace("_HORIZONTAL_", GetResource("_HORIZONTAL_"));
            if (options.Contains("_UICONTROLTYPE_"))
                options = options.Replace("_UICONTROLTYPE_", GetResource("_UICONTROLTYPE_"));
            if (options.Contains("_MODERATED_"))
                options = options.Replace("_MODERATED_", GetResource("_MODERATED_"));
            if (options.Contains("_WHICHPICTURE_"))
                options = options.Replace("_WHICHPICTURE_", GetResource("_WHICHPICTURE_"));
            if (options.Contains("_WHICHPICTURE2_"))
                options = options.Replace("_WHICHPICTURE2_", GetResource("_WHICHPICTURE2_"));
            if (options.Contains("_VIEWCONDITION_"))
                options = options.Replace("_VIEWCONDITION_", GetResource("_VIEWCONDITION_"));
            if (options.Contains("_LEGENDPOSITIONS_"))
                options = options.Replace("_LEGENDPOSITIONS_", GetResource("_LEGENDPOSITIONS_"));
            if (options.Contains("_CHARTTYPES_"))
                options = options.Replace("_CHARTTYPES_", GetResource("_CHARTTYPES_"));
            if (options.Contains("_USECACHE_"))
                options = options.Replace("_USECACHE_", GetResource("_USECACHE_"));
            if (options.Contains("_USECACHEFORCONF_"))
                options = options.Replace("_USECACHEFORCONF_", GetResource("_USECACHEFORCONF_"));

            return options;
        }

        [Browsable(false)]
        public static Type GetType(string typeNamespace, string typeName)
        {
            Type type = Assembly.GetExecutingAssembly().GetType("Cinar.CMS.Library." + typeNamespace + "." + typeName, false, true);
            if (type != null)
                return type;
            else
            {
                if (!String.IsNullOrEmpty(Provider.AppSettings["customAssemblies"]))
                    foreach (string customAssembly in Provider.AppSettings["customAssemblies"].SplitWithTrim(','))
                    {
                        Assembly assembly = GetAssembly(customAssembly);
                        if (assembly == null)
                            throw new Exception("Couldnt load assembly: " + customAssembly);

                        type = assembly.GetType(customAssembly + "." + typeNamespace + "." + typeName, false, true);
                        if (type != null)
                            return type;
                    }
            }
            return null;
        }
        public static Type GetEntityType(string entityName)
        {
            return GetType("Entities", entityName);
        }
        public static Type GetModuleType(string moduleName)
        {
            return GetType("Modules", moduleName);
        }
        public static BaseEntity CreateEntity(Type entityType)
        {
            return (BaseEntity)entityType.GetConstructor(Type.EmptyTypes).Invoke(null);
        }
        public static BaseEntity CreateEntity(string entityName)
        {
            return CreateEntity(GetEntityType(entityName));
        }
        public static Modules.Module CreateModule(Type moduleType)
        {
            return (Modules.Module)moduleType.GetConstructor(Type.EmptyTypes).Invoke(null);
        }
        public static Modules.Module CreateModule(string moduleName)
        {
            return CreateModule(GetModuleType(moduleName));
        }
        public static List<Type> GetModuleTypes()
        {
            return GetModuleTypes(false);
        }
        public static List<Type> GetModuleTypes(bool abstractModules)
        {
            return getTypes(typeof(Modules.Module), abstractModules);
        }
        public static List<Type> GetEntityTypes()
        {
            return GetEntityTypes(false);
        }
        public static List<Type> GetEntityTypes(bool abstractEntities)
        {
            return getTypes(typeof(Entities.BaseEntity), abstractEntities);
        }

        private static List<Type> getTypes(Type baseType, bool abstractTypes)
        {
            List<Type> types = new List<Type>();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
                if (type.IsSubclassOf(baseType) && type.IsAbstract == abstractTypes)
                    types.Add(type);
            if (!String.IsNullOrEmpty(Provider.AppSettings["customAssemblies"]))
                foreach (string customAssembly in Provider.AppSettings["customAssemblies"].SplitWithTrim(','))
                {
                    Assembly assembly = GetAssembly(customAssembly);
                    if (assembly == null)
                        throw new Exception("Couldnt load assembly: " + customAssembly);
                    foreach (Type type in assembly.GetTypes())
                        if (type.IsSubclassOf(baseType) && type.IsAbstract == abstractTypes)
                            types.Add(type);
                }
            types.Sort(delegate(Type t1, Type t2) { return Provider.GetResource(t1.Name).CompareTo(Provider.GetResource(t2.Name)); });
            return types;
        }
        private static Hashtable assemblies = new Hashtable();
        [Browsable(false)]
        public static Assembly GetAssembly(string assemblyName)
        {
            if (assemblies[assemblyName] == null)
                assemblies[assemblyName] = Assembly.Load(assemblyName);
            return (Assembly)assemblies[assemblyName];
        }

        [Browsable(false)]
        public static void OnBeginRequest()
        {
            Provider.Response.Charset = "utf-8";
            Provider.Request.ContentEncoding = Encoding.UTF8;
            Provider.Response.ContentEncoding = Encoding.UTF8;

            if (Provider.Session != null && Provider.Session.IsNewSession && Provider.Request.UserLanguages != null && Provider.Request.UserLanguages.Length > 0)
            {
                // check tables
                if (Provider.Database.Tables["Module"] == null)
                {
                    Provider.Database.CreateTableForType(typeof(Modules.Module));
                    //HttpContext.Current.RewritePath("Default.aspx");
                }

                // if language is specified by the browser, use it:
                string lang = Provider.Request.UserLanguages[0];
                if (lang.Contains("-"))
                    lang = lang.Substring(0, lang.IndexOf('-')); // Here we omit the country code while we shouldn't.. 
                Lang l = (Lang)Provider.Database.Read(typeof(Lang), "Code like {0} AND Visible=1", lang + "%");
                if (l != null)
                    Provider.CurrentCulture = l.Code;
                else
                    Provider.CurrentCulture = Provider.Database.Read<Lang>(Provider.Configuration.DefaultLang).Code;

                Provider.SetHttpContextUser();

                HttpContext.Current.RewritePath((Provider.Configuration.StartUrlsWithLangCode ? "/" + Provider.CurrentCulture.Split('-')[0]:"") + "/Default.aspx");
            }
        }

        [Browsable(false)]
        public static Content UploadContent()
        {
            try
            {
                Content content = (Content)Provider.CreateEntity("Content");
                content.SetFieldsByPostData(Provider.Request.Form);

                string authorName = Provider.Request.Form["Author"];
                if (!string.IsNullOrWhiteSpace(authorName))
                {
                    Author author = (Author)Provider.Database.Read(typeof(Author), "Name={0}", authorName);
                    bool authorVarAmaResmiYok = author != null && String.IsNullOrEmpty(author.Picture);
                    if (author == null || authorVarAmaResmiYok)
                    {
                        if (author == null)
                            author = new Author();
                        author.Name = authorName;
                        string authorPicture = Provider.Request.Form["AuthorPicture"];
                        if (authorPicture != null && authorPicture.StartsWith("http://"))
                        {
                            try
                            {
                                string imgFileName = Provider.AppSettings["authorDir"] + "/" + author.Name.MakeFileName() + "_" + (DateTime.Now.Millisecond % 1000) + authorPicture.Substring(authorPicture.LastIndexOf('.'));
                                WebClient wc = new WebClient();
                                wc.Proxy.Credentials = CredentialCache.DefaultCredentials;
                                wc.DownloadFile(authorPicture, Provider.MapPath(imgFileName));
                                author.Picture = imgFileName;
                            }
                            catch (Exception ex)
                            {
                                Provider.Log("Notice", "UploadContent", ex.Message + "\n (Yazar resmi:" + author.Picture + ")");
                                author.Picture = "";
                            }
                        }
                        author.Save();
                    }
                    content.AuthorId = author.Id;
                }

                string sourceName = Provider.Request.Form["Source"];
                if (!string.IsNullOrWhiteSpace(sourceName))
                {
                    Source source = (Source)Provider.Database.Read(typeof(Source), "Name={0}", sourceName);
                    if (source == null)
                    {
                        source = new Source();
                        source.Name = sourceName;
                        source.Save();
                    }
                    content.SourceId = source.Id;
                }

                string categoryTitle = Provider.Request.Form["Category"];
                if (!string.IsNullOrWhiteSpace(categoryTitle))
                {
                    Content category = (Content)Provider.Database.Read(typeof(Content), "Title={0}", categoryTitle);
                    if (category == null)
                    {
                        category = new Content();
                        category.ClassName = "Category";
                        category.CategoryId = 2; // int.Parse(Provider.AppSettings["newsParentCatId"]);
                        category.Title = categoryTitle;
                        category.Save();
                    }
                    content.CategoryId = category.Id;
                }

                content.Save();

                return Provider.Database.Read<Content>(content.Id);
            }
            catch (Exception ex)
            {
                Provider.Log("Error", "UploadContent", Provider.ToString(ex, true, true, true));
                return null;
            }
        }

        [Browsable(false)]
        public static string BuildPath(string fileName, string specialFolder, bool useYearMonthDateFolders)
        {
            string subFolders = "";
            if (useYearMonthDateFolders)
            {
                string year = DateTime.Now.Year.ToString();
                string month = DateTime.Now.Month.ToString();
                string day = DateTime.Now.Day.ToString();
                if (!Directory.Exists(Provider.MapPath(Provider.AppSettings[specialFolder] + "/" + year + "/" + month + "/" + day)))
                    Directory.CreateDirectory(Provider.MapPath(Provider.AppSettings[specialFolder] + "/" + year + "/" + month + "/" + day));
                subFolders = "/" + year + "/" + month + "/" + day;
            }
            return Provider.AppSettings[specialFolder] + subFolders + "/" + fileName;
        }

        [Browsable(false)]
        public static string GetRegionInnerHtml(string template, string region)
        {
            var modules = new List<Modules.Module>(Modules.Module.Read(template, region));
            return GetRegionInnerHtml(modules);
        }
        [Browsable(false)]
        public static string GetRegionInnerHtml(List<Modules.Module> modules)
        {
            return Provider.GetRegionInnerHtml(modules, true);
        }
        [Browsable(false)]
        public static string GetRegionInnerHtml(List<Modules.Module> modules, bool editable)
        {
            var sb = new StringBuilder();

            if (modules.Count == 0 && Provider.DesignMode)
                sb.Append(Provider.GetResource("Empty region"));

            modules.ForEach(delegate(Modules.Module module)
            {
                module.editable = editable;
                sb.Append(module.Show());
            });

            return sb.ToString();
        }
        #endregion

        #region FetchAutoContent
        [Browsable(false)]
        public static void FetchAutoContentDetails(Content content)
        {
            ContentSource contentSource = (ContentSource)Provider.Database.Read(typeof(ContentSource), content.ContentSourceId);

            if (contentSource.ContentRegExp == "")
            {
                content.Metin = "Content regExp not defined.";
                content.Save();
                return; //***
            }

            WebRequest req = WebRequest.Create(content.SourceLink);
            if (req.Proxy != null)
                req.Proxy.Credentials = CredentialCache.DefaultCredentials;
            WebResponse webResponse = req.GetResponse();
            string response = new System.IO.StreamReader(webResponse.GetResponseStream(), Encoding.GetEncoding("iso-8859-9")).ReadToEnd();
            response = response.Replace(Environment.NewLine, "\n");

            Regex regexObj = new Regex(contentSource.ContentRegExp, RegexOptions.Singleline);
            Match match = regexObj.Match(response);

            if (!match.Success)
            {
                content.Metin = "RegExp match failed.";
            }
            else
            {
                if (match.Groups["metin"].Value == "")
                    content.Metin = "RegExp matched, but named group 'metin' is empty.";
                else
                    content.Metin = "<p>" + CMSUtility.ClearTextFromWeb(match.Groups["metin"].Value) + "</p>";

                if (match.Groups["title"].Value != "")
                    content.Title = CMSUtility.ClearTextFromWeb(match.Groups["title"].Value);

                if (match.Groups["desc"].Value != "")
                    content.Description = "<p>" + CMSUtility.ClearTextFromWeb(match.Groups["desc"].Value) + "</p>";

                if (match.Groups["date"].Value != "")
                    try { content.PublishDate = DateTime.Parse(match.Groups["desc"].Value.Trim()); }
                    catch { }

                if (match.Groups["contentpic"].Value != "")
                {
                    string contentImgUrl = match.Groups["contentpic"].Value.Trim();
                    WebClient wc = new WebClient();
                    wc.Proxy.Credentials = CredentialCache.DefaultCredentials;
                    string imgFileName = Provider.AppSettings["uploadDir"] + "/" + content.Title.MakeFileName() + contentImgUrl.Substring(contentImgUrl.LastIndexOf('.'));
                    wc.DownloadFile(contentImgUrl, MapPath(imgFileName));
                    content.Picture = imgFileName;
                }
            }
            content.Save();
        }
        [Browsable(false)]
        public static void FetchAutoContent(ContentSource contentSource)
        {
            // bugün fetch edildiyse bir daha fetch etmeye gerek yok
            if (contentSource.LastFetched.Date == DateTime.Now.Date)
                return;

            // en son bir saatten daha önce fetch edildiyse bir daha fetch etmeyi dene
            if (DateTime.Now - contentSource.LastFetchTrial > TimeSpan.FromMinutes((double)contentSource.FetchFrequency))
            {
                string url = parseLinkPattern(contentSource.ListPageAddress);
                bool contentFetched = Provider.parseContentsAndSave(url, contentSource);

                if (contentFetched)
                    contentSource.LastFetched = DateTime.Now;
                contentSource.LastFetchTrial = DateTime.Now;
                contentSource.Save();
            }
        }
        private static bool parseContentsAndSave(string url, ContentSource contentSource)
        {
            bool contentFetched = false;
            WebRequest req = WebRequest.Create(url);
            if (req.Proxy != null)
                req.Proxy.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse webResponse = (HttpWebResponse)req.GetResponse();
            string response = null;
            if (!String.IsNullOrEmpty(contentSource.Encoding))
            {
                string orjResponse = new System.IO.StreamReader(webResponse.GetResponseStream(), Encoding.GetEncoding(contentSource.Encoding)).ReadToEnd();
                response = orjResponse.ConvertEncoding(contentSource.Encoding, "iso-8859-9");
            }
            else
                response = new System.IO.StreamReader(webResponse.GetResponseStream(), Encoding.GetEncoding("iso-8859-9")).ReadToEnd();
            response = response.Replace(Environment.NewLine, "\n");

            Regex regexObj = new Regex(contentSource.ListRegExp, RegexOptions.Singleline);
            Match match = regexObj.Match(response);
            while (match.Success)
            {
                Content content = new Content();

                if (match.Groups["author"].Value != "")
                {
                    string authorName = CMSUtility.ClearTextFromWeb(match.Groups["author"].Value);
                    Author author = (Author)Provider.Database.Read(typeof(Author), "Name={0}", authorName);
                    //object authorId = Provider.Database.GetValue("select Id from Author where Name={0}", authorName);
                    if (author == null)
                    {
                        author = new Author();
                        author.Description = url + " adresinden otomatik olarak kaydedildi.";
                        author.Name = authorName;
                        if (match.Groups["authorpic"].Value != "")
                        {
                            string authorImgUrl = match.Groups["authorpic"].Value.Trim().ConvertToAbsoluteURL(url);
                            WebClient wc = new WebClient();
                            wc.Proxy.Credentials = CredentialCache.DefaultCredentials;
                            string imgFileName = Provider.AppSettings["authorDir"] + "/" + authorName.MakeFileName() + authorImgUrl.Substring(authorImgUrl.LastIndexOf('.'));
                            wc.DownloadFile(authorImgUrl, MapPath(imgFileName));
                            author.Picture = imgFileName;
                        }
                        author.Save();
                        content.AuthorId = author.Id;
                    }
                    else
                    {
                        content.AuthorId = author.Id;
                        if (author.DisableAutoContent)
                        {
                            match = match.NextMatch();
                            continue; //***
                        }
                    }
                }

                try { content.PublishDate = DateTime.Parse(match.Groups["date"].Value.Trim()); }
                catch { }
                content.Title = CMSUtility.ClearTextFromWeb(match.Groups["title"].Value);
                content.Description = "<p>" + CMSUtility.ClearTextFromWeb(match.Groups["desc"].Value) + "</p>";
                content.Metin = ""; // burada metin kaydedilemez (liste çünkü) (metin ve picture daha sonra kaydediliyor - detaya bakıldığında)
                content.CategoryId = contentSource.CategoryId;
                content.ClassName = contentSource.ClassName;
                content.SourceId = contentSource.SourceId;
                string sourceLink = match.Groups["link"].Value.Trim();
                content.SourceLink = (new Uri(new Uri(url), sourceLink)).ToString();
                content.ContentSourceId = contentSource.Id;
                if (content.AuthorId == 0) content.AuthorId = contentSource.AuthorId;
                try
                {
                    content.Save();
                }
                catch { }

                contentFetched = true;

                match = match.NextMatch();
            }

            return contentFetched;
        }
        private static string parseLinkPattern(string linkPattern)
        {
            DateTime today = DateTime.Today;
            if (today.Hour < 4) // gecenin 4'üne kadar today=yesterday olsun.
                today = DateTime.Today.AddDays(-1d);

            return linkPattern
                .Replace("{year}", today.ToString("yyyy"))
                .Replace("{month}", today.ToString("MM"))
                .Replace("{day}", today.ToString("dd"));
        }
        #endregion

        public static string FacebookLoginButton
        {
            get {
                string code = @"
    <div class=""fb-login-button"" data-show-faces=""false"" data-width=""100"" data-max-rows=""1"" scope=""email,user_about_me""></div>
    <div id=""fb-root""></div>
    <script>
      window.fbAsyncInit = function() {
        FB.init({
          appId      : 'APP_ID',
          status     : true,
          cookie     : true,
          xfbml      : true
        });
    
        FB.Event.subscribe('auth.authResponseChange', function(response) {
            if (response.status === 'connected') {
                // the user is logged in and has authenticated your app, and response.authResponse supplies the user's ID, a valid access token, a signed request, and the time the access token and signed request each expire
                var uid = response.authResponse.userID;
                var accessToken = response.authResponse.accessToken;
        
                // Handle the access token
                var form = document.createElement(""form"");
                form.setAttribute(""method"", 'post');
                form.setAttribute(""action"", '/FacebookLogin.ashx');
                
                var field = document.createElement(""input"");
                field.setAttribute(""type"", ""hidden"");
                field.setAttribute(""name"", 'accessToken');
                field.setAttribute(""value"", accessToken);
                form.appendChild(field);
                
                document.body.appendChild(form);
                form.submit();
            } else if (response.status === 'not_authorized') {
                // the user is logged in to Facebook, but has not authenticated your app
            } else {
                // the user isn't logged in to Facebook.
            }
        });
    };
    
      // Load the SDK Asynchronously
      (function(d){
         var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
         if (d.getElementById(id)) {return;}
         js = d.createElement('script'); js.id = id; js.async = true;
         js.src = ""//connect.facebook.net/en_US/all.js"";
         ref.parentNode.insertBefore(js, ref);
       }(document));
    </script>
";
                return code.Replace("APP_ID", Provider.Configuration.FacebookAppId);
            }
        }

        public static XmlDocument GetXmlDocument(string url)
        {
            XmlDocument res = new XmlDocument();
            res.Load(url);
            return res;
        }

        public static void RegenerateScripts()
        {
            // read database metadata
            Provider.Database.ClearMetadataCache();
            File.Delete(Provider.MapPath("/_thumbs/db.config"));
            Provider.Items["db"] = null;

            // create external script and style files
            foreach (string scriptName in new[] { "cinar_cms_css", "famfamfam_css", "cinar_cms_js", "controls_js", "default_css", "default_js", "en_js", "message_js", "tr_js", "help_html" })
            {
                string s = Properties.Resources.ResourceManager.GetString(scriptName);
                File.WriteAllText(Provider.Server.MapPath("/_thumbs/" + scriptName.Replace("_", ".")), s, Encoding.UTF8);
            }

            File.WriteAllText(Provider.Server.MapPath("/_thumbs/DefaultJavascript.js"), Provider.Configuration.DefaultJavascript, Encoding.UTF8);
            File.WriteAllText(Provider.Server.MapPath("/_thumbs/DefaultStyleSheet.css"), Provider.Configuration.DefaultStyleSheet, Encoding.UTF8);
        }
    }

    public class CMSUtility
    {
        public static string ToJSON(Type type)
        {
            string typeClass = type.IsSubclassOf(typeof(BaseEntity)) ? "Entity" : "Module";
            string typeName = type.Name;
            string _namespace = type.Namespace.IndexOf('.')>-1 ? type.Namespace.Substring(0,type.Namespace.IndexOf('.')) : type.Namespace;
            string displayName = Provider.GetResource(type.Name);
            string description = Provider.GetResource(type.Name + "Desc");

            StringBuilder sb = new StringBuilder();
            sb.Append("{\n");

            sb.AppendFormat("    typeClass: {0},\n", typeClass.ToJS());
            sb.AppendFormat("    typeName: {0},\n", typeName.ToJS());
            sb.AppendFormat("    namespace: {0},\n\n", _namespace.ToJS());

            sb.AppendFormat("    displayName: {0},\n", displayName.ToJS());
            sb.AppendFormat("    description: {0},\n\n", description.ToJS());

            sb.Append("    fields: [\n");

            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi.Name == "Item") continue;
                if (pi.GetSetMethod() == null) continue;

                EditFormFieldPropsAttribute editProps = (EditFormFieldPropsAttribute)CMSUtility.GetAttribute(pi, typeof(EditFormFieldPropsAttribute));
                ColumnDetailAttribute columnProps = (ColumnDetailAttribute)CMSUtility.GetAttribute(pi, typeof(ColumnDetailAttribute));

                if (editProps.ControlType == ControlType.Undefined)
                    editProps.ControlType = Provider.GetDefaultControlType(columnProps.ColumnType, pi, columnProps);
                if (columnProps.ColumnType == DbType.Undefined)
                    columnProps.ColumnType = Column.GetDbTypeOf(pi.PropertyType);
                if (columnProps.ColumnType == DbType.Text)
                    columnProps.Length = Int32.MaxValue;

                string fieldName = pi.Name;
                string fieldType = pi.PropertyType.Name;
                bool isNotNull = columnProps.IsNotNull;
                bool isPrimaryKey = columnProps.IsPrimaryKey;
                string referenceTypeName = columnProps.References != null ? columnProps.References.Name : "";
                long maxLength = columnProps.Length;
                string displayName2 = Provider.GetResource(pi.DeclaringType.Name + "." + pi.Name);
                string description2 = Provider.GetResource(pi.DeclaringType.Name + "." + pi.Name + "Desc");
                string defaultControlType = editProps.ControlType.ToString();

                #region extra control specific metadata
                string options = "";
                switch (editProps.ControlType)
                {
                    case ControlType.StringEdit:
                        options += ",maxLength:" + columnProps.Length;
                        break;
                    case ControlType.IntegerEdit:
                        options += ",maxLength:10";
                        break;
                    case ControlType.DecimalEdit:
                        break;
                    case ControlType.DateTimeEdit:
                        break;
                    case ControlType.PictureEdit:
                        break;
                    case ControlType.CSSEdit:
                        break;
                    case ControlType.MemoEdit:
                        break;
                    case ControlType.FilterEdit:
                        break;
                    case ControlType.ComboBox:
                        if (columnProps.ColumnType == Cinar.Database.DbType.Boolean && editProps.Options.IndexOf("items:") == -1)
                            options += String.Format(",items:[[false,'{0}'],[true,'{1}']]", Provider.GetResource("No"), Provider.GetResource("Yes"));
                        if (columnProps.References != null)
                            options += ",entityName:'" + columnProps.References.Name + "', itemsUrl:'EntityInfo.ashx'";
                        break;
                    case ControlType.LookUp:
                        if (columnProps.References != null)
                            options += ",entityName:'" + columnProps.References.Name + "', itemsUrl:'EntityInfo.ashx'";
                        break;
                    case ControlType.TagEdit:
                        options += ",entityName:'Tag', itemsUrl:'EntityInfo.ashx'";
                        break;
                    default:
                        throw new Exception(Provider.GetResource("{0} type of controls not supported yet.", editProps.ControlType));
                }
                if (!String.IsNullOrEmpty(editProps.Options)) options += ", " + editProps.Options;

                if (!String.IsNullOrEmpty(options))
                    options = ",\n            controlOptions:{" + options.Substring(1) + "}";
                #endregion

                sb.AppendFormat(@"        {{
            fieldName:{0},
            fieldType:{1},
            isNotNull:{2},
            isPrimaryKey:{3},
            referenceTypeName:{4},
            maxLength:{5},
            displayName:{6},
            description:{7},
            defaultControlType:{8}{9}}}," + "\n",
                    fieldName.ToJS(),
                    fieldType.ToJS(),
                    isNotNull.ToJS(),
                    isPrimaryKey.ToJS(),
                    referenceTypeName.ToJS(),
                    maxLength.ToJS(),
                    displayName2.ToJS(),
                    description2.ToJS(),
                    defaultControlType.ToJS(),
                    options);
            }
            sb.Remove(sb.Length - 2, 1);
            sb.Append("    ],\n");

            sb.Append("    relatedTypes: [\n");

            foreach (Type entityType in Provider.GetEntityTypes())
                foreach (PropertyInfo pi in entityType.GetProperties())
                {
                    ColumnDetailAttribute fda = (ColumnDetailAttribute)CMSUtility.GetAttribute(pi, typeof(ColumnDetailAttribute));
                    if (fda.References == type)
                    {
                        string _namespace2 = entityType.Namespace.IndexOf('.')>-1 ? entityType.Namespace.Substring(0,entityType.Namespace.IndexOf('.')) : entityType.Namespace;
                        sb.AppendFormat(@"      {{
            label:{0},
            description:{1},
            namespace:{2},
            typeName:{3},
            fieldName:{4},
            relationType:{5}}}," + "\n",
                            Provider.GetResource(entityType.Name).ToJS(),
                            Provider.GetResource(entityType.Name + "Desc").ToJS(),
                            _namespace2.ToJS(),
                            entityType.Name.ToJS(),
                            pi.Name.ToJS(),
                            fda.ReferenceType.ToString().ToJS());
                    }
                }

            sb.Remove(sb.Length - 2, 1);
            sb.Append("    ]}");

            return Provider.ParseOptions(sb.ToString());
        }

        /// <summary>
        /// Bu HTTP request'ini yapan şey bir robot yazılım mı? Yoksa insan kontrolündeki bir browser yazılımı mı?
        /// </summary>
        public static bool RequestByBot
        {
            get
            {
                List<string> crawlers = new List<string>()
                {
                    "bot","crawler","spider","80legs","baidu","yahoo! slurp","ia_archiver","mediapartners-google",
                    "lwp-trivial","nederland.zoek","ahoy","anthill","appie","arale","araneo","ariadne",            
                    "atn_worldwide","atomz","bjaaland","ukonline","calif","combine","cosmos","cusco",
                    "cyberspyder","digger","grabber","downloadexpress","ecollector","ebiness","esculapio",
                    "esther","felix ide","hamahakki","kit-fireball","fouineur","freecrawl","desertrealm",
                    "gcreep","golem","griffon","gromit","gulliver","gulper","whowhere","havindex","hotwired",
                    "htdig","ingrid","informant","inspectorwww","iron33","teoma","ask jeeves","jeeves",
                    "image.kapsi.net","kdd-explorer","label-grabber","larbin","linkidator","linkwalker",
                    "lockon","marvin","mattie","mediafox","merzscope","nec-meshexplorer","udmsearch","moget",
                    "motor","muncher","muninn","muscatferret","mwdsearch","sharp-info-agent","webmechanic",
                    "netscoop","newscan-online","objectssearch","orbsearch","packrat","pageboy","parasite",
                    "patric","pegasus","phpdig","piltdownman","pimptrain","plumtreewebaccessor","getterrobo-plus",
                    "raven","roadrunner","robbie","robocrawl","robofox","webbandit","scooter","search-au",
                    "searchprocess","senrigan","shagseeker","site valet","skymob","slurp","snooper","speedy",
                    "curl_image_client","suke","www.sygol.com","tach_bw","templeton","titin","topiclink","udmsearch",
                    "urlck","valkyrie libwww-perl","verticrawl","victoria","webscout","voyager","crawlpaper",
                    "webcatcher","t-h-u-n-d-e-r-s-t-o-n-e","webmoose","pagesinventory","webquest","webreaper",
                    "webwalker","winona","occam","robi","fdse","jobo","rhcs","gazz","dwcp","yeti","fido","wlm",
                    "wolp","wwwc","xget","legs","curl","webs","wget","sift","cmc"
                };

                if (string.IsNullOrWhiteSpace(Provider.Request.UserAgent))
                    return true;

                string ua = Provider.Request.UserAgent.ToLower();
                return crawlers.Exists(x => ua.Contains(x));

                //return Provider.Request.UserAgent == null ||
                //    Provider.Request.UserAgent.ToLowerInvariant().Contains("crawler") ||
                //    Provider.Request.UserAgent.ToLowerInvariant().Contains("bot") ||
                //    Provider.Request.UserAgent.ToLowerInvariant().Contains("spider") ||
                //    Provider.Request.UserAgent.ToLowerInvariant().Contains("larbin") ||
                //    Provider.Request.UserAgent.ToLowerInvariant().Contains("search") ||
                //    Provider.Request.UserAgent.ToLowerInvariant().Contains("indexer") ||
                //    Provider.Request.UserAgent.ToLowerInvariant().Contains("archiver") ||
                //    Provider.Request.UserAgent.ToLowerInvariant().Contains("nutch") ||
                //    Provider.Request.UserAgent.ToLowerInvariant().Contains("capture");
            }
        }
        public static string GetRequestFileName()
        {
            return System.IO.Path.GetFileName(Provider.Request.PhysicalPath);
        }

        #region StringUtility
        public static string HtmlEncode(object html)
        {
            if (html == null)
                return "";
            return Provider.Server.HtmlEncode(html.ToString());
        }
        public static string HtmlDecode(object html)
        {
            if (html == null)
                return "";
            return Provider.Server.HtmlDecode(html.ToString());
        }

        public static string MD5(string str)
        {
            //System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            //return Encoding.UTF8.GetString(md5.ComputeHash(Encoding.UTF8.GetBytes(str)), 0, 16);
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(0, 16);
        }

        public static string ClearTextFromWeb(string str)
        {
            if (str == null) return null;
            str = str.Replace("", "'").Replace("", "\"").Replace("", "\""); // anlamsız word karakterleri
            str = Regex.Replace(str, "<br>|<br/>|</p>|</div>", "\n", RegexOptions.Singleline | RegexOptions.IgnoreCase); // br yerine newline
            str = Regex.Replace(str, "<b>", "[#b#]", RegexOptions.Singleline | RegexOptions.IgnoreCase); // bold kalsın
            str = Regex.Replace(str, "</b>", "[#/b#]", RegexOptions.Singleline | RegexOptions.IgnoreCase); // bold kalsın
            str = Regex.Replace(str, "<.+?>", "", RegexOptions.Singleline); // html tagları
            str = str.Replace("[#b#]", "<b>").Replace("[#/b#]", "</b>"); // bold kalsın
            str = Regex.Replace(str, "\n", "</p>\n<p>"); // nl2br
            return HtmlDecode(str.Trim());
        }
        #endregion

        #region ReflectionUtility
        public static object GetAttribute(ICustomAttributeProvider mi, Type attributeType)
        {
            if(mi==null)
                return attributeType.GetConstructor(Type.EmptyTypes).Invoke(null);

            object[] attribs = mi.GetCustomAttributes(attributeType, true);
            object res = null;
            if (attribs.Length > 0) 
                res = attribs[0]; 
            else 
                res = attributeType.GetConstructor(Type.EmptyTypes).Invoke(null);

            // bakalım override edilmiş mi...
            if (mi is PropertyInfo)
            {
                PropertyInfo pi = (PropertyInfo)mi;
                OverrideAttributeAttribute ofa = null;
                foreach (OverrideAttributeAttribute item in pi.ReflectedType.GetCustomAttributes(typeof(OverrideAttributeAttribute), true))
                    if (pi.Name == item.FieldName && item.AttributeType == attributeType)
                    {
                        ofa = item;
                        break;
                    }
                if (ofa != null)
                {
                    string[] fields = ofa.AttribProps.Split('|');
                    string[] values = ofa.NewValues.Split('|');
                    if (fields.Length == values.Length)
                        for (int i = 0; i < fields.Length; i++)
                        {
                            PropertyInfo pInfo = res.GetType().GetProperty(fields[i]);
                            pInfo.SetValue(res, values[i].ChangeType(pInfo.PropertyType), null);
                        }
                }
            }

            return res;
        }
        #endregion


    }

    public class ProviderWrapper
    {
        public Content Content
        {
            get
            {
                return Provider.Content;
            }
        }
        public Configuration Configuration
        {
            get
            {
                return Provider.Configuration;
            }
        }
        public NameValueCollection AppSettings
        {
            get
            {
                return Provider.AppSettings;
            }
        }
        public Lang CurrentLanguage
        {
            get
            {
                return Provider.CurrentLanguage;
            }
        }
        public bool DesignMode
        {
            get
            {
                return Provider.DesignMode;
            }
        }
        public object Request
        {
            get
            {
                if (Provider.Items["httpRequestWrapper"] == null)
                    Provider.Items["httpRequestWrapper"] = new HttpRequestWrapper();
                return Provider.Items["httpRequestWrapper"];
            }
        }
        public HttpServerUtility Server
        {
            get
            {
                return Provider.Server;
            }
        }
        public HttpSessionState Session
        {
            get
            {
                return Provider.Session;
            }
        }
        public User User
        {
            get
            {
                return Provider.User;
            }
        }
        public Tag Tag
        {
            get
            {
                return Provider.Tag;
            }
        }

        public Database.Database Database {
            get {
                return Provider.Database;
            }
        }

        public DataTable GetDataTable(string sql)
        {
            return Provider.Database.GetDataTable(sql);
        }
        public DataTable GetDataTable(string sql, object p1)
        {
            return Provider.Database.GetDataTable(sql, p1);
        }
        public DataTable GetDataTable(string sql, object p1, object p2)
        {
            return Provider.Database.GetDataTable(sql, p1, p2);
        }
        public DataTable GetDataTable(string sql, object p1, object p2, object p3)
        {
            return Provider.Database.GetDataTable(sql, p1, p2, p3);
        }
        public DataTable GetDataTable(string sql, object p1, object p2, object p3, object p4)
        {
            return Provider.Database.GetDataTable(sql, p1, p2, p3, p4);
        }
        public DataRow GetDataRow(string sql)
        {
            return Provider.Database.GetDataRow(sql);
        }
        public DataRow GetDataRow(string sql, object p1)
        {
            return Provider.Database.GetDataRow(sql, p1);
        }
        public DataRow GetDataRow(string sql, object p1, object p2)
        {
            return Provider.Database.GetDataRow(sql, p1, p2);
        }
        public DataRow GetDataRow(string sql, object p1, object p2, object p3)
        {
            return Provider.Database.GetDataRow(sql, p1, p2, p3);
        }
        public DataRow GetDataRow(string sql, object p1, object p2, object p3, object p4)
        {
            return Provider.Database.GetDataRow(sql, p1, p2, p3, p4);
        }
        public object GetValue(string sql)
        {
            return Provider.Database.GetValue(sql);
        }
        public object GetValue(string sql, object p1)
        {
            return Provider.Database.GetValue(sql, p1);
        }
        public object GetValue(string sql, object p1, object p2)
        {
            return Provider.Database.GetValue(sql, p1, p2);
        }
        public object GetValue(string sql, object p1, object p2, object p3)
        {
            return Provider.Database.GetValue(sql, p1, p2, p3);
        }
        public object GetValue(string sql, object p1, object p2, object p3, object p4)
        {
            return Provider.Database.GetValue(sql, p1, p2, p3, p4);
        }
        public int ExecuteNonQuery(string sql)
        {
            return Provider.Database.ExecuteNonQuery(sql);
        }
        public int ExecuteNonQuery(string sql, object p1)
        {
            return Provider.Database.ExecuteNonQuery(sql, p1);
        }
        public int ExecuteNonQuery(string sql, object p1, object p2)
        {
            return Provider.Database.ExecuteNonQuery(sql, p1, p2);
        }
        public int ExecuteNonQuery(string sql, object p1, object p2, object p3)
        {
            return Provider.Database.ExecuteNonQuery(sql, p1, p2, p3);
        }
        public int ExecuteNonQuery(string sql, object p1, object p2, object p3, object p4)
        {
            return Provider.Database.ExecuteNonQuery(sql, p1, p2, p3, p4);
        }
    }

    public class HttpRequestWrapper
    {
        public string this[string key] 
        {
            get {
                return String.IsNullOrEmpty(Provider.Request[key]) ? "" : Provider.Server.HtmlEncode(Provider.Request[key].Replace("'", "''"));
            } 
        }
        public Uri Url 
        {
            get
            {
                return Provider.Request.Url;
            }
        }
    }

    public class CinarUriParser : UriBuilder
    {
        public CinarUriParser(string url)
            : base(url)
        {
            this.queryPart = new QueryParts(this.Query, this);
        }

        public new string Query
        {
            get { return base.Query; }
            set { base.Query = value; queryPart = new QueryParts(value, this); }
        }

        private QueryParts queryPart;
        public QueryParts QueryPart
        {
            get { return queryPart; }
        }

        public class QueryParts
        {
            private Hashtable ht;
            private CinarUriParser parser;

            public QueryParts(string query, CinarUriParser parser)
            {
                this.parser = parser;
                this.ht = new Hashtable();
                if (query.StartsWith("?")) query = query.Substring(1);
                string[] parts = query.Split(new String[] { "&amp;", "&" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    string[] keyAndValue = part.Split('=');
                    ht[keyAndValue[0]] = keyAndValue[1];
                }
            }

            private void rebuildQuery()
            {
                string[] newQueryParts = new string[ht.Count];
                int i = 0;
                foreach (string _key in ht.Keys)
                    newQueryParts[i++] = _key + "=" + ht[_key];
                parser.Query = String.Join("&", newQueryParts);
            }

            public string this[string key]
            {
                get
                {
                    return (string)ht[key];
                }
                set
                {
                    ht[key] = value;
                    rebuildQuery();
                }
            }

            public void Add(string key, string val) {
                this[key] = val;
            }

            public void Remove(string key)
            {
                ht.Remove(key);
                rebuildQuery();
            }
        }
    }
}
