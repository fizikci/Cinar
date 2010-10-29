using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cinar.WinUI
{
    public class Cookie
    {
        public Cookie() 
        {
            SkinName = "Black";
        }

        public string SkinName { get; set; }

        public void Save()
        {
            XmlSerializer ser = new XmlSerializer(typeof(Cookie));
            using (FileStream fs = new FileStream(getCookiePath(), FileMode.Create))
            {
                ser.Serialize(fs, this);
                fs.Close();
            }
        }

        private static string getCookiePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "InterCookie.xml");
        }

        public static Cookie Load()
        {
            Cookie cookie = new Cookie();
            string cookiePath = getCookiePath();

            if (!File.Exists(cookiePath))
            {
                cookie.Save();
            }
            else
            {
                XmlSerializer ser = new XmlSerializer(typeof(Cookie));
                using (FileStream fs = new FileStream(getCookiePath(), FileMode.Open))
                {
                    cookie = (Cookie)ser.Deserialize(fs);
                    fs.Close();
                }
            }

            return cookie;
        }
    }
}
