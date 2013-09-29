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
    public class WordBlackListAdmin : StaticHtml
    {
        public WordBlackListAdmin()
        {
            this.InnerHtml = @"<script>
function addToBlacklist(btn)
{
    var word = jQuery(btn).prev().val();
    var e = {Name:word};
    
    saveEntity('WordBlacklist', e, function(){
        jQuery("".blacklist"").append('<p class=""BadWord"">' + e.Name + ' <span class=""fff delete"" onclick=""deleteWord('+e.Id+', this)""></span></p>');
    });
}
function deleteWord(id, btn){
    deleteEntity('WordBlacklist', id, function(){\$(btn).parent().remove();}, false)
}
\$(function(){
    readEntityList(""WordBlacklist"",'',function(list){
        var blacklist = jQuery("".blacklist"");
        for(var i=0; i<list.length; i++)
            blacklist.append('<p class=""BadWord"">' + list[i].Name + ' <span class=""fff delete"" onclick=""deleteWord('+list[i].Id+', this)""></span></p>');
    });
})
</script>
<div class=""addNewBadword"">Yeni Kelime Ekle: <input type=""text""/><button onclick=""addToBlacklist(this)"">+</button></div>
<div class=""blacklist""></div><div style=""clear:both""></div>";

            this.TopHtml = "<h1>$=Provider.TR(\"Yasaklı Kelimeler\")$</h1>";

        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} .BadWord {{float:left;border-radius:3px;background-color:#F7D1D1; margin-left:6px;padding:0 4px;margin-top:10px;}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }
}
