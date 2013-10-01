using Cinar.CMS.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.CMS.Library.Entities
{
    /// <summary>
    /// Bu entity PostAd görüntülenme logunu tutar.
    /// InsertDate sadece günü gösterir, saat ve dakikayı göstermez.
    /// Böylece bir Post'un her gün kaç defa gösterildiğinin raporu bu tablodan alınabilir.
    /// </summary>
    public class PostAdLog : SimpleBaseEntity
    {
        public int PostId { get; set; }
        public int ViewCount { get; set; }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            if (!isUpdate) {
                this.InsertDate = DateTime.Now.Date;
            }
        }
    }
}
