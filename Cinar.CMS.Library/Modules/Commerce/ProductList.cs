using System;
using System.Text;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Commerce")]
    public class ProductList : ContentListByFilter
    {
        public ProductList()
        {
            this.showCurrentContent = true;
        }

        protected string basketPage = "";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string BasketPage
        {
            get { return basketPage; }
            set { basketPage = value; }
        }

        private string addToBasketLink = "Add to basket";
        public string AddToBasketLink
        {
            get { return addToBasketLink; }
            set { addToBasketLink = value; }
        }

        protected override IDatabaseEntity[] GetContentList()
        {
            Entities.Content content = Provider.Content;
            FilterParser filterForContent = new FilterParser(this.filter, "Content");
            string whereMevcutIcerik = "and Content.Id<>" + (content == null ? 0 : content.Id);

            string where = filterForContent.GetWhere();
            string sql = @"
                select distinct top " + this.HowManyItems + @"
                    Content.Id,
                    Content.ClassName,
                    Content.Hierarchy,
                    Content.Title,
                    Content.SpotTitle,
                    TCategoryId.Title as CategoryName,
                    Content.PublishDate,
                    TAuthorId.Name as AuthorName,
                    Content.Picture,
                    Content.Description,
                    " + (this.ShowMetin ? "Content.Metin," : "") + @"
                    Content.ShowInPage,
                    Product.ListPrice,
                    Product.DiscountRate
                from Content
                    inner join Content as TCategoryId ON Content.CategoryId = TCategoryId.Id
                    inner join Product ON Product.ContentId = Content.Id
	                left join Author as TAuthorId ON TAuthorId.Id = Content.AuthorId
                where Content.Visible=1 " + (this.showCurrentContent ? "" : whereMevcutIcerik) + (where != "" ? " AND " + where : "") + @" 
                order by " + this.OrderBy + " " + (this.Ascending ? "asc" : "desc");

            IDatabaseEntity[] contents = Provider.Database.ReadList(typeof(Entities.Content), sql, filterForContent.GetParams());
            return contents;
        }


        protected override string getCellHTML(int row, int col)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.getCellHTML(row, col));

            int index = row * this.cols + col;
            if (this.contents.Length <= index)
                return String.Empty;

            Entities.Content content = (Entities.Content)this.contents[index];

            if (content["ListPrice"] == null) content["ListPrice"] = Decimal.Zero;
            if (content["DiscountRate"] == null) content["DiscountRate"] = Decimal.Zero;

            decimal listPrice = (decimal)content["ListPrice"];
            decimal ourPrice = listPrice - listPrice * (decimal)content["DiscountRate"] / 100;

            sb.AppendFormat("<b>{0:0,0.00}</b><br/>", ourPrice);

            sb.AppendFormat("<a href=\"{0}?cmdName=add&itemId={1}\">{2}</a>", basketPage, content.Id, addToBasketLink);

            return sb.ToString();
        }
    }
}
