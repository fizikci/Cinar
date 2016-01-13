var currentMousePos = { x: -1, y: -1 };
jQuery(function($) {
    $(document).mousemove(function(event) {
        currentMousePos.x = event.pageX;
        currentMousePos.y = event.pageY;
    });
});

function scroll(element, parent){
     //$(parent)[0].scrollIntoView(true);
     var oldScrollTop = $(parent).scrollTop();
     var newScrollTop = $(parent).scrollTop() + $(element).offset().top - $(parent).offset().top;
     if((newScrollTop-oldScrollTop)>=$(parent).height())
        $(parent).animate({ scrollTop: newScrollTop }, { duration: 'slow', easing: 'swing'});
}

Array.prototype.shuffle = function() {
  var i = this.length, j, temp;
  if ( i == 0 ) return this;
  while ( --i ) {
     j = Math.floor( Math.random() * ( i + 1 ) );
     temp = this[i];
     this[i] = this[j];
     this[j] = temp;
  }
  return this;
}