using Cinar.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cinar.QueueJobs.Entities
{
    public abstract class BaseEntity : IDatabaseEntity
    {
        [ColumnDetail(IsAutoIncrement = true, IsPrimaryKey = true)]
        public int Id { get; set; }

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
