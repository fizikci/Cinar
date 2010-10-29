using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinar.Database;
using System.Collections;

namespace Cinar.Entities
{
    public class BaseEntity : IDatabaseEntity
    {
        public BaseEntity() 
        {
            InsertDate = DateTime.Now;
        }

        public virtual void Initialize()
        {
            
        }

        [FieldDetail(IsPrimaryKey=true, IsAutoIncrement=true, IsNotNull=true)]
        public int Id
        {
            get;
            set;
        }

        public DateTime InsertDate { get; set; }

        public int InsertUserId { get; set; }

        public bool Deleted { get; set; }

        public void Save()
        {
            if (Context.Db == null)
                throw new Exception("Cannot reach context database");

            if (this.Id == 0)
                InsertUserId = Context.ClientUser.Id;

            Context.Db.Save(this);
        }
        public void Delete()
        {
            if (Context.Db == null)
                throw new Exception("Cannot reach context database");

            Context.Db.ExecuteNonQuery("delete from " + this.GetType().Name + " where Id={0}", this.Id);
        }

        public virtual string GetNameField()
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
    }
}
