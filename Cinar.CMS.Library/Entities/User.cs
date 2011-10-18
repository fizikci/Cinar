using System;
using System.Collections.Generic;
using System.Text;
using Cinar.Database;
using System.Web;
using System.Collections.Specialized;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [DefaultData(ColumnList = "Id, Email, Password, Roles, Nick, Keyword", ValueList = "1, 'root@local', '63A9F0EA7BB98050', 'User,Editor,Designer', 'Admin', 'jhrd74ghe63'")]
    [DefaultData(ColumnList = "Id, Email, Password, Roles, Nick, Keyword", ValueList = "2, 'editor', '63A9F0EA7BB98050', 'User,Editor', 'Editör', 'ge548rhe46e'")]
    [DefaultData(ColumnList = "Id, Email, Password, Roles, Nick, Keyword", ValueList = "3, 'anonim', '', '', 'Anonim', '63beyte674hge'")]
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Id, Email as [User.Email], Roles as [User.Roles], Visible as [BaseEntity.Visible] from [User]")]
    public class User : BaseEntity
    {
        private string email;
        [ColumnDetail(IsNotNull = true, Length = 100), EditFormFieldProps(Options = @"regEx:'^[\w-]+@([\w-]+\.)+[\w-]+$'")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string password = "";
        [ColumnDetail(IsNotNull = true, Length = 16), EditFormFieldProps(Options = "password:true,required:false")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string keyword;
        [ColumnDetail(Length = 16), EditFormFieldProps(Visible = false)]
        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }

        private string nick;
        [ColumnDetail(IsNotNull = true, Length = 100)]
        public string Nick
        {
            get { return nick; }
            set { nick = value; }
        }

        private string roles = "User";
        [ColumnDetail(IsNotNull = true, Length = 100)]
        public string Roles
        {
            get { return roles; }
            set { roles = value; }
        }

        #region kişisel
        private string name;
        [ColumnDetail(Length = 50)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string surname;
        [ColumnDetail(Length = 50)]
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        private string gender;
        [ColumnDetail(Length = 50)]
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        #endregion

        #region meslek
        private string occupation;
        [ColumnDetail(Length = 50)]
        public string Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }

        private string company;
        [ColumnDetail(Length = 100)]
        public string Company
        {
            get { return company; }
            set { company = value; }
        }

        private string department;
        [ColumnDetail(Length = 50)]
        public string Department
        {
            get { return department; }
            set { department = value; }
        }
        #endregion

        #region iletişim (tel, adres, web)
        private string phoneCell;
        [ColumnDetail(Length = 50)]
        public string PhoneCell
        {
            get { return phoneCell; }
            set { phoneCell = value; }
        }

        private string phoneWork;
        [ColumnDetail(Length = 50)]
        public string PhoneWork
        {
            get { return phoneWork; }
            set { phoneWork = value; }
        }

        private string phoneHome;
        [ColumnDetail(Length = 50)]
        public string PhoneHome
        {
            get { return phoneHome; }
            set { phoneHome = value; }
        }

        private string addressLine1;
        [ColumnDetail(Length = 200)]
        public string AddressLine1
        {
            get { return addressLine1; }
            set { addressLine1 = value; }
        }

        private string addressLine2;
        [ColumnDetail(Length = 200)]
        public string AddressLine2
        {
            get { return addressLine2; }
            set { addressLine2 = value; }
        }

        private string city;
        [ColumnDetail(Length = 50)]
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        private string country;
        [ColumnDetail(Length = 50)]
        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        private string zipCode;
        [ColumnDetail(Length = 5)]
        public string ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }

        private string web;
        [ColumnDetail(Length = 150), EditFormFieldProps(Options = @"regEx:'(((ht|f)tp(s?):\/\/)|(www\.[^ \[\]\(\)\n\r\t]+)|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})\/)([^ \[\]\(\),;&quot;\'&lt;&gt;\n\r\t]+)([^\. \[\]\(\),;&quot;\'&lt;&gt;\n\r\t])|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})'")]
        public string Web
        {
            get { return web; }
            set { web = value; }
        }
        #endregion

        #region diğer
        private string education;
        [ColumnDetail(Length = 50)]
        public string Education
        {
            get { return education; }
            set { education = value; }
        }

        private string certificates;
        [ColumnDetail(Length = 200)]
        public string Certificates
        {
            get { return certificates; }
            set { certificates = value; }
        }

        private string about;
        [ColumnDetail(Length = 200)]
        public string About
        {
            get { return about; }
            set { about = value; }
        }

        private string avatar;
        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit)]
        [PictureFieldProps(SpecialFolder = "avatarDir", SpecialNameField = "Nick", AddRandomNumber = false, UseYearMonthDayFolders = false)]
        public string Avatar
        {
            get { return avatar; }
            set { avatar = value; }
        }
        #endregion

        private int redirectCount = 0;
        public int RedirectCount
        {
            get { return redirectCount; }
            set { redirectCount = value; }
        }

        public bool IsAnonim() {
            return this.Email == "anonim";
        }

        public bool IsInRole(string role)
        {
            return this.Roles == role || this.Roles.Contains(role + ",") || this.Roles.Contains("," + role);
        }

        public override void SetFieldsByPostData(NameValueCollection postData)
        {
            string oldPasswordHash = this.Password; // eski şifre

            base.SetFieldsByPostData(postData); // formdan gelen verileri kaydedelim

            if (String.IsNullOrEmpty(this.Password) || this.Password.Trim() == "") // eğer formdan gelen şifre boşsa eski şifreyi koruyalım 
                this.Password = oldPasswordHash;
            else
                this.Password = Utility.MD5(this.Password);

            this["Password2"] = postData["Password2"];
            HttpPostedFile postedFile = Provider.Request.Files["Avatar"];
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                string avatarDir = Provider.AppSettings["avatarDir"];
                if (String.IsNullOrEmpty(avatarDir))
                    throw new Exception(Provider.GetResource("Avatar folder is not specified in config file."));
                if (!avatarDir.EndsWith("/")) avatarDir += "/";
                string avatarFilePath = Provider.Server.MapPath(avatarDir + this.Email + System.IO.Path.GetExtension(postedFile.FileName));
                Provider.Request.Files["Avatar"].SaveAs(avatarFilePath);
                this.avatar = System.IO.Path.GetFileName(avatarFilePath);
            }
        }

        public override List<string> Validate()
        {
            List<string> errorList = base.Validate();
            object password2 = this["Password2"];
            if (password2 == null || !Utility.MD5(password2.ToString()).Equals(this.Password))
                errorList.Add("Şifreler boş bırakılmamalı ve aynı olmalıdır.");
            return errorList;
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            if(!isUpdate)
            {
                //this.Password = Provider.MD5(this.Password); // password işi SetFieldsByPostData'da hallediliyor
                this.Visible = false;
                this.Keyword = Utility.MD5(DateTime.Now.Ticks.ToString());
            }
        }

        protected override void afterSave(bool isUpdate)
        {
            base.afterSave(isUpdate);

            if (!isUpdate)
            {
                string msg = String.Format(@"
                                Merhaba {0},<br/><br/>
                                Aşağıdaki linki kullanarak {1} üyeliğinizi aktif hale getirebilirsiniz:<br/><br/>
                                <a href=""http://{2}/LoginWithKeyword.ashx?keyword={3}"">http://{2}/LoginWithKeyword.ashx?keyword={3}</a>",
                                this.GetNameValue(),
                                Provider.Configuration.SiteName,
                                Provider.Configuration.SiteAddress,
                                this.Keyword);
                Provider.SendMail(this.Email, "Üyeliğinizi onaylayınız", msg);
            }

            if (this.Id == Provider.User.Id)
                Provider.User = this;
        }

        public override string GetNameValue()
        {
            return this.nick;
        }
        public override string GetNameColumn()
        {
            return "Nick";
        }

        public string GetAddress()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.addressLine1 + "<br/>");
            sb.AppendLine(this.addressLine2 + "<br/>");
            sb.AppendLine(this.City + " / " + this.Country);
            sb.AppendLine(" (" + this.zipCode + ")<br/>");

            return sb.ToString();
        }

        public override void Initialize()
        {
            base.Initialize();

            this.Password = "";
        }
    }
}
