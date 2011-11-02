using System;
using System.Text;
using System.Data;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System.Xml.Serialization;
using Cinar.Scripting;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Development")]
    public class DataList : TableView
    {
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.entityTypes")]
        public string EntityName { get; set; }

        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.FilterEdit, Options = "entityName:'use#EntityName'")]
        public string Filter { get; set; }

        public int HowManyItems { get; set; }

        protected string orderBy = "Id";
        [ColumnDetail(Length = 20), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "entityName:'use#EntityName'")]
        public string OrderBy
        {
            get {
                if (!String.IsNullOrEmpty(Provider.Request["orderBy"]))
                    return Provider.Request["orderBy"];
                return orderBy; 
            }
            set { orderBy = value; }
        }

        protected bool ascending = false;
        [EditFormFieldProps(Options = "items:_ASCENDING_")]
        public bool Ascending
        {
            get {
                if (!String.IsNullOrEmpty(Provider.Request["ascending"]))
                    Boolean.TryParse(Provider.Request["ascending"], out ascending);
                return ascending; 
            }
            set { ascending = value; }
        }

        [ColumnDetail(IsNotNull = true, ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string DataTemplate { get; set; }

        public int PictureWidth { get; set; }

        public int PictureHeight { get; set; }

        protected internal string defaultWhere = "1=1";
        protected internal bool sayNoRecord = true;

        DataTable data = null;

        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            if (String.IsNullOrEmpty(EntityName))
                return Provider.GetResource("Select entity");

            Cinar.Database.Table tbl = Provider.Database.Tables[EntityName];
            if(tbl==null)
                return Provider.GetResource("The table [entityName] coulnd't be found").Replace("[entityName]",EntityName);

            string pageUrl = Provider.Request.Url.ToString();
            UriParser uriParser = new UriParser(pageUrl);

            if (!String.IsNullOrEmpty(Provider.Request["delete"]))
            {
                deleteEntity();
                Provider.Response.Redirect(Provider.Request["returnUrl"], true);
                return String.Empty; //***
            }

            FilterParser filterParser = new FilterParser(this.Filter, EntityName);
            string where = filterParser.GetWhere();

            int pageNo = 0;
            Int32.TryParse(Provider.Request["pageNo"], out pageNo);

            string sql = String.Format(@"
                select
                    {0}
                from
                    {1}
                where
                    {7} {2}
                order by
                    {3} {4}
                limit {5} offset {6}",
                                     "*",
                                     this.EntityName,
                                     String.IsNullOrEmpty(where) ? "" : ("and " + where),
                                     this.OrderBy,
                                     this.Ascending ? "asc" : "desc",
                                     HowManyItems,
                                     pageNo * HowManyItems,
                                     defaultWhere);

            data = Provider.Database.GetDataTable(sql, filterParser.GetParams());

            Provider.Translate(EntityName, data);

            if (data.Rows.Count == 0)
                return this.sayNoRecord ? Provider.GetResource("No record") : "";
            else
                sb.Append(base.show());
            
            // paging
            string prevPageLink = "", nextPageLink = "";
            uriParser = new UriParser(pageUrl);
            if (pageNo > 0)
            {
                uriParser.QueryPart["pageNo"] = (pageNo - 1).ToString();
                prevPageLink = String.Format("<a href=\"{0}\" class=\"prev\">{1}</a>", uriParser.Uri, Provider.GetModuleResource("Previous Page"));
            }
            if (data.Rows.Count == HowManyItems)
            {
                uriParser.QueryPart["pageNo"] = (pageNo + 1).ToString();
                nextPageLink = String.Format("<a href=\"{0}\" class=\"next\">{1}</a>", uriParser.Uri, Provider.GetModuleResource("Next Page"));
            }

            sb.AppendFormat("<div class=\"paging\">{0} {1}</div>", prevPageLink, nextPageLink);

            return sb.ToString();
        }

        private void deleteEntity()
        {
            int id = 0;
            Int32.TryParse(Provider.Request["delete"], out id);
            if (id > 0)
            {
                BaseEntity entity = (BaseEntity)Provider.Database.Read(Provider.GetEntityType(EntityName), id);
                if(entity!=null)
                    entity.Delete();
            }
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("#{0}_{1} {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.paging {{background:#0C51B1;font-weight:bold;padding:4px;text-align:center}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.paging a {{color:white}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.paging a.prev {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.paging a.next {{}}\n", this.Name, this.Id);
            return sb.ToString();
        }

        protected override int rowCount
        {
            get { return (int)Math.Ceiling((double)data.Rows.Count / (double)this.cols); }
        }

        DataRow dr;

        public DataList()
        {
            PictureHeight = 0;
            PictureWidth = 0;
            DataTemplate = "$entity.Id$ numaralý kayýt";
            HowManyItems = 30;
            Filter = "";
            EntityName = "";
        }

        protected override string getCellHTML(int row, int col)
        {
            string html = this.DataTemplate;

            int index = row * this.cols + col;

            if (data.Rows.Count <= index)
                return String.Empty;

            dr = data.Rows[index];

            IDatabaseEntity entity = Provider.Database.DataRowToEntity(Provider.GetEntityType(this.EntityName), dr);

            Interpreter engine = Provider.GetInterpreter(html, this);
            engine.SetAttribute("entity", entity);
            engine.SetAttribute("index", index + 1);
            engine.Parse();
            engine.Execute();
            html = engine.Output;

            return html;
        }

        [XmlIgnore]
        public string DeleteLink
        {
            get
            {
                return Utility.GetRequestFileName() + "?delete=" + dr["Id"] + "&returnUrl=" + Provider.Server.UrlEncode(Provider.Request.RawUrl);
            }
        }

        [XmlIgnore]
        public string ThumbPicture
        {
            get
            {
                if (!dr.IsNull("Picture"))
                    return Provider.GetThumbPath(dr["Picture"].ToString(), this.PictureWidth, this.PictureHeight);
                else if (!dr.IsNull("FileName"))
                    return Provider.GetThumbPath(dr["FileName"].ToString(), this.PictureWidth, this.PictureHeight);
                else
                    return "Could not find a picture field - Try something else";
            }
        }
    }
}