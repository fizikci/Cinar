using System;
using System.Text;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Message")]
    public class GenericForm : StaticHtml
    {
        protected string subject = "A form submitted by site visitor";
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        protected string thanksMessage = "Your form has been sent. Thank you";
        public string ThanksMessage
        {
            get { return thanksMessage; }
            set { thanksMessage = value; }
        }

        public GenericForm()
        {
            InnerHtml = @"
<form action=""$=Provider.Request.RawUrl$"" method=""post"">
    <input type=""hidden"" name=""genericFormForm"" value=""$=this.Id$""/>
    $
    if (this.ErrorMessage)
        echo('<div class=""error"">'+this.ErrorMessage+'</div>');
    $

    <table>
        <tr><td>Name</td><td><input type=""text"" name=""Name""/></td></tr>
        <tr><td>Surname</td><td><input type=""text"" name=""Surname""/></td></tr>
        <tr><td>...</td><td>...</td></tr>
    </table>

    <input class=""send"" type=""submit"" value=""Send""/>
</form>
";
        }

        string errorMessage = "";
        public string ErrorMessage { get { return errorMessage; } }

        internal override string show()
        {
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
                    Provider.SendMailWithAttachment(this.Subject, sbMsg.ToString());
                    return thanksMessage;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message.Replace("\n", "<br/>\n");
                }
            }

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
            sb.AppendFormat("#{0}_{1} div.error {{color:crimson;padding:4px;margin:4px;border:1px dashed #efefef}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input.send {{}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }
}
