using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cinar.Database;
using System.IO;
using System.Collections;
using System.Net;
using System.Data;

namespace Cinar.Test
{
    public class DictDownload
    {
        public static void Run()
        {
            Database.Database db = new Database.Database(DatabaseProvider.MySQL, "localhost", "subtitleclick", "root", "bkbk", 100);

            DataTable dt = db.GetDataTable("select distinct word from st_dicttr");
            int i = 0;

            foreach (DataRow dr in dt.Rows) 
            {
                string word = dr["word"].ToString().ToLowerInvariant();
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(wc.DownloadString("https://glosbe.com/gapi/translate?from=en&dest=tr&format=json&phrase="+ word));

                Console.WriteLine(++i + ": " + dr["word"]); // D:\WWW_YEDEK_20161108\subtitleclick.com\subtitleclick_20170114.sql

                foreach(var tuc in obj.tuc)
                    if(tuc.phrase!=null)
                        db.ExecuteNonQuery("insert into st_dicttr2(word,wordtype,definition) values ({0},{1},{2})", word, "", tuc.phrase.text);

                //System.Threading.Thread.Sleep(3000);
            }
        }
    }


    public class Phrase
    {
        public string text { get; set; }
        public string language { get; set; }
    }

    public class Meaning
    {
        public string language { get; set; }
        public string text { get; set; }
    }

    public class Tuc
    {
        public Phrase phrase { get; set; }
        public long? meaningId { get; set; }
        public List<int> authors { get; set; }
        public List<Meaning> meanings { get; set; }
    }

    public class __invalid_type__1
    {
        public string U { get; set; }
        public int id { get; set; }
        public string N { get; set; }
        public string url { get; set; }
    }

    public class __invalid_type__89651
    {
        public string U { get; set; }
        public int id { get; set; }
        public string N { get; set; }
        public string url { get; set; }
    }

    public class __invalid_type__91945
    {
        public string U { get; set; }
        public int id { get; set; }
        public string N { get; set; }
        public string url { get; set; }
    }

    public class __invalid_type__93369
    {
        public string U { get; set; }
        public int id { get; set; }
        public string N { get; set; }
        public string url { get; set; }
    }

    public class __invalid_type__25018
    {
        public string U { get; set; }
        public int id { get; set; }
        public string N { get; set; }
        public string url { get; set; }
    }

    public class __invalid_type__87482
    {
        public string U { get; set; }
        public int id { get; set; }
        public string N { get; set; }
        public string url { get; set; }
    }

    public class __invalid_type__2908
    {
        public string U { get; set; }
        public int id { get; set; }
        public string N { get; set; }
        public string url { get; set; }
    }

    public class __invalid_type__60172
    {
        public string U { get; set; }
        public int id { get; set; }
        public string N { get; set; }
        public string url { get; set; }
    }

    public class Authors
    {
        public __invalid_type__1 __invalid_name__1 { get; set; }
        public __invalid_type__89651 __invalid_name__89651 { get; set; }
        public __invalid_type__91945 __invalid_name__91945 { get; set; }
        public __invalid_type__93369 __invalid_name__93369 { get; set; }
        public __invalid_type__25018 __invalid_name__25018 { get; set; }
        public __invalid_type__87482 __invalid_name__87482 { get; set; }
        public __invalid_type__2908 __invalid_name__2908 { get; set; }
        public __invalid_type__60172 __invalid_name__60172 { get; set; }
    }

    public class RootObject
    {
        public string result { get; set; }
        public List<Tuc> tuc { get; set; }
        public string phrase { get; set; }
        public string from { get; set; }
        public string dest { get; set; }
        public Authors authors { get; set; }
    }

}
