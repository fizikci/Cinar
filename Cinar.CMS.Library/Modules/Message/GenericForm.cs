using System;
using System.Text;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Message")]
    public class GenericForm : Module
    {
        private string formHtml = @"<table><tr><td>Name</td><td><input type=""text"" name=""name""/></td></tr><tr><td>Surname</td><td><input type=""text"" name=""Surname""/></td></tr><tr><td>...</td><td>...</td></tr></table>";
        [ColumnDetail(ColumnType=Cinar.Database.DbType.Text)]
        public string FormHtml
        {
            get { return formHtml; }
            set { formHtml = value; }
        }

        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            string thanksMessage = Provider.GetModuleResource("Your form has been sent. Thank you.");
            string btnText = Provider.GetResource("Send");

            string errorMessage = "";
            if (Provider.Request["genericFormForm"] == this.Id.ToString())
            {
                string rowFormat = "<tr><td valign=top>{0} :</td><td valign=top>{1}</td></tr>";

                StringBuilder sbMsg = new StringBuilder();
                sbMsg.Append("<table>");
                foreach (string key in Provider.Request.Form.AllKeys) 
                {
                    if (key == "genericFormForm") continue; //***
                    sbMsg.AppendFormat(rowFormat, key, Provider.Request[key]);
                }
                sbMsg.Append("</table>");
                sbMsg.Append("<hr/>");
                sbMsg.Append("<table>");
                sbMsg.AppendFormat(rowFormat, Provider.GetModuleResource("Date"), DateTime.Now);
                sbMsg.AppendFormat(rowFormat, Provider.GetModuleResource("User"), Provider.User.Name + " (ID: " + Provider.User.Id + ")");
                sbMsg.AppendFormat(rowFormat, Provider.GetModuleResource("IP"), Provider.Request.UserHostAddress);
                sbMsg.AppendFormat(rowFormat, Provider.GetModuleResource("Referrer"), Provider.Request.UrlReferrer.ToString());
                sbMsg.AppendFormat(rowFormat, Provider.GetModuleResource("User Agent"), Provider.Request.UserAgent);
                sbMsg.Append("</table>");

                try
                {
                    Provider.SendMail(Provider.GetModuleResource("A form submitted by site visitor"), sbMsg.ToString());
                    return thanksMessage;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message.Replace("\n", "<br/>\n");
                }
            }

            sb.Append("<form action=\"" + Provider.Request.RawUrl + "\" method=\"post\">");
            sb.AppendFormat("<input type=\"hidden\" name=\"genericFormForm\" value=\"{0}\"/>", this.Id);

            if (errorMessage != "")
                sb.AppendFormat("<div class=\"error\">{0}</div>", errorMessage);

            sb.Append(string.IsNullOrEmpty(this.formHtml) ? Provider.Content.Metin : this.FormHtml);

            sb.AppendFormat("<input class=\"send\" type=\"submit\" value=\"{0}\"/>", btnText);

            sb.Append("</form>");

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
            sb.AppendFormat("#{0}_{1} div.error {{color:crimson;padding:4px;margin:4px;border:1px dashed #efefef}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input.send {{}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }
}
