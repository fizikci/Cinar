using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InternetTracker.Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            testContentExtractor();
        }

        private static void testContentExtractor()
        {
            var text = "";
            using (WebClient wc = new WebClient())
            {
                //wc.Headers["Connection"] = "keep-alive";
                wc.Headers["Cache-Control"] = "max-age=0";
                wc.Headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                wc.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36";
                //wc.Headers["Accept-Encoding"] = "gzip,deflate,sdch";
                wc.Headers["Accept-Language"] = "tr,en;q=0.8,en-US;q=0.6";

                var textOrj = wc.DownloadString("http://www.mynet.com/haber/politika/kilicdarogluna-buyuk-surpriz-1301614-1");//"file://C:/Users/BulentKeskin/Desktop/Kılıçdaroğluna Büyük Sürpriz Haberi ve Son Dakika Haberler Mynet.htm"

                foreach (char c in textOrj)
                {
                    if (c != ' ' && Char.IsWhiteSpace(c))
                        continue;
                    text += c;
                }
                while (text.Contains("  ")) text = text.Replace("  ", " ");
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(text);

                clearNodes(doc, "//head");
                clearNodes(doc, "//script");
                clearNodes(doc, "//style");
                clearNodes(doc, "//a");

                text = doc.DocumentNode.InnerHtml;
            }
            var res = "";

            bool tag = false;

            foreach(char c in text){
                if (c == '<'){
                    tag = true;
                    res += "[ST]\r\n";
                    continue;
                }
                if (c == '>')
                {
                    tag = false;
                    continue;
                }

                if (!tag)
                {
                    if (c != ' ' && Char.IsWhiteSpace(c))
                        continue;
                    res += c;
                }
            }
            while (res.Contains("  ")) res = res.Replace("  ", " ");
            res = res.Replace(" [ST]", "[ST]");
            res = replaceNonUTF8Chars(res);

            Console.WriteLine(res);
        }

        private static void clearNodes(HtmlAgilityPack.HtmlDocument doc, string p)
        {
            var bodyNode = doc.DocumentNode.SelectNodes(p);
            if (bodyNode != null)
                foreach (HtmlNode node in bodyNode)
                {
                    node.ParentNode.RemoveChild(node);
                }
        }

        public static string replaceNonUTF8Chars(string text)
        {
            var dict = new Dictionary<string, string>() { { "¡", "Â¡" }, { "¢", "Â¢" }, { "£", "Â£" }, { "¤", "Â¤" }, { "¥", "Â¥" }, { "¦", "Â¦" }, { "§", "Â§" }, { "¨", "Â¨" }, { "©", "Â©" }, { "ª", "Âª" }, { "«", "Â«" }, { "¬", "Â¬" }, { "®", "Â®" }, { "¯", "Â¯" }, { "°", "Â°" }, { "±", "Â±" }, { "²", "Â²" }, { "³", "Â³" }, { "´", "Â´" }, { "µ", "Âµ" }, { "¶", "Â¶" }, { "·", "Â·" }, { "¸", "Â¸" }, { "¹", "Â¹" }, { "º", "Âº" }, { "»", "Â»" }, { "¼", "Â¼" }, { "½", "Â½" }, { "¾", "Â¾" }, { "¿", "Â¿" }, { "À", "Ã€" }, { "Â", "Ã‚" }, { "Ã", "Ãƒ" }, { "Ä", "Ã„" }, { "Å", "Ã…" }, { "Æ", "Ã†" }, { "Ç", "Ã‡" }, { "È", "Ãˆ" }, { "É", "Ã‰" }, { "Ê", "ÃŠ" }, { "Ë", "Ã‹" }, { "Ì", "ÃŒ" }, { "Î", "ÃŽ" }, { "Ñ", "Ã‘" }, { "Ò", "Ã’" }, { "Ó", "Ã“" }, { "Ô", "Ã”" }, { "Õ", "Ã•" }, { "Ö", "Ã–" }, { "×", "Ã—" }, { "Ø", "Ã˜" }, { "Ù", "Ã™" }, { "Ú", "Ãš" }, { "Û", "Ã›" }, { "Ü", "Ãœ" }, { "Þ", "Ãž" }, { "ß", "ÃŸ" }, { "à", "Ã " }, { "á", "Ã¡" }, { "â", "Ã¢" }, { "ã", "Ã£" }, { "ä", "Ã¤" }, { "å", "Ã¥" }, { "æ", "Ã¦" }, { "ç", "Ã§" }, { "è", "Ã¨" }, { "é", "Ã©" }, { "ê", "Ãª" }, { "ë", "Ã«" }, { "ì", "Ã¬" }, { "í", "Ã­" }, { "î", "Ã®" }, { "ï", "Ã¯" }, { "ð", "Ã°" }, { "ñ", "Ã±" }, { "ò", "Ã²" }, { "ó", "Ã³" }, { "ô", "Ã´" }, { "õ", "Ãµ" }, { "ö", "Ã¶" }, { "÷", "Ã·" }, { "ø", "Ã¸" }, { "ù", "Ã¹" }, { "ú", "Ãº" }, { "û", "Ã»" }, { "ü", "Ã¼" }, { "ý", "Ã½" }, { "þ", "Ã¾" }, { "ÿ", "Ã¿" }, { "†", "â€ " }, { "Š", "Å " } };

            foreach (var item in dict)
                text = text.Replace(item.Value, item.Key);

            return text;
        }
    }
}
