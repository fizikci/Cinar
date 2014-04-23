using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Definition.Id as [Definition.Id], Definition.Name as [NamedEntity.Name] from Definition", QueryOrderBy = "Definition.Id desc")]
    public class Definition : NamedEntity
    {
        public string Kind { get; set; }
    }
}
