﻿using System;
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
    public class TwitterProvider
    {
        private string query;
        private string lang;
        private int page;

        public TwitterProvider(string query, string lang, int page)
        {
            this.query = query;
            this.lang = lang;
            this.page = page;
        }

        internal List<Tweet> GetData()
        {
            Uri serviceUri = new Uri("http://search.twitter.com/search.json?q=\"" + query + "\"&lang=" + lang + "&page=" + page);
            WebClient downloader = new WebClient();
            downloader.Encoding = Encoding.UTF8;
            string json = downloader.DownloadString(serviceUri);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            TwitterSearch foo = ser.Deserialize<TwitterSearch>(json);

            return foo.results;
        }

        internal List<Hashtable> GetData(Context context, Expression where, ListSelectPart fieldNames)
        {
            List<Hashtable> list = new List<Hashtable>();

            foreach (Tweet item in GetData())
            {
                if (item.Filter(context, where))
                {
                    Hashtable ht = new Hashtable();
                    foreach (SelectPart field in fieldNames)
                        ht[field.Alias] = field.Field.Calculate(context);
                    list.Add(ht);
                }
            }

            return list;

        }
    }
    public class TwitterSearch
    {
        public List<Tweet> results { get; set; }
        public TwitterSearch()
        {
            results = new List<Tweet>();
        }

        public long max_id { get; set; }
        public long since_id { get; set; }
        public string refresh_url { get; set; }
        public string next_page { get; set; }
        public int results_per_page { get; set; }
        public int page { get; set; }
        public string query { get; set; }
    }
    public class Tweet : BaseItem
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
