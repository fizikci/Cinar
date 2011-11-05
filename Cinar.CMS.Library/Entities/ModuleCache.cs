using Cinar.Database;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = false)]
    public class ModuleCache : BaseEntity
    {
        public ModuleCache()
        {
            ContentId = 1;
        }

        public int ModuleId { get; set; }

        //[ColumnDetail(IsNotNull = true, References = typeof(Content)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int ContentId { get; set; }

        //[ColumnDetail(IsNotNull = true, References = typeof(Lang)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int LangId { get; set; }

        [ColumnDetail(IsNotNull = true, ColumnType = DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string CachedHTML { get; set; }

        public override string GetNameValue()
        {
            return this.ModuleId.ToString();
        }
        public override string GetNameColumn()
        {
            return "ModuleId";
        }
    }
}
