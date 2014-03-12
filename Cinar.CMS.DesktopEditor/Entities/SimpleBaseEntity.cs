using System;
using System.Collections.Generic;
using Cinar.Database;
using System.Xml.Serialization;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public abstract class SimpleBaseEntity
    {
        public int Id { get; set; }

        public DateTime InsertDate { get; set; }
    }
}
