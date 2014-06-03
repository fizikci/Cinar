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
        [ColumnDetail(References = typeof(Event))]
        public int EventId { get; set; }
        
        [ColumnDetail(References = typeof(Contact))]
        public int ContactId { get; set; }

        [ColumnDetail(ColumnType = DbType.VarChar, Length=8)]
        public EventContactStates State { get; set; }
    }

    public enum EventContactStates
    {
        None,
        Invited,
        Joining,
        Rejected
    }
}
