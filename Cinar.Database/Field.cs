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
using System.Collections;
using System.Data;
using System.Xml.Serialization;

namespace Cinar.Database
{
    /// <summary>
    /// Bir tablonun bir alanına ait bilgi taşıyan sınıf.
    /// </summary>
    [Serializable]
    public class Field
    {
        internal FieldCollection parent;

        /// <summary>
        /// Bu field'ın ait olduğu tablo
        /// </summary>
        [XmlIgnore]
        public Table Table
        {
            get { return this.parent.table; }
        }
	
        private string name;
        /// <summary>
        /// Field'ın adı.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private DbType fieldType;
        /// <summary>
        /// Bu field'ın tipi
        /// </summary>
        public Cinar.Database.DbType FieldType
        {
            get { return fieldType; }
            set { fieldType = value; }
        }

        private bool isNullable;
        /// <summary>
        /// Insert ve updatelerde bu field'ın değeri boş (null) bırakılabilir mi?
        /// </summary>
        public bool IsNullable
        {
            get { return isNullable; }
            set { isNullable = value; }
        }

        private string defaultValue;
        /// <summary>
        /// Bir insert sorgusunda bu field belirtilmezse default olarak girilmesi gereken değer nedir?
        /// Bu değer veritabanından "select " + field.DefaultValue + " from " + field.Table.Name şeklinde okunmalıdır.
        /// </summary>
        public string DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        private long length;
        /// <summary>
        /// Eğer bu bir varchar veya char türü bir alan ise maximum karekter boyu nedir?
        /// </summary>
        public long Length
        {
            get { return length; }
            set { length = value; }
        }

        /// <summary>
        /// Bu alan tek başına Primary Key olarak tanımlanmış mıdır?
        /// </summary>
        [XmlIgnore]
        public bool IsPrimaryKey
        {
            get 
            {
                foreach (Key key in this.Table.Keys)
                    if (key.IsPrimary && key.FieldNames.Count == 1 && key.Fields[0].Name==this.Name)
                        return true;
                return false;
            }
        }

        private bool isAutoIncrement;
        /// <summary>
        /// Bu alan auto_increment veya serial veya identity midir? Yani otomatik artar mı?
        /// </summary>
        public bool IsAutoIncrement
        {
            get { return isAutoIncrement; }
            set { isAutoIncrement = value; }
        }

        private string referenceFieldName;
        /// <summary>
        /// Bu alanın referans verdiği başka bir tablodaki alan.
        /// </summary>
        public string ReferenceFieldName
        {
            get { return referenceFieldName; }
            set { referenceFieldName = value; }
        }
	

        /// <summary>
        /// Bu alanın referans verdiği başka bir tablodaki alan.
        /// </summary>
        [XmlIgnore]
        public Field ReferenceField
        {
            get {
                if (String.IsNullOrEmpty(referenceFieldName))
                    return null;

                string[] parts = referenceFieldName.Split('.');
                return this.Table.Database.Tables[parts[0]].Fields[parts[1]]; 
            }
            set {
                if (value != null)
                {
                    this.referenceFieldName = value.Table.Name + "." + value.Name;
                }
            }
        }
        public override string ToString()
        {
            return this.Name;// this.Table.Name + "." + this.Name + " : " + this.FieldType;
        }
        //public string ToDDL()
        //{
        //    string str = this.Name + " " + this.Table.Database.dbProvider.DbTypeToString(this.FieldType);
        //    if (this.Length > 0 && (FieldType == DbType.Char || FieldType == DbType.VarChar || FieldType == DbType.NChar || FieldType == DbType.NVarChar))
        //        str += "(" + this.Length + ")";
        //    if (this.IsAutoIncrement)
        //        str += " IDENTITY";
        //    if (!this.IsNullable)
        //        str += " NOT NULL";
        //    if (this.IsPrimaryKey)
        //        str += " PRIMARY KEY";
        //    if (!string.IsNullOrEmpty(this.DefaultValue))
        //        str += " DEFAULT " + this.DefaultValue;
        //    if (this.ReferenceField != null)
        //        str += " REFERENCES [" + this.ReferenceField.Table.Name + "](" + this.ReferenceField.Name + ")";
        //    return str;
        //}

        internal string GetDefaultValueQuoted()
        {
            if (this.Table.parent.db.Provider == DatabaseProvider.SQLServer && !String.IsNullOrEmpty(defaultValue) && this.IsDateType())
            {
                DateTime dt;
                if (!DateTime.TryParse(defaultValue, out dt))
                    defaultValue = "getdate()";
                else if (dt < new DateTime(1753, 1, 1, 12, 0, 0))
                    defaultValue = "getdate()";
                else if (defaultValue.StartsWith("'") && defaultValue.EndsWith("'"))
                    ;
                else
                    defaultValue = "'" + defaultValue + "'";
            }
            else
            {
                if (defaultValue.StartsWith("'") && defaultValue.EndsWith("'"))
                    ;
                else
                    defaultValue = "'" + defaultValue + "'";
            }

            return defaultValue;
        }

        public bool IsDateType()
        {
            return
                fieldType == DbType.Date ||
                fieldType == DbType.DateTime ||
                fieldType == DbType.DateTimeSmall;
                //fieldType == DbType.Time ||
                //fieldType == DbType.Timestamp ||
                //fieldType == DbType.Timestamptz ||
                //fieldType == DbType.Timetz;
        }

        public bool IsNumericType()
        {
            return
                fieldType == DbType.Byte ||
                fieldType == DbType.Currency ||
                fieldType == DbType.CurrencySmall ||
                fieldType == DbType.Decimal ||
                fieldType == DbType.Double ||
                fieldType == DbType.Float ||
                fieldType == DbType.Int16 ||
                fieldType == DbType.Int32 ||
                fieldType == DbType.Int64 ||
                fieldType == DbType.Numeric ||
                fieldType == DbType.Real;
        }

        public bool IsStringType()
        {
            return
                fieldType == DbType.Char ||
                fieldType == DbType.Enum ||
                fieldType == DbType.Guid ||
                fieldType == DbType.NChar ||
                fieldType == DbType.NText ||
                fieldType == DbType.NVarChar ||
                fieldType == DbType.Text ||
                fieldType == DbType.TextLong ||
                fieldType == DbType.TextMedium ||
                fieldType == DbType.TextTiny ||
                fieldType == DbType.VarChar ||
                fieldType == DbType.Xml;
        }
    }

    public class FieldCollection : CollectionBase
    {
        internal Table table;
        public FieldCollection()
        {
        }
        public FieldCollection(Table table)
        {
            this.table = table;
        }
        public int Add(Field field)
        {
            field.parent = this;
            return this.List.Add(field);
        }
        public Field this[int index]
        {
            get
            {
                return (Field)this.List[index];
            }
        }
        public Field this[string name]
        {
            get
            {
                foreach(Field fld in this.List)
                    if(fld.Name == name)
                        return fld;
                return null;
            }
        }
        public Field Find(DbType fieldType)
        {
            foreach (Field fld in this.List)
                if (fld.FieldType == fieldType)
                    return fld;
            return null;
        }
        //public override string ToString()
        //{
        //    return this.ToDDL();
        //}

        //public string ToDDL()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach(Field f in this)
        //        sb.Append("\t" + f.ToDDL() + "," + Environment.NewLine);
        //    sb = sb.Remove(sb.Length - 3, 2);
        //    return sb.ToString();
        //}

        public string[] ToStringArray() { 
            string[] res = new string[this.Count];
            for (int i = 0; i < this.Count; i++)
                res[i] = this[i].Name;
            return res;
        }
    }

}
