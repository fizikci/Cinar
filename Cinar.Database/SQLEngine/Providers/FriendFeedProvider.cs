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

        internal List<FriendFeedItem> GetData()
        {
            Uri serviceUri = new Uri("http://friendfeed.com/api/feed/search?q=\"" + query + "\"&locale=" + lang);
            WebClient downloader = new WebClient();
            downloader.Encoding = Encoding.UTF8;
            string json = downloader.DownloadString(serviceUri);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            FriendFeedSearch foo = ser.Deserialize<FriendFeedSearch>(json);

            return foo.entries;
        }

        internal List<Hashtable> GetData(Context context, Expression where, ListSelectPart fieldNames)
        {
            List<Hashtable> list = new List<Hashtable>();

            foreach (FriendFeedItem item in GetData())
            {
                if (item.Filter(context, where))
                {
                    Hashtable ht = new Hashtable();
                    foreach (SelectPart field in fieldNames)
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
        public string id { get; set; }
        public DateTime updated { get; set; }
        public string title { get; set; }
        public string link { get { return "http://friendfeed.com/" + user.nickname + "/" + id.Split('-')[0]; } }
        public DateTime published { get; set; }
        public FriendFeedUser user { get; set; }
        public string FromUser { get { return user.name; } }

        public FriendFeedItem()
        {
        }
    }
    public class FriendFeedUser
    {
        public string name { get; set; }
        public string nickname { get; set; }

        public FriendFeedUser()
        {
        }
    }
}
