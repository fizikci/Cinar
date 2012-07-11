using System;
using Cinar.Database;
using System.Text.RegularExpressions;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Id, USD as [ExchangeRate.USD], EUR as [ExchangeRate.EUR] from ExchangeRate")]
    public class ExchangeRate : BaseEntity
    {
        public int USD { get; set; }
        public int AUD { get; set; }
        public int DKK { get; set; }
        public int EUR { get; set; }
        public int GBP { get; set; }
        public int CHF { get; set; }
        public int SEK { get; set; }
        public int CAD { get; set; }
        public int KWD { get; set; }
        public int NOK { get; set; }
        public int SAR { get; set; }
        public int JPY { get; set; }
        public int BGN { get; set; }
        public int RON { get; set; }
        public int RUB { get; set; }
        public int IRR { get; set; }
        public int CNY { get; set; }
        public int PKR { get; set; }
    }

}
