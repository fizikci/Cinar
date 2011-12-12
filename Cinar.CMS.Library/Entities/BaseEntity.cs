using System;
using System.Collections.Generic;
using Cinar.Database;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    public abstract class BaseEntity : ObjectWithTags, IDatabaseEntity
    {
        #region fields

        #endregion

        public virtual void Initialize()
        {
        }

        [ColumnDetail(IsAutoIncrement = true, IsNotNull = true, IsPrimaryKey = true), EditFormFieldProps(Visible = false)]
        public int Id { get; set; }

        [ColumnDetail(IsNotNull = true, DefaultValue="1990-01-01"), EditFormFieldProps(Visible = false)]
        public DateTime InsertDate { get; set; }

        [ColumnDetail(References = typeof(User)), EditFormFieldProps(Visible = false)]
        public int InsertUserId { get; set; }

        private User _insertUser;
        [XmlIgnore]
        public User InsertUser
        {
            get { return _insertUser ?? (_insertUser = (User) Provider.Database.Read(typeof (User), this.InsertUserId)); }
        }

        [EditFormFieldProps(Visible = false)]
        public DateTime UpdateDate { get; set; }

        [ColumnDetail(References = typeof(User)), EditFormFieldProps(Visible = false)]
        public int UpdateUserId { get; set; }

        private User _updateUser;

        public BaseEntity()
        {
            Visible = true;
        }

        [XmlIgnore]
        public User UpdateUser
        {
            get { return _updateUser ?? (_updateUser = (User) Provider.Database.Read(typeof (User), this.UpdateUserId)); }
        }

        [ColumnDetail(IsNotNull = true, DefaultValue = "1")]
        public bool Visible { get; set; }

        public int OrderNo { get; set; }

        public virtual string GetNameValue()
        {
            throw new Exception(Provider.GetResource("Please override the method GetNameValue() for the entity {0}", this.GetType().Name));
        }
        public virtual string GetNameColumn()
        {
            throw new Exception(Provider.GetResource("Please override the method GetNameField() for the entity {0}", this.GetType().Name));
        }

        public string GetPropertyEditorJSON()
        {
            return Provider.GetPropertyEditorJSON(this, false);
        }

        public virtual List<string> Validate()
        {
            List<string> errorList = new List<string>();

            Table tbl = Provider.Database.Tables[this.GetType().Name];

            foreach (Column field in tbl.Columns)
            {
                object fieldValue = this.GetType().GetProperty(field.Name).GetValue(this, null);

                bool isEmpty = !field.IsNullable && (fieldValue == null || String.IsNullOrWhiteSpace(fieldValue.ToString()));
                if (isEmpty)
                    errorList.Add(field.Name + " alanı boş bırakılamaz!");

                bool isOverflow = fieldValue != null && fieldValue is String && fieldValue.ToString().Length > field.Length;
                if (isOverflow)
                    errorList.Add(field.Name + " alanının değeri " + field.Length + " karakterden fazla olamaz!");
            }

            return errorList;
        }

        public void Save()
        {
            try
            {
                Provider.Database.Begin();

                bool savedBefore = (Id > 0);

                this.UpdateDate = DateTime.Now;
                this.UpdateUserId = Provider.User.Id;
                if (!savedBefore)
                {
                    this.InsertDate = DateTime.Now;
                    this.InsertUserId = Provider.User.Id;
                }

                this.beforeSave(savedBefore);
                Provider.Database.Save(this);
                this.afterSave(savedBefore);

                Provider.Database.Commit();
            }
            catch (Exception ex)
            {
                Provider.Database.Rollback();
                throw ex;
            }
        }
        protected virtual void beforeSave(bool isUpdate) { }
        protected virtual void afterSave(bool isUpdate) { }


        public void Delete()
        {
            try
            {
                Provider.Database.Begin();

                this.beforeDelete();

                // dil tablolarında data varsa silelim
                if(Provider.Database.Tables[this.GetType().Name+"Lang"]!=null)
                    Provider.Database.ExecuteNonQuery("delete from [" + this.GetType().Name + "Lang] where " + this.GetType().Name + "Id=" + this.Id);

                // entitinin kendisini silelim
                Provider.Database.ExecuteNonQuery("delete from ["+this.GetType().Name+"] where Id=" + this.Id);
                
                this.afterDelete();

                Provider.Database.Commit();
            }
            catch (Exception ex)
            {
                Provider.Database.Rollback();
                throw ex;
            }
        }
        protected virtual void beforeDelete() { }
        protected virtual void afterDelete() { }
    }
}
