using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Cinar.SQLParser;
using System.IO;
using System.Reflection;

namespace Cinar.SQLEngine.Providers
{
    public class RSSProvider
    {
        private string url;

        public RSSProvider(string url)
        {
            this.url = url;
        }

        internal List<Hashtable> GetData(Context context, Expression where, ListSelect fieldNames)
        {
            List<Hashtable> list = new List<Hashtable>();

            var client = new RSSReader.RSS(url);
            foreach (RSSReader.RSSItem rItem in client.Items)
            {
                RSSItem item = new RSSItem(rItem);
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
    public class RSSItem : BaseItem
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime PubDate { get; set; }

        public RSSItem(RSSReader.RSSItem item)
        {
            this.Title = item.Title;
            this.Link = item.Link;
            this.Description = item.Description;
            this.Category = item.Category;
            this.PubDate = item.PubDate;
        }
    }

}
