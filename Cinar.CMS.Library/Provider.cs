using System;
using System.Collections.Generic;
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
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Net;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Xml;
using System.Security.Principal;
using System.IO;
using Cinar.Scripting;
using Module = System.Reflection.Module;
using System.Linq;
using DbType = Cinar.Database.DbType;


namespace Cinar.CMS.Library
{
    public class Provider
    {
        public static Database.Database Database
        {
            get
            {
                if (Provider.Items["db"] == null)
                {
                    string sqlCon = Provider.AppSettings["sqlConnection"];
                    DatabaseProvider sqlPro = (DatabaseProvider)Enum.Parse(typeof(DatabaseProvider), Provider.AppSettings["sqlProvider"]);
                    if (sqlCon.Contains("|DataDirectory|")) sqlCon = sqlCon.Replace("|DataDirectory|", MapPath("/App_Data"));
                    Database.Database db = new Database.Database(sqlCon, sqlPro, Provider.MapPath("/_thumbs/db.config"));
                    Provider.Items.Add("db", db);
                    db.NoTransactions = Provider.AppSettings["noTransactions"]=="true";
                }
                return (Database.Database)Provider.Items["db"];
            }
        }

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
            foreach (PropertyInfo pi in obj.GetProperties())
            {
                if (pi.Name == "Item") continue;
                if (pi.GetSetMethod() == null) continue;

                EditFormFieldPropsAttribute editProps = (EditFormFieldPropsAttribute)CMSUtility.GetAttribute(pi, typeof(EditFormFieldPropsAttribute));
                if (!addInvisibles && !editProps.Visible)
                    continue; //***

                editProps.Category = editProps.Category ?? Provider.GetResource((pi.DeclaringType == typeof(NamedEntity) ? obj.GetType() : pi.DeclaringType).Name);
                editProps.OrderNo = ctrlOrderNo++;

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
                string description = Provider.GetResource(pi.DeclaringType.Name + "." + pi.Name + "Desc");

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
                    default:
                        throw new Exception(Provider.GetResource("{0} type of controls not supported yet.", editProps.ControlType));
                }
                if (!String.IsNullOrEmpty(editProps.Options)) options += ", " + editProps.Options;

                if (!String.IsNullOrEmpty(options))
                    options = ", options:{" + options.Substring(1) + "}";

                res.Add("\t{" + String.Format("label:'{0}', description:'{1}', type:'{2}', id:'{3}', category:{4}, orderNo:{5}, value:{6}{7}",
                    caption.ToHTMLString(),
                    description.ToHTMLString(),
                    editProps.ControlType,
                    pi.Name,
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
            if (options.Contains("_AFTERSAVEBEHAVIOR_"))
                options = options.Replace("_AFTERSAVEBEHAVIOR_", GetResource("_AFTERSAVEBEHAVIOR_"));
            if (options.Contains("_UICONTROLTYPE_"))
                options = options.Replace("_UICONTROLTYPE_", GetResource("_UICONTROLTYPE_"));
            if (options.Contains("_MODERATED_"))
                options = options.Replace("_MODERATED_", GetResource("_MODERATED_"));
            if (options.Contains("_WHICHPICTURE_"))
                options = options.Replace("_WHICHPICTURE_", GetResource("_WHICHPICTURE_"));
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
        public static Assembly GetAssembly(string assemblyName)
        {
            if (assemblies[assemblyName] == null)
                assemblies[assemblyName] = Assembly.Load(assemblyName);
            return (Assembly)assemblies[assemblyName];
        }
        
        public static IDatabaseEntity[] GetIdNameList(string entityName, string extraWhere, string simpleWhere)
        {
            Type tip = Provider.GetEntityType(entityName);
            BaseEntity sampleEntity = Provider.CreateEntity(entityName);

            extraWhere = extraWhere.Replace("_nameField_", sampleEntity.GetNameColumn());

            FilterParser filterParser = new FilterParser(extraWhere, entityName);
            extraWhere = filterParser.GetWhere();

            string where = "where " + (String.IsNullOrEmpty(simpleWhere) ? "1=1" : simpleWhere) + (String.IsNullOrEmpty(extraWhere) ? "" : " AND (" + extraWhere + ")");

            IDatabaseEntity[] entities = Provider.Database.ReadList(tip, "select Id, [" + sampleEntity.GetNameColumn() + "] from [" + entityName + "]" + where + " order by [" + sampleEntity.GetNameColumn() + "]", filterParser.GetParams());
            return entities;
        }
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
                    HttpContext.Current.RewritePath("Default.aspx");
                }

                // if language is specified by the browser, use it:
                string lang = Provider.Request.UserLanguages[0];
                if (lang.Contains("-"))
                    lang = lang.Substring(0, lang.IndexOf('-')); // Here we omit the country code while we shouldn't.. 
                Lang l = (Lang)Provider.Database.Read(typeof(Lang), "Code like {0}", lang + "%");
                if (l != null)
                    Provider.CurrentCulture = l.Code;

                Provider.SetHttpContextUser();
            }
        }

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
        public static bool DevelopmentMode
        {
            get {
                return AppSettings["developmentMode"] == "true";
            }
        }
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
        public static void SetHttpContextUser()
        {
            if (Provider.Session["User"] == null)
            {
                Entities.User user = (Entities.User)Provider.Database.Read(typeof(Entities.User), "Email='anonim'");
                Provider.ContextUser = new GenericPrincipal(new GenericIdentity(user.Email), user.Roles.Split(','));
                Provider.Session["User"] = user;
            }
            else
            {
                Provider.ContextUser = new GenericPrincipal(new GenericIdentity(Provider.User.Email), Provider.User.Roles.Split(','));
            }
        }
        public static string CurrentCulture
        {
            get
            {
                string lang = (string)Provider.Session["currentCulture"];

                if (lang == null)
                {
                    lang = (string)Provider.Database.GetValue("select Code from Lang where Id={0}", Provider.Configuration.DefaultLang);
                    if (lang == null)
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
                Provider.Session["currentCulture"] = value;
            }
        }
        public static Lang CurrentLanguage 
        {
            get {
                if (Provider.Items["currentLang"] == null)
                    Provider.Items["currentLang"] = Provider.Database.Read(typeof(Lang), "Code={0}", CurrentCulture);
                return (Lang)Provider.Items["currentLang"];
            }
        }
        private static Configuration configuration;
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
        public static Content Content
        {
            get
            {
                if (Provider.Items["item"] == null && Provider.Session["item"] != null)
                {
                    IDatabaseEntity[] items = Provider.Database.ReadList(typeof(Content), "select * from [Content] where Id={0}", Provider.Session["item"]);
                    if(items==null)
                        items = Provider.Database.ReadList(typeof(Content), "select * from [Content] where Id={0}", 1);
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
        public static int PreviousContentId
        {
            get
            {
                if (Items["previousContentId"] == null)
                {
                    string tagSQL = "";
                    if (Provider.Tag != null && Provider.Tag.Id>0)
                        tagSQL = " AND Id in (SELECT ContentId FROM ContentTag WHERE TagId = "+Provider.Tag.Id+")";
                    Items["previousContentId"] = Provider.Database.GetInt("select Id from Content where Id<{0} AND CategoryId={1} " + tagSQL + " order by Id desc limit 1", Content.Id, Content.CategoryId);
                }
                return (int)Items["previousContentId"];
            }
        }
        public static int NextContentId
        {
            get
            {
                if (Items["nextContentId"] == null)
                {
                    string tagSQL = "";
                    if (Provider.Tag != null && Provider.Tag.Id > 0)
                        tagSQL = " AND Id in (SELECT ContentId FROM ContentTag WHERE TagId = " + Provider.Tag.Id + ")";
                    Items["nextContentId"] = Provider.Database.GetInt("select Id from Content where Id>{0} AND CategoryId={1} " + tagSQL + " order by Id limit 1", Content.Id, Content.CategoryId);
                }
                return (int)Items["nextContentId"];
            }
        }

        /// <summary>
        /// Request["tag"] veya Request["tagId"] parametrelerini kullanarak aktif tag'ı döndürür
        /// </summary>
        public static Tag Tag
        {
            get
            {
                if (Provider.Items["tag"] == null)
                {
                    if (!String.IsNullOrEmpty(Provider.Request["tagId"]))
                    {
                        IDatabaseEntity[] items = Provider.Database.ReadList(typeof(Tag), "select * from [Tag] where Id={0}", Provider.Request["tagId"]);
                        Provider.Translate(items);
                        Provider.Items["tag"] = items.Length == 1 ? items[0] : null;
                    }
                    else if (!String.IsNullOrEmpty(Provider.Request["tag"]))
                    {
                        IDatabaseEntity[] items = Provider.Database.ReadList(typeof(Tag), "select * from [Tag] where Name={0}", Provider.Request["tag"]);
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

        public static Content UploadContent()
        {
            try
            {
                Content content = (Content)Provider.CreateEntity("Content");
                content.SetFieldsByPostData(Provider.Request.Form);

                if (content.ClassName == "Article")
                {
                    string authorName = Provider.Request.Form["Author"];
                    if (string.IsNullOrEmpty(authorName))
                    {
                        content.AuthorId = 0;
                    }
                    else
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
                }

                string sourceName = Provider.Request.Form["Source"];
                if (string.IsNullOrEmpty(sourceName))
                {
                    content.SourceId = 0;
                }
                else
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
                if (string.IsNullOrEmpty(categoryTitle))
                {
                    content.CategoryId = 1;
                    content.Visible = false;
                }
                else
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

                return content;
            }
            catch (Exception ex)
            {
                Provider.Log("Error", "UploadContent", Provider.ToString(ex, true, true, true));
                return null;
            }
        }

        public static string BuildPath(string fileName, string specialFolder, bool useYearMonthDateFolders)
        {
            string subFolders = "";
            if (useYearMonthDateFolders)
            {
                string year = DateTime.Now.Year.ToString();
                string month = DateTime.Now.Month.ToString();
                string day = DateTime.Now.Day.ToString();
                if (!System.IO.Directory.Exists(Provider.MapPath(Provider.AppSettings[specialFolder] + "/" + year)))
                    System.IO.Directory.CreateDirectory(Provider.MapPath(Provider.AppSettings[specialFolder] + "/" + year));
                if (!System.IO.Directory.Exists(Provider.MapPath(Provider.AppSettings[specialFolder] + "/" + year + "/" + month)))
                    System.IO.Directory.CreateDirectory(Provider.MapPath(Provider.AppSettings[specialFolder] + "/" + year + "/" + month));
                if (!System.IO.Directory.Exists(Provider.MapPath(Provider.AppSettings[specialFolder] + "/" + year + "/" + month + "/" + day)))
                    System.IO.Directory.CreateDirectory(Provider.MapPath(Provider.AppSettings[specialFolder] + "/" + year + "/" + month + "/" + day));
                subFolders = "/" + year + "/" + month + "/" + day;
            }
            return Provider.AppSettings[specialFolder] + subFolders + "/" + fileName;
        }

        public static string GetRegionInnerHtml(string template, string region)
        {
            var modules = new List<Modules.Module>(Modules.Module.Read(template, region));
            return GetRegionInnerHtml(modules);
        }
        public static string GetRegionInnerHtml(List<Modules.Module> modules)
        {
            return Provider.GetRegionInnerHtml(modules, true);
        }
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

            return false;
        }
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
            return false;
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

        public static void Translate(IDatabaseEntity[] entities)
        {
            if (Provider.CurrentLanguage.Id == Configuration.DefaultLang || entities == null || entities.Length == 0)
                return; //*** aynı dil çevirmeye gerek yok.

            string entityName = entities[0].GetType().Name;

            if (Provider.Database.Tables[entityName + "Lang"] == null)
                return; //*** dil tablosu yok

            int langId = (int)Provider.Database.GetValue("select Id from Lang where Code={0}", CurrentCulture);
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
                        if (pi == null || drLang.IsNull(dc) || drLang[dc].ToString()=="")
                            continue; //***
                        pi.SetValue(relatedEntity, drLang[dc], null);
                    }
            }
        }

        public static void Translate(string entityName, DataTable dt)
        {
            if (Provider.CurrentLanguage.Id == Configuration.DefaultLang || dt == null || dt.Rows.Count == 0)
                return; //*** aynı dil çevirmeye gerek yok.

            if (Provider.Database.Tables[entityName + "Lang"] == null)
                return; //*** dil tablosu yok

            int langId = (int)Provider.Database.GetValue("select Id from Lang where Code={0}", CurrentCulture);
            ArrayList alIds = new ArrayList();
            foreach(DataRow dr in dt.Rows) { alIds.Add(dr["Id"].ToString()); }
            string ids = String.Join(",", (string[])alIds.ToArray(typeof(string)));

            DataTable dtLang = Provider.Database.GetDataTable("select * from " + entityName + "Lang where " + entityName + "Id in (" + ids + ") and LangId={0}", langId);

            foreach (DataRow drLang in dtLang.Rows)
            {
                DataRow relatedRow = null;
                foreach(DataRow dr in dt.Rows)
                    if(drLang[entityName + "Id"].Equals(dr["Id"]))
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
        }

        #region Resource
        /*
        private static Hashtable _resources = new Hashtable();
        public static Hashtable StaticResources(string lang)
        {
            if (_resources[lang] != null)
                return (Hashtable)_resources[lang];

            Hashtable ht = new Hashtable();

            //try
            //{
            //    XmlDocument doc = new XmlDocument();
            //    doc.Load(Provider.MapPath("/external/lang/" + lang + ".xml"));

            //    foreach (XmlNode node in doc.SelectNodes("/resources/entry"))
            //        ht[node.Attributes["key"].Value] = node.Attributes["value"].Value;
            //}
            //catch 
            //{
 
            //}

            _resources[lang] = ht;

            return ht;
        }
        */
        public static string GetResource(string code, params object[] args)
        {
            string lang = CurrentCulture.Split('-')[0];

            string str = lang == "tr" ? (StaticResources.tr.ContainsKey(code) ? StaticResources.tr[code] : null) : (StaticResources.en.ContainsKey(code) ? StaticResources.en[code] : null);

            return str == null ? "? " + String.Format(code, args) : String.Format(str, args);
        }

        //TODO: Bu resource stringleri veritabanına taşıyalım, değiştirilmesine izin verelim
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
                ht["Enter your email address and click send button. You will receive your special adress where you can change your password."] = "E-Posta adresinizi girip tamama basınız. Şifrenizi değiştirebileceğiniz size özel adres e-posta adresinize gönderilecektir.";
                ht["There isn't any user with the email address you entered. Please check."] = "Bu email adresine sahip bir kullanıcı yok. Lütfen adresinizi doğru yazdığınızdan emin olunuz.";
                ht["Please change your password by using the address below"] = "Aşağıdaki linki kullanarak şifrenizi yenileyebilirsiniz.";
                ht["Your Password"] = "Şifre hatırlatma";
                ht["A message sent to your email address. Please read it."] = "Email adresinize mesaj gönderildi. Lütfen bu mesajda yazılanları uygulayınız.";
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
                    ht[code] = "?!! " + code;
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
            
            if (string.IsNullOrEmpty(orderBy) || orderBy.Trim()=="")
                orderBy = orderByDefault;

            if (orderBy.Contains(" "))
            {
                string[] parts = orderBy.Split(' ');
                orderBy = "[" + parts[0].Trim('[', ']') + "] " + parts[1];
            }
            else
                orderBy = "[" + orderBy.Trim().Trim('[', ']') + "]";

            sql += " order by " + orderBy;
            sql += " limit " + limit + " offset " + (limit * pageIndex);

            dt = Provider.Database.ReadTable(entityType, sql, parameters);
            return dt;
        }
        public static int ReadListTotalCount(Type entityType, string where, object[] parameters)
        {
            string sql = "select count(*)  from [" + entityType.Name + "] " + (String.IsNullOrEmpty(where) ? "" : ("where " + where));

            return Provider.Database.GetInt(sql, parameters);
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
        public static string GetThumbPath(string imageUrl, int prefWidth, int prefHeight, bool cropPicture)
        {
            if (String.IsNullOrEmpty(imageUrl))
            {
                imageUrl = Provider.Configuration.NoPicture;
                if (String.IsNullOrEmpty(imageUrl))
                    return Provider.DesignMode ? "ERR: " + Provider.GetResource("No picture. And NoPicture image not specified in configuration.") : "ERR: ";
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

            string thumbUrl = "/_thumbs/" + prefWidth + "x" + prefHeight + "_" + imageUrl.Replace("/","_");
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
        public static NameValueCollection AppSettings
        {
            get
            {
                    return System.Web.Configuration.WebConfigurationManager.AppSettings;
            }
        }
        public static HttpServerUtility Server
        {
            get
            {
                return HttpContext.Current.Server;
            }
        }
        public static HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }
        public static HttpResponse Response
        {
            get
            {
                return HttpContext.Current.Response;
            }
        }
        public static HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }
        public static IDictionary Items
        {
            get { return HttpContext.Current.Items; }
        }
        public static HttpApplicationState Application
        {
            get { return HttpContext.Current.Application; }
        }
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
        public static string MapPath(string path)
        {
            if (AppSettings.AllKeys.Contains("MapPathPrefix"))
                path = AppSettings["MapPathPrefix"] + path;
            return Provider.Server.MapPath(path);
        }
        #endregion


        #region SendMail
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
        public static string SendMail(string from, string to, string subject, string message)
        {
            return Provider.SendMail(from, from, to, to, subject, message);
        }
        public static string SendMail(string to, string subject, string message)
        {
            return Provider.SendMail(null, null, to, to, subject, message);
        }
        public static string SendMail(string subject, string message)
        {
            return Provider.SendMail(null, null, Provider.Configuration.AuthEmail, Provider.Configuration.SiteName, subject, message);
        }
        #endregion

        #region FetchAutoContent
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
            if(req.Proxy!=null)
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

        public static void Log(string logType, string category, string description)
        {
            Log log = new Log();
            log.LogType = logType;
            log.Category = category;
            log.Description = description;
            log.Save();
        }

        public static Interpreter GetInterpreter(string template, object forThis)
        {
            Interpreter engine = new Interpreter(template, new List<string>() { "Cinar.CMS.Library", "Cinar.CMS.Library.Entities", "Cinar.CMS.Library.Modules", "Cinar.CMS.Library.Handlers" });
            engine.AddAssembly(typeof(Provider).Assembly);
            engine.AddAssembly(typeof(Utility).Assembly);
            if (!String.IsNullOrEmpty(Provider.AppSettings["customAssemblies"]))
                foreach (string customAssembly in Provider.AppSettings["customAssemblies"].SplitWithTrim(','))
                {
                    Assembly assembly = GetAssembly(customAssembly);
                    if (assembly == null)
                        continue;
                    engine.AddAssembly(assembly);
                }

            engine.SetAttribute("Context", new ProviderWrapper());
            engine.SetAttribute("this", forThis);

            return engine;
        }

        public static string GetPageUrl(string template, int id, string categoryTitle, string contentTitle)
        {
            if (Provider.DesignMode)
                return string.Format(
                    "/{0}?item={1}",
                    template,
                    id);

            return string.Format(
                    "/{0}/{1}/{2}_{3}.aspx",
                    template.Replace(".aspx", "").ToLowerInvariant(),
                    categoryTitle.MakeFileName().ToLowerInvariant(),
                    contentTitle.MakeFileName().ToLowerInvariant(),
                    id);
        }

        public static string GetPageUrl(string template, int contentId)
        {
            Content content = Provider.Database.Read<Entities.Content>(contentId);
            if (content != null)
                return Provider.GetPageUrl(Provider.GetTemplate(content, ""), content.Id, content.Category.Title, content.Title);
            else
                return "javascript:alert('No such content'); return false;";
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
                return Provider.Request.UserAgent == null ||
                    Provider.Request.UserAgent.ToLowerInvariant().Contains("crawler") ||
                    Provider.Request.UserAgent.ToLowerInvariant().Contains("bot") ||
                    Provider.Request.UserAgent.ToLowerInvariant().Contains("spider") ||
                    Provider.Request.UserAgent.ToLowerInvariant().Contains("larbin") ||
                    Provider.Request.UserAgent.ToLowerInvariant().Contains("search") ||
                    Provider.Request.UserAgent.ToLowerInvariant().Contains("indexer") ||
                    Provider.Request.UserAgent.ToLowerInvariant().Contains("archiver") ||
                    Provider.Request.UserAgent.ToLowerInvariant().Contains("nutch") ||
                    Provider.Request.UserAgent.ToLowerInvariant().Contains("capture");
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
            object[] attribs = mi.GetCustomAttributes(attributeType, true);
            object res = null;
            if (attribs.Length > 0) res = attribs[0]; else res = attributeType.GetConstructor(Type.EmptyTypes).Invoke(null);

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
                            pInfo.SetValue(res, Convert.ChangeType(values[i], pInfo.PropertyType), null);
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

    public class UriParser : UriBuilder
    {
        public UriParser(string url)
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
            private UriParser parser;

            public QueryParts(string query, UriParser parser)
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

            public void Remove(string key)
            {
                ht.Remove(key);
                rebuildQuery();
            }
        }
    }
}
