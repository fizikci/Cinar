using System.Collections.Generic;
using System.Text;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Containers")]
    public class TabView : ModuleContainer, IRegionContainer
    {
        protected string tabs = "Tab 1,Tab 2";
        [ColumnDetail(IsNotNull = true, DefaultValue = "Tab 1,Tab 2", Length=300)]
        public string Tabs
        {
            get { return tabs; }
            set { tabs = value; }
        }

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();
            string[] tabPages;

            sb.AppendFormat("<div class=\"tabButtons\" id=\"tabButtons{0}\">", Id);
            tabPages = tabs.Split(',');
            for (int i = 0; i < tabPages.Length; i++)
                sb.AppendFormat("<a class=\"{2}\" href=\"#\" onclick=\"showTabPage({0},{1});return false;\">{3}</a>", Id, i, i>0?"tabBtnPassive":"tabBtnActive", tabPages[i]);
            sb.Append("</div><div style=\"clear:both\"></div>");

           
            for (int i = 0; i < tabPages.Length; i++)
            {
                string conRegion = Provider.GetRegionInnerHtml(getPageModules(i));
                sb.AppendFormat("<div id=\"TabPage_{0}_{1}\" class=\"Region\" style=\"display:{3}\">{2}</div>", this.Id, i, conRegion, i>0?"none":"");
            }

            return sb.ToString();
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }

        private List<Module> getPageModules(int pageNo)
        {
            string regionId = "TabPage_" + this.Id + "_" + pageNo;
            return ChildModules.FindAll(delegate(Module mdl) { return mdl.Region == regionId; });
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} div.tabButtons {{font-weight:bold; font-size:12px;}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} a.tabBtnActive {{padding:4px;background:orange;color:white;border-top:2px solid orange;display:block;float:left;width:auto;margin-right:4px;}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} a.tabBtnActive:hover {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} a.tabBtnPassive {{padding:4px;background:silver;border-top:2px solid silver;color:orange;display:block;float:left;width:auto;margin-right:4px;}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} a.tabBtnPassive:hover {{border-top:2px solid orange}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.Region {{padding:10px;border:2px solid orange;background:white}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }
}
