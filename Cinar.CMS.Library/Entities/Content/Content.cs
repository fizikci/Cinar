using System;
using System.Collections.Generic;
using Cinar.Database;
using System.Drawing;
using System.Collections;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Linq;

namespace Cinar.CMS.Library.Entities
{
    [DefaultData(ColumnList = "CategoryId,ClassName,Title,Hierarchy,PublishDate,InsertDate", ValueList = "0,'Category','Kök','','1990-01-01','1990-01-01'")]
    [EditFormDetails(DetailType = typeof(ContentLang), RelatedFieldName = "ContentId")]
    //[EditFormDetails(DetailType = typeof(Product), RelatedFieldName = "ContentId")]
    [EditFormDetails(DetailType = typeof(ContentPicture), RelatedFieldName = "ContentId")]
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Content.Id as [Content.Id], Content.Title as [Content.Title], TCategoryId.Title as [TCategoryId.Title], Content.PublishDate as [Content.PublishDate], Content.Visible as [Content.Visible] from [Content] left join [Content] as TCategoryId ON TCategoryId.Id = [Content].CategoryId", QueryOrderBy = "Content.PublishDate desc")]
    public class Content : BaseEntity
    {
        [EditFormFieldProps(Visible=false)]
        public string Keyword { get; set; }

        private string className = "Content";
        [ColumnDetail(IsNotNull = true, DefaultValue = "Content"), EditFormFieldProps(ControlType = ControlType.ComboBox, OrderNo = 1, Options = "items:_CLASSNAMELIST_", Category = "Temel Bilgiler")]
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        private int catId = 1;
        [ColumnDetail(IsNotNull = true, DefaultValue = "1", References = typeof(Content)), EditFormFieldProps(ControlType = ControlType.LookUp, OrderNo = 2, Options = "extraFilter:'ClassName=Category'", Category = "Temel Bilgiler")]
        public int CategoryId
        {
            get { return catId; }
            set { catId = value; }
        }

        private string title;
        [ColumnDetail(IsNotNull = true, Length = 200), EditFormFieldProps(ControlType = ControlType.MemoEdit, OrderNo = 3, Category = "Temel Bilgiler")]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string description;
        [ColumnDetail(ColumnType = DbType.Text), EditFormFieldProps(Category = "Temel Bilgiler", OrderNo = 4)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string metin;
        [ColumnDetail(ColumnType = DbType.Text), EditFormFieldProps(Category = "Temel Bilgiler", OrderNo = 5)]
        public string Metin
        {
            get { return metin; }
            set { metin = value; }
        }

        private DateTime publishDate = DateTime.Now;
        [ColumnDetail(IsNotNull = true, DefaultValue = "1990-01-01"), EditFormFieldProps(Category = "Temel Bilgiler", OrderNo = 6)]
        public DateTime PublishDate
        {
            get { return publishDate; }
            set { publishDate = value; }
        }

        private string picture;
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit, OrderNo = 8, Category = "Temel Bilgiler")]
        [PictureFieldProps(SpecialFolder = "uploadDir", SpecialNameField = "Title", AddRandomNumber = true, UseYearMonthDayFolders = true)]
        public string Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        private string picture2;
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit, OrderNo = 9, Category = "Temel Bilgiler")]
        [PictureFieldProps(SpecialFolder = "uploadDir", SpecialNameField = "Title", AddRandomNumber = true, UseYearMonthDayFolders = true)]
        public string Picture2
        {
            get { return picture2; }
            set { picture2 = value; }
        }

        private string keywords;
        [ColumnDetail(ColumnType = DbType.Text), EditFormFieldProps(Category = "Temel Bilgiler")]
        public string Keywords
        {
            get { return keywords; }
            set { keywords = value; }
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
        [ColumnDetail(References = typeof(Author)), EditFormFieldProps(ControlType = ControlType.LookUp, Category = "Temel Bilgiler", OrderNo = 10)]
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
        [ColumnDetail(References = typeof(Source)), EditFormFieldProps(ControlType = ControlType.LookUp, Category = "Temel Bilgiler", OrderNo = 11)]
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

        private string tags = "";
        [ColumnDetail(Length = 300), EditFormFieldProps(ControlType = ControlType.TagEdit, Category = "Temel Bilgiler", OrderNo = 12)]
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
            get
            {
                return GetPageLinkWithTitle("");
            }
        }

        public string GetPageLinkWithTitle(string page)
        {
            return Provider.GetPageUrl(string.IsNullOrWhiteSpace(page) ? Provider.GetTemplate(this, "") : page, this.Id, this.Category.Title, this.Title);
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
        [ColumnDetail(IsNotNull = true, DefaultValue = "0"), EditFormFieldProps(Category = "Temel Bilgiler", OrderNo = 13)]
        public bool IsManset
        {
            get { return isManset; }
            set { isManset = value; }
        }

        private string spotTitle;
        [ColumnDetail(Length = 200), EditFormFieldProps(ControlType = ControlType.MemoEdit, Category = "Temel Bilgiler", OrderNo = 14)]
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
        [ColumnDetail(Length = 200), EditFormFieldProps(ControlType = ControlType.MemoEdit, Category = "Temel Bilgiler", OrderNo = 15)]
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

        [ColumnDetail(IsNotNull = true, DefaultValue = "0"), EditFormFieldProps(Options = "readOnly:true")]
        public int LikeIt
        {
            get;
            set;
        }

        private string extraField1 = "";
        private string extraField2 = "";
        private string extraField3 = "";
        private string extraField4 = "";
        private string extraField5 = "";

        [EditFormFieldProps(Category = "Extra")]
        public string ExtraField1 { get { return extraField1; } set { extraField1 = value; } }
        [EditFormFieldProps(Category = "Extra")]
        public string ExtraField2 { get { return extraField2; } set { extraField2 = value; } }
        [EditFormFieldProps(Category = "Extra")]
        public string ExtraField3 { get { return extraField3; } set { extraField3 = value; } }
        [EditFormFieldProps(Category = "Extra")]
        public string ExtraField4 { get { return extraField4; } set { extraField4 = value; } }
        [EditFormFieldProps(Category = "Extra")]
        public string ExtraField5 { get { return extraField5; } set { extraField5 = value; } }

        public override void BeforeSave()
        {
            base.BeforeSave();

            this.Tags = this.Tags.Trim(',', ' ');

            if (Id==0)
                this.Keyword = Guid.NewGuid().ToString();

            if (this.Id == 1)
                throw new Exception(Provider.GetResource("Root category cannot be updated!"));

            // başlıksız içerik mi olur! kontrolü
            if (String.IsNullOrEmpty(this.Title))
                throw new Exception(Provider.GetResource("You should enter title"));

            // resim gelmişse kaydedelim
            if (Provider.Request.Files["Picture"] != null && Provider.Request.Files["Picture"].ContentLength > 0)
            {
                string picFileName = Provider.Request.Files["Picture"].FileName;
                if (!String.IsNullOrEmpty(picFileName))
                {
                    string imgUrl = Provider.AppSettings["uploadDir"] + "/" + System.IO.Path.GetFileName(picFileName);
                    Image bmp = Image.FromStream(Provider.Request.Files["Picture"].InputStream);
                    if (bmp.Width > Provider.Configuration.ImageUploadMaxWidth)
                    {
                        Image bmp2 = bmp.ScaleImage(Provider.Configuration.ImageUploadMaxWidth, 0);
                        imgUrl = imgUrl.Substring(0, imgUrl.LastIndexOf('.')) + ".jpg";
                        bmp2.SaveJpeg(Provider.MapPath(imgUrl), Provider.Configuration.ThumbQuality);
                    }
                    else
                        Provider.Request.Files["Picture"].SaveAs(Provider.MapPath(imgUrl));
                    this.Picture = imgUrl;

                    Provider.DeleteThumbFiles(imgUrl);
                }
            }
            if (Provider.Request.Files["Picture2"] != null && Provider.Request.Files["Picture2"].ContentLength > 0)
            {
                string picFileName = Provider.Request.Files["Picture2"].FileName;
                if (!String.IsNullOrEmpty(picFileName))
                {
                    string imgUrl = Provider.AppSettings["uploadDir"] + "/" + System.IO.Path.GetFileName(picFileName);
                    Image bmp = Image.FromStream(Provider.Request.Files["Picture2"].InputStream);
                    if (bmp.Width > Provider.Configuration.ImageUploadMaxWidth)
                    {
                        Image bmp2 = bmp.ScaleImage(Provider.Configuration.ImageUploadMaxWidth, 0);
                        imgUrl = imgUrl.Substring(0, imgUrl.LastIndexOf('.')) + ".jpg";
                        bmp2.SaveJpeg(Provider.MapPath(imgUrl), Provider.Configuration.ThumbQuality);
                    }
                    else
                        Provider.Request.Files["Picture2"].SaveAs(Provider.MapPath(imgUrl));
                    this.Picture2 = imgUrl;

                    Provider.DeleteThumbFiles(imgUrl);
                }
            }

            if (this.catId > 0)
            {
                // eğer kategori değişmişse alt içeriklerin hiyerarşilerini update edelim.
                string newHierarchy = Provider.GetHierarchyLike(this.catId);
                if (Id>0 && this.Hierarchy != newHierarchy)
                    Provider.Database.ExecuteNonQuery(String.Format("update Content set Hierarchy=REPLACE(Hierarchy,'{0},{1}','{2},{1}')", this.Hierarchy, this.Id.ToString().PadLeft(5, '0'), newHierarchy));

                // yeni hiyerarşi
                this.Hierarchy = newHierarchy;
            }
        }

        private void updateTags(bool isUpdate)
        {
            string oldTags = this.GetOriginalValues()["Tags"] == null ? "" : this.GetOriginalValues()["Tags"].ToString().ToLower();
            string[] arrOldTags = oldTags.SplitWithTrim(',');
            string[] arrNewTags = this.Tags.ToLower().SplitWithTrim(',');
            string[] arrNewTagRanks = this.TagRanks.SplitWithTrim(',');

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
            if (isUpdate && this.GetOriginalValues()["Visible"]!=null && !this.GetOriginalValues()["Visible"].Equals(this.Visible))
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

        public override void AfterSave()
        {
            base.AfterSave();

            // Aktif content'te değişiklik olduysa tekrar okunmasını sağlayalım
            if (Provider.Content != null && Provider.Content.Id == this.Id)
                Provider.Content = null;

            // etiketleri güncelleyelim
            updateTags(Id>0);
        }

        protected override bool beforeDelete()
        {
            base.beforeDelete();

            if (this.Id == 1)
                throw new Exception(Provider.GetResource("Root category cannot be deleted!"));

            foreach (ContentContent cc in Provider.Database.ReadList(typeof(ContentContent), "select * from ContentContent where ParentContentId={0}", this.Id))
            {
                cc.ChildContent.Delete();
                cc.Delete();
            }

            foreach (ContentPicture cp in Provider.Database.ReadList(typeof(ContentPicture), "select * from ContentPicture where ContentId={0}", this.Id))
                cp.Delete();

            foreach (ContentUser cu in Provider.Database.ReadList(typeof(ContentUser), "select * from ContentUser where ContentId={0}", this.Id))
                cu.Delete();

            foreach (UserComment uc in Provider.Database.ReadList(typeof(UserComment), "select * from UserComment where ContentId={0}", this.Id))
                uc.Delete();

            if (this.Tags.Trim() != "")
            {
                Provider.Database.ExecuteNonQuery("delete from ContentTag where ContentId={0}", this.Id);
                foreach (string tagToDelete in this.Tags.SplitWithTrim(','))
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

            return true;
        }

        [Description("Returns the upper category of this content which is the closest to the root category.")]
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

        [Description("Returns true if this content under the content specified with id")]
        public bool IsUnder(int id)
        {
            return this.Id!=id && this.Hierarchy.Contains(id.ToString().PadLeft(5, '0'));
        }

        public bool IsOver(string hierarchy)
        {
            return this.Hierarchy != hierarchy && hierarchy.StartsWith(this.Hierarchy);
        }

        [Description("Deletes this content and children contents recursively")]
        public void RecursiveDelete()
        {
            Provider.Database.Execute(innerRecursiveDelete);
        }

        private void innerRecursiveDelete()
        {
            List<Content> children = Provider.Database.ReadList<Content>(FilterExpression.Where("CategoryId", CriteriaTypes.Eq, this.Id));
            foreach (var content in children)
                content.RecursiveDelete();
            this.Delete();
        }

        [Description("Returns the path of the thumb picture of this content picture")]
        public string GetThumbPicture(int width, int height, bool cropPicture)
        {
            return Provider.GetThumbPath(this.Picture, width, height, cropPicture);
        }


        [XmlIgnore]
        [Description("Category of this content.")]
        public Content Category
        {
            get
            {
                if (this.Id == 1)
                    return null;
                return (Content)Provider.Translate(Provider.Database.Read<Content>(this.CategoryId));
            }
        }

        [XmlIgnore]
        [Description("Returns the root content. All the other contents is branches of the root content.")]
        public Content Root
        {
            get
            {
                return (Content)Provider.Translate(Provider.Database.Read<Content>(1));
            }
        }

        [XmlIgnore]
        [Description("Returns the sub contents of this content. (where CategoryId = this.Id)")]
        public List<Content> Contents
        {
            get
            {
                return Provider.Translate(Provider.Database.ReadList<Content>("select * from Content where CategoryId={0}", this.Id)).Cast<Content>().ToList();
            }
        }

        [XmlIgnore]
        [Description("Returns the child contents of this content.")]
        public List<Content> ChildContents
        {
            get
            {
                return Provider.Translate(Provider.Database.ReadList<Content>("select * from Content where Id in (select ChildContentId from ContentContent where ParentContentId={0})", this.Id)).Cast<Content>().ToList();
            }
        }

        [XmlIgnore]
        [Description("Returns the picture and files of this content.")]
        public List<ContentPicture> Files
        {
            get
            {
                return Provider.Translate(Provider.Database.ReadList<ContentPicture>("select * from ContentPicture where ContentId={0}", this.Id)).Cast<ContentPicture>().ToList();
            }
        }

        [XmlIgnore]
        [Description("Returns the pdf files of this content.")]
        public List<ContentPicture> PdfFiles
        {
            get
            {
                return Provider.Translate(Provider.Database.ReadList<ContentPicture>("select * from ContentPicture where ContentId={0} AND fileName like {1}", this.Id, "%.pdf")).Cast<ContentPicture>().ToList();
            }
        }

        public static Content Get(int id) {
            return (Content)Provider.Translate(Provider.Database.Read<Content>(id));
        }
    }

}
