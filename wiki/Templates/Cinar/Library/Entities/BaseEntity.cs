using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.Collections.Specialized;
using System.Collections;
using System.Web;
using Cinar.Database;
using $=util.Cap(table.Database.Name)$.Library;
using System.ComponentModel;

namespace $=util.Cap(table.Database.Name)$.Library.Entities
{
    [DataObject(true)]
    public abstract class BaseEntity : ObjectWithTags, IDatabaseEntity
    {
        public virtual void Initialize()
        {
        }

        #region fields
        private int id;
        private DateTime insertDate;
        private int insertUserId;
        private bool visible = true;
        #endregion

        [ColumnDetail(IsAutoIncrement=true, IsPrimaryKey=true, IsNotNull=true)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime InsertDate
        {
            get { return insertDate; }
            set { insertDate = value; }
        }

        [ColumnDetail(References = typeof(User))]
        public int InsertUserId
        {
            get { return insertUserId; }
            set { insertUserId = value; }
        }

        [ColumnDetail(IsNotNull = false, DefaultValue = "1")]
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public virtual void Validate()
        {
            Table tbl = Provider.Database.Tables[this.GetType().Name];

            if (tbl == null)
                tbl = Provider.Database.CreateTableForType(this.GetType());

            foreach (Column field in tbl.Columns)
            {
                object fieldValue = this.GetType().GetProperty(field.Name).GetValue(this, null);

                bool isEmpty = !field.IsNullable && (fieldValue == null || String.IsNullOrEmpty(fieldValue.ToString()));
                if (isEmpty)
                    throw new Exception(field.Name + " alanı boş bırakılamaz!");

                bool isOverflow = fieldValue != null && fieldValue is String && fieldValue.ToString().Length > field.Length;
                if (isOverflow)
                    throw new Exception(field.Name + " alanının değeri " + field.Length + " karakterden fazla olamaz!");
            }
        }

        public virtual void Save(bool ignoreValidation)
        {
            try
            {
                Provider.Database.Begin();

                bool savedBefore = (Id > 0);

                if (!savedBefore)
                {
                    this.InsertDate = DateTime.Now;
                    this.InsertUserId = Provider.User.Id;
                }
                else
                {
                    History hist = new History();
                    hist.EntityName = this.GetType().Name;
                    hist.EntityId = this.Id;
                    hist.Changes = this.GetChanges();
                    hist.Save();
                }

                if(!ignoreValidation)
                    this.Validate();

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

        public virtual void Save()
        {
            this.Save(false);
        }

        private string GetChanges()
        {
            StringBuilder sb = new StringBuilder();

            Hashtable originals = this.GetOriginalValues();
            foreach (string key in originals.Keys)
            {
                PropertyInfo pi = this.GetType().GetProperty(key);
                if (pi != null)
                {
                    object newVal = pi.GetValue(this, null);
                    object oldVal = originals[key];

                    if (newVal!=null && newVal.Equals(oldVal)) continue; //***

                    if (pi.PropertyType.IsEnum)
                        oldVal = oldVal!=null ? Utility.IntToEnum(pi.PropertyType, (int)oldVal) : Enum.GetValues(pi.PropertyType).GetValue(0);
                    string strNewVal = newVal != null ? newVal.ToString() : "";
                    string strOldVal = oldVal != null ? oldVal.ToString() : "";
                    if(strOldVal!=strNewVal)
                        sb.AppendFormat("{0} : ({1}) => ({2})" + Environment.NewLine, 
                            key, 
                            strOldVal, 
                            strNewVal);
                }
            }

            return sb.ToString();
        }
        protected virtual void beforeSave(bool isUpdate) { }
        protected virtual void afterSave(bool isUpdate) { }

        public static T Read<T>(int id)
        {
            return (T)Provider.Database.Read(typeof(T), id);
        }
        public void Read()
        {
            if (this.Id <= 0)
                throw new Exception("Entity id must be greater than zero");

            Provider.Database.FillEntity(this);
        }

        public void Delete()
        {
            try
            {
                Provider.Database.Begin();

                this.beforeDelete();

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


        #region IDatabaseEntity Members
        public virtual string GetNameColumn()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual string GetNameValue()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

    }
}
