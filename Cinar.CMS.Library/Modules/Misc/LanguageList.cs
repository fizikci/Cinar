using System;
using System.Linq;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Misc")]
    public class LanguageList : Module
    {
        private bool showLangName = true;
        public bool ShowLangName
        {
            get { return showLangName; }
            set { showLangName = value; }
        }

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();
            IDatabaseEntity[] langs = Provider.Database.ReadList(typeof(Lang), "select * from Lang order by OrderNo").SafeCastToArray<IDatabaseEntity>();

            CinarUriParser uriParser = new CinarUriParser(Provider.Request.Url.Scheme + "://" + Provider.Request.Url.Authority + Provider.Request.RawUrl);
            //uriParser.QueryPart["currentCulture"] = Provider.Configuration.DefaultLang;
            //sb.AppendFormat("<div><a href=\"{0}\">{1}</a></div>", uriParser.ToString(), Provider.GetResource("Turkish"));

            foreach (Lang lang in langs)
            {
                uriParser.QueryPart["currentCulture"] = lang.Code;
                sb.AppendFormat("<a class=\"{0}\" href=\"{1}\">{2}</a>", lang.Code, uriParser, showLangName ? lang.Name : "");
            }

            return sb.ToString();
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("#{0} {{}}\n", getCSSId());
            sb.AppendFormat("#{0} a {{}}\n", getCSSId());
            sb.AppendFormat("#{0} a:hover {{}}\n", getCSSId());
            sb.AppendFormat("#{0} a.tr-TR {{}}\n", getCSSId());
            sb.AppendFormat("#{0} a.de-DE {{}}\n", getCSSId());
            sb.AppendFormat("#{0} a.en-US {{}}\n", getCSSId());
            return sb.ToString();
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }
    }
}
