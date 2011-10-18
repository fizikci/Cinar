using System;
using System.Text;
using Cinar.CMS.Library.Entities;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Membership")]
    public class LoginForm : Module
    {
        protected string redirect = "";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string Redirect
        {
            get { return redirect; }
            set { redirect = value; }
        }
        
        private bool showMembershipLink = true;
        public bool ShowMembershipLink
        {
            get { return showMembershipLink; }
            set { showMembershipLink = value; }
        }

        private bool showMembershipInfoLink = true;
        public bool ShowMembershipInfoLink
        {
            get { return showMembershipInfoLink; }
            set { showMembershipInfoLink = value; }
        }

        private bool showPasswordForgetLink = true;
        public bool ShowPasswordForgetLink
        {
            get { return showPasswordForgetLink; }
            set { showPasswordForgetLink = value; }
        }

        private bool showRememberMe = true;
        public bool ShowRememberMe
        {
            get { return showRememberMe; }
            set { showRememberMe = value; }
        }

        private bool showActivationLink = true;
        public bool ShowActivationLink
        {
            get { return showActivationLink; }
            set { showActivationLink = value; }
        }


        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            if (!Provider.User.IsAnonim() && !String.IsNullOrEmpty(this.redirect) && !Provider.DesignMode)
            {
                Provider.Response.Redirect(this.redirect, true);
                return String.Empty; //***
            }

            string membershipLink = Provider.GetModuleResource("Sign up");
            string membershipInfoLink = Provider.GetModuleResource("My profile");
            string passwordForgetLink = Provider.GetModuleResource("Forgot your password?");
            string greeting = Provider.GetModuleResource("Welcome");
            string emailLabel = Provider.GetModuleResource("E-Mail");
            string passLabel = Provider.GetModuleResource("Password");
            string rememberMeLabel = Provider.GetModuleResource("Remember me");
            string sendActivationLabel = Provider.GetModuleResource("Send activation code");

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
                if (showRememberMe)
                    sb.AppendFormat("<div class=\"loginRememberMeLabel\"><input type=\"checkbox\" name=\"RememberMe\" value=\"1\"{0}/> {1}</div>", toBeRemembered ? " checked" : "", rememberMeLabel);
                sb.AppendFormat("<input class=\"loginSubmitButton\" type=\"submit\" value=\"{0}\"/>", Provider.GetModuleResource("Enter"));
                if (showMembershipLink)
                    sb.AppendFormat("<a href=\"{0}\">{1}</a>", Provider.Configuration.MembershipFormPage, membershipLink);
                if (showPasswordForgetLink)
                    sb.AppendFormat("<a href=\"{0}\">{1}</a>", Provider.Configuration.RememberPasswordFormPage, passwordForgetLink);
                if (showActivationLink)
                    sb.AppendFormat("<a href=\"{0}\">{1}</a>", Provider.Configuration.UserActivationPage, sendActivationLabel);
                sb.AppendFormat("</form>");
            }
            if (!Provider.User.IsAnonim() && String.IsNullOrEmpty(this.Redirect))
            {
                sb.AppendFormat("{0} {1}", greeting, Provider.User.Nick);
                if (showMembershipInfoLink)
                    sb.AppendFormat("<a href=\"{0}\">{1}</a>", Provider.Configuration.MembershipProfilePage, membershipInfoLink);
                if (Provider.ContextUser.IsInRole("Designer"))
                {
                    UriParser uriParser = Provider.Request.Url.ToString().Contains(".ashx") ? new UriParser(Provider.Configuration.MainPage) : new UriParser(Provider.Request.Url.ToString());
                    if (Provider.DesignMode)
                    {
                        uriParser.QueryPart["DesignMode"] = "Off";
                        sb.AppendFormat("<a href=\"{0}\">{1}</a>", uriParser.ToString(), Provider.GetResource("View Mode"));
                    }
                    else
                    {
                        uriParser.QueryPart["DesignMode"] = "On";
                        sb.AppendFormat("<a href=\"{0}\">{1}</a>", uriParser.ToString(), Provider.GetResource("Design Mode"));
                    }
                }
                if (Provider.ContextUser.IsInRole("Editor"))
                        sb.AppendFormat("<a href=\"{0}\">{1}</a>", "Admin/"/*Provider.Configuration.AdminPage*/, Provider.GetResource("Site Management"));
                sb.AppendFormat("<a href=\"{0}\">{1}</a>", Provider.Configuration.MainPage, Provider.GetModuleResource("Home Page"));
                sb.AppendFormat("<a href=\"DoLogin.ashx?logout=1\">{0}</a>", Provider.GetResource("Logout"));
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
            sb.AppendFormat("#{0}_{1} div.loginEmailLabel {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input.loginEmailInput {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.loginPassLabel {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input.loginPassInput {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.loginRememberMeLabel {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.loginError {{color:red}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input.loginSubmitButton {{display:block}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} a {{display:block;padding-left:10px}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }
}
