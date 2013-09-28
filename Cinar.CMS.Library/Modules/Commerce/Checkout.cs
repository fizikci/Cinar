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
    [ModuleInfo(Grup = "Commerce")]
    public class Checkout : StaticHtml
    {
        public Checkout()
        {
            this.InnerHtml = @"<script>
function satinalma(){

    \$('#ccBtn').disable();
    \$('#ccLoad').css('visibility','visible');

    new Ajax.Request('/VirtualPOSBankAsya.ashx', {
        method: 'post',
        parameters: \$('#ccForm').serialize(),
        onComplete: function(req) {
            \$('#ccLoad').css('visibility','hidden');
            if(req.responseText.startsWith('HATA:')){
                \$('#ccErr').html(req.responseText);
                \$('#ccErr').css({background:'#FFdfdf', padding:'4px', margin:'4px'});
                \$('#ccBtn').enable();
                return;
            }
            \$('#theForm').html('Ödemeniz alınmıştır. Teşekkür ederiz.');
            \$('#fTitle').html('Tebrikler');
        },
        onException: function(req, ex){alert('İşlem yapılamadı');}
    });
}
</script>

<form id=""ccForm"">

<input type=""hidden"" name=""postBack"" value=""1""/>

<div class=""err"" id=""ccErr""></div>

<table><tr><td width=""170"" align=""center"">

&nbsp;<br/>
<b>Ödenecek Tutar:</b><br/>
$ using Cinar.CMS.Library.Handlers; $ 
<font size=""5"">$=VirtualPOS.Amount$ TL</font><br/>
<br/>
<br/>
<img src=""/external/icons/rapidssl.png"" width=""121""/>
</td><td id=""theForm"">

<table><tr>

<td>
<div class=""label"">Kart Tipi:</div>
<div class=""ctrl""><select name=""CardType"" onchange=""\$('#ccimg').attr('src','/external/icons/'+\$(this).val()+'.png');""><option value=""VISA"">VISA</option><option value=""MASTERCARD"">Master Card</option></select> <img src=""/external/icons/visa.png"" style=""vertical-align:middle"" id=""ccimg""/></div>
<div class=""label"">Kart No:</div>
<div class=""ctrl""><input type=""text"" name=""CardNumber"" style=""width:150px""/></div>
<div class=""label"">Son Kullanma Tarihi:</div>
<div class=""ctrl"">
<select name=""ExpiryMonth"">
	<option value=""0"">Ay</option>
	$
		for(var i=1; i<10; i++)
			echo('<option value=""0'+i+'"">0'+i+'</option>');
		for(i=10; i<13; i++)
			echo('<option value=""'+i+'"">'+i+'</option>');
	$
</select>
-
<select name=""ExpiryYear"">
	<option value=""0"">Yıl</option>
	$
		var d = DateTime.Now; 
		for(i=d.Year; i<d.Year+10; i++)
			echo('<option value=""'+i+'"">'+i+'</option>');
	$
</select>
</div>
<div class=""label"">CVV2:</div>
<div class=""ctrl""><input type=""text"" name=""CVV2""/></div>
</td>

<td>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </td>

<td>
<div class=""label"">Ad Soyad:</div>
<div class=""ctrl""><input id=""AdSoyad"" name=""AdSoyad""/></div>
<div class=""label"">Email:</div>
<div class=""ctrl""><input id=""Email"" name=""Email""/></div>
<div class=""label"">Telefon:</div>
<div class=""ctrl""><input id=""Telefon"" name=""Telefon""/></div>
<div class=""label"">Adres:</div>
<div class=""ctrl""><textarea id=""Adres"" name=""Adres""></textarea></div>
</td>

</tr>
<tr><td colspan=""3"" align=""right"">
<input type=""checkbox"" name=""kabul"" id=""kabul"" value=""1""/><label for=""kabul"">Satış sözleşmesini okudum.</label>
&nbsp; <input type=""button"" id=""ccBtn"" onclick=""satinalma();"" value=""Gönder""/>
&nbsp; <img id=""ccLoad"" src=""/external/icons/loading.gif"" style=""visibility:hidden""/>
</td></tr>
</table>
</td></tr></table>
</form>";

            this.TopHtml = "<h1>$=Provider.TR(\"Ödeme\")$</h1>";

        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            return sb.ToString();
        }
    }
}
