using System;
using Cinar.Database;
using System.Xml.Serialization;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false, QuerySelect = "select AuthorLang.Id, AuthorLang.Name as [AuthorLang.Name], Lang.Name as [Lang.Name], AuthorLang.Visible as [AuthorLang.Visible] from [AuthorLang] left join [Lang] ON Lang.Id = [AuthorLang].LangId", QueryOrderBy = "AuthorLang.Id desc")]
    public class AuthorLang : NamedEntityLang
    {
        public string Title { get; set; }

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

        public override void BeforeSave()
        {
            base.BeforeSave();

            // isimsiz author mi olur! kontrolu
            if (String.IsNullOrEmpty(this.Name))
                throw new Exception(Provider.GetResource("Required field: Name"));
        }
    }

}
