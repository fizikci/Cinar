using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true)]
    public class ProjectComment : BaseEntity
    {
        [ColumnDetail(References = typeof(Project))]
        public int ProjectId { get; set; }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Details { get; set; }
    }
}
