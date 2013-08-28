// Copyright (c) 2006 Sébastien Gruhier (http://xilinus.com, http://itseb.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// VERSION 1.1

var Window = Class.create();
Window.keepMultiModalWindow = false;
Window.prototype = {
  // Constructor
  // Available parameters : className, title, minWidth, minHeight, maxWidth, maxHeight, width, height, top, left, bottom, right, resizable, zIndex, opacity, recenterAuto, wiredDrag
  //                        hideEffect, showEffect, showEffectOptions, hideEffectOptions, effectOptions, url, draggable, closable, minimizable, maximizable, parent, onload
  //                        add all callbacks (if you do not use an observer)
  //                        onDestroy onStartResize onStartMove onResize onMove onEndResize onEndMove onFocus onBeforeShow onShow onHide onMinimize onMaximize onClose
  
  initialize: function() {
    var id;
    var optionIndex = 0;
    // For backward compatibility like win= new Window("id", {...}) instead of win = new Window({id: "id", ...})
    if (arguments.length > 0) {
      if (typeof arguments[0] == "string" ) {
        id = arguments[0];
        optionIndex = 1;
      }
      else
        id = (arguments[0] ? arguments[0].id : null);
    }
    
    // Generate unique ID if not specified
    if (!id)
      id = "window_" + new Date().getTime();
      
    if ($('#'+id).length)
      alert("Window " + id + " is already registered in the DOM! Make sure you use setDestroyOnClose() or destroyOnClose: true in the constructor");

    this.hasEffectLib = String.prototype.parseColor != null;
    this.options = Object.extend({
      //className:         WindowUtilities.className,
      minWidth:          100, 
      minHeight:         20,
      resizable:         true,
      closable:          true,
      minimizable:       true,
      maximizable:       true,
      draggable:         true,
      userData:          null,
      effectOptions:     null,
      parent:            document.body,
      title:             "&nbsp;",
      url:               null,
      onload:            Prototype.emptyFunction,
      width:             200,
      height:            300,
      opacity:           1,
      recenterAuto:      true,
      wiredDrag:         false,
      closeCallback:     null,
      destroyOnClose:    false
    }, arguments[optionIndex] || {});
    
    if (typeof this.options.top == "undefined" &&  typeof this.options.bottom ==  "undefined") 
      this.options.top = Math.random()*500;
    if (typeof this.options.left == "undefined" &&  typeof this.options.right ==  "undefined") 
      this.options.left = Math.random()*500;

    if (this.options.parent != document.body)  
      this.options.parent = $(this.options.parent);
      
    this.element = this._createWindow(id);
    
    // Bind event listener
    this.eventMouseDown = this._initDrag.bindAsEventListener(this);
    this.eventMouseUp   = this._endDrag.bindAsEventListener(this);
    this.eventMouseMove = this._updateDrag.bindAsEventListener(this);
    this.eventOnLoad    = this._getWindowBorderSize.bindAsEventListener(this);
    this.eventMouseDownContent = this.toFront.bindAsEventListener(this);
    this.eventResize = this._recenter.bindAsEventListener(this);
	
    this.topbar = $('#'+this.element.attr('id') + "_top");
    this.bottombar = $('#'+this.element.attr('id') + "_bottom");
    this.content = $('#'+this.element.attr('id') + "_content");
    
    this.topbar.bind("mousedown", this.eventMouseDown);
    this.bottombar.bind("mousedown", this.eventMouseDown);
    this.content.bind("mousedown", this.eventMouseDownContent);
    $(window).bind("load", this.eventOnLoad);
    $(window).bind("resize", this.eventResize);
    $(window).bind("scroll", this.eventResize);
    
    if (this.options.draggable)  {
      var that = this;
      [this.topbar, this.topbar.parent().prev(), this.topbar.parent().next()].each(function(element) {
        element.bind("mousedown", that.eventMouseDown);
        element.addClass("top_draggable");
      });
      [this.bottombar.parent(), this.bottombar.parent().prev(), this.bottombar.parent().next()].each(function(element) {
        element.bind("mousedown", that.eventMouseDown);
        element.addClass("bottom_draggable");
      });
      
    }    
    
    if (this.options.resizable) {
      this.sizer = $('#'+this.element.attr('id') + "_sizer");
      this.sizer.bind("mousedown", this.eventMouseDown);
    }  
    
    this.useLeft = typeof this.options.left != "undefined";
    this.useTop = typeof this.options.top != "undefined";
    if(this.useLeft && this.useTop)
        this.setLocation(parseFloat(this.options.top), parseFloat(this.options.left));
              
    this.storedLocation = null;
    
    this.setOpacity(this.options.opacity);
    if (this.options.zIndex)
      this.setZIndex(this.options.zIndex)

    if (this.options.destroyOnClose)
      this.setDestroyOnClose(true);

    this._getWindowBorderSize();
    this.width = this.options.width;
    this.height = this.options.height;
    this.visible = false;
    
    this.constraint = false;
    this.constraintPad = {top: 0, left:0, bottom:0, right:0};
    
    if (this.width && this.height)
      this.setSize(this.options.width, this.options.height);
    this.setTitle(this.options.title)
    Windows.register(this);      
  },
  
  // Destructor
  destroy: function() {
    this._notify("onDestroy");
    this.topbar.unbind("mousedown", this.eventMouseDown);
    this.bottombar.unbind("mousedown", this.eventMouseDown);
    this.content.unbind("mousedown", this.eventMouseDownContent);
    
    $(window).unbind("load", this.eventOnLoad);
    $(window).unbind("resize", this.eventResize);
    $(window).unbind("scroll", this.eventResize);
    
    this.content.unbind("load", this.options.onload);

    if (this._oldParent && this._oldParent.length) {
      var content = this.getContent();
      var originalContent = null;
      for(var i = 0; i < content.childNodes.length; i++) {
        originalContent = content.childNodes[i];
        if (originalContent.nodeType == 1) 
          break;
        originalContent = null;
      }
      if (originalContent)
        this._oldParent.append(originalContent);
      this._oldParent = null;
    }

    if (this.sizer)
        this.sizer.unbind("mousedown", this.eventMouseDown);

    if (this.options.url) 
      this.content.src = null

     if(this.iefix) 
      this.iefix.remove();

    this.element.remove();
    Windows.unregister(this);      
  },
    
  // Sets close callback, if it sets, it should return true to be able to close the window.
  setCloseCallback: function(callback) {
    this.options.closeCallback = callback;
  },
  
  // Gets window content
  getContent: function () {
    return this.content;
  },
  
  // Sets the content with an element id
  setContent: function(id, autoresize, autoposition) {
    var element = $('#'+id);
    if (null == element) throw "Unable to find element '" + id + "' in DOM";
    this._oldParent = element.parent();

    var d = null;
    var p = null;

    if (autoresize) 
      d = getDimensions(element);
    if (autoposition) 
      p = element.offset();

    var content = this.getContent();
    // Clear HTML (and even iframe)
    this.setHTMLContent("");
    content = this.getContent();
    
    content.appendChild(element);
    element.show();
    if (autoresize) 
      this.setSize(d.width, d.height);
    if (autoposition) 
      this.setLocation(p.left - this.heightN, p.top - this.widthW);    
  },
  
//  setClassName: function(className){
//    var allElms = this.element.descendants();
//    for(var i=0; i<allElms.length; i++){
//        var elm = allElms[i];
//        elm.className = elm.className.replace(this.options.className, className);
//    }
//    this.options.className = className;
//  },
  
  setHTMLContent: function(html) {
    // It was an url (iframe), recreate a div content instead of iframe content
    if (this.options.url) {
      this.content.src = null;
      this.options.url = null;
      
  	  var content ="<div id=\"" + this.getId() + "_content\" class=\"" + this.options.className + "_content\"> </div>";
      $('#'+this.getId() +"_table_content").html(content);
      
      this.content = $('#'+this.getId() + "_content");
    }
      
    this.getContent().html(html);
  },
  
  setURL: function(url) {
    // Not an url content, change div to iframe
    if (!this.options.url) {
  	  this.options.url = url;
      var content= "<iframe frameborder=\"0\" name=\"" + this.getId() + "_content\"  id=\"" + this.getId() + "_content\" src=\"" + url + "\"> </iframe>";
      $('#'+this.getId() +"_table_content").html(content);
      
      this.content = $('#'+this.getId() + "_content");
    } else {
  	  this.options.url = url;
	    $('#'+this.getId() + '_content').src = url;
    }
  },

  getURL: function() {
  	return this.options.url ? this.options.url : null;
  },

  refresh: function() {
    if (this.options.url)
	    $('#'+this.getId() + '_content').src = this.options.url;
  },
  
  // Stores position/size in a cookie, by default named with window id
  setCookie: function(name, expires, path, domain, secure) {
    name = name || this.element.attr('id');
    this.cookie = [name, expires, path, domain, secure];
    
    // Get cookie
    var value = WindowUtilities.getCookie(name)
    // If exists
    if (value) {
      var values = value.split(',');
      var x = values[0].split(':');
      var y = values[1].split(':');

      var w = parseFloat(values[2]), h = parseFloat(values[3]);
      var mini = values[4];
      var maxi = values[5];

      this.setSize(w, h);
      if (mini == "true")
        this.doMinimize = true; // Minimize will be done at onload window event
      else if (maxi == "true")
        this.doMaximize = true; // Maximize will be done at onload window event

      this.useLeft = x[0] == "l";
      this.useTop = y[0] == "t";

      this.element.css(this.useLeft ? {left: x[1]} : {right: x[1]});
      this.element.css(this.useTop ? {top: y[1]} : {bottom: y[1]});
    }
  },
  
  // Gets window ID
  getId: function() {
    return this.element.attr('id');
  },
  
  // Detroys itself when closing 
  setDestroyOnClose: function() {
    this.options.destroyOnClose = true;
  },
  
  setConstraint: function(bool, padding) {
    this.constraint = bool;
    this.constraintPad = Object.extend(this.constraintPad, padding || {});
    // Reset location to apply constraint
    if (this.useTop && this.useLeft)
      this.setLocation(parseFloat(this.element.css('top')), parseFloat(this.element.css('left')));
  },
  
  // initDrag event

  _initDrag: function(event) {
    // No resize on minimized window
    if ($(event.target) == this.sizer && this.isMinimized())
      return;

    // No move on maximzed window
    if ($(event.target) != this.sizer && this.isMaximized())
      return;
      
    if (isIE && this.heightN == 0)
      this._getWindowBorderSize();
    
    // Get pointer X,Y
    this.pointer = [event.pageX, event.pageY];
    
    if (this.options.wiredDrag) 
      this.currentDrag = this._createWiredElement();
    else
      this.currentDrag = this.element;
      
    // Resize
    if ($(event.target) == this.sizer) {
      this.doResize = true;
      this.widthOrg = this.width;
      this.heightOrg = this.height;
      this.bottomOrg = parseFloat(this.element.css('bottom'));
      this.rightOrg = parseFloat(this.element.css('right'));
      this._notify("onStartResize");
    }
    else {
      this.doResize = false;

      // Check if click on close button, 
      var closeButton = $('#'+this.getId() + '_close');
      if (closeButton && Position.within(closeButton, this.pointer[0], this.pointer[1])) {
        this.currentDrag = null;
        return;
      }

      this.toFront();

      if (! this.options.draggable) 
        return;
      this._notify("onStartMove");
    }    
    // Register global event to capture mouseUp and mouseMove
    $(document).bind("mouseup", this.eventMouseUp);
    $(document).bind("mousemove", this.eventMouseMove);
    
    // Add an invisible div to keep catching mouse event over iframes
    WindowUtilities.disableScreen('__invisible__', '__invisible__');

    // Stop selection while dragging
    document.body.ondrag = function () { return false; };
    document.body.onselectstart = function () { return false; };
    
    this.currentDrag.show();
    event.stopPropagation();
  },

  // updateDrag event
  _updateDrag: function(event) {
    var pointer = [event.pageX, event.pageY];    
    var dx = pointer[0] - this.pointer[0];
    var dy = pointer[1] - this.pointer[1];
    
    // Resize case, update width/height
    if (this.doResize) {
      var w = this.widthOrg + dx;
      var h = this.heightOrg + dy;
      
      dx = this.width - this.widthOrg
      dy = this.height - this.heightOrg
      
      // Check if it's a right position, update it to keep upper-left corner at the same position
      if (this.useLeft) 
        w = this._updateWidthConstraint(w)
      else 
        this.currentDrag.css({right: (this.rightOrg -dx) + 'px'});
      // Check if it's a bottom position, update it to keep upper-left corner at the same position
      if (this.useTop) 
        h = this._updateHeightConstraint(h)
      else
        this.currentDrag.css({bottom: (this.bottomOrg -dy) + 'px'});
        
      this.setSize(w , h);
      this._notify("onResize");
    }
    // Move case, update top/left
    else {
      this.pointer = pointer;
      
      if (this.useLeft) {
        var left =  parseFloat(this.currentDrag.css('left')) + dx;
        var newLeft = this._updateLeftConstraint(left);
        // Keep mouse pointer correct
        this.pointer[0] += newLeft-left;
        this.currentDrag.css({left: newLeft + 'px'});
      }
      else 
        this.currentDrag.css({right: parseFloat(this.currentDrag.css('right')) - dx + 'px'});
      
      if (this.useTop) {
        var top =  parseFloat(this.currentDrag.css('top')) + dy;
        var newTop = this._updateTopConstraint(top);
        // Keep mouse pointer correct
        this.pointer[1] += newTop - top;
        this.currentDrag.css({top: newTop + 'px'});
      }
      else 
        this.currentDrag.css({bottom: parseFloat(this.currentDrag.css('bottom')) - dy + 'px'});

      this._notify("onMove");
    }
    if (this.iefix) 
      this._fixIEOverlapping(); 
      
    this._removeStoreLocation();
    event.stopPropagation();
  },

   // endDrag callback
   _endDrag: function(event) {
    // Remove temporary div over iframes
     WindowUtilities.enableScreen('__invisible__');
    
    if (this.doResize)
      this._notify("onEndResize");
    else
      this._notify("onEndMove");
    
    // Release event observing
    $(document).unbind("mouseup", this.eventMouseUp,false);
    $(document).unbind("mousemove", this.eventMouseMove, false);

    event.stopPropagation();
    
    this._hideWiredElement();

    // Store new location/size if need be
    this._saveCookie()
      
    // Restore selection
    document.body.ondrag = null;
    document.body.onselectstart = null;
  },

  _updateLeftConstraint: function(left) {
    if (this.constraint && this.useLeft && this.useTop) {
      var width = this.options.parent == document.body ? WindowUtilities.getPageSize().windowWidth : getDimensions(this.options.parent).width;

      if (left < this.constraintPad.left)
        left = this.constraintPad.left;
      if (left + this.width + this.widthE + this.widthW > width - this.constraintPad.right) 
        left = width - this.constraintPad.right - this.width - this.widthE - this.widthW;
    }
    return left;
  },
  
  _updateTopConstraint: function(top) {
    if (this.constraint && this.useLeft && this.useTop) {        
      var height = this.options.parent == document.body ? WindowUtilities.getPageSize().windowHeight : getDimensions(this.options.parent).height;
      
      var h = this.height + this.heightN + this.heightS;

      if (top < this.constraintPad.top)
        top = this.constraintPad.top;
      if (top + h > height - this.constraintPad.bottom) 
        top = height - this.constraintPad.bottom - h;
    }
    return top;
  },
  
  _updateWidthConstraint: function(w) {
    if (this.constraint && this.useLeft && this.useTop) {
      var width = this.options.parent == document.body ? WindowUtilities.getPageSize().windowWidth : getDimensions(this.options.parent).width;
      var left =  parseFloat(this.element.css("left"));

      if (left + w + this.widthE + this.widthW > width - this.constraintPad.right) 
        w = width - this.constraintPad.right - left - this.widthE - this.widthW;
    }
    return w;
  },
  
  _updateHeightConstraint: function(h) {
    if (this.constraint && this.useLeft && this.useTop) {
      var height = this.options.parent == document.body ? WindowUtilities.getPageSize().windowHeight : getDimensions(this.options.parent).height;
      var top =  parseFloat(this.element.css("top"));

      if (top + h + this.heightN + this.heightS > height - this.constraintPad.bottom) 
        h = height - this.constraintPad.bottom - top - this.heightN - this.heightS;
    }
    return h;
  },
  
  
  // Creates HTML window code
  _createWindow: function(id) {
    var className = this.options.className;
    var win = $(document.createElement("div"));
    win.attr('id', id);
    win.addClass("dialog");

    var content;
    if (this.options.url)
      content= "<iframe frameborder=\"0\" name=\"" + id + "_content\"  id=\"" + id + "_content\" src=\"" + this.options.url + "\"> </iframe>";
    else
      content ="<div id=\"" + id + "_content\" class=\"" +className + "_content\"> </div>";

    var closeDiv = this.options.closable ? "<div class='"+ className +"_close' id='"+ id +"_close' onclick='Windows.close(\""+ id +"\", event)'> </div>" : "";
    var minDiv = this.options.minimizable ? "<div class='"+ className + "_minimize' id='"+ id +"_minimize' onclick='Windows.minimize(\""+ id +"\", event)'> </div>" : "";
    var maxDiv = this.options.maximizable ? "<div class='"+ className + "_maximize' id='"+ id +"_maximize' onclick='Windows.maximize(\""+ id +"\", event)'> </div>" : "";
    var seAttributes = this.options.resizable ? "class='" + className + "_sizer' id='" + id + "_sizer'" : "class='"  + className + "_se'";
    var blank = "../themes/default/blank.gif";
    
    win.html(closeDiv + minDiv + maxDiv + "\
      <table id='"+ id +"_row1' class=\"top table_window\">\
        <tr>\
          <td class='"+ className +"_nw'></td>\
          <td class='"+ className +"_n'><div id='"+ id +"_top' class='"+ className +"_title title_window'>"+ this.options.title +"</div></td>\
          <td class='"+ className +"_ne'></td>\
        </tr>\
      </table>\
      <table id='"+ id +"_row2' class=\"mid table_window\">\
        <tr>\
          <td class='"+ className +"_w'></td>\
            <td id='"+ id +"_table_content' class='"+ className +"_content' valign='top'>" + content + "</td>\
          <td class='"+ className +"_e'></td>\
        </tr>\
      </table>\
        <table id='"+ id +"_row3' class=\"bot table_window\">\
        <tr>\
          <td class='"+ className +"_sw'></td>\
            <td class='"+ className +"_s'><div id='"+ id +"_bottom' class='status_bar'><span style='float:left; width:1px; height:1px'></span></div></td>\
            <td " + seAttributes + "></td>\
        </tr>\
      </table>\
    ");
    win.hide();
    win.insertBefore(this.options.parent.firstChild);
    $('#'+id + "_content").on("load", this.options.onload);
    return $(win);
  },
  
  
  changeClassName: function(newClassName) {
    var className = this.options.className;
    var id = this.getId();
    var win = this;
    $A(["_close","_minimize","_maximize","_sizer", "_content"]).each(function(value) { win._toggleClassName($('#'+id + value), className + value, newClassName + value) });
    $("#" + id + " td").each(function(eix,td) {td[0].className = td[0].className.sub(className,newClassName) });
    this.options.className = newClassName;
  },
  
  _toggleClassName: function(element, oldClassName, newClassName) {
    if (element) {
      element.removeClass(oldClassName);
      element.addClass(newClassName);
    }
  },
  
  // Sets window location
  setLocation: function(top, left) {
    top = this._updateTopConstraint(top);
    left = this._updateLeftConstraint(left);
    
    // by BK
    Position.prepare();
    top += Position.deltaY;
    left += Position.deltaX;
    // end by BK

    this.element.css({top: top + 'px'});
    this.element.css({left: left + 'px'});

    this.useLeft = true;
    this.useTop = true;
  },
    
  getLocation: function() {
    var location = {};
    if (this.useTop)
      location = Object.extend(location, {top: this.element.css("top")});
    else
      location = Object.extend(location, {bottom: this.element.css("bottom")});
    if (this.useLeft)
      location = Object.extend(location, {left: this.element.css("left")});
    else
      location = Object.extend(location, {right: this.element.css("right")});
    
    return location;
  },
  
  // Gets window size
  getSize: function() {
    return {width: this.width, height: this.height};
  },
    
  // Sets window size
  setSize: function(width, height) {    
    width = parseFloat(width);
    height = parseFloat(height);
    
    // Check min and max size
    if (width < this.options.minWidth)
      width = this.options.minWidth;

    if (height < this.options.minHeight)
      height = this.options.minHeight;
      
    if (this.options. maxHeight && height > this.options. maxHeight)
      height = this.options. maxHeight;

    if (this.options. maxWidth && width > this.options. maxWidth)
      width = this.options. maxWidth;

    this.width = width;
    this.height = height;
    var e = this.currentDrag ? this.currentDrag : this.element;
    e.css({width: width + this.widthW + this.widthE + "px"})
    e.css({height: height  + this.heightN + this.heightS + "px"})

    // Update content height
    if (!this.currentDrag || this.currentDrag == this.element) {
      var content = $('#'+this.getId() + '_content');
      content.css({height: height  + 'px'});
      content.css({width: width  + 'px'});
    }
  },
  
  updateHeight: function() {
    this.setSize(this.width, this.content.scrollHeight)
  },
  
  updateWidth: function() {
    this.setSize(this.content.scrollWidth, this.height)
  },
  
  // Brings window to front
  toFront: function() {
    if (Windows.focusedWindow == this) 
      return;
    this.setZIndex(Windows.maxZIndex + 20);
    this._notify("onFocus");
    if (this.iefix) 
      this._fixIEOverlapping(); 
  },
  
  // Displays window modal state or not
  show: function(modal) {
    if (modal) {
      Windows.addModalWindow(this);
      
      this.modal = true;      
      this.setZIndex(Windows.maxZIndex + 20);
      Windows.unsetOverflow(this);
    }
    
    // To restore overflow if need be
    if (this.oldStyle)
      this.getContent().css({overflow: this.oldStyle});
      
    if (! this.width || !this.height) {
      var size = WindowUtilities._computeSize(this.content.html(), this.content.attr('id'), this.width, this.height, 0, this.options.className)
      if (this.height)
        this.width = size + 5
      else
        this.height = size + 5
    }

    this.setSize(this.width, this.height);
    if (this.centered)
      this._center(this.centerTop, this.centerLeft);    
    
    this._notify("onBeforeShow");   
    this.element.show();  
      
    this._checkIEOverlapping();
    this.visible = true;
    WindowUtilities.focusedWindow = this
    this._notify("onShow");   
  },
  
  // Displays window modal state or not at the center of the page
  showCenter: function(modal, top, left) {
    this.centered = true;
    this.centerTop = top;
    this.centerLeft = left;

    this.show(modal);
  },
  
  isVisible: function() {
    return this.visible;
  },
  
  _center: function(top, left) {
    var windowScroll = WindowUtilities.getWindowScroll();    
    var pageSize = WindowUtilities.getPageSize();    

    if (!top)
      top = (pageSize.windowHeight - (this.height + this.heightN + this.heightS))/2;
    top += windowScroll.top
    
    if (!left)
      left = (pageSize.windowWidth - (this.width + this.widthW + this.widthE))/2;
    left += windowScroll.left 
    
    this.setLocation(top, left);
    this.toFront();
  },
  
  _recenter: function(event) {
    if (this.modal && this.centered) {
      var pageSize = WindowUtilities.getPageSize();
      // Check for this stupid IE that sends dumb events
      if (this.pageSize && this.pageSize.pageWidth == pageSize.windowWidth && this.pageSize.pageHeight == pageSize.windowHeight) 
        return;
      
      this.pageSize = pageSize;
      // set height of Overlay to take up whole page and show
      if ($('#overlay_modal')) {
        $('#overlay_modal').css({height:(pageSize.pageHeight + 'px'), width: (pageSize.pageWidth + 'px')});
      }    
      if (this.options.recenterAuto)
        this._center(this.centerTop, this.centerLeft);    
    }
  },
  
  // Hides window
  hide: function() {
    this.visible = false;
    if (this.modal) {
      Windows.removeModalWindow(this);
      Windows.resetOverflow();
    }
    // To avoid bug on scrolling bar
    this.oldStyle = this.getContent().css('overflow') || "auto"
    this.getContent().css({overflow: "hidden"});

    this.element.hide();  

     if(this.iefix) 
      this.iefix.hide();

    if (!this.doNotNotifyHide)
      this._notify("onHide");
  },

  close: function() {
    // Asks closeCallback if exists
    if (this.visible) {
      if (this.options.closeCallback && ! this.options.closeCallback(this)) 
        return;

      Windows.updateFocusedWindow();
      
      this.doNotNotifyHide = true;
      this.hide();
      this.doNotNotifyHide = false;
      this._notify("onClose");
    }
  },
  
  minimize: function() {
    var r2 = $('#'+this.getId() + "_row2");
    var dh = getDimensions(r2).height;
    
    if (r2.visible()) {
      var h  = this.element.getHeight() - dh;
      this.height -= dh;

      r2.hide()
      this.element.css({height: h + "px"})
      if (! this.useTop) {
        var bottom = parseFloat(this.element.css('bottom'));
        this.element.css({bottom: (bottom + dh) + 'px'});
      }
    } 
    else {      
      var h  = this.element.getHeight() + dh;
      this.height += dh;
      
      this.element.css({height: h + "px"})
      if (! this.useTop) {
        var bottom = parseFloat(this.element.css('bottom'));
        this.element.css({bottom: (bottom - dh) + 'px'});
      }
      r2.show();
      this.toFront();
    }
    this._notify("onMinimize");
    
    // Store new location/size if need be
    this._saveCookie()
  },
  
  maximize: function() {
    if (this.isMinimized())
      return;
  
    if (isIE && this.heightN == 0)
      this._getWindowBorderSize();
      
    if (this.storedLocation != null) {
      this._restoreLocation();
      if(this.iefix) 
        this.iefix.hide();
    }
    else {
      this._storeLocation();
      Windows.unsetOverflow(this);
      
      var windowScroll = WindowUtilities.getWindowScroll();
      var pageSize = WindowUtilities.getPageSize();    
      var left = windowScroll.left;
      var top = windowScroll.top;
      
      if (this.options.parent != document.body) {
        windowScroll =  {top:0, left:0, bottom:0, right:0};
        var dim = getDimensions(this.options.parent);
        pageSize.windowWidth = dim.width;
        pageSize.windowHeight = dim.height;
        top = 0; 
        left = 0;
      }
      
      if (this.constraint) {
        pageSize.windowWidth -= Math.max(0, this.constraintPad.left) + Math.max(0, this.constraintPad.right);
        pageSize.windowHeight -= Math.max(0, this.constraintPad.top) + Math.max(0, this.constraintPad.bottom);
        left +=  Math.max(0, this.constraintPad.left);
        top +=  Math.max(0, this.constraintPad.top);
      }
      
      this.element.css(this.useLeft ? {left: left} : {right: left});
      this.element.css(this.useTop ? {top: top} : {bottom: top});

      this.setSize(pageSize.windowWidth - this.widthW - this.widthE , pageSize.windowHeight - this.heightN - this.heightS)
      this.toFront();
      if (this.iefix) 
        this._fixIEOverlapping(); 
    }
    this._notify("onMaximize");

    // Store new location/size if need be
    this._saveCookie()
  },
  
  isMinimized: function() {
    var r2 = $('#'+this.getId() + "_row2");
    return !r2.is(':visible');
  },
  
  isMaximized: function() {
    return (this.storedLocation != null);
  },
  
  setOpacity: function(opacity) {
      this.element.css('opacity', opacity);
  },
  
  setZIndex: function(zindex) {
    this.element.css({zIndex: zindex});
    Windows.updateZindex(zindex, this);
  },

  setTitle: function(newTitle) {
    if (!newTitle || newTitle == "") 
      newTitle = "&nbsp;";
      
    $('#'+this.getId() + '_top').html(newTitle);
  },

  setStatusBar: function(element) {
    var statusBar = $('#'+this.getId() + "_bottom");

    if (typeof(element) == "object") {
      if (this.bottombar.firstChild)
        this.bottombar.replaceChild(element, this.bottombar.firstChild);
      else
        this.bottombar.appendChild(element);
    }
    else
      this.bottombar.html(element);
  },

  _checkIEOverlapping: function() {
    if(!this.iefix && (navigator.appVersion.indexOf('MSIE')>0) && (navigator.userAgent.indexOf('Opera')<0) && (this.element.css('position')=='absolute')) {
        $('#'+this.getId()).append('<iframe id="' + this.getId() + '_iefix" '+ 'style="display:none;position:absolute;filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" ' + 'src="javascript:false;" frameborder="0" scrolling="no"></iframe>');
        this.iefix = $('#'+this.getId()+'_iefix');
    }
    if(this.iefix) 
      setTimeout(this._fixIEOverlapping.bind(this), 50);
  },

  _fixIEOverlapping: function() {
      Position.clone(this.element, this.iefix);
      this.iefix.css('z-index',this.element.css('z-index') - 1);
      this.iefix.show();
  },
  
  _getWindowBorderSize: function(event) {
    // Hack to get real window border size!!
    var div = this._createHiddenDiv(this.options.className + "_n")
    this.heightN = getDimensions(div).height;    
    $(div).remove();

    var div = this._createHiddenDiv(this.options.className + "_s")
    this.heightS = getDimensions(div).height;    
    $(div).remove();

    var div = this._createHiddenDiv(this.options.className + "_e")
    this.widthE = getDimensions(div).width;    
    $(div).remove();

    var div = this._createHiddenDiv(this.options.className + "_w")
    this.widthW = getDimensions(div).width;
    $(div).remove();

    // Workaround for IE!!
    if (isIE) {
      this.heightS = getDimensions($('#'+this.getId() +"_row3")).height;
      this.heightN = getDimensions($('#'+this.getId() +"_row1")).height;
    }

    // Safari size fix
    if (/Konqueror|Safari|KHTML/.test(navigator.userAgent))
      this.setSize(this.width, this.height);
    if (this.doMaximize)
      this.maximize();
    if (this.doMinimize)
      this.minimize();
  },
 
  _createHiddenDiv: function(className) {
    var objBody = document.body;
    var win = $(document.createElement("div"));
    win.attr('id', this.element.attr('id')+ "_tmp");
    win.addClass(className);
    win.hide();
    win.html('');
    win.insertBefore(objBody.firstChild);
    return win;
  },
  
  _storeLocation: function() {
    if (this.storedLocation == null) {
      this.storedLocation = {useTop: this.useTop, useLeft: this.useLeft, 
                             top: this.element.css('top'), bottom: this.element.css('bottom'),
                             left: this.element.css('left'), right: this.element.css('right'),
                             width: this.width, height: this.height };
    }
  },
  
  _restoreLocation: function() {
    if (this.storedLocation != null) {
      this.useLeft = this.storedLocation.useLeft;
      this.useTop = this.storedLocation.useTop;
      
      this.element.css(this.useLeft ? {left: this.storedLocation.left} : {right: this.storedLocation.right});
      this.element.css(this.useTop ? {top: this.storedLocation.top} : {bottom: this.storedLocation.bottom});
      this.setSize(this.storedLocation.width, this.storedLocation.height);
      
      Windows.resetOverflow();
      this._removeStoreLocation();
    }
  },
  
  _removeStoreLocation: function() {
    this.storedLocation = null;
  },
  
  _saveCookie: function() {
    if (this.cookie) {
      var value = "";
      if (this.useLeft)
        value += "l:" +  (this.storedLocation ? this.storedLocation.left : this.element.css('left'))
      else
        value += "r:" + (this.storedLocation ? this.storedLocation.right : this.element.css('right'))
      if (this.useTop)
        value += ",t:" + (this.storedLocation ? this.storedLocation.top : this.element.css('top'))
      else
        value += ",b:" + (this.storedLocation ? this.storedLocation.bottom :this.element.css('bottom'))
        
      value += "," + (this.storedLocation ? this.storedLocation.width : this.width);
      value += "," + (this.storedLocation ? this.storedLocation.height : this.height);
      value += "," + this.isMinimized();
      value += "," + this.isMaximized();
      WindowUtilities.setCookie(value, this.cookie)
    }
  },
  
  _createWiredElement: function() {
    if (! this.wiredElement) {
      if (isIE)
        this._getWindowBorderSize();
      var div = $(document.createElement("div"));
      div.addClass("wired_frame " + this.options.className + "_wired_frame");
      
      div.css('position','absolute');
      div.insertBefore(this.options.parent.firstChild);
      this.wiredElement = $(div);
    }
    if (this.useLeft) 
      this.wiredElement.css({left: this.element.css('left')});
    else 
      this.wiredElement.css({right: this.element.css('right')});
      
    if (this.useTop) 
      this.wiredElement.css({top: this.element.css('top')});
    else 
      this.wiredElement.css({bottom: this.element.css('bottom')});

    var dim = getDimensions(this.element);
    this.wiredElement.css({width: dim.width + "px", height: dim.height +"px"});

    this.wiredElement.css({zIndex: Windows.maxZIndex+30});
    return this.wiredElement;
  },
  
  _hideWiredElement: function() {
    if (! this.wiredElement || ! this.currentDrag)
      return;
    if (this.currentDrag == this.element) 
      this.currentDrag = null;
    else {
      if (this.useLeft) 
        this.element.css({left: this.currentDrag.css('left')});
      else 
        this.element.css({right: this.currentDrag.css('right')});

      if (this.useTop) 
        this.element.css({top: this.currentDrag.css('top')});
      else 
        this.element.css({bottom: this.currentDrag.css('bottom')});

      this.currentDrag.hide();
      this.currentDrag = null;
      if (this.doResize)
        this.setSize(this.width, this.height);
    } 
  },
  
  _notify: function(eventName) {
    if (this.options[eventName])
      this.options[eventName](this);
    else
      Windows.notify(eventName, this);
  }
};

// Windows containers, register all page windows
var Windows = {
  windows: [],
  modalWindows: [],
  observers: [],
  focusedWindow: null,
  maxZIndex: 0,

  addObserver: function(observer) {
    this.removeObserver(observer);
    this.observers.push(observer);
  },
  
  removeObserver: function(observer) {  
    this.observers = this.observers.reject( function(o) { return o==observer });
  },
  
  //  onDestroy onStartResize onStartMove onResize onMove onEndResize onEndMove onFocus onBeforeShow onShow onHide onMinimize onMaximize onClose
  notify: function(eventName, win) {  
    this.observers.each( function(o) {if(o[eventName]) o[eventName](eventName, win);});
  },

  // Gets window from its id
  getWindow: function(id) {
    return this.windows.detect(function(d) { return d.getId() ==id });
  },

  // Gets the last focused window
  getFocusedWindow: function() {
    return this.focusedWindow;
  },

  updateFocusedWindow: function() {
    this.focusedWindow = this.windows.length >=2 ? this.windows[this.windows.length-2] : null;    
  },
  
  getLastWindowByCTagName: function(cTagName){
	for(var i=this.windows.length-1; i>=0; i--)
		if(this.windows[i].cTagName && this.windows[i].cTagName==cTagName)
			return this.windows[i];
	return null;
  },
  
  // Registers a new window (called by Windows constructor)
  register: function(win) {
    this.windows.push(win);
  },
    
  // Add a modal window in the stack
  addModalWindow: function(win) {
    // Disable screen if first modal window
    if (this.modalWindows.length == 0)
      WindowUtilities.disableScreen(win.options.className, 'overlay_modal', win.getId());
    else {
      // Move overlay over all windows
      if (Window.keepMultiModalWindow) {
        $('#overlay_modal').css('z-index',Windows.maxZIndex + 20);
        Windows.maxZIndex += 20;
        WindowUtilities._hideSelect(this.modalWindows.last().getId());
      }
      // Hide current modal window
      else
        this.modalWindows.last().element.hide();
      // Fucking IE select issue
      WindowUtilities._showSelect(win.getId());
    }      
    this.modalWindows.push(win);    
  },
  
  removeModalWindow: function(win) {
    this.modalWindows.pop();
    
    // No more modal windows
    if (this.modalWindows.length == 0)
      WindowUtilities.enableScreen();     
    else {
      if (Window.keepMultiModalWindow) {
        this.modalWindows.last().toFront();
        WindowUtilities._showSelect(this.modalWindows.last().getId());        
      }
      else
        this.modalWindows.last().element.show();
    }
  },
  
  // Registers a new window (called by Windows constructor)
  register: function(win) {
    this.windows.push(win);
  },
  
  // Unregisters a window (called by Windows destructor)
  unregister: function(win) {
    this.windows = this.windows.reject(function(d) { return d==win });
  }, 
  
  // Closes all windows
  closeAll: function() {  
    this.windows.each( function(w) {Windows.close(w.getId())} );
  },
  
  closeAllModalWindows: function() {
    WindowUtilities.enableScreen();     
    this.modalWindows.each( function(win) {win.hide()});    
  },

  // Minimizes a window with its id
  minimize: function(id, event) {
    var win = this.getWindow(id)
    if (win && win.visible)
      win.minimize();
    event.stopPropagation();
  },
  
  // Maximizes a window with its id
  maximize: function(id, event) {
    var win = this.getWindow(id)
    if (win && win.visible)
      win.maximize();
    event.stopPropagation();
  },

  // Closes a window with its id
  close: function(id, event) {
    var win = this.getWindow(id);
    if (win) 
      win.close()//win.options.destroyOnClose ? win.close() : win.hide();
    if (event)
      event.stopPropagation();
  },
  
  unsetOverflow: function(except) {    
    this.windows.each(function(d) { d.oldOverflow = d.getContent().css("overflow") || "auto" ; d.getContent().css({overflow: "hidden"}) });
    if (except && except.oldOverflow)
      except.getContent().css({overflow: except.oldOverflow});
  },

  resetOverflow: function() {
    this.windows.each(function(d) { if (d.oldOverflow) d.getContent().css({overflow: d.oldOverflow}) });
  },

  updateZindex: function(zindex, win) {
    if (zindex > this.maxZIndex)
      this.maxZIndex = zindex;
    //TODO: işte tam burada win'i spread diğerlerini alphacube yapabiliriz.
    //if(this.focusedWindow) this.focusedWindow.setClassName(WindowUtilities.className);
    //win.setClassName(WindowUtilities.activeClassName);
    this.focusedWindow = win;
  }
};

var Dialog = {
  dialogId: null,
  onCompleteFunc: null,
  callFunc: null, 
  parameters: null, 
    
  confirm: function(content, parameters) {
    // Get Ajax return before
    if (content && typeof content != "string") {
      Dialog._runAjaxRequest(content, parameters, Dialog.confirm);
      return 
    }
    content = content || "";
    
    parameters = parameters || {};
    var okLabel = parameters.okLabel ? parameters.okLabel : "Ok";
    var cancelLabel = parameters.cancelLabel ? parameters.cancelLabel : "Cancel";

    // Backward compatibility
    parameters = Object.extend(parameters, parameters.windowParameters || {});
    parameters.windowParameters = parameters.windowParameters || {};

    parameters.className = parameters.className || "alert";

    var okButtonClass = "class ='" + (parameters.buttonClass ? parameters.buttonClass + " " : "") + " ok_button'" 
    var cancelButtonClass = "class ='" + (parameters.buttonClass ? parameters.buttonClass + " " : "") + " cancel_button'" 
    var content = "\
      <div class='" + parameters.className + "_message'>" + content  + "</div>\
        <div class='" + parameters.className + "_buttons'>\
          <input type='button' value='" + okLabel + "' onclick='Dialog.okCallback()' " + okButtonClass + "/>\
          <input type='button' value='" + cancelLabel + "' onclick='Dialog.cancelCallback()' " + cancelButtonClass + "/>\
        </div>\
    ";
    return this._openDialog(content, parameters)
  },
  
  alert: function(content, parameters) {
    // Get Ajax return before
    if (content && typeof content != "string") {
      Dialog._runAjaxRequest(content, parameters, Dialog.alert);
      return 
    }
    content = content || "";
    
    parameters = parameters || {};
    var okLabel = parameters.okLabel ? parameters.okLabel : "Ok";

    // Backward compatibility
    parameters = Object.extend(parameters, parameters.windowParameters || {});
    parameters.windowParameters = parameters.windowParameters || {};
    
    parameters.className = parameters.className || "alert";
    
    var okButtonClass = "class ='" + (parameters.buttonClass ? parameters.buttonClass + " " : "") + " ok_button'" 
    var content = "\
      <div class='" + parameters.className + "_message'>" + content  + "</div>\
        <div class='" + parameters.className + "_buttons'>\
          <input type='button' value='" + okLabel + "' onclick='Dialog.okCallback()' " + okButtonClass + "/>\
        </div>";
    return this._openDialog(content, parameters)
  },
  
  info: function(content, parameters) {   
    // Get Ajax return before
    if (content && typeof content != "string") {
      Dialog._runAjaxRequest(content, parameters, Dialog.info);
      return 
    }
    content = content || "";
     
    // Backward compatibility
    parameters = parameters || {};
    parameters = Object.extend(parameters, parameters.windowParameters || {});
    parameters.windowParameters = parameters.windowParameters || {};
    
    parameters.className = parameters.className || "alert";
    
    var content = "<div id='modal_dialog_message' class='" + parameters.className + "_message'>" + content  + "</div>";
    if (parameters.showProgress)
      content += "<div id='modal_dialog_progress' class='" + parameters.className + "_progress'>  </div>";

    parameters.ok = null;
    parameters.cancel = null;
    
    return this._openDialog(content, parameters)
  },
  
  setInfoMessage: function(message) {
    $('#modal_dialog_message').update(message);
  },
  
  closeInfo: function() {
    Windows.close(this.dialogId);
  },
  
  _openDialog: function(content, parameters) {
    var className = parameters.className;
    
    if (! parameters.height && ! parameters.width) {
      parameters.width = WindowUtilities.getPageSize().pageWidth / 2;
    }
    if (parameters.id)
      this.dialogId = parameters.id;
    else { 
      var t = new Date();
      this.dialogId = 'modal_dialog_' + t.getTime();
      parameters.id = this.dialogId;
    }

    // compute height or width if need be
    if (! parameters.height || ! parameters.width) {
      var size = WindowUtilities._computeSize(content, this.dialogId, parameters.width, parameters.height, 5, className)
      if (parameters.height)
        parameters.width = size + 5
      else
        parameters.height = size + 5
    }
    //parameters.resizable = parameters.resizable || false;
    
    parameters.effectOptions = parameters.effectOptions || {duration: 1};
    parameters.minimizable = false;
    parameters.maximizable = false;
    //parameters.draggable = false;
    parameters.closable = false;
    
    var win = new Window(parameters);
    win.getContent().html(content);
    win.showCenter(true, parameters.top, parameters.left);  
    win.setDestroyOnClose();
    
    win.cancelCallback = parameters.onCancel || parameters.cancel; 
    win.okCallback = parameters.onOk || parameters.ok;
    
    return win;    
  },
  
  _getAjaxContent: function(originalRequest)  {
      Dialog.callFunc(originalRequest.responseText, Dialog.parameters)
  },
  
  _runAjaxRequest: function(message, parameters, callFunc) {
    if (message.options == null)
      message.options = {}  
    Dialog.onCompleteFunc = message.options.onComplete;
    Dialog.parameters = parameters;
    Dialog.callFunc = callFunc;
    
    message.options.onComplete = Dialog._getAjaxContent;
    new Ajax.Request(message.url, message.options);
  },
  
  okCallback: function() {
    var win = Windows.focusedWindow;
    if (!win.okCallback || win.okCallback(win)) {
      // Remove onclick on button
      $("#" + win.getId()+" input").each(function(eix,element) {element.onclick=null;})
      win.close();
    }
  },

  cancelCallback: function() {
    var win = Windows.focusedWindow;
    // Remove onclick on button
    $("#" + win.getId()+" input").each(function(eix,element) {element.onclick=null})
    win.close();
    if (win.cancelCallback)
      win.cancelCallback(win);
  }
}
/*
  Based on Lightbox JS: Fullsize Image Overlays 
  by Lokesh Dhakar - http://www.huddletogether.com

  For more information on this script, visit:
  http://huddletogether.com/projects/lightbox/

  Licensed under the Creative Commons Attribution 2.5 License - http://creativecommons.org/licenses/by/2.5/
  (basically, do anything you want, just leave my name and link)
*/

var isIE = navigator.appVersion.match(/MSIE/) == "MSIE";

var WindowUtilities = {
  //className: 'alphacube',
  //activeClassName: 'spread',
  // From script.aculo.us
  getWindowScroll: function() {
    var w = window;
      var T, L, W, H;
      with (w.document) {
        if (w.document.documentElement && typeof documentElement.scrollTop != "undefined") {
          T = documentElement.scrollTop;
          L = documentElement.scrollLeft;
        } else if (w.document.body) {
          T = body.scrollTop;
          L = body.scrollLeft;
        }
        if (w.innerWidth) {
          W = w.innerWidth;
          H = w.innerHeight;
        } else if (w.document.documentElement && typeof documentElement.clientWidth != "undefined") {
          W = documentElement.clientWidth;
          H = documentElement.clientHeight;
        } else {
          W = body.offsetWidth;
          H = body.offsetHeight
        }
      }
      return { top: T, left: L, width: W, height: H };
  }, 
  //
  // getPageSize()
  // Returns array with page width, height and window width, height
  // Core code from - quirksmode.org
  // Edit for Firefox by pHaez
  //
  getPageSize: function(){
    var xScroll, yScroll;

    if (window.innerHeight && window.scrollMaxY) {  
      xScroll = document.body.scrollWidth;
      yScroll = window.innerHeight + window.scrollMaxY;
    } else if (document.body.scrollHeight > document.body.offsetHeight){ // all but Explorer Mac
      xScroll = document.body.scrollWidth;
      yScroll = document.body.scrollHeight;
    } else { // Explorer Mac...would also work in Explorer 6 Strict, Mozilla and Safari
      xScroll = document.body.offsetWidth;
      yScroll = document.body.offsetHeight;
    }

    var windowWidth, windowHeight;

    if (self.innerHeight) {  // all except Explorer
      windowWidth = self.innerWidth;
      windowHeight = self.innerHeight;
    } else if (document.documentElement && document.documentElement.clientHeight) { // Explorer 6 Strict Mode
      windowWidth = document.documentElement.clientWidth;
      windowHeight = document.documentElement.clientHeight;
    } else if (document.body) { // other Explorers
      windowWidth = document.body.clientWidth;
      windowHeight = document.body.clientHeight;
    }  
    var pageHeight, pageWidth;

    // for small pages with total height less then height of the viewport
    if(yScroll < windowHeight){
      pageHeight = windowHeight;
    } else { 
      pageHeight = yScroll;
    }

    // for small pages with total width less then width of the viewport
    if(xScroll < windowWidth){  
      pageWidth = windowWidth;
    } else {
      pageWidth = xScroll;
    }

    return {pageWidth: pageWidth ,pageHeight: pageHeight , windowWidth: windowWidth, windowHeight: windowHeight};
  },

   disableScreen: function(className, overlayId, contentId) {
    WindowUtilities.initLightbox(overlayId, className);
    var objBody = document.body;

    // prep objects
     var objOverlay = $('#'+overlayId);

    var pageSize = WindowUtilities.getPageSize();

    // Hide select boxes as they will 'peek' through the image in IE, store old value
    if (contentId && isIE) {
      WindowUtilities._hideSelect();
      WindowUtilities._showSelect(contentId);
    }  
  
    // set height of Overlay to take up whole page and show
    objOverlay.css({height:(pageSize.pageHeight + 'px'), width: (pageSize.windowWidth + 'px'), display: 'block'});  
  },

   enableScreen: function(id) {
     id = id || 'overlay_modal';
     var objOverlay =  $('#'+id);
    if (objOverlay) {
      // hide lightbox and overlay
      objOverlay.hide();

      // make select boxes visible using old value
      if (id != "__invisible__") 
        WindowUtilities._showSelect();
      
      objOverlay.remove();//parentNode.removeChild(objOverlay);
    }
  },

  _hideSelect: function(id) {
    if (isIE) {
      id = id ==  null ? "" : "#" + id + " ";
      $(id + 'select').each(function(eix,element) {
        if (! WindowUtilities.isDefined(element.oldVisibility)) {
          element.oldVisibility = $(element).css('visibility') ? $(element).css('visibility') : "visible";
          $(element).css('visibility', "hidden");
        }
      });
    }
  },
  
  _showSelect: function(id) {
    if (isIE) {
      id = id ==  null ? "" : "#" + id + " ";
      $(id + 'select').each(function(eix,element) {
        if (WindowUtilities.isDefined(element.oldVisibility)) {
          // Why?? Ask IE
          try {
            $(element).css('visibility', element.oldVisibility);
          } catch(e) {
            $(element).css('visibility', "visible");
          }
          element.oldVisibility = null;
        }
        else {
          if ($(element).css('visibility'))
            $(element).css('visibility', "visible");
        }
      });
    }
  },

  isDefined: function(object) {
    return typeof(object) != "undefined" && object != null;
  },
  
  // initLightbox()
  // Function runs on window load, going through link tags looking for rel="lightbox".
  // These links receive onclick events that enable the lightbox display for their targets.
  // The function also inserts html markup at the top of the page which will be used as a
  // container for the overlay pattern and the inline image.
  initLightbox: function(id, className) {
    // Already done, just update zIndex
    if ($('#'+id).length) {
      $('#'+id).css({zIndex: Windows.maxZIndex + 10});
    }
    // create overlay div and hardcode some functional styles (aesthetic styles are in CSS file)
    else {
      var objBody = document.body;
      var objOverlay = $(document.createElement("div"));
      objOverlay.attr('id', id);
      objOverlay.addClass("overlay_" + className);
      objOverlay.css({
		  display: 'none',
		  position: 'absolute',
		  top: '0',
		  left: '0',
		  zIndex: Windows.maxZIndex + 10,
		  width: '100%'});
      objOverlay.insertBefore(objBody.firstChild);
    }
  },
  
  setCookie: function(value, parameters) {
    document.cookie= parameters[0] + "=" + escape(value) +
      ((parameters[1]) ? "; expires=" + parameters[1].toGMTString() : "") +
      ((parameters[2]) ? "; path=" + parameters[2] : "") +
      ((parameters[3]) ? "; domain=" + parameters[3] : "") +
      ((parameters[4]) ? "; secure" : "");
  },

  getCookie: function(name) {
    var dc = document.cookie;
    var prefix = name + "=";
    var begin = dc.indexOf("; " + prefix);
    if (begin == -1) {
      begin = dc.indexOf(prefix);
      if (begin != 0) return null;
    } else {
      begin += 2;
    }
    var end = document.cookie.indexOf(";", begin);
    if (end == -1) {
      end = dc.length;
    }
    return unescape(dc.substring(begin + prefix.length, end));
  },
    
  _computeSize: function(content, id, width, height, margin, className) {
    var objBody = document.body;
    var tmpObj = $(document.createElement("div"));
    tmpObj.attr('id', id);
    tmpObj.addClass(className + "_content");

    if (height)
      tmpObj.css('height',height + "px");
    else
      tmpObj.css('width', width + "px");
  
    tmpObj.css({
       position:'absolute',
       top:'0',
       left:'0',
       display:'none'
	});

    tmpObj.html(content);
    tmpObj.insertBefore(objBody.firstChild);
    
    var size;
    if (height)
      size = getDimensions($('#'+id)).width + margin;
    else
      size = getDimensions($('#'+id)).height + margin;
    tmpObj.remove();
    return size;
  }  
}

