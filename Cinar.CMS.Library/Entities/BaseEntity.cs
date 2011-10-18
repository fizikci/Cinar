using System;
using System.Collections.Generic;
using Cinar.Database;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    public abstract class BaseEntity : ObjectWithTags, IDatabaseEntity
    {
        #region fields
        private int id;
        private DateTime insertDate;
        private int insertUserId;
        private DateTime updateDate;
        private int updateUserId;
        private bool visible = true;
        private int orderNo;
        #endregion

        public virtual void Initialize()
        {
        }

        [ColumnDetail(IsAutoIncrement = true, IsNotNull = true, IsPrimaryKey = true), EditFormFieldProps(Visible = false)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [ColumnDetail(IsNotNull = true, DefaultValue="1990-01-01"), EditFormFieldProps(Visible = false)]
        public DateTime InsertDate
        {
            get { return insertDate; }
            set { insertDate = value; }
        }

        [ColumnDetail(References = typeof(User)), EditFormFieldProps(Visible = false)]
        public int InsertUserId
        {
            get { return insertUserId; }
            set { insertUserId = value; }
        }

        private User _insertUser;
        [XmlIgnore]
        public User InsertUser
        {
            get
            {
                if (_insertUser == null)
                    _insertUser = (User)Provider.Database.Read(typeof(User), this.InsertUserId);
                return _insertUser;
            }
        }

        [EditFormFieldProps(Visible = false)]
        public DateTime UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }

        [ColumnDetail(References = typeof(User)), EditFormFieldProps(Visible = false)]
        public int UpdateUserId
        {
            get { return updateUserId; }
            set { updateUserId = value; }
        }

        private User _updateUser;
        [XmlIgnore]
        public User UpdateUser
        {
            get
            {
                if (_updateUser == null)
                    _updateUser = (User)Provider.Database.Read(typeof(User), this.UpdateUserId);
                return _updateUser;
            }
        }

        [ColumnDetail(IsNotNull = true, DefaultValue = "1")]
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public int OrderNo
        {
            get { return orderNo; }
            set { orderNo = value; }
        }

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

                bool isEmpty = !field.IsNullable && (fieldValue == null || String.IsNullOrEmpty(fieldValue.ToString()));
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
