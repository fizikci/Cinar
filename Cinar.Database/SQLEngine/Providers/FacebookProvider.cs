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
    public class FacebookProvider
    {
        private string query;

        public FacebookProvider(string query)
        {
            this.query = query;
        }

        internal List<FBPost> GetData()
        {
            Uri serviceUri = new Uri("https://graph.facebook.com/search?q=\"" + query + "\"&type=post");
            WebClient downloader = new WebClient();
            downloader.Encoding = Encoding.UTF8;
            string json = downloader.DownloadString(serviceUri);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            FBSearch foo = ser.Deserialize<FBSearch>(json);

            return foo.data;
        }

        internal List<Hashtable> GetData(Context context, Expression where, ListSelect fieldNames)
        {
            List<Hashtable> list = new List<Hashtable>();

            foreach (FBPost item in GetData())
            {
                if (item.Filter(context, where))
                {
                    Hashtable ht = new Hashtable();
                    foreach (Select field in fieldNames)
                        ht[field.Alias] = field.Field.Calculate(context);
                    list.Add(ht);
                }
            }

            return list;

        }
    }
    public class FBSearch
    {
        public List<FBPost> data { get; set; }
        public FBSearch()
        {
            data = new List<FBPost>();
        }
    }
    public class FBPost : BaseItem
    {
        public string id { get; set; }
        public FBFrom from { get; set; }
        public string from_id { get { return from.id; } }
        public string from_name { get { return from.name; } }
        public string picture { get; set; }
        public string link { get; set; }
        public string source { get; set; }
        public string name { get; set; }
        public string description { get; set; }
//        public FBProperty properties { get; set; }
        public string icon { get; set; }
        public string type { get; set; }
        public string object_id { get; set; }
//        public FBApplication application { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }

        public FBPost()
        {
        }
    }
    //public class FBProperty
    //{
    //    public string name { get; set; }
    //    public string text { get; set; }
    //    public string href { get; set; }
    //}
    //public class FBApplication
    //{
    //    public string name { get; set; }
    //    public string id { get; set; }
    //}
    public class FBFrom
    {
        public string name { get; set; }
        public string id { get; set; }
    }
}
