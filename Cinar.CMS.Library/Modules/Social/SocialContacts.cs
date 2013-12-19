using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Scripting;
using Brickred.SocialAuth.NET.Core.BusinessObjects;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Social")]
    public class SocialContacts : StaticHtml
    {
        //public string ClientId { get; set; }
        //public string ClientSecret { get; set; }
        //public string ApplicationName  { get; set; }

        //private static string scopes = "https://www.google.com/m8/feeds/";

        public SocialContacts()
        {
            this.InnerHtml = @"<script>
    function addContacts(btn){
        var emails = jQuery('input[name=memberContact]').serialize();
        jQuery.ajax({
            type: ""POST"",
            url: ""DoCommand.ashx"",
            data: {method:'addContacts', emails: emails }
        })
        .done(function( msg ) {
            jQuery(btn).hide();
            jQuery('.memberContacts').html('<li>$=Provider.TR(""Seçtiğiniz kişiler eklendi."")$</li>');
        });
    }
    function inviteContacts(btn){
        var emails = jQuery('input[name=contact]').serialize();
        jQuery.ajax({
            type: ""POST"",
            url: ""DoCommand.ashx"",
            data: {method:'inviteContacts', emails: emails }
        })
        .done(function( msg ) {
            jQuery(btn).hide();
            jQuery('.contacts').hide('<li>$=Provider.TR(""Seçtiğiniz kişiler davet edildi."")$</li>');
        });
    }
</script>

$
    if(foundMemberContacts && foundMemberContacts.Count>0){
        echo('<ul class=""memberContacts"">');
        foreach(var contact in foundMemberContacts)
            echo('<li><input type=""checkbox"" name=""memberContact"" value=""'+contact.Email+'""/>'+contact.Name+' ('+contact.Email+')</li>');
        echo('</ul>');
        echo('<button onclick=""addContacts(this)"">'+Provider.TR('Seçilen kişileri takip et')+'</button>');
    }
    if(foundContacts && foundContacts.Count>0){
        echo('<ul class=""contacts"">');
        foreach(var contact in foundContacts)
            echo('<li><input type=""checkbox"" name=""contact"" value=""'+contact.Email+'""/>'+contact.Name+' ('+contact.Email+')</li>');
        echo('</ul>');
        echo('<button onclick=""inviteContacts(this)"">'+Provider.TR('Seçilen kişilere davetiye gönder')+'</button>');
    }
$
<div class=""services"" style=""$=SocialAuthUser.IsLoggedIn() ? '' : 'display:none'$"">
$=Provider.TR('Aşağıdaki servisleri kullanarak arkadaşlarınızı bulabilirsiniz')$<br/>
$
    using Brickred.SocialAuth.NET.Core;
    foreach (var provider in ProviderFactory.Providers)
    {
        var p = provider.ToString().Replace(""Brickred.SocialAuth.NET.Core.Wrappers."", """").Replace(""Wrapper"","""");
        echo('<img src=""/external/icons/social/'+p+'.png"" onclick=""location.href=\'socialAuthLogin.ashx?provider='+p+'\';""/>');
    }
$
</div>
";

            this.TopHtml = "<h1>$=Provider.TR(\"Arkadaşlarını Bul\")$</h1>";
        }

        internal override string show()
        {
            List<Contact> listMemberContacts = new List<Contact>();
            List<Contact> listContacts = new List<Contact>();

            if (!SocialAuthUser.IsLoggedIn())
            {
            }
            else
            {
                try
                {
                    var contacts = SocialAuthUser.GetCurrentUser().GetContacts(SocialAuthUser.CurrentProvider);
                    foreach (Contact c in contacts)
                    {
                        if (string.IsNullOrWhiteSpace(c.Email))
                            continue;
                        if (Provider.Database.GetInt("select Id from User where Email={0}", c.Email) > 0)
                            listMemberContacts.Add(c);
                        else
                            listContacts.Add(c);
                    }
                }
                catch (Exception e)
                {
                    Provider.Log("Error", "SocialAuth", e.Message);
                }
            }

            Interpreter engine = Provider.GetInterpreter(InnerHtml, this);
            engine.SetAttribute("foundMemberContacts", listMemberContacts);
            engine.SetAttribute("foundContacts", listContacts);
            engine.Parse();
            engine.Execute();

            return engine.Output;
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} .contacts, #{0}_{1} .memberContacts {{height: 200px;overflow-y: auto;}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} button {{margin-left:100px;}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    
    }

}
