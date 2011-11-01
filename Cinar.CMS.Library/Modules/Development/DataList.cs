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

        private string entityName = "";
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.entityTypes")]
        public string EntityName
        {
            get { return entityName; }
            set { entityName = value; }
        }
/*
        private string showFields = "";
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "entityName:'use#EntityName',multiSelect:true")]
        public string ShowFields
        {
            get { return showFields; }
            set { showFields = value; }
        }
*/
        protected string filter = "";
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.FilterEdit, Options = "entityName:'use#EntityName'")]
        public string Filter
        {
            get { return filter; }
            set { filter = value; }
        }

        protected int howManyItems = 30;
        public int HowManyItems
        {
            get { return howManyItems; }
            set { howManyItems = value; }
        }

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

        private string dataTemplate = "$entity.Id$ numaralý kayýt";
        [ColumnDetail(IsNotNull = true, ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string DataTemplate
        {
            get { return dataTemplate; }
            set { dataTemplate = value; }
        }

        protected int pictureWidth = 0;
        public int PictureWidth
        {
            get { return pictureWidth; }
            set { pictureWidth = value; }
        }

        protected int pictureHeight = 0;
        public int PictureHeight
        {
            get { return pictureHeight; }
            set { pictureHeight = value; }
        }

        protected internal string defaultWhere = "1=1";
        protected internal bool sayNoRecord = true;

        DataTable data = null;

        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            if (String.IsNullOrEmpty(entityName))
                return Provider.GetResource("Select entity");

            Cinar.Database.Table tbl = Provider.Database.Tables[entityName];
            if(tbl==null)
                return Provider.GetResource("The table [entityName] coulnd't be found").Replace("[entityName]",entityName);

            string pageUrl = Provider.Request.Url.ToString();
            UriParser uriParser = new UriParser(pageUrl);

            if (!String.IsNullOrEmpty(Provider.Request["delete"]))
            {
                deleteEntity();
                Provider.Response.Redirect(Provider.Request["returnUrl"], true);
                return String.Empty; //***
            }

            /*
            BaseEntity testEntity = Provider.CreateEntity(entityName);

            if (String.IsNullOrEmpty(showFields))
                showFields = String.Format("{0},Visible", testEntity.GetNameColumn());

            StringBuilder sbFrom = new StringBuilder();
            sbFrom.AppendFormat("[{0}]\n", entityName);

            // generate SQL
            string[] showFieldsArr = showFields.Split(',');
            ArrayList showFieldsArrWithAs = new ArrayList();
            for (int i = 0; i < showFieldsArr.Length; i++)
            {
                string field = showFieldsArr[i];
                PropertyInfo pi = testEntity.GetType().GetProperty(field);
                EditFormFieldPropsAttribute attrib = (EditFormFieldPropsAttribute)Utility.GetAttribute(pi, typeof(EditFormFieldPropsAttribute));
                ColumnDetailAttribute fieldProps = (ColumnDetailAttribute)Utility.GetAttribute(pi, typeof(ColumnDetailAttribute));

                string caption = field; //Provider.GetResource(pi.DeclaringType.Name + "." + pi.Name);

                if (fieldProps.References != null)
                {
                    BaseEntity testRefEntity = Provider.CreateEntity(fieldProps.References.Name);
                    showFieldsArrWithAs.Add(field);
                    showFieldsArrWithAs.Add("T"+field + "." + testRefEntity.GetNameColumn() + " as [" + caption + "]");

                    sbFrom.AppendFormat("\tleft join [{0}] as {1} ON {1}.{2} = [{3}].{4}\n", fieldProps.References.Name, "T"+field, "Id", entityName, field);
                }
                else
                    showFieldsArrWithAs.Add(entityName + "." + field + " as [" + caption + "]");
            }
            showFields = String.Join(",", (string[])showFieldsArrWithAs.ToArray(typeof(string)));
            showFields = entityName+".Id," + showFields;
            */

            FilterParser filterParser = new FilterParser(this.filter, entityName);
            string where = filterParser.GetWhere();

            int pageNo = 0;
            Int32.TryParse(Provider.Request["pageNo"], out pageNo);

            /*
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
                                     showFields, 
                                     sbFrom.ToString(), 
                                     String.IsNullOrEmpty(where) ? "" : ("and " + where), 
                                     entityName + "." + this.OrderBy, 
                                     this.Ascending ? "asc" : "desc", 
                                     howManyItems, 
                                     pageNo*howManyItems,
                                     defaultWhere);
            */
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
                                     this.entityName,
                                     String.IsNullOrEmpty(where) ? "" : ("and " + where),
                                     this.OrderBy,
                                     this.Ascending ? "asc" : "desc",
                                     howManyItems,
                                     pageNo * howManyItems,
                                     defaultWhere);

            data = Provider.Database.GetDataTable(sql, filterParser.GetParams());

            Provider.Translate(entityName, data);

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
            if (data.Rows.Count == howManyItems)
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
                BaseEntity entity = (BaseEntity)Provider.Database.Read(Provider.GetEntityType(entityName), id);
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
        protected override string getCellHTML(int row, int col)
        {
            string html = this.dataTemplate;

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
                    return Provider.GetThumbPath(dr["Picture"].ToString(), this.pictureWidth, this.pictureHeight);
                else if (!dr.IsNull("FileName"))
                    return Provider.GetThumbPath(dr["FileName"].ToString(), this.pictureWidth, this.pictureHeight);
                else
                    return "Could not find a picture field - Try something else";
            }
        }
    }
}