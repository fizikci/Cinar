using System;
using Cinar.Database;
using System.Xml.Serialization;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select TagLang.Id as [TagLang.Id], TagLang.Name as [TagLang.Name], Lang.Name as [Lang.Name], TagLang.Visible as [TagLang.Visible] from [TagLang] left join [Lang] ON Lang.Id = [TagLang].LangId", QueryOrderBy = "TagLang.Id desc")]
    public class TagLang : NamedEntityLang
    {
        private int tagId = 1;
        [ColumnDetail(IsNotNull = true, References = typeof(Tag)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int TagId
        {
            get { return tagId; }
            set { tagId = value; }
        }

        private string displayName;
        [ColumnDetail(Length = 100)]
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        private Tag _tag;
        [XmlIgnore]
        public Tag Tag
        {
            get
            {
                if (_tag == null)
                    _tag = (Tag)Provider.Database.Read(typeof(Tag), this.tagId);
                return _tag;
            }
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            // isimsiz tag mi olur! kontrolu
            if (String.IsNullOrEmpty(this.Name))
                throw new Exception(Provider.GetResource("Required field: Name"));
        }
    }

}
