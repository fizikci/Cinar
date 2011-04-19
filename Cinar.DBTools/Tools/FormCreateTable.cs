using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Database;

namespace Cinar.DBTools.Tools
{
    public partial class FormCreateTable : Form
    {
        public FormCreateTable()
        {
            InitializeComponent();

            string[] fieldTypes = Provider.Database.GetOriginalFieldTypes().OrderBy(s => s).ToArray();

            foreach (object o in fieldTypes)
                colFieldType.Items.Add(o);

            fieldCollectionBindingSource.DataSource = new List<FieldDef>();
        }

        public Table GetCreatedTable()
        {
            Table tbl = new Table();
            tbl.Fields = new FieldCollection(tbl);
            tbl.Indices = new IndexCollection(tbl);
            tbl.Name = txtTableName.Text.MakeFileName();
            if (!(fieldCollectionBindingSource.DataSource is Type))
                foreach (FieldDef fd in fieldCollectionBindingSource.DataSource as List<FieldDef>)
                {
                    Field f = new Field()
                    {
                        Name = fd.Name,
                        FieldTypeOriginal = fd.FieldType,
                        FieldType = Provider.Database.StringToDbType(fd.FieldType),
                        Length = fd.Length,
                        DefaultValue = fd.DefaultValue,
                        IsNullable = fd.IsNullable,
                        IsAutoIncrement = fd.IsAutoIncrement
                    };
                    tbl.Fields.Add(f);
                    if (fd.IsPrimaryKey)
                    {
                        Index k = new Index();
                        tbl.Indices.Add(k);
                        k.FieldNames.Add(f.Name);
                        k.IsPrimary = true;
                        k.IsUnique = true;
                        k.Name = "PK_" + tbl.Name;
                    }
                }
            return tbl;
        }

        public void SetTable(Table table)
        {
            txtTableName.Text = table.Name;
            List<FieldDef> fields = new List<FieldDef>();
            foreach (Field f in table.Fields)
            {
                fields.Add(new FieldDef { 
                    DefaultValue = f.DefaultValue,
                    FieldType = string.IsNullOrEmpty(f.FieldTypeOriginal) ? Provider.Database.DbTypeToString(f.FieldType) : f.FieldTypeOriginal,
                    IsAutoIncrement = f.IsAutoIncrement,
                    IsNullable = f.IsNullable,
                    IsPrimaryKey = f.IsPrimaryKey,
                    Length = (int)f.Length,
                    Name = f.Name,
                    OriginalName = f.Name
                });
            }
            fieldCollectionBindingSource.DataSource = fields;
        }

        public TableDef GetAlteredTable()
        {
            TableDef t = new TableDef();
            t.Fields = fieldCollectionBindingSource.DataSource as List<FieldDef>;
            t.Name = txtTableName.Text;
            return t;
        }
    }

    public class FieldDef
    {
        public string OriginalName { get; set; }
        public string Name { get; set; }
        public string FieldType { get; set; }
        public int Length { get; set; }
        public string DefaultValue { get; set; }
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsAutoIncrement { get; set; }
    }
    public class TableDef
    {
        public List<FieldDef> Fields { get; set; }
        public string Name { get; set; }

        public TableDef() {
            Fields = new List<FieldDef>();
        }
    }
}
