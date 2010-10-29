using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Database;
using Cinar.Entities.Standart;

namespace Cinar.Entities.MediaSearch
{
    public class ContentText : BaseEntity
    {
        [FieldDetail(References=typeof(Content))]
        public int ContentId { get; set; }

        [FieldDetail(FieldType=DbType.Text)]
        public string Content { get; set; }
    }
}
