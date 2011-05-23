new ColumnDefinitionList{
$
foreach(field in table.Columns){ 
	if(field.Name.EndsWith("id")) continue;
$	new ColumnDefinition(){Name=$=util.Cap(table.Name)$Columns.$=util.Cap(field.Name)$, DisplayName="$=util.CapWithDash(field.Name).Replace('_',' ')$", Width=$=field.IsStringType()?150:70$},
$ } $
};