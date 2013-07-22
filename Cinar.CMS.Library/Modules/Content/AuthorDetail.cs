using Cinar.CMS.Library.Entities;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Content")]
    public class AuthorDetail : AuthorSourceDetailBase
    {
        internal override string show()
        {
            Entities.Content content = Provider.Content;
            if (content == null)
                return "There must be a content in this page to access author details";
            if (content.AuthorId == 0)
                return "Author of this content has not been defined";

            return base.show();
        }

        private UserRelatedEntity entity;
        public override UserRelatedEntity Entity
        {
            get {
                if (entity == null)
                {
                    Entities.Content content = Provider.Content;
                    entity = (UserRelatedEntity)Provider.Database.Read(typeof(Author), content.AuthorId);
                }
                return entity;
            }
        }
    }

}
