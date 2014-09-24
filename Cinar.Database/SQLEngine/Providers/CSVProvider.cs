using System;
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
    public class CSVProvider
    {
        private string path;
        private char seperator;

        public CSVProvider(string path, char seperator)
        {
            this.path = path;
            this.seperator = seperator;
        }

        internal List<Hashtable> GetData(Context context, Expression where, ListSelect fieldNames)
        {
            List<Hashtable> list = new List<Hashtable>();

            foreach (CSVItem item in File.ReadAllText(path).Split('\n').Select(line => new CSVItem(line.Split(seperator))))
            {
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

    public class CSVItem : BaseItem
    {
        public CSVItem()
        {
        }

        public CSVItem(string[] vals)
        {
            int i = -1;
            foreach (char c in "ABCDEFGHIJKLMNOPQRSTUWXYZ")
            {
                i++;
                if (vals.Length == i) break;
                this.SetMemberValue(c.ToString(), vals[i]);
            }
        }

        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string E { get; set; }
        public string F { get; set; }
        public string G { get; set; }
        public string H { get; set; }
        public string I { get; set; }
        public string J { get; set; }
        public string K { get; set; }
        public string L { get; set; }
        public string M { get; set; }
        public string N { get; set; }
        public string O { get; set; }
        public string P { get; set; }
        public string Q { get; set; }
        public string R { get; set; }
        public string S { get; set; }
        public string T { get; set; }
        public string U { get; set; }
        public string V { get; set; }
        public string W { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public string Z { get; set; }
    }
}
