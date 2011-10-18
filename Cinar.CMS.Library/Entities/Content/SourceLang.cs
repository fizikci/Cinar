using System;
using Cinar.Database;
using System.Xml.Serialization;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select SourceLang.Id, SourceLang.Name as [SourceLang.Name], TLangId.Name as [Lang], SourceLang.Visible as [BaseEntity.Visible] from [SourceLang] left join [Lang] as TLangId ON TLangId.Id = [SourceLang].LangId")]
    public class SourceLang : NamedEntityLang
    {
        private int sourceId = 1;
        [ColumnDetail(IsNotNull = true, References = typeof(Source)), EditFormFieldProps(ControlType = ControlType.LookUp)]
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
                    _source = (Source)Provider.Database.Read(typeof(Source), this.sourceId);
                return _source;
            }
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            // isimsiz source mi olur! kontrolu
            if (String.IsNullOrEmpty(this.Name))
                throw new Exception(Provider.GetResource("Required field: Name"));
        }
    }

}
