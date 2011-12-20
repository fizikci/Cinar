// on load
document.observe('dom:loaded', function(){
	// on body click find visible editors and hide if not the click is within
    Event.observe(document.body,'mousedown', function(event){
        $$('.hideOnOut').each(function(editor){
            if(!Position.within(editor, Event.pointerX(event),Event.pointerY(event))){
                if(editor.id=='smMenu' && editor.visible())
                    popupMenu.onHide();
				if(event.element().hasClassName('hideOnOutException') || event.element().up('.hideOnOutException'))
					Prototype.K();
				else
					editor.hide();
            }
        });
        $$('.removeOnOut').each(function(editor){
            if(!Position.within(editor, Event.pointerX(event),Event.pointerY(event)))
                editor.remove();
        });
	});
	// listelerde resimlerin google.images'deki gibi pırtlaması için
	$$('.gogPop img').each(function(img){
		img.on('mouseover', gogPop);
	});
	// listelerde description alanlarının görünüp gizlenmesi
	var effectInExecution=null;
	$$('.toogleDesc .clItem').each(function(elm){
		var elmDesc = elm.down('div.clDesc');
		if(elmDesc){
			elmDesc.hide();
			Event.observe(elm, 'mouseenter', function(event){
				event.stop();
				//if(effectInExecution) effectInExecution.cancel(); 
				//effectInExecution = new Effect.BlindDown(elmDesc, {duration:0.3, afterFinish: function(){elm.setStyle({borderColor:'#838383'});}});
				new Effect.Parallel([new Effect.BlindDown(elmDesc), new Effect.Morph(elm, {style:'background:#C1C3C2; border-color:#838383;'})], {duration:0.3, afterFinish: function(){elm.setStyle({borderColor:'#838383'});}});
				//elmDesc.show(); new Effect.Morph(elm, {style:'clItem2', duration:0.3, afterFinish: function(){elm.setStyle({borderColor:'#838383'});}});
			});
			Event.observe(elm, 'mouseleave', function(event){
				event.stop();
				//if(effectInExecution) effectInExecution.cancel(); 
				//effectInExecution = new Effect.BlindUp(elmDesc, {duration:0.3, afterFinish: function(){elm.setStyle({borderColor:'#E1E3E2'}); elmDesc.setStyle({height:'auto'});}});
				new Effect.Parallel([new Effect.BlindUp(elmDesc), new Effect.Morph(elm, {style:'background:#E1E3E2; border-color:#E1E3E2;', afterFinish: function(){elmDesc.setStyle({height:'auto'});}})], {duration:0.3});
				//elmDesc.hide(); new Effect.Morph(elm, {style:'clItem2', transition: Effect.Transitions.reverse, afterFinish: function(){elm.removeClassName('clItem2');elm.setStyle({borderColor:'#E1E3E2'}); elmDesc.setStyle({height:'auto'});}, duration:0.3});
			});
		}
    });
	// listelerde description alanlarının resim üzerinde görünüp gizlenmesi
	$$('.showDescOnImg img').each(function(elm){
		var elmImg = elm;//.down('img');
		var elmDesc = elm.next('div.clDesc');
		if(elm.up('.lightBox')) elmDesc.on('click', function(){lightBox(elmImg);});
		if(elmDesc){
			elmDesc.hide();
			Event.observe(elmImg, 'mouseenter', function(event){
				var dimImg = elmImg.getDimensions();
				elmDesc.setStyle({left:elmImg.style.left, top:elmImg.style.top, width:dimImg.width, height:dimImg.height, margin:'0px 10px 10px 0px'});
				elmDesc.show();
			});
			Event.observe(elmDesc, 'mouseleave', function(event){
				elmDesc.hide();
			});
		}
    });
	// masonry
	Event.observe(window, 'load', function(){
		$$('.masonry').each(function(elm){
			var sorted = elm.select('img').sortBy(function(img){return img.getHeight();});
			var cols = [[], [], []];
			for(var i = 0; i<cols.length; i++)
				for(var j = i; j<sorted.length; j+=cols.length)
					cols[i].push(sorted[j]);
			var maxHeight = 0;
			for(var i = 0; i<cols.length; i++){
				var left = i * (cols[0][0].getWidth() + 10);
				var top = 0;
				for(var j = 0; j<cols[i].length; j++){
					cols[i][j].setStyle({left:left, top:top});
					top += cols[i][j].getHeight() + 10;
				}
				if(top>maxHeight) maxHeight = top;
			}
			elm.setStyle({height:maxHeight});
		});
	});
	// fadeShow (yani fadeIn'li slide show)
	if($$('.fadeShow').length)
		$$('.fadeShow').each(function(elm){
			elm.insert('<div class="indexElms"></div>');
			elm.timeout = setTimeout(function(){fadeShowShowImg(elm);}, 3000);
		});
	$$('.fadeShow .clItem').each(function(elm, i){
		if(i==0){
			elm.up().currentImg = elm;
			elm.setStyle({zIndex:2});
		}
		else
			elm.fade({ duration: 0.05, from: 1, to: 0.01 });
		elm.up('.fadeShow').select('.indexElms')[0].insert('<img src="/external/icons/bullet_'+(i==0 ? 'gray':'white')+'.png" index="'+i+'"/>');
		var indexElm = elm.up('.fadeShow').select('.indexElms')[0].descendants().last();
		indexElm.observe('click', function(event){
			fadeShowShowImg(elm.up('.fadeShow'), indexElm);
		});
	});
	// fadeWithArrows
	if($$('.fadeWithArrows').length){
		$$('.fadeWithArrows').each(function(elm){
			elm.insert('<img src="/external/icons/lbPrev.png" id="lbPrev" style="display:none"/><img src="/external/icons/lbNext.png" id="lbNext" style="display:none"/>');
			elm.down('#lbPrev').on('click', function(){fadeWithArrowsShow(elm, 'prev');});
			elm.down('#lbNext').on('click', function(){fadeWithArrowsShow(elm, 'next');});
			if(elm.select('.clItem').length > 1)
				elm.down('#lbNext').show();
		});
	}
	$$('.fadeWithArrows .clItem').each(function(elm, i){
		if(i==0){
			elm.up('.fadeWithArrows').currentImg = elm;
			elm.setStyle({zIndex:2});
		} else {
			elm.fade({ duration: 0.05, from: 1, to: 0.01 });
		}
	});
	// slideShow
	if($$('.slideShow').length)
		$$('.slideShow').each(function(elm){
			if(elm.select('img').length==0){
				elm.remove();
				return;
			}
			elm.playing = false;
			elm.alreadySliding = false;
			elm.innerHTML = '<div class="paging" style="background-image:url(/external/icons/slide_prev.png); background-position:right center;"></div>'+
							'<div class="clipper">'+
							'<div class="innerDiv">'+ 
							elm.innerHTML + 
							'</div>'+
							'</div>'+
							'<div class="paging"  style="background-image:url(/external/icons/slide_next.png); background-position:left center;"></div>'+
							'<div style="clear:both"></div>'+
							'<img src="/external/icons/play.png" class="playBtn"/>';
			
			elm.down('.playBtn').on('click', function(){
				elm.playing = !elm.playing;
				if(elm.playing){
					elm.down('.playBtn').src = '/external/icons/pause.png';
					slideShowSlide(elm);
				} else {
					clearTimeout(elm.timeout);
					elm.down('.playBtn').src = '/external/icons/play.png';
				}
			});

			var imgs = elm.select('img');
			var imgCounter = 0;
			imgs.each(function(img){
				img.observe('load', function(){
					imgCounter++;
					if(imgCounter==imgs.length){
						var totalWidth = 0;
						imgs.each(function(i){totalWidth += i.getWidth()+20;});
						elm.down('.innerDiv').setStyle({width:(totalWidth-20)+'px'});
					}
				});
			});
			elm.select('.paging')[0].observe('click', function(){slideShowSlide(elm,'back');});
			elm.select('.paging')[1].observe('click', function(){slideShowSlide(elm);});
		});
	// lightBox olayı
	if($$('.lightBox').length){
		document.on('keydown', function(event){
			var lightBoxDiv = $('lightBoxDiv');
			if(!lightBoxDiv) return;
            switch(event.keyCode){
				case Event.KEY_LEFT:
					lightBoxDiv.down('#lbPrev').simulate('click');
					break;
				case Event.KEY_RIGHT:
					lightBoxDiv.down('#lbNext').simulate('click');
					break;
				case Event.KEY_ESC:
					lightBoxDiv.hide(); hideOverlay();
					break;
			}
		});
		var imgs = $$('.lightBox img');
		if(imgs.length){
			imgs.each(function(img){
				img.on('click', function(){lightBox(img);});
			});
			var mostLiked = imgs.sortBy(function(img){return parseInt(img.readAttribute('like'));})[imgs.length-1];
			mostLiked.mostLiked=true;
			if(parseInt(mostLiked.readAttribute('like'))>0){
				var f = function(){
					var pos = mostLiked.viewportOffset();
					$(document.body).insert('<img id="mostLikedIcon" src="/external/icons/love.png" style="position:absolute;left:'+(pos.left+mostLiked.getWidth()-22)+'px;top:'+(pos.top+mostLiked.getHeight()-18)+'px;"/>');
				};
				if(mostLiked.readyState && mostLiked.readyState=='complete') // IE8 fix
					f();
				else
					mostLiked.on('load', f);
			}
		}
	}
});
function slideShowSlide(elm, dir){
	if(elm.alreadySliding) return;
	elm.alreadySliding = true;
	
    var dimCl = elm.down('.clipper').getDimensions();
	var leftID = parseInt(elm.down('.innerDiv').style.left);
    var dimID = elm.down('.innerDiv').getDimensions();

	if(leftID + dimID.width < dimCl.width+60)
		new Effect.Move(elm.down('.innerDiv'), { x: -1*leftID, y: 0, mode: 'relative', duration:1.0, afterFinish:function(){elm.alreadySliding = false;} });
	else if(dir == 'back'){
		if(leftID < 0)
			new Effect.Move(elm.down('.innerDiv'), { x: elm.down('.clipper').getWidth()+20, y: 0, mode: 'relative', duration:1.0, afterFinish:function(){elm.alreadySliding = false;} });
		else
			elm.alreadySliding = false;
	} else
		new Effect.Move(elm.down('.innerDiv'), { x: -1*(elm.down('.clipper').getWidth()+20), y: 0, mode: 'relative', duration:1.0, afterFinish:function(){elm.alreadySliding = false;} });
		
	clearTimeout(elm.timeout);
	if(elm.playing){
		elm.timeout = setTimeout(function(){slideShowSlide(elm);}, 5000);
	}
}
function fadeShowShowImg(fadeShow, indexElm){
	var currIndexElm = null;
	if(!indexElm){
		indexElm = fadeShow.down('.indexElms');
		if(!indexElm) return;
		currIndexElm = indexElm.descendants()[fadeShow.select('.clItem').indexOf(fadeShow.currentImg)];
		indexElm = currIndexElm.next() ? currIndexElm.next() : fadeShow.down('.indexElms img');
	}
	else
		currIndexElm = fadeShow.down('.indexElms').descendants()[fadeShow.select('.clItem').indexOf(fadeShow.currentImg)];
		
	indexElm.src = '/external/icons/bullet_gray.png';
	if(currIndexElm)
		currIndexElm.src = '/external/icons/bullet_white.png';
	
	clearTimeout(fadeShow.timeout);
	var i = parseInt(indexElm.readAttribute('index'));
	
	fadeShow.currentImg.fade({ duration: 0.5, from: 1, to: 0.01 });
	fadeShow.currentImg.setStyle({zIndex:1});
	
	fadeShow.currentImg = fadeShow.select('.clItem')[i];
	fadeShow.currentImg.fade({ duration: 0.5, from: 0, to: 1 });
	fadeShow.currentImg.setStyle({zIndex:2});

	fadeShow.timeout = setTimeout(function(){fadeShowShowImg(fadeShow);}, 3000);
}
function fadeWithArrowsShow(elm, which){
	var nextImg = which=='next' ? elm.currentImg.next('.clItem') : elm.currentImg.previous('.clItem');
	if(nextImg){
		elm.currentImg.fade({ duration: 0.5, from: 1, to: 0.01 });
		nextImg.fade({ duration: 0.5, from: 0, to: 1 });
		elm.currentImg = nextImg;
	}
	if(elm.currentImg.next('.clItem')) elm.down('#lbNext').show(); else elm.down('#lbNext').hide();
	if(elm.currentImg.previous('.clItem')) elm.down('#lbPrev').show(); else elm.down('#lbPrev').hide();
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
        return '? ' + code;
    return str;
}

// opacity'si düşerekten sayfanın ortasında div gösterme olayı
var showingElementWithOverlay = false;
var showingElementWithOverlayZIndex = 0;
function showElementWithOverlay(elm, autoHide, color){
    elm = $(elm);
	showingElementWithOverlayZIndex = elm.style.zIndex;
    elm.hide();
	
	var dim = $$('html')[0].getDimensions();
	if(Prototype.Browser.IE) {dim.height = $$('body')[0].scrollHeight; dim.width -= 20;}
    
    var perde = $('___perde');
    if(!perde){
        new Insertion.Top(document.body, '<div id="___perde" style="display:none;position: absolute;top: 0;left: 0;z-index: 90000;width:'+dim.width+'px;height:'+dim.height+'px;background-color: '+(color ? color : '#000')+';"'+(autoHide?' onclick="hideOverlay()"':'')+'></div>');
        perde = $('___perde');
		perde.on('mouseover', function(){
			$$('.hideOnPerde').each(function(hop){
				hop.hide();
			});
		});
    }

    //$(document.body).setStyle({overflow:'hidden'});
    new Effect.Appear(perde, { duration: .2, from: 0.0, to: 0.8, afterFinish:function(){ elm.setStyle({position:'absolute', zIndex:100000}); elm.show(); } });
	showingElementWithOverlay = elm;
}
function hideOverlay(){
    new Effect.Appear('___perde', { duration: .2, from: 0.8, to: 0.0, afterFinish:function(){ $('___perde').hide(); $(document.body).setStyle({overflow:'auto'});} });
	showingElementWithOverlay.hide();
	showingElementWithOverlay.style.zIndex = showingElementWithOverlayZIndex;
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
	
	if(img.mostLiked)
		$('mostLikedIcon').setStyle({zIndex:10});
	else
		$('mostLikedIcon').setStyle({zIndex:'auto'});
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
// lightbox faclty
function lightBox(img){
	var lightBoxDiv = $('lightBoxDiv'); if(lightBoxDiv) lightBoxDiv.remove();
	
	var allImg = img.up('.lightBox').select('img');
	var html = '<div id="lightBoxDiv">'+
					'<img src="/external/icons/lbPrev.png" id="lbPrev" class="hideOnPerde" style="display:none"/>'+
					'<img id="lbImg" src="'+img.readAttribute('path')+'"/>'+
					'<img src="/external/icons/lbNext.png" id="lbNext" class="hideOnPerde" style="display:none"/>'+
					'<div id="lbLeft"></div>'+
					'<div id="lbCenter"></div>'+
					'<div id="lbRight"><div id="lbCounter">1/20</div><img id="lbLove" src="'+img.readAttribute('likeSrc')+'"/> <span id="lbLoveCount">0</span></div>'+
					'</div>';
	$(document.body).insert(html);
	lightBoxDiv = $('lightBoxDiv');
	lightBoxDiv.down('#lbPrev').on('click',function(){if(img.previous('img')) {img = img.previous('img'); showPic();}});
	lightBoxDiv.down('#lbNext').on('click',function(){if(img.next('img')) {img = img.next('img'); showPic();}});
	
	lightBoxDiv.down('#lbLove').on('click',function(){
		var id = img.readAttribute('entityId');
		if(!getCookie('like_' + id)){
			var likerNumber = ajax({url:'LikeIt.ashx?id='+id, isJSON:false, noCache:true});
			lightBoxDiv.down('#lbLoveCount').innerHTML = likerNumber;
			img.writeAttribute('like', likerNumber);
			setCookie('like_' + id, 1, 30);
		}
	});
	function showPic(){
		lightBoxDiv.down('#lbLeft').hide();
		lightBoxDiv.down('#lbCenter').hide();
		lightBoxDiv.down('#lbRight').hide();
		lightBoxDiv.down('#lbPrev').hide();
		lightBoxDiv.down('#lbNext').hide();
		lightBoxDiv.hide();
		lightBoxDiv.select('.tag_bg').each(function(tbg){tbg.remove();});
		lbImg.src = img.readAttribute('path'); 
	}
	lightBoxDiv.hide();
	var lbImg = $('lbImg');
	lbImg.on('load', function(){
		var tagData = img.readAttribute('tagData') ? eval('('+img.readAttribute('tagData')+')') : [];
		lightBoxDiv.down('#lbCounter').innerHTML = (allImg.indexOf(img) + 1) + '/' + allImg.length;
		centerToView(lightBoxDiv);
		if(!showingElementWithOverlay)
			showElementWithOverlay(lightBoxDiv, true, 'white');
		new Effect.Appear(lightBoxDiv, { duration: 0.5, from: 0.0, to: 1.0, afterFinish: function(){
			var lbImgDim = lbImg.getDimensions();
			lightBoxDiv.down('#lbPrev').setStyle({top:(lbImgDim.height/2-19)+'px', left:'10px'});
			lightBoxDiv.down('#lbNext').setStyle({top:(lbImgDim.height/2-19)+'px', right:'10px'});
			lightBoxDiv.down('#lbLeft').innerHTML = img.readAttribute('desc') || ''; 
			lightBoxDiv.down('#lbCenter').innerHTML = img.readAttribute('title') || ''; 
			lightBoxDiv.down('#lbRight #lbLoveCount').innerHTML = img.readAttribute('like'); 
			lightBoxDiv.down('#lbLeft').setStyle({left:'0px',top:(lbImgDim.height+15)+'px'}).show();
			lightBoxDiv.down('#lbCenter').setStyle({left:(lbImgDim.width/5)+'px',top:(lbImgDim.height+15)+'px'}).show();
			lightBoxDiv.down('#lbRight').setStyle({left:(lbImgDim.width/5*4)+'px',top:(lbImgDim.height+15)+'px'}).show();

			if(tagData){
				var tagText = '';
				tagData.each(function(tag, i){
					var x = tag.x, y = tag.y;
					lightBoxDiv.insert('<div id="tagBg'+i+'" class="tag_bg hideOnPerde" style="display:none;position:absolute;top:'+y+'px;left:'+x+'px"><a href="'+tag.url+'" target="_blank">'+(tag.tag || '&nbsp;')+'</a></div>');
					tagText += '<a href="'+tag.url+'" onmouseover="lightBox_hideAllTags();$(\'tagBg'+i+'\').show()" target="_blank">'+tag.text + '</a><br/>';
				});
				lightBoxDiv.down('#lbCenter').innerHTML = tagText;
			}
		} });
	});
	lightBoxDiv.on('mouseover', function(){
		if(img.previous('img')) $('lbPrev').show(); else $('lbPrev').hide();
		if(img.next('img')) lightBoxDiv.down('#lbNext').show(); else lightBoxDiv.down('#lbNext').hide();
	});
	lbImg.on('mousemove', function(event){
		var scrollOffset = document.viewport.getScrollOffsets();
		var pointerPos = {x:event.pointerX()-scrollOffset.left,y:event.pointerY()-scrollOffset.top};
		lightBoxDiv.select('.tag_bg').each(function(tagElm){
			tagElm.show();
			var tagElmPos = tagElm.viewportOffset();
			if(tagElmPos.left-50<pointerPos.x && tagElmPos.left+100>pointerPos.x && tagElmPos.top-50<pointerPos.y && tagElmPos.top+80>pointerPos.y)
				tagElm.show();
			else
				tagElm.hide();
		});
	});
}
function lightBox_hideAllTags(){
	var lightBoxDiv = $('lightBoxDiv');
	if(!lightBoxDiv) return;
	lightBoxDiv.select('.tag_bg').each(function(tagElm){
		tagElm.hide();
	});
}

function centerToView(elm){
	elm = $(elm);
	var dim = elm.getDimensions();
	var posView = document.viewport.getScrollOffsets();
	var dimView = Position.getWindowSize();
	elm.setStyle({left:(posView[0]+(dimView.width-dim.width)/2)+'px', top:(posView[1]+(dimView.height-dim.height)/2)+'px'});	
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

/**
 * Event.simulate(@element, eventName[, options]) -> Element
 * 
 * - @element: element to fire event on
 * - eventName: name of event to fire (only MouseEvents and HTMLEvents interfaces are supported)
 * - options: optional object to fine-tune event properties - pointerX, pointerY, ctrlKey, etc.
 *
 *    $('foo').simulate('click'); // => fires "click" event on an element with id=foo
 *
 **/
(function(){
  
  var eventMatchers = {
    'HTMLEvents': /^(?:load|unload|abort|error|select|change|submit|reset|focus|blur|resize|scroll)$/,
    'MouseEvents': /^(?:click|mouse(?:down|up|over|move|out))$/
  }
  var defaultOptions = {
    pointerX: 0,
    pointerY: 0,
    button: 0,
    ctrlKey: false,
    altKey: false,
    shiftKey: false,
    metaKey: false,
    bubbles: true,
    cancelable: true
  }
  
  Event.simulate = function(element, eventName) {
    var options = Object.extend(Object.clone(defaultOptions), arguments[2] || { });
    var oEvent, eventType = null;
    
    element = $(element);
    
    for (var name in eventMatchers) {
      if (eventMatchers[name].test(eventName)) { eventType = name; break; }
    }

    if (!eventType)
      throw new SyntaxError('Only HTMLEvents and MouseEvents interfaces are supported');

    if (document.createEvent) {
      oEvent = document.createEvent(eventType);
      if (eventType == 'HTMLEvents') {
        oEvent.initEvent(eventName, options.bubbles, options.cancelable);
      }
      else {
        oEvent.initMouseEvent(eventName, options.bubbles, options.cancelable, document.defaultView, 
          options.button, options.pointerX, options.pointerY, options.pointerX, options.pointerY,
          options.ctrlKey, options.altKey, options.shiftKey, options.metaKey, options.button, element);
      }
      element.dispatchEvent(oEvent);
    }
    else {
      options.clientX = options.pointerX;
      options.clientY = options.pointerY;
      oEvent = Object.extend(document.createEventObject(), options);
      element.fireEvent('on' + eventName, oEvent);
    }
    return element;
  }
  
  Element.addMethods({ simulate: Event.simulate });
})();

var TextAreaUtil = {
	getSelection: function(textArea){
		var textArea = $(textArea);
		var startPos = 0;
		var endPos = 0;
		if(Prototype.Browser.IE){
			textArea.focus();
			var range = document.selection.createRange();
			var strLen = range.text.length;
			range.moveStart ('character', -textArea.value.length);
			startPos = range.text.length;
			endPos = startPos + strLen;
		}
		else {
			startPos = textArea.selectionStart
			endPos = textArea.selectionEnd;
		}
		var text = (textArea.value).substring(startPos,endPos);
		return {startPos:startPos, endPos:endPos, text:text};
	},
	addTag: function(textArea, front, back){
		var textArea = $(textArea);
		var scrollPos = textArea.scrollTop;
		var sel = TextAreaUtil.getSelection(textArea);
		var newStr = front + sel.text + back;
		var newValue = textArea.value.substring(0,sel.startPos) + newStr + textArea.value.substring(sel.endPos, textArea.value.length);
		textArea.value = newValue;

		if (Prototype.Browser.IE) { 
			textArea.focus();
			var range = document.selection.createRange();
			range.moveStart ('character', -textArea.value.length);
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
