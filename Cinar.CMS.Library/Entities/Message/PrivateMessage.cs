using Cinar.Database;
using System;
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

        protected override void beforeSave(bool isUpdate)
        {
            throw new Exception(Provider.TR("Mesaj güncellenemez"));
        }

        protected override void afterSave(bool isUpdate)
        {
            base.afterSave(isUpdate);

            PrivateLastMessage plm = Provider.Database.Read<PrivateLastMessage>("MailBoxOwnerId={0} AND UserId={1}", Provider.User.Id, this.ToUserId);
            if (plm == null)
                plm = new PrivateLastMessage { MailBoxOwnerId = Provider.User.Id, UserId = this.ToUserId };
            plm.Summary = this.Message.StrCrop(30);
            plm.Save();

            PrivateLastMessage plm2 = Provider.Database.Read<PrivateLastMessage>("MailBoxOwnerId={0} AND UserId={1}", this.ToUserId, Provider.User.Id);
            if (plm2 == null)
                plm2 = new PrivateLastMessage { MailBoxOwnerId = this.ToUserId, UserId = Provider.User.Id };
            plm2.Summary = this.Message.StrCrop(30);
            plm2.Save();
        }

        public override string GetNameColumn()
        {
            return "Message";
        }
    }

}
