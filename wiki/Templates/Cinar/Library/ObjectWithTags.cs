using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.Collections.Specialized;
using System.Collections;
using System.Web;

namespace $=util.Cap(table.Database.Name)$.Library
{
    public abstract class ObjectWithTags
    {
        private Hashtable tags = new Hashtable();

        public object this[string key]
        {
            get { return tags[key]; }
            set
            {
                tags[key] = value;
            }
        }

        public bool ContainsKey(string key)
        {
            return tags.ContainsKey(key);
        }

        private Hashtable originalValues = new Hashtable();
        public Hashtable GetOriginalValues()
        {
            return originalValues;
        }
    }
}
