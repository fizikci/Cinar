using System.Xml.Serialization;
using Cinar.Database;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Id, LogType, Category, Description, InsertDate from [Log]")]
    public class Log : SimpleBaseEntity
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

        public string EntityName { get; set; }
        public int EntityId { get; set; }

        [ColumnDetail(References = typeof(User)), EditFormFieldProps(Visible = false)]
        public int InsertUserId { get; set; }

        private User _insertUser;
        [XmlIgnore]
        public User InsertUser
        {
            get { return _insertUser ?? (_insertUser = (User)Provider.Database.Read(typeof(User), this.InsertUserId)); }
        }

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
