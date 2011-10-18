using System;
using Cinar.Database;

namespace Cinar.CMS.Library.Entities
{
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Id as [BaseEntity.Id], Name as [ContactUs.Name], Email as [ContactUs.Email], Subject as [ContactUs.Subject] from ContactUs", QueryOrderBy = "[BaseEntity.Id] desc")]
    public class ContactUs : BaseEntity
    {
        private string name;
        [ColumnDetail(IsNotNull = true, Length = 100)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string email;
        [ColumnDetail(IsNotNull = true, Length = 100), EditFormFieldProps(Options = @"regEx:'^[\w-]+@([\w-]+\.)+[\w-]+$'")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string subject;
        [ColumnDetail(IsNotNull = true, Length = 100)]
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        private string message;
        [ColumnDetail(IsNotNull = true, ColumnType = DbType.Text)]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            string errorMessage = "";
            if (String.IsNullOrEmpty(this.name))
                errorMessage += Provider.GetResource("Please fill in the field name.") + "\n";
            if (String.IsNullOrEmpty(this.email))
                errorMessage += Provider.GetResource("Please fill in the field e-mail.") + "\n";
            if (String.IsNullOrEmpty(this.subject))
                errorMessage += Provider.GetResource("Please fill in the field subject.") + "\n";
            if (String.IsNullOrEmpty(this.message))
                errorMessage += Provider.GetResource("Please fill in the field message.") + "\n";

            if (errorMessage != "")
                throw new Exception(errorMessage);
        }
    }

}
