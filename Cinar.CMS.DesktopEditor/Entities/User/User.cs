using System;
using System.Collections.Generic;
using System.Text;
using Cinar.Database;
using System.Web;
using System.Collections.Specialized;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
//using System.IO;

namespace Cinar.CMS.DesktopEditor.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Keyword { get; set; }

        public string Nick { get; set; }

        public string Roles { get; set; }

        public string FacebookId { get; set; }

        public string GoogleId { get; set; }

        public string YahooId { get; set; }

        public string MsnId { get; set; }

        public string LinkedinId { get; set; }

        public string TwitterId { get; set; }

        public string MyspaceId { get; set; }

        #region kişisel

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Avatar { get; set; }

        public string FullName
        {
            get { return string.IsNullOrWhiteSpace(Name + Surname) ? (string.IsNullOrWhiteSpace(Nick) ? Email : Nick) : (Name + " " + Surname); }
        }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string IdentityNumber { get; set; }

        #endregion

        #region iletişim (tel, adres, web)

        public string PhoneCell { get; set; }

        public string PhoneWork { get; set; }

        public string PhoneHome { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public string Web { get; set; }

        #endregion

        #region other

        public string Occupation { get; set; }

        public string Company { get; set; }

        public string Department { get; set; }

        public string Education { get; set; }

        public string Certificates { get; set; }

        public string About { get; set; }

        #endregion

        public int RedirectCount { get; set; }

        public int ContactCount { get; set; }

        public bool IsAnonim() {
            return this.Email == "anonim";
        }

    }
}
