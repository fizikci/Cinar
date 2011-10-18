using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select Product.Id, Product.ListPrice as [Product.ListPrice], Product.DiscountRate as [Product.DiscountRate], Product.VATRate as [Product.VATRate] from Product inner join Content on Content.Id = Product.ContentId")]
    public class Product : BaseEntity
    {
        private int contentId;
        [ColumnDetail(References = typeof(Content), IsNotNull=true, ReferenceType = ReferenceTypes.OneToOne), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int ContentId
        {
            get { return contentId; }
            set { contentId = value; }
        }

        private decimal listPrice;
        [EditFormFieldProps()]
        public decimal ListPrice
        {
            get { return listPrice; }
            set { listPrice = value; }
        }

        private decimal discountRate;
        [EditFormFieldProps()]
        public decimal DiscountRate
        {
            get { return discountRate; }
            set { discountRate = value; }
        }

        private decimal vatRate = 18;
        [EditFormFieldProps()]
        public decimal VATRate
        {
            get { return vatRate; }
            set { vatRate = value; }
        }

        private decimal otherTaxRate;
        [EditFormFieldProps()]
        public decimal OtherTaxRate
        {
            get { return otherTaxRate; }
            set { otherTaxRate = value; }
        }

        private int point;
        [EditFormFieldProps()]
        public int Point
        {
            get { return point; }
            set { point = value; }
        }

        private decimal width;
        [EditFormFieldProps()]
        public decimal Width
        {
            get { return width; }
            set { width = value; }
        }

        private decimal height;
        [EditFormFieldProps()]
        public decimal Height
        {
            get { return height; }
            set { height = value; }
        }

        private decimal depth;
        [EditFormFieldProps()]
        public decimal Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        private decimal weight;
        [EditFormFieldProps()]
        public decimal Weight
        {
            get { return weight; }
            set { weight = value; }
        }

    }

}
