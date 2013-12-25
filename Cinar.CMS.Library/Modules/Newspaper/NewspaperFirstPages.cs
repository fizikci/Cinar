using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Newspaper")]
    public class NewspaperFirstPages : StaticHtml
    {

        public NewspaperFirstPages()
        {
            this.InnerHtml = @"
<script src=""/external/javascripts/lightbox/js/lightbox-2.6.min.js""></script>
<link href=""/external/javascripts/lightbox/css/lightbox.css"" rel=""stylesheet"">
$
var gazeteler = 'aksam,anayurt,ankarabelde,ankarasonsoz,aydinlik,birgun,bugun,cumhuriyet,dunya,evrensel,fanatik,gunboyu,gunes,haberturk,hurriyet,hurriyetdailynews,hurses,milat,milligazete,milliyet,ortadogu,ozgurgundem,posta,radikal,sabah,sol,sozcu,star,takvim,taraf,todayszaman,turkiye,vatan,yeniakit,yeniasya,yenicag,yenimesaj,yenisafak,yurt,zaman';
foreach(var name in gazeteler.Split(','))
	echo('<a href=""http://www.bik.gov.tr/gazeteler/dosya/'+name+'.jpg"" data-lightbox=""roadtrip$=this.Id$""><img src=""http://www.bik.gov.tr/gazeteler/dosya/'+name+'_thumb.jpg"" width=""90"" height=""146""/></a>');
$
";
            this.CSSClass = "slideShow autoPlay";
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat(".slideShow {{width:967px;margin-left:0px;margin-top:0px;padding:20px 0px;}}\n", this.Name, this.Id);
            sb.AppendFormat(".slideShow .clipper {{width:920px;height:146px;}}\n", this.Name, this.Id);
            sb.AppendFormat(".slideShow .clipper .innerDiv {{height:146px;}}\n", this.Name, this.Id);
            sb.AppendFormat(".slideShow .clipper .innerDiv img {{margin-right:4px;}}\n", this.Name, this.Id);
            sb.AppendFormat(".slideShow div.paging {{height:146px;width:20px;}}\n", this.Name, this.Id);
            sb.AppendFormat(".slideShow .playBtn {{display:none;}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }
}
