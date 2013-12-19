using System;
using System.Collections.Generic;
using Cinar.Database;
using System.Xml.Serialization;

namespace Cinar.CMS.Library.Entities
{
    public abstract class SimpleBaseEntity : ObjectWithTags, IDatabaseEntity
    {

        public virtual void Initialize()
        {
        }

        [ColumnDetail(IsAutoIncrement = true, IsNotNull = true, IsPrimaryKey = true), EditFormFieldProps(Visible = false)]
        public int Id { get; set; }

        [ColumnDetail(IsNotNull = true, DefaultValue = "1990-01-01"), EditFormFieldProps(Visible = false)]
        public DateTime InsertDate { get; set; }


        public SimpleBaseEntity()
        {
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
            Provider.Database.Save(this);
        }
        public virtual void BeforeSave()
        {
            if (this.Id==0)
                this.InsertDate = DateTime.Now;

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
        public virtual void AfterSave()
        {
            
        }


        public void Delete()
        {
            try
            {
                Provider.Database.Begin();

                if (this.beforeDelete())
                {

                    // entitinin kendisini silelim
                    Provider.Database.ExecuteNonQuery("delete from [" + this.GetType().Name + "] where Id=" + this.Id);

                    this.afterDelete();

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
