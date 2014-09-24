using Cinar.QueueJobs.Entities;
using Cinar.QueueJobs.UI;
using NReadability;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private const string baseDir = @"D:\Work\CRAWLER_DATA";

        public override string ExecuteJob(Job job, JobData jobData)
        {
            var jobDefName = db.GetString("select Name from JobDefinition where Id=" + job.JobDefinitionId);

            switch (job.Command)
            {
                case "FindLinks":
                    {
                        string res = findLinks(job.Name);

                        var d = DateTime.Now;
                        string path = baseDir + "\\crawler\\" + d.ToString("yyyyMMdd") + "\\" + jobDefName;
                        Directory.CreateDirectory(path);
                        if(!String.IsNullOrWhiteSpace(res)) 
                            File.WriteAllText(path + "\\found.links", res, Encoding.UTF8);

                        var links = res.SplitWithTrim("\n");
                        var foundLinkCount = links.Length;

                        var filters = FormMain.GetUrlFilters().ContainsKey(job.JobDefinitionId) ? FormMain.GetUrlFilters()[job.JobDefinitionId] : null;
                        if (filters != null)
                            links = links.Where(l => !isTheLinkToBeSkipped(l, filters)).ToArray();

                        string whereIn = "'" + links.Select(l => l.Replace("'", "''")).StringJoin("','") + "'";
                        var linksAlreadySaved = db.GetList<string>("select Name from Job where Name in (" + whereIn + ") AND Status='Done' AND ResLength>0");
                        if (linksAlreadySaved != null && linksAlreadySaved.Count > 0)
                            links = links.Except(linksAlreadySaved).ToArray();
                        
                        List<int> workerIds = db.GetList<int>("select Id from Worker where Disabled=0 order by Id");
                        int counter = 0;
                        List<Job> list = new List<Job>();
                        int newLinks = 0, tryAgain = 0;
                        foreach (string url in links)
                        {
                            var downloadCount = db.GetInt("select count(*) from Job where Name={0}", url);
                            if (downloadCount > 0 && downloadCount < 5) tryAgain++;
                            if (downloadCount == 0) newLinks++;
                            if (downloadCount < 5)
                                list.Add(this.CreateJob(workerIds[counter++ % workerIds.Count], url, "DownloadContent", url, job.Id, job.JobDefinitionId));
                        }

                        this.Log(string.Format("{0} {1} {2} total links, {3} new, {4} try again", 
                            DateTime.Now.ToString("yyyy.MM.dd HH.mm   "),
                            jobDefName.PadRight(30),
                            foundLinkCount.ToString().PadLeft(5),
                            newLinks.ToString().PadLeft(5),
                            tryAgain.ToString().PadLeft(5)));

                        AddJobs(db, list);

                        return res;
                    }
                case "DownloadContent":
                    {
                        Stopwatch sw = new Stopwatch();
                        var time = new Dictionary<string, long>();

                        var d = DateTime.Now;
                        string path = baseDir + "\\crawler\\" + d.ToString("yyyyMMdd") + "\\" + jobDefName;
                        Directory.CreateDirectory(path);
                        string content = "";

                        try
                        {
                            sw.Start();
                            content = downloadContent(job.Name);
                            File.WriteAllText(path + "\\" + job.Id + ".html", content, Encoding.UTF8);
                            time.Add("Download", sw.ElapsedMilliseconds);
                        }
                        catch (Exception ex)
                        {
                            File.WriteAllText(path + "\\" + job.Id + ".json", new CleanText { Title = "Error occured while downloading the content!", Content = ex.ToStringBetter() }.ToJSON(), Encoding.UTF8);
                            sw.Stop();
                            this.Log(string.Format("{0} {1} {2} (ERROR DOWNLOAD {3})",
                                DateTime.Now.ToString("yyyy.MM.dd HH.mm "),
                                "Worker " + job.WorkerId.ToString().PadLeft(2),
                                job.Name.Replace("http://", "").Replace("www.", "").Replace(":", "").StrCrop(50).PadRight(53),
                                ex.ToStringBetter().Replace(":", "")));
                            this.Log("stats failed");
                            throw ex;
                        }

                        try
                        {
                            var cleanText = getCleanText(job.Name, content);
                            var clean = cleanText.ToJSON();

                            job.ResLength = cleanText.Content.Length;

                            File.WriteAllText(path + "\\" + job.Id + ".json", clean, Encoding.UTF8);
                            time.Add("Clear", sw.ElapsedMilliseconds-time["Download"]);
                            sw.Stop();

                            if(sw.ElapsedMilliseconds>10000)
                                this.Log(string.Format("{0} {1} {2} (Download {3} | Clear {4})",
                                    DateTime.Now.ToString("yyyy.MM.dd HH.mm "),
                                    "Worker " + job.WorkerId.ToString().PadLeft(2),
                                    job.Name.Replace("http://","").Replace("www.","").Replace(":","").StrCrop(50).PadRight(53),
                                    time["Download"].ToString().PadLeft(6),
                                    time["Clear"].ToString().PadLeft(6)));

                            this.Log("stats done");
                            if(job.ResLength>0) this.Log("stats contentFound");                            
                            return clean;
                        }
                        catch (Exception ex)
                        {
                            File.WriteAllText(path + "\\" + job.Id + ".json", new CleanText { Title = "Error occured while cleaning the text!", Content = ex.ToStringBetter() }.ToJSON(), Encoding.UTF8);
                            sw.Stop();
                            this.Log(string.Format("{0} {1} {2} (ERROR CLEAN {3})",
                                DateTime.Now.ToString("yyyy.MM.dd HH.mm "),
                                "Worker " + job.WorkerId.ToString().PadLeft(2),
                                job.Name.Replace("http://", "").Replace("www.", "").Replace(":", "").StrCrop(50).PadRight(53),
                                ex.ToStringBetter().Replace(":", "")));
                            this.Log("stats failed");
                            throw ex;
                        }

                    }
            }

            return "Command not implemented";
        }

        List<string> skippedExtensions = new List<string> { 
            ".jpg",".pdf",".doc",".png",".wmv",".rss",".gif",".jpeg",
            ".xls",".zip",".docx",".rar",".mp4",".ppt",".swf",".mp3",
            ".xlsx",".exe",".pptx",".rtf"};

        private bool isTheLinkToBeSkipped(string url, List<SiteUrlFilter> filters)
        {
            if (!string.IsNullOrWhiteSpace(skippedExtensions.Find(ext => url.EndsWith(ext))))
                return true;

            var filter = filters.Find(f=>url.StartsWith(f.Url, StringComparison.InvariantCultureIgnoreCase));
            if (filter == null)
                return false;
            return filter.Skip;
        }


        public override Database.Database GetNewDatabaseInstance()
        {
            return new Database.Database("Server=localhost;Database=portal;Uid=root;Pwd=bkbk;old syntax=yes;charset=utf8", Database.DatabaseProvider.MySQL, Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"db.config"));
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

                Encoding resolvedEncoding = Encoding.UTF8;
                var text = MyWebClient.DownloadAsString(null, fullUri.ToString(), ref resolvedEncoding);

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(text);

                var hrefs = doc.DocumentNode.SelectNodes("//a[@href]")
                    .Select(l => l.Attributes["href"].Value)
                    .Where(l => !l.StartsWith("#") && !l.StartsWith("https://") && (l.StartsWith(baseUrl) || !l.StartsWith("http://")))
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
            Encoding resolvedEncoding = Encoding.UTF8;
            var res = MyWebClient.DownloadAsString(this, url, ref resolvedEncoding);
            return res;
        }

        private CleanText getCleanText(string url, string content)
        {
            var transcoder = new NReadabilityTranscoder();
            bool success;
            try
            {
                //transcoder.Ti
                TranscodingResult textRes = transcoder.Transcode(new TranscodingInput(content));

                if (textRes.ContentExtracted)
                {
                    var title = "";
                    if (textRes.TitleExtracted)
                        title = textRes.ExtractedTitle;
                    else
                    {
                        var titleNode = transcoder.FoundDocument.GetElementsByTagName("title").First();
                        if (titleNode != null)
                            title = titleNode.Value;
                    }
                    var imgUrl = "";
                    var imgNode = transcoder.FoundDocument.GetElementsByTagName("meta").Where(e => e.GetAttributeValue("property", "") == "og:image").First();//doc.SelectSingleNode("//meta[@property='og:image']");
                    if (imgNode != null)
                        imgUrl = imgNode.GetAttributeValue("content", "");

                    var mainText = "";
                    if (transcoder.FoundContentElement != null)
                    {
                        mainText = transcoder.FoundContentElement.GetInnerHtml();
                    }

                    return new CleanText { Title = title, Image = imgUrl, Content = mainText, Url = url, FetchDate = DateTime.Now };
                }
                else
                {
                    return new CleanText { Title = "Content not found", Image = "", Content = "", Url = url, FetchDate = DateTime.Now };
                }
            }
            catch (Exception ex)
            {
                return new CleanText { Title = "Content not found", Image = ex.Message, Content = "", Url = url, FetchDate = DateTime.Now };
            }
        }

        private CleanText getCleanText_Old(string url, string content)
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
                    return new CleanText { Title = "Content not found", Image = "", Content = "", Url = url, FetchDate = DateTime.Now };
                }
            }
            catch (Exception ex)
            {
                return new CleanText { Title = "Content not found", Image = ex.ToStringBetter(), Content = "", Url = url, FetchDate = DateTime.Now };
            }
        }
    }

    public class MyWebClient : WebClient
    {
        public static string DownloadAsString(WorkerProcess wp, string url, ref Encoding resolvedEncoding)
        {
            using (MyWebClient wc = new MyWebClient())
            {
                if (wp != null)
                {
                    wc.DownloadProgressChanged += (s, e) =>
                    {
                        wp.ReportProgress((int)((100 * e.BytesReceived) / e.TotalBytesToReceive));
                    };
                }

                wc.Encoding = Encoding.ASCII;

                //wc.Headers["Connection"] = "keep-alive";
                wc.Headers["Cache-Control"] = "max-age=0";
                wc.Headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                wc.Headers["User-Agent"] = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";
                //wc.Headers["Accept-Encoding"] = "gzip,deflate,sdch";
                wc.Headers["Accept-Language"] = "tr,en;q=0.8,en-US;q=0.6";

                byte[] bytes = wc.DownloadData(url);

                string pageEncoding = "";
                resolvedEncoding = Encoding.UTF8;
                if (!string.IsNullOrWhiteSpace(wc.ResponseHeaders[HttpResponseHeader.ContentEncoding]))
                    pageEncoding = wc.ResponseHeaders[HttpResponseHeader.ContentEncoding];
                else if (!string.IsNullOrWhiteSpace(wc.ResponseHeaders[HttpResponseHeader.ContentType]))
                    pageEncoding = getCharacterSet(wc.ResponseHeaders[HttpResponseHeader.ContentType]);

                if (pageEncoding == "")
                    pageEncoding = getCharacterSet(bytes);

                if (pageEncoding != "")
                {
                    try
                    {
                        resolvedEncoding = Encoding.GetEncoding(pageEncoding);
                    }
                    catch
                    {
                        //throw new Exception("Invalid encoding: " + pageEncoding);
                    }
                }

                return resolvedEncoding.GetString(bytes);

                //return Encoding.UTF8.GetString(Encoding.Convert(wc.Encoding, Encoding.UTF8, bytes));
            }
        }

        private static string getCharacterSet(string s)
        {
            s = s.ToUpperInvariant();
            int start = s.LastIndexOf("CHARSET");
            if (start == -1)
            {
                start = s.LastIndexOf("ENCODING");
                if (start == -1)
                    return "";
            }

            start = s.IndexOf("=", start);
            if (start == -1)
                return "";

            start++;
            s = s.Substring(start).Trim().Trim('"');
            int end = s.Length;

            int i = s.IndexOf(";");
            if (i != -1)
                end = i;
            i = s.IndexOf("\"");
            if (i != -1 && i < end)
                end = i;
            i = s.IndexOf("'");
            if (i != -1 && i < end)
                end = i;
            i = s.IndexOf("/");
            if (i != -1 && i < end)
                end = i;

            return s.Substring(0, end).Trim();
        }
        private static string getCharacterSet(byte[] data)
        {
            string s = Encoding.Default.GetString(data);
            return getCharacterSet(s);
        }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = 2 * 1000;
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
