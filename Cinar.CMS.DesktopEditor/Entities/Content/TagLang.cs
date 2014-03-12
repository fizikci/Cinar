using System;
using Cinar.Database;
using System.Xml.Serialization;
//using System.IO;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public class TagLang : NamedEntityLang
    {
        private int tagId = 1;
        public int TagId
        {
            get { return tagId; }
            set { tagId = value; }
        }

        private string displayName;
        [ColumnDetail(Length = 100)]
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

    }

}
