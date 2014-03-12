using System;
using Cinar.Database;
using System.Xml.Serialization;
//using System.IO;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public class SourceLang : NamedEntityLang
    {
        private int sourceId = 1;
        public int SourceId
        {
            get { return sourceId; }
            set { sourceId = value; }
        }

    }

}
