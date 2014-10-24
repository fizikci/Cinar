/*
 * NReadability
 * http://code.google.com/p/nreadability/
 * 
 * Copyright 2010 Marek Stój
 * http://immortal.pl/
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Linq;
using SysConsole = System.Console;
using System.Text;
using System.Net;

namespace NReadability.Console
{
  public class Program
  {
    private static void Main(string[] args)
    {
        while (true)
        {
            SysConsole.Write("URL: ");
            var url = SysConsole.ReadLine();
            if (string.IsNullOrWhiteSpace(url))
                break;

            var content = downloadContent(url);

            var clean = getCleanText("", content);

            SysConsole.WriteLine();
            SysConsole.WriteLine("TITLE: " + clean.Title);
            SysConsole.WriteLine("IMAGE: " + clean.Image);
            SysConsole.WriteLine("CONTENT: " + clean.Content);
            SysConsole.WriteLine();
        }
    }


    private static CleanText getCleanText(string url, string content)
    {
        var transcoder = new NReadabilityTranscoder();
        bool success;
        try
        {
            //transcoder.Ti
            TranscodingResult textRes = transcoder.Transcode(new TranscodingInput(content));

            if (textRes.ContentExtracted)
            {
                var title = "";
                if (textRes.TitleExtracted)
                    title = textRes.ExtractedTitle;
                else
                {
                    var titleNode = transcoder.FoundDocument.GetElementsByTagName("title").First();
                    if (titleNode != null)
                        title = titleNode.Value;
                }
                var imgUrl = "";
                var imgNode = transcoder.FoundDocument.GetElementsByTagName("meta").Where(e => e.GetAttributeValue("property", "") == "og:image").First();//doc.SelectSingleNode("//meta[@property='og:image']");
                if (imgNode != null)
                    imgUrl = imgNode.GetAttributeValue("content","");

                var mainText = "";
                if (transcoder.FoundContentElement != null)
                {
                    mainText = transcoder.FoundContentElement.GetInnerHtml();
                }

                return new CleanText { Title = title, Image = imgUrl, Content = mainText, Url = url, FetchDate = DateTime.Now };
            }
            else
            {
                return new CleanText { Title = "#FAIL#", Image = "", Content = "", Url = url, FetchDate = DateTime.Now };
            }
        }
        catch (Exception ex)
        {
            return new CleanText { Title = "#FAIL#", Image = ex.Message, Content = "", Url = url, FetchDate = DateTime.Now };
        }
    }

    public class CleanText
    {
        public string Image { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime FetchDate { get; set; }
    }



    private static string downloadContent(string url)
    {
        Encoding resolvedEncoding = Encoding.UTF8;
        var res = MyWebClient.DownloadAsString(url, ref resolvedEncoding);
        return res;
    }

    public class MyWebClient : WebClient
    {
        public static string DownloadAsString(string url, ref Encoding resolvedEncoding)
        {
            using (MyWebClient wc = new MyWebClient())
            {
                wc.Encoding = Encoding.ASCII;

                //wc.Headers["Connection"] = "keep-alive";
                wc.Headers["Cache-Control"] = "max-age=0";
                wc.Headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                wc.Headers["User-Agent"] = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";
                //wc.Headers["Accept-Encoding"] = "gzip,deflate,sdch";
                wc.Headers["Accept-Language"] = "tr,en;q=0.8,en-US;q=0.6";

                byte[] bytes = wc.DownloadData(url);

                string pageEncoding = "";
                resolvedEncoding = Encoding.UTF8;
                if (!string.IsNullOrWhiteSpace(wc.ResponseHeaders[HttpResponseHeader.ContentEncoding]))
                    pageEncoding = wc.ResponseHeaders[HttpResponseHeader.ContentEncoding];
                else if (!string.IsNullOrWhiteSpace(wc.ResponseHeaders[HttpResponseHeader.ContentType]))
                    pageEncoding = getCharacterSet(wc.ResponseHeaders[HttpResponseHeader.ContentType]);

                if (pageEncoding == "")
                    pageEncoding = getCharacterSet(bytes);

                if (pageEncoding != "")
                {
                    try
                    {
                        resolvedEncoding = Encoding.GetEncoding(pageEncoding);
                    }
                    catch
                    {
                        //throw new Exception("Invalid encoding: " + pageEncoding);
                    }
                }

                return resolvedEncoding.GetString(bytes);

                //return Encoding.UTF8.GetString(Encoding.Convert(wc.Encoding, Encoding.UTF8, bytes));
            }
        }

        private static string getCharacterSet(string s)
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
        private static string getCharacterSet(byte[] data)
        {
            string s = Encoding.Default.GetString(data);
            return getCharacterSet(s);
        }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = 5 * 1000;
            return w;
        }

    }

  }
}
