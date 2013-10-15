using System;
using System.Text;
using System.Web;
using System.Data;
using Cinar.CMS.Library.Entities;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Commerce")]
    public class Basket : StaticHtml
    {
        public static Sepet Sepet
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

        public Basket() {
            this.InnerHtml = @"<form name='basket$=this.Id$' action='$=Provider.Request.RawUrl$' method='post'>
    <table cellpadding='0' cellspacing='0' border='0'>
    <tr class='header'>
        <td class='productName'>$=Provider.TR('Ürün')$</td>
        <td class='listPrice'>$=Provider.TR('Liste Fiyatı')$</td>
        <td class='ourPrice'>$=Provider.TR('Bizim Fiyatımız')$</td>
        <td class='amount'>$=Provider.TR('Miktar')$</td>
        <td class='delete'>$=Provider.TR('Çıkar')$</td>
    </tr>
$
    for (int i = 0; i < this.Sepet.Lines.Length; i++)
    {
        var si = this.Sepet.Lines[i];
$
        <tr class='$=i % 2 == 0 ? 'line' : 'line alt'$'>
            <td class='productName'><a href=""$=si.Content.GetPageLinkWithTitle('')$"">$=si.Content.Title$</a></td>
            <td class='listPrice'>$=si.ListPrice.ToString('0,0.00')$</td>
            <td class='ourPrice'>$=si.OurPrice.ToString('0,0.00')$</td>
            <td class='amount'><input type='text' name='up_$=si.ItemId$' value='$=si.Amount$' size='3' maxlength='3'/></td>
            <td class='delete'><input type='checkbox' name='del' value='$=si.ItemId$'/></td>
        </tr>
$
}
$
</table>

<table class='totals'>
<tr><td class='label'>$=Provider.TR('Ara Toplam')$</td><td class='value'>$=this.Sepet.AraToplam.ToString('0,0.00')$</td></tr>
<tr><td class='label'>$=Provider.TR('İndirim Tutarı')$</td><td class='value'>$=this.Sepet.IndirimTutari.ToString('0,0.00')$</td></tr>
<tr><td class='label'>$=Provider.TR('KDV Tutarı')$</td><td class='value'>$=this.Sepet.KDVTutari.ToString('0,0.00')$</td></tr>
<tr><td class='label'><b>$=Provider.TR('Toplam Tutar')$</b></td><td class='value'><b>$=this.Sepet.ToplamTutar.ToString('0,0.00')$</b></td></tr>
</table>

<input type='submit' name='cmdUpdate' value='$=Provider.TR('Sepeti Güncelle')$'/>
<input type='submit' name='cmdEmpty' value='$=Provider.TR('Sepeti Boşalt')$'/>
<input type='submit' name='cmdCheckOut' value='$=Provider.TR('Ödemeyi Yap')$'/>

</form>";
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());

            sb.AppendFormat("#{0}_{1} tr.header {{background:orange; color:white; font-weight:bold}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} tr.header td {{padding: 3px;}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} tr.line {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} tr.alt {{background:#efefef;}}\n", this.Name, this.Id);
            sb.AppendFormat("\n");
            sb.AppendFormat("#{0}_{1} td.listPrice {{text-align:right}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} tr.line td.listPrice {{text-decoration:line-through}}\n", this.Name, this.Id);
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

    public class Sepet
    {
        private HttpCookie kuki;

        public Sepet()
        {
            kuki = Provider.Request.Cookies["sepet"];
            if (kuki == null)
            {
                kuki = new HttpCookie("sepet", "");
            }
            kuki.Expires = DateTime.Now + new TimeSpan(365, 0, 0, 0);
            Provider.Response.Cookies.Add(kuki);
        }

        public void Add(int itemId)
        {
            int amount = GetAmount(itemId);
            if (amount == 0)
                kuki.Value += (kuki.Value == "" ? "" : ",") + "#" + itemId + ":" + 1 + "#";
            else
                kuki.Value = kuki.Value.Replace("#" + itemId + ":" + amount + "#", "#" + itemId + ":" + (amount + 1) + "#");
            lines = null;
            nameAndPriceAlreadyRead = false;
        }

        public void Remove(int itemId)
        {
            int amount = GetAmount(itemId);
            if (amount > 0)
            {
                kuki.Value = kuki.Value.Replace("#" + itemId + ":" + amount + "#,", "");
                kuki.Value = kuki.Value.Replace("#" + itemId + ":" + amount + "#", "");
            }   
            lines = null;
        }

        public void Update(int itemId, int amount)
        {
            int amountCurr = GetAmount(itemId);
            if (amountCurr == amount) return; //***

            if (amount > 0)
                kuki.Value = kuki.Value.Replace("#" + itemId + ":" + amountCurr + "#", "#" + itemId + ":" + amount + "#");
            lines = null;
        }

        public void Empty()
        {
            kuki.Value = "";
            lines = null;
        }

        public int GetAmount(int itemId)
        {
            for (int i = 0; i < this.Lines.Length; i++)
                if (this.Lines[i].ItemId == itemId)
                    return this.Lines[i].Amount;
            return 0;
        }

        private SepetItem[] lines;
        public SepetItem[] Lines
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
        public void ReadNameAndPrice()
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

        public decimal AraToplam;
        public decimal KDVTutari;
        public decimal IndirimTutari;
        public decimal ToplamTutar;
    }
    public class SepetItem
    {
        public readonly int ItemId;
        public readonly int Amount;

        public string Name;
        public decimal ListPrice;
        public decimal DiscountRate;
        public decimal OurPrice;
        public decimal VATRate;
        public Content Content;

        public SepetItem(int itemId, int amount)
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
