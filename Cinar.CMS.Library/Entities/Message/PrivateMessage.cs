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

        public User ToUser
        {
            get
            {
                return Provider.Database.Read<User>(ToUserId);
            }
        }

        public bool Read { get; set; }

        public bool DeletedBySender { get; set; }
        public bool DeletedByReceiver { get; set; }

        public override void BeforeSave()
        {
            base.BeforeSave();

            if (Id>0)
                throw new Exception(Provider.TR("Mesaj güncellenemez"));
        }

        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            PrivateLastMessage plm = Provider.Database.Read<PrivateLastMessage>("MailBoxOwnerId={0} AND UserId={1}", Provider.User.Id, this.ToUserId);
            if (plm == null)
                plm = new PrivateLastMessage { MailBoxOwnerId = Provider.User.Id, UserId = this.ToUserId };
            plm.Summary = this.Message.StrCrop(30);
            plm.Save();

            PrivateLastMessage plm2 = Provider.Database.Read<PrivateLastMessage>("MailBoxOwnerId={0} AND UserId={1}", this.ToUserId, Provider.User.Id);
            if((plm2==null || plm2.UpdateDate<DateTime.Now.AddHours(-2)) && ToUser.Settings.MailAfterMessage)
            {
                string msg = String.Format(@"
                                Merhaba {0},<br/><br/>
                                {1} size özel mesaj gönderdi:<br/><br/>
                                <i>{2}</i><br/><br/>
                                <a href=""http://{3}"">http://{3}</a>",
                this.ToUser.FullName,
                Provider.User.FullName,
                this.Message,
                Provider.Configuration.SiteAddress);
                Provider.SendMail(this.ToUser.Email, Provider.User.FullName + " size özel mesaj gönderdi", msg);
            }

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
