using Cinar.Database;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Id, LogType, Category, Description, InsertDate from [Log]")]
    public class Log : BaseEntity
    {
        /// <summary>
        /// Error, Notice
        /// </summary>
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:['Error','Notice']")]
        public string LogType { get; set; }

        [ColumnDetail(IsNotNull = true), EditFormFieldProps(Options = "isHTML:false")]
        public string Category { get; set; }

        [ColumnDetail(IsNotNull = true, ColumnType = DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string Description { get; set; }

        public override string GetNameValue()
        {
            return this.Description;
        }
        public override string GetNameColumn()
        {
            return "Description";
        }
    }
}
