var classes = {};
Ext.select('table.member-table tr.config-row td.sig').each(function(e){
    var t = e.dom.children[0].id.split('-')[0];
    var p = e.dom.children[0].id.split('-')[1];
    var pt = e.dom.childNodes[2].wholeText.split(':')[1];
    var d = e.dom.childNodes[3].childNodes[0] ? e.dom.childNodes[3].childNodes[0].innerHTML : "";

    if(pt==' Array/Object') pt = 'json';
    else if(pt==' String') pt = 'string';
    else if(pt==' Number') pt = 'int?';
    else if(pt==' Boolean') pt = 'bool?';
    else if(pt==' Object') pt = 'json';
    else if(pt==' Array') pt = 'IList';
    else if(pt==' Function') pt = 'Action';
    else if(pt==' Array/String') pt = 'List<string>';
    else if(pt==' Mixed') pt = 'json';

    if(classes[t]==undefined)
        classes[t] = {};
    classes[t][p] = {type:pt, desc:d};
});

for(var k in classes)
{
    console.log('public class ' + k);
    console.log('{');
    for(var p in classes[k])
    {
        if(classes[k][p].desc)
            console.log('\t[Description("' + classes[k][p].desc.replace('"','\\"') + '")]');
        console.log('\tpublic ' + classes[k][p].type + ' ' + p + ';');
    }
    console.log('}');
}