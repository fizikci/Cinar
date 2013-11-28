using System;
using System.Text;
using Cinar.CMS.Library.Entities;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Misc")]
    public class GotoTopButton : StaticHtml
    {
        public GotoTopButton()
        {
            this.InnerHtml = @"
<img class=""goToTop"" style=""display:none;position: fixed;bottom: 20px;right: 20px;"" onclick=""jQuery('html, body').animate({scrollTop: '0px'}, 200);"" src=""/external/icons/go_top_icon.png""/>
<script>
    jQuery(window).scroll(function() {
        if(jQuery(window).scrollTop()>500)
            jQuery('.goToTop').show();
        else
            jQuery('.goToTop').hide();
    });
</script>
";
        }

        protected override bool canBeCachedInternal()
        {
            return true;
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            return sb.ToString();
        }
    }
}
