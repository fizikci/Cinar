using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;

namespace Cinar.RSSReader
{
    public class RSS
    {
        private XmlDocument doc;

        public RSS(string url)
        {
            WebRequest req = WebRequest.Create(url);
            req.Proxy.Credentials = CredentialCache.DefaultCredentials;

            string xml = this.downloadPage(url);

            this.doc = new XmlDocument();
            doc.Load(new System.IO.StringReader(xml));
        }

        public Encoding SourceEncoding
        {
            get {
                return e;
            }
        }

        #region download page with proper encoding
        Encoding e = null;
        private string downloadPage(string url)
        {
            byte[] buffer = null;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Proxy.Credentials = CredentialCache.DefaultCredentials;

            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {

                using (Stream s = resp.GetResponseStream())
                {
                    buffer = readStream(s);
                }

                string pageEncoding = "";
                e = Encoding.UTF8;
                if (resp.ContentEncoding != "")
                    pageEncoding = resp.ContentEncoding;
                else if (resp.CharacterSet != "")
                    pageEncoding = resp.CharacterSet;
                else if (resp.ContentType != "")
                    pageEncoding = getCharacterSet(resp.ContentType);

                if (pageEncoding == "")
                    pageEncoding = getCharacterSet(buffer);

                if (pageEncoding != "")
                {
                    try
                    {
                        e = Encoding.GetEncoding(pageEncoding);
                    }
                    catch
                    {
                        //throw new Exception("Invalid encoding: " + pageEncoding);
                    }
                }

                return e.GetString(buffer);
            }
        }
        private string getCharacterSet(string s)
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
        private string getCharacterSet(byte[] data)
        {
            string s = Encoding.Default.GetString(data);
            return getCharacterSet(s);
        }
        private byte[] readStream(Stream s)
        {
            long curLength = 0;
            try
            {
                byte[] buffer = new byte[8096];
                using (MemoryStream ms = new MemoryStream())
                {
                    while (true)
                    {
                        int read = s.Read(buffer, 0, buffer.Length);
                        if (read <= 0)
                        {
                            curLength = 0;
                            return ms.ToArray();
                        }
                        ms.Write(buffer, 0, read);
                        curLength = ms.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


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
            get { 
                try 
                {
                    XmlNode node = doc.SelectSingleNode("pubDate");
                    if (node == null)
                        node = doc.SelectSingleNode("date");
                    return DateTime.Parse(node.InnerText); 
                } 
                catch { return DateTime.MinValue; } }
        }

        internal RSSItem(XmlNode doc)
        {
            this.doc = doc;
        }
    }
}
