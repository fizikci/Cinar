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
        [ColumnDetail(IsNotNull = true, Length = 100), EditFormFieldProps(Options = @"regEx:'^[\w-]+@([\w-]+\.)+[\w-]+$'")]
        public string Email { get; set; }

        [ColumnDetail(IsNotNull = true, Length = 16), EditFormFieldProps(Options = "password:true,required:false")]
        public string Password { get; set; }

        [ColumnDetail(Length = 16), EditFormFieldProps(Visible = false)]
        public string Keyword { get; set; }

        [ColumnDetail(IsNotNull = true, Length = 100)]
        public string Nick { get; set; }

        [ColumnDetail(IsNotNull = true, Length = 100)]
        public string Roles { get; set; }

        #region kişisel

        [ColumnDetail(Length = 50)]
        public string Name { get; set; }

        [ColumnDetail(Length = 50)]
        public string Surname { get; set; }

        [ColumnDetail(Length = 50)]
        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string IdentityNumber { get; set; }

        #endregion

        #region meslek

        [ColumnDetail(Length = 50)]
        public string Occupation { get; set; }

        [ColumnDetail(Length = 100)]
        public string Company { get; set; }

        [ColumnDetail(Length = 50)]
        public string Department { get; set; }

        #endregion

        #region iletişim (tel, adres, web)

        [ColumnDetail(Length = 50)]
        public string PhoneCell { get; set; }

        [ColumnDetail(Length = 50)]
        public string PhoneWork { get; set; }

        [ColumnDetail(Length = 50)]
        public string PhoneHome { get; set; }

        [ColumnDetail(Length = 200)]
        public string AddressLine1 { get; set; }

        [ColumnDetail(Length = 200)]
        public string AddressLine2 { get; set; }

        [ColumnDetail(Length = 50)]
        public string City { get; set; }

        [ColumnDetail(Length = 50)]
        public string Country { get; set; }

        [ColumnDetail(Length = 5)]
        public string ZipCode { get; set; }

        [ColumnDetail(Length = 150), EditFormFieldProps(Options = @"regEx:'(((ht|f)tp(s?):\/\/)|(www\.[^ \[\]\(\)\n\r\t]+)|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})\/)([^ \[\]\(\),;&quot;\'&lt;&gt;\n\r\t]+)([^\. \[\]\(\),;&quot;\'&lt;&gt;\n\r\t])|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})'")]
        public string Web { get; set; }

        #endregion

        #region diğer

        [ColumnDetail(Length = 50)]
        public string Education { get; set; }

        [ColumnDetail(Length = 200)]
        public string Certificates { get; set; }

        [ColumnDetail(Length = 200)]
        public string About { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(ControlType = ControlType.PictureEdit), PictureFieldProps(SpecialFolder = "avatarDir", SpecialNameField = "Nick", AddRandomNumber = false, UseYearMonthDayFolders = false)]
        public string Avatar { get; set; }

        #endregion

        public User()
        {
            RedirectCount = 0;
            Roles = "User";
            Password = "";
        }

        public int RedirectCount { get; set; }

        public bool IsAnonim() {
            return this.Email == "anonim";
        }

        public bool IsInRole(string role)
        {
            return this.Roles == role || this.Roles.Contains(role + ",") || this.Roles.Contains("," + role);
        }

        public override void SetFieldsByPostData(NameValueCollection postData)
        {
            string oldPasswordHash = this.Id > 0 ? Provider.Database.GetString("select Password from user where Id={0}", this.Id) : this.Password; // eski şifre

            base.SetFieldsByPostData(postData); // formdan gelen verileri kaydedelim

            if (String.IsNullOrWhiteSpace(this.Password)) // eğer formdan gelen şifre boşsa eski şifreyi koruyalım 
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
                string avatarUrlPath = avatarDir + this.Email + System.IO.Path.GetExtension(postedFile.FileName);
                string avatarFilePath = Provider.MapPath(avatarUrlPath);
                Provider.Request.Files["Avatar"].SaveAs(avatarFilePath);
                this.Avatar = avatarUrlPath;
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
            
            
            //todo:uncoment this

//            if (!isUpdate && !Provider.Request.Url.IsLoopback)
//            {
//                string msg = String.Format(@"
//                                Merhaba {0},<br/><br/>
//                                Aşağıdaki linki kullanarak {1} üyeliğinizi aktif hale getirebilirsiniz:<br/><br/>
//                                <a href=""http://{2}/LoginWithKeyword.ashx?keyword={3}"">http://{2}/LoginWithKeyword.ashx?keyword={3}</a>",
//                                this.GetNameValue(),
//                                Provider.Configuration.SiteName,
//                                Provider.Configuration.SiteAddress,
//                                this.Keyword);
//                Provider.SendMail(this.Email, "Üyeliğinizi onaylayınız", msg);
//            }

            if (this.Id == Provider.User.Id)
                Provider.User = this;
        }

        public override string GetNameValue()
        {
            return this.Nick;
        }
        public override string GetNameColumn()
        {
            return "Nick";
        }

        public string GetAddress()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.AddressLine1 + "<br/>");
            sb.AppendLine(this.AddressLine2 + "<br/>");
            sb.AppendLine(this.City + " / " + this.Country);
            sb.AppendLine(" (" + this.ZipCode + ")<br/>");

            return sb.ToString();
        }

        public override void Initialize()
        {
            base.Initialize();

            this.Password = "";
        }
    }
}
