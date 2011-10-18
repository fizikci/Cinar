using System.Text;
using Cinar.CMS.Library.Entities;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Membership")]
    public class UserActivationForm : Module
    {
        protected override string show()
        {
            string infoLabel = Provider.GetModuleResource("Enter your email address and click send button. You will receive your activation code.");
            string emailLabel = Provider.GetModuleResource("E-Mail");

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("<form id=\"fActCode\" method=\"post\" action=\"runModuleMethod('UserActivationForm',{0},'SendCode',$(this).serialize(true),codeSent); return false;\">", this.Id);
            sb.AppendFormat("<div class=\"actCodeInfoLabel\">{0}</div>", infoLabel);
            sb.AppendFormat("<div class=\"actCodeEmailLabel\">{0}</div>", emailLabel);
            sb.AppendFormat("<input class=\"actCodeEmailInput\" type=\"text\" name=\"email\"/>");
            sb.AppendFormat("<input class=\"actCodeSubmitButton\" type=\"submit\" value=\"{0}\"/>", Provider.GetResource("Send"));
            sb.AppendFormat("</form>");

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
            sb.AppendFormat("#{0}_{1} div.actCodeInfoLabel {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.actCodeEmailLabel {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input.actCodeEmailInput {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input.actCodeSubmitButton {{display:block}}\n", this.Name, this.Id);
            return sb.ToString();
        }

        [ExecutableByClient(true)]
        public string SendCode(string email) 
        {
            User user = (User)Provider.Database.Read(typeof(User), "Email={0}", email);
            if (user == null)
                return Provider.GetModuleResource("There isn't any user with the email address you entered. Please check.");

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}.<br/>", Provider.GetModuleResource("Please activate your membership by using the address below"));
            sb.AppendFormat("<a href=\"http://{0}/UserActivation.ashx?keyword={1}\">http://{0}/UserActivation.ashx?keyword={1}</a>", Provider.Configuration.SiteAddress, user.Keyword);

            Provider.SendMail(email, Provider.GetModuleResource("Membership activation"), sb.ToString());
            return Provider.GetModuleResource("A message sent to your email address. Please read it.");
        }
    }
}
