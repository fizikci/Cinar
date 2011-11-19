using System;
using System.Text;
using Cinar.CMS.Library.Entities;
using Cinar.Database;
using System.Collections;
using System.Text.RegularExpressions;

namespace Cinar.CMS.Library.Modules
{
    public abstract class AuthorSourceDetailBase : Module
    {
        protected int pictureWidth = 0;
        public int PictureWidth
        {
            get { return pictureWidth; }
            set { pictureWidth = value; }
        }

        protected int pictureHeight = 0;
        public int PictureHeight
        {
            get { return pictureHeight; }
            set { pictureHeight = value; }
        }
        public bool CropPicture { get; set; }

        private static string defaultFieldOrder = "image,name,description,email,web,adres,telWork,telCell";
        private string fieldOrder = AuthorDetail.defaultFieldOrder;
        [ColumnDetail(IsNotNull = true), EditFormFieldProps(ControlType = ControlType.MemoEdit)]
        public string FieldOrder
        {
            get { return fieldOrder; }
            set { fieldOrder = value; }
        }

        public abstract UserRelatedEntity Entity { get; }


        protected override string show()
        {
            User user = (User)Provider.Database.Read(typeof(User), Entity.UserId);

            StringBuilder sb = new StringBuilder();

            Hashtable fields = new Hashtable();

            if (Entity.Picture.Trim() == "")
                fields["image"] = Provider.DesignMode ? "Resim yok!!!" : "";
            else
                fields["image"] = this.getImgHTML();

            if(Entity.Name.Trim()=="")
                fields["name"] = Provider.DesignMode ? "<div class=\"name\">İsim yok!!!</div>" : "";
            else
                fields["name"] = "<div class=\"name\">" + Entity.Name + "</div>";

            if(Entity.Description.Trim()=="")
                fields["description"] = Provider.DesignMode ? "<div class=\"desc\">Açıklama yok!!!</div>" : "";
            else
                fields["description"] = "<div class=\"desc\">" + Entity.Description + "</div>";
            
            if (user != null)
            {
                fields["email"] = String.Format("<div class=\"email\"><a href=\"mailto:{0}\" title=\"{1}\">{0}</a></div>", user.Email, Provider.GetModuleResource("To send email"));
                
                if (user.Web.Trim() == "" || user.Web.Trim() == "http://")
                    fields["web"] = Provider.DesignMode ? "<div class=\"web\"><a href=\"javascript:;\">Web adresi girilmemiş!!!</a></div>" : "";
                else
                    fields["web"] = String.Format("<div class=\"web\"><a href=\"{0}\" target=\"_blank\">{1}</a></div>", "Redirect.ashx?uid="+user.Id, user.Web);
                
                if (user.AddressLine1.Trim()=="")
                    fields["adres"] = Provider.DesignMode ? "<div class=\"adres\">Adres girilmemiş!!!</div>" : "";
                else
                    fields["adres"] = "<div class=\"adres\">" + user.AddressLine1 + "</div>";

                if (user.PhoneWork.Trim() == "")
                    fields["telWork"] = Provider.DesignMode ? "<div class=\"telWork\">İş Tel girilmemiş!!!</div>" : "";
                else
                    fields["telWork"] = String.Format("<div class=\"{0}\">{1}</div>", "telWork", user.PhoneWork);
                
                if(user.PhoneCell.Trim()=="")
                    fields["telCell"] = Provider.DesignMode ? "<div class=\"telCell\">Cep Tel girilmemiş!!!</div>" : "";
                else
                    fields["telCell"] = String.Format("<div class=\"{0}\">{1}</div>", "telCell", user.PhoneCell);
            }

            foreach (string fieldName in this.fieldOrder.Split(','))
            {
                if (fields.ContainsKey(fieldName))
                    sb.Append(fields[fieldName]);
            }

            return sb.ToString();
        }

        private string getImgHTML()
        {
            if (String.IsNullOrEmpty(Entity.Picture))
                return Provider.GetResource("There is no related picture");

            return Provider.GetThumbImgHTML(Entity.Picture, this.pictureWidth, this.pictureHeight, Entity.Name, null, null, CropPicture);
        }

        public override string GetDefaultCSS()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetDefaultCSS());
            sb.AppendFormat("#{0}_{1} img {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.name {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.email {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.desc {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.web {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.adres {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.telWork {{}}\n", this.Name, this.Id);
            sb.AppendFormat("#{0}_{1} div.telCell {{}}\n", this.Name, this.Id);
            return sb.ToString();
        }

        protected override void beforeSave(bool isUpdate)
        {
            base.beforeSave(isUpdate);

            if (Regex.Match(this.fieldOrder, "[^\\w,\\,]").Success)
                throw new Exception(Provider.GetResource("FieldOrder is invalid. Please enter fields as {0}", AuthorSourceDetailBase.defaultFieldOrder));
            foreach (string fieldName in this.fieldOrder.Split(','))
                if (AuthorSourceDetailBase.defaultFieldOrder.IndexOf(fieldName) == -1)
                    throw new Exception(Provider.GetResource("{0} is not a valid field name. Please use one of the following: {1}", fieldName, AuthorSourceDetailBase.defaultFieldOrder));
        }
    }

}
