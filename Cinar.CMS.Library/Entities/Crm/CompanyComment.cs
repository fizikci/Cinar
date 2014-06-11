using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true)]
    public class CompanyComment : BaseEntity
    {
        [ColumnDetail(References = typeof(Company))]
        public int CompanyId { get; set; }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Details { get; set; }
    }
}
