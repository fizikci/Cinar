using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using Cinar.Database;

namespace $=db.Name$.API.Entity
{
    public abstract class BaseEntity : BaseDbObject
    {
        public int InsertUserId { get; set; }
        public DateTime InsertDate { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }//enum Statu

        public override void Save()
        {
            Validate();

            if (this.Id == 0)
            {
                this.InsertDate = DateTime.Now;
                this.InsertUserId = 1;// Provider.CurrentUser.Id;
            }
            else
            {
                this.UpdateDate = DateTime.Now;
                this.UpdateUserId = 1;// Provider.CurrentUser.Id;
            }
            Provider.Database.Save(this);
        }

        public override void Delete()
        {
            Provider.Database.Delete(this);
        }
    }

    public abstract class BaseDbObject : IDatabaseEntity
    {
        [ColumnDetail(IsAutoIncrement = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        public virtual void Validate()
        {
        }

        public virtual void Save()
        {
            Validate();
            Provider.Database.Save(this);
        }

        public virtual void Delete()
        {
            Provider.Database.Delete(this);
        }


        public virtual void Initialize()
        {

        }

        public virtual void BeforeSave()
        {
        }

        public virtual void AfterSave(bool isUpdate)
        {
        }


        public virtual string GetNameColumn()
        {
            return "";
        }
        public virtual string GetNameValue()
        {
            return "";
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

    }
}