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
    initialize: function(id, value, options) {
        idCounter++;
        this.hndl = idCounter;
        this.editorId = 'editor' + this.hndl;
        this.id = id;
        this.value = value;

        // option validation
        this.options = (options == null ? new Object() : options);
        if (this.options.container == null) this.options.container = document.body;
        if (this.options.onChange == null) this.options.onChange = Prototype.emptyFunction;
        var className = this.options.className ? ' class="' + this.options.className + '"' : '';

        var inpType = 'text';
        if (this.options.password) inpType = 'password';
		this.options.container = $(this.options.container);

        this.options.container.append('<div id="bk-ctrl' + idCounter + '"><input' + className + ' id="ctrlInp' + idCounter + '" type="' + inpType + '" name="' + this.id + '" style="width:100%"/><input id="ctrlBtn' + idCounter + '" type="button" style="position:absolute;display:none" value="..."/></div>');
        this.div = $('#bk-ctrl' + idCounter);
        this.input = $('#ctrlInp' + idCounter);
        if (this.options.readOnly) this.input.readOnly = true;
        if (this.options.width) this.input.css({ width: this.options.width + 'px' });
        if (!this.options.password && this.value != null) this.input.val(this.value.toString().gsub('\n', '#NL#'));
        this.button = $('#ctrlBtn' + idCounter);

        if (this.options.hidden)
            this.div.hide();
        else {
            this.input.on('mouseover', this.showBtn.bind(this));
            this.input.on('mouseout', this.hideBtn.bind(this));
            this.button.on('mouseout', this.hideBtn.bind(this));
            this.input.on('change', this.onChange.bind(this));
            this.input.on('focus', this.onFocus.bind(this));
        }
    },
    onFocus: function() {
        if (this.options.onFocus)
            this.options.onFocus(this);
    },
    onChange: function() {
        if (this.options.onChange)
            this.options.onChange(this);
    },
    showBtn: function() {
        if (this.options.hideBtn) return;
        var dim = getDimensions(this.input);
        var pos = this.input.position();
        this.button.css({ left: (pos.left + dim.width - 20) + 'px', top: (pos.top + 1) + 'px', width: 20 + 'px', height: (dim.height - 2) + 'px' }).show();
    },
    hideBtn: function(event) {
        if (this.options.hideBtn) return;
        if ($(this.button[0]).is(':hover')) return;
        this.button.hide();
    },
    setEditorPos: function(editor) {
        var editor = $(editor);
        currEditor = editor;
        currEditor.parentControl = this;
        var div = $(this.div);
        var dim = getWindowSize();
        var pos = div.offset();

        if (dim.width < pos.left + editor.width())
            editor.css({ left: (pos.left + div.width() - editor.width()) + 'px' }); // , top: pos[1] + div.outerHeight()
        else
            editor.css({ left: pos.left + 'px' }); // , top: pos[1] + div.outerHeight()

        if (dim.height < pos.top + div.outerHeight() + editor.outerHeight() && pos.top - editor.outerHeight() > 0)
            editor.css({ top: (pos.top - editor.outerHeight()) + 'px' });
        else
            editor.css({ top: (pos.top + div.outerHeight()) + 'px' });

        Windows.maxZIndex += 1;
        editor.css({ zIndex: Windows.maxZIndex });
    },
    validate: function() {
        var maxLengthValidation = '';
        var regExValidation = '';
        var requiredValidation = '';
        if (this.options.maxLength && this.options.maxLength > 0)
            maxLengthValidation = this.value.length > this.options.maxLength ? (this.label + ' alanının uzunluğu en fazla ' + this.options.maxLength + ' karakter olabilir\n') : '';
        if (this.options.regEx && this.options.regEx.length > 0)
            regExValidation = new RegExp(this.options.regEx).test(this.value) ? (this.label + ' alanı için girdiğiniz değeri kontrol ediniz\n') : '';
        if (this.options.required)
            requiredValidation = (this.value === '') ? (this.label + ' alanını boş bırakamazsınız\n') : '';
        return maxLengthValidation + regExValidation + requiredValidation;
    }
};

//############################
//#       IntegerEdit        #
//############################

var IntegerEdit = Class.create(); IntegerEdit.prototype = {
    initialize: function(id, value, options){
        options.hideBtn = true;
        Object.extend(this, new Control(id, value, options));
        this.input.bind('keydown', this.onKeyDown.bind(this));
        this.input.css({textAlign:'right'});

        this.input[0]['ctrl'] = this;
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
                    event.stopPropagation();
                break;
        }
    },
    getValue: function(){
        return this.input.val();
    },
    setValue: function(val){
        this.input.val(val ? val : 0);
    }
};

//############################
//#       DecimalEdit        #
//############################

var DecimalEdit = Class.create(); DecimalEdit.prototype = {
    initialize: function(id, value, options){
        options.hideBtn = true;
        Object.extend(this, new Control(id, value, options));
        this.input.bind('keydown', this.onKeyDown.bind(this));
        this.input.css({textAlign:'right'});

        this.input[0]['ctrl'] = this;
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
                    event.stopPropagation();
                break;
        }
    },
    getValue: function(){
        return this.input.val();
    },
    setValue: function(val){
        this.input.val(val ? val : 0);
    }
};

//############################
//#        StringEdit        #
//############################

var __oldBtnOKClick, __oldBtnCancelClick;
var StringEdit = Class.create(); StringEdit.prototype = {
    initialize: function(id, value, options) {
        Object.extend(this, new Control(id, value, options));
        if (this.options.noHTML)
            this.options.hideBtn = true;
        else
            this.button.bind('click', this.showEditor.bind(this));

        this.input[0]['ctrl'] = this;
    },
    showEditor: function(event) {
        var ths = this;

        this.win = new CinarWindow({ title: ths.label, maximizable:true });
        var list = this.win.getContent();

        var wrap = getCookie('wrap');
        var nl2br = getCookie('nl2br');

        list.append(
            '<div>' +
                '<span class="fff text_bold" title="Bold"></span>' +
                '<span class="fff text_italic" title="Italic"></span>' +
                '<span class="fff text_underline" title="Underline"></span>' +
                '<span class="fff text_smallcaps" title="Font Size & Color"></span>' +
                '<span class="fff picture" title="Add Picture"></span>' +
                '<span class="fff link" title="Add Link"></span>' +
                '<span class="fff text_padding_bottom" title="Break lines"></span>' +
                '<span class="fff flag_red" title="Translate"></span>' +
                '<span class="fff eye" style="margin-left:40px" title="Preview"></span>' +
                '<div style="float:right"><input type="checkbox" class="wrapCheck" ' + (wrap == '1' ? 'checked' : '') + '/> Wrap <input type="checkbox" class="nl2br" ' + (nl2br == '1' ? 'checked' : '') + '/> nl2br</div>' +
                '</div>' +
                '<div id="' + this.editorId + 'ta" style="border-bottom: 1px solid #bbb;border-top: 1px solid #bbb;position: absolute;left: 0px;right: 0px;bottom: 31px;top: 19px;"></div>' +
                '<center style="position: absolute;left: 0px;right: 0px;bottom: 4px;"><span class="ccBtn"><span class="fff accept"></span> ' + lang('OK') + '</span> <span class="ccBtn"><span class="fff cancel"></span> ' + lang('Cancel') + '</span></center>');

        var ta = $('#'+this.editorId + 'ta');

        if (this.input.disabled) return;

        var btnOK = list.find('.accept').parent();
        var btnCancel = list.find('.cancel').parent();
        btnOK.unbind('click', __oldBtnOKClick);
        btnCancel.unbind('click', __oldBtnCancelClick);
        __oldBtnOKClick = this.setHtml.bind(this);
        __oldBtnCancelClick = function () { ths.win.close(); };
        btnOK.bind('click', __oldBtnOKClick);
        btnCancel.bind('click', __oldBtnCancelClick);

        var wrapCheck = list.find('.wrapCheck');
        wrapCheck.bind('click', function() {
            if (wrapCheck.is(':checked'))
                ths.aceEdit.getSession().setUseWrapMode(true);
            else
                ths.aceEdit.getSession().setUseWrapMode(false);
            setCookie('wrap', wrapCheck.is(':checked') ? 1 : 0);
        });
        var nl2brCheck = list.find('.nl2br');
        nl2brCheck.bind('click', function() {
            setCookie('nl2br', nl2brCheck.is(':checked') ? 1 : 0);
        });

        ta.on('keydown', function(event) {
            switch (event.keyCode) {
            case Event.KEY_RETURN:
                if (list.find('.nl2br').is(':checked'))
                    ths.aceEdit.insert('<br/>');
                break;
            }
        });

        list.find('.fff').each(function(eix,img) {
            if (img.className.indexOf('bold') > -1)
                $(this).bind('click', function() { ths.aceEdit.insert('<b>' + ths.aceEdit.session.getTextRange(ths.aceEdit.getSelectionRange()) + '</b>'); });
            if (img.className.indexOf('italic') > -1)
                $(this).bind('click', function() { ths.aceEdit.insert('<i>' + ths.aceEdit.session.getTextRange(ths.aceEdit.getSelectionRange()) + '</i>'); });
            if (img.className.indexOf('underline') > -1)
                $(this).bind('click', function() { ths.aceEdit.insert('<u>' + ths.aceEdit.session.getTextRange(ths.aceEdit.getSelectionRange()) + '</u>'); });
            if (img.className.indexOf('smallcaps') > -1)
                $(this).bind('click', function() { ths.aceEdit.insert('<font size="5" color="black">' + ths.aceEdit.session.getTextRange(ths.aceEdit.getSelectionRange()) + '</font>'); });
            if (img.className.indexOf('link') > -1)
                $(this).bind('click', function () { ths.aceEdit.insert('<a href="http://www.address.com" target="_blank">' + ths.aceEdit.session.getTextRange(ths.aceEdit.getSelectionRange()) + '</a>'); });
            if (img.className.indexOf('text_padding_bottom') > -1)
                $(this).bind('click', function () { alert("not implemented yet"); });
            if (img.className.indexOf('flag_red') > -1)
                $(this).bind('click', function() { ths.aceEdit.insert('$=Provider.TR("' + ths.aceEdit.session.getTextRange(ths.aceEdit.getSelectionRange()) + '")$'); });
            if (img.className.indexOf('picture') > -1)
                $(this).bind('click', function() {
                    openFileManager(null, function(path) {
                        ths.aceEdit.insert('<img src="' + path + '"/>' + ths.aceEdit.session.getTextRange(ths.aceEdit.getSelectionRange()));
                        Windows.getFocusedWindow().close();
                    });
                });
            if (img.className.indexOf('eye') > -1)
                $(this).bind('click', function() {
                    if (!$('#'+ths.editorId + 'Preview').length) {
                        var dim = getDimensions($('#'+ths.editorId + 'ta'));
                        var pos = $('#'+ths.editorId + 'ta').offset();
                        pos = { left: pos.left + 1, top: pos.top + 1 };
                        dim = { width: dim.width - 2, height: dim.height - 2 };
                        Windows.maxZIndex++;
                        $(document.body).append('<div id="' + ths.editorId + 'Preview" style="background:white;overflow:auto;text-align:left;left:' + pos.left + 'px;top:' + pos.top + 'px;z-index:' + Windows.maxZIndex + ';width:' + dim.width + 'px;height:' + dim.height + 'px;position:absolute;"></div>');
                    }
                    $('#'+ths.editorId + 'Preview').html(ths.aceEdit.getValue());
                    showElementWithOverlay('#'+ths.editorId + 'Preview', true, 'black');
                });
        });

        event.preventDefault();

        this.aceEdit = ace.edit(this.editorId + 'ta');
        this.aceEdit.setTheme("ace/theme/eclipse");
        this.aceEdit.getSession().setMode("ace/mode/html");
        this.aceEdit.getSession().setUseWrapMode(wrap == '1');
        this.aceEdit.setValue(this.input.val().gsub('#NL#', '\n'));
        this.aceEdit.focus();
    },
    win:null,
    getValue: function() {
        return this.input.val().gsub('#NL#', '\n');
    },
    setValue: function(val) {
        this.input.val(val ? val.gsub('\n', '#NL#') : '');
    },
    setHtml: function(event) {
        var list = $('#'+this.editorId);
        this.input.val(this.aceEdit.getValue().gsub('\n', '#NL#'));
        this.win.close();
    }
};

//############################
//#       PictureEdit        #
//############################

var PictureEdit = Class.create(); PictureEdit.prototype = {
    fileManager: null,
    initialize: function(id, value, options) {
        Object.extend(this, new Control(id, value, options));
        this.button.bind('click', this.showEditor.bind(this));
        this.input[0]['ctrl'] = this;
    },
    showEditor: function(event) {
		var ths = this;
		openFileManager(ths.getValue(), function(filePath){
			Windows.getFocusedWindow().close();
			ths.setValue(filePath);
		});
    },
    getValue: function() {
        return this.input.val();
    },
    setValue: function(val) {
        this.input.val(val ? val : '');
    }
};

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
    initialize: function(options) {
        Object.extend(this, options);
        if (!this.folder) this.folder = '/UserFiles';
        this.container = $(this.container ? this.container : document.body);
        var html = '<div><div id="fileBrowserList" onselectstart="return false;"></div>' +
            '<div id="fileBrowserFooter">' +
            '<form action="SystemInfo.ashx?method=uploadFile" method="post" enctype="multipart/form-data" target="fakeUplFrm" class="ui-widget-content ui-corner-all">' +
            '<input type="hidden" name="folder"/>' +
            'Dosya: <input type="file" name="upload" multiple="multiple" style="display:inline"/><input type="submit" value="Yükle"/><div id="fileBrowserLoading">&nbsp;</div>' +
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
        this.container.append(html);
        this.container.find('form').each(function(eix,frm) {
            $(frm).on('submit', function() {
                $('#fileBrowserLoading').show();
                $(frm).find('input[name=folder]').val(currFolder);
                if ($(frm).hasClass('delForm'))
                    $(frm).find('input[name=name]').val($A($('#fileBrowserList .fileSelected')).collect(function(elm) { return $(elm).attr('name'); }).join('#NL#'));
            });
        });
        currFolder = this.folder;
        currPicEdit = this;
        this.getFileList();

        var ths = this;

        $('#fb_btnImgEdit').on('click', function() {
            var arr = ths.getSelectedFiles();
            if (arr.length > 0){
				var name = arr[0];
				if(name.endsWith('.png') || name.endsWith('.jpg') || name.endsWith('.jpeg') || name.endsWith('.gif') || name.endsWith('.jpe'))
				    editImage(name);
				else if (name.endsWith('.txt') || name.endsWith('.js') || name.endsWith('.css') || name.endsWith('.html') || name.endsWith('.htm'))
				    editTextFile(name.substring(name.lastIndexOf('/')+1));
				else
					niceAlert(lang('Plese select a picture file to edit'));
			}
        });

    },
    getFileList: function() {
        var list = $('#fileBrowserList');

        if ($('#fileBrowserLoading').length) $('#fileBrowserLoading').show();
        var ths = this;
        new Ajax.Request('SystemInfo.ashx?method=getFileList&folder=' + currFolder, {
            onComplete: function(resp) {
                resp = eval("(" + resp.responseText + ")");
                if (resp.success) {
                    var folders = currFolder.substring(1).split('/');
                    var folderLinks = '';
                    for (var i = 0; i < folders.length; i++) {
                        var str = '';
                        for (var k = 0; k <= i; k++)
                            str += '/' + folders[k];
                        folderLinks += '<span onclick="currFolder = \'' + str + '\'; currPicEdit.getFileList();">' + folders[i] + '</span>' + ' / ';
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
                            } else {
                                var src = ('/external/icons/' + fileClass + '.png');
                                str += '<div class="fileNameBox ' + ths.getFileClassName(item) + (item.size < 0 ? "" : " fileItem") + ' ui-widget-content ui-corner-all" name="' + item.name + '"><img src="' + src + '"/><br/>' + item.name + '</div>';
                            }

                        }
                    }
                    list.html(str);
                    list.find('.folder').each(function(eix,elm) {
                        $(elm).on('click', function(event) {
                            var path = currFolder + '/' + $(elm).attr('name');
                            if (!(event.ctrlKey || event.shiftKey || event.metaKey))
                                list.find('.fileNameBox').each(function(eix,fnm) { $(fnm).removeClass('fileSelected'); });
                            $(elm).toggleClass('fileSelected');
                            $('#fileManagerRenameForm input[name=newName]').val($(elm).attr('name'));
                        });
                        $(elm).on('dblclick', function() {
                            var f = $(elm).attr('name');
                            currFolder = currFolder + '/' + f;
                            ths.getFileList();
                        });
                    });
                    list.find('.fileItem').each(function(eix,elm) {
                        $(elm).on('dblclick', function() {
                            var path = currFolder + '/' + $(elm).attr('name');
                            if (ths.onSelectFile)
                                ths.onSelectFile(path);
                        });
                        $(elm).on('click', function(event) {
                            var path = currFolder + '/' + $(elm).attr('name');
                            if (!(event.ctrlKey || event.shiftKey || event.metaKey))
                                list.find('.fileNameBox').each(function(eix,fnm) { $(fnm).removeClass('fileSelected'); });
                            $(elm).toggleClass('fileSelected');
                            $('#fileManagerRenameForm input[name=newName]').val($(elm).attr('name'));
                        });
                    });
                } else
                    alert(resp.errorMessage);

                $('#fileBrowserLoading').hide();
            }
        });

    },
    getSelectedFiles: function() {
        return $A($('#fileBrowserList .fileSelected')).collect(function(elm) { return currFolder + '/' + $(elm).attr('name'); });
    },
    getSize: function(item) {
        if (item.size < 0) return ''; //***

        if (item.size >= 1024 * 1024) return Math.round(item.size / 1024 / 1024) + ' MB';
        if (item.size >= 1024) return Math.round(item.size / 1024) + ' KB';
        return item.size + ' B';
    },
    getFileClassName: function(item) {
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
    formatDate: function(d) {
        var date = d.getDate(), month = d.getMonth() + 1, hour = d.getHours(), minute = d.getMinutes();
        if (date.toString().length == 1) date = '0' + date;
        if (month.toString().length == 1) month = '0' + month;
        if (hour.toString().length == 1) hour = '0' + hour;
        if (minute.toString().length == 1) minute = '0' + minute;
        return date + '/' + month + '/' + d.getFullYear() + ' ' + hour + ':' + minute;
    }
};
fileBrowserUploadFeedback = function (msg, url) {
	currPicEdit.getFileList();
	currPicEdit.container.find('form').trigger('reset');
}

function editImage(path){
	if(typeof path == 'string'){
		if(path.startsWith('/_thumbs')){
			niceAlert(lang('Editable image path not defined'));
			return;
		}
		if(!path.startsWith('/'))
			path = $(path).attr('path') || $(path).attr('src');
	}
	else {
		path = $(path).attr('path') || $(path).attr('src');
	}
	
	var win = new Window({ className: 'alphacube', title: '<span class="fff edit"></span> ' + lang('Edit Picture'), resizable: false, maximizable: false, minimizable: false, width: 800, height: 600, wiredDrag: true, destroyOnClose: true }); 
	var str = '<div class="cc_ei_toolbar"><span id="cc_select" class="ccBtn"><span class="fff shape_handles"></span> Select</span><span id="cc_crop" class="ccBtn"><span class="fff cut"></span> Crop</span><span id="cc_turncw" class="ccBtn"><span class="fff arrow_rotate_clockwise"></span> Turn CW</span><span id="cc_turnccw" class="ccBtn"><span class="fff arrow_rotate_anticlockwise"></span> Turn CCW</span><span class="fff shape_group"></span> <input id="cc_ei_width"/> x <input id="cc_ei_height"/><span id="cc_resize" class="ccBtn"> Resize</span><span id="cc_reset" class="ccBtn"><span class="fff arrow_undo"></span> Reset</span></div>';
	str += '<div class="cc_ei_canvas"><img id="cc_ei_preview" src="'+path+'"/><div id="cc_ei_selection" style="display:none"></div><div id="cc_ei_nw" style="display:none"></div><div id="cc_ei_se" style="display:none"></div></div>';
	str += '<div id="cc_ei_status"></div>';
	win.getContent().prepend(str);
	win.showCenter();
	win.toFront();
	
	var imgPreview = $('#cc_ei_preview');
	var sel = $('#cc_ei_selection');

	$('#cc_select').on('click', toggleSelect);
	function toggleSelect(){
		sel.toggle();
		$('#cc_ei_nw').toggle();
		$('#cc_ei_se').toggle();
	}
	
	$('#cc_ei_selection').draggable({drag:function(){
		var dim = getDimensions(sel);
		var pos = sel.position();
		$('#cc_ei_nw').css({left:(pos.left-5)+'px', top:(pos.top-5)+'px'});
		$('#cc_ei_se').css({left:(pos.left+dim.width-5)+'px', top:(pos.top+dim.height-5)+'px'});
		$('#cc_ei_status').html('<b>File:</b> ' + path + ' &nbsp; <b>Selection:</b> ' + pos.left + ', ' + pos.top + ', ' + dim.width + ', ' + dim.height);
	}});
	$('#cc_ei_nw').draggable({drag:resizeSelection});
	$('#cc_ei_se').draggable({drag:resizeSelection});
	
	function resizeSelection(){
		var posNW = $('#cc_ei_nw').position();
		var posSE = $('#cc_ei_se').position();
		sel.css({
			left: (posNW.left+5) + 'px',
			top: (posNW.top+5) + 'px',
			width: (posSE.left-posNW.left) + 'px',
			height: (posSE.top-posNW.top) + 'px'
		});
		
		var dim = getDimensions(sel);
		var pos = sel.position();
		$('#cc_ei_status').html('<b>File:</b> ' + path + ' &nbsp; <b>Selection:</b> ' + pos.left + ', ' + pos.top + ', ' + dim.width + ', ' + dim.height);
	}
	
	imgPreview.on('load', function(){
		var dim = getDimensions(imgPreview);
		var dimCanvas = getDimensions(imgPreview.parent());
		if(dimCanvas.width>dim.width)
			imgPreview.css({left:(dimCanvas.width-dim.width)/2+'px'});
		if(dimCanvas.height>dim.height)
			imgPreview.css({top:(dimCanvas.height-dim.height)/2+'px'});
		if(dimCanvas.width<dim.width)
			imgPreview.parent().scrollLeft = (dim.width-dimCanvas.width)/2;
		if(dimCanvas.height<dim.height)
			imgPreview.parent().scrollTop = (dim.height-dimCanvas.height)/2;
		
		$('#cc_ei_width').val(dim.width);
		$('#cc_ei_height').val(dim.height);
		
		$('#cc_ei_status').html('<b>File:</b> ' + path);
	});
	
	$('#cc_ei_width').on('change', function(){
		var dim = getDimensions(imgPreview);
		var w = parseInt($('#cc_ei_width').val());
		$('#cc_ei_height').val(Math.round(w*dim.height/dim.width));
	});
	$('#cc_ei_height').on('change', function(){
		var dim = getDimensions(imgPreview);
		var h = parseInt($('#cc_ei_height').val());
		$('#cc_ei_width').val(Math.round(h*dim.width/dim.height));
	});
	
	$('#cc_crop').on('click', function(){
		if(!sel.is(':visible')){
			niceAlert('Select first');
			return;
		}
		var dim = getDimensions(sel);
		var pos = sel.position();
		var posImg = imgPreview.position();
		
		var res = ajax({url:'EditImageCrop.ashx?x='+(pos.left-posImg.left)+'&y='+(pos.top-posImg.top)+'&w='+dim.width+'&h='+dim.height+'&path='+path, isJSON:false, noCache:true});
		if(res.startsWith('ERR:')){
			niceAlert(res);
			return;
		}
		else {
			if(sel.is(':visible')) toggleSelect();
			imgPreview.attr('src', path + '?' + new Date().getMilliseconds());
		}
	});
	$('#cc_turncw').on('click', function(){
		var res = ajax({url:'EditImageRotate.ashx?dir=CW&path='+path, isJSON:false, noCache:true});
		if(res.startsWith('ERR:')){
			niceAlert(res);
			return;
		}
		else {
			if(sel.is(':visible')) toggleSelect();
			imgPreview.attr('src', path + '?' + new Date().getMilliseconds());
		}
	});
	$('#cc_turnccw').on('click', function(){
		var res = ajax({url:'EditImageRotate.ashx?dir=CCW&path='+path, isJSON:false, noCache:true});
		if(res.startsWith('ERR:')){
			niceAlert(res);
			return;
		}
		else {
			if(sel.is(':visible')) toggleSelect();
			imgPreview.attr('src', path + '?' + new Date().getMilliseconds());
		}
	});
	$('#cc_resize').on('click', function(){
		var res = ajax({url:'EditImageResize.ashx?width='+$('#cc_ei_width').val()+'&height='+$('#cc_ei_height').val()+'&path='+path, isJSON:false, noCache:true});
		if(res.startsWith('ERR:')){
			niceAlert(res);
			return;
		}
		else {
			if(sel.is(':visible')) toggleSelect();
			imgPreview.attr('src', path + '?' + new Date().getMilliseconds());
		}
	});
	$('#cc_reset').on('click', function(){
		var res = ajax({url:'EditImageReset.ashx?path='+path, isJSON:false, noCache:true});
		if(res.startsWith('ERR:')){
			niceAlert(res);
			return;
		}
		else {
			if(sel.is(':visible')) toggleSelect();
			imgPreview.attr('src', path + '?' + new Date().getMilliseconds());
		}
	});
}

function editTextFile(name) {
    new AceEditor({
        titleIcon: 'page',
        title: currFolder + '/' + name,
        buttons: [{ icon: 'save', id: 'btnSaveTextFile', text: lang('Save'), callback: function (editor) { saveTextFile(editor); } }],
        text: ajax({ url: 'SystemInfo.ashx?method=getTextFile&name=' + name + '&folder=' + currFolder, isJSON: false, noCache: true }),
        lang: name.substring(name.lastIndexOf('.') + 1)
    });

    function saveTextFile(editor) {
        var params = new Object();
        params['name'] = name;
        params['folder'] = currFolder;
        params['source'] = editor.getValue();
        new Ajax.Request('SystemInfo.ashx?method=saveTextFile', {
            method: 'post',
            parameters: params,
            onComplete: function (req) {
                if (req.responseText.startsWith('ERR:')) { niceAlert(req.responseText); return; }
                Windows.getFocusedWindow().close();
                niceInfo(name + ' saved.');
            },
            onException: function (req, ex) { throw ex; }
        });
    }
}

//############################
//#          LookUp          #
//############################

var LookUp = Class.create(); LookUp.prototype = {
    text: '',
    lastValue: 0,
    items: [],
    listHeight: 200,
    initialize: function (id, value, options) {
        Object.extend(this, new Control(id, value, options));
        this.button.bind('click', this.showEditor.bind(this));
        this.input.bind('focus', this.focus.bind(this));
        this.input.bind('blur', this.blur.bind(this));
        this.input[0]['ctrl'] = this;

        if (this.options.entityName) {
            var txt = value ? ajax({ url: this.options.itemsUrl + '?method=getEntityNameValue&entityName=' + this.options.entityName + '&id=' + value, isJSON: false, noCache: false }) : '';
            this.setText(txt);
        }
    },
    showEditor: function (event) {
		var ths = this;
		openEntityListForm({
			entityName: ths.options.entityName, 
			extraFilter: ths.options.extraFilter,
			title:ths.label,
			hideFilterPanel:true,
			selectCallback: function(v, txt){
				Windows.getFocusedWindow().close();
				ths.onSelect(v, txt);
			}
		});
    },
    complete: function () {
        if (this.input.val() && this.input.val().length >= 1) {
            if (!this.editor) {
                this.div.after('<div class="editor hideOnOut" style="text-align:left;overflow:auto;position:absolute;border:1px solid black;display:none;background:white"></div>');
                this.editor = this.div.next();
            }
            var ths = this;
            var params = { extraFilter: ths.options.extraFilter + (ths.options.extraFilter ? ' AND ' : '') + '_nameField_like' + ths.input.val() + '%' };
            new Ajax.Request(ths.options.itemsUrl + '?method=getList&entityName=' + ths.options.entityName, {
                method: 'post',
                parameters: params,
                onComplete: function (req) {
                    if (req.responseText.startsWith('ERR:')) {
                        niceAlert(req.responseText);
                        ths.items = [];
                        return;
                    } else {
                        ths.items = eval('(' + req.responseText + ')');

                        var list = $(ths.editor);
                        list.html('');

                        var insertionHtml = '';
                        for (var i = 0; i < ths.items.length; i++) {
                            var row = ths.items[i];
                            var text = typeof row == 'object' ? (row.length > 1 ? row[1] : row[0]) : row;
                            insertionHtml += '<div class="item" id="__itm' + ths.hndl + '_' + i + '">' + text + '</div>';
                        }
                        list.append(insertionHtml);

                        if (list.outerHeight() > ths.listHeight) list.css({ height: ths.listHeight + 'px' });
                        var i = 0;
                        var elm = $('#__itm' + ths.hndl + '_' + i);
                        while (elm) {
                            elm.bind('mouseover', ths.onItemMouseOver.bind(ths));
                            elm.bind('mouseout', ths.onItemMouseOut.bind(ths));
                            elm.bind('click', ths.onItemClick.bind(ths));
                            i++;
                            elm = $('#__itm' + ths.hndl + '_' + i);
                        }

                        if (!ths.listDimensionCalculated) {
                            var w = ths.div.width();
                            var h = list.outerHeight();
                            h = h > ths.listHeight ? ths.listHeight : h;
                            list.css({ height: h + 'px', width: w + 'px' });
                            ths.listDimensionCalculated = true;
                        }

                        list.show();
                    }
                },
                onException: function (req, ex) { throw ex; }
            });
        }
    },
    listDimensionCalculated: false,
    onItemMouseOver: function (e) {
        var elm = $(e.target).closest('DIV');
        elm.addClass('selItem');
    },
    onItemMouseOut: function (e) {
        var elm = $(e.target).closest('DIV');
        elm.removeClass('selItem');
    },
    onItemClick: function (e) {
        var elm = $(e.target).closest('DIV');
        var list = this.editor;
        var index = list.find('.item').indexOf(elm);
        var val = this.items[index];
        this.setValue(val[0]);
        this.setText(val[1]);
        list.hide();
    },
    focus: function () {
        this.text = this.input.val();
        this.lastValue = this.value;
    },
    blur: function () {
        if (this.input.val() == '') {
            this.setValue(0);
            this.setText('');
            return;
        }
        if (this.text != this.input.val() && this.lastValue == this.value) { // text changed but value doesnt.
            //yeni id'yi oku value'ya set et. id yoksa texti eski haline çevir.
            var ths = this;
            var params = { name: ths.input.val(), extraFilter: ths.options.extraFilter };
            new Ajax.Request(ths.options.itemsUrl + '?method=getEntityId&entityName=' + ths.options.entityName, {
                method: 'post',
                parameters: params,
                onComplete: function (req) {
                    if (req.responseText.startsWith('ERR:')) {
                        niceAlert(req.responseText);
                        return;
                    }
                    try {
                        entObj = eval('(' + req.responseText + ')');
                    } catch (e) {
                        niceAlert(e.message);
                    }
                    if (entObj && entObj.id) {
                        ths.setValue(entObj.id);
                        ths.setText(entObj.name);
                    } else {
                        ths.setText(ths.text);
                    }
                },
                onException: function (req, ex) { throw ex; }
            });
        }
    },
    onSelect: function (v, txt) {
        this.setValue(v);
        this.setText(txt);
        $('#'+this.editorId).remove();
    },
    getValue: function () {
        return this.value;
    },
    setValue: function (v) {
        this.value = v;
    },
    setText: function (txt) {
        this.input.val(txt);
    }
};

//#########################
//#       TagEdit        #
//#########################
var autoCompleteTagEdit = true;
var TagEdit = Class.create(); TagEdit.prototype = {
    listHeight: 200,
    initialize: function(id, value, options) {
        Object.extend(this, new Control(id, value, options));
        if (options.listHeight) this.listHeight = options.listHeight;
        this.options.hideBtn = true;
		
        var ths = this;

        this.input.keyup(function (event) {
            if (!autoCompleteTagEdit)
                return;

			switch(event.keyCode){
				case Event.KEY_UP:
				case Event.KEY_DOWN:
				case Event.KEY_RETURN:
					return false;
			}
			var str = ths.getValue().split(',').last().trim();
			if(str.length>=2)
				new Ajax.Request(ths.options.itemsUrl + '?method=getList&entityName=' + ths.options.entityName+'&extraFilter=_nameField_like'+str+'%', {
					method: 'get',
					asynchronous: false,
					onComplete: function(req) {
						if (req.responseText.startsWith('ERR:'))
							ths.options.items = [req.responseText.substr(4)];
						else {
							ths.options.items = $A(eval('(' + req.responseText + ')')).findAll(function(elm){return elm[0]!=0;});
							ths.openList();
						}
					},
					onException: function(req, ex) { throw ex; }
				});
			else {
				var list = $(ths.editor);
				list.html('');
				list.hide();
				currEditor = null;
			}
		});

		this.input.keydown(function(event){
			switch(event.keyCode){
				case Event.KEY_UP:
					var sel = $(ths.editor).find('.selItem');
					sel.prev().mouseover();
					return false;
				case Event.KEY_DOWN:
					var sel = $(ths.editor).find('.selItem');
					if(sel.length)
						sel.next().mouseover();
					else
						$(ths.editor).find('.item:first').mouseover();
					return false;
				case Event.KEY_RETURN:
					var sel = $(ths.editor).find('.selItem');
					sel.click();
					return false;
			}
		});
   
		this.input[0]['ctrl'] = this;
    },
    beforeOpenList: function() {
        if (!(this.editor && this.editor.length)) {
            this.div.after('<div class="editor hideOnOut TagEdit" style="display:none"></div>');
            this.editor = this.div.next();
        }
        this.fetchData();
    },
    openList: function() {
        this.beforeOpenList();

        var list = $(this.editor);
        if (this.input.is(':disabled')) return;
		list.height('');
		var w = this.div.width();
		var h = list.outerHeight();
		h = h > this.listHeight ? this.listHeight : h;
		list.css({ height: h + 'px', width: w + 'px' });

        list.show();
		currEditor = list;
    },
    fetchData: function() {
        var list = $(this.editor);
        list.html('');

        var insertionHtml = '';
        for (var i = 0; i < this.options.items.length; i++) {
            var row = this.options.items[i];
            var text = typeof row == 'object' ? (row.length > 1 ? row[1] : row[0]) : row;
            insertionHtml += '<div class="item" id="__itm' + this.hndl + '_' + i + '"><nobr>' + text + '</nobr></div>';
        }
        list.append(insertionHtml);

        if (list.outerHeight() > this.listHeight) list.css({ height: this.listHeight + 'px' });
        var i = 0;
        var elm = $('#__itm' + this.hndl + '_' + i);
        while (elm.length) {
            elm.bind('mouseover', this.onItemMouseOver.bind(this));
            elm.bind('click', this.onItemClick.bind(this));
            i++;
            elm = $('#__itm' + this.hndl + '_' + i);
        }

        list.hide();
		currEditor = null;
    },
    getValue: function() {
        return this.input.val();
    },
    setValue: function(val) {
        return this.input.val(val);
    },
    onItemMouseOver: function(e) {
        var elm = $(e.target).closest('.item');
		elm.closest('.editor').find('.item').removeClass('selItem');
        elm.addClass('selItem');
    },
    onItemClick: function(e) {
        var elm = $(e.target).closest('DIV');
        var list = this.editor;
		var lastVal = this.getValue().substring(0, this.getValue().lastIndexOf(','));
		this.setValue((lastVal ? lastVal+', ' : '') + elm.text() + ", ");
        list.hide();
		currEditor = null;
		this.input.focus();
		this.input.val(this.input.val());
    }
};

//############################
//#         MemoEdit         #
//############################

var MemoEdit = Class.create(); MemoEdit.prototype = {
    initialize: function(id, value, options) {
        Object.extend(this, new Control(id, value, options));
        this.button.bind('click', this.showEditor.bind(this));
        this.input[0]['ctrl'] = this;
    },
    showEditor: function(event) {
        if ($('#'+this.editorId).length) {
            $('#'+this.editorId).remove();
            currEditor = null;
            return false;
        }

        var ths = this;

        $(document.body).append('<div class="editor MemoEdit" style="display:none" id="' + this.editorId + '">' +
            '<div id="' + this.editorId + 'ta" style="height: 429px;border-bottom: 1px solid #bbb;"></div><br/>' +
            '<center>' +
            '<span id="' + this.editorId + 'btnOK" class="ccBtn"><span class="fff accept"></span> ' + lang('OK') + '</span> ' +
            (this.docType == 'css' ? '<span id="' + this.editorId + 'btnDefault" class="ccBtn"><span class="fff lorry"></span> ' + lang('Load default') + '</span> ' : '') +
            '<span id="' + this.editorId + 'btnPicture" class="ccBtn"><span class="fff picture"></span> ' + lang('Add picture') + '</span> ' +
            '<span id="' + this.editorId + 'btnCancel" class="ccBtn"><span class="fff cancel"></span> ' + lang('Cancel') + '</span>' +
            '</center>' +
            '</div>');

        var list = $('#'+this.editorId);
        if (this.input.disabled) return;

        var btnOK = $('#'+this.editorId + 'btnOK');
        var btnCancel = $('#'+this.editorId + 'btnCancel');
        btnOK.bind('click', this.setValueByEditor.bind(this));
        btnCancel.bind('click', this.showEditor.bind(this));

        var ths = this;
        $('#'+this.editorId + 'btnPicture').bind('click', function() {
            openFileManager(null, function(path) {
                ths.aceEdit.insert(path);
                Windows.getFocusedWindow().close();
            });
        });

        if (this.afterShowEditor) this.afterShowEditor();

        this.setEditorPos(list);

        list.show();
        event.stopPropagation();

        if (this.id == 'SQL') this.docType = 'sql';

        this.aceEdit = ace.edit(this.editorId + 'ta');
        this.aceEdit.setTheme("ace/theme/eclipse");
        this.aceEdit.getSession().setMode("ace/mode/" + (this.docType ? this.docType : 'html'));
        //this.aceEdit.getSession().setUseWrapMode(wrap == '1');
        this.aceEdit.setValue(this.input.val().gsub('#NL#', '\n'));
        this.aceEdit.focus();
    },
    afterShowEditor: null,
    getValue: function() {
        return this.input.val().gsub('#NL#', '\n');
    },
    setValue: function(val) {
        this.input.val(val ? val.gsub('\n', '#NL#') : '');
    },
    setValueByEditor: function(event) {
        var list = $('#'+this.editorId);
        this.input.val(this.aceEdit.getValue().gsub('\n', '#NL#'));
        list.remove();
    }
};

//############################
//#          CSSEdit         #
//############################

var __oldCSSBtnDefaultClick;
var CSSEdit = Class.create(); CSSEdit.prototype = {
    initialize: function(id, value, options) {
        var memo = new MemoEdit(id, value, options);
        memo.docType = 'css';
        memo.afterShowEditor = this.afterShowEditor;
        memo.loadDefaultCSS = this.loadDefaultCSS.bind(memo);
        Object.extend(this, memo);
    },
    afterShowEditor: function() {
        var list = $('#'+this.editorId);
        var btnDefault = $('#'+this.editorId + 'btnDefault');

        btnDefault.unbind('click', __oldCSSBtnDefaultClick);
        __oldCSSBtnDefaultClick = this.loadDefaultCSS.bind(this);
        btnDefault.bind('click', __oldCSSBtnDefaultClick);
    },
    loadDefaultCSS: function(event) {
        var form = this.options.relatedEditForm;
        var ths = this;
        new Ajax.Request('ModuleInfo.ashx?method=getDefaultCSS&name=' + form.entityName + '&id=' + form.entityId, {
            method: 'get',
            asynchronous: false,
            onComplete: function(req) {
                if (req.responseText.startsWith('ERR:'))
                    niceAlert(req.responseText);
                else
                    ths.aceEdit.setValue(req.responseText);
            }
        });
    },
    setValue: function(val) {
        this.input.val(val ? val : '');
    }
};

//############################
//#        FilterEdit        #
//############################

var __oldFilterBtnOKClick, __oldFilterBtnCancelClick;
var FilterEdit = Class.create(); FilterEdit.prototype = {
    filter: null,
    initialize: function(id, value, options) {
        Object.extend(this, new Control(id, value, options));
        var filtEdit = $('#'+this.editorId);
        if (filtEdit.length)
            filtEdit.find(':first').html('');
        this.button.bind('click', this.showEditor.bind(this));

        if(this.input.length) this.input[0]['ctrl'] = this;
    },
    showEditor: function(event) {
        if (!$('#'+this.editorId).length)
            $(document.body).append('<div class="editor FilterEdit" style="display:none" id="' + this.editorId + '"><div id="' + this.editorId + 'div" style="overflow:auto;height:270px"></div><center><span id="' + this.editorId + 'btnOK" class="ccBtn"><span class="fff accept"></span> ' + lang('OK') + '</span> <span id="' + this.editorId + 'btnCancel" class="ccBtn"><span class="fff cancel"></span> ' + lang('Cancel') + '</span></center></div>');
        else
            $('#'+this.editorId).find(':first').html("");
        var entityNameToUse = this.options.entityName;
        if (this.options.relatedEditForm && entityNameToUse.startsWith('use#')) {
            entityNameToUse = entityNameToUse.substr(4);
            entityNameToUse = this.options.relatedEditForm.getControl(entityNameToUse).getValue();
        }

        this.filter = new FilterEditor($('#'+this.editorId).find(':first'), ajax({ url: 'EntityInfo.ashx?method=getFieldsList&entityName=' + entityNameToUse, isJSON: true, noCache: false }));

        var list = $('#'+this.editorId);
        if (list.is(':visible')) {
            list.remove();
            currEditor = null;
            return false;
        }
        if (this.input.is(':disabled')) return;

        var btnOK = $('#'+this.editorId + 'btnOK');
        var btnCancel = $('#'+this.editorId + 'btnCancel');
        btnOK.unbind('click', __oldFilterBtnOKClick);
        btnCancel.unbind('click', __oldFilterBtnCancelClick);
        __oldFilterBtnOKClick = this.setValueByEditor.bind(this);
        __oldFilterBtnCancelClick = this.showEditor.bind(this);
        btnOK.bind('click', __oldFilterBtnOKClick);
        btnCancel.bind('click', __oldFilterBtnCancelClick);

        if (this.afterShowEditor) this.afterShowEditor();

        this.filter.setFilters(this.input.val());

        this.setEditorPos(list);

        list.show();
        list.find(':first').focus();
        event.stopPropagation();
    },
    afterShowEditor: null,
    getValue: function() {
        return this.input.val();
    },
    setValue: function(val) {
        this.input.val(val ? val : '');
    },
    setValueByEditor: function() {
        var list = $('#'+this.editorId);
        var h = this.filter.serialize();
        var str = '';
        var i = 0;
        while (true) {
            if (!h['f_' + i]) break;
            if (i > 0) str += ' AND ';
            str += h['f_' + i] + h['o_' + i] + h['c_' + i];
            i++;
        }
        this.input.val(str);
        list.remove();
    }
};

//############################
//#       DateTimeEdit       #
//############################

var __monthCombo = null;
var __yearCombo = null;
var DateTimeEdit = Class.create(); DateTimeEdit.prototype = {
    dateValue: null,
    initialize: function(id, value, options) {
        if (value instanceof Date) {
            this.dateValue = value;
            value = this.getValue();
        }
        Object.extend(this, new Control(id, value, options));
        this.setText(value);
        this.button.bind('click', this.showEditor.bind(this));
        this.input.bind('click', this.showEditor.bind(this));
        this.input[0]['ctrl'] = this;
        this.input.readOnly = true;
    },
    showEditor: function(event) {
        if (!$('#'+this.editorId).length) {
            $(document.body).append('<div class="editor removeOnOut DateTimeEdit" style="display:none" id="' + this.editorId + '"><table width="100%"><tr><td class="cH" id="__cH1"></td><td class="cH" id="__cH2"></td></tr></table><div id="__cM"></div></div>');
            var editor = $('#'+this.editorId);
            __monthCombo = new ComboBox('_cH1', this.dateValue.getMonth() + 1, { container: $('#__cH1'), width: 80, listHeight: 100, items: [[1, lang('January')], [2, lang('February')], [3, lang('March')], [4, lang('April')], [5, lang('May')], [6, lang('June')], [7, lang('July')], [8, lang('August')], [9, lang('September')], [10, lang('October')], [11, lang('November')], [12, lang('December')]], onChange: this.monthYearChanged.bind(this) });
            __yearCombo = new ComboBox('_cH2', this.dateValue.getFullYear(), { container: $('#__cH2'), width: 60, listHeight: 100, items: $R(1930, (new Date()).getFullYear() + 5).toArray().reverse(false), onChange: this.monthYearChanged.bind(this) });
        }

        var editor = $('#'+this.editorId);
        var ctrl = this.div;

        if (editor.is(':visible')) {
            editor.remove();
            currEditor = null;
            return false;
        } else {
            this.buildCal(this.dateValue.getMonth() + 1, this.dateValue.getFullYear());
            this.setEditorPos(editor);
            editor.show();
        }
        event.stopPropagation();
    },
    monthYearChanged: function() {
        if (__monthCombo)
            this.buildCal(__monthCombo.value, __yearCombo.value);
    },
    selectDay: function(event) {
        var td = $(event.target);
        this.dateValue = new Date(__yearCombo.value, __monthCombo.value - 1, td.html());

        var str = this.getValue();
        str = str.substr(0, str.length - 9);
        if (this.input.val() != str) {
            this.input.val(str);
            this.options.onChange();
        }
        $('#'+this.editorId).remove();
    },
    // thanks to Brian Gosselin for the function below
    buildCal: function(m, y) {
        var cM = "width:100%";
        var cH = "font-weight:bold";
        var cDW = "background:#efefef";
        var cD = "cursor:pointer";
        var mn = [lang('January'), lang('February'), lang('March'), lang('April'), lang('May'), lang('June'), lang('July'), lang('August'), lang('September'), lang('October'), lang('November'), lang('December')];
        var dim = [31, 0, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

        var oD = new Date(y, m - 1, 1);
        oD.od = oD.getDay() + 1;

        var todaydate = new Date();
        var scanfortoday = (y == todaydate.getFullYear() && m == todaydate.getMonth() + 1) ? todaydate.getDate() : 0;

        dim[1] = (((oD.getFullYear() % 100 != 0) && (oD.getFullYear() % 4 == 0)) || (oD.getFullYear() % 400 == 0)) ? 29 : 28;
        var t = '<table style="' + cM + '" cellpadding="2" border="0" cellspacing="0">';
        t += '<tr align="center">';
        for (s = 0; s < 7; s++)
            t += '<td style="' + cDW + '">' + lang('SMTWTFS').substr(s, 1) + '</td>';
        t += '</tr><tr align="center">';
        for (i = 1; i <= 42; i++) {
            var x = ((i - oD.od >= 0) && (i - oD.od < dim[m - 1])) ? i - oD.od + 1 : '&nbsp;';
            if (x == scanfortoday)
                x = '<span style="font-weight:bold">' + x + '</span>';
            t += '<td style="' + cD + '"' + (x != '&nbsp;' ? ' class="calDay"' : '') + '>' + x + '</td>';
            if (((i) % 7 == 0) && (i < 36))
                t += '</tr><tr align="center">';
        }
        t += '</tr></table>';

        $('#'+this.editorId+' #__cM').html(t);
        var ths = this;
        $('#'+this.editorId+' .calDay').each(function(eix,elm) { $(elm).bind('click', ths.selectDay.bind(ths)); });
    },
    parse: function (val) {
        var parts = val.split('T');
        var dmy = parts[0];
        var hm;
        if(parts.length>1)
            hm = val.split('T')[1];
        dmy = dmy.split('-');
        if(hm)
            hm = hm.split(':');
        var dt = new Date();
        dt.setFullYear(parseInt(dmy[0]));
        dt.setMonth(parseInt(dmy[1]) - 1);
        dt.setDate(parseInt(dmy[2]));
        if (hm) {
            dt.setHours(parseInt(hm[0]));
            dt.setMinutes(parseInt(hm[1]));
        }

        return dt;
    },
    setText: function(value) {
        this.dateValue = this.parse(value);
        this.input.val(value.substr(0, value.length - 9));
    },
    getValue: function() {
        var d = this.dateValue;
        return d.getFullYear() + '-' + this.addZero(d.getMonth() + 1) + '-' + this.addZero(d.getDate()) + ' ' + this.addZero(d.getHours()) + ':' + this.addZero(d.getMinutes()) + ':' + this.addZero(d.getSeconds());
    },
    setValue: function(d) {
        if (!d) return;
        if (typeof d == 'string') {
            this.setText(d);
            d = this.dateValue;
        } else
            this.input.val(d.getFullYear() + '-' + this.addZero(d.getMonth() + 1) + '-' + this.addZero(d.getDate()) + ' ' + this.addZero(d.getHours()) + ':' + this.addZero(d.getMinutes()) + ':' + this.addZero(d.getSeconds()));
    },
    addZero: function(num) {
        if (num.toString().length < 2)
            return '0' + num;
        return num.toString();
    }
};

//#########################
//#       ComboBox        #
//#########################

var ComboBox = Class.create(); ComboBox.prototype = {
    listHeight: 200,
    initialize: function(id, value, options) {
        Object.extend(this, new Control(id, value, options));
        if (options.listHeight) this.listHeight = options.listHeight;
        if (!options.hideItems) {
            this.button.bind('click', this.openList.bind(this));
            this.input.bind('click', this.openList.bind(this));
        } else
            this.options.hideBtn = true;

        this.input.readOnly = true;

        if (this.options.items && this.options.addBlankItem && this.options.items[0][0] != '')
            this.options.items.unshift(['', lang('Select')]);
        var ths = this;
        if (this.options.itemsUrl) {
            new Ajax.Request(this.options.itemsUrl + '?method=getList&entityName=' + this.options.entityName, {
                method: 'get',
                asynchronous: false,
                onComplete: function(req) {
                    if (req.responseText.startsWith('ERR:'))
                        ths.options.items = [req.responseText.substr(4)];
                    else {
                        ths.options.items = eval('(' + req.responseText + ')');
                        if (ths.options.addBlankItem)
                            ths.options.items.unshift(['', lang('Select')]);
                    }
                },
                onException: function(req, ex) { throw ex; }
            });
        } else
            this.setValue(value);

        this.input[0]['ctrl'] = this;
    },
    beforeOpenList: function() {
        if (!(this.editor && this.editor.length)) {
            this.div.after('<div class="editor hideOnOut ComboBox" style="display:none"></div>');
            this.editor = this.div.next();
            this.fetchData();
        }
    },
    beforeOpenListForFields: function() {
        if (!(this.editor && this.editor.length)) {
            this.div.after('<div class="editor hideOnOut ComboBox" style="display:none"></div>');
            this.editor = this.div.next();
        }
        var entityNameToUse = this.options.entityName;
        if (entityNameToUse.startsWith('use#')) {
            entityNameToUse = entityNameToUse.substr(4);
            entityNameToUse = this.options.relatedEditForm.getControl(entityNameToUse).getValue();
            if (!entityNameToUse) entityNameToUse = 'Content'; // bi default olsun hesabı... :(
        }
        if (entityNameToUse != this.currEntityName) {
            var fields = ajax({ url: 'EntityInfo.ashx?method=getFieldsList&entityName=' + entityNameToUse, isJSON: true, noCache: false });
            var ths = this;
            this.options.items = [];
            this.options.items.push(['', 'Hiçbiri']);
            fields.each(function(f) { ths.options.items.push([f.id, f.label]) });
            this.fetchData();
            this.currEntityName = entityNameToUse;
        }
    },
    listDimensionCalculated: false,
    openList: function(event) {
        if (this.options.entityName && this.options.entityName.startsWith('use#'))
            this.beforeOpenListForFields();
        else
            this.beforeOpenList();

        var list = $(this.editor);
        if (this.input.is(':disabled')) return;

        if (!this.listDimensionCalculated) {
            var w = this.div.width();
            var h = list.outerHeight();
            h = h > this.listHeight ? this.listHeight : h;
            list.css({ height: h + 'px', width: w + 'px' });
            this.listDimensionCalculated = true;
        }

        if (this.options.multiSelect) { // multiselect listeler için seçili olanları yeşil falan gösterelim.
            var i = 0;
            var elm = $('#__itm' + this.hndl + '_' + i);
            while (elm.length) {
                elm.removeClass('checkedItem');
                if (this.input.val().indexOf(elm.text()) > -1)
                    elm.addClass('checkedItem');
                i++;
                elm = $('#__itm' + this.hndl + '_' + i);
            }
        } else {
            var i = 0, selElm = null;
            var elm = $('#__itm' + this.hndl + '_' + i);
            while (elm.length) {
                if (this.input.val().indexOf(elm.text()) > -1) selElm = elm;
                elm.removeClass('checkedItem');
                i++;
                elm = $('#__itm' + this.hndl + '_' + i);
            }
            if (selElm && selElm.length) {
                selElm.addClass('checkedItem');
                //Scroll.to(selElm);
            }
        }

        list.show();
		currEditor = list;
    },
    fetchData: function() {
        var list = $(this.editor);
        list.html('');

        var insertionHtml = '';
        for (var i = 0; i < this.options.items.length; i++) {
            var row = this.options.items[i];
            var text = typeof row == 'object' ? (row.length > 1 ? row[1] : row[0]) : row;
            insertionHtml += '<div class="item" id="__itm' + this.hndl + '_' + i + '"><nobr>' + text + '</nobr></div>';
        }
        list.append(insertionHtml);

        if (list.outerHeight() > this.listHeight) list.css({ height: this.listHeight + 'px' });
        var i = 0;
        var elm = $('#__itm' + this.hndl + '_' + i);
        while (elm.length) {
            elm.bind('mouseover', this.onItemMouseOver.bind(this));
            elm.bind('mouseout', this.onItemMouseOut.bind(this));
            elm.bind('click', this.onItemClick.bind(this));
            i++;
            elm = $('#__itm' + this.hndl + '_' + i);
        }

        list.hide();
		currEditor = null;
    },
    getValue: function() {
        return this.value;
    },
    setValue: function(val) {
        if (this.options.multiSelect) {
            if (this.options.items == null) this.beforeOpenListForFields();
            this.value = val;
            this.input.val('');
            var fields = val.split(',');
            for (var i = 0; i < fields.length; i++) {
                var item = this.options.items.find(function(row) { return row == fields[i] || row[0] == fields[i]; });
                if (item) {
                    var txt = typeof item == 'object' ? (item.length > 1 ? item[1] : item[0]) : item;
                    if (this.input.val().length > 0) txt = ',' + txt;
                    this.input.val(this.input.val()+txt);
                }
            }
        } else {
            if (val == null || this.options.items == null) return;
            var item = this.options.items.find(function(row) { return row == val || row[0] == val; });
            if (item) {
                this.value = val;
                this.setText(typeof item == 'object' ? (item.length > 1 ? item[1] : item[0]) : item);
            }
        }
    },
    setText: function(str) {
        if (this.input.val() != str) {
            this.input.val(str);
            this.options.onChange(this);
        }
    },
    selectedIndex: -1,
    setSelectedIndex: function(index) {
        if (index < 0 || index >= this.options.items.length) return;

        if (this.options.multiSelect) {
            if (index == 0) {
                this.setValue('');
                return;
            } //***

            var val = this.options.items[index];
            val = typeof val == 'object' ? val[0] : val;
            var currVal = this.getValue();
            if (currVal == '')
                currVal = val;
            else if (currVal.indexOf(val) > -1) {
                var fields = currVal.split(',');
                currVal = '';
                for (var i = 0; i < fields.length; i++)
                    if (fields[i] != val)
                        currVal += ',' + fields[i];
                currVal = currVal.substr(1);
            } else
                currVal += ',' + val;
            this.setValue(currVal);
        } else {
            this.selectedIndex = index;
            var val = this.options.items[index];
            val = typeof val == 'object' ? val[0] : val;
            this.setValue(val);
        }
    },
    onItemMouseOver: function(e) {
        var elm = $(e.target).closest('DIV');
        elm.addClass('selItem');
    },
    onItemMouseOut: function(e) {
        var elm = $(e.target).closest('DIV');
        elm.removeClass('selItem');
    },
    onItemClick: function(e) {
        var elm = $(e.target).closest('DIV');
        var list = this.editor;
        var index = list.find('.item').toArray().indexOf(elm[0]);
        this.setSelectedIndex(index);
        list.hide();
		currEditor = null;
    }
};

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
	initialData: null,
    initialize: function(container, controls, entityName, entityId, strFilterExp, hideCategory, renameLabels, showRelatedEntities, defaultValues) {
        container = $(container);
        if (container == null) container = $(document.body);
        if (!hideCategory) hideCategory = '';
        if (!defaultValues) defaultValues = {};

        this.hndl = ++formHandle;
        this.cntrlId = 'cntrl' + this.hndl + '_';
        this.controls = [];
        this.labels = [];
        this.descriptions = [];
        this.entityName = entityName;
        this.entityId = entityId ? entityId : 0;

        controls = controls.sortBy(function(ctrl) { return ctrl.orderNo; });

        var filters = parseFilterExp(strFilterExp);
        var hideFieldValue = {}, ind = -1;
        while (filters['f_' + (++ind)] != undefined)
            if (filters['o_' + ind] == '=')
                hideFieldValue[filters['f_' + ind]] = filters['c_' + ind];

        var ths = this;

        var str = '<table class="editForm" width="99%" cellpadding=0 cellspacing=0 border=0>';
        str += '<tr><td><div><table class="cntrlsTbl" cellpadding="0" cellspacing="0" border="0"><tbody>';
        var categories = controls.collect(function(item) { return item.category; }).uniq().compact();
        for (var k = 0; k < categories.length; k++) {
            var cat = categories[k];
            if (hideCategory.indexOf(cat) == -1)
                str += '<tr class="category"><td colspan="2">' + cat + '</td></tr>';
            for (var i = 0; i < controls.length; i++) {
                var control = controls[i];
                if (control.category != cat || control.type == 'ListForm') continue;
                if (hideCategory.indexOf(cat) > -1) hideFieldValue[control.id] = control.value;
                if (renameLabels && renameLabels[control.id]) { control.label = renameLabels[control.id]; control.description = ''; }
                str += '<tr ' + (hideFieldValue[control.id] !== undefined ? 'style="display:none"' : '') + '>';
                str += '<td onclick="$(this).parent().find(\'input\').focus()">' + (hideFieldValue[control.id] !== undefined ? '' : ('&nbsp;' + control.label)) + '</td>';
                str += '<td id="' + this.cntrlId + i + '"></td>';
                str += '</tr>';
            }
        }
        str += '<tr class="category" id="detailsHeader' + this.hndl + '"><td colspan="2">İlişkili Veriler</td></tr>';
        str += '<tr><td colspan="2" id="details' + this.hndl + '"></td></tr>';
        str += '</tbody></table></div></td></tr>';
        str += '<tr><td style="min-height:50px;padding:7px 0px;"><div id="desc' + this.hndl + '" style="height:50px;background:#F1EFE2;padding:4px;overflow-y: auto;"></div></td></tr>';
        str += '<tr><td style="height:16px;text-align:right"><span class="ccBtn" id="btnSave' + this.hndl + '"><span class="fff disk"></span> ' + lang('Save') + '</span></td></tr>';
        str += '</table>';

        container.append(str);
        var details = $('#details' + this.hndl);
        this.tdDesc = $('#desc' + this.hndl);
        $('#btnSave' + this.hndl).bind('click', this.saveClick.bind(this));

        for (var i = 0; i < controls.length; i++) {
            var control = controls[i];
            var aControl = null;
            if (control.options == null) control.options = new Object();
            control.options.container = $('#'+this.cntrlId + i);
            control.options.relatedEditForm = ths;
            // Eğer bu EditForm bir EditForm'un içindeki detail ListForm'unda "Yeni Ekle" butonuna tıklayarak açılıyorsa... (ilişkili kontrolü gizle)
            if (control.id && hideFieldValue[control.id] !== undefined) {
                control.options.hidden = true;
                control.value = hideFieldValue[control.id];
            }
            if (defaultValues[control.id])
                control.value = defaultValues[control.id];

            switch (control.type) {
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
            case 'TagEdit':
                aControl = new TagEdit(control.id, control.value, control.options);
                break;
            case 'LookUp':
                aControl = new LookUp(control.id, control.value, control.options);
                break;
            case 'ListForm':
                if (this.entityId == 0) continue; //***
                var entityDisplayName = controls.find(function(c) { return c.id == 'Title' || c.id == 'Name' || c.id == 'Question' }).value;
                if (entityDisplayName) entityDisplayName = ' (' + entityDisplayName.split("'").join('').split('"').join('') + ')';
                if (!showRelatedEntities || showRelatedEntities.indexOf(control.entityName) > -1)
                    details.append('<span class="ccBtn" onclick="openEntityListForm(\'' + control.entityName + '\', \'' + control.label + entityDisplayName + '\', \'' + control.relatedFieldName + '=' + this.entityId + '\')"><span class="fff '+getEntityIcon(control.entityName)+'"></span> ' + control.label + '</span>');
                continue;
            default:
                throw 'No control of this kind: ' + control.type;
                break;
            }
            aControl.label = control.label;
            this.controls.push(aControl);
            aControl.description = control.description;

            aControl.input.bind('focus', this.showDesc.bind(this));
            aControl.input.bind('blur', this.clearDesc.bind(this));
        }
        // kontrollerin değerlerini set edelim (mesela combobox'ların değeri set edilirken optionları yükleniyor, use# muhabbeti olanlar var)
        for (var i = 0; i < controls.length; i++) {
            var aControl = this.getControl(controls[i].id);
            if (aControl && aControl.setValue)
                aControl.setValue(controls[i].value);
        }
        // yoksa detail linklerini gizleyelim
        if (details.find('span').length==0) {
            details.hide();
            $('#detailsHeader' + this.hndl).hide();
        }
    
		this.initialData = this.serialize();
	},
    showDesc: function(event) {
        var elm = $(event.target).closest('INPUT');
        if (elm[0].ctrl)
            this.tdDesc.html(elm[0].ctrl.description);
        var td = elm.closest('tr').find('td');
        if (td.length) td.css({ backgroundColor: '#316AC5', color: 'white' });
    },
    clearDesc: function(event) {
        var elm = $(event.target).closest('INPUT');
        var td = elm.closest('tr').find('td');
        if (td.length) td.css({ backgroundColor: '', color: '' });
    },
    serialize: function() {
        var h = new Object();
        for (var i = 0; i < this.controls.length; i++)
            h[this.controls[i].id] = this.controls[i].getValue();
        return h;
    },
    getControl: function(name) {
        for (var i = 0; i < this.controls.length; i++)
            if (this.controls[i].id == name)
                return this.controls[i];
        return null;
    },
    saveClick: function(event) {
        for (var i = 0; i < this.controls.length; i++)
            this.controls[i].value = this.controls[i].getValue();

        var res = '';
        for (var i = 0; i < this.controls.length; i++)
            res += this.controls[i].validate();
        if (res.length == 0)
            this.onSave(this);
        else
            niceAlert(res);
    },
	isModified: function(){
		var data = this.serialize();
		for(var key in data)
			if(data[key]!=this.initialData[key])
				return true;
		return false;
	}
};

function openEditForm(entityName, entityId, title, controls, onSave, filter, hideCategory, renameLabels, showRelatedEntities, defaultValues) {
	var dim = getDimensions($(document.body));
	var left=dim.width-390, top=60, width=350, height=dim.height-60;
	var win = new Window({ className: "alphacube", title: '<span class="fff '+(getEntityIcon(entityName) || getModuleIcon(entityName))+'"></span> ' + title, left: left, top: top, width: width, height: height, wiredDrag: true, destroyOnClose: true }); 
	var winContent = $(win.getContent());
	var pe = new EditForm(winContent, controls, entityName, entityId, filter, hideCategory, renameLabels, showRelatedEntities, defaultValues);
	pe.onSave = onSave;
	win['form'] = pe;
	win.show();
	win.toFront();
	var dimWin = getDimensions(winContent.find('.editForm'));
	win.setSize(350,dimWin.height+5);
    pe.controls[0].input.focus();
}


//##########################
//#        ListForm        #
//##########################

var ListForm = Class.create(); ListForm.prototype = {
    hndl: 0,
    formType: 'ListForm',
    container: null,
    filter: null,
    options: null,
    listGrid: null,
    pageIndex: 0,
    limit: 20,
	hideEditButtons: false,
    selRows: null, //set when getSelectedEntityId is called
    initialize: function(container, options) {
        idCounter++;
        this.hndl = idCounter;

        container = $(container);
        if (container == null) container = $(document.body);
        this.container = container;
        this.options = options;

        this.container.prepend('<table' + (this.options.hideFilterPanel ? ' style="display:none"' : '') + ' class="lf-filter"><tr><td width="5%" style="padding-right:3px;vertical-align:middle">' + lang('Filter') + '</td><td width="83%" id="filter' + this.hndl + '"></td><td width="11%" style="vertical-align:middle"><span id="btnFilter' + this.hndl + '" class="ccBtn" style="margin:0px 0px 0px 10px"><span class="fff zoom"></span> ' + lang('Apply') + '</span></td></table>');
        this.filter = new FilterEdit('id', this.options.extraFilter, { entityName: options.entityName, container: '#filter' + this.hndl, readOnly: true });
        $('#btnFilter' + this.hndl).bind('click', this.fetchData.bind(this));

        if (this.options.hideFilterPanel) {
            this.container.prepend('<div class="lf-search"><input type="text" id="search_' + this.hndl + '"/><span id="btnSearch' + this.hndl + '" class="ccBtn" style="margin:0px 0px 0px 10px"><span class="fff zoom"></span> ' + lang('Search') + '</span></div>');
            $('#btnSearch' + this.hndl).bind('click', this.fetchData.bind(this));
        }

        this.container.append('<div class="lf-dataArea" id="lf-dataArea' + this.hndl + '"></div>');

        var str = '<div class="lf-listFormFooter">';
        if (this.options.commands)
            for (var i = 0; i < this.options.commands.length; i++) {
                var cmd = this.options.commands[i];
                str += '<div style="float:left;margin-top:4px"><span id="btnListFormsCmd' + cmd.id + this.hndl + '" class="ccBtn"><span class="fff '+cmd.icon+'"></span> ' + lang(cmd.name) + '</span></div>';
            }
        str += '<span class="fff resultset_previous" id="btnPrev' + this.hndl + '" title="' + lang('Previous Page') + ' (PgUp)"></span>';
        str += '<span class="cpager" id="pageNo' + this.hndl + '">1</span>';
        str += '<span class="fff resultset_next" id="btnNext' + this.hndl + '" title="' + lang('Next Page') + ' (PgDw)" style="margin-right:50px"></span>';
		if(!options.hideEditButtons){
			str += '<span class="fff add" id="btnAdd' + this.hndl + '" title="' + lang('Add') + ' (Ins)"></span>';
			str += '<span class="fff pencil" id="btnEdit' + this.hndl + '" title="' + lang('Edit') + ' (Ent)"></span>';
			str += '<span class="fff delete" id="btnDelete' + this.hndl + '" title="' + lang('Delete') + ' (Del)"></span>';
		}
        str += '<span class="fff database_refresh" id="btnRefresh' + this.hndl + '" title="' + lang('Refresh') + '"></span>';
        str += '<span class="fff information" id="btnInfo' + this.hndl + '" title="' + lang('Info') + '"></span>';
        str += '</div>';
        this.container.append(str);
        $('#btnPrev' + this.hndl).bind('click', this.cmdPrev.bind(this));
        $('#btnNext' + this.hndl).bind('click', this.cmdNext.bind(this));
        $('#btnAdd' + this.hndl).bind('click', this.cmdAdd.bind(this));
        $('#btnEdit' + this.hndl).bind('click', this.cmdEdit.bind(this));
        $('#btnDelete' + this.hndl).bind('click', this.cmdDelete.bind(this));
        $('#btnRefresh' + this.hndl).bind('click', this.fetchData.bind(this));
        $('#btnInfo' + this.hndl).bind('click', this.cmdInfo.bind(this));
        if (this.options.commands)
            for (var i = 0; i < this.options.commands.length; i++) {
                var cmd = this.options.commands[i];
                $('#btnListFormsCmd' + cmd.id + this.hndl).bind('click', cmd.handler.bind(this));
            }

        this.fetchData();
    },
	addCommand: function(cmd){
		this.container.find('.lf-listFormFooter').prepend(
			'<div style="float:left;margin-top:4px"><span id="btnListFormsCmd' + cmd.id + this.hndl + '" class="ccBtn"><span class="fff '+cmd.icon+'"></span> ' + lang(cmd.name) + '</span></div>'
		);
        $('#btnListFormsCmd' + cmd.id + this.hndl).bind('click', cmd.handler.bind(this));
	},
    fetchData: function() {
        var ths = this;
        var params = {};
        if (ths.filter && ths.filter.filter)
            params = ths.filter.filter.serialize();
        params.search = $('#search_' + ths.hndl).val();

        new Ajax.Request(this.options.ajaxUri + '?method=getGridList&entityName=' + this.options.entityName + (this.options.extraFilter ? '&extraFilter=' + this.options.extraFilter : '') + (this.options.orderBy ? '&orderBy=' + this.options.orderBy : '') + '&page=' + this.pageIndex + '&limit=' + (this.options.limit || this.limit), {
            method: 'post',
            parameters: params,
            onComplete: function(req) {
                if (req.responseText.startsWith('ERR:')) {
                    niceAlert(req.responseText);
                    return;
                }
                var dataArea = $('#lf-dataArea' + ths.hndl);
                dataArea.html(req.responseText);

                if (ths.options['renameLabels']) {
                    var headers = dataArea.find('TH');
                    for (var i = 0; i < headers.length; i++) {
                        var fieldName = $(headers[i]).attr("id").split('_')[1];
                        var key = fieldName.indexOf('.') > -1 ? fieldName.split('.')[1] : fieldName;
                        if (ths.options.renameLabels[key])
                            $(headers[i]).html(ths.options.renameLabels[key]);
                    }
                }

                ths.listGrid = new ListGrid(dataArea.find(':first'), ths.options.selectCallback, ths.sortCallback.bind(ths));

                $('#btnPrev' + ths.hndl).css('opacity',ths.pageIndex <= 0 ? 0.3 : 1.0);
                $('#btnNext' + ths.hndl).css('opacity',ths.listGrid.mayHaveNextPage(ths.limit) ? 1.0 : 0.3);
                $('#pageNo' + ths.hndl).html(ths.pageIndex + 1);
            },
            onException: function(req, ex) { throw ex; }
        });
    },
    sortCallback: function(sortColumnId) {
        var sortColumn = sortColumnId.split('_')[1];
        if (this.options.orderBy && this.options.orderBy.startsWith(sortColumn)) {
            if (this.options.orderBy.endsWith('desc'))
                this.options.orderBy = sortColumn;
            else
                this.options.orderBy = sortColumn + ' desc';
        } else
            this.options.orderBy = sortColumn;
        this.fetchData();
    },
    cmdPrev: function() {
        if (this.pageIndex > 0) {
            this.pageIndex--;
            this.fetchData();
        }
    },
    cmdNext: function() {
        if (this.listGrid.mayHaveNextPage(this.limit)) {
            this.pageIndex++;
            this.fetchData();
        }
    },
    cmdAdd: function() {
        var ths = this;
        new Ajax.Request(this.options.ajaxUri + '?method=new&entityName=' + this.options.entityName, {
            method: 'get',
            onComplete: function(req) {
                if (req.responseText.startsWith('ERR:')) {
                    niceAlert(req.responseText);
                    return;
                }
                var res = null;
                try {
                    res = eval('(' + req.responseText + ')');
                } catch(e) {
                    niceAlert(e.message);
                }
                openEditForm(
                    ths.options.entityName,
                    0,
                    lang('New') + ' ' + ths.options.hrEntityName,
                    res,
                    ths.insertEntity.bind(ths),
                    ths.filter ? ths.filter.getValue() : null, ths.options.editFormHideCategory,
                    ths.options.renameLabels);
            },
            onException: function(req, ex) { throw ex; }
        });
    },
    insertEntity: function(pe) {
        var params = pe.serialize();
        var ths = this;
        new Ajax.Request(this.options.ajaxUri + '?method=insertNew&entityName=' + pe.entityName, {
            method: 'post',
            parameters: params,
            onComplete: function(req) {
                if (req.responseText.startsWith('ERR:')) {
                    niceAlert(req.responseText);
                    return;
                }
                Windows.getFocusedWindow().destroy();
                ths.fetchData();
                //ths.cmdAdd();
            },
            onException: function(req, ex) { throw ex; }
        });
    },
    cmdEdit: function() {
        var id = this.getSelectedEntityId();
        if (!id || id <= 0) return;
        var ths = this;
        new Ajax.Request(this.options.ajaxUri + '?method=edit&entityName=' + ths.options.entityName + '&id=' + id, {
            method: 'get',
            onComplete: function(req) {
                if (req.responseText.startsWith('ERR:')) {
                    niceAlert(req.responseText);
                    return;
                }
                var res = null;
                try {
                    res = eval('(' + req.responseText + ')');
                } catch(e) {
                    niceAlert(e.message);
                }
                openEditForm(
                    ths.options.entityName,
                    id,
                    lang('Edit') + ' ' + ths.options.hrEntityName,
                    res,
                    ths.saveEntity.bind(ths),
                    ths.filter ? ths.filter.getValue() : null, ths.options.editFormHideCategory,
                    ths.options.renameLabels);
            },
            onException: function(req, ex) { throw ex; }
        });
    },
    getSelectedEntityId: function() {
        this.selRows = this.listGrid.getSelectedRows() || [];
        if (this.selRows.length == 0) return 0;
        var id = parseInt(this.selRows[0].id.split('_')[1]);
        return id;
    },
    getSelectedEntities: function() {
        return this.listGrid.getSelectedEntities();
    },
    getSelectedEntity: function() {
        return this.listGrid.getSelectedEntity();
    },
    saveEntity: function(pe) {
        var params = pe.serialize();
        var ths = this;
        new Ajax.Request(this.options.ajaxUri + '?method=save&entityName=' + pe.entityName + '&id=' + pe.entityId, {
            method: 'post',
            parameters: params,
            onComplete: function(req) {
                if (req.responseText.startsWith('ERR:')) {
                    niceAlert(req.responseText);
                    return;
                }
                ths.fetchData();
                Windows.getFocusedWindow().destroy();
            },
            onException: function(req, ex) { throw ex; }
        });
    },
    cmdDelete: function() {
        var ths = this;
        niceConfirm(lang('The record will be deleted!'), function() {
            var id = ths.getSelectedEntityId();
            if (!id || id <= 0) return;
            new Ajax.Request(ths.options.ajaxUri + '?method=delete&entityName=' + ths.options.entityName + '&id=' + id, {
                method: 'get',
                onComplete: function(req) {
                    if (req.responseText.startsWith('ERR:')) {
                        niceAlert(req.responseText);
                        return;
                    }
                    $(ths.selRows[0]).remove();
                },
                onException: function(req, ex) { throw ex; }
            });
        });
    },
    cmdInfo: function() {
        var id = this.getSelectedEntityId();
        if (!id || id <= 0) return;
        niceInfo(ajax({ url: this.options.ajaxUri + '?method=info&entityName=' + name + '&id=' + id, isJSON: false, noCache: true }));
    }
};

//###########################
//#       FilterEditor      #
//###########################
var filterOps = ["like@", "<=@", ">=@", "<>@", "<@", ">@", "=@", "like", "<=", ">=", "<>", "<", ">", "="];
var FilterEditor = Class.create(); FilterEditor.prototype = {
    container: null,
    fields: [],
    fieldsComboItems: [],
    parameterOptions: [['', lang('Select')], ['Category', lang('Category')], ['Hierarchy', lang('Hierarchy')], ['Content', lang('Content')], ['PreviousContent', lang('Previous Content')], ['NextContent', lang('Next Content')], ['Author', lang('Author')], ['Source', lang('Source')], ['Yesterday', lang('Yesterday')], ['LastDay', lang('The day before yesterday')], ['LastWeek', lang('Last week')], ['LastMonth', lang('Last month')]],
    rowHtml: '<tr class="filterRow"><td class="tdField"></td><td class="tdOp"></td><td class="tdControl"></td></tr>',
    initialize: function(container, fields) {
        this.container = $(container);
        if (this.container == null) this.container = $(document.body);

        this.fields = fields.sortBy(function(f) { return __letters.indexOf(f.label.substr(0, 1)); });
        this.fieldsComboItems = [];

        this.container.append('<table class="filterTable">' + this.rowHtml + '</table>');
        var tdField = this.container.find('.tdField').last();
        var ths = this;
        this.fieldsComboItems.push(['none', lang('None')]);
        this.fields.each(function(f) { ths.fieldsComboItems.push([f.id, f.label]) });
        var cb = new ComboBox('f1', null, { items: this.fieldsComboItems, container: tdField });
        cb.options.onChange = this.fieldChanged.bind(this);
    },
    revOpItems: null,
    getRevOpItems: function() {
        if (this.revOpItems == null) {
            var arr = [];
            for (var i = 0; i < filterOps.length; i++)
                arr[filterOps.length - i - 1] = filterOps[i];
            this.revOpItems = arr;
        }
        return this.revOpItems;
    },
    fieldChanged: function(sender) {
        var row = sender.input.closest('.filterRow');
        var rowCount = row.parent().children().length;
        var tdOp = row.find('.tdOp');
        var tdControl = row.find('.tdControl');

        // selectedIndex==0 ise ve birden fazla satır varsa aktif satırı sil.
        if (sender.selectedIndex == 0) {
            if (rowCount > 1) row.remove();
            else {
                tdOp.html('');
                tdControl.html('');
            }
        } else {
            // op yoksa op'u oluştur varsa bişey yapma.
            if (tdOp.html() == '') {
                var cb = new ComboBox('o' + rowCount, null, { items: this.getRevOpItems(), container: tdOp });
                cb.options.onChange = this.opChanged.bind(this);
            }
            // control yoksa da varsa da oluştur.
            tdControl.html('');
            var fieldMetadata = this.fields[sender.selectedIndex - 1];
            var aControl = createControl('c' + rowCount, fieldMetadata, tdControl);
            aControl.label = fieldMetadata.label;
            if (!row.next().length) {
                row.after(this.rowHtml);
                var cb2 = new ComboBox('f' + (rowCount + 1), null, { items: this.fieldsComboItems, container: row.next().find('.tdField') });
                cb2.options.onChange = this.fieldChanged.bind(this);
            }
        }
    },
    opChanged: function(sender) {
        var row = sender.input.closest('.filterRow');
        var cbField = row.find('.tdField INPUT')[0]['ctrl'];
        var tdControl = row.find('.tdControl');
        tdControl.html('');
        if (sender.getValue().indexOf('@') > -1)
            new ComboBox(sender.id.replace('o', 'c'), null, { items: this.parameterOptions, container: tdControl });
        else
            createControl(sender.id.replace('o', 'c'), this.fields[cbField.selectedIndex - 1], tdControl);
    },
    serialize: function() {
        var h = new Object();
        var rows = this.container.find('.filterRow');
        for (var i = 0; i < rows.length - 1; i++) {
			var row = $(rows[i]);
            var inpField = row.find('.tdField INPUT')[0];
            var inpOp = row.find('.tdOp INPUT')[0];
            var inpCtrl = row.find('.tdControl INPUT')[0];
            h['f_' + i] = inpField['ctrl'].getValue();
            h['o_' + i] = inpOp['ctrl'].getValue();
            h['c_' + i] = inpCtrl['ctrl'].getValue();
        }
        return h;
    },
    setFilters: function(filters) {
        var h = parseFilterExp(filters);
        var i = 0;
        var table = this.container.find(':first');
        if (table[0].tagName != 'TBODY') table = table.find(':first');
        var rows = table.find('>');
        var rowCount = rows.length;
        rows.each(function(i, elm) { if (i < rowCount - 1) $(elm).remove(); }); //table.rows.clear(); demek bu.
        while (true) {
            if (!h['f_' + i]) break;
            table.prepend(this.rowHtml);
            var row = $(this.container.find('.filterRow')[i]);
            var tdField = row.find('.tdField');
            var tdOp = row.find('.tdOp');
            var tdControl = row.find('.tdControl');

            // fieldlist combo
            var cb = new ComboBox('f' + i, null, { items: this.fieldsComboItems, container: tdField });
            cb.setValue(h['f_' + i]);
            cb.options.onChange = this.fieldChanged.bind(this);
            // operators combo
            var oCb = new ComboBox('o' + i, null, { items: this.getRevOpItems(), container: tdOp });
            oCb.setValue(h['o_' + i]);
            // control for value
            var fieldMetadata = this.fields.find(function(f) { return f.id == h['f_' + i] });
            fieldMetadata.value = h['c_' + i];
            var aControl = null;
            if (h['o_' + i].indexOf('@') > -1)
                aControl = new ComboBox('c' + i, null, { items: this.parameterOptions, container: tdControl });
            else
                aControl = createControl('c' + i, fieldMetadata, tdControl);
            aControl.setValue(h['c_' + i]);

            i++;
        }
    }
};
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
        case 'TagEdit':
            aControl = new TagEdit(id, fieldMetadata.value, fieldMetadata.options);
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
    initialize: function(table, selectCallback, sortCallback) {
        this.table = $(table);
        if (this.table[0].tagName != 'TABLE') throw 'parameter is not a table!';
        this.selectCallback = selectCallback;
        this.sortCallback = sortCallback;

        var ths = this;

		this.table.find('TH').click(ths.colClick.bind(ths));

        var rows = this.table.find('TR');
        rows.each(function(i, row) {
            row = $(row);
            if (!row[0].id) return;
            row.bind('mouseover', ths.rowOver.bind(ths));
            row.bind('mouseout', ths.rowOut.bind(ths));
            row.bind('click', ths.rowSelect.bind(ths));
        });
        if (rows.length > 1 && rows[1].id)
            this.selectRow(rows[1]);
    },
    rowOver: function(event) {
        var row = $(event.target).closest('TR');
        if (row.hasClass('selected')) return;
        row.addClass('hover');
    },
    rowOut: function(event) {
        var row = $(event.target).closest('TR');
        if (row.hasClass('selected')) return;
        row.removeClass('hover');
    },
    rowSelect: function(event) {
        var row = $(event.target).closest('TR');
        this.selectRow(row);

        if (this.selectCallback)// first td            second td
            this.selectCallback(row.find(':first').html(), row.find(':first').next().html());
    },
    selectRow: function(row) {
		row = $(row);
        var ths = this;
        this.table.find('.selected').each(function(eix,r) { ths.deselectRow($(r)); });
        row.addClass('selected');
        row.removeClass('hover');
    },
    deselectRow: function(row) {
		row = $(row);
        row.removeClass('selected');
    },
    getSelectedRows: function() {
        return this.table.find('.selected');
    },
    getSelectedEntities: function() {
        var selRows = this.getSelectedRows() || [];
        if (selRows.length == 0) return [];
        var res = [];
        var headers = this.table.find('TH');
        for (var i = 0; i < selRows.length; i++) {
			var selRow = $(selRows[i]);
            var r = {};
            for (var k = 0; k < headers.length; k++) {
                var fieldName = $(headers[k]).attr("id").split('_')[1];
                var key = fieldName.indexOf('.') > -1 ? fieldName.split('.')[1] : fieldName;
                if (r[key])
                    r[key + "2"] = $(selRow.find('TD')[k]).attr("value");
                else
                    r[key] = $(selRow.find('TD')[k]).attr("value");
            }
            res.push(r);
        }
        return res;
    },
    getSelectedEntity: function() {
        var selArr = this.getSelectedEntities();
        if (selArr.length > 0)
            return selArr[0];
        return null;
    },
    colClick: function(event) {
        var col = $(event.target).closest('TH');
        if (this.sortCallback) this.sortCallback(col[0].id);
    },
    mayHaveNextPage: function(maxRowCount) {
        return this.table && this.table.find('TR').length > maxRowCount;
    }
};

//############################
//       TreeView (aNode = {data:1, text:'About Us', type:'category|content'})
//############################

var TreeView = Class.create(); TreeView.prototype = {
    container: null,
    rootElement: null,
    getNodesCallback: null,
    nodeClickCallback: null,
    initialize: function(container, rootData, rootText, getNodesCallback, nodeClickCallback) {
        this.container = $(container);
        this.getNodesCallback = getNodesCallback;
        this.nodeClickCallback = nodeClickCallback;

        this.container.append('<div id="nd_' + rootData + '"><span class="fff bullet_toggle_plus"></span><span class="fff folder"></span> <span class="nodeName">' + rootText + '</span></div>');
        var node = $('#nd_' + rootData);
        node[0]['node'] = { data: rootData, text: rootText, type: 'category', collapsed: true };
        this.rootElement = node;
        node.find(':first').bind('click', this.toggle.bind(this));
        node.find('span.nodeName').bind('click', this.nodeClick.bind(this));
    },
    toggle: function(event) {
        var img, div;
        if (!event.target) {
            div = $(event);
            img = div.find(':first');
        } else {
            img = $(event.target);
            div = img.parent();
        }
        var node = div[0]['node'];

        var childrenDiv = $('#nd_' + node.data + '_children');
        if (!childrenDiv.length) {
            div.append('<div class="treeChildren" id="nd_' + node.data + '_children"></div>');
            childrenDiv = $('#nd_' + node.data + '_children');
            var nodes = this.getNodesCallback(node.data) || [];
            if (nodes.length == 0) childrenDiv.css({ width: 0 + 'px', height: 0 + 'px' });
            for (var i = 0; i < nodes.length; i++) {
                var n = nodes[i];
                n.collapsed = true;
                childrenDiv.append('<div id="nd_' + n.data + '">' + (n.type == 'category' ? '<span class="fff bullet_toggle_plus"></span>' : '') + '<span class="fff ' + (n.type=='category'?'folder':'bullet_picture') + '"></span> <span class="nodeName">' + n.text + '</span></div>');
                var nDiv = $('#nd_' + n.data);
                nDiv[0]['node'] = n;
                nDiv.find(':first').bind('click', this.toggle.bind(this));
                nDiv.find('span.nodeName').bind('click', this.nodeClick.bind(this));
            }
        }
        if (node.collapsed) {
            childrenDiv.show();
            node.collapsed = false;
            img.removeClass('bullet_toggle_plus');
            img.addClass('bullet_toggle_minus');
        } else {
            childrenDiv.hide();
            node.collapsed = true;
            img.removeClass('bullet_toggle_minus');
            img.addClass('bullet_toggle_plus');
        }
    },
    nodeClick: function(event) {
        var span = $(event.target);
        this.container.find('.nodeName').each(function(eix,elm) { $(elm).css({ fontWeight: '' }); });
        span.css({ fontWeight: 'bold' });
        var div = span.parent();
        if (this.nodeClickCallback)
            this.nodeClickCallback(div[0]['node']);
    }
};

//#############################
//#     StyleSheetManager     #
//#############################

var StyleSheetManager = Class.create(); StyleSheetManager.prototype = {
    styleSheet: null,
    initialize: function(title) {
        for (var i = 0; i < document.styleSheets.length; i++)
            if (document.styleSheets[i].title == title) {
                this.styleSheet = document.styleSheets[i];
                break;
            }
        if (this.styleSheet != null) {
            this.styleSheet.crossDelete = this.styleSheet.deleteRule ? this.styleSheet.deleteRule : this.styleSheet.removeRule;
            this.styleSheet.crossRules = this.styleSheet.cssRules ? this.styleSheet.cssRules : this.styleSheet.rules;
        }
    },
    getRules: function() {
        return $A(this.styleSheet.crossRules);
    },
    getRule: function(selector) {
        if (!selector || selector.strip().length == 0)
            return null;
        selector = selector.strip().toLowerCase();
        return this.getRules().find(function(rule) { return rule.selectorText && rule.selectorText.toLowerCase() == selector; });
    },
    removeRule: function(selector) {
        var rules = this.getRules();
        for (var i = 0; i < rules.length; i++)
            if (rules[i].selectorText && rules[i].selectorText.toLowerCase() == selector.toLowerCase()) {
                this.styleSheet.crossDelete(i);
                break;
            }
    },
    addRule: function(selector, rule) {
        selector = selector.strip();
        rule = rule.strip();
        if (selector) this.removeRule(selector);
        if (!rule) return;

        if (this.styleSheet.insertRule)
            this.styleSheet.insertRule(selector + ' {' + rule + '}', 0);
        else
            this.styleSheet.addRule(selector, rule);
    },
    applyStyleSheet: function(sheet) {
        var rules = sheet.split('}');
        for (var i = 0; i < rules.length; i++) {
            if (!rules[i] || rules[i].indexOf('{') == -1) continue;
            var rule = rules[i].split('{');
            if (rule[0].strip()) {
                this.addRule(rule[0], rule[1]);
            }
        }
    }
};


//#############################
//#        ContextMenu        #
//#############################

var ContextMenu = Class.create(); ContextMenu.prototype = {
    menuItems: null,
    onShow: function() {
    },
    onHide: function() {
    },
    initialize: function() {
    },
    setup: function() {
        var s = '';
        s += ('<div id="smMenuContainer">');
        s += this.createMenuItems('\t', this.menuItems, 'smMenu', 'menu');
        s += ('</div>');
        $(function () { $('body').append(s); });
    },
    createMenuItems: function(tab, menus, id, subId) {
        var ths = this;
        var s = '';
        s += (tab + '<div id="' + id + '" class="smMenu hideOnOut" style="display:none">\n');
        menus.each(function(menu, index) {
            if (!menu) return;
            menu.parent = menus;
            if (menu.text == '-')
                s += (tab + '\t<hr id="' + (subId + '_' + index) + '"/>\n');
            else if (menu.items && menu.items.length > 0)
                s += (tab + '\t<div class="menuFolder" onmouseover="showSubMenu(this,\'' + (id + '_' + index) + '\')" id="' + (subId + '_' + index) + '"><span class="fff ' + menu.icon + '"></span> ' + menu.text + '</div>\n');
            else
                s += (tab + '\t<div onmousedown="runMenu(this)" onmouseover="hideMenu(this)" id="' + (subId + '_' + index) + '"><span class="fff ' + menu.icon + '"></span> ' + menu.text + '</div>\n');
        });
        s += (tab + '</div>\n');
        menus.each(function(menu, index) {
            if (!menu) return;
            if (menu.items && menu.items.length > 0)
                s += ths.createMenuItems(tab + '\t', menu.items, id + '_' + index, subId + '_' + index);
        });
        return s;
    },
    show: function(x, y) {
        var menu = $('#smMenu');
        var winDim = getWindowSize();
        var scrollPos = getViewportScrollOffsets();
        if (x > scrollPos[0] + winDim.width - menu.width()) x -= menu.width();
        if (y > scrollPos[1] + winDim.height - menu.outerHeight()) y -= menu.outerHeight();
        if (x < 0) x = 0;
        if (y < 0) y = 0;
        menu.css({ left: x + 'px', top: y + 'px' });
        this.onShow();
        $('#smMenuContainer').show();
        menu.show();

        var menus = this.menuItems;
        for (var i = 0; i < menus.length; i++) {
            var fidi = $('#menu_' + i);
            if (!fidi) continue;
            if (!menus[i].isEnabled) {
                fidi.show();
                continue;
            }
            if (menus[i].isEnabled())
                fidi.show();           //TODO: enable/disable yapılacak!
            else
                fidi.hide();
        }
    },
    hideMenu: function(link) {
        link = $(link);
		link.parent().find('.menu_selected').removeClass('menu_selected');
		link.addClass('menu_selected');
        //if(link.className!='menuFolder') return;
        $('#smMenuContainer > *').each(function(eix,elm) {
            var upElmId = link.parent().attr('id');
            if (elm.id != upElmId && elm.id.startsWith(upElmId))
                $(elm).hide();
        });
    },
    showSubMenu: function(link, id) {
        link = $(link);
        this.hideMenu(link);

        var menu = $('#'+id);
        var linkPos = link.offset();
        var winDim = getWindowSize();
        var scrollPos = getViewportScrollOffsets();

        if (linkPos.left + link.outerWidth() + menu.outerWidth() < scrollPos[0] + winDim.width)
            x = linkPos.left + link.outerWidth() - 5;
        else
            x = linkPos.left - menu.outerWidth() + 5;

        if (linkPos.top + menu.outerHeight() < scrollPos[1] + winDim.height)
            y = linkPos.top - 3;
        else
            y = linkPos.top - menu.outerHeight() + link.outerHeight() - 3;

        if (x < 0) x = 0;
        if (y < 0) y = 0;
        menu.css({ left: x + 'px', top: y + 'px' });
        menu.show();

        var theItem = this.findMenuItem(menu.find(':first'));
        if (!theItem) return; //***

        var menus = theItem.parent;
        var fid = 'menu' + id.substr(6);
        for (var i = 0; i < menus.length; i++) {
            var fidi = $('#'+fid + '_' + i);
            if (!fidi) continue;
            if (!menus[i].isEnabled) {
                fidi.show();
                continue;
            }
            if (menus[i].isEnabled())
                fidi.show();           //TODO: enable/disable yapılacak!
            else
                fidi.hide();
        }
    },
    runMenu: function(link) {
       var theItem = this.findMenuItem(link);
        $(link).parent().hide();
        this.onHide();
        $('#smMenuContainer').hide();
        theItem.callback(theItem.data, theItem.text);
    },
    findMenuItem: function(link) {
        link = $(link);
        var ids = link.attr('id').substr(5).split('_');
        var theItems = this.menuItems;
        for (var i = 0; i < ids.length - 1; i++)
            theItems = theItems[ids[i]].items;
        var theItem = theItems[ids[ids.length - 1]];
        return theItem;
    }
};
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

//function wr(s){document.write(s);}

//#############################
//#          Console          #
//#############################

var Console = Class.create(); Console.prototype = {
    txtArea: null,
    //status: null,
    requestUrl: null,
    lastPos: 0,
    cursorPos: -1,
    cmdHist: [],
    cmdHistIndex: 0,
    initialize: function(requestUrl) {
        var win = new Window({ className: 'alphacube', title: '<span class="fff application_xp_terminal" style="vertical-align:middle"></span> ' + lang('Console'), width: 800, height: 400, wiredDrag: true, destroyOnClose: true });
        var container = win.getContent();

        container.append('<textarea id="_cnsl"></textarea>');

        this.txtArea = $('#_cnsl');
        this.txtArea.css({
            backgroundColor: 'black',
            color: 'White',
            fontFamily: 'Lucida Console',
            fontSize: '12px',
            //position:'absolute',
            //left:'10px',
            //top:'30px',
            //right:'10px',
            //bottom:'5px'
            height: '100%',
            width: '100%'
        });
        this.txtArea.bind('keydown', this.onKeyDown.bind(this));

        win.showCenter();
        win.toFront();

        this.txtArea.focus();

        this.requestUrl = requestUrl;
        this.executeCommand('hello');
    },
    onKeyDown: function(event) {
        var handled = false;
        switch (event.keyCode) {
        case Event.KEY_ESC:
// pencerenin kapanmasını engelliyor
        case Event.KEY_END:
        case Event.KEY_HOME:
        case Event.KEY_PAGEUP:
        case Event.KEY_PAGEDOWN:
            event.stopPropagation();
            handled = true;
            break;
        case Event.KEY_RETURN:
            var cmd = this.txtArea.val().substr(this.lastPos);
            this.executeCommand(cmd);
            this.cmdHist.push(cmd);
            this.cmdHistIndex = this.cmdHist.length;
            event.stopPropagation();
            handled = true;
            break;
        case Event.KEY_BACKSPACE:
            if (this.cursorPos <= this.lastPos)
                event.stopPropagation();
            else
                this.cursorPos--;
            handled = true;
            break;
        case Event.KEY_LEFT:
            if (this.cursorPos <= this.lastPos)
                event.stopPropagation();
            else
                this.cursorPos--;
            handled = true;
            break;
        case Event.KEY_RIGHT:
            if (this.cursorPos > this.txtArea.val().length - 1)
                event.stopPropagation();
            else
                this.cursorPos++;
            handled = true;
            break;
        case Event.KEY_UP:
            if (this.cmdHistIndex > 0) {
                this.cmdHistIndex--;
                this.txtArea.val(this.txtArea.val().substr(0, this.lastPos) + this.cmdHist[this.cmdHistIndex]);
                this.cursorPos = this.txtArea.val().length;
            }
            event.stopPropagation();
            handled = true;
            break;
        case Event.KEY_DOWN:
            if (this.cmdHistIndex < this.cmdHist.length) {
                this.cmdHistIndex++;
                this.txtArea.val(this.txtArea.value.substr(0, this.lastPos) + (this.cmdHistIndex == this.cmdHist.length ? '' : this.cmdHist[this.cmdHistIndex]));
                this.cursorPos = this.txtArea.val().length;
            }
            event.stopPropagation();
            handled = true;
            break;
        }

        if (!handled) {
            var c = String.fromCharCode(event.keyCode);
            if ((__letters + ' ').indexOf(c) > -1)
                this.cursorPos++;
        }

        //this.status.innerText = 'Last Pos: ' + this.lastPos + ' | Curr Pos: ' + this.cursorPos + ' | Cmd Hist: ' + this.cmdHistIndex;
    },
    executeCommand: function(cmd) {
        if (cmd == 'exit' || cmd == 'bye') {
            Windows.getFocusedWindow().close();
            return;
        }
        var _url = this.requestUrl + '?cmd=' + cmd;
        this.txtArea.val(this.txtArea.val() + ajax({ url: _url, isJSON: false, noCache: true }));
        this.lastPos = this.txtArea.val().length;
        this.cursorPos = this.lastPos;
        //this.status.innerText = 'Last Pos: ' + this.lastPos + ' | Curr Pos: ' + this.cursorPos + ' | Cmd Hist: ' + this.cmdHistIndex;
        this.txtArea.scrollTop = this.txtArea.scrollHeight;
    }
};

//#############################
//#         AceEditor         #
//#############################

var AceEditor = Class.create(); AceEditor.prototype = {
    initialize: function(options) {
        var ths = this;
        options = Object.extend({
            titleIcon: 'page',
            title: 'Çınar Ace Editor',
            width: 950,
            height: 600,
            buttons: [{ icon: 'accept', type:'default', size:'mini', id: 'btnOk', text: lang('OK'), callback: function() { Windows.getFocusedWindow().close(); } }],
            text: 'Sample AceEditor window',
            lang: 'html',
            wrap: false,
        }, options || {});

        ths.options = options;
        if (ths.options.lang == 'js') ths.options.lang = 'javascript';

        var win = new Window({ className: 'alphacube', title: '<span class="fff ' + options.titleIcon + '"></span> ' + options.title, width: options.width, height: options.height, wiredDrag: true, destroyOnClose: true });
        var winContent = $(win.getContent());
        var html = '<div id="txtSource" style="position:absolute;top:4px;left:4px;right:4px;bottom:60px;border-bottom:1px solid #ccc"></div><div style="position:absolute;left:4px;right:4px;bottom:8px;height:36px;text-align:center">';
        for (var i = 0; i < options.buttons.length; i++) {
            var b = options.buttons[i];
            html += getButtonHtml(b);
        }

        var ek = '';
        html += '<select id="historyx" style="position: absolute;right: 18px;width: 360px;margin-top: 7px;">'+ek+'</select>';
        html += '</div>';
        winContent.append(html);

        $("#historyx").change(function () {
            var r = confirm("Seçilen koda dönmek istiyor musunuz?\n\nNot: Önce yazdıklarınızı kaydediniz ve kodu kaydet demekdikçe eski koda dönseniz bile vazgeçebilirsiniz.");
            if (r == true) {
                $.get('ModuleInfo.ashx?method=getHistoryDetail&id=' + this.value, function (data, status) {
                    ace.edit('txtSource').setValue(data);
                });
            }
        });

        for (var i = 0; i < options.buttons.length; i++) {
            var btn = options.buttons[i];
            $('#'+btn.id).bind('click', function(event) {
                var elm = $(event.target);
                for (var k = 0; k < ths.options.buttons.length; k++)
                    if (ths.options.buttons[k].id == elm.attr('id'))
                        ths.options.buttons[k].callback(ths);
            });
        }

        this.aceEdit = ace.edit('txtSource');
        this.aceEdit.setTheme("ace/theme/eclipse");
        this.aceEdit.getSession().setMode("ace/mode/" + options.lang);
        this.aceEdit.getSession().setUseWrapMode(options.wrap);
        this.aceEdit.setValue(options.text);
        this.aceEdit.renderer.setShowPrintMargin(false);
        this.aceEdit.focus();

        win.showCenter();
        win.toFront();
    },

    getValue: function() {
        return this.aceEdit.getValue();
    },
    setValue: function(v) {
        this.aceEdit.setValue(v);
    },
    insert: function (str) {
        this.aceEdit.insert(str);
    }
};


function getButtonHtml(options) {
    var b = Object.extend({
        icon: 'accept', // see famfamfam icons
        type: 'default', //default, primary, danger, warning, success, info
        size: 'mini', // large, small, mini
        id: 'btnId',
        text: lang('OK')
    }, options || {});
    return '<button class="btn btn-' + (b.type || 'default') + ' btn-' + (b.size || 'mini') + '" id="' + b.id + '" style="margin-left:8px;"><div class="fff ' + b.icon + '"></div>' + b.text + '</button>';
}