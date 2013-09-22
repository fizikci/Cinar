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
    [ModuleInfo(Grup = "Membership")]
    public class GoogleContacts : StaticHtml
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ApplicationName  { get; set; }

        private static string scopes = "https://www.google.com/m8/feeds/";

        public GoogleContacts()
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
            echo('<li><input type=""checkbox"" name=""memberContact"" value=""'+contact.Email+'""/>'+contact.FullName+' ('+contact.Email+')</li>');
        echo('</ul>');
        echo('<button onclick=""addContacts(this)"">'+Provider.TR('Seçilen kişileri adres defterime ekle')+'</button>');
    }
    if(foundContacts && foundContacts.Count>0){
        echo('<ul class=""contacts"">');
        foreach(var contact in foundContacts)
            echo('<li><input type=""checkbox"" name=""contact"" value=""'+contact.Email+'""/>'+contact.FullName+' ('+contact.Email+')</li>');
        echo('</ul>');
        echo('<button onclick=""inviteContacts(this)"">'+Provider.TR('Seçilen kişilere davetiye gönder')+'</button>');
    }
$

<button onclick=""location.href='$=this.Template$?code=reqGgl'"" style=""$=Provider.Request.code ? 'display:none':''$"">$=Provider.TR('Google Adres Defterimden Bul')$</button>";

            this.TopHtml = "<h1>$=Provider.TR(\"Arkadaşlarını Bul\")$</h1>";
        }

        internal override string show()
        {
            StringBuilder sb = new StringBuilder(InnerHtml);

            string redirectUrl = Provider.Request.Url.ToString();
            if (redirectUrl.Contains('?')) redirectUrl = redirectUrl.Substring(0, redirectUrl.IndexOf('?'));
            if (redirectUrl.Contains('/')) redirectUrl = redirectUrl.Substring(0, redirectUrl.LastIndexOf('/')) + "/" + this.Template;

            List<GoogleContact> listMemberContacts = new List<GoogleContact>();
            List<GoogleContact> listContacts = new List<GoogleContact>();

            if (Provider.Request["code"]==null) {
            }
            else if (Provider.Request["code"] == "reqGgl")
            {

                OAuth2Parameters parameters = new OAuth2Parameters()
                {
                    ClientId = ClientId,
                    ClientSecret = ClientSecret,
                    RedirectUri = redirectUrl,
                    Scope = scopes
                };

                string url = OAuthUtil.CreateOAuth2AuthorizationUrl(parameters);
                Provider.Response.Redirect(url);

                Provider.Response.End();
            }
            else 
            {
                OAuth2Parameters parameters = new OAuth2Parameters()
                {
                    ClientId = ClientId,
                    ClientSecret = ClientSecret,
                    RedirectUri = redirectUrl,
                    Scope = scopes,
                    AccessCode = Provider.Request["code"]
                };

                OAuthUtil.GetAccessToken(parameters);

                try
                {
                    RequestSettings settings = new RequestSettings(ApplicationName, parameters);
                    settings.AutoPaging = true;
                    ContactsRequest cr = new ContactsRequest(settings);

                    Feed<Contact> f = cr.GetContacts();
                    foreach (Contact c in f.Entries)
                        if (c != null && c.Emails != null && c.Emails.Count > 0 && c.Emails[0] != null)
                        {
                            try
                            {
                                string email = c.Emails[0].Address;
                                if(Provider.Database.GetInt("select Id from User where Email={0}", email)>0)
                                    listMemberContacts.Add(new GoogleContact { Email = email, FullName = c.Name.FullName });
                                else
                                    listContacts.Add(new GoogleContact { Email = email, FullName = c.Name.FullName });
                            }
                            catch { }

                            
                        }
                }
                catch (AppsException a)
                {
                    Provider.Log("Error", "Google Contact API", string.Format("A Google Apps error occurred.<br/><br/>Error code: {0}<br/>Invalid input: {1}<br/>Reason: {2}<br/>", a.ErrorCode, a.InvalidInput, a.Reason));
                }
            }

            Interpreter engine = Provider.GetInterpreter(sb.ToString(), this);
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

    public class GoogleContact {
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
