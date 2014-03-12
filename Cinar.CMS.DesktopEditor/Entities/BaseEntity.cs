using System;
using System.Collections.Generic;
using Cinar.Database;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime InsertDate { get; set; }

        public int InsertUserId { get; set; }

        public DateTime UpdateDate { get; set; }

        public int UpdateUserId { get; set; }

        public BaseEntity()
        {
        }

        public bool Visible { get; set; }

        public int OrderNo { get; set; }

    }
}
