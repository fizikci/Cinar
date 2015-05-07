using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cinar.Test
{
    public class BankaDovizKurlari
    {
        public static void Run()
        {
            var bankList = new List<string>() { "bankasya", "akbank", "finansbank", "halkbank", "isbankasi", "vakifbank", "yapikredi", "ziraat", "garanti", "sekerbank", "hsbc" };
            for (var i = 0; i < bankList.Count; i++)
                BankExchange.FetchRates(i + 1, bankList[i]);
        }
    }


    public class BankExchange
    {
        public int BankId { get; set; }

        public int USDBuying { get; set; }
        public int USDSelling { get; set; }
        public int EURBuying { get; set; }
        public int EURSelling { get; set; }

        public static void FetchRates(int bankId, string bankName)
        {
            var bankExchange = new BankExchange();

            switch(bankName){
                case "bankasya":
                    {
                        var ds = new DataSet("fxPrices");
                        ds.ReadXml("http://www.bankasya.com.tr/xml/kur_list.xml");

                        bankExchange.USDBuying = Int32.Parse(ds.Tables[1].Rows[0]["Kur"].ToString().Replace(".", ""));
                        bankExchange.USDSelling = Int32.Parse(ds.Tables[1].Rows[1]["Kur"].ToString().Replace(".", ""));
                        bankExchange.EURBuying = Int32.Parse(ds.Tables[1].Rows[2]["Kur"].ToString().Replace(".", ""));
                        bankExchange.EURSelling = Int32.Parse(ds.Tables[1].Rows[3]["Kur"].ToString().Replace(".", ""));

                        Console.WriteLine("BANK ASYA:");
                        Console.WriteLine(bankExchange.ToJSON());
                        Console.WriteLine();
                        break;
                    }
                default:
                    {
                        var doc = new HtmlAgilityPack.HtmlDocument();
                        var res = ("http://kur.doviz.com/" + bankName).DownloadPage();
                        doc.LoadHtml(res);

                        foreach (var li in doc.DocumentNode.SelectNodes("//div[@class='doviz-column btgreen']/ul/li"))
                        {
                            try
                            {
                                var kurClass = li.ChildNodes[1].FirstChild.FirstChild.Attributes["class"];
                                if (!kurClass.Value.StartsWith("flag "))
                                    continue;

                                var kur = kurClass.Value.Split('-')[1];

                                if (kur == "USD")
                                {
                                    bankExchange.USDBuying = (int)(decimal.Parse(li.ChildNodes[5].InnerText) * 100000);
                                    bankExchange.USDSelling = (int)(decimal.Parse(li.ChildNodes[7].InnerText) * 100000);
                                }
                                if (kur == "EUR")
                                {
                                    bankExchange.EURBuying = (int)(decimal.Parse(li.ChildNodes[5].InnerText) * 100000);
                                    bankExchange.EURSelling = (int)(decimal.Parse(li.ChildNodes[7].InnerText) * 100000);
                                }
                            }
                            catch { continue; }
                        }

                        Console.WriteLine(bankName + ":");
                        Console.WriteLine(bankExchange.ToJSON());
                        Console.WriteLine();
                        break;
                    }
            }
        }
    }

}
