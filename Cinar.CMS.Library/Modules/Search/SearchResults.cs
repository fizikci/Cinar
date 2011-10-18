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
            sb.Append("<div class=\"title\"><a href=\"$=this.PageLink$?item=$=entity.Id$\">$=entity.Title$</a></div>");
            sb.Append("<div class=\"desc\">$=entity.Description$</div>");
            sb.Append("</div>");

            this.DataTemplate = sb.ToString();
            this.EntityName = "Content";
            //this.ShowFields = "Id,Title,ShowInPage,ClassName,Hierarchy,Description";
        }

        protected override string show()
        {
            string q = Provider.Request["q"];

            if (!String.IsNullOrEmpty(q))
                this.defaultWhere = "(Title like '%" + q.Replace("'", "''") + "%' or Description like '%" + q.Replace("'", "''") + "%')";
            else
                this.defaultWhere = "1<>1";

            this.sayNoRecord = false;

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
            sb.AppendFormat("#{0}_{1} div.resultItem {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.title {{font-weight:bold}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.desc {{}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }
}
