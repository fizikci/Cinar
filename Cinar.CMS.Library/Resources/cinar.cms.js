var CinarCMS = {
    version: '1.0'
};

var regions = [];
var regionNames = [];
var regionDivs = [];
var navigationEnabled = true;

$(function(){
    try{
		if(designMode){
			document.oncontextmenu = function() {return false;};
			$(document).mousedown(function(e){
				showPopupMenu(e);
			});
		}
	
		Windows.addObserver({
			onShow: function(){
				navigationEnabled = false;
			},
			onClose: function(){
				if(!(Windows.getFocusedWindow() && Windows.getFocusedWindow().visible))
					navigationEnabled = true;
			}
		});
        
        if(isDesigner)
		    $('.Module').each(function (eix, mdl) {
		        if ($(mdl).hasClass('StaticHtml')) $(mdl).dblclick(editStaticHtml);
		    });

        if (!designMode) return; //***

        regionDivs = $('.Region');//$('Header','Left','Content','Right','Footer').compact();
        regionDivs.each(function(eix,elm){
            regionNames.push(elm.id);
        });
		
		var mdlSelHTML = '<div id="mdlSel">';
		mdlSelHTML += '<div><nobr>';
		mdlSelHTML += '<span class="fff brick_edit" onclick="editModule()" title="'+lang('Edit')+'"></span>';
		mdlSelHTML += '<span class="fff brick_delete" onclick="deleteModule()" title="' + lang('Delete') + '"></span>';
		mdlSelHTML += '<span class="fff arrow_up" onclick="upModule()" title="' + lang('Move Up') + '"></span>';
		mdlSelHTML += '<span class="fff arrow_down" onclick="downModule()" title="' + lang('Move Down') + '"></span>';
		mdlSelHTML += '<span class="fff brick_add" onclick="addModule()" title="' + lang('Add Module') + '"></span>';
		mdlSelHTML += '</nobr></div>';
		mdlSelHTML += '</div><div id="mdlSel2"></div><div id="mdlSel3"></div><div id="mdlSel4"></div>';
		$(document.body).append(mdlSelHTML);

        clearFlashes();
        
        $(document).on('keydown', documentKeyDown);
        
        mdlSel = $('#mdlSel'); mdlSel2 = $('#mdlSel2'); mdlSel3 = $('#mdlSel3'); mdlSel4 = $('#mdlSel4');
        $('.Module').each(function(eix,mdl){
            $(mdl).mousedown(highlightModule);
        });
        selectFirstModule();
		setInterval(refreshModuleHighlighter, 2000);
    }
    catch(e){
		alert(e.toString());
    }
});

function clearFlashes(){
    $('object').each(function(eix,elm){
        $(elm).prepend('<img src="/external/icons/flash_spacer.jpg" width="'+$(elm).outerWidth()+'" height="'+$(elm).outerHeight()+'"/>');
        $(elm).remove();
    });
    $('embed').each(function(eix,elm){
        $(elm).prepend('<img src="/external/icons/flash_spacer.jpg" width="'+$(elm).outerWidth()+'" height="'+$(elm).outerHeight()+'"/>');
        $(elm).remove();
    });
} 

//######################################################################
//#          SELECT MODULE (select first/next/prev) FUNCTIONS          #
//######################################################################

var mdlSel = null, mdlSel2 = null, mdlSel3 = null, mdlSel4 = null;
function highlightModule(event){
    if(!navigationEnabled) return;
    
    var mdl = $(event.target).closest('.Module');
    selectModule(mdl);    
}
function selectFirstModule(){
    var modules = $('.Module');
    if(modules.length>0){
        selMod = $(modules[0]);
        selectModule(selMod);
    }
}
function selectModule(mdl){
    if(mdl.length==0) return; //***
    
    selMod = mdl;
    selReg = selMod.closest('.Region');

	refreshModuleHighlighter();
}
function refreshModuleHighlighter(){
	if(selMod.length==0) return;
	
    var pos = selMod.offset();
    var dim = {width:selMod.outerWidth(), height:selMod.outerHeight()};
	var mdlSelPos = mdlSel.offset(), mdlSel2Pos = mdlSel2.offset(), mdlSel3Pos = mdlSel3.offset();
	
	if(pos.left!=mdlSelPos.left || pos.top!=mdlSelPos.top || pos.left+dim.width-2!=mdlSel2Pos.left || pos.top+dim.height-2!=mdlSel3Pos.top)	{
		mdlSel.hide();mdlSel2.hide();mdlSel3.hide();mdlSel4.hide();
		mdlSel.css({left:pos.left+'px', top:pos.top+'px', width:dim.width+'px', height:'0px'});
		mdlSel2.css({left:(pos.left+dim.width-2)+'px', top:pos.top+'px', width:'0px', height:dim.height+'px'});
		mdlSel3.css({left:pos.left+'px', top:(pos.top+dim.height-2)+'px', width:dim.width+'px', height:'0px'});
		mdlSel4.css({left:pos.left+'px', top:pos.top+'px', width:'0px', height:dim.height+'px'});
		mdlSel.show();mdlSel2.show();mdlSel3.show();mdlSel4.show();
	}
}
function findNextModule(mdl){
    var modules = $('.Module');
    for(var i=0; i<modules.length; i++)
        if(modules[i]==mdl[0])
            if(i==modules.length-1)
                return $(modules[0]);
            else
                return $(modules[i+1]);
}
function findPrevModule(mdl){
    var modules = $('.Module');
    for(var i=0; i<modules.length; i++)
        if(modules[i]==mdl[0])
            if(i==0)
                return $(modules[modules.length-1]);
            else
                return $(modules[i-1]);
}

var lastFocusedInput = null;

function documentKeyDown(event){
	if(navigationEnabled && event.keyCode==93){
		popupMenu.show(100, 100);
		var selMenuItem = $("#smMenuContainer div:first div:first").addClass('menu_selected');
		return false;
	}

	if(event.keyCode==Event.KEY_ESC && currEditor){
		if(currEditor.parentControl && currEditor.parentControl.showEditor)
			currEditor.parentControl.showEditor();
		currEditor.hide();
		currEditor = null;
		event.stopPropagation();
		return false;
	}

    var win = Windows.getFocusedWindow();
    if(win && win.visible){
        if(win['form'] && win['form'].formType=='ListForm'){
            var listGrid = win['form'].listGrid;
            switch(event.keyCode){
                case Event.KEY_RETURN:
                    win['form'].cmdEdit();
                    return false;
                case Event.KEY_INSERT:
                    win['form'].cmdAdd();
                    return false;
                case Event.KEY_DELETE:
                    win['form'].cmdDelete();
                    return false;
                case Event.KEY_UP:
                    var rows = listGrid.getSelectedRows() || [];
                    if(rows.length>0){
                        var row = $(rows[0]).prev();
                        if(row) listGrid.selectRow(row);
                    }
                    return false;
                case Event.KEY_DOWN:
                    var rows = listGrid.getSelectedRows() || [];
                    if(rows.length>0){
                        var row = $(rows[0]).next();
                        if(row.length) listGrid.selectRow(row);
                    }
                    return false;
            }
        }
		
		if(win.name && win.name.startsWith('dialog_') && event.keyCode==Event.KEY_RETURN){
			win.onKeyEnter();
            return false;
		}
		
		if(event.keyCode==Event.KEY_ESC){
			if(win['form'] && win['form'].formType=='EditForm' && win['form'].isModified()){
				return false;
			}

			win.close();
			if(lastFocusedInput){
				$(lastFocusedInput).focus(); lastFocusedInput.val(lastFocusedInput.val());
				lastFocusedInput = null;
			}
			return false;
		}
		
		if(!currEditor && win['form'] && win['form'].formType=='EditForm'){
			switch(event.keyCode){
				case Event.KEY_RETURN:
					win['form'].saveClick();
					return false;
				case Event.KEY_INSERT:
					var f = $(':focus');
					if(f.length && f[0]['ctrl'] && f[0]['ctrl'].button){
						f[0]['ctrl'].button.click();
						lastFocusedInput = f[0];
						return false;
					}
					break;
			}
		}
    }
    
    if(!navigationEnabled && $("#smMenuContainer div:first").is(':visible')) {
		var selMenuItem = $("#smMenuContainer div.menu_selected:first");
		switch(event.keyCode){
			case Event.KEY_UP:
				selMenuItem.prevAll('div:visible').first().trigger('mouseover');
				return false;
			case Event.KEY_DOWN:
				selMenuItem.nextAll('div:visible').first().trigger('mouseover');
				return false;
			case Event.KEY_ESC:
				$('.hideOnOut').hide();
				navigationEnabled = true;
				$("#smMenuContainer .menu_selected").removeClass('menu_selected');
				return false;
			case Event.KEY_LEFT:
				if(selMenuItem.parent().attr('id')=='smMenu')
					return false;
				selMenuItem.parent().hide();
				var id = selMenuItem.parent().attr('id').replace('smM','m');
				var supMenu = $('#'+id);
				$("#smMenuContainer .menu_selected").removeClass('menu_selected');
				supMenu.trigger('mouseover');
				return false;
			case Event.KEY_RIGHT:
				var id = selMenuItem.attr('id').replace('m','smM');
				var subMenu = $('#'+id);
				if(subMenu.length){
					$("#smMenuContainer .menu_selected").removeClass('menu_selected');
					subMenu.find('div:visible').first().trigger('mouseover');
				}
				return false;
			case Event.KEY_RETURN:
				selMenuItem.trigger('click');
				return false;
		}
	}

    if(navigationEnabled)
		switch(event.keyCode){
			case Event.KEY_INSERT:
				addModule();
				return false;
			case Event.KEY_RETURN:
				editModule();
				return false;
			case Event.KEY_DELETE:
				deleteModule();
				return false;
			case Event.KEY_LEFT:
				selMod = findPrevModule(selMod);
				selectModule(selMod);
				$('html, body').animate({scrollTop: selMod.offset().top}, 200);
				return false;
			case Event.KEY_RIGHT:
				selMod = findNextModule(selMod);
				selectModule(selMod);
				$('html, body').animate({scrollTop: selMod.offset().top}, 200);
				return false;
			case Event.KEY_UP:
			case Event.KEY_DOWN:
				return false;
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
function contentLinkSelected() { return rightClickLinkElement.length && rightClickLinkElement.attr('href').indexOf('item=') > -1; }
function tagLinkSelected() { return rightClickLinkElement.length && rightClickLinkElement.attr('href').indexOf('tagId=') > -1; }

popupMenu.menuItems = [
    {text:lang('Copy Module'), icon:'page_white_copy', isEnabled:moduleSelected, callback:copyModule},
    {text:lang('Paste Module'), icon:'page_white_paste', isEnabled:moduleCanBePasted, callback:pasteModule},
    {text:'-', isEnabled:showFirstHR},
    {text:lang('For This Module'), icon:'brick', isEnabled:moduleSelected, items:[
            {text:lang('Edit')+' (Ent)', icon:'brick_edit', isEnabled:moduleSelected, callback:function(){editModule();}},
            {text:lang('Delete')+' (Del)', icon:'brick_delete', isEnabled:moduleSelected, callback:deleteModule},
            {text:lang('Move Up'), icon:'arrow_up', isEnabled:moduleSelected, callback:upModule},
            {text:lang('Move Down'), icon:'arrow_down', isEnabled:moduleSelected, callback:downModule},
            {text:lang('Convert To'), icon:'brick', isEnabled:moduleSelected, callback:convertModule},
			{text:lang('Export')+'...', icon:'brick_go', callback:exportModule}
        ]},
    {text:lang('Add Module')+' (Ins)', icon:'brick_add', isEnabled:regionSelected, items:[
			{text:lang('Import')+'...', icon:'brick_go', callback:importModule}
		]},
    {text:'-', isEnabled:showSecondHR},
    {text:lang('For This Page'), icon:'page', items:[
            {text:lang('Edit')+'...', icon:'page_edit', callback:editTemplate},
            {text:lang('Copy')+'...', icon:'page_copy', callback:copyTemplate},
            {text:lang('Delete'), icon:'page_delete', callback:deleteTemplate},
            {text:lang('Rename')+'...', icon:'page_edit', callback:renameTemplate},
			{text:lang('Export')+'...', icon:'page_go', callback:exportTemplate}
        ]},
    {text:lang('Add New Page')+'...', icon:'page_add', callback:addTemplate},
    {text:lang('Other Pages'), icon:'page_white_stack', items:[]},
    {text:lang('Import')+'...', icon:'page_lightning', callback:importTemplate},
    {text:'-'},
    {text:lang('Data'), icon:'database', items:[]},
    {text:lang('Category-Content Tree'), icon:'chart_organisation', callback:openCategoryContentTree},
    {text:lang('Page-Module Tree'), icon:'chart_organisation', callback:openPageModuleTree},
    {text:'-'},
    {text:lang('Configuration'), icon:'cog', items:[
			{text:lang('Site Settings'), icon:'cog', callback:configure},
			{text:lang('Edit Page Load Script'), icon:'page_white_csharp', callback:editPageLoadScript},
			{text:lang('Clear Cache'), icon:'database_delete', callback:clearCache},
			{text:lang('Export Localization')+'...', icon:'flag_green', callback:exportLocalization},
			{ text: lang('Import Localization') + '...', icon: 'flag_red', callback: importLocalization },
    		{ text: lang('Regenerate Scripts & DB'), icon: 'refresh', callback: regenerateScripts },
        ]},
    {text:lang('File Manager'), icon: 'folder_picture', callback: function () { openFileManager(undefined, function (path) { window.open(path, '_blank'); }); }},
    {text:lang('Edit General CSS'), icon:'css', callback:editStyle},
    {text:lang('Edit General Javascript'), icon:'script', callback:editJavascript},
    {text:lang('Edit Content'), icon: 'edit', isEnabled: contentLinkSelected, callback: editContent },
    {text:lang('Edit Tag'), icon: 'edit', isEnabled: tagLinkSelected, callback: editTag },
    {text:lang('Console'), icon: 'application_xp_terminal', callback: openConsole },
    {text:lang('Switch to View Mode'), icon: 'cup', callback: endDesignMode },
    {text:lang('Help'), icon: 'help', callback: function () {new CinarWindow({titleIcon: 'help', title: 'Çınar CMS Documentation', width: 1100, height: 700, maximizable:true, url: '/help.html.ashx', position:'left'});} }
];
moduleTypes.each(function(mdlGrup, i){
    var items = popupMenu.menuItems[4].items;
    items[i+1] = {text:mdlGrup.grup, icon:'folder', items:[]};
    mdlGrup.items.each(function(mdlTp, j){
        items[i+1].items[j] = {text:mdlTp.name, icon:getModuleIcon(mdlTp.id), data:mdlTp.id, callback:addModule};
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
        popupMenu.menuItems[11].items[_cntr++] = {text:entTp[1], icon:getEntityIcon(entTp[0]), data:entTp[0], callback:openEntityListForm};
});
popupMenu.onShow = function(){
	navigationEnabled = false;
}
popupMenu.onHide = function(){
	navigationEnabled = true;
}
popupMenu.setup();

var rightClickLinkElement = $(); // sağ tıklanan linki saklamak içün

function showPopupMenu(event){
    if(event.which==1 || !navigationEnabled) return;

    //selReg = null;
    rightClickLinkElement = $();
    
    var elm = $(event.target);
    if(elm[0].tagName=='A') rightClickLinkElement = elm; else rightClickLinkElement = elm.closest('a');
    selReg = elm.hasClass('Region') ? elm : elm.closest('.Region');

    popupMenu.show(event.pageX, event.pageY);
}

//#############################################################################################
//#           MODULE (ADD, EDIT, DELETE, MOVE UP/DOWN, COPY, PASTE, SAVE) FUNCTIONS           #
//#############################################################################################

function addModule(elmId){
    if(!elmId){ // if the module to be added is not defined ask it hesaaabı
        var win = new Window({ className: 'alphacube', title: '<span class="fff brick_add"></span> ' + lang('Select Module'), maximizable: false, minimizable: false, width: 220, height: 210, wiredDrag: true, destroyOnClose: true }); 
        var str = '<p align="center"><select size="10" id="selectModule">';
        moduleTypes.each(function(mdlGrup, i){
            str += '<optgroup label="'+mdlGrup.grup+'">';
            mdlGrup.items.each(function(mdlTp, j){
                str += '<option value="'+mdlTp.id+'">'+mdlTp.name+'</option>';
            });
            str += '</optgroup>';
        });
        str += '</select><br/></br><span class="ccBtn" id="btnAddModuleOK"><span class="fff accept"></span> ' + lang('OK') + '</span> <span class="ccBtn" id="btnAddModuleCancel"><span class="fff cancel"></span> ' + lang('Cancel') + '</span></p>';
        win.getContent().prepend(str);
        win.showCenter();
        win.toFront();
        var selCtrl = $('#selectModule');
        selCtrl[0].selectedIndex = 0;
        selCtrl.focus();
        $('#btnAddModuleOK').on('click', function(){addModule($('#selectModule').val()); Windows.getFocusedWindow().close();});
        $('#btnAddModuleCancel').on('click', function(){Windows.getFocusedWindow().close();});
        return;
    }
    // add the module whose name is given by the parameter 'elmId' kafası
    var parentMdl = selReg.closest('.Module');
    var parentMdlId = '0';
    if(parentMdl.length) parentMdlId = parentMdl.attr('mid').split('_')[1];
    new Ajax.Request('ModuleInfo.ashx?method=addModule&template='+currTemplate+'&moduleType='+elmId+'&region='+selReg.attr('id')+'&parentModuleId='+parentMdlId, {
        method: 'get',
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
            if(selReg.find('.Module').length==0)
                selReg.html('');
            selReg.append(req.responseText);
            var newModule = selReg.find('>').last();
            newModule.on('mousedown', highlightModule);
            selectModule(newModule);
			editModule();
        },
        onException: function(req, ex){throw ex;}
    });
}
function editModule(name, id){ 
    if(!name) name = selMod.attr('mid').split('_')[0];
    if(!id) id = selMod.attr('mid').split('_')[1];
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
            var name = selMod.attr('mid').split('_')[0];
            var id = selMod.attr('mid').split('_')[1];
            var prevMdl = findPrevModule(selMod);
            new Ajax.Request('ModuleInfo.ashx?method=deleteModule&name='+name+'&id='+id, {
                method: 'get',
                onComplete: function(req) {
                    if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
                    var region = $('.Module[mid=' + name + '_' + id + ']').parent();
                    if(region.find('>').length==1)
                        region.prepend("<div class=\"cs_empty_reg\">" + lang('Empty region') + ': ' + region.attr('id') + '</div>');
                    $('.Module[mid=' + name + '_' + id + ']').remove();
                    selectModule(prevMdl);
                },
                onException: function(req, ex){throw ex;}
            });
        }
    );
}
function upModule(event){
    var id = selMod.attr('mid').split('_')[1];
    new Ajax.Request('ModuleInfo.ashx?method=upModule&id='+id, {
        method: 'get',
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
            var prev = selMod.prev();
            selMod = selMod.insertBefore(prev);
            selectModule(selMod);            
        },
        onException: function(req, ex){throw ex;}
    });
}
function downModule(event){
    var id = selMod.attr('mid').split('_')[1];
    new Ajax.Request('ModuleInfo.ashx?method=downModule&id='+id, {
        method: 'get',
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
            var next = selMod.next();
            selMod = selMod.insertAfter(next);
            selectModule(selMod);            
        },
        onException: function(req, ex){throw ex;}
    });
}
function copyModule(event){
    if(!selMod) {niceAlert(lang('Module is not copied! Please right-click on the module to be copied.')); return;}
    
    var name = selMod.attr('id').split('_')[0];
    var id = selMod.attr('id').split('_')[1];
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
    var parentMdl = selReg.closest('.Module');
    var parentMdlId = '0';
    if(parentMdl.length) parentMdlId = parentMdl.attr('id').split('_')[1];

    new Ajax.Request('ModuleInfo.ashx?method=copyModule&name='+name+'&id='+id+'&template='+currTemplate+'&region='+selReg.attr('id')+'&parentModuleId='+parentMdlId, {
        method: 'get',
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
            if(selReg.find('.Module').length==0)
                selReg.html('');
            selReg.append(req.responseText);
            var newModule = selReg.find('>').last();
            var ssm = new StyleSheetManager('moduleStyles');
            ssm.applyStyleSheet(ajax({url:'ModuleInfo.ashx?method=getModuleCSS&name='+name+'&id='+newModule.attr('mid').split('_')[1],isJSON:false,noCache:false}));
            newModule.on('mousedown', highlightModule);
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
            $('#'+pe.entityName+'_'+pe.entityId).replaceWith(req.responseText);
            Windows.getFocusedWindow().close();
            var ssm = new StyleSheetManager('moduleStyles');
            ssm.applyStyleSheet(pe.getControl('CSS').getValue());
            var mdl = $('#'+pe.entityName+'_'+pe.entityId);
            if(mdl.length){
                mdl.on('mousedown', highlightModule);
                selectModule(mdl);            
            }
        },
        onException: function(req, ex){throw ex;}
    });
}
function convertModule(elmId){
    if(!elmId){ // if the module to be converted is not defined ask it hesaaabı
        var win = new Window({ className: 'alphacube', title: '<span class="fff brick_add"></span> ' + lang('Select Module'), maximizable: false, minimizable: false, width: 220, height: 230, wiredDrag: true, destroyOnClose: true }); 
        var str = '<p align="center" style="padding-top:22px"><select size="10" id="selectModule">';
        moduleTypes.each(function(mdlGrup, i){
            str += '<optgroup label="'+mdlGrup.grup+'">';
            mdlGrup.items.each(function(mdlTp, j){
                str += '<option value="'+mdlTp.id+'">'+mdlTp.name+'</option>';
            });
            str += '</optgroup>';
        });
        str += '</select><br/></br><span class="ccBtn" id="btnAddModuleOK"><span class="fff accept"></span> ' + lang('OK') + '</span> <span class="ccBtn" id="btnAddModuleCancel"><span class="fff cancel"></span> ' + lang('Cancel') + '</span></p>';
        win.getContent().prepend(str);
        win.showCenter();
        win.toFront();
        var selCtrl = $('#selectModule');
        selCtrl[0].selectedIndex = 0;
        selCtrl.focus();
        $('#btnAddModuleOK').on('click', function(){convertModule(selCtrl.val()); Windows.getFocusedWindow().close();});
        $('#btnAddModuleCancel').on('click', function(){Windows.getFocusedWindow().close();});
        return;
    }
    // convert the module whose name is given by the parameter 'elmId' kafası
	var id = selMod.attr('mid').split('_')[1];
	var name = selMod.attr('mid').split('_')[0];
    new Ajax.Request('ModuleInfo.ashx?method=convertModule&moduleType='+elmId+'&moduleName='+name+'&id='+id, {
        method: 'get',
        onComplete: function(req) {
			location.reload();
        },
        onException: function(req, ex){throw ex;}
    });
}
function exportModule(){
    var name = selMod.attr('mid').split('_')[0];
    var id = selMod.attr('mid').split('_')[1];

    new AceEditor({
        titleIcon: 'page',
        title: lang('Export Module'),
        text: ajax({ url: 'ModuleInfo.ashx?method=exportModule&name=' + name + '&id=' + id, isJSON: false, noCache: true }),
        lang: 'txt'
    });
}
function importModule(){
    var parentMdl = selReg.closest('.Module');
    var parentMdlId = '0';
    if(parentMdl.length) parentMdlId = parentMdl.attr('mid').split('_')[1];

    new AceEditor({
        titleIcon: 'page',
        title: lang('Import Module'),
        text: 'Paste exported module code here',
        lang: 'xml',
        buttons: [{
            icon: 'disc', type: 'primary', id: 'btnImportModule', text: lang('Save'), callback: function (editor) {
            var params = {data: editor.getValue()};

            new Ajax.Request('ModuleInfo.ashx?method=importModule&template='+currTemplate+'&region='+selReg.attr('id')+'&parentModuleId='+parentMdlId, {
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

function editStaticHtml(event) {
    if (!navigationEnabled) return;
 
    var mdl = $(event.target).closest('.Module');
    var id = mdl.attr('mid').split('_')[1];
    
    new Ajax.Request('ModuleInfo.ashx?method=editStaticHtml&id=' + id, {
        method: 'get',
        onComplete: function(req) {
            if (req.responseText.startsWith('ERR:'))
                return;
            new AceEditor({
                titleIcon: 'module',
                title: 'Edit HTML',
                buttons: [{ icon: 'save', id: 'btnSaveStaticHtml', text: lang('Save'), callback: function(editor) {
                    var params = new Object();
                    params['html'] = editor.getValue();
                    new Ajax.Request('ModuleInfo.ashx?method=saveStaticHtml&id=' + id, {
                        method: 'post',
                        parameters:params,
                        onComplete: function (req) {
                            if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
                            location.reload();
                        },
                        onException: function (req, ex) { throw ex; }
                    });
                }
                }],
                text: req.responseText,
                lang: 'html'
            });

            setTimeout("$.get('ModuleInfo.ashx?method=getHistory&id=' + " + id + ", function (data, status) { var dropdown = $('#historyx'); dropdown.empty(); var obj = jQuery.parseJSON(data); $.each(obj, function () { dropdown.append($('<option />').val(this.User).text(this.Deger)); }); });", 2000);
        },
        onException: function (req, ex) { throw ex; }
    });
}

function refreshModule(module){
	module = $(module);
	if(module.length==0)
		module = selMod;
		
    var name = module.attr('mid').split('_')[0];
    var id = module.attr('mid').split('_')[1];
	
	var queryString = location.href.indexOf('?')>-1 ? '&' + location.href.substring(location.href.indexOf('?')+1) : '';
	
    var html = ajax({ url: '/GetModuleHtml.ashx?name='+name+'&id='+id + queryString, isJSON: false, noCache: true });

    module.replaceWith(html);
	module.mousedown(highlightModule);
}

function getModuleIcon(moduleName){
    var moduleIcons = {
		'Poll':'chart_pie',
		'PollResults':'chart_curve',
		'SearchForm':'application_form_magnify',
		'SearchResults':'page_white_magnify',
		'Ajanda':'calendar',
		'LanguageList':'flag_red',
		'Chart':'chart_curve',
		'StaticHtml':'xhtml',
		'TarihSaat':'date',
		'Banner':'money',
		'ExchangeRates':'money_dollar',
		'Basket':'basket',
		'BasketSummary':'basket_edit',
		'ProductList':'ipod',
		'AuthorBox':'user_edit',
		'AuthorList':'user',
		'Manset':'page_go',
		'MansetByGrouping':'page_go',
		'AutoContentListByFilter':'page_gear',
		'ImageGallery':'photos',
		'DataList':'table',
		'Form':'application_form',
		'FormField':'textfield',
		'Grid':'table',
		'PageSecurity':'page_key',
		'SQLDataList':'database_table',
		'TagCloud':'tag_red',
		'ContentDisplay':'page_red',
		'ContentGallery':'photos',
		'ContentTools':'page_gear',
		'ContentListByTag':'tag_red',
		'ContentListByFilter':'magnify',
		'LastContents':'page',
		'SourceDetail':'house',
		'Navigation':'link',
		'NavigationWithChildren':'link',
		'WhereAmI':'link',
		'ContentPicture':'picture',
		'AuthorDetail':'user_edit',
		'ModuleRepeater':'brick_add',
		'Frame':'photo',
		'Container':'application',
		'RegionRepeater':'page_red',
		'TabView':'tab',
		'Table':'table',
		'GenericForm':'application_form',
		'ContactUs':'email',
		'Comments':'comments',
		'LoginForm2':'application_form',
		'UserActivationForm':'application_form',
		'LoginForm':'application_form',
		'PasswordForm':'application_form',
		'MembershipForm':'application_form',
		'DataConverter': 'cog',
		'GotoTopButton': 'bullet_arrow_top',
		'Checkout': 'creditcards'
	};
	return moduleIcons[moduleName];
}

//#####################################
//#          TREE FUNCTIONS           #
//#####################################

// Category-Content Tree
function openCategoryContentTree() {
    var win = new Window({ className: 'alphacube', title: '<span class="fff chart_organisation"></span> ' + lang('Category-Content Tree'), top: 63, left: 15, width: 400, height: 600, wiredDrag: true, destroyOnClose: true }); 
    var winContent = $(win.getContent());
    winContent.css({overflow:'auto'});
    win['form'] = new TreeView(winContent, 1, lang('Root'), getNodes, nodeClicked);
    win.show();
    win.toFront();
	win['form'].toggle(win['form'].rootElement);
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
    var win = new Window({ className: 'alphacube', title: '<span class="fff chart_organisation"></span> ' + lang('Page-Module Tree'), top: 63, left: 15, width: 400, height: 600, wiredDrag: true, destroyOnClose: true }); 
    var winContent = $(win.getContent());
    winContent.css({overflow:'auto'});
    win['form'] = new TreeView(winContent, -1, lang('Root'), getModuleNodes, nodeModuleClicked);
    win.show();
    win.toFront();
	win['form'].toggle(win['form'].rootElement);
}
function getModuleNodes(catId){
    if(catId==-1){
        var nodes = [];
        templates.each(function(tmpl){nodes.push({data:tmpl.split('.')[0], text:tmpl, type:'category'});});
        return nodes;
    }
    else
        return ajax({url:'ModuleInfo.ashx?method=getModuleList&page='+catId+'.aspx',isJSON:true,noCache:true});
}
function nodeModuleClicked(node){
    if(node.type=='category')
        editTemplate(node.text);
    else
        editModule(node.type, node.data);
}

//###################################################################
//#       EDIT & SAVE ANY DATA SUCH AS CONTENT, AUTHOR, etc..       #
//###################################################################

function editData(entityName, id, hideCategory, callback, filter, title, renameLabels, showRelatedEntities, defaultValues) {
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
                showRelatedEntities,
                defaultValues);
        },
        onException: function(req, ex){throw ex;}
    });
}
function openEntityEditForm(options) {
    editData(options.entityName, options.id, options.hideCategory, options.callback, options.filter,
        options.title, options.renameLabels, options.showRelatedEntities, options.defaultValues);
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
			var res = null;
		    try {
		        res = eval('(' + req.responseText + ')');
		        if (callback) callback(res);
		    } catch (e) { niceAlert(e.message); }
			
		},
		onException: function(req, ex){throw ex;}
	});
}

function readEntity(entityName, id, callback){
	readEntityList(entityName, 'Id='+id, function(list){ callback(list[0]);},'Id',1);
}
function readEntityList(entityName, filter, callback, orderBy, orderAsc) {
    orderBy = orderBy || 'OrderNo';
    orderAsc = typeof (orderAsc) == 'undefined' ? 1 : orderAsc;
    new Ajax.Request('EntityInfo.ashx?method=getEntityList&entityName='+entityName+'&filter='+filter+'&orderBy='+orderBy+'&orderAsc='+orderAsc, {
        method: 'get',
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
            var res = null;
            try{res = eval('('+req.responseText+')');}catch(e){niceAlert(e.message);}
			if(callback) callback(res);
        },
        onException: function(req, ex){throw ex;}
    });
}
function deleteEntity(entityName, id, callback, noWarning){
	if(noWarning)
		deleteDataWithoutWarning(entityName, id, callback);
	else
		deleteData(entityName, id, callback);
}
function saveEntity(entityName, entity, callback){
	var method = (typeof(entity.Id)=='undefined' || !entity.Id) ? 'insertNew' : 'save';
	new Ajax.Request('EntityInfo.ashx?method='+method+'&entityName='+entityName+'&id='+entity.Id, {
		method: 'post',
		parameters: entity,
		onComplete: function(req) {
			if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
			if(method=='insertNew') 
				entity = eval('('+req.responseText+')');
			if(callback)
				callback(entity);
		},
		onException: function(req, ex){throw ex;}
	});
}

function getEntityIcon(entityName){
    var entityIcons = {
		'AuthorLang':'user_edit', 
		'ModuleCache':'brick', 
		'SourceLang':'house', 
		'Template':'html', 
		'PollQuestion':'chart_pie', 
		'PollQuestionLang':'chart_pie',
		'PollAnswer':'chart_pie',
		'PollAnswerLang':'chart_pie',
		'BannerAd':'photo',
		'Configuration':'cog',
		'Lang':'flag_red',
		'ExchangeRate':'money',
		'Tag':'tag_red',
		'TagLang':'tag_red',
		'Content':'page_white',
		'ContentLang':'page_white',
		'ContentTag':'tag_red',
		'ContentSource':'house',
		'ContentUser':'user',
		'Source':'house',
		'User':'user',
		'Log':'database',
		'ContentPictureLang':'picture',
		'ContentPicture':'picture',
		'Recommendation':'email_go',
		'UserPrefferedAuthor':'user_edit',
		'Circulation':'chart_bar',
		'Product':'ipod',
		'Author':'user_edit',
		'UserComment':'comments',
		'ContactUs':'email'
		};
	return entityIcons[entityName];
}

function openEntityListForm(entityName, caption, extraFilter, forSelect, selectCallback, hideFilterPanel, editFormHideCategory, extraCommands, renameLabels, hideEditButtons) {

    if (typeof (entityName) == "object") {
        caption = entityName.title;
        extraFilter = entityName.extraFilter;
        forSelect = entityName.selectCallback!=null;
        selectCallback = entityName.selectCallback;
        hideFilterPanel = entityName.hideFilterPanel;
        editFormHideCategory = entityName.editFormHideCategory;
        extraCommands = entityName.extraCommands;
        renameLabels = entityName.renameLabels;
		hideEditButtons = entityName.hideEditButtons;
        entityName = entityName.entityName;
    }

	var lastOpenedEntityListForm = Windows.getLastWindowByCTagName('listform');

    caption = '<span class="fff ' + getEntityIcon(entityName) + '"></span> ' + caption;
    var win = new Window({className: 'alphacube', title: caption, width:800, height:500, wiredDrag: true, destroyOnClose:true}); 
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
        renameLabels: renameLabels,
		hideEditButtons: hideEditButtons
    }
	if(entityName=='ContentPicture'){
	    options.commands.push({id: 'QuickLoad', icon: 'lightning', name: 'Quick Load', handler: function () {
	        var contentId = parseInt(this.filter.getValue().split('=')[1]);
	        var ths = this;
	        quickLoadImages(contentId, function () { ths.fetchData(); });
        }});
		options.commands.push({id:'Tagify', icon:'tag', name:'Tag Picture', handler:function(){
			var id = this.getSelectedEntityId();
			if(!id || id<=0) return;
			tagifySelectedPicture(id);
		}});
		options.commands.push({id:'Sort', icon:'sort', name:'Sort', handler:function(){
			var contentId = parseInt(this.filter.getValue().split('=')[1]);
			showContentPictures({contentId:contentId});
		}});
		options.limit = 200;
	}
	if(entityName=='Content'){
		readEntityList('Lang', 'Id<>'+defaultLangId, function(langs){
			for(var i=0; i<langs.length; i++){
				var langId = langs[i].Id;
				var langName = langs[i].Name;
				win['form'].addCommand({id: langName, icon: 'flag_red', name: langName, handler: function () {
					var contentId = this.getSelectedEntityId();
					var langId2 = langId, langName2 = langName;
					readEntityList('ContentLang', 'ContentId='+contentId+' AND LangId='+langId2, function(cl){
						openEntityEditForm({
							entityName: 'ContentLang',
							id: cl.length>0 ? cl[0].Id : 0,
							filter: 'ContentId='+contentId+' AND LangId='+langId2,
							title: langName2 + ' Çeviri'
						});
					});
				}});
			}
		}, 'Name');
	}
	if(entityName=='User'){
	    options.commands.push({id: 'LoginWith', icon: 'user', name: 'Login with...', handler: function () {
	        var userId = this.getSelectedEntityId();
	        var ths = this;
	        readEntity('User', userId, function(u){
				if(confirm('Are you sure to change your login session?'))
					location.href = '/LoginWithKeyword.ashx?keyword='+u.Keyword;
			});
        }});
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
function quickLoadImages(contentId, callback) {
	var fm = openFileManager();
	var ths = this; // this is ListForm here

	if (!callback)
	    callback = ths.fetchData;

	if (contentId) // eğer contentId gelmişse, ths'i ListForm gibi yapıyoruz
	    ths = { filter: { value: 'ContentId=' + contentId }, fetchData: function () { }};

	$('#fileBrowserFooter').append('<div style="float:right;margin-top:4px"><span id="btnOK" class="ccBtn"><span class="fff accept"></span> ' + lang('OK') + '</span></div>');
	$('#btnOK').on('click', function(){
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
				callback();
			},
			onException: function(req, ex){throw ex;}
		});

	});
}
function tagifySelectedPicture(id){
	var entity = ajax({url:'EntityInfo.ashx?method=getEntity&entityName=ContentPicture&id=' + id,isJSON:true,noCache:false});
	$(document.body).append('<img id="tagifySelectedPicture" src="'+entity.FileName+'" style="display:none;cursor:crosshair"/>');
	var img = $('#tagifySelectedPicture');
	setTimeout(function(){
		var tagData = entity.TagData ? eval('('+entity.TagData+')') : [];
		var win = new Window({className: 'alphacube', title: entity.FileName, width:img.width()+10, height:img.height()+60, wiredDrag: true, destroyOnClose:true}); 
		var winContent = $(win.getContent());
		winContent.append(img.show().remove());
		winContent.append('<p align="right"><span class="ccBtn" id="btnTagifyOK"><span class="fff accept"></span> ' + lang('OK') + '</span><span class="ccBtn" id="btnTagifyCancel"><span class="fff cancel"></span> ' + lang('Cancel') + '</span></p>');
		
		$('#btnTagifyOK').on('click', function(){
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
		$('#btnTagifyCancel').on('click', function(){
			Windows.getFocusedWindow().close();
		});
		tagData.each(function(tag, i){
			var x = tag.x, y = tag.y;
			winContent.append('<div id="tag'+i+'" class="tag_bg" style="cursor:move;position:absolute;top:'+y+'px;left:'+x+'px;width:100px;background:url(/external/icons/tagbg1.png);">'+(tag.tag || '&nbsp;')+'</div>');
			$('#tag'+i).draggable({stop:function(){tag.x=parseInt($('#tag'+i).css('left'));tag.y=parseInt($('#tag'+i).css('top'));}});
			$('#tag'+i).on('dblclick', function(){
				var winPos = win.getLocation();
				openTagForm(winPos, tag, tagData);
			});
		});
		img.on('click', function(event){
			var winPos = win.getLocation();
			var x = event.pageX - parseInt(winPos.left), y = event.pageY - parseInt(winPos.top);
			var tagId = 'tag'+tagData.length;
			winContent.append('<div id="'+tagId+'" class="tag_bg" style="cursor:move;position:absolute;top:'+y+'px;left:'+x+'px;width:100px;background:url(/external/icons/tagbg1.png);"></div>');
			var tag = {x:x, y:y};
			tagData.push(tag);
			$('#'+tagId).draggable({stop:function(){tag.x=parseInt($('#'+tagId).css('left'));tag.y=parseInt($('#'+tagId).css('top'));}});
			openTagForm(winPos, tag, tagData);
			
			$('#'+tagId).on('dblclick', function(){
				var formTag = $('#tagify_edit');
				openTagForm(winPos, tag, tagData);
			});
		});
		win.showCenter();
		win.toFront();
	}, 500);
}
function openTagForm(winPos, tag, tagData){
	if($('#tagify_edit').length) $('#tagify_edit').remove();
	
	$(document.body).append('<div id="tagify_edit" class="editor hideOnOut" style="width:200px;height:123px"><table><tr><td>Etiket:</td><td><input class="tagify_tag"/></td></tr><tr><td>Metin:</td><td><input class="tagify_text"/></td></tr><tr><td>URL:</td><td><input class="tagify_url"/></td></tr></table><p align="right"><span class="ccBtn" id="btnTagifyEditOK"><span class="fff accept"></span> ' + lang('OK') + '</span><span class="ccBtn" id="btnTagifyEditDelete"><span class="fff cancel"></span> ' + lang('Delete') + '</span></p></div>');
	var formTag = $('#tagify_edit');

	$('#btnTagifyEditOK').on('click', function(){
		tag.tag = $('#tagify_edit .tagify_tag').val();
		tag.text = $('#tagify_edit .tagify_text').val();
		tag.url = $('#tagify_edit .tagify_url').val();
		if(!tag.tag || tag.tag==''){
			alert('Boş etiket giremezsiniz. Etiket üzerinde görünecek olan metni yazınız.');
			return;
		}
		$('#tag'+tagData.indexOf(tag)).html(tag.tag);
		$('#tagify_edit').remove();
	});
	$('#btnTagifyEditDelete').on('click', function(){
		$('#tag'+tagData.indexOf(tag)).remove();
		tag[0].remove = true;
		$('#tagify_edit').remove();
	});

	formTag.css({left:(tag.x+parseInt(winPos.left)-20)+'px', top:(tag.y+parseInt(winPos.top)+20)+'px', zIndex:80000});
	formTag.find('.tagify_tag').val(tag.tag || '');
	formTag.find('.tagify_text').val(tag.text || '');
	formTag.find('.tagify_url').val(tag.url || '');
}

function showContentPictures(options) {
    options = Object.extend({
        titleIcon: 'sort',
        title: 'Resim Galerisi',
        width: 950,
        height: 600,
        contentId: 1
    }, options || {});

    var html = '<div id="sortableList">';
    html += '</div>';
    html += '<p style="position:absolute;bottom:8px;left:8px;">';
    html += '   <span class="ccBtn" id="btnQuickLoad"><span class="fff lightning"></span> ' + lang('Quick Load') + '</span>';
    html += '   <span class="ccBtn" id="btnSortImagesEdit"><span class="fff picture_edit"></span> ' + lang('Edit Picture') + '</span>';
    html += '</p>';
    html += '<p style="position:absolute;bottom:8px;right:8px;">';
    html += '   <span class="ccBtn" id="btnSortImagesDelete"><span class="fff delete"></span> ' + lang('Delete') + '</span>';
    html += '   <span class="ccBtn" id="btnSortImagesOK"><span class="fff accept"></span> ' + lang('OK') + '</span>';
    html += '   <span class="ccBtn" id="btnSortImagesCancel"><span class="fff cancel"></span> ' + lang('Cancel') + '</span>';
    html += '</p>';

    var win = new Window({ className: 'alphacube', title: '<span class="fff '+options.titleIcon+'"></span> '+options.title, width: options.width, height: options.height, wiredDrag: true, destroyOnClose: true });
    var winContent = $(win.getContent());
    winContent.append(html);
    win.showCenter();
    win.toFront();

    $('#btnQuickLoad').on('click', function () {
        quickLoadImages(options.contentId, showImages);
    });
    $('#btnSortImagesEdit').on('click', function () {
		var selImgs = $('#sortableList div.sel img');
		if(selImgs.length>0)
			editImage(selImgs.attr('src'));
		else
			niceInfo(lang('Select a picture first'));
    });

    $('#btnSortImagesOK').on('click', function () {
        var sortOrder = $('#sortableList').sortable("serialize").replace(/[^\d]+/g,',').substring(1);
        ajax({ url: 'EntityInfo.ashx?method=sortEntities&sortOrder=' + sortOrder + '&entityName=ContentPicture', isJSON: false, noCache: true });
        Windows.getFocusedWindow().close();
    });
    $('#btnSortImagesCancel').on('click', function () { Windows.getFocusedWindow().close(); });
    $('#btnSortImagesDelete').on('click', function () {
        niceConfirm(lang('The selected pictures will be removed from this list.'), function () {
            $('#sortableList div.sel').each(function (eix,elm) {
                var id = elm.id.split('_')[1];
                deleteDataWithoutWarning('ContentPicture', id, function () { $(elm).remove(); });
            });
        });
    });

    function showImages() {
        var html = '';
        var list = ajax({ url: 'EntityInfo.ashx?method=getEntityList&entityName=ContentPicture&filter=ContentId' + encodeURI('=') + options.contentId + '&orderBy=OrderNo', isJSON: true, noCache: true });
        for (var i = 0; i < list.length; i++) {
            var row = list[i];
            var src = row.FileName;
            html += '<div id="a_' + row.Id + '" ondblclick="editData(\'ContentPicture\', ' + row.Id + ')" class="sortItem" onclick="$(this).toggleClass(\'sel\')"><img src="' + src + '" width="60" height="60"/></div>';
        }

        $('#sortableList').html('');
        $('#sortableList').append(html);

        $('#sortableList').sortable();
    }

    showImages();
}

//################################################################################
//#        TEMPLATE (EDIT, COPY, DELETE, RENAME, IMPORT, EXPORT) FUNCTIONS       #
//################################################################################

function addTemplate(){
    new AceEditor({
        titleIcon: 'page',
        title: lang("New Page"),
        buttons: [{
            icon: 'disc',
            type: 'primary',
            id: 'btnSaveTemplate',
            text: lang('Save'),
            callback: function (editor) {
                nicePrompt(lang('Enter page name'), function(name){
                    if(!name || name.search(/\w+\.aspx/i)!=0){
                        niceAlert(lang('Page name should be a valid file name and end with .aspx.'));
                        return false;
                    }
                    return true;
                },
                function (templateName) {
                    saveTemplate(editor, templateName);
                });
            }
        }],
        text: ajax({ url: 'SystemInfo.ashx?method=getLastTemplateContent', isJSON: false, noCache: true }),
        lang: 'html'
    });

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
            icon: 'disc', type: 'primary', id: 'btnSaveImport', text: lang('Save'), callback: function (editor) {
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
                icon: 'page_white_get',
                id: 'btnModuleDefaultStyles',
                text: lang('Add Default Module Styles'),
                callback: function (editor) {
                    editor.setValue(editor.getValue() + ajax({ url: 'ModuleInfo.ashx?method=getAllDefaultCSS', isJSON: false, noCache: false }));
                }
            },
            {
                icon: 'picture',
                id: 'btnAddPictureToCSS',
                text: lang('Add picture'),
                callback: function (editor) {
                    openFileManager(null, function (path) {
                        editor.insert(path);
                        Windows.getFocusedWindow().close();
                    });
                }
            },
            {
                icon: 'disc',
                type: 'primary',
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
            icon: 'disc', type: 'primary', id: 'btnSaveJavascript', text: lang('Save'), callback: function (editor) {
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
            icon: 'disc', type: 'primary', id: 'btnSavePageLoadScript', text: lang('Save'), callback: function (editor) {
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
	openEntityEditForm({
		entityName: 'Configuration',
		id:1,
		hideCategory:'Temel,Basic',
		title:'Çınar CMS Ayarlar',//lang('Configuration'),
		callback: function(){
			location.reload();
		}
	});
}

//###########################################################################################
//#        OTHER UTILITY (clear cache, end design mode, edit right clicked content)         #
//###########################################################################################

function openFileManager(selectedPath, onSelectFile){
    var win = new Window({ className: 'alphacube', title: '<span class="fff folder_picture"></span> ' + lang('File Manager'), minWidth:913,  width: 965, minHeight:350, height: 600, wiredDrag: true, destroyOnClose: true }); 
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
    var params = rightClickLinkElement.attr('href').toQueryParams();
    if(params && params.item)
        editData('Content', params.item);
}
function editTag() {
    var params = rightClickLinkElement.attr('href').toQueryParams();
    if (params && params.tagId)
        editData('Tag', params.tagId);
}
function openConsole(){
    new Console('Console.ashx');
}
function exportLocalization(){
    new AceEditor({
        titleIcon: 'flag_green',
        title: lang('Export Localization'),
        text: ajax({ url: 'SystemInfo.ashx?method=exportLocalization', isJSON: false, noCache: true }),
        lang: 'xml'
    });
}
function importLocalization(){
    new AceEditor({
        titleIcon: 'flag_red',
        title: lang('Import Localization'),
        text: 'Paste exported localization xml here',
        lang: 'xml',
        buttons: [{
            icon: 'disc', type: 'primary', id: 'btnSaveImport', text: lang('Save'), callback: function (editor) {
                var params = new Object();
                params['xmlData'] = editor.getValue();
                new Ajax.Request('SystemInfo.ashx?method=importLocalization', {
                    method: 'post',
                    parameters: params,
                    onComplete: function (req) {
                        if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
                        Windows.getFocusedWindow().close();
                        niceInfo(lang('Resources added.'));
                    },
                    onException: function (req, ex) { throw ex; }
                });
            }
        }]
    });
}
function regenerateScripts() {
    ajax({ url: 'SystemInfo.ashx?method=regenerateScripts', isJSON: false, noCache: false });
    location.reload();
}