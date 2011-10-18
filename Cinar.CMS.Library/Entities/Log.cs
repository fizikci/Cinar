using Cinar.Database;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Log.Id, Log.Category as [Log.Category], Log.Description as [Log.Description] from [Log]")]
    public class Log : BaseEntity
    {
        private string logType;
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "items:['Error','Notice']")]
        /// <summary>
        ///   Error, Notice
        /// </summary>
        public string LogType
        {
            get { return logType; }
            set { logType = value; }
        }

        private string category;
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(Options = "isHTML:false")]
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        private string description;
        [ColumnDetail(IsNotNull = true, ColumnType = DbType.Text), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public override string GetNameValue()
        {
            return this.description;
        }
        public override string GetNameColumn()
        {
            return "Description";
        }
    }
}
