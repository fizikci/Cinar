using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.Database
{
    public class DatabaseEntity : IDatabaseEntity
    {
        public virtual void Initialize()
        {
        }

        public int Id
        {
            get;
            set;
        }

        public virtual string GetNameColumn()
        {
            return "";
        }

        public virtual string GetNameValue()
        {
            return "";
        }

        Hashtable ht = null;

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

        public System.Collections.Hashtable GetOriginalValues()
        {
            if (ht == null)
                ht = new Hashtable();
            return ht;
        }
    }
}
