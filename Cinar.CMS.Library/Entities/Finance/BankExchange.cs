using System;
using Cinar.Database;
using System.Text.RegularExpressions;
using System.Data;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Id, InsertDate, USD as [ExchangeRate.USD], EUR as [ExchangeRate.EUR] from RealTimeExchange")]
    public class BankExchange : SimpleBaseEntity
    {
        [ColumnDetail(IsNotNull = true, DefaultValue = "0", References = typeof(Content))]
        public int BankId { get; set; }

        public int USDBuying { get; set; }
        public int USDSelling { get; set; }
        public int EURBuying { get; set; }
        public int EURSelling { get; set; }

        public static void FetchRates(int bankId, string bankName)
        {
            var bankExchange = new BankExchange();

            if (bankName == "Bank Asya")
            {
                var ds = new DataSet("fxPrices");
                ds.ReadXml("http://www.bankasya.com.tr/xml/kur_list.xml");

                bankExchange.USDBuying = Int32.Parse(ds.Tables[1].Rows[0]["Kur"].ToString().Replace(".", ""));
                bankExchange.USDSelling = Int32.Parse(ds.Tables[1].Rows[1]["Kur"].ToString().Replace(".", ""));
                bankExchange.EURBuying = Int32.Parse(ds.Tables[1].Rows[2]["Kur"].ToString().Replace(".", ""));
                bankExchange.EURSelling = Int32.Parse(ds.Tables[1].Rows[3]["Kur"].ToString().Replace(".", ""));

                bankExchange.Save();

            }
            else
            {
                throw new Exception("Banka adı bunlardan biri olabilir: Bank Asya");
            }
        }
    }

}
