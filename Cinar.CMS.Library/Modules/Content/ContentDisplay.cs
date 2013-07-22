using System;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System.Collections;
using System.Text.RegularExpressions;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Content")]
    public class ContentDisplay : ModuleContainer, IRegionContainer
    {
        public ContentDisplay()
        {
            this.DateFormat = Provider.Configuration.DefaultDateFormat;
        }

        private static string defaultFieldOrder = "title,sourcelink,description,region,text,date,author,source,tags";
        private string fieldOrder = ContentDisplay.defaultFieldOrder;
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string FieldOrder
        {
            get { return fieldOrder; }
            set { fieldOrder = value; }
        }

        protected string dateFormat = "D";
        [EditFormFieldProps(Options = "noHTML:true")]
        public string DateFormat
        {
            get { return dateFormat; }
            set { dateFormat = value; }
        }

        private string filter = "";
        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text), EditFormFieldProps(ControlType = ControlType.FilterEdit, Options = "entityName:'Content'")]
        public string Filter
        {
            get { return filter; }
            set { filter = value; }
        }

        protected string tagTemplate = "";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string TagTemplate
        {
            get { return tagTemplate; }
            set { tagTemplate = value; }
        }

        private Content getContent()
        {
            if (this.filter == "")
                return Provider.Content; //***

            FilterParser filterForContent = new FilterParser(this.filter, "Content");

            string where = filterForContent.GetWhere();
            string sql = @"
                select distinct top 1
                    *
                from
                    Content
                where
                    Content.Visible=1" + (where != "" ? " AND " + where : "");

            IDatabaseEntity[] contents = Provider.Database.ReadList(typeof(Entities.Content), sql, filterForContent.GetParams());
            Provider.Translate(contents);

            return contents.Length > 0 ? (Entities.Content)contents[0] : null;
        }

        internal override string show()
        {
            StringBuilder sb = new StringBuilder();
            Entities.Content content = this.getContent();

            if (content == null)
            {
                if (Provider.DesignMode)
                    sb.Append(Provider.GetResource("There is no content to display. Either define filter or click a content link to access this page."));
                return sb.ToString(); //***
            }

            // eğer otomatik oluşturulmuş içerik ise ve detayları henüz okunmamışsa, okuyalım.
            if (content.ContentSourceId > 0 && String.IsNullOrEmpty(content.Metin))
                Provider.FetchAutoContentDetails(content);

            string[] fieldsArr = this.fieldOrder.Split(',');

            Hashtable fields = new Hashtable();
            fields["title"] = String.IsNullOrEmpty(content.Title) ? "" : String.Format("<div class=\"title\">{0}</div>", content.Title);
            fields["author"] = content.Author == null ? "" : String.Format("<div class=\"author\">{0}</div>", content.Author.Name);
            fields["source"] = content.Source == null ? "" : String.Format("<div class=\"source\">{0}</div>", content.Source.Name);
            fields["date"] = String.Format("<div class=\"date\">{0}</div>", content.PublishDate.ToString(this.dateFormat));
            fields["description"] = String.IsNullOrEmpty(content.Description) ? "" : String.Format("<div class=\"desc\">{0}</div>", content.Description);
            fields["text"] = String.IsNullOrEmpty(content.Metin) ? "" : String.Format("<div class=\"text\">{0}</div>", content.Metin);
            string sourceLinkText = String.IsNullOrEmpty(content.SourceLink) ? "" : (content.SourceLink.Length > 47 ? content.SourceLink.Substring(0, 40) + ".." + content.SourceLink.Substring(content.SourceLink.Length - 5) : content.SourceLink);
            fields["sourcelink"] = String.IsNullOrEmpty(content.SourceLink) ? "" : String.Format("<div class=\"sourceLink\"><span>{1}:</span> <a href=\"{0}\" target=\"_blank\">{2}</a></div>", content.SourceLink, Provider.GetResource("Source"), sourceLinkText);
            if (Array.IndexOf(fieldsArr, "tags") > -1)
                fields["tags"] = String.IsNullOrEmpty(content.Tags) ? "" : String.Format("<div class=\"tags\">{0}</div>", getTagsWithLink(content.Tags));
            fields["region"] = "";
            string conRegion = Provider.GetRegionInnerHtml(this.ChildModules);
            if (!String.IsNullOrEmpty(conRegion))
                fields["region"] = String.Format("<div id=\"conRegion{0}\" class=\"conRegion Region\">{1}</div>", this.Id, conRegion);

            foreach (string fieldName in fieldsArr)
                if (fields.ContainsKey(fieldName))
                    sb.Append(fields[fieldName]);

            return sb.ToString();
        }

        private string getTagsWithLink(string strTags)
        {
            Tag[] tags = (Tag[])Provider.Database.ReadList(typeof(Tag), "select Id, Name from Tag where Name in ('" + strTags.Replace(",", "','") + "') order by Name");
            Provider.Translate(tags);

            StringBuilder sb = new StringBuilder();
            foreach (Tag tag in tags)
                sb.AppendFormat("{0}<a href=\"{1}?tag={2}\">{3}</a>",
                    Environment.NewLine,
                    tagTemplate,
                    Provider.Server.UrlEncode(tag.Name),
                    tag.Name);
            return sb.ToString();
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            if (Regex.Match(this.fieldOrder, "[^\\w,\\,]").Success)
                throw new Exception(Provider.GetResource("FieldOrder is invalid. Please enter fields as {0}", ContentDisplay.defaultFieldOrder));
            foreach (string fieldName in this.fieldOrder.Split(','))
                if (ContentDisplay.defaultFieldOrder.IndexOf(fieldName) == -1)
                    throw new Exception(Provider.GetResource("{0} is not a valid field name. Please use one of the following: {1}", fieldName, ContentDisplay.defaultFieldOrder));
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} div.title {{font-weight:bold; font-size:20px;}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.desc {{background-color:#efefef; padding:4px; margin:4px; border:1px dashed black}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.conRegion {{float:right; width:200px}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.text {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.author {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.source {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.date {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.tags {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.sourceLink {{}}\n", this.Name, this.Id);
            return sb.ToString();
        }

        protected internal override void afterShow()
        {
            Content content = getContent();
            if (content != null && content.Id > 0 && !Provider.DesignMode)
                Provider.Database.ExecuteNonQuery("update Content set ViewCount=ViewCount+1 where Id={0}", content.Id);
        }
    }

}
