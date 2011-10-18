using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Author.Id, Author.Name as [NamedEntity.Name], Author.Visible as [BaseEntity.Visible] from [Author]", QueryOrderBy = "[NamedEntity.Name]")]
    [DefaultData(ColumnList = "Name", ValueList = "'Editorial'")]
    [OverrideAttribute(AttributeType = typeof(PictureFieldPropsAttribute), FieldName = "Picture", AttribProps = "SpecialFolder|AddRandomNumber|UseYearMonthDayFolders", NewValues = "authorDir|false|false")]
    public class Author : UserRelatedEntity
    {
        private bool disableAutoContent;
        [ColumnDetail(IsNotNull = true, DefaultValue = "0")]
        public bool DisableAutoContent
        {
            get { return disableAutoContent; }
            set { disableAutoContent = value; }
        }
    }
}
