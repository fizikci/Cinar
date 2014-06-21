﻿using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true)]
    public class ContactComment : BaseEntity
    {
        [ColumnDetail(References = typeof(Contact))]
        public int ContactId { get; set; }
        
        [XmlIgnore]
        public Contact Contact
        {
            get
            {
                return (Contact)Provider.Database.Read(typeof(Contact), this.ContactId);
            }
        }

        [ColumnDetail(ColumnType = DbType.Text)]
        public string Details { get; set; }

        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            if (this.Contact.UserId != this.InsertUserId)
            {
                new GenericNotification
                {
                    EntityName = "ContactComment",
                    EntityId = this.Id,
                    RelatedEntityName = "Contact",
                    RelatedEntityId = this.ContactId,
                    UserId = this.Contact.UserId
                }.Save();
            }
        }
    }
}
