using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.CMS.Library.Entities
{
    public class JobDefinition : SimpleBaseEntity
    {
        public string Name { get; set; }
        public string Request { get; set; }
        public string CommandName { get; set; }
        public int RepeatInSeconds { get; set; }
        public bool Disabled { get; set; }
    }
}
