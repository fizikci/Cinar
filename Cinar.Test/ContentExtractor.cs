using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cinar.Database;
using System.IO;
using System.Collections;
using System.Net;

namespace Cinar.Test
{
    public class ContentExtractor
    {
        public static void Run()
        {
            #region English Tafsir
            StringBuilder sb = new StringBuilder(1000000);

            string startText = "<font class='TextResultEnglish'><font color='black'>";
            string endText = "</font></font>";
            int i = 0;

            foreach (string key in Kuran.kuran.Keys)
            {
                string sureNo = key.Split('_')[0];
                string ayetNo = key.Split('_')[1];
                string url = "http://www.altafsir.com/Tafasir.asp?tMadhNo=2&tTafsirNo=73&tSoraNo=" + sureNo + "&tAyahNo=" + ayetNo + "&tDisplay=yes&UserProfile=0&LanguageId=2";

                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                string content = wc.DownloadString(url);

                int pos = content.IndexOf(startText);
                if (pos < 0)
                    break;

                content = content.Substring(pos + startText.Length);
                string tefsir = content.Substring(0, content.IndexOf(endText));
                sb.AppendLine("\"" + sureNo + "-" + ayetNo + "\": \"" + WebUtility.HtmlDecode(tefsir).Replace("\"", "\\\"") + "\",");

                Console.WriteLine(++i);
            }

            File.WriteAllText(@"C:\tefsir_en.js", sb.ToString(), Encoding.UTF8);

            #endregion

            #region Türkçe Tefsir
            /*
            StringBuilder sb = new StringBuilder(1000000);
            int lastAyetNo = 0;

            for (int i = 0; i < Kuran.sayfalar.Length; i++)
            {
                string url = "http://kuran.diyanet.gov.tr/KuranHandler.ashx?l=tefsir&lid=1&a="+Kuran.sayfalar[i].Replace("_", ":");

                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                string content = wc.DownloadString(url);

                while (true)
                {
                    int pos = content.IndexOf("<div id=\"tefsir_");
                    if (pos < 0)
                        break;

                    content = content.Substring(pos + 16);
                    string ayet = content.Substring(0, content.IndexOf('"'));
                    
                    int ayetNo = int.Parse(ayet.Split('-')[1]);
                    if (ayetNo - lastAyetNo > 1)
                        for (var k = lastAyetNo + 1; k < ayetNo; k++)
                            sb.AppendLine("\"" + ayet.Split('-')[0]+"-"+k + "\": \"\",");
                    lastAyetNo = ayetNo;

                    pos = content.IndexOf("<h3>Tefsir</h3>") + 15;
                    string tefsir = content.Substring(pos, content.IndexOf("<hr></span>")-pos);
                    sb.AppendLine("\""+ayet+"\": \""+ WebUtility.HtmlDecode(tefsir).Replace("\"","\\\"")+"\",");
                }

                Console.WriteLine(i);
            }

            File.WriteAllText(@"C:\tefsir.js", sb.ToString(), Encoding.UTF8);
            */

            #endregion

            #region Cevşen

            /*
            List<string> list = new List<string>();

            for (int i = 1; i < 101; i++) 
            {
                string url = "http://society.ncsu.edu/islam/jawsan/ce" + i + ".htm";

                string startsWith = "<font size=\"3\" face=\"Times New Roman, Times, serif\">";
                string endsWith = "</font>";

                WebClient wc = new WebClient();
                string content = wc.DownloadString(url).Replace("Jawshan Al-Kabir "+i, "").Replace("<title>"+i+"</title>", "")
                    .Replace("<strong>" + i + "</strong>", "").Replace("ce" + i + ".htm", "")
                    .Replace("ce" + (i-1) + ".htm", "").Replace("ce" + (i+1) + ".htm", "");

                //int start = content.IndexOf(startsWith, StringComparison.InvariantCultureIgnoreCase) + startsWith.Length;
                //int end = content.IndexOf(endsWith, start, StringComparison.InvariantCultureIgnoreCase);

                //content = content.Substring(start, end-start);

                list.Add(WebUtility.HtmlDecode(content));

                Console.WriteLine(i);
            }

            File.WriteAllText(@"C:\Users\android\Desktop\cevsen\en.js", list.ToJSON(), Encoding.UTF8);
             * */
            
            #endregion
        }
    }
}
