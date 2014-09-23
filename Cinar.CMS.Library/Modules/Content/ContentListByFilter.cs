using System;
using System.Collections.Generic;
using System.Linq;
using Cinar.Database;
using System.Collections;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Content")]
    public class ContentListByFilter : ContentList
    {
        protected string filter = "";
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.FilterEdit, Options = "entityName:'Content'")]
        public string Filter
        {
            get { return filter; }
            set { filter = value; }
        }

        protected override IDatabaseEntity[] GetContentList()
        {
            Entities.Content content = Provider.Content;
            FilterParser filterForContent = new FilterParser(this.filter, "Content");
            string whereMevcutIcerik = "and Content.Id<>" + (content == null ? 0 : content.Id);

            string where = filterForContent.GetWhere();
            string sql = String.Format(@"
                select distinct top " + this.HowManyItems + @"
                    Content.Id,
                    Content.CategoryId,
                    Content.ClassName,
                    Content.Hierarchy,
                    Content.Title,
                    Content.SpotTitle,
                    Content.PublishDate,
                    Content.Description,
                    {0}
                    {1}
                    {2}
                    {3}
                    {4}
                    Content.ShowInPage
                from Content
                    {5}
	                {6}
	                {7}
                where 
                    Content.Visible=1 
                    {8} 
                order by {9} {10}",
                           this.ShowPicture ? (this.WhichPicture == "Content.Picture2" ? "Content.Picture2,Content.Picture," : this.WhichPicture + ",") : "",
                           this.ShowMetin ? "Content.Metin," : "",
                           (this.ShowAuthor || this.WhichPicture.Contains("Author")) ? "TAuthorId.Name as AuthorName," : "",
                           (this.ShowSource || this.WhichPicture.Contains("Source")) ? "TSourceId.Name as SourceName," : "",
                           this.ShowCategory ? "TCategoryId.Title as CategoryName," : "",
                           this.ShowCategory ? "inner join Content as TCategoryId ON Content.CategoryId = TCategoryId.Id" : "",
                           (this.ShowAuthor || this.WhichPicture.Contains("Author")) ? "left join Author as TAuthorId ON TAuthorId.Id = Content.AuthorId" : "",
                           (this.ShowSource || this.WhichPicture.Contains("Source")) ? "left join Source as TSourceId ON TSourceId.Id = Content.SourceId" : "",
                           (this.showCurrentContent ? "" : whereMevcutIcerik) + (where != "" ? " AND " + where : ""),
                           this.OrderBy,
                           this.Ascending ? "asc" : "desc"
                           );

            IDatabaseEntity[] contents = Provider.Database.ReadList(typeof(Entities.Content), sql, filterForContent.GetParams()).SafeCastToArray<IDatabaseEntity>();
            return contents;
        }
    }

    public class FieldOpValue
    {
        public string Column;
        public string Op;
        public object Value;

        public FieldOpValue(string field, string op, object value)
        {
            this.Column = field;
            this.Op = op;
            this.Value = value;
        }
    }

    public class FilterParser
    {
        private string filter;
        private string entityName;

        public FilterParser(string filter, string entityName)
        {
            this.filter = filter;
            this.entityName = entityName;
        }

        private Dictionary<string, object> alParams = null;

        public string GetWhere()
        {
            Entities.Content content = Provider.Content;
            if (content == null)
                content = (Entities.Content)Provider.Database.Read(typeof(Entities.Content), 1);
            Entities.Content category = null;
            if (content.ClassName == "Category")
                category = content;
            else
                category = content.Category;

            string where = "";
            Hashtable htOperators = new Hashtable();
            Hashtable htParams = new Hashtable();
            alParams = new Dictionary<string, object>();
            foreach (string criteria in this.filter.Split(new string[] { " AND ", " and ", " And " }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] pair = criteria.Split(new string[] { "like@", "<=@", ">=@", "<>@", "<@", ">@", "=@", "like", "<=", ">=", "<>", "<", ">", "=" }, StringSplitOptions.None);
                htOperators[pair[0]] = criteria.Substring(pair[0].Length, criteria.Length - pair[0].Length - pair[1].Length);
                htParams[pair[0]] = pair[1];
            }

            int fieldNo = 0;
            ArrayList fields = new ArrayList(htParams.Keys);
            foreach (string field in fields)
            {
                FieldOpValue fov = new FieldOpValue(entityName + "." + field, htOperators[field].ToString(), htParams[field].ToString());

                #region özel durumlar
                if (fov.Op.Contains("@"))
                {
                    fov.Op = fov.Op.Substring(0, fov.Op.Length - 1);

                    if (htParams[field].Equals("Hierarchy"))
                    {
                        int parentCategoryId = category == null ? 1 : category.Id;

                        fov.Value = Provider.GetHierarchyLike(parentCategoryId)+"%";
                        fov.Column = entityName + "." + "Hierarchy";
                    }
                    else if (htParams[field].Equals("Category"))
                    {
                        if (category == null) throw new Exception(Provider.GetResource("KategoriTam cannot be used as a parameter because there is no active category"));
                        fov.Value = category.Id;
                        fov.Column = entityName + "." + "CategoryId";
                    }
                    else if (htParams[field].Equals("Author"))
                    {
                        fov.Value = content.AuthorId;
                    }
                    else if (htParams[field].Equals("Source"))
                    {
                        fov.Value = content.SourceId;
                    }
                    else if (htParams[field].Equals("Content"))
                    {
                        fov.Value = content.Id;
                    }
                    else if (htParams[field].Equals("PreviousContent"))
                    {
                        fov.Value = Provider.PreviousContentId; //Provider.Database.GetInt("select Id from Content where Id<{0} AND CategoryId={1} order by Id desc limit 1", content.Id, content.CategoryId);
                    }
                    else if (htParams[field].Equals("NextContent"))
                    {
                        fov.Value = Provider.NextContentId; //Provider.Database.GetInt("select Id from Content where Id>{0} AND CategoryId={1} order by Id limit 1", content.Id, content.CategoryId);
                    }
                    else if (htParams[field].Equals("Yesterday"))
                    {
                        fov.Value = DateTime.Now.Date.AddDays(-1d);
                    }
                    else if (htParams[field].Equals("LastDay"))
                    {
                        fov.Value = DateTime.Now.Date.AddDays(-2d);
                    }
                    else if (htParams[field].Equals("LastWeek"))
                    {
                        fov.Value = DateTime.Now.Date.AddDays(-1 * 7d);
                    }
                    else if (htParams[field].Equals("LastMonth"))
                    {
                        fov.Value = DateTime.Now.Date.AddMonths(-1);
                    }
                    else
                        throw new Exception(Provider.GetResource("Invalid parameter name"));
                }
                #endregion

                alParams.Add(field, fov.Value);

                where += (where == "" ? "" : " AND ") + fov.Column + " " + fov.Op + " {" + fieldNo + "}";
                fieldNo++;
            }

            return where;
        }

        public object[] GetParams()
        {
            if (alParams == null)
                throw new Exception(Provider.GetResource("The method getWhereFromFilter must be called first"));

            return alParams.Values.ToArray();
        }

        public Dictionary<string, object> GetNameValuePairs()
        {
            if (alParams == null)
                throw new Exception(Provider.GetResource("The method getWhereFromFilter must be called first"));

            return alParams;
        }
    }
}
