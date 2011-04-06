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

    /// <summary>
    /// Inherited classes mapped to the table which is mapped to the base class.
    /// Base class implements this interface.
    /// </summary>
    public interface ISerializeInheritedFields
    {
        [FieldDetail(IsNotNull = true, Length = 100)]
        string TypeName { get; set; }

        [FieldDetail(FieldType = Cinar.Database.DbType.Text)]
        string Details { get; set; }
    }
}
