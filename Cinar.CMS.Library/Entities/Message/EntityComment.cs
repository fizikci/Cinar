using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [Index(Name = "IND_EntityComment", ConstraintColumnNames = "EntityName,EntityId")]
    public class EntityComment : BaseEntity
    {
        public string EntityName { get; set; }
        public int EntityId { get; set; }

        [XmlIgnore]
        public IDatabaseEntity Entity { get { return Provider.Database.Read(Provider.GetEntityType(EntityName), EntityId); } }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Details { get; set; }

        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            GenericNotification not = new GenericNotification
                {
                    EntityName = "EntityComment",
                    EntityId = this.Id,
                    RelatedEntityName = EntityName,
                    RelatedEntityId = EntityId
                };

            switch (EntityName)
            {
                case "Project":
                    if ((Entity as Project).ManagerId != this.InsertUserId)
                    {
                        not.UserId = (Entity as Project).ManagerId;
                        not.Save();
                    }
                    break;
                case "Task":
                    if ((Entity as Task).AssignedToId != this.InsertUserId)
                    {
                        not.UserId = (Entity as Task).AssignedToId;
                        not.Save();
                    }
                    break;
                case "Company":
                    if ((Entity as Company).UserId != this.InsertUserId)
                    {
                        not.UserId = (Entity as Company).UserId;
                        not.Save();
                    }
                    break;
                case "Contact":
                    if ((Entity as Contact).UserId != this.InsertUserId)
                    {

                        not.UserId = (Entity as Contact).UserId;
                        not.Save();
                    }
                    break;
            }

        }
    }
}
