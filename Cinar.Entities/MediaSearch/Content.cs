using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Database;
using Cinar.Entities.Standart;

namespace Cinar.Entities.MediaSearch
{
    public class Content : BaseEntity
    {
        [ColumnDetail(Length=500)]
        public string SourceUrl { get; set; }

        [ColumnDetail(References=typeof(ContentDefinition))]
        public int ContentDefinitionId { get; set; }

        [ColumnDetail(Length=500)]
        public string Title { get; set; }
        
        [ColumnDetail(References=typeof(Author))]
        public int AuthorId { get; set; }

        [ColumnDetail(Length=500)]
        public string ImageUrl { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
