using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    public class EventContact : BaseEntity
    {
        public int EventId { get; set; }
        public int ContactId { get; set; }
        public bool Invited { get; set; }
        public bool Joining { get; set; }
    }
}
