using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Event.Id as [Event.Id], Event.Name as [NamedEntity.Name] from Event", QueryOrderBy = "Event.Id desc")]
    public class Event : NamedEntity, ICriticalEntity
    {
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string EventTime { get; set; }
    }
}
