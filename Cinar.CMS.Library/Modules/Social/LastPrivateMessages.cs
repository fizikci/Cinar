using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Contacts;
using Google.GData.Apps;
using Google.GData.Client;
using Cinar.CMS.Library.Entities;
using Cinar.Scripting;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Social")]
    public class LastPrivateMessages : StaticHtml
    {
        public LastPrivateMessages()
        {
            this.InnerHtml = @"<script type=""text/javascript"">
jQuery(function(){
    jQuery.ajax({
        dataType: ""json"",
        url: 'Social.ashx',
        data : {method: ""getLastMessages""},
        success: function(res){
            if(res.IsError){
                alert(res.ErrorMessage);
                return;
            }
            res.Data.each(function(msg){
                console.log(msg.UpdateDate);
                var str = jQuery('#privateLastMessageTemplate').html().replace('#FullName#',msg.FullName);
                str = str.replace('#Avatar#', msg.Avatar);
                str = str.replace('#UpdateDate#', prettyDate(msg.UpdateDate));
                str = str.replace('#Summary#', parseMetin(msg.Summary));
                jQuery('#privateLastMessages').append('<div class=""privateLastMessage"">'+str+'</div>');
                console.log(msg);
                jQuery('#privateLastMessages .privateLastMessage:last').click(function(){
                    if(showMessages) showMessages(msg);
                });
            });
            jQuery('#privateLastMessages .privateLastMessage:first').click();
        }
    });
});
</script>

<div id=""privateLastMessageTemplate"" style=""display:none"">
    <img src=""#Avatar#""/>
    <span class=""name"">#FullName#</span>
    <span class=""date"">#UpdateDate#</span>
    <span class=""summary"">#Summary#</span>
</div>

<div id=""privateLastMessages"">
</div>";

            this.TopHtml = "<h1>$=Provider.TR(\"Mesajlaşma\")$</h1>";

        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} {{padding:10px;}}\n", this.Name, this.Id);
            sb.AppendFormat("#privateLastMessages .summary {{float: left;font-size: 12px;display: block;width: 191px;}}\n", this.Name, this.Id);
            sb.AppendFormat("#privateLastMessages .name {{font-weight: bold;}}\n", this.Name, this.Id);
            sb.AppendFormat("#privateLastMessages .date {{float: right;font-size: 9px;margin-top: 5px;}}\n", this.Name, this.Id);
            sb.AppendFormat("#privateLastMessages img {{width: 48px;height: 48px;float: left;margin: 5px 8px 5px 5px;}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} h1 {{height: 29px;}}\n", this.Name, this.Id);
            sb.AppendFormat(".privateLastMessage {{clear: both;height: 60px;border-top: 1px solid #C5C5C5;}}\n", this.Name, this.Id);
            sb.AppendFormat(".privateLastMessage {{background-color: #FFF;cursor: pointer;}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }
}
