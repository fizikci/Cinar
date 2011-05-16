using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Database;
using Cinar.Entities.Standart;

namespace Cinar.Entities.MediaSearch
{
    public class ContentDefinition : BaseEntity
    {
        [ColumnDetail(Length=200)]
        public string RSSUrl { get; set; }

        [ColumnDetail(References=typeof(Category))]
        public int CategoryId { get; set; }

        private Category category;
        public Category Category
        {
            get
            {
                if (category == null && CategoryId != 0)
                    category = Context.Db.Read<Category>(CategoryId);
                return category;
            }
            set
            {
                category = value;
                CategoryId = value == null ? 0 : value.Id;
            }
        }

        [ColumnDetail(References=typeof(Media))]
        public int MediaId { get; set; }

        private Media media;
        public Media Media
        {
            get
            {
                if (media == null && MediaId != 0)
                    media = Context.Db.Read<Media>(MediaId);
                return media;
            }
            set
            {
                media = value;
                MediaId = value == null ? 0 : value.Id;
            }
        }

        public ContentType ContentType { get; set; }
        public string LinkSelector { get; set; }

        [ColumnDetail(Length=200)]
        public string TitleSelector { get; set; }

        [ColumnDetail(Length=200)]
        public string ContentSelector { get; set; }
        
        [ColumnDetail(Length=200)]
        public string AuthorSelector { get; set; }

        [ColumnDetail(Length=200)]
        public string ImageSelector { get; set; }

        [ColumnDetail(Length=200)]
        public string DateSelector { get; set; }
    }

    public enum ContentType
    {
        News,
        Article
    }
}
