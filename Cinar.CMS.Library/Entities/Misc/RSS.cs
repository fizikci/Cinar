using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;

//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    public class RSS
    {
        private XmlDocument doc;

        public RSS(string url)
        {
            WebRequest req = WebRequest.Create(url);
            req.Proxy.Credentials = CredentialCache.DefaultCredentials;

            this.doc = new XmlDocument();
            HttpWebResponse webResponse = (HttpWebResponse)req.GetResponse();
            Encoding srcEncoding = Encoding.GetEncoding(webResponse.CharacterSet);
            System.IO.StreamReader sr = new System.IO.StreamReader(webResponse.GetResponseStream(), srcEncoding);
            byte[] dizi = srcEncoding.GetBytes(sr.ReadToEnd().ToCharArray());
            byte[] dizi2 = Encoding.Convert(srcEncoding, Encoding.UTF8, dizi);
            doc.Load(new System.IO.StringReader(Encoding.UTF8.GetString(dizi2)));
        }

        public string Title
        {
            get { try { return doc.SelectSingleNode("/rss/channel/title").InnerText; } catch { return String.Empty; } }
        }

        public string Description
        {
            get { try { return doc.SelectSingleNode("/rss/channel/description").InnerText; } catch { return String.Empty; } }
        }

        public string Link
        {
            get { try { return doc.SelectSingleNode("/rss/channel/link").InnerText; } catch { return String.Empty; } }
        }

        public DateTime LastBuildDate
        {
            get { try { return DateTime.Parse(doc.SelectSingleNode("/rss/channel/lastbuilddate").InnerText); } catch { return DateTime.MinValue; } }
        }

        public string Copyright
        {
            get { try { return doc.SelectSingleNode("/rss/channel/copyright").InnerText; } catch { return String.Empty; } }
        }

        public string TTL
        {
            get { try { return doc.SelectSingleNode("/rss/channel/ttl").InnerText; } catch { return String.Empty; } }
        }

        private List<RSSItem> items;
        public List<RSSItem> Items
        {
            get {
                if (this.items == null)
                {
                    items = new List<RSSItem>();
                    try
                    {
                        foreach (XmlNode node in doc.SelectNodes("/rss/channel/item"))
                            items.Add(new RSSItem(node));
                    }
                    catch { }
                }
                return items;
            }
        }
    }

    public class RSSItem
    {
        private XmlNode doc;
        public string Title
        {
            get { try { return doc.SelectSingleNode("title").InnerText; } catch { return String.Empty; } }
        }
        public string Link
        {
            get { try { return doc.SelectSingleNode("link").InnerText; } catch { return String.Empty; } }
        }
        public string Description
        {
            get { try { return doc.SelectSingleNode("description").InnerText; } catch { return String.Empty; } }
        }
        public string Category
        {
            get { try { return doc.SelectSingleNode("category").InnerText; } catch { return String.Empty; } }
        }
        public DateTime PubDate
        {
            get { try { return DateTime.Parse(doc.SelectSingleNode("pubDate").InnerText); } catch { return DateTime.MinValue; } }
        }

        internal RSSItem(XmlNode doc)
        {
            this.doc = doc;
        }
    }


}
