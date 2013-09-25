using Cinar.CMS.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.CMS.Library.Entities
{
    public class PostHashTag : SimpleBaseEntity
    {
        public int PostId { get; set; }
        public int HashTagId { get; set; }
    }
}
