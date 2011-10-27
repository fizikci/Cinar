var CinarCMS = {
    version: '1.0'
}

var traceMode = false;
var designMode = true;
var trace = null;
var regions = [];
var regionNames = [];
var regionDivs = [];
var navigationEnabled = true;

window.onload = onPageLoaded;
function onPageLoaded()
{
    try{
        trace = new Trace();
        trace.write({id:'Sistem'}, 'page load started');

        if (!designMode) return;

        $(document.body).style.height = '100%';
        
        regionDivs = $$('div.Region');//$('Header','Left','Content','Right','Footer').compact();
        regionDivs.each(function(elm){
            regionNames.push(elm.id);
        });

        clearFlashes();
        
        Event.observe(document, 'keydown', selectNext);
        
        mdlSel = $('mdlSel');
        $$('div.Module').each(function(mdl){
            Event.observe(mdl, 'mouseover', highlightModule);
        });
        selectFirstModule();
        
        trace.write({id:'Sistem'}, 'page load finished');
    }
    catch(e){
        if(typeof e == 'String')
            trace.warn({id:'PageLoad'}, e);
        else
            trace.warn({id:'PageLoad'}, e.name + ' : ' + e.message);
    }
}

function clearFlashes(){
    $$('object').each(function(elm){
        new Insertion.Before(elm, '<img src="external/icons/flash_spacer.jpg" width="'+elm.width+'" height="'+elm.height+'"/>');
        elm.remove();
    });
} 

//######################################################################
//#          SELECT MODULE (select first/next/prev) FUNCTIONS          #
//######################################################################

wr('<div id="mdlSel">');
    wr('<div><nobr>');
    wr('<img src="external/icons/module_edit.png" onclick="editModule()" title="'+lang('Edit')+'">');
    wr('<img src="external/icons/module_delete.png" onclick="deleteModule()" title="'+lang('Delete')+'">');
    wr('<img src="external/icons/arrow_up.png" onclick="upModule()" title="'+lang('Move Up')+'">');
    wr('<img src="external/icons/arrow_down.png" onclick="downModule()" title="'+lang('Move Down')+'">');
    wr('<img src="external/icons/module_add.png" onclick="addModule()" title="'+lang('Add Module')+'">');
    wr('</nobr></div>');
wr('</div>');

var mdlSel = null;
function highlightModule(event){
    if(!navigationEnabled) return;
    
    var mdl = $(Event.element(event));
    while(mdl && mdl.className.indexOf('Module')==-1) {
        mdl = mdl.up();
    }
    selectModule(mdl);    
}
function selectFirstModule(){
    var modules = $$('div.Module');
    if(modules.length>0){
        selMod = modules[0];
        selectModule(selMod);
    }
}
function selectModule(mdl){
    if(!mdl) return; //***
    
    selMod = mdl;
    selReg = selMod.up('.Region');

    console.log("selMod: "+selMod.id);
    console.log("selReg: "+selReg.id);
    
    var pos = Position.cumulativeOffset(mdl);
    var dim = mdl.getDimensions();
    mdlSel.hide();
    mdlSel.setStyle({left:pos[0]+'px', top:pos[1]+'px', width:dim.width+'px', height:dim.height+'px'});
    //new Effect.Appear(mdlSel, { duration: 0.1, from: 0.0, to: 0.7 });
    mdlSel.show();
}
function findNextModule(mdl)
{
    var modules = $$('div.Module');
    for(var i=0; i<modules.length; i++)
        if(modules[i]==mdl)
            if(i==modules.length-1)
                return modules[0];
            else
                return modules[i+1];
}
function findPrevModule(mdl)
{
    var modules = $$('div.Module');
    for(var i=0; i<modules.length; i++)
        if(modules[i]==mdl)
            if(i==0)
                return modules[modules.length-1];
            else
                return modules[i-1];
}

document.observe('dom:loaded', function(){
    Windows.addObserver({
        onShow: function(){
            navigationEnabled = false;
        },
        onClose: function(){
            if(!Windows.getFocusedWindow())
                navigationEnabled = true;
        }
    });
});
function selectNext(event)
{
    //alert(event.keyCode);
    var win = Windows.getFocusedWindow();
    
    if(win){
        if(event.keyCode==Event.KEY_ESC)
            win.close();
        if(win['form'] && win['form'].formType=='ListForm'){
            var listGrid = win['form'].listGrid;
            switch(event.keyCode){
                case Event.KEY_RETURN:
                    win['form'].cmdEdit();
                    break;
                case Event.KEY_INSERT:
                    win['form'].cmdAdd();
                    break;
                case Event.KEY_DELETE:
                    win['form'].cmdDelete();
                    break;
                case Event.KEY_UP:
                    var rows = listGrid.getSelectedRows() || [];
                    if(rows.length>0){
                        var row = rows[0].previous();
                        if(row) listGrid.selectRow(row);
                    }
                    break;
                case Event.KEY_DOWN:
                    var rows = listGrid.getSelectedRows() || [];
                    if(rows.length>0){
                        var row = rows[0].next();
                        if(row) listGrid.selectRow(row);
                    }
                    break;
            }
        }
    }
    
    if(!navigationEnabled) return; //***
    
    switch(event.keyCode){
        case Event.KEY_INSERT:
            addModule();
            break;
        case Event.KEY_RETURN:
            editModule();
            break;
        case Event.KEY_DELETE:
            deleteModule();
            break;
        case Event.KEY_LEFT:
            selMod = findPrevModule(selMod);
            selectModule(selMod);
            selMod.scrollTo();
            break;
        case Event.KEY_RIGHT:
            selMod = findNextModule(selMod);
            selectModule(selMod);
            selMod.scrollTo();
            break;
        case Event.KEY_UP:
        case Event.KEY_DOWN:
            break;
    }
}

//#######################################################
//#           POPUP MENU SETUP AND FUNCTIONS            #
//#######################################################

var selMod = null;
var selReg = null;

function moduleSelected(){return selMod!=null;}
function regionSelected(){return selReg!=null;}
function moduleCanBePasted(){return regionSelected() && getCookie('copyModId')!=null;}
function showFirstHR(){return moduleSelected() || moduleCanBePasted();}
function showSecondHR(){return moduleSelected() || regionSelected();}
function contentLinkSelected() { return rightClickLinkElement != null && rightClickLinkElement.href.indexOf('item=') > -1; }
function tagLinkSelected() { return rightClickLinkElement != null && rightClickLinkElement.href.indexOf('tagId=') > -1; }

popupMenu.menuItems = [
    {text:lang('Copy Module'), icon:'external/icons/copy.gif', isEnabled:moduleSelected, callback:copyModule},
    {text:lang('Paste Module'), icon:'external/icons/paste.gif', isEnabled:moduleCanBePasted, callback:pasteModule},
    {text:'-', isEnabled:showFirstHR},
    {text:lang('For This Module'), icon:'external/icons/module.png', isEnabled:moduleSelected, items:[
            {text:lang('Edit')+' (Ent)', icon:'external/icons/module_edit.png', isEnabled:moduleSelected, callback:function(){editModule();}},
            {text:lang('Delete')+' (Del)', icon:'external/icons/module_delete.png', isEnabled:moduleSelected, callback:deleteModule},
            {text:lang('Move Up'), icon:'external/icons/arrow_up.png', isEnabled:moduleSelected, callback:upModule},
            {text:lang('Move Down'), icon:'external/icons/arrow_down.png', isEnabled:moduleSelected, callback:downModule}
        ]},
    {text:lang('Add Module')+' (Ins)', icon:'external/icons/module_add.png', isEnabled:regionSelected, items:[]},
    {text:'-', isEnabled:showSecondHR},
    {text:lang('For This Page'), icon:'external/icons/general.png', items:[
            {text:lang('Edit')+'...', icon:'external/icons/page_rename.png', callback:editTemplate},
            {text:lang('Copy')+'...', icon:'external/icons/page_copy.png', callback:copyTemplate},
            {text:lang('Delete'), icon:'external/icons/page_delete.png', callback:deleteTemplate},
            {text:lang('Rename')+'...', icon:'external/icons/page_rename.png', callback:renameTemplate}
        ]},
    {text:lang('Other Pages'), icon:'external/icons/pages.png', items:[]},
    {text:lang('Export')+'...', icon:'external/icons/exportTemplate.png', callback:exportTemplate},
    {text:lang('Import')+'...', icon:'external/icons/importTemplate.png', callback:importTemplate},
    {text:'-'},
    {text:lang('Data'), icon:'external/icons/data.png', items:[]},
    {text:lang('Category-Content Tree'), icon:'external/icons/tree.png', callback:openTree},
    {text:lang('Page-Module Tree'), icon:'external/icons/tree.png', callback:openPageModuleTree},
    {text:'-'},
    {text:lang('Edit General CSS'), icon:'external/icons/css.png', callback:editStyle},
    {text:lang('Configuration'), icon:'external/icons/Configuration.png', callback:configure},
    {text:lang('Clear Cache'), icon:'external/icons/cache.gif', callback:clearCache},
    {text:lang('Edit Content'), icon: 'external/icons/edit.png', isEnabled: contentLinkSelected, callback: editContent },
    {text:lang('Edit Tag'), icon: 'external/icons/edit.png', isEnabled: tagLinkSelected, callback: editTag },
    {text:lang('Console'), icon: 'external/icons/console.png', callback: openConsole },
    {text:lang('Switch to View Mode'), icon:'external/icons/view_mode.png', callback:endDesignMode}
];
moduleTypes.each(function(mdlGrup, i){
    var items = popupMenu.menuItems[4].items;
    items[i] = {text:mdlGrup.grup, icon:'external/icons/folder_module.png', items:[]};
    mdlGrup.items.each(function(mdlTp, j){
        items[i].items[j] = {text:mdlTp.name, icon:'external/icons/'+mdlTp.id+'.png', data:mdlTp.id, callback:addModule};
    });
});
var _cntr = 0;
templates.each(function(template, i){
    if(template!=currTemplate)
        popupMenu.menuItems[7].items[_cntr++] = {text:template, icon:'external/icons/page.png', callback:function(){location.href=this.text;}};
});
_cntr = 0;
entityTypes.each(function(entTp,i){
    if(entTp[2])
        popupMenu.menuItems[11].items[_cntr++] = {text:entTp[1], icon:'external/icons/'+entTp[0]+'.png', data:entTp[0], callback:openEntityListForm};
});
popupMenu.onShow = function(){navigationEnabled = false;}
popupMenu.onHide = function(){navigationEnabled = true;}
popupMenu.setup();

var rightClickLinkElement; // sağ tıklanan linki saklamak içün

function showPopupMenu(event){
    if(Event.isLeftClick(event) || !navigationEnabled) return;

    //selReg = null;
    rightClickLinkElement = null;
    var menus = '';
    
    var elm = Event.element(event);
    if(elm.tagName=='A') rightClickLinkElement = elm; else rightClickLinkElement = elm.up('a');
    //selReg = elm.className.indexOf('Region')>-1 ? elm : elm.up('div.Region');

    popupMenu.show(Event.pointerX(event), Event.pointerY(event));
}

//#############################################################################################
//#           MODULE (ADD, EDIT, DELETE, MOVE UP/DOWN, COPY, PASTE, SAVE) FUNCTIONS           #
//#############################################################################################

function addModule(elmId){
    if(!elmId){ // if the module to be added is not defined ask it hesaaabı
        var win = new Window({className: 'alphacube', title: '<img src="external/icons/module_add.png" style="vertical-align:middle"> ' + lang('Select Module'), maximizable:false, minimizable:false, width:220, height:210, wiredDrag: true, destroyOnClose:true, showEffect:Element.show, hideEffect:Element.hide}); 
        var str = '<p align="center"><select size="10" id="selectModule">';
        moduleTypes.each(function(mdlGrup, i){
            str += '<optgroup label="'+mdlGrup.grup+'">';
            mdlGrup.items.each(function(mdlTp, j){
                str += '<option value="'+mdlTp.id+'">'+mdlTp.name+'</option>';
            });
            str += '</optgroup>';
        });
        str += '</select><br/></br><span class="btn OK" id="btnAddModuleOK">'+lang('OK')+'</span> <span class="btn cancel" id="btnAddModuleCancel">'+lang('Cancel')+'</span></p>';
        new Insertion.Top(win.getContent(), str);
        win.showCenter();
        win.toFront();
        var selCtrl = $('selectModule');
        selCtrl.selectedIndex = 0;
        selCtrl.focus();
        $('btnAddModuleOK').observe('click', function(){addModule($('selectModule').value); Windows.getFocusedWindow().close();});
        $('btnAddModuleCancel').observe('click', function(){Windows.getFocusedWindow().close();});
        return;
    }
    // add the module whose name is given by the parameter 'elmId' kafası
    var parentMdl = selReg.up('.Module');
    var parentMdlId = '0';
    if(parentMdl) parentMdlId = parentMdl.id.split('_')[1];
    new Ajax.Request('ModuleInfo.ashx?method=addModule&template='+currTemplate+'&moduleType='+elmId+'&region='+selReg.id+'&parentModuleId='+parentMdlId, {
        method: 'get',
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
            if(!selReg.down('.Module')) //selReg.innerHTML.startsWith('Empty region')
                selReg.innerHTML = '';
            new Insertion.Bottom(selReg, req.responseText);
            var newModule = selReg.immediateDescendants().last();
            Event.observe(newModule, 'mouseover', highlightModule);
            selectModule(newModule);
        },
        onException: function(req, ex){throw ex;}
    });
}


function editModule(name, id){ 
    if(!name) name = selMod.id.split('_')[0];
    if(!id) id = selMod.id.split('_')[1];
    new Ajax.Request('ModuleInfo.ashx?method=editModule&name='+name+'&id='+id, {
        method: 'get',
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
            var title = '';
            for(var i=0; i<moduleTypes.length; i++){
                var mdlType = moduleTypes[i].items.find(function(mdl){return mdl.id==name;});
                if(mdlType){title = '<img src="external/icons/'+name+'.png" style="vertical-align:middle"> ' + mdlType.name; break;}
            }
            var dim = $(document.body).getDimensions();
            var left=dim.width-390, top=10, width=350, height=dim.height-60;
            var win = new Window({className: "alphacube", title: title, left:left, top:top, width:width, height:height, wiredDrag: true, destroyOnClose:true, showEffect:Element.show, hideEffect:Element.hide}); 
            var res = null;
            try{res = eval('('+req.responseText+')');}catch(e){niceAlert(e.message);}
            var winContent = $(win.getContent());
            var pe = new EditForm(winContent, res, name, id);
            pe.onSave = saveModule;
            win['form'] = pe;
            win.show();
            win.toFront();
            
            pe.controls[0].input.select();
        },
        onException: function(req, ex){throw ex;}
    });
}

function deleteModule(event){
    niceConfirm(
        lang('The module will be deleted!'), function(){
            var name = selMod.id.split('_')[0];
            var id = selMod.id.split('_')[1];
            var prevMdl = findPrevModule(selMod);
            new Ajax.Request('ModuleInfo.ashx?method=deleteModule&name='+name+'&id='+id, {
                method: 'get',
                onComplete: function(req) {
                    if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
                    var region = $(name+'_'+id).up();
                    if(region.immediateDescendants().length==1)
                        new Insertion.Top(region, lang('Empty region') + ': ' + region.id); 
                    Element.remove(name+'_'+id);
                    selectModule(prevMdl);
                },
                onException: function(req, ex){throw ex;}
            });
        }
    );
}

function upModule(event){
    var id = selMod.id.split('_')[1];
    new Ajax.Request('ModuleInfo.ashx?method=upModule&id='+id, {
        method: 'get',
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
            var prev = selMod.previous();
            selMod = Element.remove(selMod);
            prev.up().insertBefore(selMod, prev);
            selectModule(selMod);            
        },
        onException: function(req, ex){throw ex;}
    });
}

function downModule(event){
    var id = selMod.id.split('_')[1];
    new Ajax.Request('ModuleInfo.ashx?method=downModule&id='+id, {
        method: 'get',
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
            var next = selMod.next();
            var up = selMod.up();
            selMod = Element.remove(selMod);
            if(next.next()==null) up.appendChild(selMod); else up.insertBefore(selMod, next.next());
            //new Effect.Pulsate(selMod, {duration:1.0, pulses:3});
            selectModule(selMod);            
        },
        onException: function(req, ex){throw ex;}
    });
}

function copyModule(event){
    if(!selMod) {niceAlert(lang('Module is not copied! Please right-click on the module to be copied.')); return;}
    
    var name = selMod.id.split('_')[0];
    var id = selMod.id.split('_')[1];
    setCookie('copyModId', id);
    setCookie('copyModNm', name);
}

function pasteModule(event){
    if(getCookie('copyModId')==null){
        niceAlert(lang('Copy a module first.'));
        return;
    }
    var name = getCookie('copyModNm');
    var id = getCookie('copyModId');
    var parentMdl = selReg.up('.Module');
    var parentMdlId = '0';
    if(parentMdl) parentMdlId = parentMdl.id.split('_')[1];

    new Ajax.Request('ModuleInfo.ashx?method=copyModule&name='+name+'&id='+id+'&template='+currTemplate+'&region='+selReg.id+'&parentModuleId='+parentMdlId, {
        method: 'get',
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
            if(!selReg.down('.Module')) //selReg.innerHTML.startsWith('Empty region')
                selReg.innerHTML = '';
            new Insertion.Bottom(selReg, req.responseText);
            var newModule = selReg.immediateDescendants().last();
            var ssm = new StyleSheetManager('moduleStyles');
            ssm.applyStyleSheet(ajax({url:'ModuleInfo.ashx?method=getModuleCSS&name='+name+'&id='+newModule.id.split('_')[1],isJSON:false,noCache:false}));
            Event.observe(newModule, 'mouseover', highlightModule);
            selectModule(newModule);            
        },
        onException: function(req, ex){throw ex;}
    });
}

function saveModule(pe)
{
    var params = pe.serialize();

    new Ajax.Request('ModuleInfo.ashx?method=saveModule&name='+pe.entityName+'&id='+pe.entityId, {
        method: 'post',
        parameters: params,
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
            $(pe.entityName+'_'+pe.entityId).replace(req.responseText);
            Windows.getFocusedWindow().close();
            var ssm = new StyleSheetManager('moduleStyles');
            ssm.applyStyleSheet(pe.getControl('CSS').getValue());
            var mdl = $(pe.entityName+'_'+pe.entityId);
            if(mdl){
                Event.observe(mdl, 'mouseover', highlightModule);
                //new Effect.Pulsate(mdl, {duration:1.0, pulses:3});
                selectModule(mdl);            
            }
        },
        onException: function(req, ex){throw ex;}
    });
}

function openEntityListForm(entityName, caption, extraFilter, forSelect, selectCallback){
    caption = '<img src="external/icons/'+entityName+'.png" style="vertical-align:middle"> ' + caption;
    var win = new Window({className: 'alphacube', title: caption, width:800, height:400, wiredDrag: true, destroyOnClose:true, showEffect:Element.show, hideEffect:Element.hide}); 
    var winContent = $(win.getContent());

    var options = {
        entityName: entityName,
        hrEntityName: entityTypes.find(function(item){return item[0]==entityName;})[1],
        fields: ajax({url:'EntityInfo.ashx?method=getFieldsList&entityName='+entityName,isJSON:true,noCache:false}),
        ajaxUri: 'EntityInfo.ashx',
        forSelect: forSelect,
        selectCallback: selectCallback
    }
    if(extraFilter) options.extraFilter = extraFilter;
    win['form'] = new ListForm(winContent, options);

    win.showCenter();
    win.toFront();
    
    return win;
}

//#####################################
//#          TREE FUNCTIONS           #
//#####################################

// Category-Content Tree
function openTree(){
    var win = new Window({className: 'alphacube', title: '<img src="external/icons/tree.png" style="vertical-align:middle"> ' + lang('Category-Content Tree'), top:5, left:5, width:400, height:600, wiredDrag: true, destroyOnClose:true, showEffect:Element.show, hideEffect:Element.hide}); 
    var winContent = $(win.getContent());
    winContent.setStyle({overflow:'auto'});
    win['form'] = new TreeView(winContent, 1, lang('Root'), getNodes, nodeClicked);
    win.show();
    win.toFront();
}
function getNodes(catId){
    return ajax({url:'EntityInfo.ashx?method=getTreeList&catId='+catId,isJSON:true,noCache:false});
}
function nodeClicked(node){
    if(node.type=='category')
        openEntityListForm('Content', lang('Content List'), 'CategoryId='+node.data);
    else
        editData('Content', node.data);
}

// Page-Module Tree
function openPageModuleTree(){
    var win = new Window({className: 'alphacube', title: '<img src="external/icons/tree.png" style="vertical-align:middle"> ' + lang('Page-Module Tree'), top:5, left:5, width:400, height:600, wiredDrag: true, destroyOnClose:true, showEffect:Element.show, hideEffect:Element.hide}); 
    var winContent = $(win.getContent());
    winContent.setStyle({overflow:'auto'});
    win['form'] = new TreeView(winContent, 1, lang('Root'), getModuleNodes, nodeModuleClicked);
    win.show();
    win.toFront();
}
function getModuleNodes(catId){
    if(catId==1){
        var nodes = [];
        templates.each(function(tmpl){nodes.push({data:tmpl, text:tmpl, type:'category'});});
        return nodes;
    }
    else
        return ajax({url:'ModuleInfo.ashx?method=getModuleList&page='+catId,isJSON:true,noCache:false});
}
function nodeModuleClicked(node){
    if(node.type=='category')
        editTemplate(node.data);
    else
        editModule(node.type, node.data);
}

//###################################################################
//#       EDIT & SAVE ANY DATA SUCH AS CONTENT, AUTHOR, etc..       #
//###################################################################

function editData(entityName, id){
    new Ajax.Request('EntityInfo.ashx?method=edit&entityName='+entityName+'&id='+id, {
        method: 'get',
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
            var dim = $(document.body).getDimensions();
            var left=dim.width-390, top=10, width=350, height=dim.height-60;
            var win = new Window({className: 'alphacube', title: '<img src="external/icons/'+entityName+'.png" style="vertical-align:middle"> ' + lang('Edit ' + entityName), left:left, top:top, width:width, height:height, wiredDrag: true, destroyOnClose:true, showEffect:Element.show, hideEffect:Element.hide}); 
            var res = null;
            try{res = eval('('+req.responseText+')');}catch(e){niceAlert(e.message);}
            var winContent = $(win.getContent());
            var pe = new EditForm(winContent, res, entityName, id);
            pe.onSave = saveEditForm;
            win['form'] = pe;
            win.show();
            win.toFront();
        },
        onException: function(req, ex){throw ex;}
    });
}

function saveEditForm(pe){
        var params = pe.serialize();
        new Ajax.Request('EntityInfo.ashx?method=save&entityName='+pe.entityName+'&id='+pe.entityId, {
            method: 'post',
            parameters: params,
            onComplete: function(req) {
                if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
                Windows.getFocusedWindow().close();
            },
            onException: function(req, ex){throw ex;}
        });
}

//################################################################
//#        TEMPLATE (EDIT, COPY, DELETE, RENAME) FUNCTIONS       #
//################################################################

function copyTemplate(){
    nicePrompt(lang('Enter page name'), function(name){
            if(!name || name.search(/\w+\.aspx/i)!=0){
                niceAlert(lang('Page name should be a valid file name and end with .aspx.'));
                return false;
            }
            return true;
        },
        function(name){
            var fileCopied = ajax({url:'SystemInfo.ashx?method=copyTemplate&template='+currTemplate+'&newName='+name,isJSON:false,noCache:false});
            if(fileCopied){
                window.location.href = name;
            }
        }
    );
}
function deleteTemplate(){
    niceConfirm(
        lang('This page and all the modules in it will be deleted!'),
        function(){
            var fileDeleted = ajax({url:'SystemInfo.ashx?method=deleteTemplate&template='+currTemplate,isJSON:false,noCache:false});
            if(fileDeleted){
                window.location.href = 'Main.aspx';
            }
        }
    );
}
function renameTemplate(){
    nicePrompt(lang('Enter new page name'), function(name){
            if(!name || name.search(/\w+\.aspx/i)!=0){
                niceAlert(lang('File name should be a valid file name and end with .aspx.'));
                return false;
            }
            return true;
        },
        function(name){
            var fileRenamed = ajax({url:'SystemInfo.ashx?method=renameTemplate&template='+currTemplate+'&newName='+name,isJSON:false,noCache:false});
            if(fileRenamed){
                window.location.href = name;
            }
        }
    );
}

function editTemplate(templateName){
    if(!templateName) templateName = currTemplate;
    var win = new Window({className: 'alphacube', title: '<img src="external/icons/page.png" style="vertical-align:middle"> ' + templateName, width:800, height:400, wiredDrag: true, destroyOnClose:true, showEffect:Element.show, hideEffect:Element.hide}); 
    var winContent = $(win.getContent());
    new Insertion.Bottom(winContent, '<textarea id="txtSource" onkeydown="return insertTab(event,this);" onkeyup="return insertTab(event,this);" onkeypress="return insertTab(event,this);" style="height:350px;width:100%"></textarea><p align="right"><span class="btn save" id="btnSaveTemplate">'+lang('Save')+'</span></p>');
    $('txtSource').value = ajax({url:'SystemInfo.ashx?method=getTemplateSource&template='+templateName,isJSON:false,noCache:false});
    $('btnSaveTemplate').observe('click', function(){saveTemplate(templateName);});
    win.showCenter();
    win.toFront();
}
function saveTemplate(templateName){
    var params = new Object();
    params['source'] = $('txtSource').value;
    new Ajax.Request('SystemInfo.ashx?method=saveTemplateSource&template='+templateName, {
        method: 'post',
        parameters: params,
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
            if(templateName==currTemplate)
                location.reload();
            else {
                Windows.getFocusedWindow().close();
                niceInfo(templateName + ' saved.');
            }
        },
        onException: function(req, ex){throw ex;}
    });
}

//##############################################
//#     IMPORT & EXPORT TEMPLATES (PAGES)      #
//##############################################

function exportTemplate(){
    var win = new Window({className: 'alphacube', title: '<img src="external/icons/exportTemplate.png" style="vertical-align:middle"> ' + lang('Export'), width:800, height:400, wiredDrag: true, destroyOnClose:true, showEffect:Element.show, hideEffect:Element.hide}); 
    var winContent = $(win.getContent());
    var str = '<table width="100%" height="350"><tr><td width="30%"><div id="selectedTemplatesToExport" style="width:100%;height:100%;overflow:auto">';
    templates.each(function(tmp){
        str += '<input type=checkbox value="'+tmp+'">'+tmp+'<br>';
    });
    str += '</div></td><td>';
    str += '<textarea id="txtExport" onkeydown="return insertTab(event,this);" onkeyup="return insertTab(event,this);" onkeypress="return insertTab(event,this);" style="height:350px;width:100%"></textarea>';
    str += '</td></tr></table><span class="btn OK" id="btnMakeExport">'+lang('Transfer')+' >></span>';
    new Insertion.Bottom(winContent, str);
    $('btnMakeExport').observe('click', function(){
        var selTempsToExp = '';
        $('selectedTemplatesToExport').immediateDescendants().each(function(elm){
            if(elm.checked) selTempsToExp += ','+elm.value;
        });
        $('txtExport').value = ajax({url:'SystemInfo.ashx?method=exportTemplates&templates='+selTempsToExp,isJSON:false,noCache:false});
    });
    win.showCenter();
    win.toFront();
}
function importTemplate(){
    var win = new Window({className: 'alphacube', title: '<img src="external/icons/importTemplate.png" style="vertical-align:middle"> ' + lang('Import'), width:800, height:400, wiredDrag: true, destroyOnClose:true, showEffect:Element.show, hideEffect:Element.hide}); 
    var winContent = $(win.getContent());
    new Insertion.Bottom(winContent, '<textarea id="txtImport" onkeydown="return insertTab(event,this);" onkeyup="return insertTab(event,this);" onkeypress="return insertTab(event,this);" style="height:350px;width:100%"></textarea><p align="right"><span class="btn save" id="btnSaveImport">'+lang('Save')+'</span></p>');
    $('btnSaveImport').observe('click', saveImport);
    win.showCenter();
    win.toFront();
}
function saveImport(){
    var params = new Object();
    params['templateData'] = $('txtImport').value;
    new Ajax.Request('SystemInfo.ashx?method=importTemplates', {
        method: 'post',
        parameters: params,
        onComplete: function(req) {
            if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
            Windows.getFocusedWindow().close();
            niceInfo(lang('Pages have been added.'));
        },
        onException: function(req, ex){throw ex;}
    });
}

//#####################################
//#        EDIT GENERAL CSS           #
//#####################################

function editStyle(){
    var win = new Window({className: 'alphacube', title: '<img src="external/icons/css.png" style="vertical-align:middle"> ' + lang('General CSS'), width:800, height:400, wiredDrag: true, destroyOnClose:true, showEffect:Element.show, hideEffect:Element.hide}); 
    var winContent = $(win.getContent());
    new Insertion.Bottom(winContent, '<textarea id="txtDefStyles" onkeydown="return insertTab(event,this);" onkeyup="return insertTab(event,this);" onkeypress="return insertTab(event,this);" style="height:350px;width:100%"></textarea><p align="right"><span class="btn load" id="btnModuleDefaultStyles">'+lang('Add Default Module Styles')+'</span> <span class="btn save" id="btnSaveStyles">'+lang('Save')+'</span></p>');
    $('txtDefStyles').value = ajax({url:'SystemInfo.ashx?method=getDefaultStyles',isJSON:false,noCache:false});
    $('btnSaveStyles').observe('click', saveStyle);
    $('btnModuleDefaultStyles').observe('click', function(){$('txtDefStyles').value += ajax({url:'ModuleInfo.ashx?method=getAllDefaultCSS',isJSON:false,noCache:false});});
    win.showCenter();
    win.toFront();
}
function saveStyle(){
    var params = new Object();
    params['style'] = $('txtDefStyles').value;
    new Ajax.Request('SystemInfo.ashx?method=saveDefaultStyles', {
        method: 'post',
        parameters: params,
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
            location.reload();
        },
        onException: function(req, ex){throw ex;}
    });
}

//#####################################
//#       EDIT CONFIGURATION          #
//#####################################

function configure(){
    new Ajax.Request('EntityInfo.ashx?method=edit&entityName=Configuration&id=1', {
        method: 'get',
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
            var dim = $(document.body).getDimensions();
            var left=dim.width-390, top=10, width=350, height=dim.height-60;
            var win = new Window({className: "alphacube", title: '<img src="external/icons/Configuration.png" style="vertical-align:middle"> ' + lang('Configuration'), left:left, top:top, width:width, height:height, wiredDrag: true, destroyOnClose:true, showEffect:Element.show, hideEffect:Element.hide}); 
            var res = null;
            try{res = eval('('+req.responseText+')');}catch(e){niceAlert(e.message);}
            var winContent = $(win.getContent());
            var pe = new EditForm(winContent, res, 'Configuration', 1);
            pe.onSave = saveEditForm;
            win['form'] = pe;
            win.show();
            win.toFront();
        },
        onException: function(req, ex){throw ex;}
    });
}

//###########################################################################################
//#        OTHER UTILITY (clear cache, end design mode, edit right clicked content)         #
//###########################################################################################

function clearCache(){
    new Ajax.Request('SystemInfo.ashx?method=clearCache', {
        method: 'get',
        onComplete: function(req) {
            niceInfo(req.responseText,function(){
                window.location.reload();
            });
        },
        onException: function(req, ex){throw ex;}
    });
}

function endDesignMode(){
    var url = location.href;
    if(url.indexOf('DesignMode=On')>-1)
        url = url.replace('DesignMode=On', 'DesignMode=Off');
    else if(url.indexOf('?')>-1)
        url = url + '&DesignMode=Off';
    else
        url = url + '?DesignMode=Off';
    location.href = url;
}

function editContent(){
    var params = rightClickLinkElement.href.toQueryParams();
    if(params && params.item)
        editData('Content', params.item);
}

function editTag() {
    var params = rightClickLinkElement.href.toQueryParams();
    if (params && params.tagId)
        editData('Tag', params.tagId);
}

function openConsole(){
    new Console('Console.ashx');
}

//##########################################
//#     UTILITY Functions & Classes        #
//##########################################

var Trace = Class.create(); Trace.prototype = {
    lastDt: null,
    display: null,
    initialize: function(){
        if(!traceMode) return;
        this.display = new Window({className: "alphacube", title: "Trace Log", width:450, closable:false, maximizable:false, wiredDrag: true}); 
        var dim = $(document.body).getDimensions();
        this.display.setLocation(500, dim.width-490);
        this.display.show();
        this.display.toFront();
        this.display.minimize();
        this.lastDt = new Date();
        
        var cntnt = this.display.getContent();
        cntnt.setStyle({width:'100%',overflow:'scroll'});
    },
    write: function(sender, str){
        if(!traceMode || !this.display) return;
        var dt = new Date();
        var cntnt = this.display.getContent();

        cntnt.innerHTML += ' (' +(dt-this.lastDt)+ ')<br/>' + dt.getMinutes()+':'+dt.getSeconds()+':'+dt.getMilliseconds() + ' ' + sender.id + ' : ' + str;
        cntnt.scrollTop = cntnt.scrollHeight;
        this.lastDt = dt;
    },
    warn: function(sender, str){
        if(!traceMode || !this.display) return;
        var dt = new Date();
        var cntnt = this.display.getContent();

        cntnt.innerHTML += '<br/>' + dt.getMinutes()+':'+dt.getSeconds()+':'+dt.getMilliseconds() + ' <font color=red>' + sender.id + ' : ' + str + '</font>';
        cntnt.scrollTop = cntnt.scrollHeight;
    }
}
