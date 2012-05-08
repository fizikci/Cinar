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
        }

        protected override string show()
        {
            string q = Provider.Request["q"];
            if (q == null) q = "";

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<form id=\"fSearch_{0}\" method=\"get\" action=\"{1}\">", this.Id, this.resultsPage);
            sb.AppendFormat("<input class=\"searchText\" type=\"text\" name=\"q\" value=\"{0}\"/>", CMSUtility.HtmlEncode(q));
            sb.AppendFormat("<span class=\"cbtn cSearchResults\" onClick=\"$('fSearch_{0}').submit();return false;\"></span>", this.Id);
            sb.AppendFormat("</form>");
            return sb.ToString();
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} input.searchText {{}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }
}
