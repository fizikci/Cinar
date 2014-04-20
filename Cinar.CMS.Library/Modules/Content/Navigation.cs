using System;
using System.Text;
using Cinar.Database;
using System.Collections;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Content")]
    public class Navigation : LinkListWithBullets
    {
        protected int parentCategoryId = 1;
        [ColumnDetail(IsNotNull = true, DefaultValue = "1", References = typeof(Entities.Content))]
        [EditFormFieldProps(ControlType = ControlType.LookUp, Options = "extraFilter:'ClassName=Category'")]
        public int ParentCategoryId
        {
            get { return parentCategoryId; }
            set { parentCategoryId = value; }
        }

        protected bool showHomeLink;
        public bool ShowHomeLink
        {
            get { return showHomeLink; }
            set { showHomeLink = value; }
        }

        private bool horizontal;
        [EditFormFieldProps(Options = "items:_HORIZONTAL_")]
        public bool Horizontal
        {
            get { return horizontal; }
            set { horizontal = value; }
        }

        private bool useSpotTitle = false;
        public bool UseSpotTitle
        {
            get { return useSpotTitle; }
            set { useSpotTitle = value; }
        }

        private bool showChildCategories = false;
        public bool ShowChildCategories
        {
            get { return showChildCategories; }
            set { showChildCategories = value; }
        }

        private bool dynamic;
        public bool Dynamic
        {
            get { return dynamic; }
            set { dynamic = value; }
        }

        private int popupDepth = 0;
        public int PopupDepth
        {
            get { return popupDepth; }
            set { popupDepth = value; }
        }

        protected string bulletIcon2 = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string BulletIcon2
        {
            get { return bulletIcon2; }
            set { bulletIcon2 = value; }
        }

        private int selectedCatId;
        internal override string show()
        {
            selectedCatId = this.ParentCategoryId;
            StringBuilder sb = new StringBuilder();

            Int32.TryParse(Provider.Request["item"], out selectedCatId);
            if (selectedCatId == 0) selectedCatId = 1;

            Entities.Content selectedCat = (Entities.Content)Provider.Database.Read(typeof(Entities.Content), selectedCatId);

            if (this.showHomeLink)
                addLink(sb, Provider.Configuration.MainPage, "nav", selectedCatId == 1, 1, "", Provider.GetModuleResource("Home Page"));

            IDatabaseEntity[] cats = Provider.Database.ReadList(typeof(Entities.Content), "select Id, " + (useSpotTitle ? "SpotTitle as Title" : "Title") + ", ClassName, Hierarchy, ShowInPage from Content where CategoryId=" + (this.Dynamic ? selectedCatId : this.ParentCategoryId) + " and Visible=1 order by OrderNo");
            if (cats.Length == 0 && this.Dynamic)
                cats = Provider.Database.ReadList(typeof(Entities.Content), "select Id, " + (useSpotTitle ? "SpotTitle as Title" : "Title") + ", ClassName, Hierarchy, ShowInPage from Content where CategoryId=" + selectedCat.CategoryId + " and Visible=1 order by OrderNo");
            Provider.Translate(cats);

            foreach (Entities.Content dr in cats)
            {
                //string template = Provider.GetTemplate(dr, useTemplate);
                string template = this.forceToUseTemplate ? this.useTemplate : Provider.GetTemplate(dr, useTemplate);

                bool selected = selectedCat.IsUnder(dr.Id) || dr.Id == selectedCatId;

                addLink(sb, template, "nav", selected, dr.Id, dr.Category.Title, dr.Title);
                if (this.showChildCategories && (selectedCat.Id==dr.Id || selectedCat.Hierarchy.IndexOf(dr.Id.ToString().PadLeft(5, '0')) > -1))
                {
                    IDatabaseEntity[] subCats = Provider.Database.ReadList(typeof(Entities.Content), "select Id, " + (useSpotTitle ? "SpotTitle as Title" : "Title") + ", ClassName, Hierarchy, ShowInPage from Content where CategoryId=" + dr.Id + " and Visible=1 order by OrderNo");
                    Provider.Translate(subCats);

                    foreach (Entities.Content drSub in subCats)
                    {
                        //template = Provider.GetTemplate(drSub, useTemplate);
                        template = this.forceToUseTemplate ? this.useTemplate : Provider.GetTemplate(drSub, useTemplate);
                        selected = selectedCat.IsUnder(drSub.Id) || drSub.Id == selectedCatId;
                        addLink(sb, template, "subNav", selected, drSub.Id, drSub.Category.Title, drSub.Title);
                    }
                }
            }
            if (popupDepth > 0)
            {
                ArrayList alCats = new ArrayList();
                foreach (Entities.Content cat in cats) alCats.Add(cat.Id.ToString());
                IDatabaseEntity[] childItems = Provider.Database.ReadList(typeof(Entities.Content), "select Id, " + (useSpotTitle ? "SpotTitle as Title" : "Title") + ", ClassName, Hierarchy, ShowInPage, CategoryId from Content where CategoryId in (" + string.Join(",",(string[])alCats.ToArray(typeof(string))) + ") and Visible=1 order by OrderNo");
                sb.Append("<div  style=\"display:none;position:absolute\" class=\"popupMenuItems hideOnOut\">\n");
                foreach (Entities.Content childContent in childItems)
                {
                    string template = this.forceToUseTemplate ? this.useTemplate : Provider.GetTemplate(childContent, useTemplate);
                    addPopupLink(sb, template, "popupMenuItem", childContent.Id, childContent.Title, childContent.CategoryId, childContent.Category.Title);
                }
                sb.Append("</div>\n");
                sb.AppendFormat("<script type=\"text/javascript\">navigationPopupInit('Navigation_{0}',{1});</script>\n", this.Id, this.Horizontal?"true":"false");
            }

            sb.Append("<div style=\"clear:both\"></div>\n");

            return sb.ToString();
        }
        private void addLink(StringBuilder sb, string template, string aClass, bool selected, int id, string categoryTitle, string title)
        {
            string icon = "";
            if (this.bulletIcon != "")
            {
                if (this.bulletIcon2 != "")
                    icon = aClass == "nav" ? this.bulletIcon : this.bulletIcon2;
                else
                    icon = this.bulletIcon;
            }
            if (icon != "")
                icon = "<img src=\"" + icon + "\" align=\"absmiddle\" border=\"0\"/> ";
            sb.AppendFormat("<div class=\"" + aClass + (selected ? "Sel" : "") + "\"" + (popupDepth > 0 ? " conId=\"{2}\"" : "") + " style=\"{0}\"><a href=\"{1}\">{3}{4}</a></div>\n",
                this.horizontal ? "float:left" : "display:block",
                Provider.GetPageUrl(template, id, categoryTitle, title),
                id,
                icon,
                title);
        }

        private void addPopupLink(StringBuilder sb, string template, string aClass, int id, string title, int categoryId, string categoryTitle)
        {
            sb.AppendFormat("<div class=\"" + aClass + "\" catId=\"{0}\"><a href=\"{1}\" style=\"width:100%\">{2}</a></div>\n", 
                categoryId,
                Provider.GetPageUrl(template, id, categoryTitle, title),
                title);
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0} div.nav a {{display:block;padding:4px;text-decoration:none;font-weight:bold}}\n", getCSSId());
            sb.AppendFormat("#{0} div.nav a:hover {{background:#efefef}}\n", getCSSId());
            sb.AppendFormat("#{0} div.navSel a {{display:block;padding:4px;text-decoration:none;font-weight:bold;background:orange;color:white}}\n", getCSSId());
            sb.AppendFormat("#{0} div.subNav a {{display:block;padding:2px;padding-left:20px;text-decoration:none}}\n", getCSSId());
            sb.AppendFormat("#{0} div.subNav a:hover {{background:#efefef}}\n", getCSSId());
            sb.AppendFormat("#{0} div.subNavSel a {{display:block;padding:2px;padding-left:20px;text-decoration:none;font-weight:bold;background:orange;color:white}}\n", getCSSId());
            sb.AppendFormat("#{0} div.popupMenuItems {{background:#808080;width:200px}}\n", getCSSId());
            sb.AppendFormat("#{0} div.popupMenuItems div.popupMenuItem a {{display:block;width:100%;padding:4px;text-decoration:none;color:white;}}\n", getCSSId());
            sb.AppendFormat("#{0} div.popupMenuItems div.popupMenuItem a:hover {{background:crimson}}\n", getCSSId());
            return sb.ToString();
        }
    }
}
