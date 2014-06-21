using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true)]
    public class CompanyComment : BaseEntity
    {
        [ColumnDetail(References = typeof(Company))]
        public int CompanyId { get; set; }

        [XmlIgnore]
        public Company Company
        {
            get
            {
                return (Company)Provider.Database.Read(typeof(Company), this.CompanyId);
            }
        }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Details { get; set; }


        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            if (this.Company.UserId != this.InsertUserId)
            {
                new GenericNotification
                {
                    EntityName = "CompanyComment",
                    EntityId = this.Id,
                    RelatedEntityName = "Company",
                    RelatedEntityId = this.CompanyId,
                    UserId = this.Company.UserId
                }.Save();
            }
        }

    }
}
