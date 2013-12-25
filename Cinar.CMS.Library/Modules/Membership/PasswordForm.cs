using System;
using System.Text;
using Cinar.CMS.Library.Entities;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Membership")]
    public class PasswordForm : Module
    {
        internal override string show()
        {
            string infoLabel = Provider.GetModuleResource("Enter your email address and click send button. You will receive your special adress where you can change your password.");
            string emailLabel = Provider.GetModuleResource("E-Mail");

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("<form id=\"fPass\" method=\"post\" action=\"\" onsubmit=\"runModuleMethod('PasswordForm',{0},'SendPassword',$(this).serialize(true),passwordSent); return false;\">", this.Id);
            sb.AppendFormat("<div class=\"passInfoLabel\">{0}</div>", infoLabel);
            sb.AppendFormat("<div class=\"passEmailLabel\">{0}</div>", emailLabel);
            sb.AppendFormat("<input class=\"passEmailInput\" type=\"text\" name=\"email\"/>");
            sb.AppendFormat("<input class=\"passSubmitButton\" type=\"submit\" value=\"{0}\"/>", Provider.GetModuleResource("Send"));
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
            sb.AppendFormat("#{0}_{1} div.passInfoLabel {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.passEmailLabel {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input.passEmailInput {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input.passSubmitButton {{display:block}}\n", this.Name, this.Id);
            return sb.ToString();
        }

        [ExecutableByClient(true)]
        public string SendPassword(string email) 
        {
            Provider.Database.ExecuteNonQuery("update User set Keyword={0} where Email={1}", CMSUtility.MD5(DateTime.Now.Ticks.ToString()), email);

            User user = (User)Provider.Database.Read(typeof(User), "Email={0}", email);
            if (user == null)
                return Provider.GetModuleResource("There isn't any user with the email address you entered. Please check.");

            string msg = String.Format(@"
            {0}
            <br/>
            <a href=""http://{1}/LoginWithKeyword.ashx?keyword={2}&rempass=1"">http://{1}/LoginWithKeyword.ashx?keyword={2}&rempass=1</a>", Provider.GetModuleResource("Please change your password by using the address below"), Provider.Configuration.SiteAddress, user.Keyword);

            string res = Provider.SendMail(email, Provider.GetModuleResource("Your Password"), msg);

            if (res == "")
                return Provider.GetModuleResource("A message sent to your email address. Please read it.");
            else
                return res;
        }
    }
}
