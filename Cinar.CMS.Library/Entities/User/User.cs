﻿using System;
using System.Collections.Generic;
using System.Text;
using Cinar.Database;
using System.Web;
using System.Collections.Specialized;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
//using System.IO;

namespace Cinar.CMS.Library.Entities
{
    [Serializable]
    [DefaultData(ColumnList = "Email, Password, Roles, Nick, Keyword", ValueList = "'root@local', '63A9F0EA7BB98050', 'User,Editor,Designer', 'admin', 'jhrd74ghe63'")]
    [DefaultData(ColumnList = "Email, Password, Roles, Nick, Keyword", ValueList = "'editor', '63A9F0EA7BB98050', 'User,Editor', 'editor', 'ge548rhe46e'")]
    [DefaultData(ColumnList = "Email, Password, Roles, Nick, Keyword", ValueList = "'anonim', '', '', 'anonim', '63beyte674hge'")]
    [ListFormProps(VisibleAtMainMenu = true, QuerySelect = "select Id, Email, InsertDate, Roles, Visible from [User]")]
    public class User : BaseEntity
    {
        [ColumnDetail(IsNotNull = true, Length = 100, IsUnique = true), EditFormFieldProps(Category="Login", Options = @"regEx:'^[\w-]+@([\w-]+\.)+[\w-]+$'")]
        public string Email { get; set; }

        [ColumnDetail(IsNotNull = true, Length = 16), EditFormFieldProps(Category = "Login", Options = "password:true,required:false")]
        public string Password { get; set; }

        [ColumnDetail(Length = 16), EditFormFieldProps(Visible = false)]
        public string Keyword { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(Category = "Login")]
        public string Nick { get; set; }

        [ColumnDetail(IsNotNull = true, Length = 100), EditFormFieldProps(Category = "Login", ControlType = ControlType.TagEdit, Options = "items:['User','Editor','Designer']")]
        public string Roles { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(Visible=false)]
        public string FacebookId { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(Visible = false)]
        public string GoogleId { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(Visible = false)]
        public string YahooId { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(Visible = false)]
        public string MsnId { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(Visible = false)]
        public string LinkedinId { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(Visible = false)]
        public string TwitterId { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(Visible = false)]
        public string MyspaceId { get; set; }

        #region kişisel

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Personal")]
        public string Name { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Personal")]
        public string Surname { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(Category = "Personal", ControlType = ControlType.PictureEdit), PictureFieldProps(SpecialFolder = "avatarDir", SpecialNameField = "Nick", AddRandomNumber = false, UseYearMonthDayFolders = false)]
        public string Avatar { get; set; }

        public string FullName
        {
            get { return string.IsNullOrWhiteSpace(Name + Surname) ? (string.IsNullOrWhiteSpace(Nick) ? Email.Split('@')[0] : Nick) : (Name + " " + Surname); }
        }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Personal")]
        public string Gender { get; set; }

        [EditFormFieldProps(Category = "Personal")]
        public DateTime BirthDate { get; set; }

        [EditFormFieldProps(Category = "Personal")]
        public string IdentityNumber { get; set; }

        #endregion

        #region iletişim (tel, adres, web)

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Contact")]
        public string PhoneCell { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Contact")]
        public string PhoneWork { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Contact")]
        public string PhoneHome { get; set; }

        [ColumnDetail(Length = 200), EditFormFieldProps(Category = "Contact")]
        public string AddressLine1 { get; set; }

        [ColumnDetail(Length = 200), EditFormFieldProps(Category = "Contact")]
        public string AddressLine2 { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Contact")]
        public string City { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Contact")]
        public string Country { get; set; }

        [ColumnDetail(Length = 5), EditFormFieldProps(Category = "Contact")]
        public string ZipCode { get; set; }

        [ColumnDetail(Length = 150), EditFormFieldProps(Category = "Contact", Options = @"regEx:'(((ht|f)tp(s?):\/\/)|(www\.[^ \[\]\(\)\n\r\t]+)|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})\/)([^ \[\]\(\),;&quot;\'&lt;&gt;\n\r\t]+)([^\. \[\]\(\),;&quot;\'&lt;&gt;\n\r\t])|(([012]?[0-9]{1,2}\.){3}[012]?[0-9]{1,2})'")]
        public string Web { get; set; }

        #endregion

        #region other

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Other")]
        public string Occupation { get; set; }

        [ColumnDetail(Length = 100), EditFormFieldProps(Category = "Other")]
        public string Company { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Other")]
        public string Department { get; set; }

        [ColumnDetail(Length = 50), EditFormFieldProps(Category = "Other")]
        public string Education { get; set; }

        [ColumnDetail(Length = 200), EditFormFieldProps(Category = "Other")]
        public string Certificates { get; set; }

        [ColumnDetail(Length = 200), EditFormFieldProps(Category = "Other")]
        public string About { get; set; }

        #endregion

        public User()
        {
            RedirectCount = 0;
            Roles = "User";
            Password = "";
            FacebookId = GoogleId = YahooId = MsnId = LinkedinId = TwitterId = MyspaceId = "0";

        }

        [EditFormFieldProps(Visible = false)]
        public int RedirectCount { get; set; }

        public int ContactCount { get; set; }

        public bool IsAnonim() {
            return this.Email == "anonim";
        }

        public bool IsInRole(string role)
        {
            return string.IsNullOrWhiteSpace(role) || this.Roles == role || this.Roles.Contains(role + ",") || this.Roles.Contains("," + role);
        }

        public override void SetFieldsByPostData(NameValueCollection postData)
        {
            string oldPasswordHash = this.Id > 0 ? Provider.Database.GetString("select [Password] from [User] where Id={0}", this.Id) : this.Password; // eski şifre

            base.SetFieldsByPostData(postData); // formdan gelen verileri kaydedelim

            if (String.IsNullOrWhiteSpace(this.Password)) // eğer formdan gelen şifre boşsa eski şifreyi koruyalım 
                this.Password = oldPasswordHash;
            else
                this.Password = CMSUtility.MD5(this.Password);

            this["Password2"] = postData["Password2"];
            HttpPostedFile postedFile = Provider.Request.Files["Avatar"];
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                string avatarDir = Provider.AppSettings["avatarDir"];
                if (String.IsNullOrEmpty(avatarDir))
                    throw new Exception(Provider.GetResource("Avatar folder is not specified in config file."));
                if (!avatarDir.EndsWith("/")) avatarDir += "/";
                string avatarUrlPath = avatarDir + this.Nick.MakeFileName() + DateTime.Now.Millisecond + Path.GetExtension(postedFile.FileName);

                Image bmp = Image.FromStream(Provider.Request.Files["Avatar"].InputStream);
                if (bmp.Width > 240)
                {
                    Image bmp2 = bmp.ScaleImage(240, 0);
                    avatarUrlPath = avatarUrlPath.Substring(0, avatarUrlPath.LastIndexOf('.')) + ".jpg";
                    bmp2.SaveJpeg(Provider.MapPath(avatarUrlPath), Provider.Configuration.ThumbQuality);
                }
                else
                    Provider.Request.Files["Avatar"].SaveAs(Provider.MapPath(avatarUrlPath));


                this.Avatar = avatarUrlPath;

                Provider.DeleteThumbFiles(avatarUrlPath);
            }
        }

        public override List<string> Validate()
        {
            List<string> errorList = base.Validate();
            object password2 = this["Password2"];
            if (password2 == null || !CMSUtility.MD5(password2.ToString()).Equals(this.Password))
                errorList.Add("Şifreler boş bırakılmamalı ve aynı olmalıdır.");
            return errorList;
        }

        public override void BeforeSave()
        {
            base.BeforeSave();

            this.Name = this.Name.Capitalize();
            this.Surname = this.Surname.Capitalize();

            if (this.Nick!=null && !Regex.IsMatch(this.Nick, "^[a-zA-Z0-9_]+$"))
                throw new Exception(Provider.TR("Kullanıcı adı sadece harf ve rakamlardan oluşabilir"));

            if (Id==0)
            {
                //this.Password = Provider.MD5(this.Password); // password işi SetFieldsByPostData'da hallediliyor
                this.Visible = false;
                this.Keyword = CMSUtility.MD5((this.Nick ?? "")+DateTime.Now.Ticks.ToString());

                if (string.IsNullOrWhiteSpace(this.Country) && Provider.Request.UserLanguages != null && Provider.Request.UserLanguages.Length > 0)
                    this.Country = Provider.Request.UserLanguages[0];
            }
            else {
                //this.Visible = true; // böyle saçma şey olur mu lan? kim yazmış bunu!
                if (string.IsNullOrWhiteSpace(this.Password))
                    this.Password = Provider.Database.GetString("select Password from User where Id={0}", this.Id);
            }

            downloadPictureForFieldsThatStartsWithHttp();
        }

        public override void AfterSave(bool isUpdate)
        {
            base.AfterSave(isUpdate);
            
            
            if (!isUpdate)// && !Provider.Request.Url.IsLoopback && Provider.Session["DontSendEmail"]!=null)
            {
                string msg = String.Format(@"
                                Hi {0},<br/><br/>
                                Please activate your {1} membership by following the link below:<br/><br/>
                                <a href=""http://{2}/LoginWithKeyword.ashx?keyword={3}"">http://{2}/LoginWithKeyword.ashx?keyword={3}</a>",
                                this.GetNameValue(),
                                Provider.Configuration.SiteName,
                                Provider.Configuration.SiteAddress,
                                this.Keyword);
                Provider.SendMail(this.Email, "Membership Confirmation", msg);

                // add admin (root) as first contact to this user
                //new UserContact
                //{
                //    UserId = Provider.Database.GetInt("select Id from User where Nick={0}", "admin"),
                //    InsertUserId = this.Id
                //}.Save();
            }

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

        public string GetThumbPicture(int width, int height, bool cropPicture)
        {
             return Provider.GetThumbPath(this.Avatar, width, height, cropPicture);
        }

        protected override bool beforeDelete()
        {
            bool res = base.beforeDelete();
            if (res)
            {
                List<ReportedUser> reports = Provider.Database.ReadList<ReportedUser>(FilterExpression.Where("UserId", CriteriaTypes.Eq, this.Id));
                if(reports!=null)
                    foreach (var report in reports)
                        report.Delete();

                List<UserContact> contacts = Provider.Database.ReadList<UserContact>(FilterExpression.Where("UserId", CriteriaTypes.Eq, this.Id));
                if (contacts != null)
                    foreach (var contact in contacts)
                        contact.Delete();

                UserSettings settings = Provider.Database.Read<UserSettings>("UserId={0}", this.Id);
                if (settings != null)
                    settings.Delete();

            }
            return true;
        }

        private UserSettings us;
        public UserSettings Settings
        {
            get
            {
                if (us == null)
                {
                    us = Provider.Database.Read<UserSettings>("UserId={0}", this.Id);
                    if (us == null)
                    {
                        us = new UserSettings { UserId = this.Id };
                        us.LastNotificationCheck = this.InsertDate;
                        us.Save();
                    }
                }
                return us;
            }
        }

        public bool HasContact(int userId)
        {
            return Provider.Database.GetInt("select count(*) from UserContact where UserId={0} AND InsertUserId={1}", userId, Provider.User.Id) > 0;
        }
    }
}
