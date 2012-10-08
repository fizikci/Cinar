using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cinar.Database;
using System.IO;
using System.Collections;
using System.Net;

namespace Cinar.Test
{
    public class Downloader
    {
        public static void Run()
        {
            for (int i = 1; i < 101; i++) 
            {
                string fileName2 = i.ToString().PadLeft(2, '0');
                string fileName3 = i.ToString().PadLeft(3, '0');
                string url = "http://dua-ufku.com/blog/audio/" + fileName2 + ".MP3";
                string url2 = "http://www.dua-ufku.com/cevsen_resim/" + fileName3 + ".jpg";

                WebClient wc = new WebClient();
                wc.DownloadFile(url, @"C:\Users\android\Desktop\dua-ufku.com\" + i + ".mp3");
                wc.DownloadFile(url2, @"C:\Users\android\Desktop\dua-ufku.com\" + i + ".jpg");

                Console.WriteLine(i);
            }
        }
    }
}
