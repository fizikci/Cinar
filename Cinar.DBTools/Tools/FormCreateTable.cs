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
            txtTableName.Text = table.Name;
            List<ColumnDef> columns = new List<ColumnDef>();
            foreach (Column f in table.Columns)
            {
                columns.Add(new ColumnDef { 
                    DefaultValue = f.DefaultValue,
                    ColumnType = string.IsNullOrEmpty(f.ColumnTypeOriginal) ? Provider.Database.DbTypeToString(f.ColumnType) : f.ColumnTypeOriginal,
                    IsAutoIncrement = f.IsAutoIncrement,
                    IsNullable = f.IsNullable,
                    IsPrimaryKey = f.IsPrimaryKey,
                    Length = (int)f.Length,
                    Name = f.Name,
                    OriginalName = f.Name
                });
            }
            columnCollectionBindingSource.DataSource = columns;
        }

        public TableDef GetAlteredTable()
        {
            TableDef t = new TableDef();
            t.Columns = columnCollectionBindingSource.DataSource as List<ColumnDef>;
            t.Name = txtTableName.Text;
            return t;
        }
    }

    public class ColumnDef
    {
        public string OriginalName { get; set; }
        public string Name { get; set; }
        public string ColumnType { get; set; }
        public int Length { get; set; }
        public string DefaultValue { get; set; }
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsAutoIncrement { get; set; }
    }
    public class TableDef
    {
        public List<ColumnDef> Columns { get; set; }
        public string Name { get; set; }

        public TableDef() {
            Columns = new List<ColumnDef>();
        }
    }
}
