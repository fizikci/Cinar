using Cinar.CMS.Library.Entities;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Content")]
    public class SourceDetail : AuthorSourceDetailBase
    {
        protected override string show()
        {
            Entities.Content content = Provider.Content;
            if (content == null)
                return Provider.GetResource("There must be a content in this page to access source details");
            if (content.SourceId == 0)
                return Provider.GetResource("Source of this content has not been defined");

            return base.show();
        }

        private UserRelatedEntity entity;
        public override UserRelatedEntity Entity
        {
            get
            {
                if (entity == null)
                {
                    Entities.Content content = Provider.Content;
                    entity = (UserRelatedEntity)Provider.Database.Read(typeof(Source), content.SourceId);
                }
                return entity;
            }
        }
    }

}
