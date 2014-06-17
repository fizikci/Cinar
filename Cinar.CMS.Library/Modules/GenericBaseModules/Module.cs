using System;
using System.Net;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.CMS.Library.Handlers;
using Cinar.Database;
using System.Reflection;
using System.Data;
using System.Xml.Serialization;
using Cinar.Scripting;

namespace Cinar.CMS.Library.Modules
{
    [DefaultData(ColumnList = "OrderNo,Name,Template,Region,Details", ValueList = @"0,'StaticHtml','Default.aspx','topNav','Cinar.CMS.Serialization
InnerHtml,1514,        <button type=""button"" class=""navbar-toggle"" data-toggle=""collapse"" data-target="".nav-collapse"">
          <span class=""icon-bar""></span>
          <span class=""icon-bar""></span>
          <span class=""icon-bar""></span>
        </button>
        <a class=""navbar-brand"" href=""#"">Project name</a>
        <div class=""nav-collapse collapse"">
          <ul class=""nav navbar-nav"">
            <li class=""active""><a href=""#"">Home</a></li>
            <li><a href=""#about"">About</a></li>
            <li><a href=""#contact"">Contact</a></li>
            <li class=""dropdown"">
              <a href=""#"" class=""dropdown-toggle"" data-toggle=""dropdown"">Dropdown <b class=""caret""></b></a>
              <ul class=""dropdown-menu"">
                <li><a href=""#"">Action</a></li>
                <li><a href=""#"">Another action</a></li>
                <li><a href=""#"">Something else here</a></li>
                <li class=""divider""></li>
                <li class=""nav-header"">Nav header</li>
                <li><a href=""#"">Separated link</a></li>
                <li><a href=""#"">One more separated link</a></li>
              </ul>
            </li>
          </ul>
          <form class=""navbar-form form-inline pull-right"">
            <input type=""text"" placeholder=""Email"" class=""form-control"">
            <input type=""password"" placeholder=""Password"" class=""form-control"">
            <button type=""submit"" class=""btn btn-primary btn-small"">Sign in</button>
          </form>
        </div><!--/.nav-collapse -->LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,1Template,12,Default.aspxRegion,6,topNavOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0'")]
    [DefaultData(ColumnList = "OrderNo,Name,Template,Region,Details", ValueList = @"1,'StaticHtml','Default.aspx','jumbo','Cinar.CMS.Serialization
InnerHtml,313,<h1>Hello, world!</h1>
<p>This is a template for a simple marketing or informational website. It includes a large callout called the hero unit and three supporting pieces of content. Use it as a starting point to create something more unique.</p>
<p><a class=""btn btn-primary btn-large"">Learn more &raquo;</a></p>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,2Template,12,Default.aspxRegion,5,jumboOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,9,jumbotronVisible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0'")]
    [DefaultData(ColumnList = "OrderNo,Name,Template,Region,Details", ValueList = @"2,'StaticHtml','Default.aspx','content','Cinar.CMS.Serialization
InnerHtml,1123,  <div class=""col-lg-4"">
    <h2>Heading</h2>
    <p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>
    <p><a class=""btn btn-default"" href=""#"">View details &raquo;</a></p>
  </div>
  <div class=""col-lg-4"">
    <h2>Heading</h2>
    <p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>
    <p><a class=""btn btn-default"" href=""#"">View details &raquo;</a></p>
 </div>
  <div class=""col-lg-4"">
    <h2>Heading</h2>
    <p>Donec sed odio dui. Cras justo odio, dapibus ac facilisis in, egestas eget quam. Vestibulum id ligula porta felis euismod semper. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus.</p>
    <p><a class=""btn btn-default"" href=""#"">View details &raquo;</a></p>
  </div>
LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,3Template,12,Default.aspxRegion,7,contentOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,3,rowVisible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0'")]
    [DefaultData(ColumnList = "OrderNo,Name,Template,Region,Details", ValueList = @"3,'StaticHtml','Default.aspx','footer','Cinar.CMS.Serialization
InnerHtml,32,<hr/>
<p>&copy; Company 2013</p>LangId1,1,0InnerHtml1,0,LangId2,1,0InnerHtml2,0,LangId3,1,0InnerHtml3,0,Id,1,4Template,12,Default.aspxRegion,6,footerOrderNo,1,0Name,10,StaticHtmlCSS,0,TopHtml,0,BottomHtml,0,ParentModuleId,1,0CSSClass,0,Visible,4,TrueRoleToRead,0,UseCache,7,DefaultCacheLifeTime,1,0'")]
    public class Module : ObjectWithTags, IDatabaseEntity
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
        private string elementId = "";
        private string elementName = "div";
        private bool visible = true;
        private string roleToRead = "";
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

        [EditFormFieldProps(OrderNo=1)]
        public string ElementName
        {
            get { if (string.IsNullOrWhiteSpace(elementName)) elementName = "div"; return elementName; }
            set { elementName = value; }
        }
        [EditFormFieldProps(OrderNo = 2)]
        public string ElementId
        {
            get { return elementId; }
            set { elementId = value; }
        }

        [EditFormFieldProps(OrderNo = 3)]
        public string CSSClass
        {
            get { return this.cssClass; }
            set { this.cssClass = value; }
        }

        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.CSSEdit, OrderNo=4)]
        public string CSS
        {
            get { return css; }
            set { css = value; }
        }

        private string topHtml = "";
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(OrderNo = 5)]
        public string TopHtml
        {
            get { return topHtml; }
            set { topHtml = value; }
        }

        private string bottomHtml = "";
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(OrderNo = 6)]
        public string BottomHtml
        {
            get { return bottomHtml; }
            set { bottomHtml = value; }
        }

        [EditFormFieldProps(OrderNo = 7)]
        /// <summary>
        /// Module always visible in design mode.
        /// Module visible in view mode if only this property is true.
        /// </summary>
        public bool Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
        }

        [EditFormFieldProps(OrderNo = 8)]
        public string RoleToRead
        {
            get { return this.roleToRead; }
            set { this.roleToRead = value; }
        }

        protected string useCache = "Default";
        [ColumnDetail(IsNotNull = true, Length = 30, DefaultValue = "Default"), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:_USECACHE_", OrderNo=9)]
        public string UseCache
        {
            get { return useCache; }
            set { useCache = value; }
        }

        protected int cacheLifeTime = 0;
        [EditFormFieldProps(OrderNo = 10)]
        public int CacheLifeTime
        {
            get { return cacheLifeTime; }
            set { cacheLifeTime = value; }
        }

        [ColumnDetail(IsNotNull = true, Length = 50), EditFormFieldProps(Visible = false)]
        public string Template
        {
            get { return template; }
            set { template = value; }
        }

        [ColumnDetail(IsNotNull = true, Length = 50), EditFormFieldProps(Visible = false)]
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

        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(Visible = false)]
        public string Details
        {
            get { return details; }
            set { details = value; }
        }

        [ColumnDetail(IsNotNull = true, DefaultValue = "0"), EditFormFieldProps(Visible = false)]
        public int ParentModuleId
        {
            get { return parentModuleId; }
            set { parentModuleId = value; }
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

            string resTopHtml = this.topHtml;
            if (resTopHtml.Contains("$"))
            {
                Interpreter engine = Provider.GetInterpreter(resTopHtml, this);
                engine.Parse();
                engine.Execute();
                resTopHtml = engine.Output;
            }

            string resBottomHtml = this.bottomHtml;
            if (resBottomHtml.Contains("$"))
            {
                Interpreter engine = Provider.GetInterpreter(resBottomHtml, this);
                engine.Parse();
                engine.Execute();
                resBottomHtml = engine.Output;
            }

            //if(!String.IsNullOrEmpty(strShow) || Provider.DesignMode)
                return resTopHtml + strShow + resBottomHtml;

            //return String.Empty;
        }

        public string Show()
        {
            if (!Provider.DesignMode && !this.Visible)
                return String.Empty; //***
            if (!Provider.DesignMode && !Provider.User.IsInRole(this.RoleToRead))
                return string.Empty; //**

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

            return String.Format("<{6} id=\"{1}\" class=\"{3}{0}{5}\"{4}>{2}</{6}>\n",
                this.Name,
                string.IsNullOrWhiteSpace(ElementId) ? this.Name + "_" + this.Id : ElementId,
                html,
                this.editable ? "Module " : "",
                Provider.DesignMode ? String.Format(" title=\"Name: {0}, Id: {1}, Region: {2}\" mid=\"{3}\"", Provider.GetResource(this.name), this.id, this.region, this.Name + "_" + this.Id) : string.Format(" mid=\"{0}\"", this.Name + "_" + this.Id), // tooltip
                this.cssClass != "" ? " " + this.cssClass : "",
                this.ElementName);
        }

        internal virtual string show() { return ""; }
        protected internal virtual void beforeShow() { }
        protected internal virtual void afterShow() { }

        protected string getCSSId() 
        {
            return  string.IsNullOrWhiteSpace(ElementId) ? String.Format("{0}_{1}", this.Name, this.Id) : ElementId;
        }

        public virtual string GetDefaultCSS()
        {
            return String.Format("#{0} {{}}\n", getCSSId());
        }

        public string GetPropertyEditorJSON()
        {
            return Provider.GetPropertyEditorJSON(this, false);
        }

        public void Save()
        {
            bool isUpdate = Id > 0;
            BeforeSave();
            if (isUpdate)
            {
                Provider.Database.ExecuteNonQuery(
                    "update Module set Name={1}, Template={2}, Region={3}, OrderNo={4}, CSS={5}, Details={6}, ParentModuleId={7}, UseCache={8}, ElementId={9}, ElementName={10} where Id={0}", //  and Name={1}
                    Id, 
                    Name, 
                    Template, 
                    Region, 
                    OrderNo, 
                    CSS, 
                    this.Serialize(),
                    ParentModuleId,
                    UseCache,
                    ElementId,
                    ElementName);

                this.deleteCache();
                Provider.Database.ClearEntityWebCache(typeof(Module), this.Id);
            }
            else
            {
                this.OrderNo = Convert.ToInt32(Provider.Database.GetValue("select max(OrderNo)+1 from Module where Template={0} and Region={1}", this.Template, this.Region));
                Provider.Database.ExecuteNonQuery(
                    "insert into Module (Name, Template, Region, OrderNo, CSS, ParentModuleId, UseCache, ElementId, ElementName) values ({0},{1},{2},{3},{4},{5},{6},{7},{8})", 
                    Name, 
                    Template, 
                    Region, 
                    OrderNo, 
                    CSS, 
                    ParentModuleId,
                    UseCache,
                    ElementId,
                    ElementName);
                this.Id = Convert.ToInt32(Provider.Database.GetValue("select max(Id) from Module"));
                Provider.Database.ExecuteNonQuery(
                    "update Module set Details = {0} where Id={1}", 
                    this.Serialize(), 
                    this.Id);
            }
            AfterSave(isUpdate);
        }
        public virtual void BeforeSave() { }
        public virtual void AfterSave(bool isUpdate) { }

        public string Serialize()
        {
            return CinarSerialization.Serialize(this);
        }
        public static Module Deserialize(string moduleName, string moduleData)
        {
            //moduleData = moduleData;//.Trim();
            Module module = null;
            try
            {
                module = Provider.CreateModule(moduleName);
                try
                {
                    CinarSerialization.Deserialize(module, moduleData);
                }
                catch
                {
                    module.TopHtml = Provider.GetResource("Error while deserializing the module. This may be because module changed or database charset problem");
                }
            }
            catch
            {
                module = new StaticHtml();
                try
                {
                    CinarSerialization.Deserialize(module, moduleData);
                }
                catch
                {
                }

                ((StaticHtml)module).InnerHtml = "<font color=red>Hata</font><br/><br/>" + moduleName + " isimli bu modül bulunamadı, bu modül türü silinmiş olabilir." + ((StaticHtml)module).InnerHtml;
                if (Provider.DevelopmentMode)
                {
                    module.BottomHtml += "<br/><br/><b>Developer'a not:</b> Serialization geçersiz:";
                    module.BottomHtml += "<br/><br/>" + WebUtility.HtmlEncode(moduleData);
                }
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
            module.Id = Convert.ToInt32(drModule["Id"]);
            module.Name = drModule["Name"].ToString();
            module.Template = drModule["Template"].ToString();
            module.Region = drModule["Region"].ToString();
            module.OrderNo = Convert.ToInt32(drModule["OrderNo"]);
            module.CSS = drModule["CSS"].ToString();
            module.ParentModuleId = Convert.ToInt32(drModule["ParentModuleId"]);
            module.UseCache = drModule.Table.Columns.Contains("UseCache") ? drModule["UseCache"].ToString() : "Default";
            return module;
        }

        public static Module Read(int id)
        {
            Module m = Provider.Database.Read<Module>(id);
            if (m == null)
                return null;

            Module module = Deserialize(m.Name, m.Details);
            //module.Id = m.Id;
            //module.Name = m.Name;
            //module.Template = m.Template;
            //module.Region = m.Region;
            //module.OrderNo = m.OrderNo;
            //module.CSS = m.CSS;
            //module.ParentModuleId = m.ParentModuleId;
            //module.UseCache = m.UseCache;
            return module;
        }

        #region Cache olayı
        public string CacheHint = "no cache";

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
                    this.CacheHint = "cached before at " + cache.UpdateDate;
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

                if (this is IRegionContainer)
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
            return String.Format("{0}", getCSSId());
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
