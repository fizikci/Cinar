using System;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Poll")]
    public class PollResults : Module
    {
        private bool showPercent = true;
        [EditFormFieldProps(Options = "items:_SHOWPERCENT_")]
        public bool ShowPercent
        {
            get { return showPercent; }
            set { showPercent = value; }
        }

        protected string bulletIcon = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string BulletIcon
        {
            get { return bulletIcon; }
            set { bulletIcon = value; }
        }

        internal override string show()
        {
            int pollId = 0; Int32.TryParse(Provider.Request["pollId"], out pollId);
            if (pollId == 0)
                pollId = (int)(Provider.Database.GetValue("select max(Id) from PollQuestion where Visible=1") ?? 0);

            StringBuilder sb = new StringBuilder();
            
            if (pollId>0)
            {
                sb.Append(Poll.GetAnketGraphHTML(pollId, this.showPercent));
            }

            string icon = (this.bulletIcon == "" ? ">> " : "<img src=\"" + this.bulletIcon + "\" align=\"absmiddle\"/> ");

            sb.Append("<div class=\"list\">");
            IDatabaseEntity[] sorular = Provider.Database.ReadList(typeof(PollQuestion), "select * from PollQuestion order by InsertDate desc");
            Provider.Translate(sorular);

            CinarUriParser uriParser = new CinarUriParser(Provider.Request.Url.Scheme + "://" + Provider.Request.Url.Authority + Provider.Request.RawUrl);

            foreach (PollQuestion anket in sorular)
            {
                uriParser.QueryPart["pollId"] = anket.Id.ToString();
                sb.AppendFormat("<div class=\"listQuestion\">{0}<a href=\"{1}\">{2}</a></div>", icon, uriParser.ToString(), anket.Question);
            }
            sb.Append("</div>");

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
            sb.AppendFormat("#{0} div.pollQuestion {{}}\n", getCSSId());
            sb.AppendFormat("#{0} div.pollAnswer {{}}\n", getCSSId());
            sb.AppendFormat("#{0} div.pollBar {{}}\n", getCSSId());
            sb.AppendFormat("#{0} div.bar1 {{background:orange}}\n", getCSSId());
            sb.AppendFormat("#{0} div.bar2 {{background:maroon}}\n", getCSSId());
            sb.AppendFormat("#{0} div.bar3 {{background:navy}}\n", getCSSId());
            sb.AppendFormat("#{0} div.bar4 {{background:green}}\n", getCSSId());
            sb.AppendFormat("#{0} div.list {{}}\n", getCSSId());
            sb.AppendFormat("#{0} div.listQuestion {{}}\n", getCSSId());
            return sb.ToString();
        }
    }
}
