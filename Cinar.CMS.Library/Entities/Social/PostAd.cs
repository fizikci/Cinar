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
        public bool Completed { get; set; }

        protected override void beforeSave(bool isUpdate)
        {
            if (isUpdate)
            {
                var delta = this.ViewCount - (int)this.GetOriginalValues()["ViewCount"];
                if (delta > 0)
                {
                    var pal = Provider.Database.Read<PostAdLog>("PostId={0} AND InsertDate={1}", this.PostId, DateTime.Now.Date);
                    if (pal == null)
                        pal = new PostAdLog { PostId = this.PostId, InsertDate = DateTime.Now.Date };
                    pal.ViewCount = delta;
                    pal.Save();
                }
            }

        }
    }
}
