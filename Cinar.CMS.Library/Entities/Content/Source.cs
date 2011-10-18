using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Source.Id, Source.Name as [NamedEntity.Name], Source.Visible as [BaseEntity.Visible] from [Source]", QueryOrderBy = "[NamedEntity.Name]")]
    [DefaultData(ColumnList = "Name", ValueList = "'Editorial'")]
    [OverrideAttribute(AttributeType = typeof(PictureFieldPropsAttribute), FieldName = "Picture", AttribProps = "SpecialFolder|AddRandomNumber|UseYearMonthDayFolders", NewValues = "sourceDir|false|false")]
    public class Source : UserRelatedEntity
    {
    }
}
