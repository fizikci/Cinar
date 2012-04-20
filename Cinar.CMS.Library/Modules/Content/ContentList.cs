using System;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System.Collections;
using System.Text.RegularExpressions;

namespace Cinar.CMS.Library.Modules
{
    public abstract class ContentList : TableView
    {
        public ContentList()
        {
            this.DateFormat = Provider.Configuration.DefaultDateFormat;
        }

        protected int howManyItems = 10;
        public int HowManyItems
        {
            get { return howManyItems; }
            set { howManyItems = value; }
        }

        protected int skipFirst = 0;
        public int SkipFirst
        {
            get { return skipFirst; }
            set { skipFirst = value; }
        }

        protected int random = 0;
        public int Random
        {
            get { return random; }
            set { random = value; }
        }

        protected string orderBy = "Content.PublishDate";
        [ColumnDetail(IsNotNull = true, Length = 30), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:_ORDERCONTENTSBY_")]
        public string OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }

        protected bool ascending = false;
        [EditFormFieldProps(Options="items:_ASCENDING_")]
        public bool Ascending
        {
            get { return ascending; }
            set { ascending = value; }
        }

        protected bool ShowTitle
        {
            get { return this.fieldOrder.Contains("title"); }
        }
        protected bool ShowSpotTitle
        {
            get { return this.fieldOrder.Contains("spot"); }
        }
        protected bool ShowCategory
        {
            get { return this.fieldOrder.Contains("category"); }
        }
        protected bool ShowDescription
        {
            get { return this.fieldOrder.Contains("description") || this.fieldOrder.Contains("cat_description"); }
        }
        protected bool ShowMetin
        {
            get { return this.fieldOrder.Contains("text"); }
        }
        protected bool ShowPicture
        {
            get { return this.fieldOrder.Contains("image") || this.fieldOrder.Contains("picture2"); }
        }
        protected bool ShowAuthor
        {
            get { return this.fieldOrder.Contains("author"); }
        }
        protected bool ShowSource
        {
            get { return this.fieldOrder.Contains("source"); }
        }
        protected bool ShowPublishDate
        {
            get { return this.fieldOrder.Contains("date"); }
        }

        protected bool showPictureLeftRight = false;
        public bool ShowPictureLeftRight
        {
            get { return showPictureLeftRight; }
            set { showPictureLeftRight = value; }
        }

        protected bool showCurrentContent = false;
        public bool ShowCurrentContent
        {
            get { return showCurrentContent; }
            set { showCurrentContent = value; }
        }

        protected bool showFirstItemWithPicture = false;
        public bool ShowFirstItemWithPicture
        {
            get { return showFirstItemWithPicture; }
            set { showFirstItemWithPicture = value; }
        }
        
        protected string whichPicture = "Content.Picture";
        [ColumnDetail(Length = 30), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:_WHICHPICTURE_")]
        public string WhichPicture
        {
            get { return whichPicture; }
            set { whichPicture = value; }
        }

        protected int pictureWidth = 0;
        public int PictureWidth
        {
            get { return pictureWidth; }
            set { pictureWidth = value; }
        }

        protected int pictureHeight = 0;
        public int PictureHeight
        {
            get { return pictureHeight; }
            set { pictureHeight = value; }
        }

        public bool CropPicture { get; set; }

        protected string bulletIcon = "";
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        public string BulletIcon
        {
            get { return bulletIcon; }
            set { bulletIcon = value; }
        }

        protected int titleLength = 50;
        public int TitleLength
        {
            get { return titleLength; }
            set { titleLength = value; }
        }

        protected int descriptionLength = 150;
        public int DescriptionLength
        {
            get { return descriptionLength; }
            set { descriptionLength = value; }
        }

        protected string dateFormat = "D";
        [EditFormFieldProps(Options = "noHTML:true")]
        public string DateFormat
        {
            get { return dateFormat; }
            set { dateFormat = value; }
        }

        private static string defaultFieldOrder = "title,spot,category,image,author,date,description,source"; //text de var ama default olarak koymuyoruz, beforeSave'de text'i de dikkate alıyoruz.
        private string fieldOrder = ContentList.defaultFieldOrder;
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string FieldOrder
        {
            get { return fieldOrder; }
            set { fieldOrder = value; }
        }

        protected bool createLink = true;
        public bool CreateLink
        {
            get { return createLink; }
            set { createLink = value; }
        }

        protected string linkTarget = "";
        [EditFormFieldProps(Options = "noHTML:true")]
        public string LinkTarget
        {
            get { return linkTarget; }
            set { linkTarget = value; }
        }

        protected bool showDescriptionAsLink = false;
        public bool ShowDescriptionAsLink
        {
            get { return showDescriptionAsLink; }
            set { showDescriptionAsLink = value; }
        }

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

        [EditFormFieldProps(Options = "noHTML:true")]
        public string MoreLink { get; set; }

        protected override int rowCount
        {
            get { return (int)Math.Ceiling((double)this.contents.Length / (double)this.cols); }
        }

        private IDatabaseEntity[] _contents;
        protected IDatabaseEntity[] contents
        {
            get
            {
                if (_contents == null)
                {
                    _contents = this.GetContentList();
                    if (this.skipFirst > 0)
                    {
                        IDatabaseEntity[] skippedContents = new IDatabaseEntity[_contents.Length - this.skipFirst];
                        Array.Copy(_contents, this.skipFirst, skippedContents, 0, skippedContents.Length);
                        _contents = skippedContents;
                    }
                    if (this.random > 0 && _contents.Length > this.random)
                    {
                        Random rnd = new Random();
                        int nextId = rnd.Next(_contents.Length);
                        ArrayList randomContents = new ArrayList();
                        while (randomContents.Count < this.random)
                        {
                            if (!randomContents.Contains(_contents[nextId]))
                                randomContents.Add(_contents[nextId]);
                            nextId = rnd.Next(_contents.Length);
                        }
                        _contents = (IDatabaseEntity[])randomContents.ToArray(typeof(Entities.Content));
                    }
                    Provider.Translate(_contents);
                }
                return _contents;
            }
        }

        protected override string getCellHTML(int row, int col)
        {
            StringBuilder sb = new StringBuilder();

            int index = row * this.cols + col;
            if (this.contents.Length <= index)
                return String.Empty;

            Entities.Content content = (Entities.Content)this.contents[index];

            bool isFirstItem = row+col==0;
            string template = this.forceToUseTemplate ? this.useTemplate : Provider.GetTemplate(content, useTemplate);

            Hashtable fields = new Hashtable();
            fields["image"] = this.getImgHTML(template, content, row, col);
            fields["picture2"] = string.IsNullOrEmpty(content.Picture2) ? fields["image"] : this.getImg2HTML(template, content, row, col);
            fields["title"] = this.getTitleHTML(isFirstItem, template, content, row, col);
            fields["spot"] = this.getSpotTitleHTML(isFirstItem, template, content, row, col);
            fields["category"] = content["CategoryName"] == null ? "" : this.getCategoryHTML(content["CategoryName"].ToString(), isFirstItem, template, content, row, col);
            fields["author"] = content["AuthorName"] == null ? "" : this.getAuthorHTML(content["AuthorName"].ToString(), isFirstItem, template, content, row, col);
            fields["source"] = content["SourceName"] == null ? "" : this.getSourceHTML(content["SourceName"].ToString(), isFirstItem, template, content, row, col);
            fields["date"] = this.getPublishDateHTML(this.dateFormat.ToLower() == "ago" ? content.PublishDate.ToAgoString() : content.PublishDate.ToString(this.dateFormat), row, col);
            fields["description"] = this.getDescriptionHTML(isFirstItem, template, content, row, col);
            fields["cat_description"] = this.getCatDescriptionHTML(isFirstItem, template, content, row, col);
            fields["text"] = this.getMetinHTML(content.Metin, row, col);

            sb.Append("<div class=\"clItem\">");

            bool firstItemWithPicture = isFirstItem && this.showFirstItemWithPicture;
            if (firstItemWithPicture)
            {
                foreach (string fieldName in "title,image,description".Split(','))
                {
                    if (fields.ContainsKey(fieldName))
                        sb.Append(fields[fieldName]);
                }
            }
            else
            {
                foreach (string fieldName in this.fieldOrder.Split(','))
                    if (fields.ContainsKey(fieldName))
                    {
                        sb.Append(fields[fieldName]);
                        fields.Remove(fieldName);
                    }
                // bunu manset icin yazmak zorunda kaldık! :(
                //if(!this.showFirstItemWithPicture)
                //    foreach (string fieldName in fields.Keys)
                //        sb.Append(fields[fieldName]);
            }

            if (!string.IsNullOrEmpty(MoreLink))
                sb.AppendFormat(@"<div class=""clMore"">{0}</div>", getLinkHTML(MoreLink, template, content));

            sb.Append("</div>");

            return sb.ToString();
        }

        private string getLinkAndIconHTML(string linkText, bool isFirstItem, string template, Content content)
        {
            string icon = (this.bulletIcon == "" ? "" : (isFirstItem && this.showFirstItemWithPicture ? "" : ("<img src=\"" + this.bulletIcon + "\" align=\"absmiddle\"/> ")));
            if (this.createLink)
                return String.Format("{0}{1}", icon, getLinkHTML(linkText, template, content));
            else
                return String.Format("{0}{1}", icon, linkText);
        }

        private string getLinkHTML(string linkText, string template, Content content)
        {
            string linkTargetAttribute = linkTarget != "" ? " target=\"" + Provider.Server.HtmlEncode(linkTarget) + "\"" : "";
            return string.Format("<a href=\"{0}\"{1}>{2}</a>", Provider.GetPageUrl(template, content.Id, content.Category.Title, content.Title), linkTargetAttribute, linkText);
        }

        protected virtual string getImgHTML(string template, Content content, int row, int col)
        {
            if (this.ShowPicture || (this.showFirstItemWithPicture && row + col == 0))
            {
                string imgStyle = showPictureLeftRight ? " style=\"float:" + (row % 2 == 0 ? "left" : "right") + "\"" : "";
                string imgHtml = Provider.GetThumbImgHTML(content.Picture, this.pictureWidth, this.pictureHeight, content.Title, "pic", imgStyle, CropPicture);

                if (this.createLink)
                    return String.Format("<a href=\"{0}\">{1}</a>", Provider.GetPageUrl(template, content.Id, content.Category.Title, content.Title), imgHtml);
                else
                    return String.Format("{0}", imgHtml);

            }
            return String.Empty;
        }

        protected virtual string getImg2HTML(string template, Content content, int row, int col)
        {
            if (this.ShowPicture || (this.showFirstItemWithPicture && row + col == 0))
            {
                string imgStyle = showPictureLeftRight ? " style=\"float:" + (row % 2 == 0 ? "left" : "right") + "\"" : "";
                string imgHtml = Provider.GetThumbImgHTML(content.Picture2, this.pictureWidth, this.pictureHeight, content.Title, "pic", imgStyle, CropPicture);

                if (this.createLink)
                    return String.Format("<a href=\"{0}\">{1}</a>", Provider.GetPageUrl(template, content.Id, content.Category.Title, content.Title), imgHtml);
                else
                    return String.Format("{0}", imgHtml);

            }
            return String.Empty;
        }

        protected virtual string getTitleHTML(bool isFirstItem, string template, Content content, int row, int col)
        {
            if (!this.ShowTitle) return ""; //**

            string title = content.Title.StrCrop(this.titleLength);
            string link = getLinkAndIconHTML(title, isFirstItem, template, content);

            return String.Format("<div class=\"{0}\">{1}</div>",
                (isFirstItem && showFirstItemWithPicture) ? " clTitleFirst" : "clTitle",
                link);
        }

        protected virtual string getSpotTitleHTML(bool isFirstItem, string template, Content content, int row, int col)
        {
            if (!this.ShowSpotTitle) return ""; //**

            string title = content.SpotTitle.StrCrop(this.titleLength);
            string link = getLinkAndIconHTML(title, isFirstItem, template, content);

            return String.Format("<div class=\"{0}\">{1}</div>",
                (row + col == 0 && showFirstItemWithPicture) ? " clSTitleFirst" : "clSTitle",
                title);
        }

        protected virtual string getAuthorHTML(string authorName, bool isFirstItem, string template, Content content, int row, int col)
        {
            if (fieldOrder == "author")
                authorName = getLinkAndIconHTML(authorName, isFirstItem, template, content);
            return !this.ShowAuthor ? "" : String.Format("<div class=\"clAuthor\">{0}</div>", authorName);
        }

        protected virtual string getCategoryHTML(string categoryName, bool isFirstItem, string template, Content content, int row, int col)
        {
            if (fieldOrder == "category")
                categoryName = getLinkAndIconHTML(categoryName, isFirstItem, template, content);
            return !this.ShowCategory ? "" : String.Format("<div class=\"clCategory\">{0}</div>", categoryName);
        }

        protected virtual string getSourceHTML(string sourceName, bool isFirstItem, string template, Content content, int row, int col)
        {
            if (fieldOrder == "source")
                sourceName = getLinkAndIconHTML(sourceName, isFirstItem, template, content);
            return !this.ShowSource ? "" : String.Format("<div class=\"clSource\">{0}</div>", sourceName);
        }

        protected virtual string getPublishDateHTML(string publishDate, int row, int col)
        {
            return !this.ShowPublishDate ? "" : String.Format("<div class=\"clPubDate\">{0}</div>", publishDate);
        }

        protected virtual string getDescriptionHTML(bool isFirstItem, string template, Content content, int row, int col)
        {
            string desc = content.Description.StrCrop(this.descriptionLength);
            if (this.showDescriptionAsLink)
                desc = getLinkAndIconHTML(desc, isFirstItem, template, content);

            if (this.ShowDescription || (this.showFirstItemWithPicture && isFirstItem))
                return String.Format("<div class=\"clDesc\">{0}</div>", desc);

            return String.Empty;
        }

        protected virtual string getCatDescriptionHTML(bool isFirstItem, string template, Content content, int row, int col)
        {
            string desc = content.Category.Description.StrCrop(this.descriptionLength);
            if (this.showDescriptionAsLink)
                desc = getLinkAndIconHTML(desc, isFirstItem, template, content); // bu önemli content'in linki olmalı

            if (this.ShowDescription || (this.showFirstItemWithPicture && isFirstItem))
                return String.Format("<div class=\"clDesc\">{0}</div>", desc);

            return String.Empty;
        }

        protected virtual string getMetinHTML(string metin, int row, int col)
        {
            if (this.ShowMetin)
                return String.Format("<div class=\"clMetin\">{0}</div>", metin);
            else
                return String.Empty;
        }

        protected abstract IDatabaseEntity[] GetContentList();

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} img.pic {{float:right}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.clItem {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.clTitle {{font-weight:bold}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.clSTitle {{font-weight:bold}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.clTitleFirst {{font-weight:bold;font-size:16px}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.clSTitleFirst {{font-weight:bold;font-size:16px}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.clCategory {{font-weight:bold}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.clAuthor {{font-style:italic}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.clPubDate {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.clDesc {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.clMetin {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.clSource {{font-style:italic}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.clMore {{text-align:right}}\n", this.Name, this.Id);
            return sb.ToString();
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            if (this.howManyItems <= this.skipFirst)
                throw new Exception(Provider.GetResource("[Skip First] must be less than [How Many Items]"));

            if(this.howManyItems-this.skipFirst <= this.random)
                throw new Exception(Provider.GetResource("[Random] must be less than [How Many Items] - [Skip First]"));

            string defaultFields = ContentList.defaultFieldOrder + ",picture2,text,cat_description";
            if (Regex.Match(this.fieldOrder, "[^\\w,\\,]").Success)
                throw new Exception(Provider.GetResource("FieldOrder is invalid. Please enter fields as {0}", defaultFields));
            foreach (string fieldName in this.fieldOrder.Split(','))
                if (defaultFields.IndexOf(fieldName) == -1)
                    throw new Exception(Provider.GetResource("{0} is not a valid field name. Please use one of the following: {1}", fieldName, defaultFields));
        }
    }
}
