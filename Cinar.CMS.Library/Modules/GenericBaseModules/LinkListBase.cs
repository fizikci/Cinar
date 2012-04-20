using System;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    public abstract class LinkList : Module
    {
        protected string useTemplate = "";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string UseTemplate
        {
            get { return useTemplate; }
            set { useTemplate = value; }
        }

        protected bool forceToUseTemplate = false;
        public bool ForceToUseTemplate
        {
            get { return forceToUseTemplate; }
            set { forceToUseTemplate = value; }
        }

        protected string linkTarget = "";
        [EditFormFieldProps(Options = "noHTML:true")]
        public string LinkTarget
        {
            get { return linkTarget; }
            set { linkTarget = value; }
        }

        protected bool showCurrentContent = false;
        public bool ShowCurrentContent
        {
            get { return showCurrentContent; }
            set { showCurrentContent = value; }
        }

        protected virtual string getLink(Content content)
        {
            if (!ShowCurrentContent && content.Id == Provider.Content.Id)
                return "";

            string icon = getBulletIcon(), template = "";

            if (content.Id == 1)
            {
                template = Provider.Configuration.MainPage; //"Default.aspx";
                content.Title = Provider.GetModuleResource("Home Page");
            }
            else
                template = this.forceToUseTemplate ? this.useTemplate : Provider.GetTemplate(content, useTemplate);

            string linkTargetAttribute = linkTarget != "" ? " target=\"" + Provider.Server.HtmlEncode(linkTarget) + "\"" : "";

            return String.Format("{0}<a href=\"{1}\"{2}{3}>{4}</a>",
                icon,
                Provider.GetPageUrl(template, content.Id, content.Category.Title, content.Title),
                linkTargetAttribute,
                content.Id == Provider.Content.Id ? " class=\"sel\"" : "",
                content.Title);
        }


        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} a.sel {{color:orange}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} a.sel:hover {{}}\n", this.Name, this.Id);
            return sb.ToString();
        }

        protected virtual string getBulletIcon() 
        {
            return "";
        }
    }

    public abstract class LinkListWithBullets : LinkList
    {
        protected string bulletIcon = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string BulletIcon
        {
            get { return bulletIcon; }
            set { bulletIcon = value; }
        }

        protected override string getBulletIcon()
        {
            if (this.bulletIcon != "")
                return "<img src=\"" + this.bulletIcon + "\" align=\"absmiddle\" border=\"0\"/> ";

            return "";
        }
    }
}
