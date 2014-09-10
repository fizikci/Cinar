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

namespace NReadability.Console
{
  internal class Program
  {
    private static void Main(string[] args)
    {

        string inputFile = @"C:\Users\BulentKeskin\Desktop\2799.html";
        string outputFile = @"C:\Users\BulentKeskin\Desktop\2799.txt";

        var clean = getCleanText("", File.ReadAllText(inputFile));

      //var nReadabilityTranscoder = new NReadabilityTranscoder();
      //bool mainContentExtracted;

      //File.WriteAllText(
      //  outputFile,
      //  nReadabilityTranscoder.Transcode(File.ReadAllText(inputFile), out mainContentExtracted));
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
  }
}
