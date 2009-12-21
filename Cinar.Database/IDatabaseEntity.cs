using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Cinar.Database
{
    /// <summary>
    /// An interface for storable entities.
    /// </summary>
    public interface IDatabaseEntity
    {
        void Initialize();
        int Id
        {
            get;
            set;
        }

        string GetNameField();
        string GetNameValue();

        object this[string key]
        {
            get;
            set;
        }

        Hashtable GetOriginalValues();
    }
}
