using Cinar.CMS.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cinar.CMS.Library.Entities
{
    public class PostBlacklist : BaseEntity
    {
        public int PostId { get; set; }
        public Post Post
        {
            get { return Provider.Database.Read<Post>(PostId); }
        }

        public int WordBlacklistId { get; set; }
        public WordBlacklist PaymentTransaction
        {
            get { return Provider.Database.Read<WordBlacklist>(WordBlacklistId); }
        }
    }
}
