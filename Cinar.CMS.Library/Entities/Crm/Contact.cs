using Cinar.Database;
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
        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=Title'", Category = "Personal Info")]
        public int TitleId { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=Title'", Category = "Personal Info")]
        public int Title2Id { get; set; }

        [ColumnDetail(References = typeof(Company)), EditFormFieldProps(Category = "Personal Info")]
        public int CompanyId { get; set; }

        [ColumnDetail(Length = 300), EditFormFieldProps(ControlType = ControlType.TagEdit, Category = "Personal Info")]
        public string Tags { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=Language'", Category = "Personal Info")]
        public int Language1Id { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=Language'", Category = "Personal Info")]
        public int Language2Id { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=Language'", Category = "Personal Info")]
        public int Language3Id { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Personal Info")]
        public string Gender { get; set; }

        [EditFormFieldProps(Category = "Personal Info")]
        public DateTime BirthDate { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=ContactKind1'", Category = "Personal Info")]
        public int Kind1Id { get; set; }
        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=ContactKind2'", Category = "Personal Info")]
        public int Kind2Id { get; set; }
        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=ContactKind3'", Category = "Personal Info")]
        public int Kind3Id { get; set; }
        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=ContactKind4'", Category = "Personal Info")]
        public int Kind4Id { get; set; }
        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=ContactKind5'", Category = "Personal Info")]
        public int Kind5Id { get; set; }
        #endregion

        #region Contact Info

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Contact Info")]
        public string Phone { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Contact Info")]
        public string Phone2 { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Contact Info")]
        public string Fax { get; set; }

        [ColumnDetail(Length = 200), EditFormFieldProps(Category = "Contact Info")]
        public string AddressLine1 { get; set; }

        [ColumnDetail(Length = 200), EditFormFieldProps(Category = "Contact Info")]
        public string AddressLine2 { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Contact Info")]
        public string City { get; set; }

        [ColumnDetail(References = typeof(Definition)), EditFormFieldProps(ControlType = ControlType.ComboBox, Options = "extraFilter:'Kind=Country'", Category = "Contact Info")]
        public int CountryId { get; set; }

        [ColumnDetail(Length = 5), EditFormFieldProps(Category = "Contact Info")]
        public string ZipCode { get; set; }

        [ColumnDetail(IsNotNull = true, Length = 100, IsUnique = true), EditFormFieldProps(Category = "Contact Info", Options = @"regEx:'^[\w-]+@([\w-]+\.)+[\w-]+$'")]
        public string Email { get; set; }

        [ColumnDetail(Length = 150), EditFormFieldProps(Category = "Contact Info", Options = @"regEx:'(((ht|f)tp(s?):\/\/)|(www\.[^ \[\]\(\)\n\r\t]+)|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})\/)([^ \[\]\(\),;&quot;\'&lt;&gt;\n\r\t]+)([^\. \[\]\(\),;&quot;\'&lt;&gt;\n\r\t])|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})'")]
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
        [ColumnDetail(IsNotNull = true, References = typeof(User)), EditFormFieldProps(ControlType = ControlType.LookUp, Options = "readOnly:true")]
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

        public bool NewsletterMembership { get; set; }
    }
}
