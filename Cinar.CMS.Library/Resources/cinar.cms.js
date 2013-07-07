var CinarCMS = {
    version: '1.0'
}

var traceMode = false;
var trace = null;
var regions = [];
var regionNames = [];
var regionDivs = [];
var navigationEnabled = true;

document.observe('dom:loaded', function(){
    try{
        trace = new Trace();
        trace.write({id:'Sistem'}, 'page load started');
		
		Windows.addObserver({
			onShow: function(){
				navigationEnabled = false;
			},
			onClose: function(){
				//if(!Windows.getFocusedWindow())
					navigationEnabled = true;
			}
		});

        if (!designMode) return; //***

        //$(document.body).style.height = '100%';
        
        regionDivs = $$('div.Region');//$('Header','Left','Content','Right','Footer').compact();
        regionDivs.each(function(elm){
            regionNames.push(elm.id);
        });
		
		var mdlSelHTML = '<div id="mdlSel">';
		mdlSelHTML += '<div><nobr>';
		mdlSelHTML += '<span class="cbtn cmodule_edit" onclick="editModule()" title="'+lang('Edit')+'"></span>';
		mdlSelHTML += '<span class="cbtn cmodule_delete" onclick="deleteModule()" title="' + lang('Delete') + '"></span>';
		mdlSelHTML += '<span class="cbtn carrow_up" onclick="upModule()" title="' + lang('Move Up') + '"></span>';
		mdlSelHTML += '<span class="cbtn carrow_down" onclick="downModule()" title="' + lang('Move Down') + '"></span>';
		mdlSelHTML += '<span class="cbtn cmodule_add" onclick="addModule()" title="' + lang('Add Module') + '"></span>';
		mdlSelHTML += '</nobr></div>';
		mdlSelHTML += '</div><div id="mdlSel2"></div><div id="mdlSel3"></div><div id="mdlSel4"></div>';
		$(document.body).insert(mdlSelHTML);

        clearFlashes();
        
        Event.observe(document, 'keydown', selectNext);
        
        mdlSel = $('mdlSel'); mdlSel2 = $('mdlSel2'); mdlSel3 = $('mdlSel3'); mdlSel4 = $('mdlSel4');
        $$('div.Module').each(function(mdl){
            Event.observe(mdl, 'mousedown', highlightModule);
        });
        selectFirstModule();
		setInterval(refreshModuleHighlighter, 2000);
        
        trace.write({id:'Sistem'}, 'page load finished');
    }
    catch(e){
        if(typeof e == 'String')
            trace.warn({id:'PageLoad'}, e);
        else
            trace.warn({id:'PageLoad'}, e.name + ' : ' + e.message);
    }
});

function clearFlashes(){
    $$('object').each(function(elm){
        new Insertion.Before(elm, '<img src="/external/icons/flash_spacer.jpg" width="'+elm.width+'" height="'+elm.height+'"/>');
        elm.remove();
    });
    $$('embed').each(function(elm){
        new Insertion.Before(elm, '<img src="/external/icons/flash_spacer.jpg" width="'+elm.width+'" height="'+elm.height+'"/>');
        elm.remove();
    });
} 

//######################################################################
//#          SELECT MODULE (select first/next/prev) FUNCTIONS          #
//######################################################################

var mdlSel = null, mdlSel2 = null, mdlSel3 = null, mdlSel4 = null;
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

	refreshModuleHighlighter();
}
function refreshModuleHighlighter(){
	if(!selMod) return;
	
    var pos = Position.cumulativeOffset(selMod);
    var dim = selMod.getDimensions();
	var mdlSelPos = Position.cumulativeOffset(mdlSel), mdlSel2Pos = Position.cumulativeOffset(mdlSel2), mdlSel3Pos = Position.cumulativeOffset(mdlSel3);
	
	if(pos[0]!=mdlSelPos[0] || pos[1]!=mdlSelPos[1] || pos[0]+dim.width-2!=mdlSel2Pos[0] || pos[1]+dim.height-2!=mdlSel3Pos[1])	{
		mdlSel.hide();mdlSel2.hide();mdlSel3.hide();mdlSel4.hide();
		mdlSel.setStyle({left:pos[0]+'px', top:pos[1]+'px', width:dim.width+'px', height:'0px'});
		mdlSel2.setStyle({left:(pos[0]+dim.width-2)+'px', top:pos[1]+'px', width:'0px', height:dim.height+'px'});
		mdlSel3.setStyle({left:pos[0]+'px', top:(pos[1]+dim.height-2)+'px', width:dim.width+'px', height:'0px'});
		mdlSel4.setStyle({left:pos[0]+'px', top:pos[1]+'px', width:'0px', height:dim.height+'px'});
		mdlSel.show();mdlSel2.show();mdlSel3.show();mdlSel4.show();
	}
    //new Effect.Appear(mdlSel, { duration: 0.1, from: 0.0, to: 0.7 });
}
function findNextModule(mdl){
    var modules = $$('div.Module');
    for(var i=0; i<modules.length; i++)
        if(modules[i]==mdl)
            if(i==modules.length-1)
                return modules[0];
            else
                return modules[i+1];
}
function findPrevModule(mdl){
    var modules = $$('div.Module');
    for(var i=0; i<modules.length; i++)
        if(modules[i]==mdl)
            if(i==0)
                return modules[modules.length-1];
            else
                return modules[i-1];
}

function selectNext(event){
    //alert(event.keyCode);
    var win = Windows.getFocusedWindow();
    
    if(win){
        //if(event.keyCode==Event.KEY_ESC)
        //    win.close();
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
    {text:lang('Copy Module'), icon:'copy', isEnabled:moduleSelected, callback:copyModule},
    {text:lang('Paste Module'), icon:'paste', isEnabled:moduleCanBePasted, callback:pasteModule},
    {text:'-', isEnabled:showFirstHR},
    {text:lang('For This Module'), icon:'module', isEnabled:moduleSelected, items:[
            {text:lang('Edit')+' (Ent)', icon:'module_edit', isEnabled:moduleSelected, callback:function(){editModule();}},
            {text:lang('Delete')+' (Del)', icon:'module_delete', isEnabled:moduleSelected, callback:deleteModule},
            {text:lang('Move Up'), icon:'arrow_up', isEnabled:moduleSelected, callback:upModule},
            {text:lang('Move Down'), icon:'arrow_down', isEnabled:moduleSelected, callback:downModule},
            {text:lang('Convert To'), icon:'module', isEnabled:moduleSelected, callback:convertModule},
			{text:lang('Export')+'...', icon:'exportTemplate', callback:exportModule}
        ]},
    {text:lang('Add Module')+' (Ins)', icon:'module_add', isEnabled:regionSelected, items:[
			{text:lang('Import')+'...', icon:'importTemplate', callback:importModule}
		]},
    {text:'-', isEnabled:showSecondHR},
    {text:lang('For This Page'), icon:'general', items:[
            {text:lang('Edit')+'...', icon:'page_rename', callback:editTemplate},
            {text:lang('Copy')+'...', icon:'page_copy', callback:copyTemplate},
            {text:lang('Delete'), icon:'page_delete', callback:deleteTemplate},
            {text:lang('Rename')+'...', icon:'page_rename', callback:renameTemplate}
        ]},
    {text:lang('Add New Page')+'...', icon:'page_copy', callback:addTemplate},
    {text:lang('Other Pages'), icon:'pages', items:[]},
    {text:lang('Export')+'...', icon:'exportTemplate', callback:exportTemplate},
    {text:lang('Import')+'...', icon:'importTemplate', callback:importTemplate},
    {text:'-'},
    {text:lang('Data'), icon:'data', items:[]},
    {text:lang('Category-Content Tree'), icon:'tree', callback:openCategoryContentTree},
    {text:lang('Page-Module Tree'), icon:'tree', callback:openPageModuleTree},
    {text:'-'},
    {text:lang('Configuration'), icon:'Configuration', callback:configure},
    { text: lang('File Manager'), icon: 'folder_module', callback: function () { openFileManager(undefined, function (path) { window.open(path, '_blank'); }); }},
    {text:lang('Edit General CSS'), icon:'css', callback:editStyle},
    {text:lang('Edit General Javascript'), icon:'script', callback:editJavascript},
    {text:lang('Edit Page Load Script'), icon:'script', callback:editPageLoadScript},
    {text:lang('Clear Cache'), icon:'cache', callback:clearCache},
    {text:lang('Edit Content'), icon: 'edit', isEnabled: contentLinkSelected, callback: editContent },
    {text:lang('Edit Tag'), icon: 'edit', isEnabled: tagLinkSelected, callback: editTag },
    {text:lang('Console'), icon: 'console', callback: openConsole },
    { text: lang('Switch to View Mode'), icon: 'view_mode', callback: endDesignMode },
    {
        text: lang('Help'), icon: 'ok', callback: function () {
            new CinarWindow({
                titleIcon: 'ok',
                title: 'Çınar CMS Documentation',
                width: 1100,
                height: 700,
                url: '/help.html.ashx',
                position:'left'
            });
        }
    }
];
moduleTypes.each(function(mdlGrup, i){
    var items = popupMenu.menuItems[4].items;
    items[i+1] = {text:mdlGrup.grup, icon:'folder_module', items:[]};
    mdlGrup.items.each(function(mdlTp, j){
        items[i+1].items[j] = {text:mdlTp.name, icon:mdlTp.id, data:mdlTp.id, callback:addModule};
    });
});
var _cntr = 0;
templates.each(function(template, i){
    if(template!=currTemplate)
        popupMenu.menuItems[8].items[_cntr++] = {text:template, icon:'page', callback:function(){location.href=this.text;}};
});
_cntr = 0;
entityTypes.each(function(entTp,i){
    if(entTp[2])
        popupMenu.menuItems[12].items[_cntr++] = {text:entTp[1], icon:entTp[0], data:entTp[0], callback:openEntityListForm};
});
popupMenu.onShow = function(){
	navigationEnabled = false;
}
popupMenu.onHide = function(){
	navigationEnabled = true;
}
popupMenu.setup();

var rightClickLinkElement; // sağ tıklanan linki saklamak içün

function showPopupMenu(event){
    if(Event.isLeftClick(event) || !navigationEnabled) return;

    //selReg = null;
    rightClickLinkElement = null;
    var menus = '';
    
    var elm = Event.element(event);
    if(elm.tagName=='A') rightClickLinkElement = elm; else rightClickLinkElement = elm.up('a');
    selReg = elm.className.indexOf('Region')>-1 ? elm : elm.up('div.Region');

    popupMenu.show(Event.pointerX(event), Event.pointerY(event));
}

//#############################################################################################
//#           MODULE (ADD, EDIT, DELETE, MOVE UP/DOWN, COPY, PASTE, SAVE) FUNCTIONS           #
//#############################################################################################

function addModule(elmId){
    if(!elmId){ // if the module to be added is not defined ask it hesaaabı
        var win = new Window({ className: 'alphacube', title: '<span class="cbtn cmodule_add"></span> ' + lang('Select Module'), maximizable: false, minimizable: false, width: 220, height: 210, wiredDrag: true, destroyOnClose: true, showEffect: Element.show, hideEffect: Element.hide }); 
        var str = '<p align="center"><select size="10" id="selectModule">';
        moduleTypes.each(function(mdlGrup, i){
            str += '<optgroup label="'+mdlGrup.grup+'">';
            mdlGrup.items.each(function(mdlTp, j){
                str += '<option value="'+mdlTp.id+'">'+mdlTp.name+'</option>';
            });
            str += '</optgroup>';
        });
        str += '</select><br/></br><span class="btn cok" id="btnAddModuleOK">'+lang('OK')+'</span> <span class="btn ccancel" id="btnAddModuleCancel">'+lang('Cancel')+'</span></p>';
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
            Event.observe(newModule, 'mousedown', highlightModule);
            selectModule(newModule);
			editModule();
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
            var res = null;
            try{res = eval('('+req.responseText+')');}catch(e){niceAlert(e.message);}
            var title = '';
            for(var i=0; i<moduleTypes.length; i++){
                var mdlType = moduleTypes[i].items.find(function(mdl){return mdl.id==name;});
                if (mdlType) { title = mdlType.name; break; }
            }
			openEditForm(name, id, title, res, saveModule);
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
                        new Insertion.Top(region, "<div class=\"cs_empty_reg\">" + lang('Empty region') + ': ' + region.id + '</div>');
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
            Event.observe(newModule, 'mousedown', highlightModule);
            selectModule(newModule);            
        },
        onException: function(req, ex){throw ex;}
    });
}
function saveModule(pe){
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
                Event.observe(mdl, 'mousedown', highlightModule);
                //new Effect.Pulsate(mdl, {duration:1.0, pulses:3});
                selectModule(mdl);            
            }
        },
        onException: function(req, ex){throw ex;}
    });
}
function convertModule(elmId){
    if(!elmId){ // if the module to be converted is not defined ask it hesaaabı
        var win = new Window({ className: 'alphacube', title: '<span class="cbtn cmodule_add"></span> ' + lang('Select Module'), maximizable: false, minimizable: false, width: 220, height: 210, wiredDrag: true, destroyOnClose: true, showEffect: Element.show, hideEffect: Element.hide }); 
        var str = '<p align="center"><select size="10" id="selectModule">';
        moduleTypes.each(function(mdlGrup, i){
            str += '<optgroup label="'+mdlGrup.grup+'">';
            mdlGrup.items.each(function(mdlTp, j){
                str += '<option value="'+mdlTp.id+'">'+mdlTp.name+'</option>';
            });
            str += '</optgroup>';
        });
        str += '</select><br/></br><span class="btn cok" id="btnAddModuleOK">'+lang('OK')+'</span> <span class="btn ccancel" id="btnAddModuleCancel">'+lang('Cancel')+'</span></p>';
        new Insertion.Top(win.getContent(), str);
        win.showCenter();
        win.toFront();
        var selCtrl = $('selectModule');
        selCtrl.selectedIndex = 0;
        selCtrl.focus();
        $('btnAddModuleOK').observe('click', function(){convertModule($('selectModule').value); Windows.getFocusedWindow().close();});
        $('btnAddModuleCancel').observe('click', function(){Windows.getFocusedWindow().close();});
        return;
    }
    // convert the module whose name is given by the parameter 'elmId' kafası
	var id = selMod.id.split('_')[1];
	var name = selMod.id.split('_')[0];
    new Ajax.Request('ModuleInfo.ashx?method=convertModule&moduleType='+elmId+'&moduleName='+name+'&id='+id, {
        method: 'get',
        onComplete: function(req) {
			location.reload();
        },
        onException: function(req, ex){throw ex;}
    });
}
function exportModule(){
    var name = selMod.id.split('_')[0];
    var id = selMod.id.split('_')[1];

    new AceEditor({
        titleIcon: 'page',
        title: lang('Export Module'),
        text: ajax({ url: 'ModuleInfo.ashx?method=exportModule&name=' + name + '&id=' + id, isJSON: false, noCache: true }),
        lang: 'txt'
    });
}
function importModule(){
    var parentMdl = selReg.up('.Module');
    var parentMdlId = '0';
    if(parentMdl) parentMdlId = parentMdl.id.split('_')[1];

    new AceEditor({
        titleIcon: 'page',
        title: lang('Import Module'),
        text: 'Paste exported module code here',
        lang: 'xml',
        buttons: [{ icon: 'save', id: 'btnImportModule', text: lang('Save'), callback: function(editor){
            var params = {data: editor.getValue()};

            new Ajax.Request('ModuleInfo.ashx?method=importModule&template='+currTemplate+'&region='+selReg.id+'&parentModuleId='+parentMdlId, {
                method: 'post',
                parameters: params,
                onComplete: function(req) {
                    if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
                    location.reload();
                },
                onException: function(req, ex){throw ex;}
            });
        } }]
    });

}

function openEntityListForm(entityName, caption, extraFilter, forSelect, selectCallback, hideFilterPanel, editFormHideCategory, extraCommands, renameLabels) {

    if (typeof (entityName) == "object") {
        caption = entityName.title;
        extraFilter = entityName.extraFilter;
        forSelect = entityName.selectCallback!=null;
        selectCallback = entityName.selectCallback;
        hideFilterPanel = entityName.hideFilterPanel;
        editFormHideCategory = entityName.editFormHideCategory;
        extraCommands = entityName.extraCommands;
        renameLabels = entityName.renameLabels;
        entityName = entityName.entityName;
    }

	var lastOpenedEntityListForm = Windows.getLastWindowByCTagName('listform');

    caption = '<span class="cbtn c' + entityName + '"></span> ' + caption;
    var win = new Window({className: 'alphacube', title: caption, width:800, height:500, wiredDrag: true, destroyOnClose:true, showEffect:Element.show, hideEffect:Element.hide}); 
	win.cTagName = 'listform';
    var winContent = $(win.getContent());

    var options = {
        entityName: entityName,
        hrEntityName: entityTypes.find(function(item){return item[0]==entityName;})[1],
        fields: ajax({url:'EntityInfo.ashx?method=getFieldsList&entityName='+entityName,isJSON:true,noCache:false}),
        ajaxUri: 'EntityInfo.ashx',
        forSelect: forSelect,
        selectCallback: selectCallback,
		commands: [],
		hideFilterPanel: hideFilterPanel,
		editFormHideCategory: editFormHideCategory,
        renameLabels: renameLabels
    }
	if(entityName=='ContentPicture'){
		options.commands.push({id:'QuickLoad', icon:'DataConverter', name:'Quick Load', handler:quickLoadImages});
		options.commands.push({id:'Tagify', icon:'tag', name:'Tag Picture', handler:function(){
			var id = this.getSelectedEntityId();
			if(!id || id<=0) return;
			tagifySelectedPicture(id);
		}});
		options.commands.push({id:'Sort', icon:'sort', name:'Sort', handler:sortImages});
		options.limit = 200;
	}
	
	if(extraCommands){
		if(extraCommands.length)
			for(var i=0; i<extraCommands.length; i++) options.commands.push(extraCommands[i]);
		else
			options.commands.push(extraCommands);
	}
			
    if(extraFilter) options.extraFilter = extraFilter;
    win['form'] = new ListForm(winContent, options);

	if(lastOpenedEntityListForm!=null){
		var loc = lastOpenedEntityListForm.getLocation();
		win.setLocation(parseInt(loc.left)+80, parseInt(loc.top)+80);
		win.show();
	} 
	else
		win.showCenter();
    win.toFront();
    
    return win;
}
function quickLoadImages(contentId){
	var fm = openFileManager();
	var ths = this; // this is ListForm here

	if (contentId) // eğer contentId gelmişse, ths'i ListForm gibi yapıyoruz
	    ths = { filter: { value: 'ContentId=' + contentId }, fetchData: function () { }};

	$('fileBrowserFooter').insert('<div style="float:right;margin-top:4px"><span id="btnOK" class="btn cok">' + lang('OK') + '</span></div>');
	$('btnOK').observe('click', function(){
		var selectedFiles = fm.getSelectedFiles();
		var params = {
			values: selectedFiles.join('#NL#'),
			ContentId: ths.filter.value.split('=')[1]
		};
	    new Ajax.Request('ModuleInfo.ashx?method=insertBatch&entityName=ContentPicture&fieldName=FileName', {
			method: 'post',
			parameters: params,
			onComplete: function(req) {
				if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
				Windows.getFocusedWindow().close();
				ths.fetchData();
			},
			onException: function(req, ex){throw ex;}
		});

	});
}
function tagifySelectedPicture(id){
	var entity = ajax({url:'EntityInfo.ashx?method=getEntity&entityName=ContentPicture&id=' + id,isJSON:true,noCache:false});
	$(document.body).insert('<img id="tagifySelectedPicture" src="'+entity.FileName+'" style="display:none;cursor:crosshair"/>');
	var img = $('tagifySelectedPicture');
	setTimeout(function(){
		var tagData = entity.TagData ? eval('('+entity.TagData+')') : [];
		var win = new Window({className: 'alphacube', title: entity.FileName, width:img.getWidth()+10, height:img.getHeight()+60, wiredDrag: true, destroyOnClose:true, showEffect:Element.show, hideEffect:Element.hide}); 
		var winContent = $(win.getContent());
		winContent.insert(img.show().remove());
		winContent.insert('<p align="right"><span class="btn cok" id="btnTagifyOK">'+lang('OK')+'</span><span class="btn ccancel" id="btnTagifyCancel">'+lang('Cancel')+'</span></p>');
		
		$('btnTagifyOK').observe('click', function(){
			tagData = tagData.findAll(function(t){return t.remove!=true;});
			entity.TagData = Object.toJSON(tagData);
			new Ajax.Request('EntityInfo.ashx?method=save&entityName=ContentPicture&id=' + id, {
				method: 'post',
				parameters: entity,
				onComplete: function (req) {
					if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
					Windows.getFocusedWindow().close();
				},
				onException: function (req, ex) { throw ex; }
			});
		});
		$('btnTagifyCancel').observe('click', function(){
			Windows.getFocusedWindow().close();
		});
		tagData.each(function(tag, i){
			var x = tag.x, y = tag.y;
			winContent.insert('<div id="tag'+i+'" class="tag_bg" style="cursor:move;position:absolute;top:'+y+'px;left:'+x+'px;width:100px;background:url(/external/icons/tagbg1.png);">'+(tag.tag || '&nbsp;')+'</div>');
			new Draggable('tag'+i, {onEnd:function(){tag.x=parseInt($('tag'+i).style.left);tag.y=parseInt($('tag'+i).style.top);}});
			$('tag'+i).observe('dblclick', function(){
				var winPos = win.getLocation();
				openTagForm(winPos, tag, tagData);
			});
		});
		img.observe('click', function(event){
			var winPos = win.getLocation();
			var x = Event.pointerX(event) - parseInt(winPos.left), y = Event.pointerY(event) - parseInt(winPos.top);
			var tagId = 'tag'+tagData.length;
			winContent.insert('<div id="'+tagId+'" class="tag_bg" style="cursor:move;position:absolute;top:'+y+'px;left:'+x+'px;width:100px;background:url(/external/icons/tagbg1.png);"></div>');
			var tag = {x:x, y:y};
			tagData.push(tag);
			new Draggable(tagId, {onEnd:function(){tag.x=parseInt($(tagId).style.left);tag.y=parseInt($(tagId).style.top);}});
			openTagForm(winPos, tag, tagData);
			
			$(tagId).observe('dblclick', function(){
				var formTag = $('tagify_edit');
				openTagForm(winPos, tag, tagData);
			});
		});
		win.showCenter();
		win.toFront();
	}, 500);
}
function openTagForm(winPos, tag, tagData){
	if($('tagify_edit')) $('tagify_edit').remove();
	
	$(document.body).insert('<div id="tagify_edit" class="editor hideOnOut" style="width:200px;height:123px"><table><tr><td>Etiket:</td><td><input class="tagify_tag"/></td></tr><tr><td>Metin:</td><td><input class="tagify_text"/></td></tr><tr><td>URL:</td><td><input class="tagify_url"/></td></tr></table><p align="right"><span class="btn cok" id="btnTagifyEditOK">'+lang('OK')+'</span><span class="btn ccancel" id="btnTagifyEditDelete">'+lang('Delete')+'</span></p></div>');
	var formTag = $('tagify_edit');

	$('btnTagifyEditOK').observe('click', function(){
		tag.tag = $('tagify_edit').down('.tagify_tag').value;
		tag.text = $('tagify_edit').down('.tagify_text').value;
		tag.url = $('tagify_edit').down('.tagify_url').value;
		if(!tag.tag || tag.tag==''){
			alert('Boş etiket giremezsiniz. Etiket üzerinde görünecek olan metni yazınız.');
			return;
		}
		$('tag'+tagData.indexOf(tag)).innerHTML = tag.tag;
		$('tagify_edit').remove();
	});
	$('btnTagifyEditDelete').observe('click', function(){
		$('tag'+tagData.indexOf(tag)).remove();
		tag.remove = true;
		$('tagify_edit').remove();
	});

	formTag.setStyle({left:(tag.x+parseInt(winPos.left)-20)+'px', top:(tag.y+parseInt(winPos.top)+20)+'px', zIndex:80000});
	formTag.down('.tagify_tag').value = tag.tag || '';
	formTag.down('.tagify_text').value = tag.text || '';
	formTag.down('.tagify_url').value = tag.url || '';
}
function sortImages(){
	var ths = this; // this is ListForm here
	var table = ths.listGrid.table; 
	if(!table)
		return;
	
	var rows = table.select('tr[id]');
	if(!rows || rows.length==0)
		return;
	
	var html = '<div id="sortableList">';
	for(var i=0; i<rows.length; i++){
		var row = rows[i];
		var src = row.select('td')[2].readAttribute('value');
		html += '<div id="'+row.id+'" ondblclick="editData(\'ContentPicture\', '+row.id.split('_')[1]+')" class="sortItem" onclick="selectPic(this)"><img src="'+src+'" width="60" height="60"/></div>';
	}
	html += '</div>';
	html += '<p align="right" style="position:absolute;bottom:0px;right:0px;">';
	html += '   <span class="btn cdelete" id="btnSortImagesDelete">' + lang('Delete') + '</span>';
	html += '   <span class="btn cok" id="btnSortImagesOK">' + lang('OK') + '</span>';
	html += '   <span class="btn ccancel" id="btnSortImagesCancel">' + lang('Cancel') + '</span>';
	html += '</p>';

	var win = new Window({ className: 'alphacube', title: '<span class="cbtn csort"></span> Order Pictures', width: 1100, height: 600, wiredDrag: true, destroyOnClose: true, showEffect: Element.show, hideEffect: Element.hide }); 
    var winContent = $(win.getContent());
	winContent.insert(html);
    win.showCenter();
    win.toFront();
    $('btnSortImagesOK').observe('click', function(){
		var sortOrder = Sortable.sequence('sortableList');
		ajax({url:'EntityInfo.ashx?method=sortEntities&sortOrder='+sortOrder+'&entityName=ContentPicture',isJSON:false,noCache:true});
		Windows.getFocusedWindow().close();
		ths.fetchData();
	});
    $('btnSortImagesCancel').observe('click', function(){ths.fetchData(); Windows.getFocusedWindow().close();});
    $('btnSortImagesDelete').observe('click', function(){
		niceConfirm('Seçtiğiniz resimler bu listeden kaldırılacak. Emin misiniz?', function () {
			$$('#sortableList div.sel').each(function(elm){
				var id = elm.id.split('_')[1];
				deleteDataWithoutWarning('ContentPicture', id, function(){elm.remove();});
			});
		});
	});

	Position.includeScrollOffsets = true;
	Sortable.create('sortableList', {tag: 'div', overlap: 'horizontal', constraint: false, scroll:'sortableList',
		onChange:function(elm){ draggedPic = elm;},
		onUpdate:function(){ }
	});
}
var draggedPic = null;
function selectPic(pic){
	pic = $(pic);
	if(pic!=draggedPic)
		pic.toggleClassName('sel');
	draggedPic = null;
}

//#####################################
//#          TREE FUNCTIONS           #
//#####################################

// Category-Content Tree
function openCategoryContentTree() {
    var win = new Window({ className: 'alphacube', title: '<span class="cbtn ctree"></span> ' + lang('Category-Content Tree'), top: 5, left: 5, width: 400, height: 600, wiredDrag: true, destroyOnClose: true, showEffect: Element.show, hideEffect: Element.hide }); 
    var winContent = $(win.getContent());
    winContent.setStyle({overflow:'auto'});
    win['form'] = new TreeView(winContent, 1, lang('Root'), getNodes, nodeClicked);
    win.show();
    win.toFront();
}
function getNodes(catId){
    return ajax({url:'EntityInfo.ashx?method=getTreeList&catId='+catId,isJSON:true,noCache:true});
}
function nodeClicked(node){
    if(node.type=='category')
        openEntityListForm('Content', lang('Content List') + ' ('+node.text+')', 'CategoryId='+node.data);
    else
        editData('Content', node.data);
}

// Page-Module Tree
function openPageModuleTree(){
    var win = new Window({ className: 'alphacube', title: '<span class="cbtn ctree"></span> ' + lang('Page-Module Tree'), top: 5, left: 5, width: 400, height: 600, wiredDrag: true, destroyOnClose: true, showEffect: Element.show, hideEffect: Element.hide }); 
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
        return ajax({url:'ModuleInfo.ashx?method=getModuleList&page='+catId,isJSON:true,noCache:true});
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

function editData(entityName, id, hideCategory, callback, filter, title, renameLabels, showRelatedEntities) {
    new Ajax.Request('EntityInfo.ashx?method='+(id>0 ? 'edit' : 'new')+'&entityName='+entityName+'&id='+id, {
        method: 'get',
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
            var res = null;
            try{res = eval('('+req.responseText+')');}catch(e){niceAlert(e.message);}
			openEditForm(
				entityName, 
				id,
				title ? title : (id <= 0 ? lang('New') + ' ' + entityName : lang('Edit ' + entityName)), 
				res, 
				function(pe){ if(id>0) saveEditForm(pe, callback); else insertEditForm(pe, callback); }, 
				filter,
                hideCategory,
                renameLabels,
                showRelatedEntities);
        },
        onException: function(req, ex){throw ex;}
    });
}
function openEntityEditForm(options) {
    editData(options.entityName, options.id, options.hideCategory, options.callback, options.filter,
        options.title, options.renameLabels, options.showRelatedEntities);
}
function deleteData(entityName, id, callback){
	niceConfirm(lang('The record will be deleted!'), function () {
		deleteDataWithoutWarning(entityName, id, callback);
	});
}
function deleteDataWithoutWarning(entityName, id, callback){
	if(!id || id<=0) return;
	new Ajax.Request('EntityInfo.ashx?method=delete&entityName=' + entityName + '&id=' + id, {
		method: 'get',
		onComplete: function (req) {
			if(req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
			if(callback) callback();
		},
		onException: function (req, ex) { throw ex; }
	});
}
function saveEditForm(pe, callback){
	var params = pe.serialize();
	new Ajax.Request('EntityInfo.ashx?method=save&entityName='+pe.entityName+'&id='+pe.entityId, {
		method: 'post',
		parameters: params,
		onComplete: function(req) {
			if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
			Windows.getFocusedWindow().close();
			if(callback) callback();
		},
		onException: function(req, ex){throw ex;}
	});
}
function insertEditForm(pe, callback){
	var params = pe.serialize();
	new Ajax.Request('EntityInfo.ashx?method=insertNew&entityName='+pe.entityName, {
		method: 'post',
		parameters: params,
		onComplete: function(req) {
			if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
			Windows.getFocusedWindow().close();
			if(callback) callback();
		},
		onException: function(req, ex){throw ex;}
	});
}

//################################################################
//#        TEMPLATE (EDIT, COPY, DELETE, RENAME) FUNCTIONS       #
//################################################################

function addTemplate(){
    nicePrompt(lang('Enter page name'), function(name){
            if(!name || name.search(/\w+\.aspx/i)!=0){
                niceAlert(lang('Page name should be a valid file name and end with .aspx.'));
                return false;
            }
            return true;
        },
        function (templateName) {
            new AceEditor({
                titleIcon: 'page',
                title: templateName,
                buttons: [{ icon: 'save', id: 'btnSaveTemplate', text: lang('Save'), callback: function (editor) { saveTemplate(editor, templateName); } }],
                text: ajax({ url: 'SystemInfo.ashx?method=getLastTemplateContent', isJSON: false, noCache: true }),
                lang: 'html'
            });
        }
    );
}

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
                window.location.href = 'Default.aspx';
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
	if(templateName=='1') return;
    if(!templateName) templateName = currTemplate;

    new AceEditor({
        titleIcon: 'page',
        title: templateName,
        buttons: [{ icon: 'save', id: 'btnSaveTemplate', text: lang('Save'), callback: function (editor) { saveTemplate(editor, templateName); } }],
        text: ajax({ url: 'SystemInfo.ashx?method=getTemplateSource&template=' + templateName, isJSON: false, noCache: true }),
        lang: 'html'
    });

}
function saveTemplate(editor, templateName){
    var params = new Object();
    params['source'] = editor.getValue();
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
    new AceEditor({
        titleIcon: 'exportTemplate',
        title: lang('Export'),
        text: ajax({ url: 'SystemInfo.ashx?method=exportTemplates&templates=' + currTemplate, isJSON: false, noCache: false }),
        lang: 'xml'
    });
}
function importTemplate(){
    new AceEditor({
        titleIcon: 'importTemplate',
        title: lang('Import'),
        text: 'Paste exported page xml here',
        lang: 'xml',
        buttons: [{
            icon: 'save', id: 'btnSaveImport', text: lang('Save'), callback: function (editor) {
                var params = new Object();
                params['templateData'] = editor.getValue();
                new Ajax.Request('SystemInfo.ashx?method=importTemplates', {
                    method: 'post',
                    parameters: params,
                    onComplete: function (req) {
                        if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
                        Windows.getFocusedWindow().close();
                        niceInfo(lang('Pages have been added.'));
                    },
                    onException: function (req, ex) { throw ex; }
                });
            }
        }]
    });
}

//#####################################
//#    EDIT GENERAL CSS/Javascript    #
//#####################################

function editStyle() {
    new AceEditor({
        titleIcon: 'css',
        title: lang('General CSS'),
        text: ajax({ url: 'SystemInfo.ashx?method=getDefaultStyles', isJSON: false, noCache: true }),
        lang: 'css',
        buttons: [
            {
                icon: 'load',
                id: 'btnModuleDefaultStyles',
                text: lang('Add Default Module Styles'),
                callback: function (editor) {
                    editor.setValue(editor.getValue() + ajax({ url: 'ModuleInfo.ashx?method=getAllDefaultCSS', isJSON: false, noCache: false }));
                }
            },
            {
                icon: 'save',
                id: 'btnSaveStyles',
                text: lang('Save'),
                callback: function (editor) {
                    var params = new Object();
                    params['style'] = editor.getValue();
                    new Ajax.Request('SystemInfo.ashx?method=saveDefaultStyles', {
                        method: 'post',
                        parameters: params,
                        onComplete: function (req) {
                            if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
                            location.reload();
                        },
                        onException: function (req, ex) { throw ex; }
                    });
                }
            }
        ]
    });
}
function editJavascript(){
    new AceEditor({
        titleIcon: 'script',
        title: lang('General Javascript'),
        text: ajax({ url: 'SystemInfo.ashx?method=getDefaultJavascript', isJSON: false, noCache: true }),
        lang: 'javascript',
        buttons: [{
            icon: 'save', id: 'btnSaveJavascript', text: lang('Save'), callback: function (editor) {
                var params = new Object();
                params['code'] = editor.getValue();
                new Ajax.Request('SystemInfo.ashx?method=saveDefaultJavascript', {
                    method: 'post',
                    parameters: params,
                    onComplete: function (req) {
                        if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
                        location.reload();
                    },
                    onException: function (req, ex) { throw ex; }
                });
            }
        }]
    });
}
function editPageLoadScript(){
    new AceEditor({
        titleIcon: 'script',
        title: lang('General Page Load Script'),
        text: ajax({ url: 'SystemInfo.ashx?method=getDefaultPageLoadScript', isJSON: false, noCache: true }),
        lang: 'javascript',
        buttons: [{
            icon: 'save', id: 'btnSavePageLoadScript', text: lang('Save'), callback: function (editor) {
                var params = new Object();
                params['code'] = editor.getValue();
                new Ajax.Request('SystemInfo.ashx?method=saveDefaultPageLoadScript', {
                    method: 'post',
                    parameters: params,
                    onComplete: function (req) {
                        if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
                        location.reload();
                    },
                    onException: function (req, ex) { throw ex; }
                });
            }
        }]
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
            var res = null;
            try{res = eval('('+req.responseText+')');}catch(e){niceAlert(e.message);}
			openEditForm('Configuration', 1, lang('Configuration'), res, saveEditForm);
        },
        onException: function(req, ex){throw ex;}
    });
}

//###########################################################################################
//#        OTHER UTILITY (clear cache, end design mode, edit right clicked content)         #
//###########################################################################################

function openFileManager(selectedPath, onSelectFile){
    var win = new Window({ className: 'alphacube', title: '<span class="cbtn cfolder_module.png"></span> ' + lang('File Manager'), minWidth:913,  width: 965, minHeight:350, height: 600, wiredDrag: true, destroyOnClose: true, showEffect: Element.show, hideEffect: Element.hide }); 
    var winContent = $(win.getContent());
    var fm = new FileManager({
		container:winContent,
		folder: selectedPath ? selectedPath.substring(0, selectedPath.lastIndexOf("/")) : undefined,
		onSelectFile: onSelectFile
	});
    win.showCenter();
    win.toFront();
	return fm;
}
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
