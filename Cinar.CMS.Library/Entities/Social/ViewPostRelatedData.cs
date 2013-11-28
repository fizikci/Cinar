using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    public class ViewPostRelatedData
    {
        public int Id { get; set; }

        public List<ViewMiniUserInfo> RelatedUsers { get; set; }

        public List<ViewPost> Replies { get; set; }

        public ViewPost ReplyToPost { get; set; }
    }
}
