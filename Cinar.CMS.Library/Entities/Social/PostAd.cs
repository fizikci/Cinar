using Cinar.CMS.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.CMS.Library.Entities
{
    public class PostAd : BaseEntity
    {
        public int PostId { get; set; }
        public int ViewCount { get; set; }
    }
}
