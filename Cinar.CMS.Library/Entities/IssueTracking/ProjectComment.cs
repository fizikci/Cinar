using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true)]
    public class ProjectComment : BaseEntity
    {
        [ColumnDetail(References = typeof(Project))]
        public int ProjectId { get; set; }

        [XmlIgnore]
        public Project Project { get { return Provider.Database.Read<Project>(ProjectId); } }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Details { get; set; }

        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            if (this.Project.ManagerId != this.InsertUserId)
            {
                new GenericNotification
                {
                    EntityName = "ProjectComment",
                    EntityId = this.Id,
                    RelatedEntityName = "Project",
                    RelatedEntityId = this.ProjectId,
                    UserId = this.Project.ManagerId
                }.Save();
            }
        }
    }
}
