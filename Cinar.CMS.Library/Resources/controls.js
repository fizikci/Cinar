/*
 Javascript Controls developed for Cinar.CMS
 - bulentkeskin@gmail.com or fizikci @ http://www.bilisim-kulubu.com, 8 March 2007
*/

var __letters = '!"#$%&\'()*+,-./0123456789:;<=>?@ABCÇDEFGĞHIİJKLMNOÖPQRSŞTUÜVWXYZ[\\]^_`{|}~';
var idCounter = 0;
var ctrlButton = null;
var currEditor;

//############################
//#         Control          #
//############################

var Control = Class.create(); Control.prototype = {
    hndl: 0,
    id: null,
    div: null,
    editor: null,
    editorId: null,
    value: '',
    options: null,
    input: null,
    button: null,
    description: null,
    initialize: function(id, value, options){
        idCounter++;
        this.hndl = idCounter;
		this.editorId = 'editor' + this.hndl;
        this.id = id;
        this.value = value;
        
        // option validation
        this.options = (options==null ? new Object() : options);
        if(this.options.container==null) this.options.container = document.body;
        if(this.options.onChange==null) this.options.onChange = Prototype.emptyFunction;
		var className = this.options.className ? ' class="'+this.options.className+'"' : '';
        
        var inpType = 'text';
        if(this.options.password) inpType='password';        
        
        new Insertion.Bottom(this.options.container, '<div id="bk-ctrl'+idCounter+'"><input'+className+' id="ctrlInp'+idCounter+'" type="'+inpType+'" name="'+this.id+'" style="width:100%"/><input id="ctrlBtn'+idCounter+'" type="button" style="position:absolute;display:none" value="..."/></div>');
        this.div = $('bk-ctrl'+idCounter);
        this.input = $('ctrlInp'+idCounter);
        if(this.options.readOnly) this.input.readOnly = true;
        if(this.options.width) this.input.setStyle({width:this.options.width+'px'});
        if(!this.options.password && this.value!=null) this.input.value = this.value.toString().gsub('\n','#NL#');
        this.button = $('ctrlBtn'+idCounter);

        if(this.options.hidden)
            this.div.hide();
        else {
            Event.observe(this.input, 'mouseover', this.showBtn.bind(this));
            Event.observe(this.input, 'mouseout', this.hideBtn.bind(this));
            Event.observe(this.button, 'mouseout', this.hideBtn.bind(this));
            Event.observe(this.input, 'change', this.onChange.bind(this));
            Event.observe(this.input, 'focus', this.onFocus.bind(this));
        }
    },
    onFocus: function(){
        if(this.options.onFocus)
            this.options.onFocus(this);
    },
    onChange: function(){
        if(this.options.onChange)
            this.options.onChange(this);
    },
    showBtn: function(){
        if(this.options.hideBtn) return;
        var dim = this.input.getDimensions();
        var pos = Position.positionedOffset(this.input);
        this.button.setStyle({left:(pos[0]+dim.width-20)+'px', top:(pos[1]+1)+'px', width:20+'px', height:(dim.height-2)+'px'}).show();
    },
    hideBtn: function(event){
        if(this.options.hideBtn) return;
        if(Position.within(this.button, Event.pointerX(event), Event.pointerY(event))) return;
        this.button.hide();
    },
    setEditorPos: function(editor){
        var editor = $(editor);
		currEditor = editor;
		currEditor.parentControl = this;
        var div = $(this.div);
        var dim = Position.getWindowSize();
        var pos = Position.cumulativeOffset(div);
        
        if(dim.width<pos[0]+editor.getWidth())
            editor.setStyle({left: (pos[0]+div.getWidth()-editor.getWidth())+'px'}); // , top: pos[1] + div.getHeight()
        else
            editor.setStyle({left: pos[0]+'px'}); // , top: pos[1] + div.getHeight()

        if (dim.height < pos[1] + div.getHeight() + editor.getHeight() && pos[1] - editor.getHeight()>0)
            editor.setStyle({top: (pos[1] - editor.getHeight())+'px'});
        else
            editor.setStyle({top: (pos[1] + div.getHeight())+'px'});
		
		Windows.maxZIndex += 1;
		editor.setStyle({zIndex:Windows.maxZIndex});
    },
    validate: function(){
        var maxLengthValidation = '';
        var regExValidation = '';
        var requiredValidation = '';
        if(this.options.maxLength && this.options.maxLength>0)
            maxLengthValidation = this.value.length > this.options.maxLength ? (this.label + ' alanının uzunluğu en fazla ' + this.options.maxLength + ' karakter olabilir\n') : '';
        if(this.options.regEx && this.options.regEx.length>0)
            regExValidation = new RegExp(this.options.regEx).test(this.value) ? (this.label + ' alanı için girdiğiniz değeri kontrol ediniz\n') : '';
        if(this.options.required)
            requiredValidation = (this.value==='') ? (this.label + ' alanını boş bırakamazsınız\n') : '';
        return maxLengthValidation + regExValidation + requiredValidation;
    }
}

document.observe('dom:loaded', function(){
	Event.observe(document, 'keydown', function(event){
		if(event.keyCode==Event.KEY_ESC && currEditor && currEditor.parentControl && currEditor.parentControl.showEditor){
			currEditor.parentControl.showEditor();
			currEditor = null;
		}
	});
});

//############################
//#       IntegerEdit        #
//############################

var IntegerEdit = Class.create(); IntegerEdit.prototype = {
    initialize: function(id, value, options){
        options.hideBtn = true;
        Object.extend(this, new Control(id, value, options));
        this.input.observe('keydown', this.onKeyDown.bind(this));
        this.input.setStyle({textAlign:'right'});

        this.input['ctrl'] = this;
    },
    onKeyDown: function(event){
		if(event.keyCode>=96 && event.keyCode<=105)
			return;
        switch(event.keyCode){
            case Event.KEY_END:
            case Event.KEY_HOME:
            case Event.KEY_LEFT:
            case Event.KEY_RIGHT:
            case Event.KEY_DELETE:
            case Event.KEY_BACKSPACE:
            case Event.KEY_TAB:
                break;
            default:
                var c = String.fromCharCode(event.keyCode);
                if('½-.,0123456789'.indexOf(c)==-1)
                    Event.stop(event);
                break;
        }
    },
    getValue: function(){
        return this.input.value;
    },
    setValue: function(val){
        this.input.value = val ? val : 0;
    }
}

//############################
//#       DecimalEdit        #
//############################

var DecimalEdit = Class.create(); DecimalEdit.prototype = {
    initialize: function(id, value, options){
        options.hideBtn = true;
        Object.extend(this, new Control(id, value, options));
        this.input.observe('keydown', this.onKeyDown.bind(this));
        this.input.setStyle({textAlign:'right'});

        this.input['ctrl'] = this;
    },
    onKeyDown: function(event){
		if(event.keyCode>=96 && event.keyCode<=105)
			return;
        switch(event.keyCode){
            case Event.KEY_END:
            case Event.KEY_HOME:
            case Event.KEY_LEFT:
            case Event.KEY_RIGHT:
            case Event.KEY_DELETE:
            case Event.KEY_BACKSPACE:
                break;
            default:
                var c = String.fromCharCode(event.keyCode);
                if('-.,0123456789'.indexOf(c)==-1)
                    Event.stop(event);
                break;
        }
    },
    getValue: function(){
        return this.input.value;
    },
    setValue: function(val){
        this.input.value = val ? val : 0;
    }
}

//############################
//#        StringEdit        #
//############################

var __oldBtnOKClick, __oldBtnCancelClick;
var StringEdit = Class.create();StringEdit.prototype = {
    initialize: function(id, value, options) {
        Object.extend(this, new Control(id, value, options));
        if (this.options.noHTML)
            this.options.hideBtn = true;
        else
            this.button.observe('click', this.showEditor.bind(this));

        this.input['ctrl'] = this;
    },
    showEditor: function(event) {
        if (!$(this.editorId))
            new Insertion.Bottom(document.body, '<div class="editor StringEdit" style="display:none" id="' + this.editorId + '"></div>')

		var list = $(this.editorId);
        if (list.visible()) { list.remove(); currEditor = null; return; }
        list.innerHTML = '';
		
		var wrap = getCookie('wrap');
		var nl2br = getCookie('nl2br');

        new Insertion.Bottom(list, 
								'<div>'+
									'<span class="cbtn ceditor_bold" title="Bold"></span>' +
									'<span class="cbtn ceditor_italic" title="Italic"></span>' +
									'<span class="cbtn ceditor_underline" title="Underline"></span>' +
									'<span class="cbtn ceditor_font" title="Font Size & Color"></span>' +
									'<span class="cbtn cpicture" title="Add Picture"></span>' +
									'<span class="cbtn ceditor_anchor" title="Add Link"></span>' +
									'<span class="cbtn ceye" style="margin-left:40px" title="Preview"></span>' +
									'<div style="float:right"><input type="checkbox" class="wrapCheck" '+(wrap=='1'?'checked':'')+'/> Wrap <input type="checkbox" class="nl2br" '+(nl2br=='1'?'checked':'')+'/> nl2br</div>'+
								'</div>'+
								'<textarea id="' + this.editorId + 'ta" onkeydown="return insertTab(event,this);" '+(wrap=='1'?'':'wrap="off"')+'></textarea>'+
								'<center><span class="btn cok">' + lang('OK') + '</span> <span class="btn ccancel">' + lang('Cancel') + '</span></center>');
								
        var ta = $(this.editorId + 'ta');
		ta.value = this.input.value.gsub('#NL#', '\n');

        if (this.input.disabled) return;

        var btnOK = list.down('.cok');
        var btnCancel = list.down('.ccancel');
        Event.stopObserving(btnOK, 'click', __oldBtnOKClick);
        Event.stopObserving(btnCancel, 'click', __oldBtnCancelClick);
        __oldBtnOKClick = this.setHtml.bind(this);
        __oldBtnCancelClick = this.showEditor.bind(this);
        btnOK.observe('click', __oldBtnOKClick);
        btnCancel.observe('click', __oldBtnCancelClick);
		
		var wrapCheck = list.down('.wrapCheck');
		wrapCheck.observe('click', function(){
			if(wrapCheck.checked)
				ta.writeAttribute('wrap');
			else
				ta.writeAttribute('wrap', 'off');
			setCookie('wrap', wrapCheck.checked ? 1:0);
		});
		var nl2brCheck = list.down('.nl2br');
		nl2brCheck.observe('click', function(){
			setCookie('nl2br', nl2brCheck.checked ? 1:0);
		});
		
		ta.on('keydown', function(event){
			switch(event.keyCode){
				case Event.KEY_RETURN:
					if(list.down('.nl2br').checked) TextAreaUtil.addTag(ta, '<br/>', '');
					break;
			}
		});
		
		var ths = this;
		
		list.select('.cbtn').each(function(img){
			if (img.className.indexOf('bold')>-1)
				img.observe('click', function(){TextAreaUtil.addTag(ths.editorId + 'ta', '<b>', '</b>');});
			if (img.className.indexOf('italic') > -1)
				img.observe('click', function(){TextAreaUtil.addTag(ths.editorId + 'ta', '<i>', '</i>');});
			if (img.className.indexOf('underline') > -1)
				img.observe('click', function(){TextAreaUtil.addTag(ths.editorId + 'ta', '<u>', '</u>');});
			if (img.className.indexOf('font') > -1)
				img.observe('click', function(){TextAreaUtil.addTag(ths.editorId + 'ta', '<font size="5" color="black">', '</font>');});
			if (img.className.indexOf('anchor') > -1)
				img.observe('click', function(){TextAreaUtil.addTag(ths.editorId + 'ta', '<a href="_prompt_" target="_blank">', '</a>', 'Enter link URL', 'http://');});
			if (img.className.indexOf('picture') > -1)
				img.observe('click', function(){openFileManager(null, function(path){TextAreaUtil.addTag(ths.editorId + 'ta', '<img src="'+path+'"/>', ''); Windows.getFocusedWindow().close();});});
			if (img.className.indexOf('eye') > -1)
				img.observe('click', function(){
					if(!$(ths.editorId+'Preview')){
						var dim = $(ths.editorId + 'ta').getDimensions();
						var pos = $(ths.editorId + 'ta').cumulativeOffset();
						pos = {left:pos.left+1, top:pos.top+1};
						dim = {width:dim.width-2, height:dim.height-2};
						Windows.maxZIndex++;
						$(document.body).insert('<div id="'+ths.editorId+'Preview" style="background:white;overflow:auto;text-align:left;left:'+pos.left+'px;top:'+pos.top+'px;z-index:'+Windows.maxZIndex+';width:'+dim.width+'px;height:'+dim.height+'px;position:absolute;"></div>');
					}
					$(ths.editorId+'Preview').innerHTML = $(ths.editorId + 'ta').value;
					showElementWithOverlay(ths.editorId+'Preview', true, 'black');
				});
		});

        this.setEditorPos(list);

        list.show();
        Event.stop(event);
    },
    getValue: function() {
        return this.input.value.gsub('#NL#', '\n');
    },
    setValue: function(val) {
        this.input.value = val ? val.gsub('\n', '#NL#') : '';
    },
    setHtml: function(event) {
        var list = $(this.editorId);
        this.input.value = $(this.editorId + 'ta').value.gsub('\n', '#NL#');
        list.remove();
    }
}

//############################
//#       PictureEdit        #
//############################

var PictureEdit = Class.create(); PictureEdit.prototype = {
	fileManager: null,
    initialize: function(id, value, options){
        Object.extend(this, new Control(id, value, options));
        this.button.observe('click', this.showEditor.bind(this));
        this.input['ctrl'] = this;
    },
    showEditor: function(event){
        if ($(this.editorId)) {
            $(this.editorId).remove();
			currEditor = null;
            return;
        }
        new Insertion.Bottom(document.body, '<div class="editor PictureEdit" style="display:none" id="'+this.editorId+'">' +
											'</div>');

        var list = $(this.editorId);
        if (this.input.disabled) return;
		
		var ths = this;
		
		this.fileManager = new FileManager({
								container:list,
								folder: ths.input.value!='' ? ths.input.value.substring(0, ths.input.value.lastIndexOf("/")) : undefined,
								onSelectFile: function(path){
									ths.setValue(path);
									ths.showEditor();
								},
								canDelete: true});
		fileBrowserCurrInput = this.input;
		
		$('fileBrowserFooter').insert('<div style="float:right;margin-top:4px"><span id="'+this.editorId+'btnCancel" class="btn ccancel">' + lang('Cancel') + '</span></div>');
		
        var btnCancel = $(this.editorId+'btnCancel');
        btnCancel.observe('click', this.showEditor.bind(this));

        if (this.afterShowEditor) this.afterShowEditor();

        this.setEditorPos(list);

        list.show();
        Event.stop(event);
    },
    getValue: function(){
        return this.input.value;
    },
    setValue: function(val){
        this.input.value = val ? val : '';
    }
}

var fileBrowserCurrInput, list, footer, currFolder, currPicEdit;
var FileManager = Class.create(); FileManager.prototype = {
	width: 600,
	height: 500,
	title: 'Select file',
	container: null,
	folder: '/UserFiles',
	listMode: 'pics',
	onSelectFile: null,
	canDelete: true,
	initialize: function(options){
		Object.extend(this, options);
		if(!this.folder) this.folder = '/UserFiles';
		this.container = $(this.container ? this.container : document.body);
		var html = 	'<div><div id="fileBrowserList"></div>' +
					'<div id="fileBrowserFooter">' +
						'<form action="SystemInfo.ashx?method=uploadFile" method="post" enctype="multipart/form-data" target="fakeUplFrm" class="ui-widget-content ui-corner-all">' +
							'<input type="hidden" name="folder"/>' +
							'Dosya: <input type="file" name="upload"/><input type="submit" value="Yükle"/><div id="fileBrowserLoading">&nbsp;</div>' +
							'<iframe name="fakeUplFrm"></iframe>' +
						'</form>' +
						'<form action="SystemInfo.ashx?method=createFolder" method="post" target="fakeUplFrm" class="ui-widget-content ui-corner-all">' +
							'<input type="hidden" name="folder"/>' +
							'Klasör: <input type="text" name="name" style="width:80px"/><input type="submit" value="Oluştur"/>' +
						'</form>' +
						'<form id="fileManagerRenameForm" action="SystemInfo.ashx?method=renameFile" method="post" target="fakeUplFrm" class="ui-widget-content ui-corner-all delForm">' +
							'<input type="hidden" name="folder"/><input type="hidden" name="name"/>' +
							'Adını: <input type="text" name="newName" style="width:80px"/><input type="submit" value="Değiştir"/>' +
						'</form>' +
						'<form class="ui-widget-content ui-corner-all delForm">' +
							'<input type="button" value="Düzenle" id="fb_btnImgEdit"/>' +
						'</form>' +
						(this.canDelete ? ('<form action="SystemInfo.ashx?method=deleteFile" method="post" target="fakeUplFrm" class="ui-widget-content ui-corner-all delForm">' +
							'<input type="hidden" name="folder"/>' +
							'<input type="hidden" name="name"/><input type="submit" value="Sil"/>' +
						'</form>') : '') +
					'</div></div>';
		new Insertion.Bottom(this.container, html);
		this.container.select('form').each(function(frm){
			frm.on('submit', function () {
				$('fileBrowserLoading').show();
				frm.down('input[name=folder]').setValue(currFolder);
				if(frm.hasClassName('delForm'))
					frm.down('input[name=name]').setValue($('fileBrowserList').select('.fileSelected').collect(function(elm){return elm.readAttribute('name');}).join('#NL#'));
			});
		});
		currFolder = this.folder;
		currPicEdit = this;
		this.getFileList();
		
		var ths = this;
		
		imageEditorInit();
		$('fb_btnImgEdit').on('click', function(){
			var arr = ths.getSelectedFiles();
			if(arr.length>0)
				editCurrImage(arr[0]);
		});

	},
    getFileList: function () {
		var list = $('fileBrowserList');

		if($('fileBrowserLoading')) $('fileBrowserLoading').show();
		var ths = this;
		new Ajax.Request('SystemInfo.ashx?method=getFileList&folder=' + currFolder, {
			onComplete: function (resp) {
				resp = eval("(" + resp.responseText + ")");
				if (resp.success) {
					var folders = currFolder.substring(1).split('/');
					var folderLinks = '';
					for(var i = 0; i<folders.length; i++){
						var str = '';
						for(var k = 0; k<=i; k++)
							str += '/' + folders[k];
						folderLinks += '<span onclick="currFolder = \''+str+'\'; currPicEdit.getFileList();">'+folders[i]+'</span>' + ' / ';
					}
					var str = '<div class="nav ui-widget-content ui-corner-all">' + folderLinks + '</div>';
					if (ths.listMode == 'details') {
						str = '<table class="fileList" cellspacing="0" border="0">'; //<tr><th>Ad</th><th>Boyut</th><th>Tarih</th></tr>
						for (var i = 0; i < resp.root.length; i++) {
							var item = resp.root[i];
							str += '<tr><td class="fileName ' + ths.getFileClassName(item) + (item.size < 0 ? "" : " fileItem") + '" name="' + item.name + '">' + item.name + '</td><td class="size">' + ths.getSize(item) + '</td><td class="date">' + ths.formatDate(item.date) + '</td></tr>';
						}
						str += '</table>';
					} else {
						for (var i = 0; i < resp.root.length; i++) {
							var item = resp.root[i];
							var fileClass = ths.getFileClassName(item);
							if (fileClass == 'picture') {
								var src = (currFolder + '/' + item.name);
								str += '<div class="fileNameBox ' + ths.getFileClassName(item) + (item.size < 0 ? "" : " fileItem") + ' ui-widget-content ui-corner-all" name="' + item.name + '"><img src="' + src + '"/><br/>' + item.name + '</div>';
							}
							else {
								var src = ('/external/icons/' + fileClass + '.png');
								str += '<div class="fileNameBox ' + ths.getFileClassName(item) + (item.size < 0 ? "" : " fileItem") + ' ui-widget-content ui-corner-all" name="' + item.name + '"><img src="' + src + '"/><br/>' + item.name + '</div>';
							}

						}
					}
					list.innerHTML = str;
					list.select('.folder').each(function (elm) {
						elm.on('click', function(event){
							var path = currFolder + '/' + elm.readAttribute('name');
							if(!(event.ctrlKey || event.shiftKey || event.metaKey))
								list.select('.fileNameBox').each(function(fnm){fnm.removeClassName('fileSelected');});
							elm.toggleClassName('fileSelected');
							$('fileManagerRenameForm').down('input[name=newName]').value = elm.readAttribute('name');
						});
						elm.on('dblclick', function(){
							var f = elm.readAttribute('name');
							currFolder = currFolder + '/' + f;
							ths.getFileList();
						});
					});
					list.select('.fileItem').each(function (elm) {
						elm.on('dblclick', function(){
							var path = currFolder + '/' + elm.readAttribute('name');
							if(ths.onSelectFile)
								ths.onSelectFile(path);
						});
						elm.on('click', function(event){
							var path = currFolder + '/' + elm.readAttribute('name');
							if(!(event.ctrlKey || event.shiftKey || event.metaKey))
								list.select('.fileNameBox').each(function(fnm){fnm.removeClassName('fileSelected');});
							elm.toggleClassName('fileSelected');
							$('fileManagerRenameForm').down('input[name=newName]').value = elm.readAttribute('name');
						});
					});
				}
				else
					alert(resp.errorMessage);

				$('fileBrowserLoading').hide();
			}
		});

    },
	getSelectedFiles: function(){
		return $('fileBrowserList').select('.fileSelected').collect(function(elm){return currFolder + '/' + elm.readAttribute('name');});
	},
    getSize: function (item) {
            if (item.size < 0) return ''; //***

            if (item.size >= 1024 * 1024) return Math.round(item.size / 1024 / 1024) + ' MB';
            if (item.size >= 1024) return Math.round(item.size / 1024) + ' KB';
            return item.size + ' B';
        },
    getFileClassName: function (item) {
            if (item.size == -1)
                return 'folder';
			var name = item.name.toLowerCase();
            if (name.endsWith('.png') || name.endsWith('.jpg') || name.endsWith('.jpeg') || name.endsWith('.gif') || name.endsWith('.jpe'))
                return 'picture';
            if (name.endsWith('.wmv') || name.endsWith('.mpg') || name.endsWith('.mpeg') || name.endsWith('.flv') || name.endsWith('.avi') || name.endsWith('.3gp') || name.endsWith('.rm') || name.endsWith('.mov'))
                return 'video';
            if (name.endsWith('.wav') || name.endsWith('.mp3') || name.endsWith('.wma') || name.endsWith('.mid'))
                return 'audio';
            return 'file';
        },
    formatDate:    function (d) {
            var date = d.getDate(), month = d.getMonth() + 1, hour = d.getHours(), minute = d.getMinutes();
            if (date.toString().length == 1) date = '0' + date;
            if (month.toString().length == 1) month = '0' + month;
            if (hour.toString().length == 1) hour = '0' + hour;
            if (minute.toString().length == 1) minute = '0' + minute;
            return date + '/' + month + '/' + d.getFullYear() + ' ' + hour + ':' + minute;
        }
}
fileBrowserUploadFeedback = function (msg, url) {
	//fileBrowserCurrInput.value = url;
	currPicEdit.getFileList();
}


//############################
//#          LookUp          #
//############################

var LookUp = Class.create(); LookUp.prototype = {
    text:'',
    lastValue:0,
    items:[],
    listHeight:200,
    initialize: function(id, value, options){
        Object.extend(this, new Control(id, value, options));
        this.button.observe('click', this.showEditor.bind(this));
        //this.input.observe('keyup', this.complete.bind(this));
        this.input.observe('focus', this.focus.bind(this));
        this.input.observe('blur', this.blur.bind(this));
        this.input['ctrl'] = this;

        if(this.options.entityName){
            var txt = value ? ajax({url:this.options.itemsUrl+'?method=getEntityNameValue&entityName='+this.options.entityName+'&id='+value,isJSON:false,noCache:false}) : '';
            this.setText(txt);
        }
    },
    showEditor: function(event){
        //openEntityListForm(this.options.entityName, this.label +' '+ lang('Select'), this.options.extraFilter, true, this.onSelect.bind(this));
        if ($(this.editorId)) {
            $(this.editorId).remove();
			currEditor = null;
            return;
        }

        new Insertion.Bottom(document.body, '<div class="editor LookUp" style="display:none" id="' + this.editorId + '"></div>');

        var list = $(this.editorId);
        if (this.input.disabled) return;
		
		var entityName = this.options.entityName;
		var extraFilter = this.options.extraFilter;
		var selectCallback = this.onSelect.bind(this);

	    var options = {
			entityName: entityName,
			hrEntityName: entityTypes.find(function(item){return item[0]==entityName;})[1],
			fields: ajax({url:'EntityInfo.ashx?method=getFieldsList&entityName='+entityName,isJSON:true,noCache:false}),
			ajaxUri: 'EntityInfo.ashx',
			forSelect: true,
			selectCallback: selectCallback
		}
		if(extraFilter) options.extraFilter = extraFilter;
		var lf = new ListForm(list, options);
		$('btnAdd' + lf.hndl).hide();
		$('btnEdit' + lf.hndl).hide();
		$('btnDelete' + lf.hndl).hide();
		$('btnInfo' + lf.hndl).hide();
		
		new Insertion.Bottom(list, '<center><span id="' + this.editorId + 'btnCancel" class="btn ccancel">' + lang('Cancel') + '</span></center>');

        var btnCancel = $(this.editorId + 'btnCancel');
        btnCancel.observe('click', this.showEditor.bind(this));

        if (this.afterShowEditor) this.afterShowEditor();

        this.setEditorPos(list);

        list.show();
        Event.stop(event);
    },
    complete: function(){
        if(this.input.value && this.input.value.length>=1){
            if(!this.editor){
                new Insertion.After(this.div, '<div class="editor hideOnOut" style="text-align:left;overflow:auto;position:absolute;border:1px solid black;display:none;background:white"></div>');
                this.editor = this.div.next();
            }
            var ths = this;
            var params = {extraFilter:ths.options.extraFilter+(ths.options.extraFilter?' AND ':'')+'_nameField_like'+ths.input.value+'%'};
            new Ajax.Request(ths.options.itemsUrl+'?method=getList&entityName='+ths.options.entityName, {
                method: 'post',
                parameters: params,
                onComplete: function(req) {
                    if(req.responseText.startsWith('ERR:')){
                        niceAlert(req.responseText); ths.items=[]; return;
                    }else{
                        ths.items = eval('('+req.responseText+')');
                        
                        var list = $(ths.editor);
                        list.innerHTML = '';

                        var insertionHtml = '';
                        for(var i=0;i<ths.items.length;i++){
                            var row = ths.items[i];
                            var text = typeof row == 'object' ? (row.length>1 ? row[1] : row[0]) : row;
                            insertionHtml += '<div class="item" id="__itm'+ths.hndl+'_'+i+'">'+text+'</div>';
                        }
                        new Insertion.Bottom(list, insertionHtml);
                        
                        if(list.getHeight()>ths.listHeight) list.setStyle({height:ths.listHeight+'px'});
                        var i=0;
                        var elm = $('__itm'+ths.hndl+'_'+i);
                        while(elm){
                            elm.observe('mouseover', ths.onItemMouseOver.bind(ths));
                            elm.observe('mouseout', ths.onItemMouseOut.bind(ths));
                            elm.observe('click', ths.onItemClick.bind(ths));
                            i++; elm = $('__itm'+ths.hndl+'_'+i);
                        }

                        if(!ths.listDimensionCalculated){
                            var w = ths.div.getWidth();
                            var h = list.getHeight();
                            h = h>ths.listHeight ? ths.listHeight : h;
                            list.setStyle({height:h+'px', width:w+'px'});
                            ths.listDimensionCalculated = true;
                        }
                        
                        list.show();
                    }
                },
                onException: function(req, ex){throw ex;}
            });
        }
    },
    listDimensionCalculated: false,
    onItemMouseOver: function(e){
        var elm = Event.findElement(e,'DIV');
        elm.addClassName('selItem'); 
    },
    onItemMouseOut: function(e){
        var elm = Event.findElement(e,'DIV');
        elm.removeClassName('selItem');
    },
    onItemClick: function(e){
        var elm = Event.findElement(e,'DIV');
        var list = this.editor;
        var index = list.select('.item').indexOf(elm);
        var val = this.items[index];
        this.setValue(val[0]);
        this.setText(val[1]);
        list.hide();
    },
    focus: function(){
        this.text = this.input.value;
        this.lastValue = this.value;
    },
    blur: function(){
        if(this.input.value==''){
            this.setValue(0);
            this.setText('');
            return;
        }
        if(this.text != this.input.value && this.lastValue==this.value){// text changed but value doesnt.
            //yeni id'yi oku value'ya set et. id yoksa texti eski haline çevir.
            var ths = this;
            var params = {name:ths.input.value, extraFilter:ths.options.extraFilter};
            new Ajax.Request(ths.options.itemsUrl+'?method=getEntityId&entityName='+ths.options.entityName, {
                method: 'post',
                parameters: params,
                onComplete: function(req) {
                    if(req.responseText.startsWith('ERR:')){niceAlert(req.responseText); return;}
                    try{entObj = eval('('+req.responseText+')');}catch(e){niceAlert(e.message);}
                    if(entObj && entObj.id){
                        ths.setValue(entObj.id);
                        ths.setText(entObj.name);
                    } else {
                        ths.setText(ths.text);
                    }
                },
                onException: function(req, ex){throw ex;}
            });
        }
    },
    onSelect: function(v, txt){
        this.setValue(v);
        this.setText(txt);
        $(this.editorId).remove();
    },
    getValue: function(){
        return this.value;
    },
    setValue: function(v){
        this.value = v;
    },
    setText: function(txt){
        this.input.value = txt;
    }
}

//############################
//#         MemoEdit         #
//############################

var MemoEdit = Class.create();MemoEdit.prototype = {
    initialize: function(id, value, options) {
        Object.extend(this, new Control(id, value, options));
        this.button.observe('click', this.showEditor.bind(this));
        this.input['ctrl'] = this;
    },
    showEditor: function(event) {
        if ($(this.editorId)) {
            $(this.editorId).remove();
			currEditor = null;
            return;
        }
        new Insertion.Bottom(document.body, '<div class="editor MemoEdit" style="display:none" id="' + this.editorId + '">'+
												'<textarea id="' + this.editorId + 'ta" onkeydown="return insertTab(event,this);" wrap="off"></textarea><br/>'+
												'<center>'+
													'<span id="' + this.editorId + 'btnOK" class="btn cok">' + lang('OK') + '</span> '+
													'<span id="' + this.editorId + 'btnDefault" class="btn cload">' + lang('Load default') + '</span> '+
													'<span id="' + this.editorId + 'btnPicture" class="btn cpicture">' + lang('Add picture') + '</span> '+
													'<span id="' + this.editorId + 'btnCancel" class="btn ccancel">' + lang('Cancel') + '</span>'+
												'</center>'+
											'</div>');

        var list = $(this.editorId);
        if (this.input.disabled) return;

        var btnOK = $(this.editorId + 'btnOK');
        var btnCancel = $(this.editorId + 'btnCancel');
        btnOK.observe('click', this.setValueByEditor.bind(this));
        btnCancel.observe('click', this.showEditor.bind(this));
		
		var ths = this;
		$(this.editorId + 'btnPicture').observe('click', function(){openFileManager(null, function(path){TextAreaUtil.addTag(ths.editorId + 'ta', path, ''); Windows.getFocusedWindow().close();});});

        if (this.afterShowEditor) this.afterShowEditor();

        list.down().value = this.input.value.gsub('#NL#', '\n');

        this.setEditorPos(list);

        list.show();
        Event.stop(event);
    },
    afterShowEditor: null,
    getValue: function() {
        return this.input.value.gsub('#NL#', '\n');
    },
    setValue: function(val) {
        this.input.value = val ? val.gsub('\n', '#NL#') : '';
    },
    setValueByEditor: function(event) {
        var list = $(this.editorId);
        this.input.value = $(this.editorId + 'ta').value.gsub('\n', '#NL#');
        list.remove();
    }
}

//############################
//#          CSSEdit         #
//############################

var __oldCSSBtnDefaultClick;
var CSSEdit = Class.create(); CSSEdit.prototype = {
    initialize: function(id, value, options){
        var memo = new MemoEdit(id, value, options);
        memo.afterShowEditor = this.afterShowEditor;
        memo.loadDefaultCSS = this.loadDefaultCSS.bind(memo);
        Object.extend(this, memo);
    },
    afterShowEditor: function(){
        var list = $(this.editorId);
        var btnDefault = $(this.editorId+'btnDefault');;
        Event.stopObserving(btnDefault, 'click', __oldCSSBtnDefaultClick);
        __oldCSSBtnDefaultClick = this.loadDefaultCSS.bind(this);
        btnDefault.observe('click', __oldCSSBtnDefaultClick);
    },
    loadDefaultCSS: function(event){
        var form = this.options.relatedEditForm;
        var ths = this;
        new Ajax.Request('ModuleInfo.ashx?method=getDefaultCSS&name='+form.entityName+'&id='+form.entityId, {
            method: 'get',
            asynchronous: false,
            onComplete: function(req) {
                if(req.responseText.startsWith('ERR:'))
                    niceAlert(req.responseText);
                else
                    $(ths.editorId+'ta').value = req.responseText;
            }
        });
    },
    setValue: function(val){
        this.input.value = val ? val : '';
    }
}

//############################
//#        FilterEdit        #
//############################

var __oldFilterBtnOKClick, __oldFilterBtnCancelClick;
var FilterEdit = Class.create(); FilterEdit.prototype = {
    filter: null,
    initialize: function(id, value, options){
        Object.extend(this, new Control(id, value, options));
        var filtEdit = $(this.editorId);
        if(filtEdit)
            filtEdit.down().innerHTML = '';
        this.button.observe('click', this.showEditor.bind(this));

        this.input['ctrl'] = this;
    },
    showEditor: function(event){
        if(!$(this.editorId))
            new Insertion.Bottom(document.body, '<div class="editor FilterEdit" style="display:none" id="'+this.editorId+'"><div id="'+this.editorId+'div" style="overflow:auto;height:270px"></div><center><span id="'+this.editorId+'btnOK" class="btn cok">'+lang('OK')+'</span> <span id="'+this.editorId+'btnCancel" class="btn ccancel">'+lang('Cancel')+'</span></center></div>');
        else
            $(this.editorId).down().innerHTML = "";
        var entityNameToUse = this.options.entityName;
        if(this.options.relatedEditForm && entityNameToUse.startsWith('use#')){
               entityNameToUse = entityNameToUse.substr(4);
               entityNameToUse = this.options.relatedEditForm.getControl(entityNameToUse).getValue();
        }
        
        this.filter = new FilterEditor($(this.editorId).down(), ajax({url:'EntityInfo.ashx?method=getFieldsList&entityName='+entityNameToUse,isJSON:true,noCache:false}));
            
        var list = $(this.editorId);
        if(list.visible()){list.remove(); currEditor = null; return;}
        if(this.input.disabled) return;
        
        var btnOK = $(this.editorId+'btnOK');
        var btnCancel = $(this.editorId+'btnCancel');
        Event.stopObserving(btnOK, 'click', __oldFilterBtnOKClick);
        Event.stopObserving(btnCancel, 'click', __oldFilterBtnCancelClick);
        __oldFilterBtnOKClick = this.setValueByEditor.bind(this);
        __oldFilterBtnCancelClick = this.showEditor.bind(this);
        btnOK.observe('click', __oldFilterBtnOKClick);
        btnCancel.observe('click', __oldFilterBtnCancelClick);
        
        if(this.afterShowEditor) this.afterShowEditor();
        
        this.filter.setFilters(this.input.value);
        
        this.setEditorPos(list);

        list.show();
        list.down().focus();
        Event.stop(event);
    },
    afterShowEditor: null,
    getValue: function(){
        return this.input.value;
    },
    setValue: function(val){
        this.input.value = val ? val : '';
    },
    setValueByEditor: function(){
        var list = $(this.editorId);
        var h = this.filter.serialize();
        var str = '';
        var i=0;
        while(true){
            if(!h['f_'+i]) break;
            if(i>0) str += ' AND ';
            str += h['f_'+i] + h['o_'+i] + h['c_'+i];
            i++;
        }
        this.input.value = str;
        list.remove();
    }
}

//############################
//#       DateTimeEdit       #
//############################

var __monthCombo = null;
var __yearCombo = null;
var DateTimeEdit = Class.create(); DateTimeEdit.prototype = {
    dateValue: null,
    initialize: function(id, value, options){
		if(value instanceof Date){
			this.dateValue = value;
			value = this.getValue();
		}
        Object.extend(this, new Control(id, value, options));
        this.setText(value);
        this.button.observe('click', this.showEditor.bind(this));
        this.input.observe('click', this.showEditor.bind(this));
        this.input['ctrl'] = this;
        this.input.readOnly = true;
    },
    showEditor: function(event){
        if(!$(this.editorId)){
            new Insertion.Bottom(document.body, '<div class="editor removeOnOut DateTimeEdit" style="display:none" id="'+this.editorId+'"><table width="100%"><tr><td class="cH" id="__cH1"></td><td class="cH" id="__cH2"></td></tr></table><div id="__cM"></div></div>');
            var editor = $(this.editorId);
            __monthCombo = new ComboBox('_cH1', this.dateValue.getMonth()+1, {container:$('__cH1'), width:80, listHeight:100, items:[[1,lang('January')],[2,lang('February')],[3,lang('March')],[4,lang('April')],[5,lang('May')],[6,lang('June')],[7,lang('July')],[8,lang('August')],[9,lang('September')],[10,lang('October')],[11,lang('November')],[12,lang('December')]], onChange:this.monthYearChanged.bind(this)});
            __yearCombo = new ComboBox('_cH2', this.dateValue.getFullYear(), {container:$('__cH2'), width:60, listHeight:100, items:$R(1970,(new Date()).getFullYear()+5).toArray(), onChange:this.monthYearChanged.bind(this)});
        }

        var editor = $(this.editorId);
        var ctrl = this.div;
        
        if(editor.visible()){
            editor.remove();
			currEditor = null;
            return;
        } else {
            this.buildCal(this.dateValue.getMonth()+1, this.dateValue.getFullYear());
            this.setEditorPos(editor);
            editor.show();
        }
        Event.stop(event);
    },
    monthYearChanged: function(){
        if(__monthCombo)
            this.buildCal(__monthCombo.value, __yearCombo.value);
    },
    selectDay: function(event){
        var td = Event.element(event);
        this.dateValue = new Date(__yearCombo.value, __monthCombo.value-1, td.innerHTML);
        var str = this.dateValue.toLocaleString();
        str = str.substr(0, str.length-9);
        if(this.input.value!=str){
            this.input.value = str;
            this.options.onChange();
        }
        $(this.editorId).remove();
    },
    // thanks to Brian Gosselin for the function below
    buildCal: function(m, y){
        var cM = "width:100%";
        var cH = "font-weight:bold";
        var cDW = "background:#efefef";
        var cD = "cursor:pointer";
        var mn=[lang('January'),lang('February'),lang('March'),lang('April'),lang('May'),lang('June'),lang('July'),lang('August'),lang('September'),lang('October'),lang('November'),lang('December')];
        var dim=[31,0,31,30,31,30,31,31,30,31,30,31];

        var oD = new Date(y, m-1, 1);
        oD.od=oD.getDay()+1;

        var todaydate=new Date();
        var scanfortoday=(y==todaydate.getFullYear() && m==todaydate.getMonth()+1)? todaydate.getDate() : 0;

        dim[1]=(((oD.getFullYear()%100!=0)&&(oD.getFullYear()%4==0))||(oD.getFullYear()%400==0))?29:28;
        var t='<table style="'+cM+'" cellpadding="2" border="0" cellspacing="0">';
        t+='<tr align="center">';
        for(s=0;s<7;s++)
            t+='<td style="'+cDW+'">'+lang('SMTWTFS').substr(s,1)+'</td>';
        t+='</tr><tr align="center">';
        for(i=1;i<=42;i++){
            var x=((i-oD.od>=0)&&(i-oD.od<dim[m-1]))? i-oD.od+1 : '&nbsp;';
            if (x==scanfortoday)
                x='<span style="font-weight:bold">'+x+'</span>';
            t+='<td style="'+cD+'"'+(x!='&nbsp;'?' class="calDay"':'')+'>'+x+'</td>';
            if(((i)%7==0)&&(i<36))
                t+='</tr><tr align="center">';
        }
        t+='</tr></table>';

        $(this.editorId).down('#__cM').innerHTML = t;
        var ths = this;
        $(this.editorId).select('.calDay').each(function(elm){elm.observe('click', ths.selectDay.bind(ths));});
    },
    parse: function(val){
        var dmy = val.split(' ')[0];
        var hm = val.split(' ')[1];
        dmy = dmy.split('-');
        hm = hm.split(':');
        var dt = new Date();
        dt.setDate(dmy[0]); dt.setMonth(dmy[1]-1); dt.setFullYear(dmy[2]);
        dt.setHours(hm[0]); dt.setMinutes(hm[1]);
        
        return dt;
    },
    setText: function(value){
        this.dateValue = this.parse(value);
        var str = this.dateValue.toLocaleString();
        this.input.value = str.substr(0, str.length-9);
    },
    getValue: function(){
        var d = this.dateValue;
        return this.addZero(d.getDate())+'-'+this.addZero(d.getMonth()+1)+'-'+d.getFullYear()+' '+this.addZero(d.getHours())+':'+this.addZero(d.getMinutes())+':'+this.addZero(d.getSeconds());
    },
    setValue: function(d){
        if(!d) return;
        if(typeof d == 'string'){
            this.setText(d);
            d = this.dateValue;
        }
        else 
            this.input.value = this.addZero(d.getDate())+'-'+this.addZero(d.getMonth()+1)+'-'+d.getFullYear()+' '+this.addZero(d.getHours())+':'+this.addZero(d.getMinutes())+':'+this.addZero(d.getSeconds());
    },
	addZero: function(num){
		if(num.toString().length<2)
			return '0' + num;
		return num.toString();
	}
}

//#########################
//#       ComboBox        #
//#########################

var ComboBox = Class.create(); ComboBox.prototype = {
    listHeight:200,
    initialize: function(id, value, options){
        Object.extend(this, new Control(id, value, options));
        if(options.listHeight) this.listHeight = options.listHeight;
        if(!options.hideItems){
            this.button.observe('click', this.openList.bind(this));
            this.input.observe('click', this.openList.bind(this));
        } else
            this.options.hideBtn = true;
        this.input.readOnly = true;

        if(this.options.items && this.options.addBlankItem && this.options.items[0][0]!='')
            this.options.items.unshift(['',lang('Select')]);
        var ths = this;
        if(this.options.itemsUrl){
            new Ajax.Request(this.options.itemsUrl+'?method=getList&entityName='+this.options.entityName, {
                method: 'get',
                asynchronous: false,
                onComplete: function(req) {
                    if(req.responseText.startsWith('ERR:'))
                        ths.options.items = [req.responseText.substr(4)];
                    else{
                        ths.options.items = eval('('+req.responseText+')');
                        if(ths.options.addBlankItem)
                            ths.options.items.unshift(['',lang('Select')]);
                    }
                },
                onException: function(req, ex){throw ex;}
            });
        }
        else
			this.setValue(value);

        this.input['ctrl'] = this;
    },
    beforeOpenList: function(){
        if(!this.editor){
            new Insertion.After(this.div, '<div class="editor hideOnOut ComboBox" style="display:none"></div>');
            this.editor = this.div.next();
            this.fetchData();
        }
    },
    beforeOpenListForFields: function(){
        if(!this.editor){
            new Insertion.After(this.div, '<div class="editor hideOnOut ComboBox" style="display:none"></div>');
            this.editor = this.div.next();
        }
        var entityNameToUse = this.options.entityName;
        if(entityNameToUse.startsWith('use#')){
               entityNameToUse = entityNameToUse.substr(4);
               entityNameToUse = this.options.relatedEditForm.getControl(entityNameToUse).getValue();
               if(!entityNameToUse) entityNameToUse = 'Content'; // bi default olsun hesabı... :(
        }
        if(entityNameToUse!=this.currEntityName){
            var fields = ajax({url:'EntityInfo.ashx?method=getFieldsList&entityName='+entityNameToUse,isJSON:true,noCache:false});
            var ths = this;
            this.options.items = [];
            this.options.items.push(['','Hiçbiri']);
            fields.each(function(f){ths.options.items.push([f.id, f.label])});
            this.fetchData();
            this.currEntityName = entityNameToUse;
        }
    },
    listDimensionCalculated: false,
    openList: function(event){
        if(this.options.entityName && this.options.entityName.startsWith('use#'))
            this.beforeOpenListForFields();
        else
            this.beforeOpenList();
        
        var list = $(this.editor);
        if(this.input.disabled) return;
        
        if(!this.listDimensionCalculated){
            var w = this.div.getWidth();
            var h = list.getHeight();
            h = h>this.listHeight ? this.listHeight : h;
            list.setStyle({height:h+'px', width:w+'px'});
            this.listDimensionCalculated = true;
        }
        
        if(this.options.multiSelect){ // multiselect listeler için seçili olanları yeşil falan gösterelim.
            var i=0;
            var elm = $('__itm'+this.hndl+'_'+i);
            while(elm){
                elm.removeClassName('checkedItem');
                if(this.input.value.indexOf(elm.innerHTML)>-1)
                    elm.addClassName('checkedItem');
                i++; elm = $('__itm'+this.hndl+'_'+i);
            }
        } else {
            var i=0, selElm = null;
            var elm = $('__itm'+this.hndl+'_'+i);
            while(elm){
                if(this.input.value.indexOf(elm.innerHTML)>-1) selElm = elm;
                elm.removeClassName('checkedItem');
                i++; elm = $('__itm'+this.hndl+'_'+i);
            }
            if(selElm) {
                selElm.addClassName('checkedItem');
                //Scroll.to(selElm);
            }
        }

        list.show();
        //Event.stop(event);
    },
    fetchData: function(){
        var list = $(this.editor);
        list.innerHTML = '';

        var insertionHtml = '';
        for(var i=0;i<this.options.items.length;i++){
            var row = this.options.items[i];
            var text = typeof row == 'object' ? (row.length>1 ? row[1] : row[0]) : row;
            insertionHtml += '<div class="item" id="__itm'+this.hndl+'_'+i+'"><nobr>'+text+'</nobr></div>';
        }
        new Insertion.Bottom(list, insertionHtml);
        
        if(list.getHeight()>this.listHeight) list.setStyle({height:this.listHeight+'px'});
        var i=0;
        var elm = $('__itm'+this.hndl+'_'+i);
        while(elm){
            elm.observe('mouseover', this.onItemMouseOver.bind(this));
            elm.observe('mouseout', this.onItemMouseOut.bind(this));
            elm.observe('click', this.onItemClick.bind(this));
            i++; elm = $('__itm'+this.hndl+'_'+i);
        }
                
        list.hide();
    },
    getValue: function(){
        return this.value;
    },
    setValue: function(val){
        if(this.options.multiSelect){
            if(this.options.items==null) this.beforeOpenListForFields();
            this.value = val;
            this.input.value = '';
            var fields = val.split(',');
            for(var i=0;i<fields.length;i++){
                var item = this.options.items.find(function(row){return row==fields[i] || row[0]==fields[i];});
                if(item){
                    var txt = typeof item == 'object' ? (item.length>1 ? item[1] : item[0]) : item;
                    if(this.input.value.length>0) txt = ','+txt;
                    this.input.value += txt;
                }
            }
        } else {
            if(val==null || this.options.items==null) return;
            var item = this.options.items.find(function(row){return row==val || row[0]==val;});
            if(item){
                this.value = val;
                this.setText(typeof item == 'object' ? (item.length>1 ? item[1] : item[0]) : item);
            }
        }
    },
    setText: function(str){
        if(this.input.value != str){
            this.input.value = str;
            this.options.onChange(this);
        }
    },
    selectedIndex: -1,
    setSelectedIndex: function(index){
        if(index<0 || index>=this.options.items.length) return;
        
        if(this.options.multiSelect){
            if(index==0) {this.setValue(''); return;} //***
            
            var val = this.options.items[index];
            val = typeof val == 'object' ? val[0] : val;
            var currVal = this.getValue();
            if(currVal=='')
                currVal = val;
            else if(currVal.indexOf(val)>-1){
                var fields = currVal.split(',');
                currVal = '';
                for(var i=0;i<fields.length;i++)
                    if(fields[i]!=val)
                        currVal += ','+fields[i];
                currVal = currVal.substr(1);
            } else
                currVal += ','+val;
            this.setValue(currVal);
        } else {
            this.selectedIndex = index;
            var val = this.options.items[index];
            val = typeof val == 'object' ? val[0] : val;
            this.setValue(val);
        }
    },
    onItemMouseOver: function(e){
        var elm = Event.findElement(e,'DIV');
        elm.addClassName('selItem'); 
    },
    onItemMouseOut: function(e){
        var elm = Event.findElement(e,'DIV');
        elm.removeClassName('selItem');
    },
    onItemClick: function(e){
        var elm = Event.findElement(e,'DIV');
        var list = this.editor;
        var index = list.select('.item').indexOf(elm);
        this.setSelectedIndex(index);
        list.hide();
    }
}

//#########################
//#       EditForm        #
//#########################

var formHandle = 0;

var EditForm = Class.create(); EditForm.prototype = {
    formType: 'EditForm',
    hndl: null,
    controls: [],
    labels: [],
    descriptions: [],
    entityName: null,
    entityId: 0,
    tdDesc: null,
    onSave: null,
    cntrlId: null,
    initialize: function(container, controls, entityName, entityId, strFilterExp, hideCategory, renameLabels){
        container = $(container);
        if(container==null) container = $(document.body);
        
        this.hndl = ++formHandle;
        this.cntrlId = 'cntrl' + this.hndl + '_';
        this.controls = [];
        this.labels = [];
        this.descriptions = [];
        this.entityName = entityName;
        this.entityId = entityId ? entityId : 0;
        
        controls = controls.sortBy(function(ctrl){return ctrl.orderNo;});
		
		var filters = parseFilterExp(strFilterExp);
		var hideFieldValue = {}, ind = -1;
		while(filters['f_'+(++ind)]!=undefined)
			if(filters['o_'+ind]=='=')
				hideFieldValue[filters['f_'+ind]] = filters['c_'+ind];
			
        var ths = this;

        var str = '<table class="editForm" width="99%" cellpadding=0 cellspacing=0 border=0>';
        str += '<tr><td><div><table class="cntrlsTbl" cellpadding="0" cellspacing="0" border="0"><tbody>';
		var categories = controls.collect(function(item){return item.category;}).uniq().compact();
		for(var k=0; k<categories.length; k++){
			var cat = categories[k];
			if(hideCategory!=cat)
				str += '<tr class="category"><td colspan="2">'+cat+'</td></tr>';
			for(var i=0; i<controls.length; i++){
				var control = controls[i];
				if(control.category!=cat || control.type=='ListForm') continue;
				if (hideCategory == cat) hideFieldValue[control.id] = control.value;
				if (renameLabels && renameLabels[control.id]) control.label = renameLabels[control.id];
				str += '<tr>';
				str += '<td onclick="$(this).up().down(\'input\').focus()">'+(hideFieldValue[control.id]!=undefined?'':('&nbsp;'+control.label))+'</td>';
				str += '<td id="'+this.cntrlId+i+'"></td>';
				str += '</tr>';
			}
		}
        str += '<tr class="category" id="detailsHeader'+this.hndl+'"><td colspan="2">İlişkili Veriler</td></tr>';
        str += '<tr><td colspan="2" id="details'+this.hndl+'"></td></tr>';
        str += '</tbody></table></div></td></tr>';
        str += '<tr><td style="min-height:50px;padding:7px 0px;"><div id="desc'+this.hndl+'" style="height:50px;background:#F1EFE2;padding:4px;"></div></td></tr>';
        str += '<tr><td style="height:16px;text-align:right"><span class="btn csave" id="btnSave'+this.hndl+'">'+lang('Save')+'</span></td></tr>';
        str += '</table>';

        new Insertion.Bottom(container, str);
        var details = $('details'+this.hndl);
        this.tdDesc = $('desc'+this.hndl);
        $('btnSave'+this.hndl).observe('click', this.saveClick.bind(this));

        for(var i=0; i<controls.length; i++){
            var control = controls[i];
            var aControl = null;
            if(control.options==null) control.options = new Object();
            control.options.container = $(this.cntrlId+i);
            control.options.relatedEditForm = ths;
            // Eğer bu EditForm bir EditForm'un içindeki detail ListForm'unda "Yeni Ekle" butonuna tıklayarak açılıyorsa... (ilişkili kontrolü gizle)
            if(control.id && hideFieldValue[control.id]!=undefined){
                control.options.hidden = true;
                control.value = hideFieldValue[control.id];
            }
            switch(control.type){
                case 'StringEdit':
                    aControl = new StringEdit(control.id, control.value, control.options);
                    break;
                case 'CSSEdit':
                    aControl = new CSSEdit(control.id, control.value, control.options);
                    break;
                case 'MemoEdit':
                    aControl = new MemoEdit(control.id, control.value, control.options);
                    break;
                case 'FilterEdit':
                    aControl = new FilterEdit(control.id, control.value, control.options);
                    break;
                case 'IntegerEdit':
                    aControl = new IntegerEdit(control.id, control.value, control.options);
                    break;
                case 'DecimalEdit':
                    aControl = new DecimalEdit(control.id, control.value, control.options);
                    break;
                case 'DateTimeEdit':
                    aControl = new DateTimeEdit(control.id, control.value, control.options);
                    break;
                case 'PictureEdit':
                    aControl = new PictureEdit(control.id, control.value, control.options);
                    break;
                case 'ComboBox':
                    aControl = new ComboBox(control.id, control.value, control.options);
                    break;
                case 'LookUp':
                    aControl = new LookUp(control.id, control.value, control.options);
                    break;
                case 'ListForm':
                    if(this.entityId==0) continue; //***
					var entityDisplayName = controls.find(function(c){return c.id=='Title' || c.id=='Name' || c.id=='Question'}).value;
					if(entityDisplayName) entityDisplayName = ' (' + entityDisplayName.split("'").join('').split('"').join('') + ')';
                    details.insert('<span class="btn c'+control.entityName+'" onclick="openEntityListForm(\'' + control.entityName + '\', \'' + control.label + entityDisplayName + '\', \'' + control.relatedFieldName + '=' + this.entityId + '\')">' + control.label + '</span>');
                    continue;
                default:
                    throw 'No control of this kind: '+control.type;
                    break;
            }
            aControl.label = control.label;
            this.controls.push(aControl);
            aControl.description = control.description;
            
            aControl.input.observe('focus', this.showDesc.bind(this));
            aControl.input.observe('blur', this.clearDesc.bind(this));
        }
        // kontrollerin değerlerini set edelim (mesela combobox'ların değeri set edilirken optionları yükleniyor, use# muhabbeti olanlar var)
        for(var i=0; i<controls.length; i++){
            var aControl = this.getControl(controls[i].id);
            if(aControl && aControl.setValue)
                aControl.setValue(controls[i].value);
        }
        // yoksa detail linklerini gizleyelim
        if(!details.down('span')){
            details.hide();
			$('detailsHeader'+this.hndl).hide();
		}
    },
    showDesc: function(event){
        var elm = Event.findElement(event,'INPUT');
        if(elm.ctrl)
            this.tdDesc.innerHTML = elm.ctrl.description;
        var td = elm.up('tr').down('td');
        if(td) td.setStyle({backgroundColor:'#316AC5',color:'white'});
    },
    clearDesc: function(event){
        //this.tdDesc.innerHTML = '';
        var elm = Event.findElement(event,'INPUT');
        var td = elm.up('tr').down('td');
        if(td) td.setStyle({backgroundColor:'',color:''});
    },
    serialize: function(){
        var h = new Object();
        for(var i=0;i<this.controls.length;i++)
            h[this.controls[i].id] = this.controls[i].getValue();
        return h;
    },
    getControl: function(name){
        for(var i=0;i<this.controls.length;i++)
            if(this.controls[i].id==name)
                return this.controls[i];
        return null;
    },
    saveClick: function(event){
        for(var i=0;i<this.controls.length;i++)
            this.controls[i].value = this.controls[i].getValue();
        
        var res = '';
        for(var i=0; i<this.controls.length; i++)
            res += this.controls[i].validate();
        if(res.length==0)
            this.onSave(this);
        else
            niceAlert(res);
    }
}

function openEditForm(entityName, entityId, title, controls, onSave, filter, hideCategory, renameLabels){
	var dim = $(document.body).getDimensions();
	var left=dim.width-390, top=10, width=350, height=dim.height-60;
	var win = new Window({ className: "alphacube", title: '<span class="cbtn c'+entityName+'"></span> ' + title, left: left, top: top, width: width, height: height, wiredDrag: true, destroyOnClose: true, showEffect: Element.show, hideEffect: Element.hide }); 
	var winContent = $(win.getContent());
	var pe = new EditForm(winContent, controls, entityName, entityId, filter, hideCategory, renameLabels);
	pe.onSave = onSave;
	win['form'] = pe;
	win.show();
	win.toFront();
	var dimWin = winContent.down('.editForm').getDimensions();
	win.setSize(350,dimWin.height);
    pe.controls[0].input.select();
}


//##########################
//#        ListForm        #
//##########################

var ListForm = Class.create();ListForm.prototype = {
    hndl: 0,
    formType: 'ListForm',
    container: null,
    filter: null,
    options: null,
    listGrid: null,
    pageIndex: 0,
    limit: 20,
	selRows: null, //set when getSelectedEntityId is called
    initialize: function (container, options) {
        idCounter++;
        this.hndl = idCounter;

        container = $(container);
        if (container == null) container = $(document.body);
        this.container = container;
        this.options = options;

        new Insertion.Top(this.container, '<table style="'+(this.options.hideFilterPanel ? 'display:none' : '')+'" width="100%"><tr><td width="1%">' + lang('Filter') + '</td><td id="filter' + this.hndl + '"></td><td width="1%"><span id="btnFilter' + this.hndl + '" class="btn cfilter" style="margin:0px 0px 0px 10px">' + lang('Apply') + '</span></td></table>');
        this.filter = new FilterEdit('id', this.options.extraFilter, { entityName: options.entityName, container: 'filter' + this.hndl, readOnly:true });
        $('btnFilter' + this.hndl).observe('click', this.fetchData.bind(this));

        new Insertion.Bottom(this.container, '<div class="lf-dataArea" id="lf-dataArea' + this.hndl + '"></div>');

        var str = '<div class="lf-listFormFooter">';
		if(this.options.commands)
			for(var i=0; i<this.options.commands.length; i++){
				var cmd = this.options.commands[i];
				str += '<div style="float:left;margin-top:4px"><span id="btnListFormsCmd' + cmd.id + this.hndl + '" class="btn c'+cmd.icon+'">' + lang(cmd.name) + '</span></div>';
			}
        str += '<span class="cbtn cprev" id="btnPrev' + this.hndl + '" title="' + lang('Previous Page') + ' (PgUp)"></span>';
        str += '<span class="pager" id="pageNo' + this.hndl + '">1</span>';
        str += '<span class="cbtn cnext" id="btnNext' + this.hndl + '" title="' + lang('Next Page') + ' (PgDw)" style="margin-right:50px"></span>';
        str += '<span class="cbtn cadd" id="btnAdd' + this.hndl + '" title="' + lang('Add') + ' (Ins)"></span>';
        str += '<span class="cbtn cedit" id="btnEdit' + this.hndl + '" title="' + lang('Edit') + ' (Ent)"></span>';
        str += '<span class="cbtn cdelete" id="btnDelete' + this.hndl + '" title="' + lang('Delete') + ' (Del)"></span>';
        str += '<span class="cbtn crefresh" id="btnRefresh' + this.hndl + '" title="' + lang('Refresh') + '"></span>';
        str += '<span class="cbtn cinfo" id="btnInfo' + this.hndl + '" title="' + lang('Info') + '"></span>';
        str += '</div>';
        new Insertion.Bottom(this.container, str);
        $('btnPrev' + this.hndl).observe('click', this.cmdPrev.bind(this));
        $('btnNext' + this.hndl).observe('click', this.cmdNext.bind(this));
        $('btnAdd' + this.hndl).observe('click', this.cmdAdd.bind(this));
        $('btnEdit' + this.hndl).observe('click', this.cmdEdit.bind(this));
        $('btnDelete' + this.hndl).observe('click', this.cmdDelete.bind(this));
        $('btnRefresh' + this.hndl).observe('click', this.fetchData.bind(this));
        $('btnInfo' + this.hndl).observe('click', this.cmdInfo.bind(this));
		if(this.options.commands)
			for(var i=0; i<this.options.commands.length; i++){
				var cmd = this.options.commands[i];
				$('btnListFormsCmd' + cmd.id + this.hndl).observe('click', cmd.handler.bind(this));
			}

        this.fetchData();
    },
    fetchData: function () {
        var params = null;
        if(this.filter && this.filter.filter)
            params = this.filter.filter.serialize();
        var ths = this;
        new Ajax.Request(this.options.ajaxUri + '?method=getGridList&entityName=' + this.options.entityName + (this.options.extraFilter ? '&extraFilter=' + this.options.extraFilter : '') + (this.options.orderBy ? '&orderBy=' + this.options.orderBy : '') + '&page=' + this.pageIndex + '&limit=' + (this.options.limit || this.limit), {
            method: 'post',
            parameters: params,
            onComplete: function (req) {
                if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
                var dataArea = $('lf-dataArea' + ths.hndl);
                dataArea.innerHTML = req.responseText;

                if (ths.options['renameLabels']) {
                    var headers = dataArea.select('TH');
                    for (var i = 0; i < headers.length; i++) {
                        var fieldName = headers[i].readAttribute("id").split('_')[1];
                        var key = fieldName.indexOf('.') > -1 ? fieldName.split('.')[1] : fieldName;
                        if (ths.options.renameLabels[key])
                            headers[i].innerHTML = ths.options.renameLabels[key];
                    }
                }

                ths.listGrid = new ListGrid(dataArea.down(), ths.options.selectCallback, ths.sortCallback.bind(ths));
				
				$('btnPrev' + ths.hndl).setOpacity(ths.pageIndex<=0 ? 0.3 : 1.0);
				$('btnNext' + ths.hndl).setOpacity(ths.listGrid.mayHaveNextPage(ths.limit) ? 1.0 : 0.3);
				$('pageNo' + ths.hndl).innerHTML = (ths.pageIndex+1);
            },
            onException: function (req, ex) { throw ex; }
        });
    },
    sortCallback: function (sortColumnId) {
        var sortColumn = sortColumnId.split('_')[1];
        if (this.options.orderBy && this.options.orderBy.startsWith(sortColumn)) {
            if (this.options.orderBy.endsWith('desc'))
                this.options.orderBy = sortColumn;
            else
                this.options.orderBy = sortColumn + ' desc';
        }
        else
            this.options.orderBy = sortColumn;
        this.fetchData();
    },
    cmdPrev: function () {
        if (this.pageIndex > 0) {
            this.pageIndex--;
            this.fetchData();
        }
    },
    cmdNext: function () {
        if (this.listGrid.mayHaveNextPage(this.limit)) {
            this.pageIndex++;
            this.fetchData();
        }
    },
    cmdAdd: function () {
        var ths = this;
        new Ajax.Request(this.options.ajaxUri + '?method=new&entityName=' + this.options.entityName, {
            method: 'get',
            onComplete: function (req) {
                if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
                var res = null;
                try { res = eval('(' + req.responseText + ')'); } catch (e) { niceAlert(e.message); }
				openEditForm(
					ths.options.entityName, 
					0,
					lang('New') + ' ' + ths.options.hrEntityName, 
					res, 
					ths.insertEntity.bind(ths), 
					ths.filter ? ths.filter.getValue() : null, ths.options.editFormHideCategory,
                    ths.options.renameLabels);
            },
            onException: function (req, ex) { throw ex; }
        });
    },
    insertEntity: function (pe) {
        var params = pe.serialize();
        var ths = this;
        new Ajax.Request(this.options.ajaxUri + '?method=insertNew&entityName=' + pe.entityName, {
            method: 'post',
            parameters: params,
            onComplete: function (req) {
                if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
                Windows.getFocusedWindow().destroy();
                ths.fetchData();
                //ths.cmdAdd();
            },
            onException: function (req, ex) { throw ex; }
        });
    },
    cmdEdit: function () {
        var id = this.getSelectedEntityId();
		if(!id || id<=0) return;
        var ths = this;
        new Ajax.Request(this.options.ajaxUri + '?method=edit&entityName=' + ths.options.entityName + '&id=' + id, {
            method: 'get',
            onComplete: function (req) {
                if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
                var res = null;
                try { res = eval('(' + req.responseText + ')'); } catch (e) { niceAlert(e.message); }
				openEditForm(
					ths.options.entityName, 
					id,
					lang('Edit') + ' ' + ths.options.hrEntityName, 
					res, 
					ths.saveEntity.bind(ths), 
					ths.filter ? ths.filter.getValue() : null, ths.options.editFormHideCategory,
                    ths.options.renameLabels);
            },
            onException: function (req, ex) { throw ex; }
        });
    },
	getSelectedEntityId: function(){
        this.selRows = this.listGrid.getSelectedRows() || [];
        if (this.selRows.length == 0) return 0;
        var id = parseInt(this.selRows[0].id.split('_')[1]);
		return id;
	},
	getSelectedEntities: function(){
		return this.listGrid.getSelectedEntities();
	},
	getSelectedEntity: function(){
		return this.listGrid.getSelectedEntity();
	},	
    saveEntity: function (pe) {
        var params = pe.serialize();
        var ths = this;
        new Ajax.Request(this.options.ajaxUri + '?method=save&entityName=' + pe.entityName + '&id=' + pe.entityId, {
            method: 'post',
            parameters: params,
            onComplete: function (req) {
                if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
                ths.fetchData();
                Windows.getFocusedWindow().destroy();
            },
            onException: function (req, ex) { throw ex; }
        });
    },
    cmdDelete: function () {
        var ths = this;
        niceConfirm(lang('The record will be deleted!'), function () {
			var id = ths.getSelectedEntityId();
			if(!id || id<=0) return;
            new Ajax.Request(ths.options.ajaxUri + '?method=delete&entityName=' + ths.options.entityName + '&id=' + id, {
                method: 'get',
                onComplete: function (req) {
                    if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
                    Element.remove(ths.selRows[0]);
                },
                onException: function (req, ex) { throw ex; }
            });
        });
    },
    cmdInfo: function () {
        var id = this.getSelectedEntityId();
		if(!id || id<=0) return;
        niceInfo(ajax({ url: this.options.ajaxUri + '?method=info&entityName=' + name + '&id=' + id, isJSON: false, noCache: true }));
    }
}

//###########################
//#       FilterEditor      #
//###########################
var filterOps = ["like@", "<=@", ">=@", "<>@", "<@", ">@", "=@", "like", "<=", ">=", "<>", "<", ">", "="];
var FilterEditor = Class.create(); FilterEditor.prototype = {
    container: null,
    fields: [],
    fieldsComboItems: [],
    parameterOptions: [['',lang('Select')],['Category',lang('Category')],['Hierarchy',lang('Hierarchy')],['Content',lang('Content')],['PreviousContent',lang('Previous Content')],['NextContent',lang('Next Content')],['Author',lang('Author')],['Source',lang('Source')],['Yesterday',lang('Yesterday')],['LastDay',lang('The day before yesterday')],['LastWeek',lang('Last week')],['LastMonth',lang('Last month')]],
    rowHtml: '<tr class="filterRow"><td class="tdField"></td><td class="tdOp"></td><td class="tdControl"></td></tr>',
    initialize: function(container, fields){
        this.container = $(container);
        if(this.container==null) this.container = $(document.body);

        this.fields = fields.sortBy(function(f){return __letters.indexOf(f.label.substr(0,1));});
        this.fieldsComboItems = [];
        
        new Insertion.Bottom(this.container, '<table class="filterTable">'+this.rowHtml+'</table>');
        var tdField = this.container.select('.tdField').last();
        var ths = this;
        this.fieldsComboItems.push(['none',lang('None')]);
        this.fields.each(function(f){ths.fieldsComboItems.push([f.id, f.label])});
        var cb = new ComboBox('f1', null, {items:this.fieldsComboItems, container:tdField});
        cb.options.onChange = this.fieldChanged.bind(this);
    },
    revOpItems: null,
    getRevOpItems: function(){
        if(this.revOpItems==null){
            var arr = [];
            for (var i = 0; i < filterOps.length; i++)
                arr[filterOps.length - i - 1] = filterOps[i];
            this.revOpItems = arr;
        }
        return this.revOpItems;
    },
    fieldChanged: function(sender){
        var row = sender.input.up('.filterRow');
        var rowCount = row.up().childNodes.length;
        var tdOp = row.down('.tdOp');
        var tdControl = row.down('.tdControl');
        
        // selectedIndex==0 ise ve birden fazla satır varsa aktif satırı sil.
        if(sender.selectedIndex==0){
            if(rowCount>1) row.remove();
            else {tdOp.innerHTML = ''; tdControl.innerHTML='';}
        }
        else {
            // op yoksa op'u oluştur varsa bişey yapma.
            if(tdOp.innerHTML==''){
                var cb = new ComboBox('o'+rowCount, null, {items:this.getRevOpItems(), container:tdOp});
                cb.options.onChange = this.opChanged.bind(this);
            }
            // control yoksa da varsa da oluştur.
            tdControl.innerHTML = '';
            var fieldMetadata = this.fields[sender.selectedIndex-1];
            var aControl = createControl('c'+rowCount, fieldMetadata, tdControl);
            aControl.label = fieldMetadata.label;
			if(!row.next()){
				new Insertion.After(row, this.rowHtml);
				var cb2 = new ComboBox('f'+(rowCount+1), null, {items:this.fieldsComboItems, container:row.next().down('.tdField')});
				cb2.options.onChange = this.fieldChanged.bind(this);
			}
        }
    },
    opChanged: function(sender){
        var row = sender.input.up('.filterRow');
        var cbField = $(row.down('.tdField').down('INPUT'))['ctrl'];
        var tdControl = row.down('.tdControl');
        tdControl.innerHTML = '';
        if(sender.getValue().indexOf('@')>-1)
            new ComboBox(sender.id.replace('o','c'), null, {items:this.parameterOptions, container:tdControl});
        else
            createControl(sender.id.replace('o','c'), this.fields[cbField.selectedIndex-1], tdControl);
    },
    serialize: function(){
        var h = new Object();
        var rows = this.container.select('.filterRow');
        for(var i=0;i<rows.length-1;i++){
            var inpField = rows[i].down('.tdField').down('INPUT');
            var inpOp = rows[i].down('.tdOp').down('INPUT');
            var inpCtrl = rows[i].down('.tdControl').down('INPUT');
            h['f_'+i] = inpField['ctrl'].getValue();
            h['o_'+i] = inpOp['ctrl'].getValue();
            h['c_'+i] = inpCtrl['ctrl'].getValue();
        }
        return h;
    },
    setFilters: function(filters){
        var h = parseFilterExp(filters);
        var i = 0;
        var table = this.container.down();
        if(table.tagName!='TBODY') table = table.down();
        var rows = table.immediateDescendants(); var rowCount = rows.length;
        rows.each(function(elm,i){if(i<rowCount-1) elm.remove();}); //table.rows.clear(); demek bu.
        while(true){
            if(!h['f_'+i]) break;
            new Insertion.Top(table, this.rowHtml);
            var row = this.container.down('.filterRow');
            var tdField = row.down('.tdField');
            var tdOp = row.down('.tdOp');
            var tdControl = row.down('.tdControl');

            // fieldlist combo
            var cb = new ComboBox('f'+i, null, {items:this.fieldsComboItems, container:tdField});
            cb.setValue(h['f_'+i]);
            cb.options.onChange = this.fieldChanged.bind(this);
            // operators combo
            var oCb = new ComboBox('o'+i, null, {items:this.getRevOpItems(), container:tdOp});
            oCb.setValue(h['o_'+i]);
            // control for value
            var fieldMetadata = this.fields.find(function(f){return f.id==h['f_'+i]});
			fieldMetadata.value = h['c_'+i];
            var aControl = null;
            if(h['o_'+i].indexOf('@')>-1)
                aControl = new ComboBox('c'+i, null, {items:this.parameterOptions, container:tdControl});
            else
                aControl = createControl('c'+i, fieldMetadata, tdControl);
            aControl.setValue(h['c_'+i]);

            i++;
        }
    }
}
function parseFilterExp(filters){
	var h = new Object();
	if(!filters) return h;
	
	var criterias = filters.split(' AND ');
	if(!criterias[0]) return;
	for(var i=0; i<criterias.length; i++){
		var pair = null;
		for (var j = 0; j < filterOps.length; j++) {
		    pair = criterias[i].split(filterOps[j]);
			if(pair.length==2) break;
		}
		var op = criterias[i].substr(pair[0].length, criterias[i].length - pair[0].length - pair[1].length);
		h['f_'+i] = pair[0];
		h['o_'+i] = op;
		h['c_'+i] = pair[1];
	}
	return h;
}
function createControl(id, fieldMetadata, container){
    if(fieldMetadata.options==null) fieldMetadata.options = new Object();
    fieldMetadata.options.container = container;
    fieldMetadata.options.noHtml = true;
    var aControl = null;
    switch(fieldMetadata.type){
        case 'StringEdit':
            aControl = new StringEdit(id, fieldMetadata.value, fieldMetadata.options);
            break;
        case 'CSSEdit':
            aControl = new CSSEdit(id, fieldMetadata.value, fieldMetadata.options);
            break;
        case 'MemoEdit':
            aControl = new MemoEdit(id, fieldMetadata.value, fieldMetadata.options);
            break;
        case 'FilterEdit':
            aControl = new FilterEdit(id, fieldMetadata.value, fieldMetadata.options);
            break;
        case 'IntegerEdit':
            aControl = new IntegerEdit(id, fieldMetadata.value, fieldMetadata.options);
            break;
        case 'DecimalEdit':
            aControl = new DecimalEdit(id, fieldMetadata.value, fieldMetadata.options);
            break;
        case 'DateTimeEdit':
            aControl = new DateTimeEdit(id, fieldMetadata.value, fieldMetadata.options);
            break;
        case 'PictureEdit':
            aControl = new PictureEdit(id, fieldMetadata.value, fieldMetadata.options);
            break;
        case 'ComboBox':
            aControl = new ComboBox(id, fieldMetadata.value, fieldMetadata.options);
            break;
        case 'LookUp':
            aControl = new LookUp(id, fieldMetadata.value, fieldMetadata.options);
            break;
        default:
            throw 'No control of this kind: ' + fieldMetadata.type;
            break;
    }
    return aControl;
}

//#########################
//#       ListGrid        #
//#########################

var ListGrid = Class.create(); ListGrid.prototype = {
    table: null,
    selectCallback: null,
    sortCallback: null,
    initialize: function(table, selectCallback, sortCallback){
        this.table = $(table);
        if(this.table.tagName!='TABLE') throw 'parameter is not a table!';
        this.selectCallback = selectCallback;
        this.sortCallback = sortCallback;
        
        var ths = this;

        $A(this.table.getElementsByTagName('TH')).each(function(col){
            $(col).observe('click', ths.colClick.bind(ths));
        });

        var rows = $A(this.table.getElementsByTagName('TR'));
        rows.each(function(row){
            row = $(row);
            if(!row.id) return;
            row.observe('mouseover', ths.rowOver.bind(ths));
            row.observe('mouseout', ths.rowOut.bind(ths));
            row.observe('click', ths.rowSelect.bind(ths));
        });
        if(rows.length>1 && rows[1].id)
            this.selectRow(rows[1]);
    },
    rowOver: function(event){
        var row = Event.findElement(event, 'TR');
        if(row.hasClassName('selected')) return;
        row.addClassName('hover');
    },
    rowOut: function(event){
        var row = Event.findElement(event, 'TR');
        if(row.hasClassName('selected')) return;
        row.removeClassName('hover');
    },
    rowSelect: function(event){
        var row = Event.findElement(event, 'TR');
        this.selectRow(row);
        
        if(this.selectCallback)// first td            second td
            this.selectCallback(row.down().innerHTML, row.down().next().innerHTML);
    },
    selectRow: function(row){
        var ths = this;
        this.table.select('.selected').each(function(r){ths.deselectRow(r);});
        row.addClassName('selected');
        row.removeClassName('hover');
    },
    deselectRow: function(row){
        row.removeClassName('selected');
    },
    getSelectedRows: function(){
        return this.table.select('.selected');
    },
	getSelectedEntities: function(){
        var selRows = this.getSelectedRows() || [];
        if (selRows.length == 0) return [];
		var res = [];
		var headers = this.table.select('TH');
        for(var i=0; i<selRows.length; i++){
			var r = {};
			for(var k=0; k<headers.length; k++){
				var fieldName = headers[k].readAttribute("id").split('_')[1];
				var key = fieldName.indexOf('.')>-1 ? fieldName.split('.')[1] : fieldName;
				r[key] = selRows[i].select('TD')[k].readAttribute("value");
			}
			res.push(r);
		}
		return res;
	},
	getSelectedEntity: function(){
		var selArr = this.getSelectedEntities();
		if(selArr.length>0)
			return selArr[0];
		return null;
	},
    colClick: function(event){
        var col = Event.findElement(event, 'TH');
        if(this.sortCallback) this.sortCallback(col.id);
    },
    mayHaveNextPage: function(maxRowCount){
        return this.table && this.table.getElementsByTagName('TR').length > maxRowCount;
    }
}


//############################
//       TreeView (aNode = {data:1, text:'About Us', type:'category|content'})
//############################

var TreeView = Class.create(); TreeView.prototype = {
    container: null,
    getNodesCallback: null,
    nodeClickCallback: null,
    initialize: function(container, rootData, rootText, getNodesCallback, nodeClickCallback){
        this.container = $(container);
        this.getNodesCallback = getNodesCallback;
        this.nodeClickCallback = nodeClickCallback;

        new Insertion.Bottom(this.container, '<div id="nd_' + rootData + '"><span class="cbtn cplus"></span><span class="cbtn ccategory"></span> <span class="nodeName">' + rootText + '</span></div>');
        var node = $('nd_'+rootData);
        node['node'] = {data:rootData, text:rootText, type:'category', collapsed:true};
        node.down().observe('click', this.toggle.bind(this));
        node.down('span.nodeName').observe('click', this.nodeClick.bind(this));
    },
    toggle: function(event){
        var img = Event.element(event);
        var div = img.up();
        var node = div['node'];
        
        var childrenDiv = $('nd_'+node.data+'_children');
        if(!childrenDiv){
            new Insertion.Bottom(div, '<div class="treeChildren" id="nd_'+node.data+'_children"></div>');
            childrenDiv = $('nd_'+node.data+'_children');
            var nodes = this.getNodesCallback(node.data) || [];
            if(nodes.length==0) childrenDiv.setStyle({width:0+'px',height:0+'px'});
            for(var i=0; i<nodes.length; i++){
                var n = nodes[i];
                n.collapsed = true;
                new Insertion.Bottom(childrenDiv, '<div id="nd_' + n.data + '">' + (n.type == 'category' ? '<span class="cbtn cplus"></span>' : '') + '<span class="cbtn c' + n.type + '"></span> <span class="nodeName">' + n.text + '</span></div>');
                var nDiv = $('nd_'+n.data);
                nDiv['node'] = n;
                nDiv.down().observe('click', this.toggle.bind(this));
                nDiv.down('span.nodeName').observe('click', this.nodeClick.bind(this));
            }
        }
        if(node.collapsed){
            childrenDiv.show();
            node.collapsed = false;
            //TODO: çöz bunu:
            //img.src = '/external/icons/minus.png';
        } else {
            childrenDiv.hide();
            node.collapsed = true;
            //img.src = '/external/icons/plus.png';
        }
    },
    nodeClick: function(event){
        var div = Event.element(event).up();
        if(this.nodeClickCallback)
            this.nodeClickCallback(div['node']);
    }
}

//#############################
//#     StyleSheetManager     #
//#############################

var StyleSheetManager = Class.create(); StyleSheetManager.prototype = {
    styleSheet: null,
    initialize: function(title){
        for(var i=0; i<document.styleSheets.length; i++)
            if(document.styleSheets[i].title==title){
                this.styleSheet = document.styleSheets[i];
                break;
            }
        if(this.styleSheet!=null){
            this.styleSheet.crossDelete = this.styleSheet.deleteRule ? this.styleSheet.deleteRule : this.styleSheet.removeRule;
            this.styleSheet.crossRules = this.styleSheet.cssRules ? this.styleSheet.cssRules : this.styleSheet.rules;
        }
    },
    getRules: function(){
        return $A(this.styleSheet.crossRules);
    },
    getRule: function(selector){
        if(!selector || selector.strip().length==0)
            return null;
        selector = selector.strip().toLowerCase();
        return this.getRules().find(function(rule){return rule.selectorText && rule.selectorText.toLowerCase()==selector;});
    },
    removeRule: function(selector){
        var rules = this.getRules();
        for(var i=0; i<rules.length; i++)
            if(rules[i].selectorText && rules[i].selectorText.toLowerCase()==selector.toLowerCase()){
                this.styleSheet.crossDelete(i);
                break;
            }
    },
    addRule: function(selector, rule){
        selector = selector.strip(); rule = rule.strip();
        if(selector) this.removeRule(selector);
        if(!rule) return;
        
        if(this.styleSheet.insertRule)
            this.styleSheet.insertRule(selector + ' {' + rule + '}', 0);
        else
            this.styleSheet.addRule(selector, rule);
    },
    applyStyleSheet: function(sheet){
        var rules = sheet.split('}');
        for(var i=0; i<rules.length; i++){
            if(!rules[i] || rules[i].indexOf('{')==-1) continue;
            var rule = rules[i].split('{');
            if(rule[0].strip()){
                this.addRule(rule[0], rule[1]);
            }
        }
    }
}


//#############################
//#        ContextMenu        #
//#############################

var ContextMenu = Class.create(); ContextMenu.prototype = {
    menuItems: null,
    onShow: function(){},
    onHide: function(){},
    initialize: function(){
    },
    setup : function() {
        var s = '';
        s+=('<div id="smMenuContainer">');
        s+=this.createMenuItems('\t',this.menuItems, 'smMenu', 'menu');
        s+=('</div>');
        wr(s);
        //Event.observe(window, 'load', function(e){document.body.innerHTML+=('<textarea>'+totalS+'</textarea>');}, false);
    },
    createMenuItems: function(tab, menus, id, subId){
        var ths = this;
        var s = '';
        s+=(tab+'<div id="'+id+'" class="smMenu hideOnOut" style="display:none">\n');
        menus.each(function(menu, index){
            if(!menu) return;
            menu.parent = menus;
            if(menu.text=='-')
                s+=(tab+'\t<hr id="'+(subId+'_'+index)+'"/>\n');            
            else if(menu.items && menu.items.length>0)
                s+=(tab+'\t<div class="menuFolder" onmouseover="showSubMenu(this,\''+(id+'_'+index)+'\')" id="'+(subId+'_'+index)+'" onmouseout="menuOut(this)"><span class="cbtn c'+menu.icon+'"></span> '+menu.text+'</div>\n');
            else
                s+=(tab+'\t<div onclick="runMenu(this)" onmouseover="hideMenu(this)" id="'+(subId+'_'+index)+'" onmouseout="menuOut(this)"><span class="cbtn c'+menu.icon+'"></span> '+menu.text+'</div>\n');
        });
        s+=(tab+'</div>\n');
        menus.each(function(menu, index){
            if(!menu) return;
            if(menu.items && menu.items.length>0)
                s+=ths.createMenuItems(tab+'\t', menu.items, id+'_'+index, subId+'_'+index);
        });
        return s;
    },
    show: function(x, y){
        var menu = $('smMenu');
        var winDim = Position.getWindowSize();
		var scrollPos = document.viewport.getScrollOffsets();
        if(x>scrollPos[0]+winDim.width-menu.getWidth()) x -= menu.getWidth();
        if(y>scrollPos[1]+winDim.height-menu.getHeight()) y -= menu.getHeight();
        if(x<0) x=0; if(y<0) y=0;
        menu.setStyle({left: x+'px', top:y+'px'});
        this.onShow();
		$('smMenuContainer').show();
        menu.show();

        var menus = this.menuItems;
        for(var i=0;i<menus.length;i++){
            var fidi = $('menu_'+i);
            if(!fidi) continue;
            if(!menus[i].isEnabled){fidi.show(); continue;}
            if(menus[i].isEnabled())
                fidi.show();           //TODO: enable/disable yapılacak!
            else
                fidi.hide();
        }
    },
    hideMenu: function(link){
        link = $(link);
        link.setStyle({backgroundColor:'#316AC5', color:'white'});
        //if(link.className!='menuFolder') return;
        $('smMenuContainer').immediateDescendants().each(function(elm){
            var upElmId = link.up().id;
            if(elm.id!=upElmId && elm.id.startsWith(upElmId))
                elm.hide();
        });
    },
    showSubMenu: function(link, id){
        link = $(link);
        this.hideMenu(link);

        var menu = $(id);
        var linkPos = Position.cumulativeOffset(link);
        var winDim = Position.getWindowSize();
		var scrollPos = document.viewport.getScrollOffsets();
        
        if(linkPos[0]+link.getWidth()+menu.getWidth()<scrollPos[0]+winDim.width)
            x = linkPos[0]+link.getWidth()-5;
        else
            x = linkPos[0]-menu.getWidth()+5;
        
        if(linkPos[1]+menu.getHeight()<scrollPos[1]+winDim.height)
            y = linkPos[1]-3;
        else
            y = linkPos[1]-menu.getHeight()+link.getHeight()-3;
        
        if(x<0) x=0; if(y<0) y=0;
        menu.setStyle({left: x+'px', top:y+'px'});
        menu.show();
                
        var theItem = this.findMenuItem(menu.down());
        if(!theItem) return; //***
        
        var menus = theItem.parent;
        var fid = 'menu' + id.substr(6);
        for(var i=0;i<menus.length;i++){
            var fidi = $(fid+'_'+i);
            if(!fidi) continue;
            if(!menus[i].isEnabled){fidi.show(); continue;}
            if(menus[i].isEnabled())
                fidi.show();           //TODO: enable/disable yapılacak!
            else
                fidi.hide();
        }
    },
    runMenu: function(link){
        var theItem = this.findMenuItem(link);
        $(link).up().hide();
        this.onHide();
		$('smMenuContainer').hide();
        theItem.callback(theItem.data, theItem.text);
    },
    findMenuItem: function(link){
        link = $(link);
        var ids = link.id.substr(5).split('_');
        var theItems = this.menuItems;
        for(var i=0;i<ids.length-1;i++)
            theItems = theItems[ids[i]].items;
        var theItem = theItems[ids[ids.length-1]];
        return theItem;
    }
}
var popupMenu = new ContextMenu();
function hideMenu(link){
    popupMenu.hideMenu(link);
}
function showSubMenu(link, id){
    popupMenu.showSubMenu(link, id);
}
function runMenu(link){
    popupMenu.runMenu(link);
}
function menuOut(link){
    $(link).setStyle({backgroundColor:'', color:''});
}
function wr(s){document.write(s);}
//var totalS = '';
//function wr(s){totalS += s;}

//#############################
//#          Console          #
//#############################

var Console = Class.create(); Console.prototype = {
    txtArea: null,
    //status: null,
    requestUrl: null,
    lastPos:0,
    cursorPos:-1,
    cmdHist: [],
    cmdHistIndex:0,
    initialize: function(requestUrl){
        var win = new Window({ className: 'alphacube', title: '<span class="cbtn cconsole" style="vertical-align:middle"></span> ' + lang('Console'), width: 800, height: 400, wiredDrag: true, destroyOnClose: true, showEffect: Element.show, hideEffect: Element.hide }); 
        var container = win.getContent();
        
        container.insert('<textarea id="_cnsl"></textarea>');
        //container.insert('<br/><div id="_cnsl_status"></div>');
        //this.status = $('_cnsl_status');
        this.txtArea = $('_cnsl');
        this.txtArea.setStyle({backgroundColor: 'black',
	                            color:'White',
	                            fontFamily:'Lucida Console',
	                            fontSize:'12px',
	                            //position:'absolute',
	                            //left:'10px',
	                            //top:'30px',
	                            //right:'10px',
	                            //bottom:'5px'
	                            height:'100%',
	                            width:'100%'
	                            });
        this.txtArea.observe('keydown', this.onKeyDown.bind(this));
        
        win.showCenter();
        win.toFront();

        this.txtArea.focus();
        
        this.requestUrl = requestUrl;
        this.executeCommand('hello');
    },
    onKeyDown: function(event){
        var handled = false;
        switch(event.keyCode){
            case Event.KEY_ESC: // pencerenin kapanmasını engelliyor
            case Event.KEY_END:
            case Event.KEY_HOME:
            case Event.KEY_PAGEUP:
            case Event.KEY_PAGEDOWN:
                Event.stop(event);
                handled = true;
                break;
            case Event.KEY_RETURN:
                var cmd = this.txtArea.value.substr(this.lastPos);
                this.executeCommand(cmd);
                this.cmdHist.push(cmd);
                this.cmdHistIndex = this.cmdHist.length;
                Event.stop(event);
                handled = true;
                break;
            case Event.KEY_BACKSPACE:
                if(this.cursorPos<=this.lastPos)
                    Event.stop(event);
                else
                    this.cursorPos--;
                handled = true;
                break;
            case Event.KEY_LEFT:
                if(this.cursorPos<=this.lastPos)
                    Event.stop(event);
                else
                    this.cursorPos--;
                handled = true;
                break;
            case Event.KEY_RIGHT:
                if(this.cursorPos>this.txtArea.value.length-1)
                    Event.stop(event);
                else
                    this.cursorPos++;
                handled = true;
                break;
            case Event.KEY_UP:
                if(this.cmdHistIndex>0){
                    this.cmdHistIndex--;
                    this.txtArea.value = this.txtArea.value.substr(0, this.lastPos) + this.cmdHist[this.cmdHistIndex];
                    this.cursorPos = this.txtArea.value.length;
                }
                Event.stop(event);
                handled = true;
                break;
            case Event.KEY_DOWN:
                if(this.cmdHistIndex<this.cmdHist.length){
                    this.cmdHistIndex++;
                    this.txtArea.value = this.txtArea.value.substr(0, this.lastPos) + (this.cmdHistIndex==this.cmdHist.length?'':this.cmdHist[this.cmdHistIndex]);
                    this.cursorPos = this.txtArea.value.length;
                }
                Event.stop(event);
                handled = true;
                break;
        }
        
        if(!handled){
            var c = String.fromCharCode(event.keyCode);
            if((__letters+' ').indexOf(c)>-1)
                this.cursorPos++;
        }
        
        //this.status.innerText = 'Last Pos: ' + this.lastPos + ' | Curr Pos: ' + this.cursorPos + ' | Cmd Hist: ' + this.cmdHistIndex;
    },
    executeCommand: function(cmd){
        if(cmd=='exit' || cmd=='bye'){
            Windows.getFocusedWindow().close();
            return;
        }
        var _url = this.requestUrl + '?cmd=' + cmd;
        this.txtArea.value += ajax({url:_url,isJSON:false,noCache:true});
        this.lastPos = this.txtArea.value.length;
        this.cursorPos = this.lastPos;
        //this.status.innerText = 'Last Pos: ' + this.lastPos + ' | Curr Pos: ' + this.cursorPos + ' | Cmd Hist: ' + this.cmdHistIndex;
        this.txtArea.scrollTop = this.txtArea.scrollHeight;
    }
}

//#############################
//#          Fabtabs          #
//#############################

var Fabtabs = Class.create(); Fabtabs.prototype = {
    initialize : function(element) {
        element = $(element);
        var options = Object.extend({}, arguments[1] || {});
        this.menu = $A(element.getElementsByTagName('a'));
        this.show(this.getInitialTab());
        this.menu.each(this.setupTab.bind(this));
    },
    setupTab : function(elm) {
        Event.observe(elm,'click',this.activate.bindAsEventListener(this),false)
    },
    activate :  function(ev) {
        var elm = Event.findElement(ev, "a");
        Event.stop(ev);
        this.show(elm);
        this.menu.without(elm).each(this.hide.bind(this));
    },
    hide : function(elm) {
        $(elm).removeClassName('active-tab');
        $(this.tabID(elm)).removeClassName('active-tab-body');
    },
    show : function(elm) {
        $(elm).addClassName('active-tab');
        $(this.tabID(elm)).addClassName('active-tab-body');
    },
    tabID : function(elm) {
        return elm.href.match(/#(\w.+)/)[1];
    },
    getInitialTab : function() {
        if(document.location.href.match(/#(\w.+)/)) {
            var loc = RegExp.$1;
            var elm = this.menu.find(function(value) { return value.href.match(/#(\w.+)/)[1] == loc; });
            return elm || this.menu.first();
        } else {
            return this.menu.first();
        }
    }
}
