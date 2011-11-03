// on load
document.observe('dom:loaded', function(){
	// on body click find visible editors and hide if not the click is within
    Event.observe(document.body,'mousedown', function(event){
        $$('.hideOnOut').each(function(editor){
            if(!Position.within(editor, Event.pointerX(event),Event.pointerY(event))){
                if(editor.id=='smMenu' && editor.visible())
                    popupMenu.onHide();
                editor.hide();
            }
        });
	});
	// listelerde resimlerin google.images'deki gibi pırtlaması için
	$$('.gogPop img').each(function(img){
		img.on('mouseover', gogPop);
	});
	// lightBox olayı
	$$('.lightBox img').each(function(img){
		img.on('click', function(){lightBox(img);});
	});
	// listelerde description alanlarının görünüp gizlenmesi
	$$('.toogleDesc').each(function(elm){
		var elmDesc = elm.down('div.clDesc');
		if(elmDesc){
			elmDesc.hide();
			Event.observe(elm, 'mouseover', function(){elmDesc.show();});
			Event.observe(elm, 'mouseleave', function(){elmDesc.hide();});
		}
    });
	// 
});

// ilk üç parametreden sonrası method parametreleridir
function runModuleMethod(moduleName, moduleId, methodName, params, callback)
{
    new Ajax.Request('RunModuleMethod.ashx?name='+moduleName+'&id='+moduleId+'&methodName='+methodName, {
        method: 'post',
        parameters: params,
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){alert(req.responseText); return;}
            if(callback) callback(moduleId, req, params);
        }
    });
}

// dil olayı
var langRes = {};

function lang(code){
    var str = langRes ? langRes[code] : null;
    if(str==null)
        return '? ' + code;
    return str;
}

// opacity'si düşerekten sayfanın ortasında div gösterme olayı
var showingElementWithOverlay = false;
function showElementWithOverlay(elm, autoHide){
    elm = $(elm);
    elm.hide();
    
    var perde = $('___perde');
    if(!perde){
        new Insertion.Top(document.body, '<div id="___perde" style="display:none;position: absolute;top: 0;left: 0;z-index: 90;width:3000px;height:3000px;background-color: #000;"'+(autoHide?' onclick="hideOverlay()"':'')+'></div>');
        perde = $('___perde');
    }

    //$(document.body).setStyle({overflow:'hidden'});
    new Effect.Appear(perde, { duration: .2, from: 0.0, to: 0.8, afterFinish:function(){ elm.setStyle({position:'absolute', zIndex:100}); elm.show(); } });
	showingElementWithOverlay = elm;
}
function hideOverlay(){
    new Effect.Appear('___perde', { duration: .2, from: 0.8, to: 0.0, afterFinish:function(){ $('___perde').hide(); $(document.body).setStyle({overflow:'auto'});} });
	showingElementWithOverlay.hide();
	showingElementWithOverlay = false;
}

/*
#############################
#    Special for Modules    #
#############################
*/

function navigationPopupInit(id, horizontal){
    document.observe('dom:loaded', function(){
        $$('#'+id+' div').each(function(elm){
            var conId = elm.readAttribute('conId');
            if(!conId || conId<2) return;
            elm.observe('mouseover', function(event){
                var divPopupMenuItems = $(id).down('div.popupMenuItems');
                divPopupMenuItems.hide();
                var div = Event.findElement(event, 'div');
                var pos = div.cumulativeOffset();
                var conId = elm.readAttribute('conId');
                var visibleItemCount = 0;
                $$('#'+id+' div.popupMenuItem').each(function(elm){
                    if(elm.catId==conId){
                        elm.show();
                        visibleItemCount++;
                    }
                    else
                        elm.hide();
                });
                if(horizontal)
                    divPopupMenuItems.setStyle({left:pos.left, top:pos.top+div.getHeight()});
                else
                    divPopupMenuItems.setStyle({left:pos.left+div.getWidth(), top:pos.top});
                if(visibleItemCount)
                    divPopupMenuItems.show();
            });
        });
    });
}

function showTabPage(id, index){
    var i = 0;
    var elm = $('TabPage_'+id+'_'+i);
    while(elm){
        if(elm.id.endsWith(index.toString())) {
            elm.show();
            $('tabButtons'+id).childNodes[index].className = 'tabBtnActive';
        } else {
            elm.hide();
            $('tabButtons'+id).childNodes[i].className = 'tabBtnPassive';
        }
        i++;
        elm = $('TabPage_'+id+'_'+i);
    }
}

function showManset(event, id){
    var elm = Event.element(event);
    if(elm.className=='pic' || elm.tagName=='A')
    {
        var clItem = $(elm).up('.clItem');
        if(!clItem) return;
        
        var sTitle = clItem.down('.clSTitle');
        var title = clItem.down('.clTitle');
        var desc = clItem.down('.clDesc');
        var auth = clItem.down('.clAuthor');
        var date = clItem.down('.clDate');
        var picUrl = clItem.down('.picUrl');
        
        var sTitlePlace = $('Manset_'+id+'_sTitle');
        var titlePlace = $('Manset_'+id+'_title');
        var descPlace = $('Manset_'+id+'_desc');
        var authPlace = $('Manset_'+id+'_auth');
        var datePlace = $('Manset_'+id+'_date');
        var picPlace = $('Manset_'+id+'_pic');
        
        if(sTitlePlace && sTitle) sTitlePlace.innerHTML = sTitle.innerHTML;
        if(titlePlace && title) titlePlace.innerHTML = title.innerHTML;
        if(descPlace && desc) descPlace.innerHTML = desc.innerHTML;
        if(authPlace && auth) authPlace.innerHTML = auth.innerHTML;
        if(datePlace && date) datePlace.innerHTML = date.innerHTML;
        if(picPlace && picUrl) picPlace.src = picUrl.innerHTML;
    }
}

// ContentDisplay için etiket linkleme
function linkTags(contentId){
	var tags = $$('#'+contentId+' div.tags a');
	if(!tags || tags.length==0) return;
	var textElms = $$('#'+contentId+' div.text');
	if(!textElms || textElms.length==0) return;

	var text = textElms[0].innerHTML;
	tags.each(function(a){
		text = text.sub(' '+a.innerHTML, ' <a href="#{href}">#{innerHTML}</a>'.interpolate(a));
	});
	textElms[0].innerHTML = text;
}

// resimlerin images.google'daki gibi pırtlaması için
function gogPop(event){
	var pop = $('gogPopDiv'); if(pop) pop.remove();
	var lightBoxDiv = $('lightBoxDiv'); if(lightBoxDiv) lightBoxDiv.remove();
	
	var img = $(Event.element(event));

	$(document.body).insert('<div id="gogPopDiv" class="hideOnOut"><img src="'+img.src+'"/></div>');
	pop = $('gogPopDiv');
	if(img.up('.lightBox'))
		pop.down('img').on('click', function(){lightBox(img);});
	var pos = Position.cumulativeOffset(img);
	var dim = img.getDimensions();
	var popDim = pop.getDimensions();
	pop.setStyle({left:(pos[0]-(popDim.width-dim.width)/2)+'px', top:(pos[1]-(popDim.height-dim.height)/2)+'px'});
	pop.hide();
	new Effect.Appear(pop, { duration: 0.2, from: 0.0, to: 1.0 });
}
// lghtbox faclty
function lightBox(img){
	var lightBoxDiv = $('lightBoxDiv'); if(lightBoxDiv) lightBoxDiv.remove();
	
	var allImg = img.up('.lightBox').select('img');
	
	$(document.body).insert('<div id="lightBoxDiv"><div style="position:relative"><img src="/external/icons/lbPrev.png" id="lbPrev"><img id="lbImg" src="'+img.readAttribute('path')+'"/><img src="/external/icons/lbNext.png" id="lbNext"><div id="lbCounter">1/20</div><div id="lbTitle"></div><div id="lbDesc"></div><div id="lbLike"><img src="/external/icons/love.png"/> <span>25</span></div></div></div>');
	lightBoxDiv = $('lightBoxDiv');
	$('lbPrev').on('click',function(){img = img.previous('img'); showPic();});
	$('lbNext').on('click',function(){img = img.next('img'); showPic();});
	function showPic(){
		lbImg.src = img.readAttribute('path'); 
		lightBoxDiv.hide();
		$('lbTitle').innerHTML = img.readAttribute('title'); 
		$('lbDesc').innerHTML = img.readAttribute('desc'); 
		$('lbLike').down('span').innerHTML = img.readAttribute('like'); 
	}
	lightBoxDiv.hide();
	var lbImg = $('lbImg');
	lbImg.on('load', function(){
		if(img.previous('img')) $('lbPrev').show(); else $('lbPrev').hide();
		if(img.next('img')) $('lbNext').show(); else $('lbNext').hide();
		$('lbCounter').innerHTML = (allImg.indexOf(img) + 1) + '/' + allImg.length;
		var lbDim = lightBoxDiv.getDimensions();
		var imgDim = img.getDimensions();
		var posView = document.viewport.getScrollOffsets();
		var dimView = Prototype.Browser.IE ? {width:document.body.clientWidth, height:document.body.clientHeight} : document.viewport.getDimensions();
		lightBoxDiv.setStyle({left:(posView[0]+(dimView.width-lbDim.width)/2)+'px', top:(posView[1]+(dimView.height-lbDim.height)/2)+'px'});
		$('lbPrev').setStyle({top:(imgDim.height/2-19)+'px'});
		$('lbNext').setStyle({top:(imgDim.height/2-19)+'px'});
		$('lbTitle').setStyle({width:imgDim.width+'px'});
		$('lbDesc').setStyle({width:imgDim.width+'px'});
		if(!showingElementWithOverlay)
			showElementWithOverlay(lightBoxDiv, true);
		new Effect.Appear(lightBoxDiv, { duration: 0.5, from: 0.0, to: 1.0 });
	});
}

// chart modülü için
var Chart = Class.create({
    
    data: [[10,20,30,40],[40,30,20,10],[100,80,60,40],[80,80,80,80]],
    width: 500,
    height: 400,
    series: ['Spring','Summer','Autumn','Winter'],
    labels: ['Humidity','Pressure','Wind','Temperature'],
    minValue: 0,
    maxValue: 100,
    legendPosition: 'r',
    colors: ['ff9900','6699ff','669933','3333ff','ff66cc','ff3300','000000','666666'],
    bgColor: 'FFFFFF',
    chartBgColor: 'FFFFFF',
    margins: [30,10,10,30,70,30],
    title: 'Weather Stats by Seasons',
    titleColor: '0000FF',
    titleFontSize: 12,
    gridLines: [30,30,4,4],
    
    initialize: function(container, options){
        this.container = container;
        Object.extend(this, options || {});
    },
    show: function(){
        this.container = $(this.container);
        this.container.style.position = 'relative';
        this.container.style.width = this.width + 'px';
        this.container.style.height = this.height + 'px';
        this.container.style.background = this.bgColor;
        var str = '';
        
        this.margins[2] = (this.titleFontSize > this.margins[2]) ? this.titleFontSize : this.margins[2];
        var chartLeft = this.margins[0];
        var chartTop = this.margins[2];
        var chartWidth = this.width - (this.margins[0]+this.margins[1]+this.margins[4]);
        var chartHeight = this.height - (this.margins[2]+this.margins[3]);
        
        var seriesCount = this.data.length;
        var dataCount = this.data[0].length;
        
        
        var groupSpace = 10;
        var barSpace = 2;
        var totalSpaces = barSpace*dataCount*seriesCount + groupSpace*dataCount;
        var barWidth = Math.round((chartWidth - totalSpaces) / seriesCount / dataCount);
        var groupWidth = groupSpace + (barSpace + barWidth)*seriesCount;

        // title
        var t = this.title;
        var tc = this.titleColor;
        var fs = this.titleFontSize;
        str += '<div style="position:absolute;text-align:center;top:#{top}px;left:#{left}px;width:#{width};font-size:#{fs}px;color:#{tc}">#{title}</div>'.interpolate({top:0, left:0, width:'100%', title:t, fs:fs, tc:tc});
        // chart area
        var bg = this.chartBgColor;
        str += '<div style="position:absolute;top:#{top}px;left:#{left}px;width:#{width}px;height:#{height}px;border-left:#{borderLeft};border-bottom:#{borderBottom};background:#{bg}"></div>'.interpolate({top:chartTop, left:chartLeft, width:chartWidth, height:chartHeight, borderLeft:'1px solid red', borderBottom:'1px solid red', bg:bg});
        
        // grids
        var steps = 5;
        for(var top=chartTop, i=0; i<steps; top+=chartHeight/steps, i++){
            var val = Math.round(this.maxValue - i*(this.maxValue/steps));
            str += '<div style="position:absolute;text-align:right;top:#{top}px;left:0px;width:#{width}px">#{val}</div>'.interpolate({top:top, width:chartLeft, val:val});
            str += '<div style="position:absolute;top:#{top}px;left:#{left}px;width:#{width}px;border-top:1px dashed #D0D0D0"></div>'.interpolate({top:top, left:chartLeft, width:chartWidth});
        }
        
        for(var i = 0; i < seriesCount; i++){
            var color = this.colors[i % this.colors.length];
            for(var j = 0; j < dataCount; j++){
                var val = this.data[i][j];
                var barHeight = Math.round(val * chartHeight / 100);
                var barTop = chartTop + (chartHeight - barHeight);
                var barLeft = chartLeft + j*groupWidth + groupSpace + i*(barSpace+barWidth);
                // a bar
                str += '<div style="position:absolute;top:#{top}px;left:#{left}px;width:#{width}px;height:#{height}px;background:##{bg}"></div>'.interpolate({top:barTop, left:barLeft, width:barWidth, height:barHeight, bg:color});
            }
        }
        
        if(this.series && this.series.length>0)
            for(var i = 0; i < this.labels.length; i++){
                var label = this.labels[i];
                str += '<div style="position:absolute;text-align:center;top:#{top}px;left:#{left}px;width:#{width}px">#{label}</div>'.interpolate({top:chartTop+chartHeight, left:i*groupWidth+chartLeft, width:groupWidth, label:label});
            }
        
        if(this.series && this.series.length>0)
            for(var i = 0; i < this.series.length; i++){
                var color = this.colors[i % this.colors.length];
                var serie = this.series[i];
                str += '<div style="position:absolute;top:#{top}px;left:#{left}px;border-left:10px solid ##{color};padding-left:4px">#{serie}</div>'.interpolate({top:chartTop+i*24, left:chartLeft+chartWidth+10, serie:serie, color:color});
            }
        
        this.container.insert(str);
    }
});

// StyleEditor

var StyleEditor = Class.create({
    fields: [
        { id: 'clTitle', name: 'Başlık rengi', color: '0066CC', getElements: function() { return $$('div.sg_baslik a'); }, styleAttribute: 'color' },
        { id: 'clBack', name: 'Zemin rengi', color: 'C3D9FF', getElements: function() { return $$('div.sg_haberler'); }, styleAttribute: 'backgroundColor' },
        { id: 'clBorder', name: 'Çerçeve rengi', color: '0066CC', getElements: function() { return $$('div.sg_haberler'); }, styleAttribute: 'borderColor' },
        { id: 'clText', name: 'Metin rengi', color: '003366', getElements: function() { return $$('div.sg_metin'); }, styleAttribute: 'color' }
    ],
    initialize: function(container) {
        this.container = $(container);
    },
    setColors: function(colors) {
        if (colors) {
            colors.split(',').each((function(color, i) {
                this.fields[i].color = color;
            }).bind(this));
        }
        for(var i=0; i<this.fields.length; i++){
            this.fieldIndex = i;
            this.setColor(this.fields[i].color);
        }
    },
    show: function() {
        var res = '<div class="__styleEditor">';
        res +='Palet: <select id="cmbPalette"><option value="">Seçiniz</option><option value="0066CC,C3D9FF,0066CC,003366">Okyanus</option><option value="2D8930,CAF99B,2D8930,11593C">Amazon</option><option value="A9501B,FFCC66,6F3C1B,660000">Çikolata</option><option value="0066CC,FFFFFF,FFFFFF,333333">Zarif</option></select>';
        res += '<table>';
        this.fields.each(function(elm, i) {
            res += '<tr><td align="right">'+elm.name+'</td><td># <input type="text" id="clTxt' + i + '" name="'+elm.id+'" value="'+elm.color+'" size="6" maxLength="6"/></td><td><input class="clBtn" id="clBtn' + i + '" type="button" value="..." style="background-color:#'+elm.color+'"/></td></tr>';
        });
        res += '</table></div>';
        this.container.insert(res);
        $$('div.__styleEditor input.clBtn').each((function(btn, i) {
            btn.observe('click', this.showPalette.bind(this, btn, i));
        }).bind(this));
        $('cmbPalette').observe('change', (function(){this.setColors($('cmbPalette').options[$('cmbPalette').selectedIndex].value);}).bind(this));
    },
    fieldIndex: 0,
    showPalette: function(btn, i) {
        this.fieldIndex = i;
        var pos = btn.cumulativeOffset();
        var dim = btn.getDimensions();
        var picker = $('__color_picker');
        if (!picker) {
            var res = '<style>table.palette td a div {height:20px;width:20px;margin:2px;border:1px solid #808080}</style>';
            res += '<div style="position: absolute;background:#DFDFDF;border:1px solid #808080;padding:2px;" id="__color_picker"><table cellspacing="0" cellpadding="0" border="0" class="palette"><tbody>';
            [
                ['FFFFCC', 'FFFF66', 'FFCC66', 'F2984C', 'E1771E', 'B47B10', 'A9501B', '6F3C1B', '804000', 'CC0000', '940F04', '660000'],
                ['C3D9FF', '99C9FF', '66B5FF', '3D81EE', '0066CC', '6C82B5', '32527A', '2D6E89', '006699', '215670', '003366', '000033'],
                ['CAF99B', '80FF00', '00FF80', '78B749', '2BA94F', '38B63C', '0D8F63', '2D8930', '1B703A', '11593C', '063E3F', '002E3F'],
                ['FFBBE8', 'E895CC', 'FF6FCF', 'C94093', '9D1961', '800040', '800080', '72179D', '6728B2', '6131BD', '341473', '400058'],
                ['FFFFFF', 'E6E6E6', 'CCCCCC', 'B3B3B3', '999999', '808080', '7F7F7F', '666666', '4C4C4C', '333333', '191919', '000000']
            ].each(function(arr) {
                res += '<tr>';
                arr.each(function(color) {
                    res += '<td><a color="' + color + '" href="#"><div style="background-color:#' + color + ';"> </div></a></td>';
                });
                res += '</tr>';
            });
            res += '</tbody></table></div>';
            this.container.insert(res);
            picker = $('__color_picker');
            $$('#__color_picker a').each((function(a) {
                a.observe('click', this.setColor.bind(this, a.readAttribute('color')));
            }).bind(this));
        }
        picker.setStyle({ top: pos.top, left: (pos.left + dim.width) });
        picker.show();
    },
    setColor: function(color) {
        var i = this.fieldIndex;
        var f = this.fields[i];
        f.color = color;
        f.getElements().each(function(elm) {
            var attrib = f.styleAttribute;
            elm.style[attrib] = '#' + color;
            $('clBtn' + i).style.background = '#' + color;
            $('clTxt' + i).value = color;
        });
        var picker = $('__color_picker');
        if(picker) picker.hide();
    }
});


/*
###################################
#            Utility              #
###################################
*/

function isEmpty(obj){
        return obj==null || obj==undefined || obj=='';
}
var smAjaxCache = {};
function ajax(options){
    // cache..
    if(!options.noCache && smAjaxCache[options.url])
        return smAjaxCache[options.url];

    var res = null;
    new Ajax.Request(options.url, {
        method: 'get',
        asynchronous: false,
        onComplete: function(req) {
            if(req.responseText.startsWith('ERR:')){alert(req.responseText); return;}
            res = options.isJSON ? eval('('+req.responseText+')') : req.responseText;
            if(!options.noCache) smAjaxCache[options.url] = res;
        },
        onException: function(req, ex){throw ex;}
    });
    return res;
}

/*
##########################################
#            EXTENSIONS                  #
##########################################
*/
Object.extend(String.prototype, {
    startsWith: function(str){
        return this.substr(0, str.length) == str;
    },
    endsWith: function(str){
        return this.substr(this.length-str.length) == str;
    },
    htmlEncode: function(){
        var enc = escape(this);
        enc = enc.replace(/\//g,"%2F");
        enc = enc.replace(/\?/g,"%3F");
        enc = enc.replace(/=/g,"%3D");
        enc = enc.replace(/&/g,"%26");
        enc = enc.replace(/@/g,"%40");
        return enc;
    }
});
document.getWidth = function(){ return $(document.body).getDimensions().width; }
document.getHeight = function(){ return $(document.body).getDimensions().height; }

// for scrolling elemnts in overflowed elements, thanks to robmadole, http://dev.rubyonrails.org/ticket/8208
Scroll = Class.create(); 
Scroll = { 
    to: function(element) { 
        element = $(element); 

		var elementOverflow = element; 
        var valueT = 0, valueL = 0; 
  
        do 
        { 
            if (!elementOverflow.parentNode) { break; } 
  
            valueT += elementOverflow.cumulativeOffset().top  || 0; 
            valueL += elementOverflow.cumulativeOffset().left || 0; 
  
            if (elementOverflow.parentNode.getHeight() < elementOverflow.parentNode.scrollHeight) 
            { 
                elementOverflow.parentNode.scrollTop  = valueT; 
                elementOverflow.parentNode.scrollLeft = valueL; 
  
                Element.scrollTo(elementOverflow); 
                return; 
            } 
  
             elementOverflow = elementOverflow.parentNode; 
        } while (true); 
  
         var pos = Position.cumulativeOffset(element); 
         window.scrollTo(pos[0], pos[1]); 
         return element; 
    } 
};

////////////////////////////
//
//   cookie get/set/delete
//
////////////////////////////

// expires as days
function setCookie(name, value, expires, path){
	var today = new Date();
	today.setTime(today.getTime());

	if(expires)
		expires = expires * 1000 * 60 * 60 * 24;
	var expires_date = new Date( today.getTime() + (expires) );

	document.cookie = name + "=" +escape(value) + (expires ? ";expires=" + expires_date.toGMTString() : "") + (path ? ";path=" + path : "");
}
function getCookie(name){
	var start = document.cookie.indexOf(name + "=");
	var len = start + name.length + 1;
	if(!start && name != document.cookie.substring(0, name.length))
		return null;

	if (start == -1) return null;
	
	var end = document.cookie.indexOf( ";", len );
	if(end == -1) end = document.cookie.length;
	
	return unescape(document.cookie.substring(len, end));
}
function delCookie(name, path, domain){
	if(getCookie(name))
		document.cookie = name + "=" + (path ? ";path=" + path : "") + (domain ? ";domain=" + domain : "") + ";expires=Thu, 01-Jan-1970 00:00:01 GMT";
}


/*
# Insert a tab at the current text position in a textarea
# Jan Dittmer, jdittmer@ppp0.net, 2005-05-28
# Inspired by http://www.forum4designers.com/archive22-2004-9-127735.html
*/
function insertTab(event,obj) {
    var tabKeyCode = 9;
    if (event.which) // mozilla
        var keycode = event.which;
    else // ie
        var keycode = event.keyCode;
    if (keycode == tabKeyCode) {
        if (event.type == "keydown") {
            if (obj.setSelectionRange) {
                // mozilla
                var s = obj.selectionStart;
                var e = obj.selectionEnd;
                obj.value = obj.value.substring(0, s) + 
                    "\t" + obj.value.substr(e);
                obj.setSelectionRange(s + 1, s + 1);
                obj.focus();
            } else if (obj.createTextRange) {
                // ie
                document.selection.createRange().text="\t"
                obj.onblur = function() { this.focus(); this.onblur = null; };
            } else {
                // unsupported browsers
            }
        }
        if (event.returnValue) // ie ?
            event.returnValue = false;
        if (event.preventDefault) // dom
            event.preventDefault();
        return false; // should work in all browsers
    }
    return true;
}


// script import (as PHP does)
// only imports the scripts in the same folder with prototype.js.

function $import(path)
{
    var i, base, src = "prototype.js", scripts = document.getElementsByTagName("script");
    for (i=0; i<scripts.length; i++){
        if (scripts[i].src.match(src)){
            base = scripts[i].src.replace(src, "");
            break;
        }
    }
    document.write("<" + "script src=\"" + base + path + "\"></" + "script>");
} 

// get selection
function getSelText()
{
	var txt = '';
	if (window.getSelection)
		txt = window.getSelection();
	else if (document.getSelection)
		txt = document.getSelection();
	else if (document.selection)
		txt = document.selection.createRange().text;
	return txt;
}
document.observe('keyup', function(e){
    if(e.keyCode==69){
	    var txt = getSelText().toString();
	    if(txt.length>0) {
            var params = location.href.toQueryParams();
            if(params && params.item){
	            var resp = ajax({url:'EntityInfo.ashx?method=addTag&item='+params.item+'&tag='+txt,isJSON:false,noCache:true});
	            alert(resp);
	        }
	    }
	}
});
