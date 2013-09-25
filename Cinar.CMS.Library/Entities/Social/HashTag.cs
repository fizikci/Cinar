using Cinar.CMS.Library;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    public class HashTag : BaseEntity
    {
        public string Name { get; set; }

        public int MentionCount { get; set; }

        [ColumnDetail(IsNotNull = true, References = typeof(Lang)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int LangId { get; set; }

        private Lang _lang;
        [XmlIgnore]
        public Lang Lang
        {
            get { return _lang ?? (_lang = (Lang)Provider.Database.Read(typeof(Lang), this.LangId)); }
        }
    }
}
