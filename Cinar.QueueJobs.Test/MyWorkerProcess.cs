using Cinar.QueueJobs.Entities;
using Cinar.QueueJobs.UI;
using NReadability;
using System;
using System.Collections.Generic;
using System.IO;
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
            return typeof(Worker);
        }

        public override Type GetQueueType()
        {
            return typeof(Job);
        }

        public override Type GetQueueDataType()
        {
            return typeof(JobData);
        }

        public override bool UseJobData
        {
            get
            {
                return false;
            }
        }

        Database.Database db = null;

        public MyWorkerProcess()
        {
            db = this.GetNewDatabaseInstance();
        }

        public override string ExecuteJob(Job job, JobData jobData)
        {
            var jobDefName = db.GetString("select Name from JobDefinition where Id=" + job.JobDefinitionId);

            switch (job.Command)
            {
                case "FindLinks":
                    {
                        string res = findLinks(job.Name);

                        var d = DateTime.Now;
                        string path = AppDomain.CurrentDomain.BaseDirectory + "\\crawler\\" + d.ToString("yyyyMMdd") + "\\" + jobDefName;
                        Directory.CreateDirectory(path);
                        if(!String.IsNullOrWhiteSpace(res)) 
                            File.WriteAllText(path + "\\found.links", res, Encoding.UTF8);

                        var links = res.SplitWithTrim("\n");
                        var foundLinkCount = links.Length;

                        var filters = FormMain.GetUrlFilters().ContainsKey(job.JobDefinitionId) ? FormMain.GetUrlFilters()[job.JobDefinitionId] : null;
                        if (filters != null)
                            links = links.Where(l => !isTheLinkToBeSkipped(l, filters)).ToArray();

                        string whereIn = "'" + links.Select(l => l.Replace("'", "''")).StringJoin("','") + "'";
                        var linksAlreadySaved = db.GetList<string>("select Name from Job where Name in (" + whereIn + ")");
                        if (linksAlreadySaved != null && linksAlreadySaved.Count > 0)
                            links = links.Except(linksAlreadySaved).ToArray();
                        
                        this.Log(links.Length + " of " + foundLinkCount + " links scheduled to download for " + jobDefName);

                        List<int> workerIds = db.GetList<int>("select Id from Worker where Disabled=0 order by Id");
                        int counter = 0;
                        List<Job> list = new List<Job>();
                        foreach (string url in links)
                            list.Add(this.CreateJob(workerIds[counter++ % workerIds.Count], url, "DownloadContent", url, job.Id, job.JobDefinitionId));
                        AddJobs(db, list);

                        return res;
                    }
                case "DownloadContent":
                    {
                        var content = downloadContent(job.Name);
                        var d = DateTime.Now;
                        string path = AppDomain.CurrentDomain.BaseDirectory + "\\crawler\\" + d.ToString("yyyyMMdd") + "\\" + jobDefName;
                        Directory.CreateDirectory(path);
                        
                        File.WriteAllText(path+"\\"+job.Id+".html", content, Encoding.UTF8);
                        
                        var clean = getCleanText(job.Name, content).ToJSON();

                        File.WriteAllText(path + "\\" + job.Id + ".json", clean, Encoding.UTF8);
                        return clean;
                    }
            }

            return "Command not implemented";
        }

        private bool isTheLinkToBeSkipped(string url, List<SiteUrlFilter> filters)
        {
            var filter = filters.Find(f=>url.StartsWith(f.Url, StringComparison.InvariantCultureIgnoreCase));
            if (filter == null)
                return false;
            return filter.Skip;
        }


        public override Database.Database GetNewDatabaseInstance()
        {
            return new Database.Database("Server=localhost;Database=queue_test;Uid=root;Pwd=bk;old syntax=yes;charset=utf8", Database.DatabaseProvider.MySQL, Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"db.config"));
        }

        private string findLinks(string baseUrl)
        {
            var urls = new HashSet<string>();

            var list = getLinksOf(baseUrl, baseUrl);
            list.ExceptWith(urls);
            urls.UnionWith(list);

            int counter = 0;
            Parallel.ForEach(list, url =>
            {
                var subList = getLinksOf(baseUrl, url);
                urls.UnionWith(subList);
                counter++;

                ReportProgress((100 * counter) / list.Count);
            });

            return urls.OrderBy(o => o).StringJoin(Environment.NewLine);
        }

        private HashSet<string> getLinksOf(string baseUrl, string url)
        {
            try
            {
                var fullUri = getFullUrl(baseUrl, url);

                var text = MyWebClient.DownloadAsString(null, fullUri.ToString());

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(text);

                var hrefs = doc.DocumentNode.SelectNodes("//a[@href]")
                    .Select(l => l.Attributes["href"].Value)
                    .Where(l => !l.StartsWith("#") && !l.StartsWith("https://"))
                    .Select(l => getFullUrl(baseUrl, l).ToString())
                    .Where(l => l.StartsWith(baseUrl))
                    .Distinct().ToList();
                return new HashSet<string>(hrefs.Where(u => u.StartsWith("http")));
            }
            catch
            {
                return new HashSet<string>();
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
            var res = MyWebClient.DownloadAsString(this, url);
            return res;
        }

        private CleanText getCleanText(string url, string content)
        {
            var transcoder = new NReadabilityWebTranscoder(new NReadabilityTranscoder(), new UrlFetcher(content));
            bool success;
            try
            {
                //transcoder.Ti
                string text = transcoder.Transcode(url, out success);

                if (success)
                {
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(text);

                    var title = "";
                    if (doc.DocumentNode.SelectSingleNode("//title") != null)
                        title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
                    var imgUrl = "";
                    var imgNode = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']");
                    if (imgNode != null)
                    {
                        if (imgNode.Attributes["content"] != null)
                            imgUrl = imgNode.Attributes["content"].Value;
                    }
                    var mainText = "";
                    if (doc.DocumentNode.SelectSingleNode("//div[@id='readInner']") != null)
                        mainText = doc.DocumentNode.SelectSingleNode("//div[@id='readInner']").InnerText;

                    return new CleanText { Title = title, Image = imgUrl, Content = mainText, Url = url, FetchDate = DateTime.Now };
                }
                else
                {
                    return new CleanText { Title = "#FAIL#", Image = "", Content = "", Url = url, FetchDate = DateTime.Now };
                }
            }
            catch (Exception ex)
            {
                return new CleanText { Title = "#FAIL#", Image = ex.ToStringBetter(), Content = "", Url = url, FetchDate = DateTime.Now };
            }
        }
    }

    public class MyWebClient : WebClient
    {
        public static string DownloadAsString(WorkerProcess wp, string url) {
            using (MyWebClient wc = new MyWebClient())
            {
                if (wp != null)
                {
                    wc.DownloadProgressChanged += (s, e) =>
                    {
                        wp.ReportProgress((int)((100 * e.BytesReceived) / e.TotalBytesToReceive));
                    };
                }

                wc.Encoding = Encoding.UTF8;

                //wc.Headers["Connection"] = "keep-alive";
                wc.Headers["Cache-Control"] = "max-age=0";
                wc.Headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                wc.Headers["User-Agent"] = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";
                //wc.Headers["Accept-Encoding"] = "gzip,deflate,sdch";
                wc.Headers["Accept-Language"] = "tr,en;q=0.8,en-US;q=0.6";

                return wc.DownloadString(url);
            }
        }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = 10 * 1000;
            return w;
        }

    }

    public class UrlFetcher : IUrlFetcher
    {
        private string content;

        public UrlFetcher(string content)
        {
            // TODO: Complete member initialization
            this.content = content;
        }


        public string Fetch(string url)
        {
            return content;
        }
    }

    public class CleanText
    {
        public string Image { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime FetchDate { get; set; }
    }
}
