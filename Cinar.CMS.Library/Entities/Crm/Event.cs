using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    public class Event : NamedEntity, ICriticalEntity
    {
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string EventTime { get; set; }
    }
}
