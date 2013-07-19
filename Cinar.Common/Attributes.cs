/*
Copyright (C) 2006  Bülent Keskin

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Cinar.Database
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ColumnDetailAttribute : Attribute
    {
        public ColumnDetailAttribute()
        {
        }

        public string Name { get; set; }

        private DbType columnType = DbType.Undefined;
        /// <summary>
        /// Bu alanın tipi
        /// </summary>
        public Cinar.Database.DbType ColumnType
        {
            get { return columnType; }
            set { columnType = value; }
        }

        private bool isNotNull = false;
        /// <summary>
        /// Insert ve updatelerde bu alanın değeri boş (null) bırakılabilir mi?
        /// </summary>
        public bool IsNotNull
        {
            get { return isNotNull; }
            set { isNotNull = value; }
        }

        private string defaultValue;
        /// <summary>
        /// Bir insert sorgusunda bu alan belirtilmezse default olarak girilmesi gereken değer nedir?
        /// Bu değer veritabanından "select " + column.DefaultValue şeklinde okunmalıdır.
        /// </summary>
        public string DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        private long length = 100;
        /// <summary>
        /// Eğer bu bir varchar veya char türü bir alan ise maximum karekter boyu nedir?
        /// </summary>
        public long Length
        {
            get { return length; }
            set { length = value; }
        }

        private bool isPrimaryKey = false;
        /// <summary>
        /// Bu alan üzerine Primary Key tanımlanmış mıdır?
        /// </summary>
        public bool IsPrimaryKey
        {
            get { return isPrimaryKey; }
            set { isPrimaryKey = value; }
        }

        private bool isAutoIncrement = false;
        /// <summary>
        /// Bu alan auto_increment veya serial veya identity midir? Yani otomatik artar mı?
        /// </summary>
        public bool IsAutoIncrement
        {
            get { return isAutoIncrement; }
            set { isAutoIncrement = value; }
        }

        private bool isUnique = false;
        /// <summary>
        /// Tek alan üzerinden Unique olma durumu. Bir alan PrimaryKey olsa bile Unique olmayabilir.
        /// </summary>
        public bool IsUnique
        {
            get { return isUnique; }
            set { isUnique = value; }
        }

        private Type references;
        /// <summary>
        /// Bu alan başka bir tablo ismi olmalıdır.
        /// </summary>
        public Type References
        {
            get { return references; }
            set { references = value; }
        }

        private ReferenceTypes referenceType = ReferenceTypes.ManyToOne;
        /// <summary>
        /// Referans tipinin OneToOne olması nadir fakat dikkate değer bir durumdur.
        /// </summary>
        public ReferenceTypes ReferenceType
        {
            get { return referenceType; }
            set { referenceType = value; }
        }

    }

    public enum ReferenceTypes
    {
        OneToOne,
        ManyToOne
    }


    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class TableDetailAttribute : Attribute
    {
        public string Name { get; set; }
        public TableTypes Type { get; set; }
        public bool IsView { get; set; }
        public string ViewSQL { get; set; }
    }

    public enum TableTypes
    {
        Account,
        Transaction
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class DefaultDataAttribute : Attribute
    {
        private string columnList;
        /// <summary>
        /// Örn: Yas, Ad, Soyad
        /// </summary>
        public string ColumnList
        {
            get { return columnList; }
            set { columnList = value; }
        }
        private string valueList;
        /// <summary>
        /// Exp: 15, 'Ahmet', 'Keskin'
        /// </summary>
        public string ValueList
        {
            get { return valueList; }
            set { valueList = value; }
        }
    }

}
