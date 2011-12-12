using System;
using System.Text;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Containers")]
    public class Container : ModuleContainer, IRegionContainer
    {
        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            string frmRegion = Provider.GetRegionInnerHtml(this.ChildModules);
            if (!String.IsNullOrEmpty(frmRegion))
                sb.AppendFormat("<div id=\"frmRegion{0}\" class=\"frmRegion Region\">{1}</div>", this.Id, frmRegion);

            return sb.ToString();
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            return sb.ToString();
        }
    }

}
