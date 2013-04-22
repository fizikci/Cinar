using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Database;
using ForeignKeyConstraint = Cinar.Database.ForeignKeyConstraint;

namespace Cinar.DBTools.Tools
{
    public partial class FormCreateTable : Form
    {
        public FormCreateTable()
        {
            InitializeComponent();

            string[] columnTypes = Provider.Database.GetOriginalColumnTypes().OrderBy(s => s).ToArray();

            foreach (object o in columnTypes)
                colColumnType.Items.Add(o);

            columnCollectionBindingSource.DataSource = new List<ColumnDef>();
        }

        public Table GetCreatedTable()
        {
            Table tbl = new Table();
            tbl.Name = txtTableName.Text.MakeFileName();
            if (!(columnCollectionBindingSource.DataSource is Type))
                foreach (ColumnDef fd in columnCollectionBindingSource.DataSource as List<ColumnDef>)
                {
                    Column f = new Column()
                    {
                        Name = fd.Name,
                        ColumnTypeOriginal = fd.ColumnType,
                        ColumnType = Provider.Database.StringToDbType(fd.ColumnType),
                        Length = fd.Length,
                        DefaultValue = fd.DefaultValue,
                        IsNullable = fd.IsNullable,
                        IsAutoIncrement = fd.IsAutoIncrement
                    };
                    tbl.Columns.Add(f);
                    if (fd.IsPrimaryKey)
                    {
                        PrimaryKeyConstraint k = new PrimaryKeyConstraint();
                        tbl.Constraints.Add(k);
                        k.ColumnNames.Add(f.Name);
                        k.Name = "PK_" + tbl.Name;
                    }
                }
            return tbl;
        }

        public void SetTable(Table table)
        {
            tableDef.OriginalTable = table;

            tableDef.OriginalName = table.Name;
            txtTableName.Text = table.Name;

            foreach (Column f in table.Columns)
            {
                tableDef.Columns.Add(new ColumnDef { 
                    DefaultValue = f.DefaultValue,
                    ColumnType = string.IsNullOrEmpty(f.ColumnTypeOriginal) ? Provider.Database.DbTypeToString(f.ColumnType) : f.ColumnTypeOriginal,
                    IsAutoIncrement = f.IsAutoIncrement,
                    IsNullable = f.IsNullable,
                    IsPrimaryKey = f.IsPrimaryKey,
                    Length = (int)f.Length,
                    Name = f.Name,
                    OriginalColumn = f,

                    OriginalDefaultValue = f.DefaultValue,
                    OriginalColumnType = string.IsNullOrEmpty(f.ColumnTypeOriginal) ? Provider.Database.DbTypeToString(f.ColumnType) : f.ColumnTypeOriginal,
                    OriginalIsAutoIncrement = f.IsAutoIncrement,
                    OriginalIsNullable = f.IsNullable,
                    OriginalIsPrimaryKey = f.IsPrimaryKey,
                    OriginalLength = (int)f.Length,
                    OriginalName = f.Name,
                });
            }
            columnCollectionBindingSource.DataSource = tableDef.Columns;
        }

        TableDef tableDef = new TableDef();

        public TableDef GetAlteredTable()
        {
            tableDef.Columns = columnCollectionBindingSource.DataSource as List<ColumnDef>;
            tableDef.Name = txtTableName.Text;
            return tableDef;
        }
    }

    public class ColumnDef
    {
        public Column OriginalColumn { get; set; }

        public string Name { get; set; }
        public string ColumnType { get; set; }
        public int Length { get; set; }
        public string DefaultValue { get; set; }
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsAutoIncrement { get; set; }

        public string OriginalName { get; set; }
        public string OriginalColumnType { get; set; }
        public int OriginalLength { get; set; }
        public string OriginalDefaultValue { get; set; }
        public bool OriginalIsNullable { get; set; }
        public bool OriginalIsPrimaryKey { get; set; }
        public bool OriginalIsAutoIncrement { get; set; }
    }
    public class TableDef
    {
        public Table OriginalTable { get; set; }

        public List<ColumnDef> Columns { get; set; }
        public string Name { get; set; }

        public string OriginalName { get; set; }

        public TableDef() {
            Columns = new List<ColumnDef>();
        }

        public void UndoChanges() 
        {
            OriginalTable.Name = OriginalName;

            foreach (ColumnDef c in this.Columns) 
            {
                if (c.OriginalColumn == null && OriginalTable.Columns[c.Name] != null)
                {
                    OriginalTable.Columns.Remove(OriginalTable.Columns[c.Name]);
                    continue;
                }

                c.OriginalColumn.Name = c.OriginalName;
                c.OriginalColumn.ColumnType = OriginalTable.Database.StringToDbType(c.OriginalColumnType);
                c.OriginalColumn.ColumnTypeOriginal = c.OriginalColumnType;
                c.OriginalColumn.Length = c.OriginalLength;
                c.OriginalColumn.DefaultValue = c.OriginalDefaultValue;
                c.OriginalColumn.IsNullable = c.OriginalIsNullable;
                //c.OriginalColumn.IsPrimaryKey = c.OriginalIsPrimaryKey;
                c.OriginalColumn.IsAutoIncrement = c.OriginalIsAutoIncrement;
            }
        }
    }
}
