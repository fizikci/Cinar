using System;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System.Web;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Poll")]
    public class Poll : Module
    {
        private int pollQuestionId = 0;
        [ColumnDetail(IsNotNull=true, DefaultValue="0", References=typeof(PollQuestion))]
        [EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int PollQuestionId
        {
            get { return pollQuestionId; }
            set { pollQuestionId = value; }
        }

        private bool showPercent = true;
        [EditFormFieldProps(Options = "items:_SHOWPERCENT_")]
        public bool ShowPercent
        {
            get { return showPercent; }
            set { showPercent = value; }
        }

        private bool voted;
        private string cookieName;
        private int _pollId;
        private int pollId {
            get{
                _pollId = this.PollQuestionId;
                if (_pollId == 0)
                    _pollId = (int)(Provider.Database.GetValue("select max(Id) from PollQuestion where Visible=1") ?? 0);
                return _pollId;
            }
        }


        protected internal override void beforeShow()
        {
            cookieName = "ank" + pollId;
            voted = Provider.Request.Cookies[cookieName] != null && Provider.Request.Cookies[cookieName].Value == "1";

            // bu requestte oylama yapıldıysa cevabı kaydedelim
            if (Provider.Request.Form["cevap"] != null && Provider.Request.Form["anketId"] != null && Provider.Request.Form["anketId"] == pollQuestionId.ToString())
            {
                PollAnswer cevap = (PollAnswer)Provider.Database.Read(typeof(PollAnswer), Convert.ToInt32(Provider.Request.Form["cevap"]));
                cevap.Hit++;
                cevap.Save();
                Provider.Response.Cookies.Add(new HttpCookie(cookieName, "1"));
                voted = true;
            }
        }

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();
            
            if (voted)
            {
                sb.Append(Poll.GetAnketGraphHTML(pollId, this.showPercent));
            }
            else
            {
                if (pollId == 0)
                {
                    sb.Append(Provider.GetResource("Select poll"));
                    return sb.ToString();//***
                }
                sb.AppendFormat("<form name={0} action=\"{1}\" method=post>", cookieName, Provider.Request.RawUrl);

                PollQuestion soru = (PollQuestion)Provider.Database.Read(typeof(PollQuestion), pollId);
                Provider.Translate(new IDatabaseEntity[] { soru });

                sb.AppendFormat("<input type=\"hidden\" name=\"anketId\" value=\"{0}\">", pollQuestionId);
                sb.AppendFormat("<div class=\"pollQuestion\">{0}</div>", soru.Question);

                IDatabaseEntity[] cevaplar = Provider.Database.ReadList(typeof(PollAnswer), "select * from PollAnswer where PollQuestionId={0}", pollId).SafeCastToArray<IDatabaseEntity>();
                Provider.Translate(cevaplar);

                foreach (PollAnswer cevap in cevaplar)
                    sb.AppendFormat("<div class=\"pollAnswer\"><input type=\"radio\" name=\"cevap\" value=\"{0}\"/> {1}</div>", cevap.Id, cevap.Answer);
                
                sb.AppendFormat("<input type=\"submit\" value=\"{0}\">", Provider.GetModuleResource("Vote"));
                sb.Append("</form>");
            }
            return sb.ToString();
        }

        internal static string GetAnketGraphHTML(int ankSoruId, bool showPercent)
        {
            StringBuilder sb = new StringBuilder();

            PollQuestion soru = (PollQuestion)Provider.Database.Read(typeof(PollQuestion), ankSoruId);
            Provider.Translate(new IDatabaseEntity[] { soru });

            sb.Append("<div class=\"pollQuestion\">" + soru.Question + "</div>");

            IDatabaseEntity[] cevaplar = Provider.Database.ReadList(typeof(PollAnswer), "select * from PollAnswer where PollQuestionId={0}", ankSoruId).SafeCastToArray<IDatabaseEntity>();
            Provider.Translate(cevaplar);

            int maxHit = 0;
            foreach (PollAnswer cevap in cevaplar)
                if (maxHit < cevap.Hit)
                    maxHit = cevap.Hit;

            for (int i = 0; i < cevaplar.Length; i++)
            {
                PollAnswer cevap = (PollAnswer)cevaplar[i];
                int yuzde = 0;
                if (maxHit > 0) yuzde = (int)Math.Round((double)cevap.Hit / (double)maxHit * 100);
                sb.AppendFormat("<div class=\"pollAnswer\">{0} ({1})</div><div class=\"poolBar bar{2}\" style=\"width:{3}px\">&nbsp;</div>", cevap.Answer, showPercent ? "% " + yuzde : cevap.Hit.ToString(), i + 1, yuzde);
            }

            return sb.ToString();
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0} div.pollQuestion {{padding-left:6px}}\n", getCSSId());
            sb.AppendFormat("#{0} div.pollAnswer {{padding-left:6px}}\n", getCSSId());
            sb.AppendFormat("#{0} div.poolBar {{}}\n", getCSSId());
            sb.AppendFormat("#{0} div.bar1 {{background:orange}}\n", getCSSId());
            sb.AppendFormat("#{0} div.bar2 {{background:maroon}}\n", getCSSId());
            sb.AppendFormat("#{0} div.bar3 {{background:navy}}\n", getCSSId());
            sb.AppendFormat("#{0} div.bar4 {{background:green}}\n", getCSSId());
            return sb.ToString();
        }
    }
}
