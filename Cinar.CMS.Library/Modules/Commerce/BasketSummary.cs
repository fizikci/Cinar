using System.Text;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Commerce")]
    public class BasketSummary : LinkList
    {
        protected internal override void beforeShow()
        {
            // veri tabanından ürün adı ve fiyat bilgilerini alalım, toplamları hesaplayalım
            Basket.Sepet.ReadNameAndPrice();
        }
        internal override string show()
        {
            StringBuilder sb = new StringBuilder();

            // sepeti göstürtelim
            sb.Append("<table cellpadding='0' cellspacing='0' border='0'>");

            for (int i = 0; i < Basket.Sepet.Lines.Length; i++)
            {
                SepetItem si = Basket.Sepet.Lines[i];
                sb.AppendFormat(@"
                    <tr class='{2}'>
                        <td class='amount'>{0}</td>
                        <td>x</td>
                        <td class='productName'>{1}</td>
                    </tr>",
                          si.Amount, 
                          getLink(si.Content), 
                          i % 2 == 0 ? "row" : "row alt");
            }
            sb.Append("</table>");

            sb.AppendFormat("<div class='tutar'>{0} : {1:0,0.00}</div>", Provider.GetModuleResource("Grand Total"), Basket.Sepet.ToplamTutar);

            return sb.ToString();
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());

            sb.AppendFormat("#{0} table {{width:100%;}}\n", getCSSId());
            sb.AppendFormat("#{0} tr.row {{}}\n", getCSSId());
            sb.AppendFormat("#{0} tr.alt {{background:#efefef;}}\n", getCSSId());
            sb.AppendFormat("#{0} div.tutar {{border-top:1px solid #404040; margin:4px; padding:4px; text-align:right}}\n", getCSSId());

            return sb.ToString();
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }
    }
}
