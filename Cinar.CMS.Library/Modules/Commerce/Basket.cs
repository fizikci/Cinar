using System;
using System.Text;
using System.Web;
using System.Data;
using Cinar.CMS.Library.Entities;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Commerce")]
    public class Basket : LinkList
    {
        internal static Sepet Sepet
        {
            get {
                if (Provider.Items["sepet"] == null)
                    Provider.Items["sepet"] = new Sepet();
                return (Sepet)Provider.Items["sepet"];
            }
        }

        protected internal override void beforeShow()
        {
            if (Provider.Request["cmdName"] == "add")
                Sepet.Add(Int32.Parse(Provider.Request["itemId"]));

            if (Provider.Request["cmdUpdate"] != null)
            {
                if (Provider.Request.Form.GetValues("del") != null)
                    foreach (string itemId in Provider.Request.Form.GetValues("del"))
                        Sepet.Remove(Int32.Parse(itemId));
                foreach (string key in Provider.Request.Form.AllKeys)
                    if (key.StartsWith("up_")) {
                        string itemId = key.Substring(3);
                        Sepet.Update(Int32.Parse(itemId), Int32.Parse(Provider.Request.Form[key]));
                    }

            }
            else if (Provider.Request["cmdEmpty"] != null)
            {
                Sepet.Empty();
            }

            // veri tabanından ürün adı ve fiyat bilgilerini alalım, toplamları hesaplayalım
            Sepet.ReadNameAndPrice();
        }

        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("<form name='basket{0}' action='{1}' method='post'>", this.Id, Provider.DesignMode ? "#" : Provider.Request.Url.ToString());

            // sepeti göstürtelim
            sb.Append("<table cellpadding='0' cellspacing='0' border='0'>");
            sb.AppendFormat(@"
                <tr class='header'>
                    <td class='productName'>{0}</td>
                    <td class='listPrice'>{1}</td>
                    <td class='ourPrice'>{2}</td>
                    <td class='amount'>{3}</td>
                    <td class='delete'>{4}</td>
                </tr>", 
                      Provider.GetModuleResource("Product"), 
                      Provider.GetModuleResource("List Price"), 
                      Provider.GetModuleResource("Our Price"), 
                      Provider.GetModuleResource("Amount"), 
                      Provider.GetModuleResource("Remove"));

            for (int i = 0; i < Sepet.Lines.Length; i++)
            {
                SepetItem si = Sepet.Lines[i];
                sb.AppendFormat(@"
                    <tr class='{5}'>
                        <td class='productName'>{1}</td>
                        <td class='listPrice'>{2:0,0.00}</td>
                        <td class='ourPrice'>{3:0,0.00}</td>
                        <td class='amount'><input type='text' name='up_{0}' value='{4}' size='3' maxlength='3'/></td>
                        <td class='delete'><input type='checkbox' name='del' value='{0}'/></td>
                    </tr>",
                          si.ItemId, getLink(si.Content), si.ListPrice, si.OurPrice, si.Amount, i % 2 == 0 ? "row" : "row alt");
            }
            sb.Append("</table>");

            sb.Append("<table class='totals'>");
            sb.AppendFormat("<tr><td class='label'>{0}</td><td class='value'>{1:0,0.00}</td></tr>", Provider.GetModuleResource("Subtotal"), Sepet.AraToplam);
            sb.AppendFormat("<tr><td class='label'>{0}</td><td class='value'>{1:0,0.00}</td></tr>", Provider.GetModuleResource("Discount"), Sepet.IndirimTutari);
            sb.AppendFormat("<tr><td class='label'>{0}</td><td class='value'>{1:0,0.00}</td></tr>", Provider.GetModuleResource("VAT"), Sepet.KDVTutari);
            sb.AppendFormat("<tr><td class='label'><b>{0}</b></td><td class='value'><b>{1:0,0.00}</b></td></tr>", Provider.GetModuleResource("Grand Total"), Sepet.ToplamTutar);
            sb.Append("</table>");

            sb.AppendFormat("<input type='submit' name='cmdUpdate' value='{0}'/>", Provider.GetModuleResource("Update Basket"));
            sb.AppendFormat("<input type='submit' name='cmdEmpty' value='{0}'/>", Provider.GetModuleResource("Empty Basket"));
            sb.AppendFormat("<input type='submit' name='cmdCheckOut' value='{0}'/>", Provider.GetModuleResource("Proceed To Checkout"));

            sb.Append("</form>");

            return sb.ToString();
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());

            sb.AppendFormat("#{0}_{1} tr.header {{background:orange; color:white; font-weight:bold}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} tr.header td {{padding: 3px;}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} tr.row {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} tr.alt {{background:#efefef;}}\n", this.Name, this.Id);
            sb.AppendFormat("\n");
            sb.AppendFormat("#{0}_{1} td.listPrice {{text-align:right}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} tr.row td.listPrice {{text-decoration:line-through}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} td.ourPrice {{text-align:right;}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} td.amount {{text-align:right;}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} td.delete {{text-align:center;}}\n", this.Name, this.Id);
            sb.AppendFormat("\n");
            sb.AppendFormat("#{0}_{1} table {{width:100%;}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} td.label {{width:80%; text-align:right}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} td.value {{width:20%; text-align:right}}\n", this.Name, this.Id);

            return sb.ToString();
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }
    }

    internal class Sepet
    {
        private HttpCookie kuki;

        internal Sepet()
        {
            kuki = Provider.Request.Cookies["sepet"];
            if (kuki == null)
            {
                kuki = new HttpCookie("sepet", "");
            }
            kuki.Expires = DateTime.Now + new TimeSpan(365, 0, 0, 0);
            Provider.Response.Cookies.Add(kuki);
        }

        internal void Add(int itemId)
        {
            int amount = GetAmount(itemId);
            if (amount == 0)
                kuki.Value += (kuki.Value == "" ? "" : ",") + "#" + itemId + ":" + 1 + "#";
            else
                kuki.Value = kuki.Value.Replace("#" + itemId + ":" + amount + "#", "#" + itemId + ":" + (amount + 1) + "#");
            lines = null;
            nameAndPriceAlreadyRead = false;
        }

        internal void Remove(int itemId)
        {
            int amount = GetAmount(itemId);
            if (amount > 0)
            {
                kuki.Value = kuki.Value.Replace("#" + itemId + ":" + amount + "#,", "");
                kuki.Value = kuki.Value.Replace("#" + itemId + ":" + amount + "#", "");
            }   
            lines = null;
        }

        internal void Update(int itemId, int amount)
        {
            int amountCurr = GetAmount(itemId);
            if (amountCurr == amount) return; //***

            if (amount > 0)
                kuki.Value = kuki.Value.Replace("#" + itemId + ":" + amountCurr + "#", "#" + itemId + ":" + amount + "#");
            lines = null;
        }

        internal void Empty()
        {
            kuki.Value = "";
            lines = null;
        }

        internal int GetAmount(int itemId)
        {
            for (int i = 0; i < this.Lines.Length; i++)
                if (this.Lines[i].ItemId == itemId)
                    return this.Lines[i].Amount;
            return 0;
        }

        private SepetItem[] lines;
        internal SepetItem[] Lines
        {
            get {
                if (lines == null)
                {
                    string[] items = kuki.Value.Split(',');
                    if (items[0] == "")
                        return lines = new SepetItem[0]; //***

                    lines = new SepetItem[items.Length];
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] parts = items[i].Trim('#').Split(':');
                        lines[i] = new SepetItem(Int32.Parse(parts[0]), Int32.Parse(parts[1]));
                    }
                }
                return lines; 
            }
        }

        private string getProductIds() {
            string str = "0";
            foreach (SepetItem si in this.Lines)
                str += "," + si.ItemId;
            return str;
        }

        private bool nameAndPriceAlreadyRead = false;
        internal void ReadNameAndPrice()
        {
            if (this.Lines.Length == 0 || nameAndPriceAlreadyRead) return;

            this.IndirimTutari = 0;
            this.KDVTutari = 0;
            this.AraToplam = 0;

            DataTable dt = Provider.Database.ReadTable(typeof(Product), @"
                select
                    c.Id,
                    c.Title,
                    c.ShowInPage,
                    c.Hierarchy,
                    c.ClassName,
                    p.ListPrice, 
                    p.DiscountRate, 
                    p.VATRate 
                from 
                    Content c, 
                    Product p 
                where 
                    p.ContentId = c.Id and 
                    c.Id in (" + getProductIds() + ")");

            dt.PrimaryKey = new DataColumn[] { dt.Columns["Id"] };

            for (int i = 0; i < this.Lines.Length; i++)
            {
                SepetItem si = this.Lines[i];
                DataRow dr = dt.Rows.Find(si.ItemId);
                si.Name = dr["Title"].ToString();
                si.ListPrice = dr.IsNull("ListPrice") ? Decimal.Zero : (decimal)dr["ListPrice"];
                si.DiscountRate = dr.IsNull("DiscountRate") ? Decimal.Zero : (decimal)dr["DiscountRate"];
                si.VATRate = dr.IsNull("VATRate") ? Decimal.Zero : (decimal)dr["VATRate"];
                si.Content = (Entities.Content)Provider.Database.DataRowToEntity(typeof(Entities.Content), dr);

                si.OurPrice = si.ListPrice - si.ListPrice * si.DiscountRate / 100;
                this.IndirimTutari += si.ListPrice * si.Amount * si.DiscountRate / 100;
                this.KDVTutari += si.OurPrice * si.Amount * si.VATRate / 100;
                this.AraToplam += si.OurPrice * si.Amount;
            }

            this.ToplamTutar = this.AraToplam + this.KDVTutari;

            nameAndPriceAlreadyRead = true;
        }

        internal decimal AraToplam;
        internal decimal KDVTutari;
        internal decimal IndirimTutari;
        internal decimal ToplamTutar;
    }
    internal class SepetItem
    {
        internal readonly int ItemId;
        internal readonly int Amount;

        internal string Name;
        internal decimal ListPrice;
        internal decimal DiscountRate;
        internal decimal OurPrice;
        internal decimal VATRate;
        internal Content Content;

        internal SepetItem(int itemId, int amount)
        {
            if (itemId < 1 || amount < 1)
                throw new Exception(Provider.GetResource("A product with id {0} cannot be add to the basket.", itemId));
            if (amount < 1)
                throw new Exception(Provider.GetResource("Invalid amount: {0}.", amount));

            this.ItemId = itemId;
            this.Amount = amount;
            this.Name = "#" + itemId;
        }
    }
}
