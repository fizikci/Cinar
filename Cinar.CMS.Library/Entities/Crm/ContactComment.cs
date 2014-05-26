using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true)]
    public class ContactComment : BaseEntity
    {
        [ColumnDetail(References = typeof(Contact))]
        public int ContactId { get; set; }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Details { get; set; }
    }
}
