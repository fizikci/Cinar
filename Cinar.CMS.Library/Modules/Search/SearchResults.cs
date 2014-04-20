using System;
using System.Text;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Search")]
    public class SearchResults : DataList
    {
        public SearchResults()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"resultItem\">");
            sb.Append("<div class=\"title\"><a href=\"$=entity.PageLink$\">$=entity.Title$</a></div>");
            sb.Append("<div class=\"desc\">$=entity.Description$</div>");
            sb.Append("</div>");

            this.DataTemplate = sb.ToString();
            this.EntityName = "Content";
            //this.ShowFields = "Id,Title,ShowInPage,ClassName,Hierarchy,Description";
        }

        internal override string show()
        {
            string q = Provider.Request["q"];

            if (!String.IsNullOrEmpty(q))
                this.defaultWhere = "(Title like '%" + q.Replace("'", "''") + "%' or Description like '%" + q.Replace("'", "''") + "%')";
            else
                this.defaultWhere = "1<>1";

            return base.show();
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0} div.resultItem {{}}\n", getCSSId());
            sb.AppendFormat("#{0} div.title {{font-weight:bold}}\n", getCSSId());
            sb.AppendFormat("#{0} div.desc {{}}\n", getCSSId());
            return sb.ToString();
        }
    }
}
