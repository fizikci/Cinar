using Cinar.CMS.Library;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.CMS.Library.Entities
{
    [Index(Name = "IND_GenericNotification", ConstraintColumnNames = "EntityName,EntityId")]
    public class GenericNotification : BaseEntity
    {
        /// <summary>
        /// Bu notification kime gösterilecek
        /// </summary>
        public int UserId { get; set; }

        public User User { get { return Provider.Database.Read<User>(UserId); } }

        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public string RelatedEntityName { get; set; }
        public int RelatedEntityId { get; set; }

        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            string msg = String.Format(@"
<i>{0}</i>{1}{2}:<br/>
<br/>
<a href=""http://{3}{4}"">{5}</a>
",
                        this.InsertUser.FullName,
                        EntityName==RelatedEntityName ? " updated the " : "wrote a comment for the ",
                        this.RelatedEntityName,
                        Provider.Configuration.SiteAddress,
                        getViewLink(),
                        Provider.Database.GetString("select Name from ["+RelatedEntityName+"] where Id={0}", RelatedEntityId));
            Provider.SendMail(this.User.Email, this.InsertUser.FullName + (EntityName == RelatedEntityName ? " updated a " : "wrote a comment for a ") + RelatedEntityName, msg);
        }

        private string getViewLink(){
            var prefix = "/Crm";
            if(this.EntityName=="Task" || this.EntityName=="Project")
                prefix = "/Issue";
	        return prefix + this.EntityName + "View.aspx?eId=" + this.EntityId;
        }

    }
}
