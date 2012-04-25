using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Id, Name as [NamedEntity.Name], DisplayName as [Tag.DisplayName], Headline as [Tag.Headline] from Tag", QueryOrderBy = "[NamedEntity.Name]")]
    [EditFormDetails(DetailType = typeof(TagLang), RelatedFieldName = "TagId")]
    //[OverrideAttribute(AttributeType = typeof(PictureFieldPropsAttribute), FieldName = "Picture", AttribProps = "SpecialFolder", NewValues = "sourceDir")]
    public class Tag : NamedEntity
    {
        private string displayName;
        [ColumnDetail(Length = 100), EditFormFieldProps(Category="Ekstra")]
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        private bool headline = false;
        [ColumnDetail(DefaultValue = "0"), EditFormFieldProps(Category = "Ekstra")]
        public bool Headline
        {
            get { return headline; }
            set { headline = value; }
        }

        private int contentCount = 0;
        [EditFormFieldProps(Options = "readOnly:true", Category = "Ekstra")]
        public int ContentCount
        {
            get { return contentCount; }
            set { contentCount = value; }
        }

        private bool noise = false;
        [ColumnDetail(DefaultValue = "0"), EditFormFieldProps(Category = "Ekstra")]
        public bool Noise
        {
            get { return noise; }
            set { noise = value; }
        }

        protected override bool beforeDelete()
        {
            base.beforeDelete();

            Provider.Database.ExecuteNonQuery("delete from ContentTag where TagId={0}", this.Id);

            return true;
        }
    }
}
