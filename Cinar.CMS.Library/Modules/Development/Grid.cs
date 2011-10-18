using System;
using System.Text;
using System.Reflection;
using System.Data;
using Cinar.CMS.Library.Entities;
using Cinar.Database;


namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Development")]
    public class Grid : Module
    {

        private string entityName = "";
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.entityTypes")]
        public string EntityName
        {
            get { return entityName; }
            set { entityName = value; }
        }

        private string showFields = "";
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "entityName:'use#EntityName',multiSelect:true")]
        public string ShowFields
        {
            get { return showFields; }
            set { showFields = value; }
        }

        protected string filter = "";
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.FilterEdit, Options = "entityName:'use#EntityName'")]
        public string Filter
        {
            get { return filter; }
            set { filter = value; }
        }

        protected string editPage = "";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string EditPage
        {
            get { return editPage; }
            set { editPage = value; }
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
                if (!String.IsNullOrEmpty(Provider.Request["orderBy"]) && showFields.Contains(Provider.Request["orderBy"]))
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
                {
                    Boolean.TryParse(Provider.Request["ascending"], out ascending);
                }
                return ascending; 
            }
            set { ascending = value; }
        }

        private string newRecordLink = "<img border=\"0\" src=\"external/icons/yonetim_yeni_kayit.gif\" />";
        [ColumnDetail(Length=200)]
        public string NewRecordLink
        {
            get { return newRecordLink; }
            set { newRecordLink = value; }
        }

        protected bool editable = true;
        public bool Editable
        {
            get { return editable; }
            set { editable = value; }
        }

        protected bool deletable = true;
        public bool Deletable
        {
            get { return deletable; }
            set { deletable = value; }
        }

        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            if (String.IsNullOrEmpty(entityName))
                return Provider.GetResource("Select entity");

            Cinar.Database.Table tbl = Provider.Database.Tables[entityName];
            if(tbl==null)
                return Provider.GetResource("The table [entityName] coulnd't be found").Replace("[entityName]", entityName);

            string pageUrl = Provider.Request.Url.ToString();
            UriParser uriParser = new UriParser(pageUrl);

            if (!String.IsNullOrEmpty(Provider.Request["delete"]))
            {
                deleteEntity();
                uriParser.QueryPart.Remove("delete");
                pageUrl = uriParser.ToString();
            }

            BaseEntity testEntity = Provider.CreateEntity(entityName);

            if (String.IsNullOrEmpty(showFields))
                showFields = String.Format("{0},Visible", testEntity.GetNameColumn());

            StringBuilder sbFrom = new StringBuilder();
            sbFrom.AppendFormat("[{0}]\n", entityName);

            // generate SQL
            string[] showFieldsArr = showFields.Split(',');
            string[] showFieldsArrWithAs = new string[showFieldsArr.Length];
            for (int i = 0; i < showFieldsArr.Length; i++)
            {
                string field = showFieldsArr[i];
                PropertyInfo pi = testEntity.GetType().GetProperty(field);
                EditFormFieldPropsAttribute attrib = (EditFormFieldPropsAttribute)Utility.GetAttribute(pi, typeof(EditFormFieldPropsAttribute));
                ColumnDetailAttribute fieldProps = (ColumnDetailAttribute)Utility.GetAttribute(pi, typeof(ColumnDetailAttribute));

                string caption = Provider.GetResource(pi.DeclaringType.Name + "." + pi.Name);

                if (fieldProps.References != null)
                {
                    BaseEntity testRefEntity = Provider.CreateEntity(fieldProps.References.Name);
                    showFieldsArrWithAs[i] = "T"+field + "." + testRefEntity.GetNameColumn() + " as [" + caption + "]";

                    sbFrom.AppendFormat("\tleft join [{0}] as {1} ON {1}.{2} = [{3}].{4}\n", fieldProps.References.Name, "T"+field, "Id", entityName, field);
                }
                else
                    showFieldsArrWithAs[i] = entityName + "." + field + " as [" + caption + "]";
            }
            showFields = String.Join(",", showFieldsArrWithAs);
            showFields = entityName+".Id," + showFields;

            FilterParser filterParser = new FilterParser(this.filter, entityName);
            string where = filterParser.GetWhere();

            int pageNo = 0;
            Int32.TryParse(Provider.Request["pageNo"], out pageNo);

            string sql = String.Format(@"
                select
                    {0}
                from
                    {1}
                where
                    1=1 {2}
                order by
                    {3} {4}
                limit {5} offset {6}
            ", 
                                        showFields, 
                                        sbFrom.ToString(), 
                                        String.IsNullOrEmpty(where) ? "" : ("and " + where), 
                                        entityName + "." + this.OrderBy, 
                                        this.Ascending ? "asc" : "desc", 
                                        howManyItems, 
                                        pageNo*howManyItems);

            DataTable dt = Provider.Database.GetDataTable(sql, filterParser.GetParams());

            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\n");
            
            // header
            sb.Append("<tr class=\"header\">\n");
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (i == 0)
                    continue;
                string colName = dt.Columns[i].ColumnName;
                string img = "";
                uriParser.QueryPart["orderBy"] = showFieldsArr[i - 1];
                if (this.OrderBy == showFieldsArr[i - 1])
                {
                    uriParser.QueryPart["ascending"] = (!this.Ascending).ToString();
                    img = this.Ascending ? " (asc)" : " (desc)";
                }
                else
                    uriParser.QueryPart["ascending"] = "True";
                sb.AppendFormat("<td><a href=\"{0}\">{1}</a>{2}</td>\n", uriParser.ToString(), colName, img);
            }
            if (this.editable) sb.AppendFormat("<td>{0}</td>\n", "&nbsp;");
            if (this.deletable) sb.AppendFormat("<td>{0}</td>\n", "&nbsp;");
            sb.Append("</tr>\n");

            string nameField = testEntity.GetNameColumn();
            
            // data
            uriParser = new UriParser(pageUrl);
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("<tr class=\"data\">\n");
                string editUrl = this.editPage + "?item=" + dr[0] + "&returnUrl=" + Provider.Server.UrlEncode(Provider.Request.RawUrl);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == 0)
                        continue;
                    object data = (dr.IsNull(i) || dr[i].Equals("")) ? "&nbsp;" : dr[i];
                    if (showFieldsArr[i-1] == nameField && this.editable)
                        data = "<a href=\"" + editUrl + "\">" + data + "</a>";
                    if (dr.Table.Columns[i].DataType == typeof(bool))
                        data = ((bool)data) ? Provider.GetResource("Yes") : Provider.GetResource("No");
                    sb.AppendFormat("<td>{0}</td>\n", data);
                }
                if (this.deletable)
                {
                    uriParser.QueryPart["delete"] = dr[0].ToString();
                    sb.AppendFormat("<td><img src=\"external/icons/delete.png\" onclick=\"if(confirm('Kayýt silinecek!')) location.href='{0}'\"/></td>\n", uriParser.ToString());
                }
                if (this.editable)
                    sb.AppendFormat("<td><img src=\"external/icons/edit.png\" onclick=\"location.href='{0}'\"/></td>\n", editUrl);
                sb.Append("</tr>\n");
            }

            // paging
            string prevPageLink = "", nextPageLink = "";
            uriParser = new UriParser(pageUrl);
            if (pageNo > 0)
            {
                uriParser.QueryPart["pageNo"] = (pageNo - 1).ToString();
                prevPageLink = String.Format("<a href=\"{0}\"><< {1}</a>&nbsp;&nbsp;", uriParser.Uri.ToString(), Provider.GetModuleResource("Previous Page"));
            }
            if (dt.Rows.Count == howManyItems)
            {
                uriParser.QueryPart["pageNo"] = (pageNo + 1).ToString();
                nextPageLink = String.Format("&nbsp;&nbsp;<a href=\"{0}\">{1} >></a>", uriParser.Uri.ToString(), Provider.GetModuleResource("Next Page"));
            }

            sb.AppendFormat("<tr class=\"footer\"><td colspan=\"100\">{0} {1}</td></tr>\n", prevPageLink, nextPageLink);
            sb.Append("</table>\n");

            if (!String.IsNullOrEmpty(newRecordLink)) {
                sb.AppendFormat("<p class=\"newRec\"><a href=\"{0}\">{1}</a></p>\n", this.editPage + "?returnUrl=" + Provider.Server.UrlEncode(Provider.Request.RawUrl), newRecordLink);
            }

            return sb.ToString();
        }

        private void deleteEntity()
        {
            int id = 0;
            Int32.TryParse(Provider.Request["delete"], out id);
            if (id > 0)
            {
                BaseEntity entity = (BaseEntity)Provider.Database.Read(Provider.GetEntityType(entityName), id);
                entity.Delete();
            }
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("#{0}_{1} {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} table {{width:100%}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} table tr.header td {{background:#0C51B1;color:white;font-weight:bold;padding:4px}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} table tr.header td a {{color:white}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} table tr.data td {{padding:2px;border-bottom:1px solid #404040}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} table tr.data td a {{color:black}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} table tr.data td a:hover {{color:#cc0000}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} table tr.footer td {{background:#0C51B1;font-weight:bold;padding:4px;text-align:center}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} table tr.footer td a {{color:white}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} img {{cursor:pointer}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} p.newRec {{text-align:right;margin-top:10px}}\n", this.Name, this.Id);
            return sb.ToString();
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }
    }
}
