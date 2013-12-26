using System;
using System.Collections.Generic;
using Cinar.Database;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Cinar.CMS.Library.Entities
{
    public abstract class BaseEntity : ObjectWithTags, IDatabaseEntity
    {
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

        [Description("Returns the JSON for the EditForm of this entity.")]
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

        [Description("Saves this entity to database (if Id>0 updates, else inserts)")]
        public void Save()
        {
             Provider.Database.Save(this);
        }

        public virtual void BeforeSave()
        {
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = Provider.User.Id;
            if (Id==0)
            {
                this.InsertDate = DateTime.Now;
                this.InsertUserId = Provider.User.Id;
            }

            #region if critical entity, log it
            if (this is ICriticalEntity)
            {
                if (Id>0)
                {
                    Provider.Database.ClearEntityWebCache(this.GetType(), this.Id);
                    IDatabaseEntity originalEntity = Provider.Database.Read(this.GetType(), this.Id);
                    string changes = originalEntity.CompareFields(this);

                    Provider.Log("History_" + this.GetType().Name, "Update", changes);
                }
                else
                {
                    Provider.Log("History_" + this.GetType().Name, "Insert", this.SerializeToString());
                }
            }
            #endregion

        }
        public virtual void AfterSave(bool isUpdate) { }

        [Description("Deletes this entity")]
        public void Delete()
        {
            try
            {
                Provider.Database.Begin();

                if (this.beforeDelete())
                {

                    // dil tablolarında data varsa silelim
                    if (Provider.Database.Tables[this.GetType().Name + "Lang"] != null)
                        Provider.Database.ExecuteNonQuery("delete from [" + this.GetType().Name + "Lang] where " + this.GetType().Name + "Id=" + this.Id);

                    // entitinin kendisini silelim
                    Provider.Database.ExecuteNonQuery("delete from [" + this.GetType().Name + "] where Id=" + this.Id);

                    this.afterDelete();

                    Provider.Database.ClearEntityWebCache(this.GetType(), this.Id);

                    if (this is ICriticalEntity)
                        Provider.Log("History_" + this.GetType().Name, "Delete", this.SerializeToString());
                }

                Provider.Database.Commit();
            }
            catch (Exception ex)
            {
                Provider.Database.Rollback();
                throw ex;
            }
        }
        protected virtual bool beforeDelete() { return true; }
        protected virtual void afterDelete() { }
    }
}
