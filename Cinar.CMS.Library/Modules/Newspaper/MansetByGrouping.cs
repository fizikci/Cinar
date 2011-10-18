using System;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System.Collections;
using System.Text.RegularExpressions;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Newspaper")]
    public class MansetByGrouping : LastContents
    {
        public MansetByGrouping() 
        {
            this.Cols = 1;
            this.HowManyItems = 10;
            this.FieldOrder = "title,image,description";
            this.DisplayAsTable = false;
        }

        protected int spotLength = 50;
        public int SpotLength
        {
            get { return spotLength; }
            set { spotLength = value; }
        }

        protected bool withAnimation = false;
        public bool WithAnimation
        {
            get { return withAnimation; }
            set { withAnimation = value; }
        }

        private static string defaultMansetFieldOrder = "title,spot,image,author,date,description";
        private string mansetFieldOrder = MansetByGrouping.defaultMansetFieldOrder;
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string MansetFieldOrder
        {
            get { return mansetFieldOrder; }
            set { mansetFieldOrder = value; }
        }

        protected int pictureWidthM = 0;
        public int PictureWidthM
        {
            get { return pictureWidthM; }
            set { pictureWidthM = value; }
        }

        protected int pictureHeightM = 0;
        public int PictureHeightM
        {
            get { return pictureHeightM; }
            set { pictureHeightM = value; }
        }

        protected override string show()
        {
            StringBuilder sb = new StringBuilder();

            Entities.Content firstContent = null;
            if (this.contents != null && this.contents.Length > 0)
                firstContent = (Entities.Content)this.contents[0];

            if (firstContent == null)
                return "";

            Hashtable fields = new Hashtable();
            fields["title"] = String.Format("<div id=\"Manset_{0}_title\" class=\"mansetTitle\">{1}</div>\n", this.Id, Utility.StrCrop(firstContent.Title, this.titleLength));
            fields["spot"] = String.Format("<div id=\"Manset_{0}_title\" class=\"mansetSTitle\">{1}</div>\n", this.Id, Utility.StrCrop(firstContent.SpotTitle, this.titleLength));
            fields["image"] = Provider.GetThumbImgHTML(firstContent.Picture, pictureWidthM, pictureHeightM, null, "mansetImage", "id=\"Manset_" + this.Id + "_pic\"");
            fields["author"] = String.Format("<div id=\"Manset_{0}_auth\" class=\"mansetAuth\">{1}</div>\n", this.Id, firstContent["AuthorName"]);
            fields["date"] = String.Format("<div id=\"Manset_{0}_date\" class=\"mansetDate\">{1}</div>\n", this.Id, firstContent.PublishDate.ToString(this.dateFormat));
            fields["description"] = String.Format("<div id=\"Manset_{0}_desc\" class=\"mansetDesc\">{1}</div>\n", this.Id, Utility.StrCrop(firstContent.Description, this.descriptionLength));

            sb.AppendFormat("<table{0} width=\"100%\" border=\"0\">\n", this.withAnimation ? " onmouseover=\"showManset(event, " + this.Id + ")\"" : "");
            sb.Append("<tr><td align=\"center\" valign=\"middle\">\n");
            foreach (string fieldName in this.mansetFieldOrder.Split(','))
                if (fields.ContainsKey(fieldName))
                    sb.Append(fields[fieldName]);
            sb.Append("</td><td>\n");
            sb.Append(base.show());
            sb.Append("</td></tr>\n");
            sb.Append("</table>\n");

            return sb.ToString();
        }
        protected override string getImgHTML(string template, Content content, int row, int col)
        {
            return String.Format("<div style=\"display:none\" class=\"picUrl\">{0}</div>", Provider.GetThumbPath(content.Picture, pictureWidthM, pictureHeightM))
                + base.getImgHTML(template, content, row, col);
        }

        protected override string getTitleHTML(bool isFirstItem, string template, Content content, int row, int col)
        {
            if (this.mansetFieldOrder.Contains("title") && !this.ShowTitle)
                return String.Format("<div class=\"clTitle\" style=\"display:none\">{0}</div>", Utility.StrCrop(content.Title, this.titleLength));
            else
                return base.getTitleHTML(isFirstItem, template, content, row, col);
        }

        protected override string getSpotTitleHTML(bool isFirstItem, string template, Content content, int row, int col)
        {
            if (this.mansetFieldOrder.Contains("spot") && !this.ShowSpotTitle)
                return String.Format("<div class=\"clSTitle\" style=\"display:none\">{0}</div>", Utility.StrCrop(content.SpotTitle, this.titleLength));
            else
                return base.getSpotTitleHTML(isFirstItem, template, content, row, col);
        }

        protected override string getDescriptionHTML(bool isFirstItem, string template, Content content, int row, int col)
        {
            string desc = Utility.StrCrop(content.Description, this.descriptionLength);
            if (this.mansetFieldOrder.Contains("description") && !this.ShowDescription)
                return String.Format("<div class=\"clDesc\" style=\"display:none\">{0}</div>", desc);
            else
                return base.getDescriptionHTML(isFirstItem, template, content, row, col);
        }

        protected override string getAuthorHTML(string authorName, bool isFirstItem, string template, Content content, int row, int col)
        {
            if (this.mansetFieldOrder.Contains("author") && !this.ShowAuthor)
                return String.Format("<div class=\"clAuth\" style=\"display:none\">{0}</div>", authorName);
            else
                return base.getAuthorHTML(authorName, isFirstItem, template, content, row, col);
        }

        protected override string getPublishDateHTML(string publishDate, int row, int col)
        {
            if (this.mansetFieldOrder.Contains("date") && !this.ShowPublishDate)
                return String.Format("<div class=\"clDate\" style=\"display:none\">{0}</div>", publishDate);
            else
                return base.getPublishDateHTML(publishDate, row, col);
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} div.clTitle {{font-weight:bold}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.mansetTitle {{font-weight:bold}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} img.mansetImage {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.mansetDesc {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.mansetAuth {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.mansetDate {{}}\n", this.Name, this.Id);
            return sb.ToString();
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);
            if (!this.mansetFieldOrder.Contains("image"))
                throw new Exception(Provider.GetResource("\"image\" is essential for the Field Order of the Headline module."));

            if (Regex.Match(this.mansetFieldOrder, "[^\\w,\\,]").Success)
                throw new Exception(Provider.GetResource("FieldOrder is invalid. Please enter fields as {0}", MansetByGrouping.defaultMansetFieldOrder));
            foreach (string fieldName in this.mansetFieldOrder.Split(','))
                if (MansetByGrouping.defaultMansetFieldOrder.IndexOf(fieldName) == -1)
                    throw new Exception(Provider.GetResource("{0} is not a valid field name. Please use one of the following: {1}", fieldName, MansetByGrouping.defaultMansetFieldOrder));
        }

    }
}
