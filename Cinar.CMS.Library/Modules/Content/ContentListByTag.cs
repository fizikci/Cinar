using System;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Content")]
    public class ContentListByTag : ContentListByFilter
    {
        //TODO: Bir SQLBuilder sınıfı ile SQL oluşturulursa bu kod sadece SQL'e tag join'ini ekleyip, base'in GetContentList()'ini çağırabilir. Aksi halde aşağıdaki gibi kod tekrarı oluyor.
        protected override IDatabaseEntity[] GetContentList()
        {
            Entities.Content content = Provider.Content;
            FilterParser filterForContent = new FilterParser(this.filter, "Content");
            string whereMevcutIcerik = "and Content.Id<>" + (content == null ? 0 : content.Id);

            string tagJoin = "inner join ContentTag ON Content.Id = ContentTag.ContentId and ContentTag.TagId=" + (Provider.Tag != null ? Provider.Tag.Id : 0);

            string where = filterForContent.GetWhere();
            string sql = String.Format(@"
                select distinct top " + this.HowManyItems + @"
                    Content.Id,
                    Content.CategoryId,
                    Content.ClassName,
                    Content.Hierarchy,
                    Content.Title,
                    Content.SpotTitle,
                    TCategoryId.Title as CategoryName,
                    Content.PublishDate,
                    TAuthorId.Name as AuthorName,
                    TSourceId.Name as SourceName,
                    {0},
                    Content.Description,
                    {1}
                    Content.ShowInPage
                from Content
                    inner join Content as TCategoryId ON Content.CategoryId = TCategoryId.Id
                    {5}
	                left join Author as TAuthorId ON TAuthorId.Id = Content.AuthorId
	                left join Source as TSourceId ON TSourceId.Id = Content.SourceId
                where 
                    Content.Visible=1 
                    {2} 
                order by {3} {4}",
                           this.WhichPicture,
                           this.ShowMetin ? "Content.Metin," : "",
                           (this.showCurrentContent ? "" : whereMevcutIcerik) + (where != "" ? " AND " + where : ""),
                           this.OrderBy,
                           this.Ascending ? "asc" : "desc",
                           Provider.Tag != null ? tagJoin : "");

            IDatabaseEntity[] contents = Provider.Database.ReadList(typeof(Entities.Content), sql, filterForContent.GetParams()).SafeCastToArray<IDatabaseEntity>();
            return contents;
        }
    }
}
