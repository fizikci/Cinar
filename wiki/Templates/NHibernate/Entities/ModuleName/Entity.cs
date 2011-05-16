$
function onBase(fieldName)
{
	var cont = (fieldName=="id");
	cont = cont || (fieldName == "insert_date");
	cont = cont || (fieldName == "insert_user_id");
	cont = cont || (fieldName == "update_date");
	cont = cont || (fieldName == "update_user_id");
	cont = cont || (fieldName == "deleted");
	return cont;
}
$using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Interpress.Entities.Base;

namespace Interpress.Entities.$=util.Cap(table.Database.Name)$
{
    [DataContract(IsReference = true)]
    public class $=util.Cap(table.Name)$ : BaseEntity
    {
$ 
foreach(field in table.Columns){ 
	if(onBase(field.Name)) continue; 
	var TYPE = util.CSType(field.ColumnType.ToString());
	var fieldName = util.Camel(field.Name);
	var val = util.CSTypeConstant(field.ColumnType.ToString());
$
        private $=TYPE$ $=fieldName$ = $=val$;
$ } $
$ 
foreach(field in table.Columns){ 
	if(field.ReferenceColumn) { 
		var TYPE = util.Cap(field.ReferenceColumn.Table.Name);
		var fieldName = util.Camel(field.Name).Replace('Id','');
$
        private $=TYPE$ $=fieldName$ = null;
$ }} $

$ 
foreach(field in table.Columns){ 
	if(onBase(field.Name)) continue; 

	var TYPE = util.CSType(field.ColumnType.ToString());
	var fieldName = util.Camel(field.Name);
	var ColumnName = util.Cap(field.Name);
$
        [DataMember]
        public virtual $=TYPE$ $=ColumnName$
        {
            get { return $=fieldName$; }
            set { $=fieldName$ = value; }
        }
$ } $

$ 
foreach(field in table.Columns){ 
	if(field.ReferenceColumn) { 
		var fieldName = util.Camel(field.Name).Replace('Id','');
$
        [DataMember]
        public virtual $=util.Cap(field.ReferenceColumn.Table.Name)$ $=util.Cap(field.Name).Replace('Id','')$
        {
            get { return $=fieldName$; }
            set { $=fieldName$ = value; if($=fieldName$!=null) $=util.Camel(field.Name)$ = $=fieldName$.Id;}
        }
$ }} $

$ 
if(table.IsView==false)
{
foreach(tbl in table.Database.Tables){ 
	foreach(field in tbl.Columns){ 
		if(field.ReferenceColumn) {
			if(field.ReferenceColumn.Table.Name==table.Name && field.ReferenceColumn.Name==table.PrimaryColumn.Name){ 
$
        IList<$=util.Cap(tbl.Name)$> $=util.Camel(tbl.Name)$Listesi;
        
        public virtual IList<$=util.Cap(tbl.Name)$> $=util.Cap(tbl.Name)$Listesi
        {
            get { return $=util.Camel(tbl.Name)$Listesi; }
            set { $=util.Camel(tbl.Name)$Listesi = value; }
        }
$ }}}}} $

    }

	public class $=util.Cap(table.Name)$Columns : BaseEntityColumns
	{
$ 
foreach(field in table.Columns){ 
	if(onBase(field.Name)) continue; 
	var ColumnName = util.Cap(field.Name);
	if(field.ReferenceColumn) {
		echo('\r\n        public const string '+ColumnName.Replace('Id','')+' = "'+ColumnName.Replace('Id','')+'";\r\n');
	}
$
        public const string $=ColumnName$ = "$=ColumnName$";$ } $

	}
}

