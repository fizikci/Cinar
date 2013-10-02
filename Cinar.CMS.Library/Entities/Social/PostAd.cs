using Cinar.CMS.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cinar.CMS.Library.Entities
{
    public class PostAd : BaseEntity
    {
        public int PostId { get; set; }
        public Post Post
        {
            get { return Provider.Database.Read<Post>(PostId); }
        }

        public int PaymentTransactionId { get; set; }
        public PaymentTransaction PaymentTransaction
        {
            get { return Provider.Database.Read<PaymentTransaction>(PaymentTransactionId); }
        }

        public AdStatus AdStatus { get; set; }

        public int ViewCount { get; set; }
        public int ClickCount { get; set; }

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
                    pal.ViewCount += delta;
                    pal.Save();

                    // 1 CPM = 1 TL hesap ediyoruz
                    // Ödenen para kuruş cinsinden olduğuna göre 1 CPM = 1000 views = 100 kuruş
                    // Bu durumda 10 views = 1 kuruş. Yani kuruş olarak ödenen paranın 10 katı view yapılacak 
                    if(this.PaymentTransaction.Amount*10 <= this.ViewCount)
                        this.AdStatus = AdStatus.Completed;
                }
            }

            if (!isUpdate)
            {
                HttpContext.Current.Cache.Remove("post_ads");
                this.PaymentTransactionId = Provider.Database.GetInt("select max(Id) from PaymentTransaction where InsertUserId = " + Provider.User.Id);
            }
        }
    }

    public enum AdStatus
    {
        Request,
        Active,
        Completed
    }
}
