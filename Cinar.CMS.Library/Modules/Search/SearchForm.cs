using System;
using System.Text;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Search")]
    public class SearchForm : Module
    {
        private string resultsPage = "";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string ResultsPage
        {
            get { return resultsPage; }
            set { resultsPage = value; }
        }

        protected internal override void beforeShow()
        {
            base.beforeShow();

            if (String.IsNullOrEmpty(this.CSS))
                this.CSS = String.Format("#{0}_{1} span.searchButton {{background:url(external/icons/SearchResults.png);margin-left:10px;cursor:pointer;width:16px}}\n", this.Name, this.Id);
        }

        protected override string show()
        {
            string q = Provider.Request["q"];
            if (q == null) q = "";

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<form id=\"fSearch_{0}\" method=\"get\" action=\"{1}\">", this.Id, this.resultsPage);
            sb.AppendFormat("<input class=\"searchText\" type=\"text\" name=\"q\" value=\"{0}\"/>", Utility.HtmlEncode(q));
            sb.AppendFormat("<span class=\"searchButton\" onClick=\"$('fSearch_{0}').submit();return false;\">&nbsp;</span>", this.Id);
            sb.AppendFormat("</form>");
            return sb.ToString();
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} input.searchText {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} span.searchButton {{background:url(external/icons/SearchResults.png);margin-left:10px;cursor:pointer;width:16px}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }
}
