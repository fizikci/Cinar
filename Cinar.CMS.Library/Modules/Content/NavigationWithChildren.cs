using System;
using System.Text;
using Cinar.Database;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Content")]
    public class NavigationWithChildren : LinkListWithBullets
    {
        protected int parentCategoryId = 1;
        [ColumnDetail(IsNotNull = true, DefaultValue = "1", References = typeof(Entities.Content))]
        [EditFormFieldProps(ControlType = ControlType.LookUp, Options="extraFilter:'ClassName=Category'")]
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

        private bool followRoot = false;
        public bool FollowRoot
        {
            get { return followRoot; }
            set { followRoot = value; }
        }

        private bool useSpotTitle = false;
        public bool UseSpotTitle
        {
            get { return useSpotTitle; }
            set { useSpotTitle = value; }
        }

        private int indent = 16;
        public int Indent
        {
            get { return indent; }
            set { indent = value; }
        }

        private bool listContents = true;
        public bool ListContents
        {
            get { return listContents; }
            set { listContents = value; }
        }

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();

            if (Provider.Content == null)
                return String.Empty; //***

            int rootId = this.parentCategoryId>1 ? this.parentCategoryId : (this.followRoot ? 1 : Provider.Content.FindMainCategoryId());

            this.writeSubMenus(rootId, true, sb);

            return sb.ToString();
        }

        private void writeSubMenus(int parentId, bool isRoot, StringBuilder sb)
        {
            if (Provider.Content.IsUnder(parentId) || Provider.Content.Id == parentId)
            {
                IDatabaseEntity[] cats = Provider.Database.ReadList(typeof(Entities.Content), "select Id, " + (useSpotTitle ? "SpotTitle as Title" : "Title") + ", ClassName, Hierarchy, ShowInPage from Content where CategoryId={0} and Visible=1" + (listContents ? "" : " and ClassName='Category'") + " order by OrderNo", parentId);
                Provider.Translate(cats);

                if (cats.Length == 0)
                    return;

                sb.AppendFormat("<div style=\"margin-left:{0}px\">", (isRoot ? 0 : 1) * this.indent);
                if (isRoot && this.showHomeLink)
                {
                    Entities.Content cRoot = new Entities.Content(); cRoot.Id = 1;
                    sb.Append(getLink(cRoot));
                    //addLink(sb, Provider.Configuration.MainPage, 1, "Ana Sayfa");
                }
                foreach (Entities.Content c in cats)
                {
                    string template = this.forceToUseTemplate ? this.useTemplate : Provider.GetTemplate(c, useTemplate);

                    sb.Append(getLink(c)); //addLink(sb, template, c.Id, c.Title);
                    writeSubMenus(c.Id, false, sb);
                }
                sb.Append("</div>");
            }
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0} a {{display:block;padding:4px;text-decoration:none;font-weight:bold}}\n", getCSSId());
            sb.AppendFormat("#{0} a:hover {{background:#efefef}}\n", getCSSId());
            return sb.ToString();
        }
    }
}
