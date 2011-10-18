using System;
using Cinar.Database;
using System.Data;
using System.Collections;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Content")]
    public class LastContents : ContentListByFilter
    {
        private string groupBy = "CategoryId";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:_GROUPBY_")]
        public string GroupBy
        {
            get { return groupBy; }
            set { groupBy = value; }
        }

        protected override IDatabaseEntity[] GetContentList()
        {
            Entities.Content content = Provider.Content;
            FilterParser filterForContent = new FilterParser(this.filter, "Content");

            string whereMevcutIcerik = "and Content.Id<>" + (content == null ? 0 : content.Id);

            string ids = "";
            string showPictureOf = this.groupBy == "CategoryId" ? "Content" : ("T" + this.groupBy);

            string where = filterForContent.GetWhere();
            string distSQL = @"
                        select top " + this.howManyItems + @"
                            Content." + groupBy + @" as GroupId,
                            max(Content.Id) as Id
                        from Content
	                        " + (groupBy == "AuthorId" ? "inner" : "left") + @" join Author as TAuthorId ON TAuthorId.Id = Content.AuthorId
	                        " + (groupBy == "SourceId" ? "inner" : "left") + @" join Source as TSourceId ON TSourceId.Id = Content.SourceId
	                        " + (groupBy == "CategoryId" ? "inner" : "left") + @" join Content as TCategoryId ON TCategoryId.Id = Content.CategoryId
                        where 
                             Content.Visible=1 " + (this.showCurrentContent ? "" : whereMevcutIcerik) + (where != "" ? " AND " + where : "") + @"
                        group by Content." + groupBy;
            DataTable dt = Provider.Database.GetDataTable(distSQL, filterForContent.GetParams());

            if (dt == null || dt.Rows.Count == 0)
                throw new Exception(Provider.GetResource("There is no content with this criteria."));

            ArrayList al = new ArrayList();
            foreach (DataRow dr in dt.Rows)
                al.Add(dr["Id"].ToString());
            ids = String.Join(",", (string[])al.ToArray(typeof(String)));

            string sql = @"
                        select 
                            Content.Id, 
                            Content.Title,
                            Content.ClassName,
                            Content.Hierarchy,
                            Content.PublishDate,
                            TAuthorId.Name as AuthorName,
                            TSourceId.Name as SourceName,
                            TCategoryId.Title as CategoryName,
                            Content.ShowInPage,
                            Content.Description,
                            " + (this.ShowMetin ? "Content.Metin," : "") + @"
                            " + showPictureOf + @".Picture
                        from Content
	                        " + (groupBy == "AuthorId" ? "inner" : "left") + @" join Author as TAuthorId ON TAuthorId.Id = Content.AuthorId
	                        " + (groupBy == "SourceId" ? "inner" : "left") + @" join Source as TSourceId ON TSourceId.Id = Content.SourceId
	                        " + (groupBy == "CategoryId" ? "inner" : "left") + @" join Content as TCategoryId ON TCategoryId.Id = Content.CategoryId
                        where 
                            Content.Id in (" + ids + @")
                        order by " + this.OrderBy + " " + (this.Ascending ? "asc" : "desc");
            return Provider.Database.ReadList(typeof(Entities.Content), sql);
        }
    }

}
