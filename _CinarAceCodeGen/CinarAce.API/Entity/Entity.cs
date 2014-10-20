$
function onBase(ColumnName)
{
	var cont = (ColumnName=="Id");
	cont = cont || (ColumnName == "Name");
	cont = cont || (ColumnName == "InsertDate");
	cont = cont || (ColumnName == "InsertUserId");
	cont = cont || (ColumnName == "UpdateDate");
	cont = cont || (ColumnName == "UpdateUserId");
	cont = cont || (ColumnName == "IsDeleted");
	cont = cont || (ColumnName == "Visible");
	cont = cont || (ColumnName == "Kodu");
	cont = cont || (ColumnName == "Description");
	cont = cont || (ColumnName == "ThumbnailPicture");
	cont = cont || (ColumnName == "Onaylandi");
	cont = cont || (ColumnName == "OrderNo");
	return cont;
}
$ using Cinar.Database;
using $=db.Name$.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace $=db.Name$.API.Entity
{
    public class $=table.Name$ : $=table.Columns["Name"]!=null ? "NamedEntity":"BaseEntity"$
    {
$ 
foreach(Column in table.Columns){ 
	if(onBase(Column.Name)) continue; 

	var TYPE = util.CSType(Column.ColumnType.ToString());
	var columnName = util.Camel(Column.Name);
	var ColumnName = util.Cap(Column.Name);
	
	if(Column.Name!=ColumnName) echo('\r\n        [ColumnDetail(Name="'+Column.Name+'")]\r\n');
$
        public $=TYPE$ $=ColumnName$ {get; set;}
$ } $

$ 
foreach(Column in table.Columns){ 
	if(Column.ReferenceColumn) { 
		var ColumnName = util.Camel(Column.Name).Replace('Id','');
$
        public $=util.Cap(Column.ReferenceColumn.Table.Name)$ $=util.Cap(Column.Name).Replace('Id','')$ { get { return Provider.Database.Read<$=util.Cap(Column.ReferenceColumn.Table.Name)$>($=Column.Name$); } }
$ }} $

    }
}
