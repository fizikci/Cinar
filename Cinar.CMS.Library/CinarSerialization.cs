using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Module = Cinar.CMS.Library.Modules.Module;

namespace Cinar.CMS.Library
{
    public class CinarSerialization
    {
        public static string Serialize(object obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Cinar.CMS.Serialization\n");
            foreach (PropertyInfo pi in obj.GetProperties())
            {
                if (pi.Name == "Item") continue;
                if (pi.GetSetMethod() == null) continue;

                object val = pi.GetValue(obj, null);
                if (val != null)
                {
                    string valStr = val.ToString();
                    sb.AppendFormat("{0},{1},{2}", pi.Name, valStr.Length, valStr);
                }
            }
            return sb.ToString();
        }

        public static void Deserialize(object obj, string data)
        {
            foreach (KeyValuePair<string, string> keyValuePair in Deserialize(data))
            {
                try
                {
                    PropertyInfo pi = obj.GetType().GetProperty(keyValuePair.Key);
                    pi.SetValue(obj, keyValuePair.Value.ChangeType(pi.PropertyType), null);
                }catch{}
            }
        }

        public static Dictionary<string, string> Deserialize(string data)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            try
            {
                data = data.Substring("Cinar.CMS.Serialization\n".Length)
                    //
                    //   if Cinar.CMS.Serialization is edited from a different database tool, it may replace \r\n with \n
                    //  in that case cms will not work properly and module will be shown as "Empty".
                    //
                    //   As a solution: new lines are changed to it C# form.
                    .Replace("\r\n", "\n").Replace("\n", "\r\n");

                while (data.Length > 0)
                {
                    string propName = data.Substring(0, data.IndexOf(','));
                    data = data.Substring(propName.Length + 1);
                    string valLengthStr = data.Substring(0, data.IndexOf(','));
                    data = data.Substring(valLengthStr.Length + 1);
                    int length = Int32.Parse(valLengthStr);
                    string valStr = data.Substring(0, length);
                    data = data.Substring(length);

                    res.Add(propName, valStr);
                }
            }
            catch { }
            return res;
        }
    }
}
