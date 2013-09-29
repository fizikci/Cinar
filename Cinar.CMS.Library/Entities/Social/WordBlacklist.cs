using Cinar.CMS.Library;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    public class WordBlacklist : BaseEntity
    {
        public string Name { get; set; }

    }
}
