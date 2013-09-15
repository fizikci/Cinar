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

        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string SQL { get; set; }

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
            get
            {
                if (!String.IsNullOrEmpty(Provider.Request["ascending"]))
                    Boolean.TryParse(Provider.Request["ascending"], out ascending);
                return ascending;
            }
            set { ascending = value; }
        }

        public bool ShowPaging { get; set; }
        public bool AjaxPaging { get; set; }
        public string LabelPrevPage { get; set; }
        public string LabelNextPage { get; set; }

        [ColumnDetail(IsNotNull = true, ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.StringEdit)]
        public string DataTemplate { get; set; }

        public int PictureWidth { get; set; }
        public int PictureHeight { get; set; }
        public bool CropPicture { get; set; }

        protected internal string defaultWhere = "1=1";

        DataTable data = null;

        private int _pageNo = -1;
        private int pageNo {
            get {

                if (_pageNo == -1)
                    Int32.TryParse(Provider.Request["pageNo" + this.Id], out _pageNo);
                return _pageNo;
            }
        }

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();

            if (String.IsNullOrEmpty(EntityName))
                return Provider.GetResource("Select entity");

            Cinar.Database.Table tbl = Provider.Database.Tables[EntityName];
            if(tbl==null)
                return Provider.GetResource("The table [entityName] coulnd't be found").Replace("[entityName]",EntityName);

            string pageUrl = Provider.Request.Url.Scheme + "://" + Provider.Request.Url.Authority + Provider.Request.RawUrl;
            CinarUriParser uriParser = new CinarUriParser(pageUrl);

            if (!String.IsNullOrEmpty(Provider.Request["delete"]))
            {
                deleteEntity();
                Provider.Response.Redirect(Provider.Request["returnUrl"], true);
                return String.Empty; //***
            }

            FilterParser filterParser = new FilterParser(this.Filter, EntityName);
            string where = filterParser.GetWhere();

            string sql = "";
            if (string.IsNullOrWhiteSpace(this.SQL))
                sql = Provider.Database.AddPagingToSQL(String.Format(@"
                    select
                        {0}
                    from
                        {1}
                    where
                        {2} {3}
                    order by
                        {4} {5}",
                                     "*",
                                     this.EntityName,
                                     defaultWhere,
                                     String.IsNullOrEmpty(where) ? "" : ("and " + where),
                                     this.OrderBy,
                                     this.Ascending ? "asc" : "desc"),
                                     HowManyItems,
                                     pageNo);
            else
            {
                Interpreter engine = Provider.GetInterpreter(SQL, this);
                engine.Parse();
                engine.Execute();
                SQL = engine.Output;

                if (SQL.ToLowerInvariant().Contains("limit"))
                    return "Do not use limit in SQL. DataList does it.";

                sql = Provider.Database.AddPagingToSQL(String.Format(SQL),
                                    HowManyItems,
                                    pageNo);
            }

            data = Provider.Database.GetDataTable(sql, filterParser.GetParams());

            Provider.Translate(EntityName, data);

            if (data.Rows.Count == 0)
                return "";
            else
                sb.Append(base.show());
            
            // paging
            if (this.ShowPaging)
            {
                string prevPageLink = "", nextPageLink = "";
                uriParser = new CinarUriParser(pageUrl);
                if (AjaxPaging)
                {
                    uriParser.Path = "/GetModuleHtml.ashx";
                    uriParser.QueryPart["name"] = "DataList";
                    uriParser.QueryPart["id"] = this.Id.ToString();
                }
                if (pageNo > 0)
                {
                    uriParser.QueryPart["pageNo" + this.Id] = (pageNo - 1).ToString();
                    prevPageLink = String.Format("<a href=\"{0}\" class=\"prev\"{1}>{2}</a>",
                        AjaxPaging ? "javascript:void()" : uriParser.Uri.ToString(),
                        AjaxPaging ? " onclick=\"showDataListPage('" + uriParser.Uri + "', " + this.Id + ");\"" : "",
                        LabelPrevPage == "Previous Page" ? Provider.GetModuleResource("Previous Page") : LabelPrevPage);
                }
                if (data.Rows.Count == HowManyItems)
                {
                    uriParser.QueryPart["pageNo" + this.Id] = (pageNo + 1).ToString();
                    nextPageLink = String.Format("<a href=\"{0}\" class=\"next\"{1}>{2}</a>",
                        AjaxPaging ? "#" : uriParser.Uri.ToString(),
                        AjaxPaging ? " onclick=\"showDataListPage('" + uriParser.Uri + "', " + this.Id + ");\"" : "",
                        LabelNextPage == "Next Page" ? Provider.GetModuleResource("Next Page") : LabelNextPage);
                }

                if(!string.IsNullOrWhiteSpace(prevPageLink) || !string.IsNullOrWhiteSpace(nextPageLink))
                    sb.AppendFormat("<div class=\"paging\">{0} {1}</div>", prevPageLink, nextPageLink);
            }

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
            DataTemplate = "$=entity.Id$ numaralý kayýt";
            HowManyItems = 30;
            Filter = "";
            EntityName = "";
            ShowPaging = true;
            LabelNextPage = "Next Page";
            LabelPrevPage = "Previous Page";
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
            engine.SetAttribute("index", index + 1 + pageNo * HowManyItems);
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
                return CMSUtility.GetRequestFileName() + "?delete=" + dr["Id"] + "&returnUrl=" + Provider.Server.UrlEncode(Provider.Request.RawUrl);
            }
        }

        [XmlIgnore]
        public string ThumbPicture
        {
            get
            {
                if (dr.Table.Columns.Contains("Picture"))
                    return Provider.GetThumbPath(dr["Picture"].ToString(), this.PictureWidth, this.PictureHeight, this.CropPicture);
                else if (dr.Table.Columns.Contains("FileName"))
                    return Provider.GetThumbPath(dr["FileName"].ToString(), this.PictureWidth, this.PictureHeight, this.CropPicture);
                else if (dr.Table.Columns.Contains("Avatar"))
                    return Provider.GetThumbPath(dr["Avatar"].ToString(), this.PictureWidth, this.PictureHeight, this.CropPicture);
                else
                    return "Could not find a picture field - Try something else";
            }
        }
    }
}