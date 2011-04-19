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
using System.ComponentModel;

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
        [XmlIgnore, Browsable(false)]
        public Table Table
        {
            get { return this.parent.table; }
        }
	
        private string name;
        [Description("Name of the field"), Category("DDL")]
        public string Name
        {
            get { return name; }
            set {
                string oldName = name;
                name = value;
                
                if (oldName != name && this.parent != null && this.parent.table != null && this.parent.table.Keys != null && parent.table.Keys.Count > 0)
                    this.parent.table.Keys.
                        FindAll(k => k.FieldNames.Contains(oldName)).
                        ForEach(k => { k.FieldNames.Remove(oldName); k.FieldNames.Add(name); });
            }
        }

        private DbType fieldType;
        [Description("Type of the field"), Category("DDL")]
        public Cinar.Database.DbType FieldType
        {
            get { return fieldType; }
            set { fieldType = value; }
        }

        [Description("Field type name on the owner database"), Category("DDL")]
        public string FieldTypeOriginal { get; set; }

        private bool isNullable;
        [Description("Can be nullable?"), Category("DDL")]
        public bool IsNullable
        {
            get { return isNullable; }
            set { isNullable = value; }
        }

        private string defaultValue;
        [Description("If not specified, what is the default value of this field? (Can be an SQL expression.)"), Category("DDL")]
        public string DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        private long length;
        [Description("What is the maximum number of characters if this field is char or varchar?"), Category("DDL")]
        public long Length
        {
            get { return length; }
            set { length = value; }
        }

        [Description("Is this this field a standalone primary key?"), Category("Extra Info")]
        [XmlIgnore]
        public bool IsPrimaryKey
        {
            get 
            {
                if(this.Table.Keys!=null)
                    foreach (Key key in this.Table.Keys)
                        if (key.IsPrimary && key.FieldNames.Count == 1 && key.Fields[0].Name==this.Name)
                            return true;
                return false;
            }
        }

        private bool isAutoIncrement;
        [Description("Is this field auto_icrement or serial or identity? That is automatically increasing number.."), Category("Extra Info")]
        public bool IsAutoIncrement
        {
            get { return isAutoIncrement; }
            set { isAutoIncrement = value; }
        }

        private string referenceFieldName;
        [Description("Another field on a different table which is referenced by this field as tableName.fieldName"), Category("Extra Info")]
        public string ReferenceFieldName
        {
            get { return referenceFieldName; }
            set { referenceFieldName = value; }
        }


        [XmlIgnore, Browsable(false)]
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
                else
                {
                    if (!defaultValue.StartsWith("'"))
                        defaultValue = "'" + defaultValue;
                    if (!defaultValue.EndsWith("'"))
                        defaultValue = defaultValue + "'";
                }
            }
            else
            {
                if (!defaultValue.StartsWith("'"))
                    defaultValue = "'" + defaultValue;
                if (!defaultValue.EndsWith("'"))
                    defaultValue = defaultValue + "'";
            }

            return defaultValue;
        }

        public bool IsDateType()
        {
            return
                SimpleFieldType == SimpleDbType.DateTime;
        }

        public bool IsNumericType()
        {
            return
                SimpleFieldType == SimpleDbType.Integer ||
                SimpleFieldType == SimpleDbType.Float;
        }

        public bool IsStringType()
        {
            return
                SimpleFieldType == SimpleDbType.String ||
                SimpleFieldType == SimpleDbType.Text;
        }

        public SimpleDbType SimpleFieldType
        {
            get {
                switch (FieldType)
                {
                    case DbType.Boolean:
                        return SimpleDbType.Boolean;
                    case DbType.Byte:
                    case DbType.Int16:
                    case DbType.Int32:
                    case DbType.Int64:
                        return SimpleDbType.Integer;
                    case DbType.Real:
                    case DbType.Float:
                    case DbType.Decimal:
                    case DbType.Double:
                    case DbType.Numeric:
                    case DbType.Currency:
                    case DbType.CurrencySmall:
                        return SimpleDbType.Float;
                    case DbType.DateTimeSmall:
                    case DbType.DateTime:
                    case DbType.Date:
                    case DbType.Time:
                    case DbType.Timetz:
                    case DbType.Timestamp:
                    case DbType.Timestamptz:
                        return SimpleDbType.DateTime;
                    case DbType.Char:
                    case DbType.VarChar:
                    case DbType.NChar:
                    case DbType.NVarChar:
                    case DbType.Guid:
                    case DbType.Enum:
                    case DbType.Variant:
                    case DbType.Set:
                        return SimpleDbType.String;
                    case DbType.Binary:
                    case DbType.VarBinary:
                    case DbType.Image:
                    case DbType.Blob:
                    case DbType.BlobTiny:
                    case DbType.BlobMedium:
                    case DbType.BlobLong:
                        return SimpleDbType.ByteArray;
                    case DbType.Text:
                    case DbType.NText:
                    case DbType.TextTiny:
                    case DbType.TextMedium:
                    case DbType.TextLong:
                    case DbType.Xml:
                        return SimpleDbType.Text;
                    default:
                        return SimpleDbType.String;
                }
            }
        }

        public void GenerateUIMetadata()
        {
            UIMetadata = new FieldUIMetadata();
            UIMetadata.DisplayName = Name;
            UIMetadata.DisplayOrder = parent.IndexOf(this);
            if (this.ReferenceField != null)
                UIMetadata.EditorType = EditorTypes.LookUp;
            else if (this.IsDateType())
                UIMetadata.EditorType = EditorTypes.DateEdit;
            else if (this.IsStringType())
                UIMetadata.EditorType = this.Length > 200 ? EditorTypes.MemoEdit : EditorTypes.TextEdit;
            else if (this.IsNumericType())
                UIMetadata.EditorType = EditorTypes.NumberEdit;
            else if (fieldType == DbType.Boolean)
                UIMetadata.EditorType = EditorTypes.CheckBox;
            else
                UIMetadata.EditorType = EditorTypes.TextEdit;
            UIMetadata.GridColumnWidth = 150;
            UIMetadata.ShortDisplayName = Name;
            UIMetadata.ShowInFilterPanel = false;
            UIMetadata.ShowInForm = !this.IsPrimaryKey;
            UIMetadata.ShowInGrid = !this.IsPrimaryKey;
            UIMetadata.SortableInGrid = true;
        }

        [Description("The definitions for this field to be used generatin of UI code"), Category("Extra Info")]
        public FieldUIMetadata UIMetadata { get; set; }
    }

    public class FieldCollection : List<Field>
    {
        internal Table table;
        public FieldCollection()
        {
        }
        public FieldCollection(Table table)
        {
            this.table = table;
        }
        public new int Add(Field field)
        {
            field.parent = this;
            base.Add(field);
            return base.Count;
        }
        public Field this[string name]
        {
            get
            {
                foreach(Field fld in this)
                    if(fld.Name == name)
                        return fld;
                return null;
            }
        }
        public Field Find(DbType fieldType)
        {
            foreach (Field fld in this)
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

    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class FieldUIMetadata
    {
        public string DisplayName { get; set; }
        public string ShortDisplayName { get; set; }
        public EditorTypes EditorType { get; set; }
        public string GroupName { get; set; }
        public int DisplayOrder { get; set; }
        public int GridColumnWidth { get; set; }
        public bool ShowInForm { get; set; }
        public bool ShowInGrid { get; set; }
        public bool SortableInGrid { get; set; }
        public bool ShowInFilterPanel { get; set; }
    }

    public enum EditorTypes
    {
        Undefined,
        TextEdit,
        MemoEdit,
        HTMLEdit,
        CheckBox,
        ComboBox,
        LookUp,
        DateEdit,
        NumberEdit,
        TimeEdit
    }
}
