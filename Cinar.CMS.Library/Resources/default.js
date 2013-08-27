// on load
$(function(){
	// on body click find visible editors and hide if not the click is within
    $(document.body).on('mousedown', function(event){
        $('.hideOnOut').each(function(eix,editor){
            if(!Position.within(editor, event.pageX,event.pageY)){
                if(editor.id=='smMenu' && $(editor).is(':visible'))
                    popupMenu.onHide();
				if($(event.target).hasClass('hideOnOutException') || $(event.target).closest('.hideOnOutException'))
					Prototype.K();
				else
					$(editor).hide();
            }
        });
        $('.removeOnOut').each(function(eix,editor){
            if(!Position.within(editor, event.pageX,event.pageY))
                $(editor).remove();
        });
	});
	// listelerde resimlerin google.images'deki gibi pırtlaması için
	$('.gogPop img').each(function(eix,img){
		$(img).on('mouseover', gogPop);
	});
	// listelerde description alanlarının görünüp gizlenmesi
	var effectInExecution=null;
	$('.toogleDesc .clItem').each(function(eix,elm){
		var elmDesc = $(elm).find('div.clDesc');
		if(elmDesc){
			elmDesc.hide();
			$(elm).on('mouseenter', function(event){
				event.stop();
				new Effect.Parallel([new Effect.BlindDown(elmDesc), new Effect.Morph(elm, {style:'background:#C1C3C2; border-color:#838383;'})], {duration:0.3, afterFinish: function(){$(elm).css({borderColor:'#838383'});}});
			});
			$(elm).on('mouseleave', function(event){
				event.stop();
				new Effect.Parallel([new Effect.BlindUp(elmDesc), new Effect.Morph(elm, {style:'background:#E1E3E2; border-color:#E1E3E2;', afterFinish: function(){elmDesc.css({height:'auto'});}})], {duration:0.3});
			});
		}
    });
	// listelerde description alanlarının resim üzerinde görünüp gizlenmesi
	$('.showDescOnImg img').each(function(eix,elm){
		var elmImg = $(elm);
		var elmDesc = $(elm).next('div.clDesc');
		if($(elm).closest('.lightBox')) elmDesc.on('click', function(){lightBox(elmImg);});
		if(elmDesc.length){
			elmDesc.hide();
			elmImg.on('mouseenter', function(event){
				var dimImg = getDimensions(elmImg);
				elmDesc.css({left:elmImg.css('left'), top:elmImg.css('top'), width:dimImg.width+'px', height:dimImg.height+'px', margin:'0px 10px 10px 0px'});
				elmDesc.show();
			});
			elmDesc.on('mouseleave', function(event){
				elmDesc.hide();
			});
		}
    });
	// masonry
	$(window).on('load', function(){
		$('.masonry').each(function(eix,elm){
			var sorted = $(elm).find('img');
			var cols = [[], [], []];
			for(var i = 0; i<cols.length; i++)
				for(var j = i; j<sorted.length; j+=cols.length)
					cols[i].push($(sorted[j]));
			var maxHeight = 0;
			for(var i = 0; i<cols.length; i++){
				var left = i * (cols[0][0].outerWidth() + 10);
				var top = 0;
				for(var j = 0; j<cols[i].length; j++){
					cols[i][j].css({left:left+'px', top:top+'px'});
					top += cols[i][j].outerHeight() + 10;
				}
				if(top>maxHeight) maxHeight = top;
			}
			$(elm).css({height:maxHeight+'px'});
		});
	});
	// fadeShow (yani fadeIn'li slide show)
	if($('.fadeShow').length)
		$('.fadeShow').each(function(eix,elm){
			if($(elm).find('.clItem').length>0){
				$(elm).append('<div class="indexElms"></div>');
				$(elm).timeout = setTimeout(function(){fadeShowShowImg($(elm));}, 4000);
			}
		});
	$('.fadeShow .clItem').each(function(i,elm){
		if($(elm).closest('.fadeShow').find('.clItem').length<=1)
			return;
	
		if(i==0){
			$(elm).parent().currentImg = elm;
			$(elm).css({zIndex:2});
		}
		else
			$(elm).fade({ duration: 0.05, from: 1, to: 0.01 });
		$(elm).closest('.fadeShow').find('.indexElms').first().append('<img src="/external/icons/bullet_'+(i==0 ? 'gray':'white')+'.png" index="'+i+'"/>');
		var indexElm = $(elm).closest('.fadeShow').find('.indexElms').first().find('*').last();
		indexElm.on('click', function(event){
			fadeShowShowImg($(elm).closest('.fadeShow'), indexElm);
		});
	});
	// fadeWithArrows
	if($('.fadeWithArrows').length){
		$('.fadeWithArrows').each(function(eix,elm){
			$(elm).append('<img src="/external/icons/lbPrev.png" id="lbPrev" style="display:none"/><img src="/external/icons/lbNext.png" id="lbNext" style="display:none"/>');
			$(elm).find('#lbPrev').on('click', function(){fadeWithArrowsShow(elm, 'prev');});
			$(elm).find('#lbNext').on('click', function(){fadeWithArrowsShow(elm, 'next');});
			if($(elm).find('.clItem').length > 1){
				$(elm).find('#lbPrev').show();
				$(elm).find('#lbNext').show();
				setInterval(function(){fadeWithArrowsShow(elm, 'next');}, 5000);
			}
		});
	}
	$('.fadeWithArrows .clItem').each(function(i,elm){
		if(i==0){
			$(elm).closest('.fadeWithArrows').currentImg = elm;
			$(elm).css({zIndex:2});
		} else {
			$(elm).fade({ duration: 0.05, from: 1, to: 0.01 });
		}
	});
	// slideShow
	if($('.slideShow').length)
		$('.slideShow').each(function(eix,elm){
			if($(elm).find('img').length==0){
				$(elm).remove();
				return;
			}
			elm.playing = $(elm).hasClass('autoPlay');
			if(elm.playing) elm.timeout = setTimeout(function(){slideShowSlide(elm);}, 5000);
			elm.alreadySliding = false;
			elm.mouseIsOver = false;
			$(elm).html('<div class="paging" style="background-image:url(/external/icons/slide_prev.png); background-position:right center;"></div>'+
							'<div class="clipper">'+
							'<div class="innerDiv">'+ 
							$(elm).html() + 
							'</div>'+
							'</div>'+
							'<div class="paging"  style="background-image:url(/external/icons/slide_next.png); background-position:left center;"></div>'+
							'<div style="clear:both"></div>'+
							'<img src="/external/icons/play.png" class="playBtn"/>');
			
			$(elm).find('.playBtn').on('click', function(){
				elm.playing = !elm.playing;
				if(elm.playing){
					$(elm).find('.playBtn').src = '/external/icons/pause.png';
					slideShowSlide(elm);
				} else {
					clearTimeout(elm.timeout);
					$(elm).find('.playBtn').attr('src', '/external/icons/play.png');
				}
			});
			
			$(elm).on('mouseenter', function(){
				elm.mouseIsOver = true;
				clearTimeout(elm.timeout);
			});
			$(elm).on('mouseleave', function(){
				elm.mouseIsOver = false; 
				if(elm.playing)
					elm.timeout = setTimeout(function(){slideShowSlide(elm);}, 5000);
			});

			var imgs = $(elm).find('img');
			var imgCounter = 0;
			imgs.each(function(eix,img){
				$(img).on('load', function(){
					imgCounter++;
					if(imgCounter==imgs.length){
						var totalWidth = 0;
						imgs.each(function(eix,i){totalWidth += $(i).outerWidth()+20;});
						$(elm).find('.innerDiv').css({width:(totalWidth-10)+'px'});
					}
				});
			});
			$(elm).find('.paging').first().on('click', function(){slideShowSlide(elm,'back');});
			$(elm).find('.paging').first().next().on('click', function(){slideShowSlide(elm);});
		});
	// lightBox olayı
	if($('.lightBox').length){
		document.on('keydown', function(event){
			var lightBoxDiv = $('#lightBoxDiv');
			if(!lightBoxDiv) return;
            switch(event.keyCode){
				case Event.KEY_LEFT:
					lightBoxDiv.find('#lbPrev').simulate('click');
					break;
				case Event.KEY_RIGHT:
					lightBoxDiv.find('#lbNext').simulate('click');
					break;
				case Event.KEY_ESC:
					lightBoxDiv.hide(); hideOverlay();
					break;
			}
		});
		$('.lightBox').each(function(eix,elm){
			var imgs = $(elm).find('img');
			if(imgs.length){
				imgs.each(function(eix,img){
					$(img).on('click', function(){lightBox(img);});
				});
				if($(elm).hasClass('fbLike')){
					var mostLiked = imgs.sortBy(function(img){return parseInt($(img).attr('like'));})[imgs.length-1];
					mostLiked.mostLiked=true;
					if(parseInt(mostLiked.attr('like'))>0){
						var f = function(){
							var pos = mostLiked.viewportOffset();
							$(document.body).append('<img id="mostLikedIcon" src="/external/icons/love.png" style="position:absolute;left:'+(pos.left+$(mostLiked).outerWidth()-22)+'px;top:'+(pos.top+$(mostLiked).outerHeight()-18)+'px;"/>');
						};
						if(mostLiked.readyState && mostLiked.readyState=='complete') // IE8 fix
							f();
						else
							mostLiked.on('load', f);
					}
				}
			}
		});
	}
	// mansetAktuel
	$('.mansetAktuel').each(function(eix,manset){
		$(manset).append('<div class="links" style="float:right"></div><div style="clear:both"></div>');
		var links = $(manset).find('.links');
		var currItem = null;
		$(manset).find('.clItem').each(function(i,elm){
			var src = $(elm).find('img').attr('src');
			$(elm).find('img').first().parent().remove();
			$(elm).css({background:'url('+src.replace("'","\\'")+') no-repeat left top'});
			$(elm).append('<div class="summary"></div>');
			
			if($(elm).find('div.clSTitle') && !$(elm).find('div.clSTitle').html()) 
				$(elm).find('div.clSTitle').html($(elm).find('div.clTitle a').html());


			var summary = $(elm).find('.summary');
			if($(elm).find('div.clCategory').length) summary.append($(elm).find('div.clCategory').remove());
			if($(elm).find('div.clPubDate').length) summary.append($(elm).find('div.clPubDate').remove());
			if($(elm).find('div.clSTitle').length) summary.append($(elm).find('div.clSTitle').remove());
			if($(elm).find('div.clDesc').length) summary.append($(elm).find('div.clDesc').remove());
			
			var elmTitle = $(elm).find('div.clTitle');
			links.append(elmTitle.remove());
			elmTitle.on('mouseover', function(){currItem.hide(); $(elm).show(); currItem = elm;});

			if(i>0)
				$(elm).hide();
			else
				currItem = elm;
		});
	});
	// slideAll
	$('.slideAll').each(function(eix,slideAll){
		var dim = getDimensions(slideAll);
		$(slideAll).css({position:'relative'});
		$(slideAll).find('.clTitle').each(function(eix,elm){$(elm).hide();});
		$(slideAll).find('.clDesc').each(function(eix,elm){$(elm).hide();});
		var items = $(slideAll).find('.clItem');
		
		var slideAllIntFunc = function(){
			slideAllCurrIndex = ++slideAllCurrIndex % items.length; 
			slideAllShow(items,slideAllCurrIndex,dim);
			setTimeout(function(){slideAllCollapse(items, dim);}, 4000);
		}
		var slideAllTimer = setInterval(slideAllIntFunc, 5000);

		items.each(function(index,elm){
			$(elm).css({position:'absolute', left:(dim.width/items.length*index)+'px'});
			$(elm).on('mouseenter', function(){clearInterval(slideAllTimer); slideAllShow(items, index, dim);});
		});
		slideAll.on('mouseleave', function(){slideAllCollapse(items, dim); slideAllTimer = setInterval(slideAllIntFunc, 5000);});
	});
	// image editor
	if($('.cc_edit_img').length){
		imageEditorInit();
		$('.cc_edit_img').each(makeImageEditable);
	}
});

var slideAllCurrIndex = -1;

function slideAllShow(items, index, dim){
	var elm = items[index];
	var w = elm.getWidth();
	var space = (dim.width-w)/(items.length-1);
	for(var i=0; i<items.length; i++){
		var l = i<=index ? i * space : ((i-1)*space+w);
		new Effect.Morph(items[i], {style:'left:'+l+'px', duration: 0.4});
		items[i].find('.clTitle').hide();
		items[i].find('.clDesc').hide();
	}
	elm.find('.clTitle').show();
	elm.find('.clDesc').show();
}

function slideAllCollapse(items, dim){
	for(var i=0; i<items.length; i++){
		var l = dim.width/items.length*i;
		new Effect.Morph(items[i], {style:'left:'+l+'px', duration: 0.4});
		items[i].find('.clTitle').hide();
		items[i].find('.clDesc').hide();
	}
}

var cc_edit_curr = null;
function imageEditorInit(){
	if($('#cc_ei_btn').length==0)
		$(document.body).append('<button id="cc_ei_btn" style="display:none">Edit</button>');
	else
		return;
		
	$('#cc_ei_btn').on('click', editCurrImage);
}
function editCurrImage(path){
	if(!path){
		path = cc_edit_curr.attr('path') || cc_edit_curr.attr('src');
		if(!path || path.startsWith('/_thumbs')){
			niceAlert(lang('Editable image path not defined'));
			return;
		}
	}
	
	var win = new Window({ className: 'alphacube', title: '<span class="cbtn cedit"></span> ' + lang('Edit Picture'), resizable: false, maximizable: false, minimizable: false, width: 800, height: 600, wiredDrag: true, destroyOnClose: true }); 
	var str = '<div class="cc_ei_toolbar"><span id="cc_select" class="ccBtn cselect">Select</span><span id="cc_crop" class="ccBtn ccrop">Crop</span><span id="cc_turncw" class="ccBtn cturncw">Turn CW</span><span id="cc_turnccw" class="ccBtn cturnccw">Turn CCW</span><input id="cc_ei_width"/> x <input id="cc_ei_height"/><span id="cc_resize" class="ccBtn cresize">Resize</span><span id="cc_reset" class="ccBtn creset">Reset</span></div>';
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
	
	new Draggable('cc_ei_selection', {onDrag:function(){
		var dim = getDimensions(sel);
		var pos = sel.position();
		$('#cc_ei_nw').css({left:(pos.left-5)+'px', top:(pos.top-5)+'px'});
		$('#cc_ei_se').css({left:(pos.left+dim.width-5)+'px', top:(pos.top+dim.height-5)+'px'});
		$('#cc_ei_status').html('<b>File:</b> ' + path + ' &nbsp; <b>Selection:</b> ' + pos.left + ', ' + pos.top + ', ' + dim.width + ', ' + dim.height);
	}});
	new Draggable('cc_ei_nw', {onDrag:resizeSelection});
	new Draggable('cc_ei_se', {onDrag:resizeSelection});
	
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
		if(!sel.visible()){
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
			if(sel.visible()) toggleSelect();
			imgPreview.src = path + '?' + new Date().getMilliseconds();
		}
	});
	$('#cc_turncw').on('click', function(){
		var res = ajax({url:'EditImageRotate.ashx?dir=CW&path='+path, isJSON:false, noCache:true});
		if(res.startsWith('ERR:')){
			niceAlert(res);
			return;
		}
		else {
			if(sel.visible()) toggleSelect();
			imgPreview.src = path + '?' + new Date().getMilliseconds();
		}
	});
	$('#cc_turnccw').on('click', function(){
		var res = ajax({url:'EditImageRotate.ashx?dir=CCW&path='+path, isJSON:false, noCache:true});
		if(res.startsWith('ERR:')){
			niceAlert(res);
			return;
		}
		else {
			if(sel.visible()) toggleSelect();
			imgPreview.src = path + '?' + new Date().getMilliseconds();
		}
	});
	$('#cc_resize').on('click', function(){
		var res = ajax({url:'EditImageResize.ashx?width='+$('#cc_ei_width').val()+'&height='+$('#cc_ei_height').val()+'&path='+path, isJSON:false, noCache:true});
		if(res.startsWith('ERR:')){
			niceAlert(res);
			return;
		}
		else {
			if(sel.visible()) toggleSelect();
			imgPreview.src = path + '?' + new Date().getMilliseconds();
		}
	});
	$('#cc_reset').on('click', function(){
		var res = ajax({url:'EditImageReset.ashx?path='+path, isJSON:false, noCache:true});
		if(res.startsWith('ERR:')){
			niceAlert(res);
			return;
		}
		else {
			if(sel.visible()) toggleSelect();
			imgPreview.src = path + '?' + new Date().getMilliseconds();
		}
	});
}

function makeImageEditable(eix,img){
	$(img).on('mouseenter', function(){
		cc_edit_curr = img;
		var dim = getDimensions(img);
		var pos = $(img).offset();
		var btn = $('#cc_ei_btn');
		btn.css({left:(pos.left + dim.width-40)+'px', top:(pos.top + dim.height-20)+'px'});
		btn.show();
	});
}

function slideShowSlide(elm, dir){
	if(elm.alreadySliding) return;
	elm.alreadySliding = true;
	
    var dimCl = getDimensions(elm.find('.clipper'));
	var leftID = parseInt(elm.find('.innerDiv').css('left'));
    var dimID = getDimensions(elm.find('.innerDiv'));

	if(leftID + dimID.width < dimCl.width+60)
		new Effect.Move(elm.find('.innerDiv'), { x: -1*leftID, y: 0, mode: 'relative', duration:1.0, afterFinish:function(){elm.alreadySliding = false;} });
	else if(dir == 'back'){
		if(leftID < 0)
			new Effect.Move(elm.find('.innerDiv'), { x: elm.find('.clipper').getWidth()+20, y: 0, mode: 'relative', duration:1.0, afterFinish:function(){elm.alreadySliding = false;} });
		else
			elm.alreadySliding = false;
	} else
		new Effect.Move(elm.find('.innerDiv'), { x: -1*(elm.find('.clipper').getWidth()+20), y: 0, mode: 'relative', duration:1.0, afterFinish:function(){elm.alreadySliding = false;} });
		
	clearTimeout(elm.timeout);
	if(elm.playing && !elm.mouseIsOver){
		elm.timeout = setTimeout(function(){slideShowSlide(elm);}, 5000);
	}
}
function fadeShowShowImg(fadeShow, indexElm){
	var currIndexElm = null;
	if(!indexElm){
		indexElm = fadeShow.find('.indexElms');
		if(!indexElm) return;
		currIndexElm = indexElm.find('*')[fadeShow.find('.clItem').indexOf(fadeShow.currentImg)];
		indexElm = currIndexElm.next() ? currIndexElm.next() : fadeShow.find('.indexElms img');
	}
	else
		currIndexElm = fadeShow.find('.indexElms').find('*')[fadeShow.find('.clItem').indexOf(fadeShow.currentImg)];
		
	indexElm.src = '/external/icons/bullet_gray.png';
	if(currIndexElm)
		currIndexElm.src = '/external/icons/bullet_white.png';
	
	clearTimeout(fadeShow.timeout);
	var i = parseInt(indexElm.attr('index'));
	
	fadeShow.currentImg.fade({ duration: 0.5, from: 1, to: 0.01 });
	fadeShow.currentImg.css({zIndex:1});
	
	fadeShow.currentImg = fadeShow.find('.clItem')[i];
	fadeShow.currentImg.fade({ duration: 0.5, from: 0, to: 1 });
	fadeShow.currentImg.css({zIndex:2});

	fadeShow.timeout = setTimeout(function(){fadeShowShowImg(fadeShow);}, 4000);
}
function fadeWithArrowsShow(elm, which){
	var nextImg = which=='next' ? elm.currentImg.next('.clItem') : elm.currentImg.previous('.clItem');
	if(!nextImg)
		nextImg = which=='next' ? elm.find('.clItem') : elm.find('.clItem').last();
	if(nextImg && nextImg!=elm.currentImg){
		var curr = elm.currentImg;
		curr.fade({ duration: 0.5, from: 1, to: 0.01, afterFinish: function(){curr.css({ zIndex:1 });} });
		nextImg.fade({ duration: 0.5, from: 0, to: 1, afterFinish: function(){nextImg.css({ zIndex:2 });} });
		elm.currentImg = nextImg;
	}
}

// params: {}
function runModuleMethod(moduleName, moduleId, methodName, params, callback){
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
        return (designMode?'? ':'') + code;
    return str;
}

// opacity'si düşerekten sayfanın ortasında div gösterme olayı
var showingElementWithOverlay = false;
var showingElementWithOverlayZIndex = 0;
function showElementWithOverlay(elm, autoHide, color){
    elm = $(elm);
	showingElementWithOverlayZIndex = elm.css('z-index');
    elm.hide();
	
	var dim = Position.getWindowSize();
	var bodyDim = getDimensions($('html')[0]);
	if(dim.height<bodyDim.height)
		dim = bodyDim;
	if(Prototype.Browser.IE) {dim.height = $('body')[0].scrollHeight; dim.width -= 20;}
    
    var perde = $('#___perde');
    if(perde.length==0){
        $(document.body).prepend('<div id="___perde" style="display:none;position: absolute;top: 0;left: 0;z-index: 90000;width:'+dim.width+'px;height:'+dim.height+'px;background-color: '+(color ? color : '#000')+';"'+(autoHide?' onclick="hideOverlay()"':'')+'></div>');
        perde = $('#___perde');
		perde.on('mouseover', function(){
			$('.hideOnPerde').each(function(eix,hop){
				$(hop).hide();
			});
		});
    }

    new Effect.Appear(perde, { duration: .2, from: 0.0, to: 0.8, afterFinish:function(){ elm.css({position:'absolute', zIndex:100000}); elm.show(); } });
	showingElementWithOverlay = elm;
}
function hideOverlay(){
    new Effect.Appear('___perde', { duration: .2, from: 0.8, to: 0.0, afterFinish:function(){ $('#___perde').hide(); $(document.body).css({overflow:'auto'});} });
	showingElementWithOverlay.hide();
	showingElementWithOverlay.css('z-index', showingElementWithOverlayZIndex);
	showingElementWithOverlay = false;
}

/*
#############################
#    Special for Modules    #
#############################
*/

function navigationPopupInit(id, horizontal){
    $(function(){
        $('#'+id+' div').each(function(eix,elm){
            var conId = $(elm).attr('conId');
            if(!conId || conId<2) return;
            $(elm).on('mouseover', function(event){
                var divPopupMenuItems = $('#'+id).find('div.popupMenuItems');
                divPopupMenuItems.hide();
                var div = $(event.target).closest('div');
                var pos = div.offset();
                var conId = $(elm).attr('conId');
                var visibleItemCount = 0;
                $('#'+id+' div.popupMenuItem').each(function(eix,elm){
                    if(elm.catId==conId){
                        $(elm).show();
                        visibleItemCount++;
                    }
                    else
                        $(elm).hide();
                });
                if(horizontal)
                    divPopupMenuItems.css({left:pos.left, top:pos.top+div.getHeight()});
                else
                    divPopupMenuItems.css({left:pos.left+div.getWidth(), top:pos.top});
                if(visibleItemCount)
                    divPopupMenuItems.show();
            });
        });
    });
}

function showManset(event, id){
    var elm = $(event.target);
    if(elm.hasClass('pic') || elm[0].tagName=='A')
    {
        var clItem = $(elm).closest('.clItem');
        if(clItem.length==0) return;
        
        var sTitle = clItem.find('.clSTitle');
        var title = clItem.find('.clTitle');
        var desc = clItem.find('.clDesc');
        var auth = clItem.find('.clAuthor');
        var date = clItem.find('.clDate');
        var picUrl = clItem.find('.picUrl');
        
        var sTitlePlace = $('#Manset_'+id+'_sTitle');
        var titlePlace = $('#Manset_'+id+'_title');
        var descPlace = $('#Manset_'+id+'_desc');
        var authPlace = $('#Manset_'+id+'_auth');
        var datePlace = $('#Manset_'+id+'_date');
        var picPlace = $('#Manset_'+id+'_pic');
        
        if(sTitlePlace && sTitle) sTitlePlace.html(sTitle.html());
        if(titlePlace && title) titlePlace.html(title.html());
        if(descPlace && desc) descPlace.html(desc.html());
        if(authPlace && auth) authPlace.html(auth.html());
        if(datePlace && date) datePlace.html(date.html());
        if(picPlace && picUrl) picPlace.attr('src',picUrl.html());
    }
}

// ContentDisplay için etiket linkleme
function linkTags(contentId){
	var tags = $('#'+contentId+' div.tags a');
	if(!tags || tags.length==0) return;
	var textElms = $('#'+contentId+' div.text');
	if(!textElms || textElms.length==0) return;

	var text = textElms.first().html();
	tags.each(function(eix,a){
		text = text.sub(' '+$(a).html(), ' <a href="#{href}">#{innerHTML}</a>'.interpolate(a));
	});
	textElms.first().html(text);
}

// resimlerin images.google'daki gibi pırtlaması için
function gogPop(event){
	var pop = $('#gogPopDiv'); if(pop) pop.remove();
	var lightBoxDiv = $('#lightBoxDiv'); if(lightBoxDiv) lightBoxDiv.remove();
	
	var img = $(event.target);
	
	if($('#mostLikedIcon').length){
		if(img.mostLiked)
			$('#mostLikedIcon').css({zIndex:10});
		else
			$('#mostLikedIcon').css({zIndex:'auto'});
	}
	$(document.body).append('<div id="gogPopDiv" class="hideOnOut"><img src="'+img.src+'"/></div>');
	pop = $('#gogPopDiv');
	if(img.closest('.lightBox'))
		pop.find('img').on('click', function(){lightBox(img);});
	var pos = img.offset();
	var dim = getDimensions(img);
	var popDim = getDimensions(pop);
	pop.css({left:(pos.left-(popDim.width-dim.width)/2)+'px', top:(pos.top-(popDim.height-dim.height)/2)+'px'});
	pop.hide();
	new Effect.Appear(pop, { duration: 0.2, from: 0.0, to: 1.0 });
}
// lightbox faclty
function lightBox(img){
	var lightBoxDiv = $('#lightBoxDiv'); if(lightBoxDiv) lightBoxDiv.remove();
	
	var fbLikeExist = img.closest('.lightBox').hasClass('fbLike');
	var allImg = img.parent().find('img');
	var html = '<div id="lightBoxDiv">'+
					'<img src="/external/icons/lbPrev.png" id="lbPrev" class="hideOnPerde" style="display:none"/>'+
					'<img id="lbImg" src="'+img.attr('path')+'"/>'+
					'<img src="/external/icons/lbNext.png" id="lbNext" class="hideOnPerde" style="display:none"/>'+
					'<div id="lbLeft"></div>'+
					'<div id="lbCenter"></div>'+
					'<div id="lbRight"><div id="lbCounter">1/20</div>'+(fbLikeExist?'<img id="lbLove" src="'+img.attr('likeSrc')+'"/> <span id="lbLoveCount">0</span>':'') + '</div>'+
					'</div>';
	$(document.body).append(html);
	lightBoxDiv = $('#lightBoxDiv');
	lightBoxDiv.find('#lbPrev').on('click',function(){if(img.previous('img')) {img = img.previous('img'); showPic();}});
	lightBoxDiv.find('#lbNext').on('click',function(){if(img.next('img')) {img = img.next('img'); showPic();}});
	
	if(fbLikeExist){
		lightBoxDiv.find('#lbLove').on('click',function(){
			var id = img.attr('entityId');
			if(!getCookie('like_' + id)){
				var likerNumber = ajax({url:'LikeIt.ashx?id='+id, isJSON:false, noCache:true});
				lightBoxDiv.find('#lbLoveCount').html(likerNumber);
				img.attr('like', likerNumber);
				setCookie('like_' + id, 1, 30);
			}
		});
	}

	function showPic(){
		lightBoxDiv.find('#lbLeft').hide();
		lightBoxDiv.find('#lbCenter').hide();
		lightBoxDiv.find('#lbRight').hide();
		lightBoxDiv.find('#lbPrev').hide();
		lightBoxDiv.find('#lbNext').hide();
		lightBoxDiv.hide();
		lightBoxDiv.find('.tag_bg').each(function(eix,tbg){$(tbg).remove();});
		lbImg.src = img.attr('path'); 
	}
	lightBoxDiv.hide();
	var lbImg = $('#lbImg');
	lbImg.on('load', function(){
		var tagData = img.attr('tagData') ? eval('('+img.attr('tagData')+')') : [];
		lightBoxDiv.find('#lbCounter').html((allImg.indexOf(img) + 1) + ' / ' + allImg.length);
		centerToView(lightBoxDiv);
		if(!showingElementWithOverlay)
			showElementWithOverlay(lightBoxDiv, true, 'white');
		new Effect.Appear(lightBoxDiv, { duration: 0.5, from: 0.0, to: 1.0, afterFinish: function(){
			var lbImgDim = getDimensions(lbImg);
			lightBoxDiv.find('#lbPrev').css({top:(lbImgDim.height/2-19)+'px', left:'10px'});
			lightBoxDiv.find('#lbNext').css({top:(lbImgDim.height/2-19)+'px', right:'10px'});
			var desc = img.attr('desc') || '';
			var title = img.attr('title') || ''; 
			if(img.attr('tag')!='video')
				lightBoxDiv.find('#lbLeft').html(desc);
			else
				desc = '';
			lightBoxDiv.find('#lbCenter').html(title); 
			if(fbLikeExist) lightBoxDiv.find('#lbRight #lbLoveCount').html(img.attr('like')); 
			lightBoxDiv.find('#lbLeft').css({left:'10px',top:(lbImgDim.height+15)+'px',width:(title?45:90)+'%'}).show();
			lightBoxDiv.find('#lbCenter').css({left:(lbImgDim.width*(desc?0.45:0)+10)+'px',width:(desc?45:90)+'%',top:(lbImgDim.height+15)+'px'}).show();
			lightBoxDiv.find('#lbRight').css({left:(lbImgDim.width*.9+10)+'px',top:(lbImgDim.height+15)+'px'}).show();
			
			if(img.attr('tag')=='video'){
				var w = lbImgDim.width, h = lbImgDim.height;
				lbImg.replace(img.attr('desc'));
				lightBoxDiv.find('iframe').css({width:w+'px', height:h+'px'}).attr('id','lbImg');
			}

			if(tagData){
				var tagText = '';
				tagData.each(function(tag, i){
					var x = tag.x, y = tag.y;
					tag.tag = tag.tag || '&nbsp;';
					var tagProp = {width:95, bg:'tagbg1.png'};
					if(tag.tag.length>9) tagProp = {width:125, bg:'tagbg2.png'};
					if(tag.tag.length>12) tagProp = {width:148, bg:'tagbg3.png'};
					if(tag.tag.length>14) tagProp = {width:245, bg:'tagbg4.png'};
					if(tag.tag.length>24) tagProp = {width:307, bg:'tagbg5.png'};
					if(tag.tag.length>30) tagProp = {width:390, bg:'tagbg6.png'};
					lightBoxDiv.append('<div id="tagBg'+i+'" class="tag_bg hideOnPerde" style="display:none;position:absolute;top:'+y+'px;left:'+x+'px;width:'+tagProp.width+'px;background:url(/external/icons/'+tagProp.bg+');"><a href="'+tag.url+'" target="_blank">'+tag.tag+'</a></div>');
					tagText += '<a href="'+tag.url+'" onmouseover="lightBox_hideAllTags();$(\'#tagBg'+i+'\').show()" target="_blank">'+tag.text + '</a><br/>';
				});
				lightBoxDiv.find('#lbCenter').html(tagText);
			}
		} });
	});
	lightBoxDiv.on('mouseover', function(){
		if(img.previous('img')) $('#lbPrev').show(); else $('#lbPrev').hide();
		if(img.next('img')) lightBoxDiv.find('#lbNext').show(); else lightBoxDiv.find('#lbNext').hide();
		if(img.attr('tag')=='video'){
			$('#lbPrev').hide();
			lightBoxDiv.find('#lbNext').hide();
		}
	});
	lbImg.on('mousemove', function(event){
		var scrollOffset = getViewportScrollOffsets();
		var pointerPos = {x:event.pointerX()-scrollOffset.left,y:event.pointerY()-scrollOffset.top};
		lightBoxDiv.find('.tag_bg').each(function(eix,tagElm){
			$(tagElm).show();
			var tagElmPos = $(tagElm).viewportOffset();
			if(tagElmPos.left-50<pointerPos.x && tagElmPos.left+100>pointerPos.x && tagElmPos.top-50<pointerPos.y && tagElmPos.top+80>pointerPos.y)
				$(tagElm).show();
			else
				$(tagElm).hide();
		});
	});
}
function lightBox_hideAllTags(){
	var lightBoxDiv = $('#lightBoxDiv');
	if(!lightBoxDiv) return;
	lightBoxDiv.find('.tag_bg').each(function(eix,tagElm){
		$(tagElm).hide();
	});
}

function centerToView(elm){
	elm = $(elm);
	var dim = getDimensions(elm);
	var posView = getViewportScrollOffsets();
	var dimView = Position.getWindowSize();
	elm.css({left:(posView[0]+(dimView.width-dim.width)/2)+'px', top:(posView[1]+(dimView.height-dim.height)/2)+'px'});	
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
		var ths = this;
        this.container.css({position:'relative', width: ths.width + 'px', height: ths.height + 'px', background: ths.bgColor});
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
        
        this.container.append(str);
    }
});

// StyleEditor

var StyleEditor = Class.create({
    fields: [
        { id: 'clTitle', name: 'Başlık rengi', color: '0066CC', getElements: function() { return $('div.sg_baslik a'); }, styleAttribute: 'color' },
        { id: 'clBack', name: 'Zemin rengi', color: 'C3D9FF', getElements: function() { return $('div.sg_haberler'); }, styleAttribute: 'backgroundColor' },
        { id: 'clBorder', name: 'Çerçeve rengi', color: '0066CC', getElements: function() { return $('div.sg_haberler'); }, styleAttribute: 'borderColor' },
        { id: 'clText', name: 'Metin rengi', color: '003366', getElements: function() { return $('div.sg_metin'); }, styleAttribute: 'color' }
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
        this.container.append(res);
        $('div.__styleEditor input.clBtn').each((function(i,btn) {
            btn.on('click', this.showPalette.bind(this, btn, i));
        }).bind(this));
        $('#cmbPalette').on('change', (function(){this.setColors($('#cmbPalette').options[$('#cmbPalette').selectedIndex].value);}).bind(this));
    },
    fieldIndex: 0,
    showPalette: function(btn, i) {
        this.fieldIndex = i;
        var pos = btn.offset();
        var dim = getDimensions(btn);
        var picker = $('#__color_picker');
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
            this.container.append(res);
            picker = $('#__color_picker');
            $('#__color_picker a').each((function(eix,a) {
                $(a).bind('click', this, this.setColor);
            }).bind(this));
        }
        picker.css({ top: pos.top, left: (pos.left + dim.width) });
        picker.show();
    },
    setColor: function(color) {
        var i = this.fieldIndex;
        var f = this.fields[i];
        f.color = color;
        f.getElements().each(function(eix,elm) {
            var attrib = f.styleAttribute;
            elm.style[attrib] = '#' + color;
            $('#clBtn' + i).css('background', '#' + color);
            $('#clTxt' + i).val(color);
        });
        var picker = $('#__color_picker');
        if(picker) picker.hide();
    }
});

function showDataListPage(url, dataListId) {
    var dataList = $('#DataList_' + dataListId);
    var html = ajax({ url: url, isJSON: false, noCache: true });
    if (html) {
        dataList.replace(html);
        if (preparePaging)
            preparePaging(dataList);
    }
}

//############################
//       CinarWindow
//############################

var CinarWindow = Class.create(); CinarWindow.prototype = {
    initialize: function (options) {
        var ths = this;
        options = Object.extend({
            titleIcon: 'page',
            title: 'Çınar Window',
            width: 950,
            height: 600,
            html: '',
            url: '',
            position: 'center' // left, right
        }, options || {});

        var winOptions = {
            className: 'alphacube',
            title: '<span class="cbtn c' + options.titleIcon + '"></span> ' + options.title,
            width: options.width,
            height: options.height,
            wiredDrag: true,
            destroyOnClose: true
        };

        switch (options.position) {
            case 'left':
                var dim = getDimensions($(document.body));
                winOptions.left = 20;
                winOptions.top = 20;
                break;
            case 'right':
                var dim = getDimensions($(document.body));
                winOptions.left = dim.width - options.width - 20;
                winOptions.top = 20;
                break;
        }

        var win = new Window(winOptions);
        this.win = win;
        if (options.url)
            $(win.getContent()).append('<iframe src="' + options.url + '" style="width:100%;height:100%;"/>');
        else if (options.html)
            $(win.getContent()).append(options.html);

        if (options.position == 'center')
            win.showCenter();
        else
            win.show();

        win.toFront();
    },
    getContent: function () {
        return this.win.getContent();
    },
    close: function () {
        this.win.destroy();
    }
};




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

Object.extend(Array.prototype, {
	binarySearch: function(find, comparator) {
	  var low = 0, high = this.length - 1,
		  i, comparison;
	  while (low <= high) {
		i = Math.floor((low + high) / 2);
		comparison = comparator(this[i], find);
		if (comparison < 0) { low = i + 1; continue; };
		if (comparison > 0) { high = i - 1; continue; };
		return i;
	  }
	  return null;
	}
});

Position.getWindowSize = function(w) {
	var width, height;
	w = w ? w : window;
	width = w.innerWidth || (w.document.documentElement.clientWidth || w.document.body.clientWidth);
	height = w.innerHeight || (w.document.documentElement.clientHeight || w.document.body.clientHeight);

	return { width: width, height: height };
}

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
  
            valueT += elementOverflow.offset().top  || 0; 
            valueL += elementOverflow.offset().left || 0; 
  
            if (elementOverflow.parentNode.getHeight() < elementOverflow.parentNode.scrollHeight) 
            { 
                elementOverflow.parentNode.scrollTop  = valueT; 
                elementOverflow.parentNode.scrollLeft = valueL; 
  
                Element.scrollTo(elementOverflow); 
                return; 
            } 
  
             elementOverflow = elementOverflow.parentNode; 
        } while (true); 
  
         var pos = element.offset(); 
         window.scrollTo(pos.left, pos.top); 
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

function $import(path){
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
function getSelText(){
	var txt = '';
	if (window.getSelection)
		txt = window.getSelection();
	else if (document.getSelection)
		txt = document.getSelection();
	else if (document.selection)
		txt = document.selection.createRange().text;
	return txt;
}
$(document).on('keyup', function(e){
    if(e.keyCode==69){
	    var txt = getSelText().toString();
	    if(txt.length>3) {
            var params = location.href.toQueryParams();
            if(params && params.item){
	            var resp = ajax({url:'EntityInfo.ashx?method=addTag&item='+params.item+'&tag='+txt,isJSON:false,noCache:true});
	            alert(resp);
	        }
	    }
	}
});

var TextAreaUtil = {
	getSelection: function(textArea){
		var textArea = $(textArea);
		var startPos = 0;
		var endPos = 0;
		if(Prototype.Browser.IE){
			textArea.focus();
			var range = document.selection.createRange();
			var strLen = range.text.length;
			range.moveStart ('character', -textArea.val().length);
			startPos = range.text.length;
			endPos = startPos + strLen;
		}
		else {
			startPos = textArea.selectionStart
			endPos = textArea.selectionEnd;
		}
		var text = (textArea.val()).substring(startPos,endPos);
		return {startPos:startPos, endPos:endPos, text:text};
	},
	addTag: function(textArea, front, back, prompt, prompt_val){
		if(prompt){
			prompt_val = window.prompt(prompt, prompt_val);
			if(!prompt_val) return;
			front = front.replace('_prompt_', prompt_val);
			back = back.replace('_prompt_', prompt_val);
		}
	
		var textArea = $(textArea);
		var scrollPos = textArea.scrollTop;
		var sel = TextAreaUtil.getSelection(textArea);
		var newStr = front + sel.text + back;
		var newValue = textArea.val().substring(0,sel.startPos) + newStr + textArea.val().substring(sel.endPos, textArea.val().length);
		textArea.val(newValue);

		if (Prototype.Browser.IE) { 
			textArea.focus();
			var range = document.selection.createRange();
			range.moveStart ('character', -textArea.val().length);
			range.moveStart ('character', sel.startPos+newStr.length);
			range.moveEnd ('character', 0);
			range.select();
		}
		else {
			textArea.selectionStart = sel.startPos+newStr.length;
			textArea.selectionEnd = sel.startPos+newStr.length;
			textArea.focus();
		}
		textArea.scrollTop = scrollPos;
	}
}

//##########################
//#        Dialogs         #
//##########################

function nicePrompt(prompt, validationCallback, okCallback){
    var title = '<span class="cbtn cinfo" style="vertical-align:middle"></span> ' + lang('Information');
    var win = new Window({className: 'alphacube', title: title, maximizable:false, minimizable:false, width:420, height:115, wiredDrag: true, destroyOnClose:true}); 
    var str = '<p align="center"><br/>' + prompt + '<br/>';
    str += '<input type="text" id="promptCtrl" style="width:400px"></p>';
    str += '<p style="position: absolute;right: 0;bottom: 3px;"><span id="btnPromptOK" class="ccBtn cok">' + lang('OK') + '</span> <span id="btnPromptCancel" class="ccBtn ccancel">' + lang('Cancel') + '</span></p>';
    win.getContent().prepend(str);
    win.showCenter();
    win.toFront();
    var selCtrl = $('#promptCtrl');
    selCtrl.focus();
    if(!validationCallback) validationCallback = function(){return true;};
    $('#btnPromptOK').on('click', function(){
        var val = selCtrl.val();
        if(validationCallback(val)){
            Windows.getFocusedWindow().close();
            if(okCallback) okCallback(val);
        }
    });
    $('#btnPromptCancel').on('click', function(){Windows.getFocusedWindow().close();});
}
function niceAlert(alert){
    var title = '<span class="cbtn cerror" style="vertical-align:middle"></span> ' + lang('Error');
    var win = new Window({className: 'alphacube', title: title, maximizable:false, minimizable:false, width:420, height:100, wiredDrag: true, destroyOnClose:true}); 
    var str = '<br/>' + alert.replace('\n','<br/>') + '<br/>';
    str += '</br><p style="position: absolute;right: 0;bottom: 3px;"><span id="btnPromptOK" class="ccBtn cok">' + lang('OK') + '</span></p>';
    win.getContent().prepend(str);
    win.showCenter();
    win.toFront();
    $('#btnPromptOK').on('click', function(){Windows.getFocusedWindow().close();});
}
function niceInfo(alert, okCallback){
    var title = '<span class="cbtn cinfo" style="vertical-align:middle"></span> ' + lang('Information');
    var win = new Window({className: 'alphacube', title: title, maximizable:false, minimizable:false, width:420, height:100, wiredDrag: true, destroyOnClose:true}); 
    var str = '<br/>' + alert.replace('\n','<br/>') + '<br/>';
    str += '</br><p style="position: absolute;right: 0;bottom: 3px;"><span id="btnPromptOK" class="ccBtn cok">' + lang('OK') + '</span></p>';
    win.getContent().prepend(str);
    win.showCenter();
    win.toFront();
    $('#btnPromptOK').on('click', function(){
        Windows.getFocusedWindow().close();
        if(okCallback) okCallback();
    });
}
function niceConfirm(confirm, okCallback){
    var title = '<span class="cbtn cwarning" style="vertical-align:middle"></span> ' + lang('Warning');
    var win = new Window({className: 'alphacube', title: title, maximizable:false, minimizable:false, width:420, height:100, wiredDrag: true, destroyOnClose:true}); 
    var str = '<p align="center"><br/>' + confirm + '</p>';
    str += '<p style="position: absolute;right: 0;bottom: 3px;"><span id="btnPromptOK" class="ccBtn cok">' + lang('OK') + '</span> <span id="btnPromptCancel" class="ccBtn ccancel">' + lang('Cancel') + '</span></p>';
    win.getContent().prepend(str);
    win.showCenter();
    win.toFront();
    var selCtrl = $('#btnPromptCancel');
    selCtrl.focus();
    $('#btnPromptOK').on('click', function(){
        Windows.getFocusedWindow().close();
        if(okCallback) okCallback();
    });
    $('#btnPromptCancel').on('click', function(){Windows.getFocusedWindow().close();});
}
