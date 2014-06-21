using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true)]
    public class TaskComment : BaseEntity
    {
        [ColumnDetail(References = typeof(Task))]
        public int TaskId { get; set; }

        [XmlIgnore]
        public Task Task { get { return Provider.Database.Read<Task>(TaskId); } }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Details { get; set; }

        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            if (this.Task.AssignedToId != this.InsertUserId)
            {
                new GenericNotification
                {
                    EntityName = "TaskComment",
                    EntityId = this.Id,
                    RelatedEntityName = "Task",
                    RelatedEntityId = this.TaskId,
                    UserId = this.Task.AssignedToId
                }.Save();
            }
        }
    }
}
