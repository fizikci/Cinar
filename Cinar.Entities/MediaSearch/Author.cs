using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Database;
using Cinar.Entities.Standart;

namespace Cinar.Entities.MediaSearch
{
    public class Author : NamedEntity
    {
        [ColumnDetail(References=typeof(Media))]
        public int MediaId { get; set; }

        private Media media;
        public Media Media {
            get 
            {
                if (media == null && MediaId != 0)
                    media = CinarContext.Db.Read<Media>(MediaId);
                return media;
            }
            set
            {
                media = value;
                MediaId = value == null ? 0 : value.Id;
            }
        }
    }
}
