﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Collections;
using System.Xml;
using Cinar.SQLParser;
using System.IO;
using System.Reflection;
using System.Net;
using System.Web.Script.Serialization;


namespace Cinar.SQLEngine.Providers
{
    public class YoutubeProvider
    {
        private string query;

        public YoutubeProvider(string query)
        {
            this.query = query;
        }

        internal List<RSSItem> GetData()
        {
            SyndicationFeed s = new SyndicationFeed();
            SyndicationFeed client = SyndicationFeed.Load(new XmlTextReader("http://gdata.youtube.com/feeds/api/videos?q=\"" + query + "\"&max-results=50&lr=tr"));

            return client.Items.Select(i => new RSSItem(i)).ToList();
        }

        internal List<Hashtable> GetData(Context context, Expression where, ListSelect fieldNames)
        {
            List<Hashtable> list = new List<Hashtable>();

            foreach (RSSItem item in GetData())
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
}
