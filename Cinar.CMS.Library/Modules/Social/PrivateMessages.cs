using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Scripting;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Social")]
    public class PrivateMessages : StaticHtml
    {
        public PrivateMessages()
        {
            this.InnerHtml = @"<div id=""msgTemp"" class=""message hide"">
    <img src=""#src#""/>
    <div class=""fullname"">#fullname#</div>
    <div class=""date"">#date#</div>
    <br/>
    <div class=""text"">#text#</div>
    <div style=""clear:both""></div>
</div>
<div id=""messages"">
</div>
    <div id=""userInput"" class=""message hide"">
        <img id=""userAvatar"" style=""width:48px;height:48px;""/>
        <div class=""text"">
            <textarea name=""message""></textarea>
            <input type=""button"" onclick=""sendMessage(this)"" value='$=Provider.TR(""Gönder"")$'/>
        </div>
    </div>
<script>
function sendMessage(button){
    var msg = jQuery(button).parent().find(""[name=message]"").val();
    if(!msg){
        return;
    }
    var nick = jQuery(button).closest("".message"").attr(""usernick"");
    jQuery.ajax({
        url: ""Social.ashx"",
        data: {method:""sendMessage"", message: msg, toUserNick: nick},
        success: function(){
            appendMessageToBottom(userInfo.Avatar48, userInfo.FullName, msg, prettyDate())
            jQuery(button).parent().find(""[name=message]"").val("""");
        }
    });
}

function appendMessageToBottom(avatar, fullname, text, date){
    var temp = jQuery(""#msgTemp"").html();
    
    var appendMsg = temp.replace(""#src#"", avatar);
    appendMsg = appendMsg.replace(""#fullname#"", fullname);
    appendMsg = appendMsg.replace(""#text#"", text);
    appendMsg = appendMsg.replace(""#date#"", date);
    
    jQuery(""#messages"").append('<div class=""message"">' + appendMsg + '</div>')
    
    scrollToBottom(""#messages"", ""#messages > :last"");
}

function showMessages(lastMsg){
    jQuery.ajax({
        dataType: ""json"",
        url: 'Social.ashx',
        data : {method: ""getMessages"", UserId: lastMsg.UserId},
        success: function(result){
            if(result.IsError){
                alert(result.ErrorMessage);
                return;
            }
            jQuery(""#msgTemp"").addClass(""hide"");
            jQuery(""#messages"").empty();
            var temp = jQuery(""#msgTemp"").html();
            result.Data.each(function(message){
                if($=Provider.User.Id$ == message.InsertUserId){
                    var avatar = userInfo.Avatar48;
                    var fullname = userInfo.FullName;
                }else{
                    var fullname = lastMsg.FullName;
                    var avatar = lastMsg.Avatar;
                }
                var text = message.Message;
                var date = message.InsertDate;
                var nextMessage = temp.replace(""#src#"", avatar);
                nextMessage = nextMessage.replace(""#fullname#"", fullname);
                nextMessage = nextMessage.replace(""#text#"", text);
                nextMessage = nextMessage.replace(""#date#"", prettyDate(date));
                jQuery(""#messages"").prepend('<div class=""message"">' + nextMessage + '</div>');
            });
            jQuery(""#userInput"")
            .attr(""userNick"", lastMsg.Nick).removeClass(""hide"")
            .find(""#userAvatar"").attr(""src"",userInfo.Avatar48);
            scrollToBottom(""#messages"", ""#messages > :last"");
        }
    });
}    
</script>";

            this.TopHtml = "<h1>$=Provider.TR(\"Özel Mesajlar\")$</h1>";

        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} {{background-image: none;background-color: #FFF;border: 1px solid #E8E8E8;border-radius: 6px;overflow: hidden;}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} h1 {{padding: 16px;}}\n", this.Name, this.Id);
            sb.AppendFormat("#messages {{min-height: 100px;max-height: 360px;overflow-y: auto;}}\n", this.Name, this.Id);
            sb.AppendFormat(".message img {{width: 24px;height: 24px;float: left;margin: 6px 9px 0 0;}}\n", this.Name, this.Id);
            sb.AppendFormat(".message {{padding: 0 20px 10px;}}\n", this.Name, this.Id);
            sb.AppendFormat(".message > div {{display:inline-block;vertical-align: top;}}\n.message .fullname {{float: left;font-weight: bold;}}\n.message .date {{float: right;}}\n", this.Name, this.Id);
            sb.AppendFormat(".message .text {{float: left;}}\n#userInput .text {{float: right;margin-top: -40px;height: 100px;}}\n.text input[type=button] {{float: right;position: relative;top: -40px;right: -7px;}}\n", this.Name, this.Id);
            sb.AppendFormat(".text textarea {{resize: none; width: 595px; height: 80px;outline: none;}}\n#userInput img {{float: right;margin: 0 0 0 9px;position: relative;top: 10px;right: 6px;}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }
}
