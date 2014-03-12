using Cinar.Database;

namespace Cinar.CMS.DesktopEditor.Entities
{
    [DefaultData(ColumnList = "Name", ValueList = "'Editorial'")]
    public class Author : UserRelatedEntity
    {
        public string Title { get; set; }

        private bool disableAutoContent;
        [ColumnDetail(IsNotNull = true, DefaultValue = "0")]
        public bool DisableAutoContent
        {
            get { return disableAutoContent; }
            set { disableAutoContent = value; }
        }
    }
}
