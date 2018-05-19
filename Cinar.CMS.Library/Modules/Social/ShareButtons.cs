using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Social")]
    public class ShareButtons : StaticHtml
    {
        public ShareButtons()
        {
            this.InnerHtml = @"
        <a title=""google"" onclick=""window.open('https://plus.google.com/share?url='+encodeURIComponent(location.href),'', 'menubar=no,toolbar=no,resizable=yes,scrollbars=yes,height=600,width=600');return false;"" href=""javascript:;""><img  style=""width: 17px;vertical-align:sub"" src=""/external/icons/googleplus.png"" border=""0""/></a> &nbsp;
        <a title=""facebook"" onclick=""window.open('http://www.facebook.com/sharer.php?u='+encodeURIComponent(location.href), 'facebook','toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600'); return false;"" href=""javascript:;""><img style=""width: 16px;"" src=""/external/icons/facebook.png"" border=""0""/></a> &nbsp;
        <a title=""twitter"" onclick=""window.open('http://twitter.com/?status='+encodeURIComponent(document.title)+' '+encodeURIComponent(location.href), 'twitter','toolbar=yes,scrollbars=yes,resizable=yes,width=800,height=600'); return false;"" href=""javascript:;""><img style=""width: 21px;vertical-align: sub;"" src=""/external/icons/twitter.png"" border=""0""/></a>
";
        }
    }
}
