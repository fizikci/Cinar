using System;
using Cinar.Database;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    public class UserContact : BaseEntity
    {
        private int userId;
        [ColumnDetail(References = typeof(User)), EditFormFieldProps(ControlType = ControlType.LookUp)]
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        [XmlIgnore]
        public User User
        {
            get
            {
                return (User)Provider.Database.Read(typeof(User), this.userId);
            }
        }

        public override void BeforeSave()
        {
            base.BeforeSave();

            if (Id==0)
            {
                if (Provider.Database.GetInt("select count(*) from UserContact where InsertDate>{0}", DateTime.Now.Date) >= 50)
                    throw new Exception(Provider.TR("Günde 50 kişiden fazla takip edilemez"));
            }
        }
    }
}
