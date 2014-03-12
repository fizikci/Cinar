using System;
using Cinar.Database;
using System.Drawing;
using System.Xml.Serialization;
//using System.IO;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public class NamedEntityLang : BaseEntity
    {
        public int LangId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public string Picture2 { get; set; }
    }

}
