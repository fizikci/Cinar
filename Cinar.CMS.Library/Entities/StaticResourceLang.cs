using System;
using Cinar.Database;
using System.Drawing;
using System.Xml.Serialization;
using System.Web;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    public class StaticResourceLang : BaseEntity
    {
        [ColumnDetail(IsNotNull = true, References = typeof(StaticResource)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int StaticResourceId { get; set; }

        [XmlIgnore]
        public StaticResource StaticResource
        {
            get { return Provider.Database.Read<StaticResource>(this.StaticResourceId); }
        }

        [ColumnDetail(IsNotNull = true, References = typeof(Lang)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int LangId { get; set; }

        [XmlIgnore]
        public Lang Lang
        {
            get { return Provider.Database.Read<Lang>(this.LangId); }
        }

        [ColumnDetail(IsNotNull = true, Length = 200)]
        public string Translation { get; set; }

        public override string GetNameValue()
        {
            return this.Translation;
        }
        public override string GetNameColumn()
        {
            return "Translation";
        }
    }

}
