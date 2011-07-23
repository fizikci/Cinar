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
    public class FriendFeedProvider
    {
        private string query;
        private string lang;

        public FriendFeedProvider(string query, string lang)
        {
            this.query = query;
            this.lang = lang;
        }

        internal List<Tweet> GetData()
        {
            Uri serviceUri = new Uri("http://search.twitter.com/search.json?q=\"" + query + "\"&lang=" + lang);
            WebClient downloader = new WebClient();
            string json = downloader.DownloadString(serviceUri);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            TwitterSearch foo = ser.Deserialize<TwitterSearch>(json);

            return foo.results;
        }

        internal List<Hashtable> GetData(Context context, Expression where, ListSelect fieldNames)
        {
            List<Hashtable> list = new List<Hashtable>();

            foreach (Tweet item in GetData())
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
    public class FriendFeedSearch
    {
        public List<FriendFeedItem> entries { get; set; }
        public FriendFeedSearch()
        {
            entries = new List<FriendFeedItem>();
        }
    }
    public class FriendFeedItem : BaseItem
    {
        public string from_user_id_str { get; set; }
        public string profile_image_url { get; set; }
        public DateTime created_at { get; set; }
        public string from_user { get; set; }
        public string id_str { get; set; }
        public string to_user_id { get; set; }
        public string text { get; set; }
        public long id { get; set; }
        public long from_user_id { get; set; }
        public string iso_language_code { get; set; }
        public string to_user_id_str { get; set; }
        public string source { get; set; }

        public Tweet()
        {
        }
    }
}
