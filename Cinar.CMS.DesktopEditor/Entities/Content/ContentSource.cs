using System;
using Cinar.Database;

namespace Cinar.CMS.DesktopEditor.Entities
{
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
        public int SourceId
        {
            get { return sourceId; }
            set { sourceId = value; }
        }

        private int categoryId;
        public int CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }


        private int authorId;
        public int AuthorId
        {
            get { return authorId; }
            set { authorId = value; }
        }

        private string className = "News";
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        private string listPageAddress;
        public string ListPageAddress
        {
            get { return listPageAddress; }
            set { listPageAddress = value; }
        }

        private string listRegExp;
        public string ListRegExp
        {
            get { return listRegExp; }
            set { listRegExp = value; }
        }

        private string contentRegExp;
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
                fetchFrequency = value; 
            }
        }

        private DateTime lastFetched;
        public DateTime LastFetched
        {
            get { return lastFetched; }
            set { lastFetched = value; }
        }

        private DateTime lastFetchTrial;
        public DateTime LastFetchTrial
        {
            get { return lastFetchTrial; }
            set { lastFetchTrial = value; }
        }

    }

}
