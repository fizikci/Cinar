// Prototype Masonry version 0.2
// a port of jQuery Masonry for prototype.js version 1.6.1
//
// Ported by: Ilian Konchev (konchev@gmail.com)


Element.Storage = {
  UID: 1
};


var Masonry = Class.create({

	initialize: function(element, options, callback){
		element.store('callback',callback);
		element.store('columnWidth', options.columnWidth || element.retrieve('columnWidth',undefined))
		element.store('itemSelector', options.itemSelector || undefined);
		element.store('topOffset', options.topOffset || 0);
		element.store('leftOffset', options.leftOffset || 0);
		element.store('$bricks', options.$bricks || undefined);
		element.store('$brickParent', options.$brickParent || element);
		element.store('masoned', element.retrieve('masoned',undefined));
		element.store('colCountOld', element.retrieve('colCount',undefined))
		element.store('colCount', options.colCount || undefined)
		element.store('appendedContent', options.appendedContent || undefined)
		element.store('singleMode', options.singleMode || false)
		this.masonry(element)
	},

	masonry: function(element, callback){
		if (element.retrieve('masoned',undefined) == undefined){
			element.store('masoned',element)
		}
		this.masonrySetup(element);
		this.masonryArrange(element);
	},

	placeBrick: function(element, $brick, setCount, setY, setSpan) {
		
		var shortCol = 0;
		for ( i=0; i < setCount; i++ ) {
				if ( setY[i] < setY[ shortCol ] ) shortCol = i;
		}
		
		$brick.setStyle({ top: (setY[ shortCol ]+element.retrieve('topOffset',0))+"px", left: (element.retrieve('colW',undefined) * shortCol + element.retrieve('posLeft',undefined)) + element.retrieve('leftOffset',0) + "px"});
		colY = element.retrieve('colY',[]);
		for ( i=0; i < setSpan; i++ ) {
			colY[ shortCol + i ] = setY[ shortCol ] + $brick.getHeight() + parseInt($brick.getStyle('marginTop')) + parseInt($brick.getStyle('marginBottom'));
		}
		element.store('colY',colY);
	},

	masonrySetup: function(element) {
		var bricks = element.retrieve('itemSelector',undefined) == undefined ? element.retrieve('$brickParent', element).childElements() : element.retrieve('$brickParent', element).select(element.retrieve('itemSelector',undefined));
		if (element.retrieve('appendedContent',undefined) != undefined) {
			var bricks = element.retrieve('itemSelector',undefined) == undefined ? element.retrieve('appendedContent', element).childElements() : element.retrieve('appendedContent', element).select(element.retrieve('itemSelector',undefined));
		}
		element.store('$bricks',bricks);
		//bricks.each(function(brick){console.log(brick);})

		var colW = undefined;
		if ( element.retrieve('columnWidth',undefined) == undefined) {
			var firstBrick = element.retrieve('$bricks',undefined).first();
			colW = firstBrick.getWidth() + parseInt(firstBrick.getStyle('marginLeft')) + parseInt(firstBrick.getStyle('marginRight'));
			
		} else {
			colW = element.retrieve('columnWidth',1);
		}
		colCount = Math.floor( (element.getWidth() - (2 * element.retrieve('leftOffset',0))) / colW ) ;
		colCount = Math.max( colCount, 1 );
		element.store('columnWidth',colW);
		element.store('colW',colW);
		element.store('colCount',colCount);
	},
	masonryArrange: function(element) {
		// if masonry hasn't been called before
		if( element.retrieve('masoned',undefined)) element.setStyle({position: 'relative'});

	
		if ( (element.retrieve('masoned',undefined) != undefined) || element.retrieve('appendedContent',undefined) != undefined ) {
			// just the new bricks

			element.retrieve('$bricks').each(function(el){el.setStyle({position: 'absolute'});});
		}
		
		// get top left position of where the bricks should be
		var cursor = new Element('div');
		element.insert({top: cursor});
		element.store('posTop', Math.round( cursor.positionedOffset().top));
		element.store('posLeft', Math.round( cursor.positionedOffset().left));
		cursor.remove();

		// set up column Y array
		if ( (element.retrieve('masoned',undefined)) && element.retrieve('appendedContent',undefined) != undefined ) {
			//if appendedContent is set, use colY from last call
			colY = element.retrieve('colY',0)
			/*
			*  in the case that the wall is not resizeable,
			*  but the colCount has changed from the previous time
			*  masonry has been called
			*/
			for (i = element.retrieve('colCountOld',0); i < element.retrieve('colCount',0); i++) {
				colY[i] = element.retrieve('posTop',0)
      };

		} else {
			colY = [];
			for ( i=0; i < element.retrieve('colCount',0); i++) {
				colY[i] = element.retrieve('posTop',0);
			}
			element.store('colY',colY)
		}
		var obj = this;
		// layout logic
		if ( element.retrieve('singleMode',false) ) {
			element.retrieve('$bricks',undefined).each(function($brick){
				obj.placeBrick(element, $brick, element.retrieve('colCount',undefined), element.retrieve('colY',undefined), 1);
			});
		} else {
			
			element.retrieve('$bricks',undefined).each(function($brick) {
				//how many columns does this brick span
				var colSpan = Math.ceil( $brick.getWidth() / element.retrieve('colW',1));
				colSpan = Math.min( colSpan, element.retrieve('colCount',undefined));
				if ( colSpan == 1 ) {
					// if brick spans only one column, just like singleMode
					obj.placeBrick(element, $brick, element.retrieve('colCount',undefined), element.retrieve('colY',undefined), 1);
				} else {
					// brick spans more than one column

					//how many different places could this brick fit horizontally
					var groupCount = element.retrieve('colCount',undefined) + 1 - colSpan;
					var groupY = [0];
					var savedColY = element.retrieve('colY',[]);
					// for each group potential horizontal position
					for ( i=0; i < groupCount; i++ ) {
						groupY[i] = 0;
						// for each column in that group
						for ( j=0; j < colSpan; j++ ) {
							// get the maximum column height in that group
							groupY[i] = Math.max( groupY[i], savedColY[i+j] );
						}
					}
					
					obj.placeBrick(element, $brick, groupCount, groupY, colSpan);
				}
			}); //        each(function(el)...
		}  //         layout logic

		// set the height of the wall to the tallest column
		var savedColY = element.retrieve('colY',[]);
		var wallH = 0
		for ( i=0; i < element.retrieve('colCount',undefined); i++ ) {
			wallH = Math.max( wallH, savedColY[i] );
		}
		element.store('wallH',wallH);
		element.setStyle({height: (wallH - element.retrieve('posTop',undefined))+"px"});
		var callback = element.retrieve('callback',undefined)
		if (callback != undefined) {
			callback.call(element.retrieve('$bricks',undefined));
		}
	} //  /masonryArrange function
});