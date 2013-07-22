using System;
using System.Text;
using Cinar.CMS.Library.Entities;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Membership")]
    public class LoginForm2 : StaticHtml
    {
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string Redirect { get; set; }

        public LoginForm2()
        {
            Redirect = "";
            this.InnerHtml = @"$
if (Provider.User.IsAnonim() || Provider.DesignMode)
{
$
	<form id=""fLogin"" method=""post"" action=""DoLogin.ashx"">
		<input type=""hidden"" name=""RedirectURL"" value=""$=Provider.Request.RawUrl$""/>
	$
	if (Provider.Session['loginError'])
	{
		echo('<div class=""loginError"">' + Provider.Session['loginError'] + '</div>');
		Provider.Session['loginError'] = null;
	}
	$
		<div class=""loginEmailLabel"">Email</div>
		<input class=""loginEmailInput"" type=""text"" name=""Email""/>
		<div class=""loginPassLabel"">Þifre</div>
		<input class=""loginPassInput"" type=""password"" name=""Passwd""/>
		<input class=""loginSubmitButton"" type=""submit"" value=""Giriþ""/>

		<a href=""$= Provider.Configuration.MembershipFormPage $"">Üyelik</a>
		<a href=""$= Provider.Configuration.RememberPasswordFormPage $"">Þifremi unuttum</a>
		<a href=""$= Provider.Configuration.UserActivationPage $"">Aktivasyon kodu</a>
	</form>
$
}
if (!Provider.User.IsAnonim())
{
$
	<span class=""greeting"">Hoþgeldiniz $= Provider.User.Nick $</span>
	<a href=""$= Provider.Configuration.MembershipProfilePage $"">Üyelik Bilgilerim</a>
	$
	if (Provider.ContextUser.IsInRole('Editor'))
		echo('<a href=""' + Provider.Configuration.AdminPage + '"">Site Yönetimi</a>');
	$
	<a href=""/"">Ana Sayfa</a>
	<a href=""DoLogin.ashx?logout=1"">Oturumu Kapat</a>
$
}
$";
        }

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();

            if (!Provider.User.IsAnonim() && !String.IsNullOrEmpty(this.Redirect) && !Provider.DesignMode && !string.IsNullOrWhiteSpace(Provider.Request["formDoLogin"]))
            {
                Provider.Response.Redirect(this.Redirect, true);
                return String.Empty; //***
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
            sb.AppendFormat("#{0}_{1} div.loginEmailLabel {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input.loginEmailInput {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.loginPassLabel {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input.loginPassInput {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.loginError {{color:red}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input.loginSubmitButton {{display:block}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} span.greeting {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} a {{display:block;padding-left:10px}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }
}
