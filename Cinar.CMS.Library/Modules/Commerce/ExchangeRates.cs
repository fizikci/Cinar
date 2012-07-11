using System;
using System.Text;
using Cinar.CMS.Library.Entities;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Commerce")]
    public class ExchangeRates : StaticHtml
    {

        public ExchangeRates()
        {
            this.InnerHtml = @"
$
var er = Provider.GetExchangeRates();
$
USD: $=er.USD$<br/>
EUR: $=er.EUR$
";
        }
    }
}
