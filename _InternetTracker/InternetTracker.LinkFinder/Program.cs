using NReadability;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InternetTracker.LinkFinder
{
    class Program
    {
        static HashSet<string> urls = new HashSet<string>();

        static void Main(string[] args)
        {
            List<string> listDomains = URLS.Replace("\r","").Split('\n').ToList();

            foreach (string baseUrl in listDomains)
            {
                Console.WriteLine();
                Console.WriteLine(baseUrl);

                urls = new HashSet<string>();

                var list = getLinksOf(baseUrl, baseUrl);
                list.ExceptWith(urls);
                urls.UnionWith(list);
                
                foreach (var url in list)
                {
                    var subList = getLinksOf(baseUrl, url);
                    urls.UnionWith(subList);
                }

                string baseDir = @"C:\Users\Administrator\Desktop\OUTPUT\FoundLinks\" + DateTime.Now.ToString("yyyyMMdd") + "/";
                Directory.CreateDirectory(baseDir);

                File.WriteAllText(baseDir+baseUrl.Replace("http://","").Trim('/')+".txt", urls.OrderBy(o=>o).StringJoin(Environment.NewLine), Encoding.UTF8);

                foreach (string linkUrl in urls)
                {
                    if (!linkUrl.StartsWith("http://"))
                        continue;

                    Console.WriteLine(linkUrl);
                    //var x = getCleanText(linkUrl);
                    //Console.WriteLine("TITLE: {0}", x.Item1);
                    //Console.WriteLine("IMAGE: {0}", x.Item2);
                    //Console.WriteLine("METIN: {0}", x.Item3);

                    //Console.WriteLine();
                }
            }


            //File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt"), urls.StringJoin(Environment.NewLine));
        }

        private static HashSet<string> getLinksOf(string baseUrl, string url)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;

                //wc.Headers["Connection"] = "keep-alive";
                wc.Headers["Cache-Control"] = "max-age=0";
                wc.Headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                wc.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36";
                //wc.Headers["Accept-Encoding"] = "gzip,deflate,sdch";
                wc.Headers["Accept-Language"] = "tr,en;q=0.8,en-US;q=0.6";
                try
                {
                    var fullUri = getFullUrl(baseUrl, url);

                    Console.Write(".");

                    var text = wc.DownloadString(fullUri);

                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(text);

                    var hrefs = doc.DocumentNode.SelectNodes("//a[@href]").Select(l => l.Attributes["href"].Value).Where(l => !l.StartsWith("#") && !l.StartsWith("https://") && (l.StartsWith(baseUrl) || !l.StartsWith("http://"))).Select(l=>getFullUrl(baseUrl, l).ToString()).Distinct().ToList();
                    return new HashSet<string>(hrefs);
                }
                catch{
                    Console.Write("!");
                    return new HashSet<string>();
                }
            }
        }

        private static Uri getFullUrl(string baseUrl, string url)
        {
            var baseUri = new Uri(baseUrl);
            var relativeUri = new Uri(url, UriKind.RelativeOrAbsolute);
            var fullUri = new Uri(baseUri, relativeUri);
            return fullUri;
        }

        private static Tuple<string, string, string> getCleanText(string url)
        {
            var transcoder = new NReadabilityWebTranscoder();
            bool success;
            try
            {
                string text = transcoder.Transcode(url, out success);

                if (success)
                {
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(text);

                    var title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
                    var imgUrl = "";
                    var imgNode = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']");
                    if (imgNode != null) imgUrl = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']").Attributes["content"].Value;
                    var mainText = doc.DocumentNode.SelectSingleNode("//div[@id='readInner']").InnerText;

                    return new Tuple<string, string, string>(title, imgUrl, mainText);
                }
                else
                {
                    return new Tuple<string, string, string>("#FAIL#", "", "");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<string, string, string>("#FAIL#", ex.ToStringBetter(), "");
            }
        }



        private static string URLS = @"http://www.haber7.com
http://www.ntvmsnbc.com
http://www.haberturk.com
http://www.cnnturk.com.tr
http://www.ahaber.com.tr
http://www.ensonhaber.com
http://www.objektifhaber.com
http://www.haberx.com
http://www.gazeteport.com
http://www.internethaber.com
http://www.t24.com.tr
http://www.aktifhaber.com
http://www.abhaber.com
http://www.ajanshaber.com
http://www.acikgazete.com
http://www.aygazete.com
http://www.bianet.org
http://www.bigpara.com
http://www.gazeteci.tv
http://www.gercekgundem.com
http://www.eurovizyon.co.uk
http://www.f5haber.com
http://seksihaber.com
http://www.haber3.com
http://www.haberler.com
http://www.eurovizyon.co.uk
http://www.f5haber.com
http://www.habervitrini.com
http://www.habervaktim.com
http://www.hurhaber.com
http://www.imedya.tv
http://www.iyibilgi.com
http://www.kanaldhaber.com.tr
http://www.samanyoluhaber.com
http://www.mansethaber.com
http://www.aljazeera.com.tr
http://www.moralhaber.net
http://haber.mynet.com
http://www.memurlar.net
http://www.odatv.com
http://www.netgazete.com
http://www.rotahaber.com
http://www.dipnot.tv
http://www.pressturk.com
http://www.aa.com.tr
http://www.ihlassondakika.com
http://www.dha.com.tr
http://www.cihan.com.tr
http://www.sansursuz.com
http://www.sonsayfa.com
http://www.sondakika.com
http://www.timeturk.com
http://www.tgrthaber.com.tr
http://www.turktime.com
http://www.yazete.com
http://www.wsj.com.tr
http://www.haberedikkat.com
http://www.gazetea24.com
http://www.ulkehaber.com
http://www.yirmidorthaber.com
http://www.agos.com.tr
http://www.aksam.com.tr
http://www.anayurtgazetesi.com
http://www.cumhuriyet.com.tr
http://www.birgun.net
http://www.bugun.com.tr
http://www.dunyagazetesi.com.tr
http://efsanefotospor.com
http://www.evrensel.net
http://www.fanatik.com.tr
http://www.fotomac.com.tr
http://www.gunes.com
http://htgazete.com
http://www.hurriyet.com.tr
http://www.turkishdailynews.com
http://www.milligazete.com.tr
http://www.milliyet.com.tr
http://www.ortadogugazetesi.net
http://www.posta.com.tr
http://www.radikal.com.tr
http://www.sabah.com.tr
http://www.sozcu.com.tr
http://www.stargazete.com
http://www.takvim.com.tr
http://www.turkiyegazetesi.com
http://www.taraf.com.tr
http://www.ticaretsicil.gov.tr
http://www.todayszaman.com
http://www.gazetevatan.com
http://www.yeniakit.com.tr
http://www.yeniasir.com.tr
http://www.yeniasya.com.tr
http://www.yenimesaj.com.tr
http://www.yenicaggazetesi.com.tr
http://www.yenisafak.com.tr
http://www.zaman.com.tr";
        /*
         * */
    }


}
