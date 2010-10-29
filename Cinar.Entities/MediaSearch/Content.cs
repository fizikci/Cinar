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
        [FieldDetail(Length=500)]
        public string SourceUrl { get; set; }

        [FieldDetail(References=typeof(ContentDefinition))]
        public int ContentDefinitionId { get; set; }

        [FieldDetail(Length=500)]
        public string Title { get; set; }
        
        [FieldDetail(References=typeof(Author))]
        public int AuthorId { get; set; }

        [FieldDetail(Length=500)]
        public string ImageUrl { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
