CREATE VIEW View$=table.Name$
AS
SELECT
$
var join = '';
foreach(var field in table.Columns)
{ 
	echo('	'+table.Name+'.'+field.Name+',\n');
	if(field.ReferenceColumn)
	{
		echo('	'+field.ReferenceColumn.Table.Name+'.'+field.ReferenceColumn.Table.StringColumn.Name+' AS '+field.ReferenceColumn.Table.Name+'_name,\n');
		join += '	LEFT JOIN '+field.ReferenceColumn.Table.Name+' ON '+table.Name+'.'+field.Name+' = '+field.ReferenceColumn.Table.Name+'.'+field.ReferenceColumn.Name + '\n';
	}
}
$
FROM
	$=table.Name$
$=join$