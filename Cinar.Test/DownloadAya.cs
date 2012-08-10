using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Database;
using System.Data;
using System.Web;

namespace Cinar.Test
{
    public class DownloadAya
    {
        public static void Run()
        {
            string arabicStart = @"<font size=""5"" face=""Arabic Typesetting"">";
            string turkishStart = @"<font face=""Verdana"" size=""2"">";

            Database.Database db = new Database.Database(DatabaseProvider.MySQL, "localhost", "quran", "root", "bk", 30);

            DataTable dt = db.GetDataTable("select sura, aya from quran_text order by 1,2");
            foreach (DataRow dr in dt.Rows) 
            {
                string url = string.Format("http://www.kuranmeali.com/mobil/m-kelime.asp?sure={0}&ayet={1}", dr["sura"], dr["aya"]);
                Encoding enc = Encoding.UTF8;
                string res = url.DownloadPage(ref enc);

                int index = 0, end = -1, orderNo = 0;

                while (true)
                {
                    index = res.IndexOf(arabicStart, index);
                    if (index == -1) break;
                    end = res.IndexOf("</font>", index);

                    var arabic = HttpUtility.HtmlDecode(res.Substring(index + arabicStart.Length, end - index - arabicStart.Length).Replace(" : ",""));

                    index = res.IndexOf(turkishStart, end);
                    end = res.IndexOf("</font>", index);

                    var turkish = HttpUtility.HtmlDecode(res.Substring(index + turkishStart.Length, end - index - turkishStart.Length).Replace("\\","'"));

                    turkish = turkish.Replace("ý", "ı").Replace("ð", "ğ").Replace("þ", "ş").Replace("Ý", "I").Replace("Ð", "Ğ").Replace("Þ", "Ş");

                    db.ExecuteNonQuery("insert into quran_kelime_meal(sura, aya, orderno, arabic, turkish) values({0}, {1}, {2}, {3}, {4})", dr["sura"], dr["aya"], orderNo, arabic, turkish);
                    orderNo++;
                }

                Console.WriteLine("{0}, {1}", dr["sura"], dr["aya"]);
            }

        }
    }
}
