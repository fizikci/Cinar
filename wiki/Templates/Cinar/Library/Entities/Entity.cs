$
function onBase(columnName)
{
	var cont = (columnName=="id");
	cont = cont || (columnName == "insert_date");
	cont = cont || (columnName == "InsertDate");
	cont = cont || (columnName == "insert_user_id");
	cont = cont || (columnName == "InsertUserId");
	cont = cont || (columnName == "visible");
	cont = cont || (columnName == "Visible");
	return cont;
}
$
using System;
using Cinar.Database;
using Library.Entities;

namespace $=util.Cap(table.Database.Name)$.Library.Entities
{
    public class $=util.Cap(table.Name)$ : BaseEntity
    {
$ 
foreach(column in table.Columns){ 
	if(onBase(column.Name)) continue; 
	var TYPE = util.CSType(column.ColumnType.ToString());
	var columnName = util.Camel(column.Name);
	var val = util.CSTypeConstant(column.ColumnType.ToString());
$
        private $=TYPE$ $=columnName$ = $=val$;
$ } $
$ 
foreach(column in table.Columns){ 
	if(column.ReferenceColumn) { 
		var TYPE = util.Cap(column.ReferenceColumn.Table.Name);
		var columnName = util.Camel(column.Name).Replace('Id','');
$
        private $=TYPE$ $=columnName$ = null;
$ }} $

$ 
foreach(column in table.Columns){ 
	if(onBase(column.Name)) continue; 

	var TYPE = util.CSType(column.ColumnType.ToString());
	var columnName = util.Camel(column.Name);
	var ColumnName = util.Cap(column.Name);
$
        public $=TYPE$ $=ColumnName$
        {
            get { return $=columnName$; }
            set { $=columnName$ = value; }
        }
$ } $

$ 
foreach(column in table.Columns){ 
	if(column.ReferenceColumn) { 
		var columnName = util.Camel(column.Name).Replace('Id','');
$
        public virtual $=util.Cap(column.ReferenceColumn.Table.Name)$ $=util.Cap(column.Name).Replace('Id','')$
        {
            get { return $=columnName$; }
            set { $=columnName$ = value; if($=columnName$!=null) $=util.Camel(column.Name)$ = $=columnName$.Id;}
        }
$ }} $

$ 
foreach(tbl in table.Database.Tables){ 
	foreach(column in tbl.Columns){ 
		if(column.ReferenceColumn) {
			if(column.ReferenceColumn.Table.Name==table.Name && column.ReferenceColumn.Name==table.PrimaryColumn.Name){ 
$
        IList<$=util.Cap(tbl.Name)$> $=util.Camel(tbl.Name)$Listesi;
        
        public virtual IList<$=util.Cap(tbl.Name)$> $=util.Cap(tbl.Name)$Listesi
        {
            get { return $=util.Camel(tbl.Name)$Listesi; }
            set { $=util.Camel(tbl.Name)$Listesi = value; }
        }
$ }}}} $

    }

	public class $=util.Cap(table.Name)$Columns : BaseEntityColumns
	{
$ 
foreach(column in table.Columns){ 
	if(onBase(column.Name)) continue; 
	var columnName = util.Cap(column.Name);
$
        public const string $=columnName$ = "$=columnName$";$ } $

	}
}


