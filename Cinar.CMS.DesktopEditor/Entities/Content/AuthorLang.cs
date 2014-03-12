using System;
using Cinar.Database;
using System.Xml.Serialization;
//using System.IO;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public class AuthorLang : NamedEntityLang
    {
        public string Title { get; set; }

        private int authorId = 1;
        public int AuthorId
        {
            get { return authorId; }
            set { authorId = value; }
        }
    }

}
