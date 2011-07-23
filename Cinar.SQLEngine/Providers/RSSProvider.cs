using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Collections;
using Cinar.SQLParser;
using System.IO;
using System.Reflection;
using System.Xml;

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

            SyndicationFeed s = new SyndicationFeed();
            SyndicationFeed client = SyndicationFeed.Load(new XmlTextReader(url));

            foreach (SyndicationItem rItem in client.Items)
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
        public string Id { get; set; }
        public string Title { get; set; }
        public string Links { get; set; }
        public string Summary { get; set; }
        public string ContentType { get; set; }
        public string Categories { get; set; }
        public string Authors { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime LastUpdatedTime { get; set; }

        public RSSItem(SyndicationItem item)
        {
            this.Id = item.Id;
            this.Title = item.Title!=null ? item.Title.Text : null;
            this.Links = item.Links == null || item.Links.Count == 0 ? null : item.Links[0].Uri.ToString();//string.Join(",", item.Links.Select(c => c.Uri.ToString()).ToArray());
            this.Summary = item.Summary != null ? item.Summary.Text : null;
            this.ContentType = item.Content != null ? item.Content.Type : null;
            this.Categories = item.Categories == null || item.Categories.Count == 0 ? null : string.Join(",", item.Categories.Skip(1).Select(c => c.Name).ToArray());
            this.PublishDate = item.PublishDate.DateTime;
            this.Authors = item.Authors == null || item.Authors.Count == 0 ? null : string.Join(",", item.Authors.Select(c => c.Name).ToArray());
            this.LastUpdatedTime = item.LastUpdatedTime.DateTime;
        }
    }

}
