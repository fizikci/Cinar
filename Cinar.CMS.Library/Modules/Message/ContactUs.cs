using System;
using System.Text;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Message")]
    public class ContactUs : Module
    {
        private bool sendMail = false;
        public bool SendMail
        {
            get { return sendMail; }
            set { sendMail = value; }
        }

        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            string thanksMessage = Provider.GetModuleResource("Your message has been sent. Thank you.");

            string errorMessage = "";
            if (Provider.Request["contactUsForm"] == this.Id.ToString())
            {
                Entities.ContactUs contactUs = new Entities.ContactUs();
                contactUs.Email = Provider.Request["email"];
                contactUs.Message = Provider.Request["message"];
                contactUs.Name = Provider.Request["name"];
                contactUs.Subject = Provider.Request["subject"];

                try
                {
                    contactUs.Save();
                    if(sendMail)
                        Provider.SendMail(contactUs.Email, contactUs.Name, Provider.Configuration.AuthEmail, Provider.Configuration.SiteName, Provider.GetResource("From visitor: ") + contactUs.Subject, contactUs.Message);
                    return thanksMessage;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message.Replace("\n", "<br/>\n");
                }
            }

            sb.Append("<form action=\"" + Provider.Request.RawUrl + "\" method=\"post\">");
            sb.AppendFormat("<input type=\"hidden\" name=\"contactUsForm\" value=\"{0}\"/>", this.Id);

            if (errorMessage != "")
                sb.AppendFormat("<div class=\"error\">{0}</div>", errorMessage);

            sb.AppendFormat("{0}<br/>", Provider.GetModuleResource("Your Name"));
            sb.AppendFormat("<input class=\"name\" type=\"text\" name=\"name\" value=\"{0}\"/><br/>", CMSUtility.HtmlEncode(Provider.Request["name"]));
            sb.AppendFormat("{0}<br/>", Provider.GetModuleResource("Your Email Address"));
            sb.AppendFormat("<input class=\"email\" type=\"text\" name=\"email\" value=\"{0}\"/><br/><br/>", CMSUtility.HtmlEncode(Provider.Request["email"]));

            sb.AppendFormat("{0}<br/>", Provider.GetModuleResource("Subject"));
            sb.AppendFormat(@"<select class=""subject"" name=""subject"" value=""{0}"">
                <option value="""">{1}</option>
                <option value=""{2}"">{2}</option>
                <option value=""{3}"">{3}</option>
                <option value=""{4}"">{4}</option>
                <option value=""{5}"">{5}</option></select><br/>", CMSUtility.HtmlEncode(Provider.Request["subject"]),
                                                                Provider.GetResource("Select"),
                                                                Provider.GetModuleResource("Thank"),
                                                                Provider.GetModuleResource("Complaint"),
                                                                Provider.GetModuleResource("Request"),
                                                                Provider.GetModuleResource("Recommendation"));
            sb.AppendFormat("{0}<br/>", Provider.GetModuleResource("Your Message"));
            sb.AppendFormat("<textarea class=\"message\" name=\"message\">{0}</textarea><br/><br/>", CMSUtility.HtmlEncode(Provider.Request["message"]));

            sb.AppendFormat("<input class=\"send\" type=\"submit\" value=\"{0}\"/>", Provider.GetResource("Send"));

            sb.Append("</form>");

            return sb.ToString();
        }

        protected override bool canBeCachedInternal()
        {
            return false;
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} div.error {{color:crimson;padding:4px;margin:4px;border:1px dashed #efefef}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input.name {{width:300px}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input.email {{width:300px}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} select.subject {{width:300px}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} textarea.message {{width:400px;height:300px}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} input.send {{}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }
}
