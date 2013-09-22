using Cinar.Database;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select PrivateMessage.Id as [PrivateMessage.Id], PrivateMessage.Message as [PrivateMessage.Message] from PrivateMessage", QueryOrderBy = "PrivateMessage.Id desc")]
    public class PrivateMessage : BaseEntity
    {
        [ColumnDetail(IsNotNull = true, ColumnType = DbType.Text)]
        public string Message { get; set; }

        public int ToUserId { get; set; }

        public bool Read { get; set; }

        public bool DeletedBySender { get; set; }
        public bool DeletedByReceiver { get; set; }

        public override string GetNameColumn()
        {
            return "Message";
        }
    }

}
