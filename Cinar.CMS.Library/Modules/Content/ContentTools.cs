using System;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System.Collections;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Content")]
    public class ContentTools : Module
    {
        private static string defaultToolOrder = "comment,email,print,recommendation";
        private string toolOrder = ContentTools.defaultToolOrder;
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string ToolOrder
        {
            get { return toolOrder; }
            set { toolOrder = value; }
        }

        protected string commentIcon = "UserFiles/Image/buttons/yorum.gif";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string CommentIcon
        {
            get { return commentIcon; }
            set { commentIcon = value; }
        }

        protected string emailIcon = "UserFiles/Image/buttons/email.gif";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string EmailIcon
        {
            get { return emailIcon; }
            set { emailIcon = value; }
        }

        protected string printIcon = "UserFiles/Image/buttons/yazdir.gif";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string PrintIcon
        {
            get { return printIcon; }
            set { printIcon = value; }
        }

        protected string recommendIcon = "UserFiles/Image/buttons/tavsiye.gif";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string RecommendIcon
        {
            get { return recommendIcon; }
            set { recommendIcon = value; }
        }

        private bool showLabels;
        public bool ShowLabels
        {
            get { return showLabels; }
            set { showLabels = value; }
        }

        protected string printPage = "";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string PrintPage
        {
            get { return printPage; }
            set { printPage = value; }
        }

        private string recommendMessage = "Merhaba [name2],<br><br>Arkadaşınız [name1] size sitemizin aşağıda adresi verilen sayfasını tavsiye ediyor.<br><br>[link]<br><br>Saygılarımızla,<br>[SiteName]";
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps()]
        public string RecommendMessage
        {
            get { return recommendMessage; }
            set { recommendMessage = value; }
        }

        private string getToolHtml(string icon, string link) 
        {
            icon = icon == "" ? "" : ("<img src=\"" + icon + "\" align=\"absmiddle\" border=\"0\"/>");
            string res = (icon == "" ? "" : icon + " ") + (this.showLabels ? link : "");
            return res;
        }

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();

            string commentLink = Provider.GetModuleResource("Comment");
            string emailLink = Provider.GetModuleResource("Feedback");
            string printLink = Provider.GetModuleResource("Print");
            string recommendLink = Provider.GetModuleResource("Recommend");

            Entities.Content content = Provider.Content;

            bool commentsModuleExist = this.ContainerPage == null;
            Comments mdlComments = null;
            if (this.ContainerPage != null)
            {
                commentsModuleExist = this.ContainerPage.HasModule(typeof(Comments));
                if(commentsModuleExist)
                    mdlComments = (Comments)this.ContainerPage.GetModule(typeof(Comments));
            }

            if (String.IsNullOrEmpty(this.printPage))
                this.printPage = "Print.ashx";

            Hashtable tools = new Hashtable();
            tools["comment"] = (commentsModuleExist || Provider.DesignMode) ? String.Format("<a class=\"comment\" href=\"javascript:{0}\">{1}</a>", mdlComments == null ? "void(0)" : mdlComments.getWriteCommentJS(), getToolHtml(commentIcon, commentLink)) : "";
            tools["email"] = String.Format("<a class=\"email\" href=\"mailto:{0}?subject={1}\">{2}</a>", Provider.Configuration.AuthEmail, content == null ? "" : CMSUtility.HtmlEncode(content.Title), getToolHtml(emailIcon, emailLink));
            tools["print"] = content != null ? String.Format("<a class=\"print\" href=\"{0}?item={1}\" target=\"_blank\">{2}</a>", printPage, content.Id, getToolHtml(printIcon, printLink)) : "";
            tools["recommendation"] = String.Format("<a class=\"recommend\" href=\"javascript:recommend({0})\">{1}</a>", this.Id, getToolHtml(recommendIcon, recommendLink));

            foreach (string toolName in this.toolOrder.Split(','))
            {
                if (tools.ContainsKey(toolName))
                    sb.Append(tools[toolName]);
            }

            return sb.ToString();
        }

        [ExecutableByClient(true)]
        public string SendMail(string name1, string email1, string name2, string email2, string link)
        {
            if (String.IsNullOrEmpty(name1) || String.IsNullOrEmpty(email1) || String.IsNullOrEmpty(name2) || String.IsNullOrEmpty(email2))
                throw new Exception(Provider.GetResource("Please fill in all the fields."));

            Recommendation rec = (Recommendation)Provider.CreateEntity("Recommendation");
            rec.ContentId = Provider.Content != null ? Provider.Content.Id : 0;
            rec.EmailFrom = email1;
            rec.EmailTo = email2;
            rec.NameFrom = name1;
            rec.NameTo = name2;
            rec.Save();

            string message = "";
            message = this.recommendMessage.Replace("[name1]", name1);
            message = message.Replace("[name2]", name2);
            message = message.Replace("[link]", "<a href=\"" + link + "\">" + link + "</a>");
            message = message.Replace("[SiteName]", "<a href=\"http://" + Provider.Configuration.SiteAddress + "\">" + Provider.Configuration.SiteName + "</a>");

            string res = Provider.SendMail(email1, name1, email2, name2, Provider.GetModuleResource("Recommendation from [name]").Replace("[name]", name1), message);

            if (res=="")
                return Provider.GetModuleResource("Your message has been sent");
            else
                return Provider.GetModuleResource("Your message has been sent");//("Your message couldn't be sent")+" Reason: " + res;
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} a.comment {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} a.email {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} a.print {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} a.recommend {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.recommendForm {{background:white; margin:44px; margin-left:20px; padding:4px; border:1px dashed #dfdfdf;left:200px;top:200px;width:400px;}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.title {{background:#46A2F4; color:white; font-weight:bold;margin-bottom:20px;padding-top:10px;text-align:center;}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.recommendForm div.buttons {{text-align:center;}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div {{margin-bottom:15px;}}\n", this.Name, this.Id);
            return sb.ToString();
        }
    }

}
