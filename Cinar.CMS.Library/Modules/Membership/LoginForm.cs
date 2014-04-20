using System;
using System.Text;
using Cinar.CMS.Library.Entities;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Membership")]
    public class LoginForm : Module
    {
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string Redirect { get; set; }

        public string ShowMembershipLink { get; set; }
        public string ShowMembershipInfoLink { get; set; }
        public string ShowPasswordForgetLink { get; set; }
        public string ShowRememberMe { get; set; }
        public string ShowActivationLink { get; set; }
        public string ShowSiteManagementLink { get; set; }
        public string ShowHomePageLink { get; set; }
        public string ShowLogoutLink { get; set; }

        public LoginForm()
        {
            Redirect = "";
            ShowActivationLink = "Send activation code";
            ShowRememberMe = "Remember me";
            ShowPasswordForgetLink = "Forgot your password?";
            ShowMembershipInfoLink = "My profile";
            ShowMembershipLink = "Sign up";
            ShowSiteManagementLink = "Site Management";
            ShowHomePageLink = "Home Page";
            ShowLogoutLink = "Logout";
        }

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();

            if (!Provider.User.IsAnonim() && !String.IsNullOrEmpty(this.Redirect) && !Provider.DesignMode && !string.IsNullOrWhiteSpace(Provider.Request["formDoLogin"]))
            {
                Provider.Response.Redirect(this.Redirect, true);
                return String.Empty; //***
            }

            string membershipLink = Provider.GetModuleResource(ShowMembershipLink);
            string membershipInfoLink = Provider.GetModuleResource(ShowMembershipInfoLink);
            string passwordForgetLink = Provider.GetModuleResource(ShowPasswordForgetLink);
            string greeting = Provider.GetModuleResource("Welcome");
            string emailLabel = Provider.GetModuleResource("E-Mail");
            string passLabel = Provider.GetModuleResource("Password");
            string rememberMeLabel = Provider.GetModuleResource(ShowRememberMe);
            string sendActivationLabel = Provider.GetModuleResource(ShowActivationLink);

            if (Provider.User.IsAnonim() || Provider.DesignMode)
            {
                User user = null;
                bool toBeRemembered = Provider.Request.Cookies["keyword"] != null && Provider.Request.Cookies["keyword"].Value != "";
                if (toBeRemembered)
                {
                    user = (User)Provider.Database.Read(typeof(User), "Keyword={0}", Provider.Request.Cookies["keyword"].Value);
                    if (user == null) toBeRemembered = false;
                }

                sb.AppendFormat("<form id=\"fLogin\" method=\"post\" action=\"DoLogin.ashx\">");
                sb.AppendFormat("<input type=\"hidden\" name=\"RedirectURL\" value=\"{0}\"/>", Provider.Request.RawUrl);
                if (Provider.Session["loginError"] != null)
                {
                    sb.AppendFormat("<div class=\"loginError\">{0}</div>", Provider.Session["loginError"]);
                    Provider.Session["loginError"] = null;
                }
                sb.AppendFormat("<div class=\"loginEmailLabel\">{0}</div>", emailLabel);
                sb.AppendFormat("<input class=\"loginEmailInput\" type=\"text\" name=\"Email\"{0}/>", toBeRemembered ? " value=\"" + user.Email + "\"" : "");
                sb.AppendFormat("<div class=\"loginPassLabel\">{0}</div>", passLabel);
                sb.AppendFormat("<input class=\"loginPassInput\" type=\"password\" name=\"Passwd\"{0}/>", toBeRemembered ? " value=\"remember\"" : "");
                if (!string.IsNullOrWhiteSpace(ShowRememberMe))
                    sb.AppendFormat("<div class=\"loginRememberMeLabel\"><input type=\"checkbox\" name=\"RememberMe\" value=\"1\"{0}/> {1}</div>", toBeRemembered ? " checked" : "", rememberMeLabel);
                sb.AppendFormat("<input class=\"loginSubmitButton\" type=\"submit\" value=\"{0}\"/>", Provider.GetModuleResource("Enter"));
                if (!string.IsNullOrWhiteSpace(ShowMembershipLink))
                    sb.AppendFormat("<a href=\"{0}\">{1}</a>", Provider.Configuration.MembershipFormPage, membershipLink);
                if (!string.IsNullOrWhiteSpace(ShowPasswordForgetLink))
                    sb.AppendFormat("<a href=\"{0}\">{1}</a>", Provider.Configuration.RememberPasswordFormPage, passwordForgetLink);
                if (!string.IsNullOrWhiteSpace(ShowActivationLink))
                    sb.AppendFormat("<a href=\"{0}\">{1}</a>", Provider.Configuration.UserActivationPage, sendActivationLabel);
                sb.AppendFormat("</form>");
            }
            if (!Provider.User.IsAnonim())
            {
                sb.AppendFormat("<span class=\"greeting\">{0} {1}</span>", greeting, Provider.User.Nick);
                if (!string.IsNullOrWhiteSpace(ShowMembershipInfoLink))
                    sb.AppendFormat("<a href=\"{0}\">{1}</a>", Provider.Configuration.MembershipProfilePage, membershipInfoLink);
                if (Provider.ContextUser.IsInRole("Designer"))
                {
                    CinarUriParser uriParser = Provider.Request.RawUrl.Contains(".ashx") ? new CinarUriParser(Provider.Request.Url.Scheme + "://" + Provider.Request.Url.Authority + "/" + Provider.Configuration.MainPage) : new CinarUriParser(Provider.Request.Url.Scheme + "://" + Provider.Request.Url.Authority + Provider.Request.RawUrl);
                    if (Provider.DesignMode)
                    {
                        uriParser.QueryPart["DesignMode"] = "Off";
                        sb.AppendFormat("<a href=\"{0}\">{1}</a>", uriParser, Provider.GetResource("View Mode"));
                    }
                    else
                    {
                        uriParser.QueryPart["DesignMode"] = "On";
                        sb.AppendFormat("<a href=\"{0}\">{1}</a>", uriParser, Provider.GetResource("Design Mode"));
                    }
                }
                if (Provider.ContextUser.IsInRole("Editor") && !string.IsNullOrWhiteSpace(ShowSiteManagementLink))
                    sb.AppendFormat("<a href=\"{0}\">{1}</a>", Provider.Configuration.AdminPage, Provider.GetResource(ShowSiteManagementLink));
                if (!string.IsNullOrWhiteSpace(ShowHomePageLink))
                    sb.AppendFormat("<a href=\"{0}\">{1}</a>", Provider.Configuration.MainPage, Provider.GetModuleResource(ShowHomePageLink));
                if (!string.IsNullOrWhiteSpace(ShowLogoutLink))
                    sb.AppendFormat("<a href=\"DoLogin.ashx?logout=1\">{0}</a>", Provider.GetResource(ShowLogoutLink));
            }
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
            sb.AppendFormat("#{0} div.loginEmailLabel {{}}\n", getCSSId());
            sb.AppendFormat("#{0} input.loginEmailInput {{}}\n", getCSSId());
            sb.AppendFormat("#{0} div.loginPassLabel {{}}\n", getCSSId());
            sb.AppendFormat("#{0} input.loginPassInput {{}}\n", getCSSId());
            sb.AppendFormat("#{0} div.loginRememberMeLabel {{}}\n", getCSSId());
            sb.AppendFormat("#{0} div.loginError {{color:red}}\n", getCSSId());
            sb.AppendFormat("#{0} input.loginSubmitButton {{display:block}}\n", getCSSId());
            sb.AppendFormat("#{0} span.greeting {{}}\n", getCSSId());
            sb.AppendFormat("#{0} a {{display:block;padding-left:10px}}\n", getCSSId());
            return sb.ToString();
        }
    }
}
