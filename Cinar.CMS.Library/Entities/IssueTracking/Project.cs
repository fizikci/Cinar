using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true)]
    public class Project : NamedEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }

        [ColumnDetail(References = typeof(User))]
        public int ManagerId { get; set; }

        public User Manager
        {
            get
            {
                return Provider.Database.Read<User>(ManagerId);
            }
        }

        public Project() {
            ManagerId = Provider.User.Id;
        }

        public List<ProjectUser> GetTeamMembers()
        {
            return Provider.Database.ReadList<ProjectUser>("SELECT * FROM ProjectUser WHERE ProjectId = {0} ORDER BY Id", Id) ?? new List<ProjectUser>();
        }

        public List<Task> GetTasks()
        {
            return Provider.Database.ReadList<Task>("SELECT * FROM Task WHERE ProjectId = {0} ORDER BY Id", Id) ?? new List<Task>();
        }

        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            if (!isUpdate)
                new ProjectUser { ProjectId = this.Id, UserId = Provider.User.Id }.Save();
        }
    }
}
