using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Cinar.Database
{
    /// <summary>
    /// An interface for storable entities.
    /// </summary>
    public interface IDatabaseEntity : IDatabaseEntityMinimal
    {
        int Id
        {
            get;
            set;
        }
    }

    public interface IDatabaseEntityMinimal
    {
        void Initialize();
        void BeforeSave();
        void AfterSave(bool isUpdate);

        string GetNameColumn();
        string GetNameValue();

        object this[string key]
        {
            get;
            set;
        }

        Hashtable GetOriginalValues();
    }

    /// <summary>
    /// Inherited classes mapped to the table of the base class.
    /// Base class implements this interface.
    /// </summary>
    public interface ISerializeSubclassFields
    {
        [ColumnDetail(IsNotNull = true, Length = 100)]
        string TypeName { get; set; }

        [ColumnDetail(ColumnType = Cinar.Database.DbType.Text)]
        string Details { get; set; }
    }


}
