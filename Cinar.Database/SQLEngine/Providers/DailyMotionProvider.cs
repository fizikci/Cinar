using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Cinar.SQLParser;
using System.IO;
using System.Reflection;
using System.Net;
using System.Web.Script.Serialization;


namespace Cinar.SQLEngine.Providers
{
    public class DailyMotionProvider
    {
        private string query;
        private string lang;

        public DailyMotionProvider(string query, string lang)
        {
            this.query = query;
            this.lang = lang;
        }

        internal List<DailyMotionItem> GetData()
        {
            Uri serviceUri = new Uri("https://api.dailymotion.com/videos?fields=id,url,title,description,created_time,modified_time,owner_screenname&search=\"" + query + "\"" + (!string.IsNullOrEmpty(lang) ? "&language=" + lang : ""));
            WebClient downloader = new WebClient();
            downloader.Encoding = Encoding.UTF8;
            string json = downloader.DownloadString(serviceUri);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            DailyMotionSearch foo = ser.Deserialize<DailyMotionSearch>(json);

            return foo.list;
        }

        internal List<Hashtable> GetData(Context context, Expression where, ListSelect fieldNames)
        {
            List<Hashtable> list = new List<Hashtable>();

            foreach (DailyMotionItem item in GetData())
            {
                if (item.Filter(context, where))
                {
                    Hashtable ht = new Hashtable();
                    foreach (Select field in fieldNames)
                        ht[field.Alias] = field.Field.Calculate(context);//context.Variables[fieldName];
                    list.Add(ht);
                }
            }

            return list;

        }
    }
    public class DailyMotionSearch
    {
        public int page { get; set; }
        public int limit { get; set; }
        public bool hasmore { get; set; }

        public List<DailyMotionItem> list { get; set; }
        public DailyMotionSearch()
        {
            list = new List<DailyMotionItem>();
        }
    }
    public class DailyMotionItem : BaseItem
    {
        public string id { get; set; }
        public int modified_time { get; set; }
        public DateTime LastUpdate { get { return new DateTime(1970, 1, 1).AddSeconds(modified_time); } }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public int created_time { get; set; }
        public DateTime PublishDate { get { return new DateTime(1970, 1, 1).AddSeconds(created_time); } }
        public string owner_screenname { get; set; }

        public DailyMotionItem()
        {
        }
    }
}
