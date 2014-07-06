using Cinar.QueueJobs.Entities;
using Cinar.QueueJobs.UI;
using NReadability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cinar.QueueJobs.Test
{
    public class MyWorkerProcess : WorkerProcess
    {
        public override Type GetWorkerType()
        {
            return typeof(BaseWorker);
        }

        public override Type GetQueueType()
        {
            return typeof(BaseJob);
        }

        public override Type GetQueueDataType()
        {
            return typeof(BaseJobData);
        }

        public override string ExecuteJob(BaseJob job, BaseJobData jobData)
        {
            switch (job.Command)
            {
                case "FindLinks":
                    return findLinks(jobData.Request);
                case "DownloadContent":
                    return downloadContent(jobData.Request);
            }

            return "Command not implemented";
        }




        public override Database.Database GetNewDatabaseInstance()
        {
            return new Database.Database("Server=localhost;Database=queue_test;Uid=root;Pwd=bk;old syntax=yes;charset=utf8", Database.DatabaseProvider.MySQL);
        }


        private string findLinks(string baseUrl)
        {
            var urls = new HashSet<string>();

            var list = getLinksOf(baseUrl, baseUrl);
            list.ExceptWith(urls);
            urls.UnionWith(list);

            int counter = 0;
            foreach (var url in list)
            {
                var subList = getLinksOf(baseUrl, url);
                urls.UnionWith(subList);
                counter++;

                ReportProgress((100 * counter) / list.Count);
            }


            return urls.Where(u => u.StartsWith("http")).OrderBy(o => o).StringJoin(Environment.NewLine);
        }

        private HashSet<string> getLinksOf(string baseUrl, string url)
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

                    var hrefs = doc.DocumentNode.SelectNodes("//a[@href]").Select(l => l.Attributes["href"].Value).Where(l => !l.StartsWith("#") && !l.StartsWith("https://") && (l.StartsWith(baseUrl) || !l.StartsWith("http://"))).Select(l => getFullUrl(baseUrl, l).ToString()).Distinct().ToList();
                    return new HashSet<string>(hrefs);
                }
                catch
                {
                    return new HashSet<string>();
                }
            }
        }

        private Uri getFullUrl(string baseUrl, string url)
        {
            var baseUri = new Uri(baseUrl);
            var relativeUri = new Uri(url, UriKind.RelativeOrAbsolute);
            var fullUri = new Uri(baseUri, relativeUri);
            return fullUri;
        }

        private string downloadContent(string url)
        {
            var res = getCleanText(url);
            return res.Item1 + Environment.NewLine + res.Item2 + Environment.NewLine + res.Item3;
        }

        private Tuple<string, string, string> getCleanText(string url)
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
    }


}
