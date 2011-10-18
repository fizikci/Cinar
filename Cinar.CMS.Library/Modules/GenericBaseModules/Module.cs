using System;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.CMS.Library.Handlers;
using Cinar.Database;
using System.Reflection;
using System.Data;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Modules
{
    [DefaultData(ColumnList = "Name,Template,Region,Details", ValueList = "'LoginForm','Main.aspx','Content','<?xml version=\"1.0\" encoding=\"utf-16\"?><LoginForm xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Id>1</Id><Template>Login.aspx</Template><Region>Content</Region><OrderNo>1</OrderNo><Name>LoginForm</Name><CSS/></LoginForm>'")]
    public abstract class Module : ObjectWithTags, IDatabaseEntity
    {
        #region fields
        private int id = -1;
        private string template;
        private string region;
        private int orderNo;
        private int parentModuleId;
        private string name;
        private string css = "";
        private string details;
        private string cssClass = "";
        #endregion

        public virtual void Initialize()
        {
        }

        internal ASPXHandler ContainerPage = null;
        internal bool editable = true;

        #region Properties
        [ColumnDetail(IsAutoIncrement = true, IsNotNull = true, IsPrimaryKey = true), EditFormFieldProps(Visible = false)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [ColumnDetail(IsNotNull = true, Length = 20), EditFormFieldProps(Visible = false)]
        public string Template
        {
            get { return template; }
            set { template = value; }
        }

        [ColumnDetail(IsNotNull = true, Length = 20), EditFormFieldProps(Visible = false)]
        public string Region
        {
            get { return region; }
            set { region = value; }
        }

        [ColumnDetail(IsNotNull = true, DefaultValue = "0"), EditFormFieldProps(Visible = false)]
        public int OrderNo
        {
            get { return orderNo; }
            set { orderNo = value; }
        }

        [ColumnDetail(IsNotNull = true, Length = 30), EditFormFieldProps(Visible = false)]
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(this.name))
                    this.name = this.GetType().Name;
                return this.name;
            }
            set { this.name = value; }
        }

        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.CSSEdit)]
        public string CSS
        {
            get { return css; }
            set { css = value; }
        }

        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(Visible = false)]
        public string Details
        {
            get { return details; }
            set { details = value; }
        }

        private string topHtml = "";
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string TopHtml
        {
            get { return topHtml; }
            set { topHtml = value; }
        }

        private string bottomHtml = "";
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string BottomHtml
        {
            get { return bottomHtml; }
            set { bottomHtml = value; }
        }

        [ColumnDetail(IsNotNull = true, DefaultValue = "0"), EditFormFieldProps(Visible = false)]
        public int ParentModuleId
        {
            get { return parentModuleId; }
            set { parentModuleId = value; }
        }

        public string CSSClass
        {
            get
            {
                return this.cssClass;
            }
            set { this.cssClass = value; }
        }
        #endregion

        public virtual string GetNameValue()
        {
            return "No name value!";
        }
        public virtual string GetNameColumn()
        {
            return "No name field!";
        }

        private string toHtml() 
        {
            string strShow = this.show();
            if (String.IsNullOrEmpty(strShow))
                return String.Empty;
            else
                return this.topHtml + strShow + this.bottomHtml;
        }

        public string Show()
        {
            string html = "";
            try
            {
                //TODO: bunu düşün ve hallet
                if (this.cacheLifeTime == 0)
                    this.cacheLifeTime = Provider.Configuration.CacheLifeTime;

                //this.beforeShow(); // bu satır, beforeShow'un bütün modüllerin show()'undan önce çağrılması için BasePage.ProcessRequest'e taşındı

                if (this.canBeCachedInternal())
                    html = this.readFromCache();
                else
                    html = this.toHtml();

                if (html.Trim() == String.Empty)
                {
                    if (Provider.DesignMode)
                        html = Provider.GetResource("Empty content");
                    else
                        return "";
                }
                //this.afterShow(); // bu satır, afterShow'un bütün modüllerin show()'undan sonra çağrılması için BasePage.Render'a taşındı
            } catch (Exception ex){
                html = Provider.ToString(ex, true);
            }
            string tooltip = String.Format(" title=\"Name: {0}, Id: {1}, Region: {2}\"", Provider.GetResource(this.name), this.id, this.region);
            return String.Format("<div id=\"{0}_{1}\" class=\"{3}{0}{5}\"{4}>{2}</div>\n",
                this.Name,
                this.Id,
                html,
                this.editable ? "Module " : "",
                Provider.DesignMode ? tooltip : "",
                this.cssClass != "" ? " " + this.cssClass : "");
        }

        protected abstract string show();
        protected internal virtual void beforeShow() { }
        protected internal virtual void afterShow() { }

        public virtual string GetDefaultCSS()
        {
            return String.Format("#{0}_{1} {{}}\n", this.Name, this.Id);
        }

        public string GetPropertyEditorJSON()
        {
            return Provider.GetPropertyEditorJSON(this, false);
        }

        public void Save()
        {

            try
            {
                Provider.Database.Begin();

                bool savedBefore = (Id > 0);

                this.beforeSave(savedBefore);

                if (savedBefore)
                {
                    Provider.Database.ExecuteNonQuery(
                        "update Module set Template={2}, Region={3}, OrderNo={4}, CSS={5}, Details={6}, ParentModuleId={7}, UseCache={8} where Id={0} and Name={1}", 
                        Id, 
                        Name, 
                        Template, 
                        Region, 
                        OrderNo, 
                        CSS, 
                        this.Serialize(),
                        ParentModuleId,
                        UseCache);

                    this.deleteCache();
                }
                else
                {
                    this.OrderNo = Convert.ToInt32(Provider.Database.GetValue("select max(OrderNo)+1 from Module where Template={0} and Region={1}", this.Template, this.Region));
                    Provider.Database.ExecuteNonQuery(
                        "insert into Module (Name, Template, Region, OrderNo, CSS, ParentModuleId, UseCache) values ({0},{1},{2},{3},{4},{5},{6})", 
                        Name, 
                        Template, 
                        Region, 
                        OrderNo, 
                        CSS, 
                        ParentModuleId,
                        UseCache);
                    this.Id = (int)Provider.Database.GetValue("select max(Id) from Module");
                    Provider.Database.ExecuteNonQuery(
                        "update Module set Details = {0} where Id={1}", 
                        this.Serialize(), 
                        this.Id);
                }

                this.afterSave(savedBefore);

                Provider.Database.Commit();
            }
            catch (Exception ex)
            {
                Provider.Database.Rollback();
                throw ex;
            }
        }
        protected virtual void beforeSave(bool isUpdate) { }
        protected virtual void afterSave(bool isUpdate) { }

        public string Serialize()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Cinar.CMS.Serialization\n");
            foreach (PropertyInfo pi in Utility.GetProperties(this))
            {
                if (pi.Name == "Item") continue;
                if (pi.GetSetMethod() == null) continue;

                object val = pi.GetValue(this, null);
                if (val != null)
                {
                    string valStr = val.ToString();
                    sb.AppendFormat("{0},{1},{2}", pi.Name, valStr.Length, valStr);
                }
            }
            return sb.ToString();
        }
        public static Module Deserialize(string moduleName, string moduleData)
        {
            XmlSerializer ser = null;
            moduleData = moduleData.Trim();
            Module module = null;
            try
            {
                if(!moduleData.StartsWith("Cinar.CMS.Serialization"))
                    ser = new XmlSerializer(Provider.GetModuleType(moduleName));
                module = Provider.CreateModule(moduleName);
                try
                {
                    if (!moduleData.StartsWith("Cinar.CMS.Serialization"))
                        module = (Module)ser.Deserialize(new System.IO.StringReader(moduleData));
                    else
                    {
                        moduleData = moduleData.Substring("Cinar.CMS.Serialization\n".Length);
                        while (moduleData.Length > 0) 
                        {
                            string propName = moduleData.Substring(0, moduleData.IndexOf(','));
                            moduleData = moduleData.Substring(propName.Length+1);
                            string valLengthStr = moduleData.Substring(0, moduleData.IndexOf(','));
                            moduleData = moduleData.Substring(valLengthStr.Length + 1);
                            int length = Int32.Parse(valLengthStr);
                            string valStr = moduleData.Substring(0, length);
                            moduleData = moduleData.Substring(length);

                            PropertyInfo pi = module.GetType().GetProperty(propName);
                            pi.SetValue(module, Convert.ChangeType(valStr, pi.PropertyType), null);
                        }
                    }
                }
                catch
                {
                    module.TopHtml = Provider.GetResource("Error while deserializing the module. This may be because module changed or database charset problem");
                }
            }
            catch 
            {
                module = new StaticHtml();
                ((StaticHtml)module).InnerHtml = "<font color=red>Hata</font><br/><br/>";
                ((StaticHtml)module).BottomHtml = moduleName + " isimli bu modül bulunamadı, bu modül türü silinmiş olabilir. Bu modülü siliniz.";
                if(Provider.DevelopmentMode)
                    ((StaticHtml)module).BottomHtml += "<br/><br/><b>Developer'a not:</b> Bu hata modül deserialize edilemediği zaman oluşur.";
            }
            return module;
        }

        public void Delete()
        {
            try
            {
                Provider.Database.Begin();

                this.beforeDelete();

                Module[] modules = Module.Read(Provider.Database.GetDataTable("select * from [Module] where ParentModuleId = " + this.Id + ";"));
                foreach (Module module in modules)
                    module.Delete();

                Provider.Database.ExecuteNonQuery("delete from [Module] where Id=" + this.Id + " and Name='" + this.Name + "'");
                this.afterDelete();
                this.deleteCache();

                Provider.Database.Commit();
            }
            catch (Exception ex)
            {
                Provider.Database.Rollback();
                throw ex;
            }
        }
        protected virtual void beforeDelete() { }
        protected virtual void afterDelete() { }

        public static Module CreateModule(string moduleName, string template, string region, int parentModuleId)
        {
            Module module = Provider.CreateModule(moduleName);
            module.Template = template;
            module.Region = region;
            module.ParentModuleId = parentModuleId;
            module.OrderNo = Convert.ToInt32(Provider.Database.GetValue(String.Format("select count(*)+1 from Module where Template='{0}' and Region='{1}'", module.Template, module.Region)));

            return module;
        }

        public static Module[] Read(string template, string region)
        {
            return Read(Provider.Database.ReadTable(typeof(Module), "select * from Module where Template={0} and Region={1} and ParentModuleId=0 order by OrderNo", template, region));
        }
        public static Module[] Read(string template)
        {
            return Read(Provider.Database.ReadTable(typeof(Module), "select * from Module where Template={0} and ParentModuleId=0 order by OrderNo", template));
        }
        public static Module[] Read(DataTable dtModules)
        {
            if (dtModules == null || dtModules.Rows.Count == 0)
                return new Module[0]; //***

            Module[] modules = new Module[dtModules.Rows.Count];

            for (int i = 0; i < dtModules.Rows.Count; i++)
                modules[i] = fromDataRow(dtModules.Rows[i]);

            return modules;
        }
        private static Module fromDataRow(DataRow drModule)
        {
            Module module = Deserialize(drModule["Name"].ToString(), drModule["Details"].ToString());
            module.Id = (int)drModule["Id"];
            module.Name = drModule["Name"].ToString();
            module.Template = drModule["Template"].ToString();
            module.Region = drModule["Region"].ToString();
            module.OrderNo = Convert.ToInt32(drModule["OrderNo"]);
            module.CSS = drModule["CSS"].ToString();
            module.ParentModuleId = Convert.ToInt32(drModule["ParentModuleId"]);
            module.UseCache = drModule.Table.Columns.Contains("UseCache") ? drModule["UseCache"].ToString() : "Default";
            return module;
        }

        public static Module Read(string moduleName, int id)
        {
            DataRow drModule = Provider.Database.GetDataRow("select * from [Module] where Id=" + id);
            return fromDataRow(drModule);
        }

        #region Cache olayı
        public string CacheHint = "no cache";

        protected string useCache = "Default";
        [ColumnDetail(IsNotNull = true, Length = 30, DefaultValue = "Default"), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:_USECACHE_")]
        public string UseCache
        {
            get { return useCache; }
            set { useCache = value; }
        }

        protected int cacheLifeTime = 0;
        [EditFormFieldProps()]
        public int CacheLifeTime
        {
            get { return cacheLifeTime; }
            set { cacheLifeTime = value; }
        }

        protected virtual bool canBeCachedInternal()
        {
            bool _useCache = false;
            if (this.useCache == "Default")
                _useCache = Provider.Configuration.UseCache != "False";
            else
                _useCache = this.useCache != "False";

            return this.cacheLifeTime > 0 && _useCache;
        }
        private string readFromCache()
        {
            if (useCache == "Default")
                useCache = Provider.Configuration.UseCache;
            ModuleCache cache = (ModuleCache)Provider.Database.Read(typeof(ModuleCache), "ModuleId={0}" + (useCache == "Item" ? " AND ContentId={1}" : "") + " AND LangId={2}", this.Id, Provider.Content.Id, Provider.CurrentLanguage.Id);
            
            DateTime lastCached = new DateTime(1900, 1, 1);
            if (cache != null)
                lastCached = cache.UpdateDate;
            int cacheDuration = Convert.ToInt32(((TimeSpan)(DateTime.Now - lastCached)).TotalMinutes);

            try
            {
                string html = "";
                if (cacheDuration > this.cacheLifeTime)
                {
                    html = this.toHtml();
                    if (cache == null)
                        cache = new ModuleCache();
                    cache.ContentId = Provider.Content.Id;
                    cache.CachedHTML = html;
                    cache.LangId = Provider.CurrentLanguage.Id;
                    cache.ModuleId = this.Id;
                    cache.Save();

                    this.CacheHint = "cached now";
                }
                else
                {
                    html = cache.CachedHTML;
                    this.CacheHint = "cached at " + cache.UpdateDate;
                }
                return html;
            }
            catch (Exception ex)
            {
                return Provider.DesignMode ? "<div><b>" + Provider.GetResource("Cache problem") + "!</b><br/>" + ex.Message + "<br/>" + this.toHtml() : this.toHtml();
            }
        }
        private void deleteCache()
        {
            if(Provider.Database.Tables["ModuleCache"]!=null)
                Provider.Database.ExecuteNonQuery("delete from ModuleCache where ModuleId={0}", this.Id);
        }
        #endregion

        public void SaveACopyFor(string templateName)
        {
            Module copy = Module.Deserialize(this.Name, this.Serialize());

            Provider.Database.Begin();
            try
            {
                copy.Id = 0;
                copy.Template = templateName;
                copy.Save();
                copy.CSS = copy.CSS.Replace("_" + this.Id, "_" + copy.Id);
                copy.Save();

                if (this is ModuleContainer)
                {
                    ModuleContainer iCont = this as ModuleContainer;
                    if (iCont.ChildModules != null && iCont.ChildModules.Count > 0)
                        foreach (Module child in iCont.ChildModules)
                        {
                            child.Region = child.Region.Replace(child.ParentModuleId.ToString(), copy.Id.ToString());
                            child.ParentModuleId = copy.Id;
                            child.SaveACopyFor(templateName);
                        }
                }

                Provider.Database.Commit();
            }
            catch (Exception ex)
            {
                Provider.Database.Rollback();
                throw ex;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", this.Name, this.Id);
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ExecutableByClientAttribute : Attribute
    {
        public ExecutableByClientAttribute(bool executable)
        {
            this.executable = executable;
        }

        private bool executable;

        public bool Executable
        {
            get { return executable; }
            set { executable = value; }
        }

    }

}
