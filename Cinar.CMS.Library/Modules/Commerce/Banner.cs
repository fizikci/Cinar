using System;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System.Collections;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Commerce")]
    public class Banner : Module
    {
        private BannerAd _bannerAd;
        private BannerAd bannerAd
        {
            get 
            {
                if (_bannerAd == null)
                {
                    if (fixedBannerId > 0) // sabit reklamın bitiş süresi geçmemişse gösterilsin (bu pek hoş bir şey değil aslında, bitiş süresi dolduktan sonra bile gereksiz okuma yapmaya devam edecek)
                    {
                        _bannerAd = (BannerAd)Provider.Database.Read(typeof(BannerAd), fixedBannerId);
                        if (_bannerAd.FinishDate > DateTime.Now)
                            return _bannerAd; //***
                    }

                    ArrayList al = new ArrayList();
                    if (this.useTags && Provider.Content != null && !String.IsNullOrEmpty(Provider.Content.Tags))
                    {
                        string tagWhere = "Tags like '%" + Provider.Content.Tags.Replace("'", "''").Replace(",", "%' or Tags like '%") + "%'";
                        foreach (BannerAd banner in Provider.Database.ReadList(typeof(BannerAd), "select * from BannerAd where Visible=1 and " + tagWhere))
                            al.Add(banner);
                    }
                    if (al.Count == 0)
                    {
                        foreach (BannerAd banner in Provider.Database.ReadList(typeof(BannerAd), "select * from BannerAd where Visible=1"))
                            al.Add(banner);
                    }
                    if (al.Count == 0)
                        throw new Exception(Provider.GetResource("There is no suitable banner to show!"));
                    _bannerAd = (BannerAd)al[new Random().Next(al.Count)];
                }
                return _bannerAd;
            }
        }

        private bool useTags;
        public bool UseTags
        {
            get { return useTags; }
            set { useTags = value; }
        }

        private int fixedBannerId = 0;
        [ColumnDetail(DefaultValue = "0", References = typeof(BannerAd)), EditFormFieldProps(ControlType = ControlType.LookUp, Options = "addBlankItem:false")]
        public int FixedBannerId
        {
            get { return fixedBannerId; }
            set { fixedBannerId = value; }
        }


        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            // botlara bannerlarımızı tüketmeyelim, günah.
            if (Utility.RequestByBot)
                return String.Empty;
            
            // Application değişkeninde bugün için tarihe göre visible ayarları yapılmış mı kontrolü yap
            if (Provider.Application[DateTime.Now.Date.ToString()] == null)
            {
                // düne ait girişi sil
                if (Provider.Application[DateTime.Now.AddDays(-1).Date.ToString()] != null)
                    Provider.Application.Remove(DateTime.Now.AddDays(-1).Date.ToString());
                // bugünü ekle
                Provider.Application[DateTime.Now.Date.ToString()] = "OK";
                // tarih aralığı bugünü içeren reklamları visible, diğerlerini invisible yap.
                // önce tarihe göre olan bannerlar
                foreach (BannerAd banner in Provider.Database.ReadList(typeof(BannerAd), "select * from BannerAd where StartDate<={0} and FinishDate>={0} and ViewCondition='DateInterval' and Visible=0", DateTime.Now.Date))
                {
                    banner.Visible = true;
                    banner.Save();
                }
                foreach (BannerAd banner in Provider.Database.ReadList(typeof(BannerAd), "select * from BannerAd where (StartDate>{0} or FinishDate<{0}) and ViewCondition='DateInterval' and Visible=1", DateTime.Now.Date))
                {
                    banner.Visible = false;
                    banner.Save();
                }
                // tarihe göre olmayıp StartDate'i gelmiş yeni bannerları visible yap
                foreach (BannerAd banner in Provider.Database.ReadList(typeof(BannerAd), "select * from BannerAd where StartDate<={0} and ViewCondition<>'DateInterval' and Visible=0 and ViewCount=0", DateTime.Now.Date))
                {
                    banner.Visible = true;
                    banner.Save();
                }
            }

            bool isFlash = bannerAd.BannerFile.EndsWith(".swf");
            string onClick = String.Format("runModuleMethod('Banner',{0},'AdClick',{{bannerId:{1}}});", this.Id, bannerAd.Id);

            if (isFlash) //TODO: flash htmlini düzelt.
                sb.AppendFormat("<object src=\"{0}\"><embed src=\"{0}\"></embed></object>", bannerAd.BannerFile);
            else
                sb.AppendFormat("<a href=\"{0}\" onclick=\"{1}\" target=\"_blank\"><img src=\"{2}\" border=\"0\"/></a>", bannerAd.Href.StartsWith("http") ? bannerAd.Href : ("http://" + bannerAd.Href), onClick, bannerAd.BannerFile);

            if (!Provider.DesignMode)
            {
                bannerAd.ViewCount++;
                if (bannerAd.ViewCondition == "View" && bannerAd.ViewCount >= bannerAd.MaxViewCount)
                {
                    bannerAd.Visible = false;
                    Provider.SendMail(Provider.GetResource("Banner publish ends"), Provider.GetResource("Banner with id {0} and name \"{1}\" ran out of its credits. It will not be published anymore.", bannerAd.Id, bannerAd.Name));
                }
                bannerAd.Save();
            }

            return sb.ToString();
        }

        [ExecutableByClient(true)]
        public string AdClick(int bannerId)
        {
            BannerAd bannerAd = (BannerAd)Provider.Database.Read(typeof(BannerAd), bannerId);
            bannerAd.ClickCount++;
            if (bannerAd.ViewCondition == "Click" && bannerAd.ClickCount >= bannerAd.MaxClickCount)
            {
                bannerAd.Visible = false;
                Provider.SendMail(Provider.GetResource("Banner publish ends"), Provider.GetResource("Banner with id {0} and name \"{1}\" ran out of its credits. It will not be published anymore.", bannerAd.Id, bannerAd.Name));
            }
            bannerAd.Save();

            return "OK";
        }


        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("#{0}_{1} {{text-align:center}}\n", this.Name, this.Id);
            return sb.ToString();
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }
    }

}
