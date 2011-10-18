using System;
using Cinar.Database;
using System.Xml.Serialization;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select AuthorLang.Id, AuthorLang.Name as [AuthorLang.Name], TLangId.Name as [Lang], AuthorLang.Visible as [BaseEntity.Visible] from [AuthorLang] left join [Lang] as TLangId ON TLangId.Id = [AuthorLang].LangId")]
    public class AuthorLang : NamedEntityLang
    {
        private int authorId = 1;
        [ColumnDetail(IsNotNull = true, References = typeof(Author)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int AuthorId
        {
            get { return authorId; }
            set { authorId = value; }
        }

        private Author _author;
        [XmlIgnore]
        public Author Author
        {
            get
            {
                if (_author == null)
                    _author = (Author)Provider.Database.Read(typeof(Author), this.authorId);
                return _author;
            }
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            // isimsiz author mi olur! kontrolu
            if (String.IsNullOrEmpty(this.Name))
                throw new Exception(Provider.GetResource("Required field: Name"));
        }
    }

}
