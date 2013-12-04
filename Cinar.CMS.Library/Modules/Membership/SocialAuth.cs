using System;
using System.Text;
using Brickred.SocialAuth.NET.Core;
using Cinar.CMS.Library.Entities;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Membership")]
    public class SocialAuth : StaticHtml
    {
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string Redirect { get; set; }

        public SocialAuth()
        {
            InnerHtml = @"
$
    using Brickred.SocialAuth.NET.Core;
    foreach (var provider in ProviderFactory.Providers)
    {
        var p = provider.ToString().Replace(""Brickred.SocialAuth.NET.Core.Wrappers."", """").Replace(""Wrapper"","""");
        echo('<img src=""/external/icons/social/'+p+'.png"" onclick=""location.href=\'socialAuthLogin.ashx?provider='+p+'\';""/>');
    }
$
<script>
function loginWith(provider){
    location.href='socialAuthLogin.ashx?provider='+provider;
    /*
    jQuery.ajax({
        dataType: ""json"",
        url: 'socialAuthLogin.ashx',
        data: {provider:provider},
        success: function(res){
            alert(res);        
        }
    });
    */
}
</script>";
        }

        internal override string show()
        {
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
            return sb.ToString();
        }
    }
}
