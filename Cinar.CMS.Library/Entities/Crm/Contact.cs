﻿using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Contact.Id as [Contact.Id], Contact.Name as [NamedEntity.Name] from Contact", QueryOrderBy = "Contact.Id desc")]
    public class Contact : NamedEntity, ICriticalEntity
    {
        #region Personal Info
        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=ContactPrefix'", Category = "")]
        public int PrefixId { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=Title'", Category = "")]
        public int TitleId { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=Title2'", Category = "")]
        public int Title2Id { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=RelationWithUs'", Category = "")]
        public int RelationWithUsId { get; set; }

        [ColumnDetail(References = typeof(Company)), EditFormFieldProps(Category = "Details")]
        public int CompanyId { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=Department'", Category = "")]
        public int DepartmentId { get; set; }

        [ColumnDetail(Length = 300), EditFormFieldProps(ControlType = ControlType.TagEdit, Category = "")]
        public string Tags { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=Language'", Category = "More")]
        public int Language1Id { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=Language'", Category = "More")]
        public int Language2Id { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=Language'", Category = "More")]
        public int Language3Id { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "")]
        public string Gender { get; set; }

        [EditFormFieldProps(Category = "")]
        public DateTime BirthDate { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=ContactKind1'", Category = "Details")]
        public int Kind1Id { get; set; }
        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=ContactKind2'", Category = "Details")]
        public int Kind2Id { get; set; }
        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=ContactKind3'", Category = "Details")]
        public int Kind3Id { get; set; }
        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=ContactKind4'", Category = "Details")]
        public int Kind4Id { get; set; }
        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=ContactKind5'", Category = "Details")]
        public int Kind5Id { get; set; }
        #endregion

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=AssistantType'", Category = "")]
        public int AssistantTypeId { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "")]
        public string AssistantName { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "")]
        public string AssistantEmail { get; set; }


        #region Contact Info

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Address")]
        public string Phone { get; set; }

        [ColumnDetail(Length = 10), EditFormFieldProps(Category = "Address")]
        public string InterPhone { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Address")]
        public string Phone2 { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Address")]
        public string Fax { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Address")]
        public string PhoneMobile { get; set; }

        [ColumnDetail(Length = 200), EditFormFieldProps(Category = "Address")]
        public string AddressLine1 { get; set; }

        [ColumnDetail(Length = 200), EditFormFieldProps(Category = "Address")]
        public string AddressLine2 { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Address")]
        public string City { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Address")]
        public string Town { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=Country'", Category = "Address")]
        public int CountryId { get; set; }

        [ColumnDetail(Length = 5), EditFormFieldProps(Category = "Address")]
        public string ZipCode { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(Category = "", Options = @"regEx:'^[\w-]+@([\w-]+\.)+[\w-]+$'")]
        public string Email { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(Category = "", Options = @"regEx:'^[\w-]+@([\w-]+\.)+[\w-]+$'")]
        public string Email2 { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(Category = "", Options = @"regEx:'^[\w-]+@([\w-]+\.)+[\w-]+$'")]
        public string Email3 { get; set; }

        [ColumnDetail(Length = 150), EditFormFieldProps(Category = "Address", Options = @"regEx:'(((ht|f)tp(s?):\/\/)|(www\.[^ \[\]\(\)\n\r\t]+)|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})\/)([^ \[\]\(\),;&quot;\'&lt;&gt;\n\r\t]+)([^\. \[\]\(\),;&quot;\'&lt;&gt;\n\r\t])|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})'")]
        public string Web { get; set; }

        #endregion

        private string extraField1 = "";
        private string extraField2 = "";
        private string extraField3 = "";
        private string extraField4 = "";
        private string extraField5 = "";

        [EditFormFieldProps(Category = "Extra")]
        public string ExtraField1 { get { return extraField1; } set { extraField1 = value; } }
        [EditFormFieldProps(Category = "Extra")]
        public string ExtraField2 { get { return extraField2; } set { extraField2 = value; } }
        [EditFormFieldProps(Category = "Extra")]
        public string ExtraField3 { get { return extraField3; } set { extraField3 = value; } }
        [EditFormFieldProps(Category = "Extra")]
        public string ExtraField4 { get { return extraField4; } set { extraField4 = value; } }
        [EditFormFieldProps(Category = "Extra")]
        public string ExtraField5 { get { return extraField5; } set { extraField5 = value; } }

        private int userId;
        [ColumnDetail(IsNotNull = true, References = typeof(User)), EditFormFieldProps(ControlType = ControlType.LookUp, Options = "readOnly:true", Category = "Details")]
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private User _user;
        [XmlIgnore]
        public User User
        {
            get
            {
                if (_user == null)
                    _user = (User)Provider.Database.Read(typeof(User), this.userId);
                return _user;
            }
        }

        [ColumnDetail(Length = 300), EditFormFieldProps(ControlType = ControlType.TagEdit, Category = "Extra")]
        public string NewsletterMembership { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=Interest'", Category = "More")]
        public int InterestId1 { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=Interest'", Category = "More")]
        public int InterestId2 { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=Interest'", Category = "More")]
        public int InterestId3 { get; set; }

        [EditFormFieldProps(Category = "Details")]
        public string ReferenceBy { get; set; }



        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);

            if (!this.UserId.Equals(this.GetOriginalValues()["UserId"]) && Provider.User.Id != this.UserId)
            {
                new GenericNotification
                {
                    EntityName = "Contact",
                    EntityId = this.Id,
                    RelatedEntityName = "Contact",
                    RelatedEntityId = this.Id,
                    UserId = this.UserId
                }.Save();
            }
        }

    }
}
