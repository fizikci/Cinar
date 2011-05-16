using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Database;
using Cinar.Entities.Standart;

namespace Cinar.Entities.MediaSearch
{
    public class ContentTag : BaseEntity
    {
        [ColumnDetail(References=typeof(Content))]
        public int ContentId { get; set; }

        [ColumnDetail(References=typeof(Tag))]
        public int TagId { get; set; }
    }
}
