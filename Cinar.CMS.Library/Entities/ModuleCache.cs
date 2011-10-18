using Cinar.Database;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false)]
    public class ModuleCache : BaseEntity
    {
        private int moduleId = 0;
        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }

        private int contentId = 1;
        [ColumnDetail(IsNotNull = true, References = typeof(Content))]
        [EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int ContentId
        {
            get { return contentId; }
            set { contentId = value; }
        }

        private int langId;
        [ColumnDetail(IsNotNull = true, References = typeof(Lang)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int LangId
        {
            get { return langId; }
            set { langId = value; }
        }

        private string cachedHTML;
        [ColumnDetail(IsNotNull = true, ColumnType = DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string CachedHTML
        {
            get { return cachedHTML; }
            set { cachedHTML = value; }
        }

        public override string GetNameValue()
        {
            return this.moduleId.ToString();
        }
        public override string GetNameColumn()
        {
            return "ModuleId";
        }
    }
}
