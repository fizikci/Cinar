using System;
using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select ContentSource.Id as [ContentSource.Id], ContentSource.Name as [ContentSource.Name], Content.Title as [Content.Title], Source.Name as [Source.Name], Author.Name as [Author.Name], ContentSource.LastFetchTrial as [ContentSource.LastFetchTrial], ContentSource.Visible as [ContentSource.Visible] from [ContentSource] left join [Content] ON Content.Id = [ContentSource].CategoryId left join [Source] ON Source.Id = [ContentSource].SourceId left join [Author] ON Author.Id = [ContentSource].AuthorId", QueryOrderBy = "ContentSource.Name desc")]
    public class ContentSource : BaseEntity
    {
        private string name;
        [ColumnDetail(IsNotNull = true, Length = 50)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int sourceId;
        [ColumnDetail(IsNotNull = true, References = typeof(Source)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int SourceId
        {
            get { return sourceId; }
            set { sourceId = value; }
        }

        private int categoryId;
        [ColumnDetail(References = typeof(Content)), EditFormFieldProps(ControlType = ControlType.LookUp, Options="extraFilter:'ClassName=Category'")]
        public int CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }


        private int authorId;
        [ColumnDetail(References = typeof(Author)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int AuthorId
        {
            get { return authorId; }
            set { authorId = value; }
        }

        private string className = "News";
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:_CLASSNAMELIST_")]
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        private string listPageAddress;
        [ColumnDetail(IsNotNull = true, Length = 200), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string ListPageAddress
        {
            get { return listPageAddress; }
            set { listPageAddress = value; }
        }

        private string listRegExp;
        [ColumnDetail(ColumnType = DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string ListRegExp
        {
            get { return listRegExp; }
            set { listRegExp = value; }
        }

        private string contentRegExp;
        [ColumnDetail(ColumnType = DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string ContentRegExp
        {
            get { return contentRegExp; }
            set { contentRegExp = value; }
        }

        private string encoding;
        public string Encoding
        {
            get { return encoding; }
            set { encoding = value; }
        }

        protected int fetchFrequency = 180;
        [ColumnDetail(IsNotNull = true, DefaultValue = "180")]
        public int FetchFrequency
        {
            get { return fetchFrequency; }
            set {
                if (value < 10) throw new Exception(Provider.GetResource("Fetch Frequency cannot be less than 10 minutes!"));
                fetchFrequency = value; 
            }
        }

        private DateTime lastFetched;
        [EditFormFieldProps(Options = "readOnly:true")]
        public DateTime LastFetched
        {
            get { return lastFetched; }
            set { lastFetched = value; }
        }

        private DateTime lastFetchTrial;
        [EditFormFieldProps(Options = "readOnly:true")]
        public DateTime LastFetchTrial
        {
            get { return lastFetchTrial; }
            set { lastFetchTrial = value; }
        }


        public override string GetNameValue()
        {
            return this.Name;
        }
        public override string GetNameColumn()
        {
            return "Name";
        }
    }

}
