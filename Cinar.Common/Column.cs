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
using System.Linq;

namespace Cinar.Database
{
    /// <summary>
    /// Bir tablonun bir alanına ait bilgi taşıyan sınıf.
    /// </summary>
    [Serializable]
    public class Column : IMetadata
    {
        [XmlIgnore, Browsable(false)]
        public ColumnCollection parent;

        /// <summary>
        /// Bu column'ın ait olduğu tablo
        /// </summary>
        [XmlIgnore, Browsable(false)]
        public Table Table
        {
            get { return this.parent.table; }
        }
	
        private string name;
        [Description("Name of the column"), Category("Definition")]
        public string Name
        {
            get { return name; }
            set {
                string oldName = name;
                name = value;

                if (oldName != name && this.parent != null && this.parent.table != null && this.parent.table.Indices != null && parent.table.Indices.Count > 0)
                    this.parent.table.Indices.
                        FindAll(k => k.ColumnNames.Contains(oldName)).
                        ForEach(k => { k.ColumnNames.Remove(oldName); k.ColumnNames.Add(name); });
                if (oldName != name && this.parent != null && this.parent.table != null && this.parent.table.Constraints != null && parent.table.Constraints.Count > 0)
                    this.parent.table.Constraints.
                        FindAll(k => k.ColumnNames.Contains(oldName)).
                        ForEach(k => { k.ColumnNames.Remove(oldName); k.ColumnNames.Add(name); });
            }
        }

        private DbType columnType;
        [Description("Type of the column"), Category("Definition")]
        public DbType ColumnType
        {
            get { return columnType; }
            set { columnType = value; }
        }

        [Description("Column type name on the owner database"), Category("Definition"), ReadOnly(true)]
        public string ColumnTypeOriginal { get; set; }

        private bool isNullable;
        [Description("Can be nullable?"), Category("Definition")]
        public bool IsNullable
        {
            get { return isNullable; }
            set { isNullable = value; }
        }

        private string defaultValue;
        [Description("If not specified, what is the default value of this column? (Can be an SQL expression.)"), Category("Definition")]
        public string DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        private long length;
        [Description("What is the maximum number of characters if this column is char or varchar?"), Category("Definition")]
        public long Length
        {
            get { return length; }
            set { length = value; }
        }

        [Description("Is this column a standalone primary key?"), Category("Extra Info"), ReadOnly(true)]
        [XmlIgnore]
        public bool IsPrimaryKey
        {
            get 
            {
                if(this.Table.Constraints!=null)
                    foreach (Constraint con in this.Table.Constraints)
                        if (con is PrimaryKeyConstraint && con.ColumnNames.Contains(this.Name))
                            return true;
                return false;
            }
        }

        private bool isAutoIncrement;
        [Description("Is this column auto_increment or serial or identity? That is automatically increasing number.."), Category("Extra Info")]
        public bool IsAutoIncrement
        {
            get { return isAutoIncrement; }
            set { isAutoIncrement = value; }
        }

        [Description("Another column on a different table which is referenced by this column as tableName.columnName"), Category("Extra Info")]
        public string ReferenceColumnName
        {
            get
            {
                Column referenceColumn = ReferenceColumn;
                return referenceColumn != null ? referenceColumn.Table.Name + "." + referenceColumn.Name : "";
            }
            set
            {
                if (this.parent == null)
                    return;

                if (!string.IsNullOrWhiteSpace(value) && value.Contains("."))
                {
                    string[] parts = value.Split('.');
                    if (this.Table.Database.Tables[parts[0]] == null)
                        throw new Exception("There is no such table as " + parts[0]);
                    if (this.Table.Database.Tables[parts[0]].Columns[parts[1]] == null)
                        throw new Exception("There is no such column as " + parts[1]);
                    Column forCol = this.Table.Database.Tables[parts[0]].Columns[parts[1]];
                    Table tbl = this.Table.Database.Tables[forCol.Table.Name];

                    var refCons = tbl.GetPrimaryKeyConstraint();

                    ForeignKeyConstraint fkc = new ForeignKeyConstraint()
                    {
                        Name = "fk_" + this.Table.Name + "_" + this.Name + "___" + forCol.Table.Name + "_" + forCol.Name,
                        RefConstraintName = refCons.Name,
                        RefTableName = forCol.Table.Name
                    };
                    fkc.ColumnNames.Add(this.Name);
                    this.Table.Constraints.Add(fkc);
                }
            }
        }

        [XmlIgnore, Browsable(false)]
        public Column ReferenceColumn
        {
            get
            {
                ForeignKeyConstraint fkc = (ForeignKeyConstraint)this.Table.Constraints.Find(c => c is ForeignKeyConstraint && c.ColumnNames.Count == 1 && c.ColumnNames[0] == this.Name);
                if (fkc == null)
                    return null;

                string foreignColumnName = this.Table.Database.Tables[fkc.RefTableName].Constraints[fkc.RefConstraintName].ColumnNames[0];
                return this.Table.Database.Tables[fkc.RefTableName].Columns[foreignColumnName];
            }
        }
        public override string ToString()
        {
            return this.Name;// this.Table.Name + "." + this.Name + " : " + this.ColumnType;
        }

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
                SimpleColumnType == SimpleDbType.DateTime;
        }

        public bool IsNumericType()
        {
            return
                SimpleColumnType == SimpleDbType.Integer ||
                SimpleColumnType == SimpleDbType.Float;
        }

        public bool IsStringType()
        {
            return
                SimpleColumnType == SimpleDbType.String ||
                SimpleColumnType == SimpleDbType.Text;
        }

        [Description("Simple type of this column"), Category("Extra Info")]
        public SimpleDbType SimpleColumnType
        {
            get {
                switch (ColumnType)
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
                    case DbType.Guid:
                    case DbType.Enum:
                    case DbType.Variant:
                    case DbType.Set:
                    default:
                        return SimpleDbType.Other;
                }
            }
        }

        public static DbType GetDbTypeOf(Type type)
        {
            DbType res = DbType.VarChar;

            if (type.IsEnum)
                return DbType.Int32;

            if (type.FullName.Contains("Byte[]"))
                return DbType.BlobLong;

            string typeName = type.Name;

            if (type.IsGenericType)
                typeName = type.GetGenericArguments()[0].Name;

            switch (typeName)
            {
                case "Int16":
                    res = DbType.Int16;
                    break;
                case "Int32":
                    res = DbType.Int32;
                    break;
                case "Int64":
                    res = DbType.Int64;
                    break;
                case "Boolean":
                    res = DbType.Boolean;
                    break;
                case "Byte":
                    break;
                case "String":
                    res = DbType.VarChar;
                    break;
                case "DateTime":
                    res = DbType.DateTime;
                    break;
                case "Decimal":
                    res = DbType.Decimal;
                    break;
                case "Float":
                    res = DbType.Float;
                    break;
                case "Double":
                    res = DbType.Double;
                    break;
            }
            return res;
        }

        public ColumnUIMetadata GenerateUIMetadata()
        {
            if (UIMetadata != null)
                return UIMetadata;

            UIMetadata = new ColumnUIMetadata();
            UIMetadata.DisplayName = Name;
            UIMetadata.DisplayOrder = parent.IndexOf(this);
            if (this.ReferenceColumn != null)
                UIMetadata.EditorType = EditorTypes.LookUp;
            else if (this.IsDateType())
                UIMetadata.EditorType = EditorTypes.DateEdit;
            else if (this.IsStringType())
                UIMetadata.EditorType = this.Length > 100 ? EditorTypes.MemoEdit : EditorTypes.TextEdit;
            else if (this.IsNumericType())
                UIMetadata.EditorType = EditorTypes.NumberEdit;
            else if (columnType == DbType.Boolean)
                UIMetadata.EditorType = EditorTypes.CheckBox;
            else
                UIMetadata.EditorType = EditorTypes.TextEdit;
            UIMetadata.GridColumnWidth = 150;
            UIMetadata.ShortDisplayName = Name;
            UIMetadata.ShowInFilterPanel = false;
            UIMetadata.ShowInForm = !this.IsPrimaryKey;
            UIMetadata.ShowInGrid = !this.IsPrimaryKey;
            UIMetadata.SortableInGrid = true;

            return UIMetadata;
        }

        [Description("The definitions for this column to be used generating of UI code"), Category("Extra Info")]
        public ColumnUIMetadata UIMetadata { get; set; }
    }

    public class ColumnCollection : List<Column>
    {
        [XmlIgnore, Browsable(false)]
        public Table table;
        public Table Table { get { return table; } }

        public ColumnCollection()
        {
        }
        public ColumnCollection(Table table)
        {
            this.table = table;
        }
        public new int Add(Column column)
        {
            column.parent = this;
            base.Add(column);
            return base.Count;
        }
        public Column this[string name]
        {
            get
            {
                foreach(Column column in this)
                    if(column.Name == name)
                        return column;
                return null;
            }
        }
        public Column Find(DbType columnType)
        {
            foreach (Column column in this)
                if (column.ColumnType == columnType)
                    return column;
            return null;
        }

        public string[] ToStringArray() { 
            string[] res = new string[this.Count];
            for (int i = 0; i < this.Count; i++)
                res[i] = this[i].Name;
            return res;
        }

        public override string ToString()
        {
            return table + " Columns";
        }
    }

    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class ColumnUIMetadata
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
        TimeEdit,
        TagEdit,
        Hidden
    }
}
