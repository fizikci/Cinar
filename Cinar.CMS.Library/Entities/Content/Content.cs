using System;
using Cinar.Database;
using System.Drawing;
using System.Collections;
using System.Xml.Serialization;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [DefaultData(ColumnList = "CategoryId,ClassName,Title,Hierarchy,PublishDate,InsertDate", ValueList = "0,'Category','Kök','','1980-1-1',now()")]
    [EditFormDetails(DetailType = typeof(ContentLang), RelatedFieldName = "ContentId")]
    //[EditFormDetails(DetailType = typeof(Product), RelatedFieldName = "ContentId")]
    [EditFormDetails(DetailType = typeof(ContentPicture), RelatedFieldName = "ContentId")]
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Content.Id, Content.Title as [Content.Title], TCategoryId.Title as [Content.CategoryId], Content.PublishDate as [Content.PublishDate], Content.Visible as [BaseEntity.Visible] from [Content] left join [Content] as TCategoryId ON TCategoryId.Id = [Content].CategoryId", QueryOrderBy = "[Content.PublishDate] desc")]
    public class Content : BaseEntity
    {
        private string className = "Content";
        [ColumnDetail(IsNotNull = true, DefaultValue="Content"), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:_CLASSNAMELIST_")]
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        private string title;
        [ColumnDetail(IsNotNull = true, Length = 200), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string description;
        [ColumnDetail(ColumnType = DbType.Text)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string keywords;
        [ColumnDetail(ColumnType = DbType.Text)]
        public string Keywords
        {
            get { return keywords; }
            set { keywords = value; }
        }

        private string metin;
        [ColumnDetail(ColumnType = DbType.Text)]
        public string Metin
        {
            get { return metin; }
            set { metin = value; }
        }

        private string hierarchy = "";
        [ColumnDetail(IsNotNull = true, DefaultValue = "", Length = 100), EditFormFieldProps(Visible = false)]
        public string Hierarchy
        {
            get { return hierarchy; }
            set { hierarchy = value; }
        }

        public override string GetNameValue()
        {
            return this.title;
        }
        public override string GetNameColumn()
        {
            return "Title";
        }
        
        private int authorId = 1;
        [ColumnDetail(References = typeof(Author)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int AuthorId
        {
            get { return authorId; }
            set { authorId = value; }
        }

        private Author _author;
        [XmlIgnore]
        public Author Author
        {
            get
            {
                if (_author == null)
                    _author = (Author)Provider.Database.Read(typeof(Author), this.AuthorId);
                return _author;
            }
        }

        private int sourceId = 1;
        [ColumnDetail(References = typeof(Source)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int SourceId
        {
            get { return sourceId; }
            set { sourceId = value; }
        }

        private Source _source;
        [XmlIgnore]
        public Source Source
        {
            get
            {
                if (_source == null)
                    _source = (Source)Provider.Database.Read(typeof(Source), this.SourceId);
                return _source;
            }
        }

        private DateTime publishDate = DateTime.Now;
        [ColumnDetail(IsNotNull = true, DefaultValue = "1990-01-01")]
        public DateTime PublishDate
        {
            get { return publishDate; }
            set { publishDate = value; }
        }

        private string picture;
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        [PictureFieldProps(SpecialFolder = "uploadDir", SpecialNameField = "Title", AddRandomNumber = true, UseYearMonthDayFolders = true)]
        public string Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        private string tags = "";
        [ColumnDetail(Length = 300), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        private string tagRanks = "";
        [ColumnDetail(Length = 300), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string TagRanks
        {
            get { return tagRanks; }
            set { tagRanks = value; }
        }

        private int catId = 1;
        [ColumnDetail(IsNotNull = true, DefaultValue = "1", References = typeof(Content)), EditFormFieldProps(ControlType = ControlType.LookUp, Options="extraFilter:'ClassName=Category'")]
        public int CategoryId
        {
            get { return catId; }
            set { catId = value; }
        }

        private Content _category;
        [XmlIgnore]
        public Content Category
        {
            get
            {
                if (_category == null)
                    _category = (Content)Provider.Database.Read(typeof(Content), this.CategoryId);
                return _category;
            }
        }

        private string showInPage = "";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string ShowInPage
        {
            get { return showInPage; }
            set { showInPage = value; }
        }

        [XmlIgnore]
        public string PageLink
        {
            get {
                return Provider.GetTemplate(this, "");
            }
        }

        private string showContentsInPage = "";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string ShowContentsInPage
        {
            get { return showContentsInPage; }
            set { showContentsInPage = value; }
        }

        private string showCategoriesInPage = "";
        [EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:window.templates, addBlankItem:true")]
        public string ShowCategoriesInPage
        {
            get { return showCategoriesInPage; }
            set { showCategoriesInPage = value; }
        }

        private bool isManset;
        [ColumnDetail(IsNotNull = true, DefaultValue = "0")]
        public bool IsManset
        {
            get { return isManset; }
            set { isManset = value; }
        }

        private string spotTitle;
        [ColumnDetail(Length = 200), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string SpotTitle
        {
            get { return spotTitle; }
            set { spotTitle = value; }
        }

        private int contentSourceId;
        [ColumnDetail(References = typeof(ContentSource)), EditFormFieldProps(ControlType = ControlType.LookUp, Options = "readOnly:true")]
        public int ContentSourceId
        {
            get { return contentSourceId; }
            set { contentSourceId = value; }
        }

        private string sourceLink;
        [ColumnDetail(Length = 200), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string SourceLink
        {
            get { return sourceLink; }
            set { sourceLink = value; }
        }

        private int viewCount = 0;
        [ColumnDetail(IsNotNull = true, DefaultValue = "0"), EditFormFieldProps(Options = "readOnly:true")]
        public int ViewCount
        {
            get { return viewCount; }
            set { viewCount = value; }
        }

        private int commentCount = 0;
        [ColumnDetail(IsNotNull = true, DefaultValue = "0"), EditFormFieldProps(Options = "readOnly:true")]
        public int CommentCount
        {
            get { return commentCount; }
            set { commentCount = value; }
        }

        private int recommendCount = 0;
        [ColumnDetail(IsNotNull = true, DefaultValue = "0"), EditFormFieldProps(Options = "readOnly:true")]
        public int RecommendCount
        {
            get { return recommendCount; }
            set { recommendCount = value; }
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            if (this.Id == 1)
                throw new Exception(Provider.GetResource("Root category cannot be updated!"));

            // başlıksız içerik mi olur! kontrolü
            if (String.IsNullOrEmpty(this.Title))
                throw new Exception(Provider.GetResource("You should enter title"));

            //if (this.Tags.Length > 0)
            //    if (Regex.Match(this.Tags, "[^\\w,\\,\\-\\s]").Success)
            //        throw new Exception(Provider.GetResource("You may use only letters, space and comma for the field Tags"));

            // resim gelmişse kaydedelim
            if (Provider.Request.Files["PictureFile"] != null && Provider.Request.Files["PictureFile"].ContentLength > 0)
            {
                string picFileName = Provider.Request.Files["PictureFile"].FileName;
                if (!String.IsNullOrEmpty(picFileName))
                {
                    string imgUrl = Provider.AppSettings["uploadDir"] + "/" + System.IO.Path.GetFileName(picFileName);
                    Bitmap bmp = (Bitmap)Bitmap.FromStream(Provider.Request.Files["PictureFile"].InputStream);
                    if (bmp.Width > Provider.Configuration.ImageUploadMaxWidth)
                    {
                        Bitmap bmp2 = Utility.ScaleImage(bmp, Provider.Configuration.ImageUploadMaxWidth, 0);
                        imgUrl = imgUrl.Substring(0, imgUrl.LastIndexOf('.')) + ".jpg";
                        Utility.SaveJpeg(Provider.Server.MapPath(imgUrl), bmp2, Provider.Configuration.ThumbQuality);
                    }
                    else
                        Provider.Request.Files["PictureFile"].SaveAs(Provider.Server.MapPath(imgUrl));
                    this.Picture = imgUrl;
                }
            }

            if (this.catId > 0)
            {
                // eğer kategori değişmişse alt içeriklerin hiyerarşilerini update edelim.
                string newHierarchy = Provider.GetHierarchyLike(this.catId);
                if (isUpdate && this.Hierarchy != newHierarchy)
                    Provider.Database.ExecuteNonQuery(String.Format("update Content set Hierarchy=REPLACE(Hierarchy,'{0},{1}','{2},{1}')", this.Hierarchy, this.Id.ToString().PadLeft(5, '0'), newHierarchy));

                // yeni hiyerarşi
                this.Hierarchy = newHierarchy;
            }
        }

        private void updateTags(bool isUpdate)
        {
            string oldTags = this.GetOriginalValues()["Tags"] == null ? "" : this.GetOriginalValues()["Tags"].ToString().ToLower();
            string[] arrOldTags = Utility.SplitWithTrim(oldTags, ',');
            string[] arrNewTags = Utility.SplitWithTrim(this.Tags.ToLower(), ',');
            string[] arrNewTagRanks = Utility.SplitWithTrim(this.TagRanks, ',');

            ArrayList alToDelete = new ArrayList(), alToAdd = new ArrayList();
            for (int i = 0; i < arrOldTags.Length; i++)
                if (Array.IndexOf(arrNewTags, arrOldTags[i]) == -1)
                    alToDelete.Add(arrOldTags[i]);
            for (int i = 0; i < arrNewTags.Length; i++)
                if (Array.IndexOf(arrOldTags, arrNewTags[i]) == -1)
                {
                    if (i < arrNewTagRanks.Length)
                        arrNewTags[i] += "|" + arrNewTagRanks[i];
                    alToAdd.Add(arrNewTags[i]);
                }

            foreach (string tagToDelete in alToDelete)
            {
                Tag t = (Tag)Provider.Database.Read(typeof(Tag), "Name={0}", tagToDelete);
                if (t == null) continue;
                Provider.Database.ExecuteNonQuery("delete from ContentTag where ContentId={0} and TagId={1}", this.Id, t.Id);
                if(Provider.Configuration.CountTags) t.ContentCount = Convert.ToInt32(Provider.Database.GetValue("select count(*) from ContentTag t, Content c where t.ContentId=c.Id and t.TagId={0} and c.Visible=1", t.Id));
                if (t.ContentCount == 0)
                    t.Delete();
                else
                    t.Save();
            }
            foreach (string tagToAddPair in alToAdd)
            {
                string[] pair = tagToAddPair.Split('|');
                string tagToAdd = pair[0];

                Tag t = (Tag)Provider.Database.Read(typeof(Tag), "Name={0}", tagToAdd);
                if (t == null)
                {
                    t = new Tag();
                    t.Name = tagToAdd;
                    t.Save(); // to obtain Id..
                }

                ContentTag ct = new ContentTag();
                ct.ContentId = this.Id;
                ct.TagId = t.Id;
                if (pair.Length > 1)
                {
                    int rank = 0; Int32.TryParse(pair[1], out rank);
                    ct.Rank = rank;
                }
                ct.Save();


                if (Provider.Configuration.CountTags) t.ContentCount = Convert.ToInt32(Provider.Database.GetValue("select count(*) from ContentTag t, Content c where t.ContentId=c.Id and t.TagId={0} and c.Visible=1", t.Id));
                if (t.ContentCount == 0)
                    t.Delete();
                else
                    t.Save();
            }
            if (isUpdate && !this.GetOriginalValues()["Visible"].Equals(this.Visible))
                foreach (string strTag in arrNewTags)
                {
                    Tag t = (Tag)Provider.Database.Read(typeof(Tag), "Name={0}", strTag);
                    if (t == null) continue;
                    if (Provider.Configuration.CountTags) t.ContentCount = Convert.ToInt32(Provider.Database.GetValue("select count(*) from ContentTag t, Content c where t.ContentId=c.Id and t.TagId={0} and c.Visible=1", t.Id));
                    if (t.ContentCount == 0)
                        t.Delete();
                    else
                        t.Save();
                }
        }

        protected override void afterSave(bool isUpdate)
        {
            base.afterSave(isUpdate);

            // Aktif content'te değişiklik olduysa tekrar okunmasını sağlayalım
            if (Provider.Content != null && Provider.Content.Id == this.Id)
                Provider.Content = null;

            // etiketleri güncelleyelim
            updateTags(isUpdate);
        }

        protected override void beforeDelete()
        {
            base.beforeDelete();

            if (this.Id == 1)
                throw new Exception(Provider.GetResource("Root category cannot be deleted!"));

            foreach (ContentPicture cp in Provider.Database.ReadList(typeof(ContentPicture), "select * from ContentPicture where ContentId={0}", this.Id))
                cp.Delete();

            foreach (UserComment uc in Provider.Database.ReadList(typeof(UserComment), "select * from UserComment where ContentId={0}", this.Id))
                uc.Delete();

            if (this.Tags.Trim() != "")
            {
                Provider.Database.ExecuteNonQuery("delete from ContentTag where ContentId={0}", this.Id);
                foreach (string tagToDelete in Utility.SplitWithTrim(this.Tags, ','))
                {
                    Tag t = (Tag)Provider.Database.Read(typeof(Tag), "Name={0}", tagToDelete);
                    if (t == null) continue;
                    t.ContentCount = Convert.ToInt32(Provider.Database.GetValue("select count(*) from ContentTag t, Content c where t.ContentId=c.Id and t.TagId={0} and c.Visible=1", t.Id));
                    if (t.ContentCount == 0)
                        t.Delete();
                    else
                        t.Save();
                }
            }
        }

        public int FindMainCategoryId()
        {
            if (String.IsNullOrEmpty(this.Hierarchy))
                return 1;
            else if (this.Hierarchy == "00001")
                return this.Id;

            string[] ids = this.Hierarchy.Split(',');
            return Convert.ToInt32(ids[1]);
        }

        public bool IsUnder(string hierarchy)
        {
            return this.Hierarchy!=hierarchy && this.Hierarchy.StartsWith(hierarchy);
        }

        public bool IsUnder(int id)
        {
            return this.Id!=id && this.Hierarchy.Contains(id.ToString().PadLeft(5, '0'));
        }

        public bool IsOver(string hierarchy)
        {
            return this.Hierarchy != hierarchy && hierarchy.StartsWith(this.Hierarchy);
        }
    }

}
