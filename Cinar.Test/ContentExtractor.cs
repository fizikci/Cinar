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
        }
    }
}
