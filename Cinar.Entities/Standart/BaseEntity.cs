﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Cinar.Database;
using System.Collections;

namespace Cinar.Entities.Standart
{
    public class BaseEntity : IDatabaseEntity
    {
        public virtual void Initialize()
        {
            
        }

        [ColumnDetail(IsPrimaryKey=true, IsAutoIncrement=true, IsNotNull=true)]
        public int Id
        {
            get;
            set;
        }

        public DateTime InsertDate { get; set; }
        public string CreatedOn
        {
            get
            {
                return InsertDate.ToString("dd MMMM yyyy");
            }
        }

        public int InsertUserId { get; set; }
        public User CreatedBy {
            get {
                if (CinarContext.Db == null)
                    return null;
                return CinarContext.Db.Read<User>(InsertUserId);
            }
        }

        public bool Deleted { get; set; }

        string changes = null;

        public void BeforeSave()
        {
            if (CinarContext.Db == null)
                throw new Exception("Cannot reach context database");

            if (this.Id == 0)
            {
                InsertUserId = CinarContext.ClientUser.Id;
                InsertDate = DateTime.Now;
            }

            bool isNewEntity = this.Id == 0;

            if (this is ICriticalEntity && !isNewEntity)
            {
                CinarContext.Db.ClearEntityWebCache(this.GetType(), this.Id);
                IDatabaseEntity originalEntity = CinarContext.Db.Read(this.GetType(), this.Id);
                changes = originalEntity.CompareFields(this);
            }
        }

        public virtual void Save()
        {
            CinarContext.Db.Save(this);
        }
        public virtual void Delete()
        {
            if (CinarContext.Db == null)
                throw new Exception("Cannot reach context database");

            if (this is ICriticalEntity)
            {
                // EntityHistorytablosuna bu işlemi loglayalım
                EntityHistory eh = new EntityHistory()
                                       {
                                           Details = this.SerializeToString(),
                                           EntityName = this.GetType().Name,
                                           EntityId = this.Id,
                                           InsertDate = DateTime.Now,
                                           InsertUserId = CinarContext.ClientUser.Id,
                                           Operation = CRUDOperation.Delete
                                       };
                eh.Save();
            }

            CinarContext.Db.ExecuteNonQuery("delete from " + this.GetType().Name + " where Id={0}", this.Id);
        }

        public virtual string GetNameColumn()
        {
            return "Id";
        }
        public virtual string GetNameValue()
        {
            return Id.ToString();
        }
        private Hashtable ht = new Hashtable();
        public object this[string key]
        {
            get
            {
                return ht[key];
            }
            set
            {
                ht[key] = value;
            }
        }
        public Hashtable GetOriginalValues()
        {
            return ht;
        }

        /// <summary>
        /// Use this method such as: entity.SetFieldsByPostData(Request.Form);
        /// </summary>
        /// <param name="postData"></param>
        public virtual void SetFieldsByPostData(NameValueCollection postData)
        {
            for (int i = 0; i < postData.Count; i++)
            {
                PropertyInfo pi = this.GetType().GetProperty(postData.GetKey(i));
                if (pi == null || pi.GetSetMethod() == null) continue;

                string strVal = postData[i];

                if (pi.PropertyType == typeof(bool))
                {
                    if (strVal.ToLower() == "1") strVal = "True";
                    if (strVal.ToLower() == "0") strVal = "False";
                    if (strVal.ToLower() != "true") strVal = "False";
                }

                object val = null;
                try
                {
                    if (pi.PropertyType.IsEnum)
                        val = Enum.Parse(pi.PropertyType, strVal);
                    else
                        val = Convert.ChangeType(strVal, pi.PropertyType, CultureInfo.CurrentCulture);
                }
                catch
                {
                    throw new Exception(string.Format("The field {0} cannot have {1} as value.", this.GetType().Name + "." + pi.Name, strVal));
                }

                pi.SetValue(this, val, null);
            }
            for (int i = 0; i < postData.Count; i++)
            {
                string key = postData.GetKey(i);
                if (!this.ht.ContainsKey(key)) continue;
                this.ht[key] = postData[i];
            }
        }

        public void AfterSave(bool isUpdate)
        {
            if (this is ICriticalEntity)
            {
                // EntityHistory tablosuna bu işlemi loglayalım
                if (!isUpdate)
                {
                    EntityHistory eh = new EntityHistory()
                    {
                        Details = "",
                        EntityName = this.GetType().Name,
                        EntityId = this.Id,
                        InsertDate = DateTime.Now,
                        InsertUserId = CinarContext.ClientUser.Id,
                        Operation = CRUDOperation.Insert
                    };
                    eh.Save();
                }
                else
                {
                    if (!string.IsNullOrEmpty(changes))
                    {
                        EntityHistory eh = new EntityHistory()
                        {
                            Details = changes,
                            EntityName = this.GetType().Name,
                            EntityId = this.Id,
                            InsertDate = DateTime.Now,
                            InsertUserId = CinarContext.ClientUser.Id,
                            Operation = CRUDOperation.Update
                        };
                        eh.Save();
                    }
                }
            }
        }
    }
}
