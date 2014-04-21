using System;
using System.Text;
using Cinar.CMS.Library.Entities;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Membership")]
    public class LoginForm2 : StaticHtml
    {
        public LoginForm2()
        {
            this.InnerHtml = @"$
if (Provider.User.IsAnonim() || Provider.DesignMode)
{
$
	<form id=""fLogin"" method=""post"" action=""DoLogin.ashx"">
    <fieldset>
    <legend>Oturum A�</legend>
		<input type=""hidden"" name=""RedirectURL"" value=""$=Provider.Request.RedirectURL""/>
	    $
	    if (Provider.Session['loginError'])
	    {
		    echo('<p class=""text-danger"">' + Provider.Session['loginError'] + '</p>');
		    Provider.Session['loginError'] = null;
	    }
	    $
        <div class=""form-group"">
		    <label for=""Email"">Email</label>
		    <input class=""form-control"" type=""text"" name=""Email"" id=""Email""/>
        </div>
        <div class=""form-group"">
    	    <label for=""Passwd"">�ifre</label>
		    <input class=""form-control"" type=""password"" name=""Passwd""/>
        </div>
	    <a href=""$= Provider.Configuration.MembershipFormPage $"" class=""btn btn-link"">�yelik</a>
	    <a href=""$= Provider.Configuration.RememberPasswordFormPage $"" class=""btn btn-link"">�ifremi unuttum</a>
	    <a href=""$= Provider.Configuration.UserActivationPage $"" class=""btn btn-link"">Aktivasyon kodu</a>
	    <input class=""btn btn-primary"" type=""submit"" value=""Giri�""/>
    </fieldset>
    </form>
$
}
if (!Provider.User.IsAnonim() && !Provider.DesignMode)
{
$
	<h1>Ho�geldiniz $= Provider.User.FullName $</h1>
	<a href=""$= Provider.Configuration.MembershipProfilePage $"" class=""btn btn-link"">�yelik Bilgilerim</a>
	$
    if (Provider.ContextUser.IsInRole('Editor'))
		echo('<a href=""' + Provider.Configuration.AdminPage + '"" class=""btn btn-link"">Site Y�netimi</a>');
    if (Provider.ContextUser.IsInRole('Designer'))
		echo('<a href=""?DesignMode='+(Provider.DesignMode?'Off':'On')+'"" class=""btn btn-link"">'+(Provider.DesignMode?'�zleme Modu':'Tasar�m Modu')+'</a>');
	$
	<a href=""DoLogin.ashx?logout=1"" class=""btn btn-link"">Oturumu Kapat</a>
$
}
$";
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            return sb.ToString();
        }
    }
}
