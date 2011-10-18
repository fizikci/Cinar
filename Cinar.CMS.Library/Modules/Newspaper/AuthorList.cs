namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Newspaper")]
    public class AuthorList : MansetByGrouping
    {
        public AuthorList()
        {
            this.WithAnimation = true;
            this.WhichPicture = "Author";
            this.MansetFieldOrder = "author,image,title,description";
            this.FieldOrder = "title,author";
            this.GroupBy = "AuthorId";
        }
    }
}
