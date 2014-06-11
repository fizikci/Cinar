$
function onBase(ColumnName)
{
	var cont = (ColumnName=="Id");
	cont = cont || (ColumnName == "InsertDate");
	cont = cont || (ColumnName == "InsertUserId");
	cont = cont || (ColumnName == "UpdateDate");
	cont = cont || (ColumnName == "UpdateUserId");
	cont = cont || (ColumnName == "IsDeleted");
	return cont;
}
$using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $=db.Name$.DTO.EntityInfo
{
    public class $=table.Name$Info : BaseEntityInfo
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
        private $=TYPE$ $=ColumnName$ = null;
        public virtual $=util.Cap(Column.ReferenceColumn.Table.Name)$ $=util.Cap(Column.Name).Replace('Id','')$
        {
            get { return $=ColumnName$; }
            set { $=ColumnName$ = value; if($=ColumnName$!=null) $=util.Camel(Column.Name)$ = $=ColumnName$.Id;}
        }
$ }} $

    }
}
