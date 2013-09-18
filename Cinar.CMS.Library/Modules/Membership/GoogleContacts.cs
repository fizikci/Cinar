using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Contacts;
using Google.GData.Apps;
using Google.GData.Client;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Membership")]
    public class GoogleContacts : Module
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ApplicationName  { get; set; }

        private static string scopes = "https://www.google.com/m8/feeds/";

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();

            string redirectUrl = Provider.Request.Url.ToString();
            if (redirectUrl.Contains('?')) redirectUrl = redirectUrl.Substring(0, redirectUrl.IndexOf('?'));
            if (redirectUrl.Contains('/')) redirectUrl = redirectUrl.Substring(0, redirectUrl.LastIndexOf('/')) + "/" + this.Template;

            if (Provider.Request["code"]==null) {
                sb.AppendFormat("<button onclick=\"location.href='{0}'\">{1}</button>", this.Template + "?code=req", Provider.TR("Google Contacts"));
            }
            else if (Provider.Request["code"] == "req")
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
                            sb.AppendLine(c.Emails[0].Address + " (" + c.Name.FullName+")<br/>");
                }
                catch (AppsException a)
                {
                    sb.AppendLine("A Google Apps error occurred.<br/>");
                    sb.AppendLine("<br/>");
                    sb.AppendFormat("Error code: {0}<br/>", a.ErrorCode);
                    sb.AppendFormat("Invalid input: {0}<br/>", a.InvalidInput);
                    sb.AppendFormat("Reason: {0}<br/>", a.Reason);
                }
            }

            return sb.ToString();
        }
    }
}
