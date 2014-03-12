using System;
using Cinar.Database;
using System.Drawing;
using System.Xml.Serialization;
using System.Web;
//using System.IO;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public class StaticResourceLang : BaseEntity
    {
        public int StaticResourceId { get; set; }
        public int LangId { get; set; }
        public string Translation { get; set; }
    }

}
