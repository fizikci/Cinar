using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace Ext
{
    public class Action
    {
        public bool? disabled;
        [Description("The function that will be invoked by each component tied to this action when the component's primary event is trigge...")]
        public Action handler;
        public bool? hidden;
        [Description("The CSS class selector that specifies a background image to be used as the header icon for all components using this...")]
        public string iconCls;
        public string itemId;
        public json scope;
        public string text;
    }
    public class Component : util.Observable
    {
        public bool? allowDomMove;
        [Description("Specify the id of the element, a DOM element or an existing Element corresponding to a DIV that is already present in...")]
        public json applyTo;
        [Description("A tag name or DomHelper spec used to create the Element which will encapsulate this Component. You do not normally ne...")]
        public json autoEl;
        [Description("True if the component should check for hidden classes (e.g. 'x-hidden' or 'x-hide-display') and remove them on render...")]
        public bool? autoShow;
        [Description("The CSS class used to to apply to the special clearing div rendered directly after each form field wrapper to provide...")]
        public string clearCls;
        [Description("An optional extra CSS class that will be added to this component's Element (defaults to ''). This can be useful for ...")]
        public string cls;
        [Description("Optional. Specify an existing HTML element, or the id of an existing HTML element to use as the content for this comp...")]
        public string contentEl;
        [Description("An optional extra CSS class that will be added to this component's container. This can be useful for adding customize...")]
        public string ctCls;
        public json data;
        public bool? disabled;
        public string disabledClass;
        [Description("The label text to display next to this Component (defaults to ''). Note: this config is only used when this Component...")]
        public string fieldLabel;
        public bool? hidden;
        [Description("true to completely hide the label element (label and separator). Defaults to false. By default, even if you do not sp...")]
        public bool? hideLabel;
        [Description("How this component should be hidden. Supported values are 'visibility' (css visibility), 'offsets' (negative offset p...")]
        public string hideMode;
        [Description("True to hide and show the component's container when hide/show is called on the component, false to hide and show the...")]
        public bool? hideParent;
        [Description("An HTML fragment, or a DomHelper specification to use as the layout element content (defaults to ''). The HTML conten...")]
        public string html;
        [Description("The unique id of this component (defaults to an auto-assigned id). You should assign an id if you need to be able to ...")]
        public string id;
        [Description("Note: this config is only used when this Component is rendered by a Container which has been configured to use the Fo...")]
        public string itemCls;
        [Description("An itemId can be used as an alternative way to get a reference to a component when no json reference is available. ...")]
        public string itemId;
        [Description("The separator to display after the text of each fieldLabel. This property may be configured at various levels. The o...")]
        public string labelSeparator;
        [Description("A CSS style specification string to apply directly to this field's label. Defaults to the container's labelStyle val...")]
        public string labelStyle;
        [Description("An optional extra CSS class that will be added to this component's Element when the mouse moves over the Element, and...")]
        public string overCls;
        [Description("An json or array of objects that will provide custom functionality for this component. The only requirement for a ...")]
        public List<string> plugins;
        [Description("The registered ptype to create. This config option is not used when passing a config json into a constructor. This ...")]
        public string ptype;
        [Description("A path specification, relative to the Component's ownerCt specifying into which ancestor Container to place a named r...")]
        public string _ref;
        [Description("Specify the id of the element, a DOM element or an existing Element that this component will be rendered into. Notes ...")]
        public json renderTo;
        [Description("An array of events that, when fired, should trigger this component to save its state (defaults to none). stateEvents ...")]
        public IList stateEvents;
        [Description("The unique id for this component to use for state management purposes (defaults to the component id if one was set, o...")]
        public string stateId;
        [Description("A flag which causes the Component to attempt to restore the state of internal properties from a saved state on startu...")]
        public bool? stateful;
        [Description("A custom style specification to be applied to this component's Element. Should be a valid argument to Ext.Element.ap...")]
        public string style;
        [Description("An Ext.Template, Ext.XTemplate or an array of strings to form an Ext.XTemplate. Used in conjunction with the data and...")]
        public json tpl;
        [Description("The Ext.(X)Template method to use when updating the content area of the Component. Defaults to 'overwrite' (see Ext.X...")]
        public string tplWriteMode;
        [Description("The registered xtype to create. This config option is not used when passing a config json into a constructor. This ...")]
        public string xtype;
    }
    public class Fx
    {
        public string afterCls;
        [Description("A style specification string, e.g. \"width:100px\", or an json in the form {width:\"100px\"}, or a function which retu...")]
        public string afterStyle;
        public bool? block;
        [Description("A function called when the effect is finished. Note that effects are queued internally by the Fx class, so a callba...")]
        public Action callback;
        [Description("Whether to allow subsequently-queued effects to run at the same time as the current effect, or to ensure that they ru...")]
        public bool? concurrent;
        public int? duration;
        [Description("A valid Ext.lib.Easing value for the effect:&lt;div class=\"mdetail-params\"&gt; backBoth backIn backOut bounceBoth boun...")]
        public string easing;
        public int? endOpacity;
        public bool? remove;
        public json scope;
        [Description("Whether preceding effects should be stopped and removed before running current effect (only applies to non blocking e...")]
        public bool? stopFx;
        [Description("Whether to use the display CSS property instead of visibility when hiding Elements (only applies to effects that en...")]
        public bool? useDisplay;
    }
    public class KeyNav
    {
        [Description("The method to call on the Ext.EventObject after this KeyNav intercepts a key. Valid values are Ext.EventObject.stopE...")]
        public string defaultEventAction;
        public bool? disabled;
        [Description("Handle the keydown event instead of keypress (defaults to false). KeyNav automatically does this for IE since IE doe...")]
        public bool? forceKeyDown;
    }
    public class Layer
    {
        public string cls;
        public bool? constrain;
        public json dh;
        [Description("True to automatically create an Ext.Shadow, or a string indicating the shadow's display Ext.Shadow.mode. False to dis...")]
        public string shadow;
        public int? shadowOffset;
        public bool? shim;
        public bool? useDisplay;
        public int? zindex;
    }
    public class LoadMask
    {
        public string msg;
        public string msgCls;
        [Description("True to create a single-use mask that is automatically destroyed after loading (useful for page loads), False to pers...")]
        public bool? removeMask;
        [Description("Optional Store to which the mask is bound. The mask is displayed when a load request is issued, and hidden on either ...")]
        public Ext.data.Store store;
    }
    public class Resizable : util.Observable
    {
        [Description("String 'auto' or an array [width, height] with values to be added to the resize operation's new size (defaults to [0...")]
        public string[] adjustments;
        public bool? animate;
        public json constrainTo;
        public bool? disableTrackOver;
        public bool? draggable;
        public int? duration;
        public bool? dynamic;
        public string easing;
        public bool? enabled;
        public string handleCls;
        [Description("String consisting of the resize handles to display (defaults to undefined). Specify either 'all' or any of 'n s e w ...")]
        public string handles;
        public int? height;
        public int? heightIncrement;
        public int? maxHeight;
        public int? maxWidth;
        public int? minHeight;
        public int? minWidth;
        public int? minX;
        public int? minY;
        [Description("Deprecated")]
        public bool? multiDirectional;
        [Description("True to ensure that the resize handles are always visible, false to display them only when the user mouses over the ...")]
        public bool? pinned;
        public bool? preserveRatio;
        public bool? resizeChild;
        public json resizeRegion;
        public bool? transparent;
        public int? width;
        public int? widthIncrement;
        [Description("True to wrap an element with a div if needed (required for textareas and images, defaults to false) in favor of the ...")]
        public bool? wrap;
    }
    public class Shadow
    {
        [Description("The shadow display mode.")]
        public string mode;
        public string offset;
    }
    public class Template
    {
        public bool? compiled;
        [Description("Specify true to disable format functions in the template. If the template does not contain format functions, settin...")]
        public bool? disableFormats;
        [Description("The regular expression used to match template variables. Defaults to:re : /\\{([\\w-]+)\\}/g ...")]
        public string re;
    }
    public class XTemplate : Template { }


    public class BoxComponent : util.Observable
    {
        [Description("Note: this config is only used when this Component is rendered by a Container which has been configured to use an Anc...")]
        public string anchor;
        [Description("true to use overflow:'auto' on the components layout element and show scroll bars automatically when necessary, false...")]
        public bool? autoScroll;
        [Description("The maximum value in pixels which this BoxComponent will set its height to. Warning: This will override any size mana...")]
        public int? boxMaxHeight;
        [Description("The maximum value in pixels which this BoxComponent will set its width to. Warning: This will override any size manag...")]
        public int? boxMaxWidth;
        [Description("The minimum value in pixels which this BoxComponent will set its height to. Warning: This will override any size mana...")]
        public int? boxMinHeight;
        [Description("The minimum value in pixels which this BoxComponent will set its width to. Warning: This will override any size manag...")]
        public int? boxMinWidth;
        [Description("Note: this config is only used when this Component is rendered by a Container which has been configured to use a BoxL...")]
        public int? flex;
        [Description("The height of this component in pixels (defaults to auto). Note to express this dimension as a percentage or offset s...")]
        public int? height;
        [Description("Note: this config is only used when this BoxComponent is rendered by a Container which has been configured to use the...")]
        public json margins;
        public int? pageX;
        public int? pageY;
        [Description("Note: this config is only used when this BoxComponent is rendered by a Container which has been configured to use the...")]
        public string region;
        [Description("Note: this config is only used when this BoxComponent is a child item of a TabPanel. A string to be used as innerHTML...")]
        public string tabTip;
        [Description("The width of this component in pixels (defaults to auto). Note to express this dimension as a percentage or offset se...")]
        public int? width;
        public int? x;
        public int? y;
        [Description("True to use height:'auto', false to use fixed height (or allow it to be managed by its parent Container's layout mana...")]
        public bool? autoHeight;
        [Description("True to use width:'auto', false to use fixed width (or allow it to be managed by its parent Container's layout manage...")]
        public bool? autoWidth;
    }

    public class Button : BoxComponent
    {
        public bool? allowDepress;
        [Description("(Optional) The side of the Button box to render the arrow if the button has an associated menu. Two values are allow...")]
        public string arrowAlign;
        [Description("(Optional) A DomQuery selector which is used to extract the active, clickable element from the DOM structure created...")]
        public string buttonSelector;
        [Description("The DOM event that will fire the handler of the button. This can be any valid event name (dblclick, contextmenu). De...")]
        public string clickEvent;
        public string cls;
        public bool? disabled;
        public bool? enableToggle;
        public bool? handleMouseEvents;
        [Description("A function called when the button is clicked (can be used instead of click event). The handler is passed the followi...")]
        public Action handler;
        public bool? hidden;
        [Description("The path to an image to display in the button (the image will be set as the background-image CSS property of the but...")]
        public string icon;
        [Description("(Optional) The side of the Button box to render the icon. Four values are allowed.")]
        public string iconAlign;
        public string iconCls;
        [Description("Standard menu attribute consisting of a reference to a menu json, a menu id or a menu config blob (defaults to unde...")]
        public json menu;
        public string menuAlign;
        public int? minWidth;
        [Description("If used in a Toolbar, the text to be used if this item is shown in the overflow menu. See also Ext.Toolbar.Item.ove...")]
        public string overflowText;
        public bool? pressed;
        [Description("True to repeat fire the click event while the mouse is down. This can also be a ClickRepeater config json (default...")]
        public bool? repeat;
        [Description("(Optional) The size of the Button. Three values are allowed.")]
        public string scale;
        public json scope;
        public int? tabIndex;
        [Description(" A Template used to create the Button's DOM structure. Instances, or subclasses which need a different DOM structure...")]
        public Ext.Template template;
        public string text;
        public string toggleGroup;
        [Description("Function called when a Button with enableToggle set to true is clicked. Two arguments are passed:&lt;ul class=\"mdetail-p...")]
        public Action toggleHandler;
        [Description("The tooltip for the button - can be a string to be used as innerHTML (html tags are accepted) or QuickTips config obj...")]
        public string tooltip;
        public string tooltipType;
        public string type;
    }
    public class Container : BoxComponent
    {
        [Description("A string component id or the numeric index of the component that should be initially activated within the container's...")]
        public string activeItem;
        [Description("If true the container will automatically destroy any contained component that is removed from it, else destruction mu...")]
        public bool? autoDestroy;
        [Description("An array of events that, when fired, should be bubbled to any parent container. See Ext.util.Observable.enableBubble....")]
        public IList bubbleEvents;
        [Description("When set to true (50 milliseconds) or a number of milliseconds, the layout assigned for this container will buffer th...")]
        public bool? bufferResize;
        [Description("The default xtype of child Components to create in this Container when a child item is specified as a raw configurati...")]
        public string defaultType;
        [Description("This option is a means of applying default settings to all added items whether added through the items config or via ...")]
        public json defaults;
        [Description("If true the container will force a layout initially even if hidden or collapsed. This option is useful for forcing fo...")]
        public bool? forceLayout;
        [Description("True to hide the borders of each contained component, false to defer to the component's existing border settings (def...")]
        public bool? hideBorders;
        [Description("** IMPORTANT: be sure to specify a layout if needed ! ** A single item, or an array of child Components to be added t...")]
        public List<util.Observable> items;
        public json layoutConfig;
        [Description("True to automatically monitor window resize events to handle anything that is sensitive to the current size of the vi...")]
        public bool? monitorResize;
        [Description("*Important: In order for child items to be correctly sized and positioned, typically a layout manager must be specifi...")]
        public string layout;
        public string resizeEvent;
    }
    public class Panel : Container
    {
        [Description("true to animate the transition when the panel is collapsed, false to skip the animation (defaults to true if the Ext....")]
        public bool? animCollapse;
        [Description("The id of the node, a DOM node or an existing Element corresponding to a DIV that is already present in the document ...")]
        public json applyTo;
        [Description("true to use height:'auto', false to use fixed height (defaults to false). Note: Setting autoHeight: true means that t...")]
        public bool? autoHeight;
        [Description("A valid url spec according to the Updater Ext.Updater.update method. If autoLoad is not null, the panel will attempt ...")]
        public string autoLoad;
        [Description("The bottom toolbar of the panel. This can be a Ext.Toolbar json, a toolbar config, or an array of buttons/button co...")]
        public Toolbar bbar;
        [Description("A DomHelper element specification json specifying the element structure of this Panel's bbar Element. See bodyCfg ...")]
        public json bbarCfg;
        [Description("True to display an interior border on the body element of the panel, false to hide it (defaults to true). This only a...")]
        public bool? bodyBorder;
        [Description("A DomHelper element specification json may be specified for any Panel Element. By default, the Default element in t...")]
        public json bodyCfg;
        [Description("Additional css class selector to be applied to the body element in the format expected by Ext.Element.addClass (defau...")]
        public string bodyCssClass;
        [Description("Custom CSS styles to be applied to the body element in the format expected by Ext.Element.applyStyles (defaults to nu...")]
        public string bodyStyle;
        [Description("True to display the borders of the panel's body element, false to hide them (defaults to true). By default, the bord...")]
        public bool? border;
        [Description("The alignment of any buttons added to this panel. Valid values are 'right', 'left' and 'center' (defaults to 'right'...")]
        public string buttonAlign;
        [Description("buttons will be used as items for the toolbar in the footer (fbar). Typically the value of this configuration propert...")]
        public IList buttons;
        [Description("A DomHelper element specification json specifying the element structure of this Panel's bwrap Element. See bodyCfg...")]
        public json bwrapCfg;
        [Description("Panels themselves do not directly support being closed, but some Panel subclasses do (like Ext.Window) or a Panel Cla...")]
        public bool? closable;
        [Description("true to make sure the collapse/expand toggle button always renders first (to the left of) any other tools in the pane...")]
        public bool? collapseFirst;
        [Description("true")]
        public bool? collapsed;
        public string collapsedCls;
        [Description("True to make the panel collapsible and have the expand/collapse toggle button automatically rendered into the header ...")]
        public bool? collapsible;
        [Description("Render this panel disabled (default is false). An important note when using the disabled config on panels is that IE ...")]
        public bool? disabled;
        [Description("true to enable dragging of this Panel (defaults to false). For custom drag/drop implementations, an Ext.Panel.DD conf...")]
        public bool? draggable;
        [Description("A comma-delimited list of panel elements to initialize when the panel is rendered. Normally, this list will be gener...")]
        public string elements;
        [Description("A Toolbar json, a Toolbar config, or an array of Buttons/Button configs, describing a Toolbar to be rendered into t...")]
        public Toolbar fbar;
        [Description("This property is used to configure the underlying Ext.Layer. Acceptable values for this configuration property are:&lt;d...")]
        public json floating;
        [Description("true to create the footer element explicitly, false to skip creating it. The footer will be created automatically if ...")]
        public bool? footer;
        [Description("A DomHelper element specification json specifying the element structure of this Panel's footer Element. See bodyCf...")]
        public json footerCfg;
        [Description("true to create the Panel's header element explicitly, false to skip creating it. If a title is set the header will b...")]
        public bool? header;
        [Description("true")]
        public bool? headerAsText;
        [Description("A DomHelper element specification json specifying the element structure of this Panel's header Element. See bodyCf...")]
        public json headerCfg;
        [Description("true")]
        public bool? hideCollapseTool;
        [Description("The CSS class selector that specifies a background image to be used as the header icon (defaults to ''). An example o...")]
        public string iconCls;
        [Description("A Ext.KeyMap config json (in the format expected by Ext.KeyMap.addBinding used to assign custom key handling to thi...")]
        public KeyMap keys;
        [Description("true to mask the panel when it is disabled, false to not mask it (defaults to true). Either way, the panel will alwa...")]
        public bool? maskDisabled;
        public int? minButtonWidth;
        [Description("A shortcut for setting a padding style on the body element. The value can either be a number to be applied to all sid...")]
        public int? padding;
        [Description("Defaults to false. When set to true, an extra css class 'x-panel-normal' will be added to the panel's element, effec...")]
        public bool? preventBodyReset;
        public string resizeEvent;
        [Description("true (or a valid Ext.Shadow Ext.Shadow.mode value) to display a shadow behind the panel, false to display no shadow (...")]
        public Shadow shadow;
        [Description("The number of pixels to offset the shadow if displayed (defaults to 4). Note that this option only applies when float?...")]
        public int? shadowOffset;
        [Description("false to disable the iframe shim in browsers which need one (defaults to true). Note that this option only applies wh...")]
        public bool? shim;
        [Description("The top toolbar of the panel. This can be a Ext.Toolbar json, a toolbar config, or an array of buttons/button confi...")]
        public Toolbar tbar;
        [Description("A DomHelper element specification json specifying the element structure of this Panel's tbar Element. See bodyCfg ...")]
        public json tbarCfg;
        [Description("The title text to be used as innerHTML (html tags are accepted) to display in the panel header (defaults to ''). When...")]
        public string title;
        [Description("true to allow expanding and collapsing the panel (when collapsible = true) by clicking anywhere in the header bar, fa...")]
        public bool? titleCollapse;
        [Description("A Template used to create tools in the header Element. Defaults to:new Ext.Template('&amp;lt;div class=\"x-tool x-tool-{id...")]
        public Ext.Template toolTemplate;
        [Description("Overrides the baseCls setting to baseCls = 'x-plain' which renders the panel unstyled except for required attributes ...")]
        public bool? unstyled;
        [Description("The base CSS class to apply to this panel's element (defaults to 'x-panel'). Another option available by default is t...")]
        public string baseCls;
        [Description("false by default to render with plain 1px square borders. true to render with 9 elements, complete with custom rounde...")]
        public bool? frame;
        [Description("An array of tool button configs to be added to the header tool area. When rendered, each tool is stored as an Element...")]
        public IList tools;
    }
    public class ButtonGroup : Panel
    {
        public string baseCls;
        public int? columns;
        public bool? frame;
        public string layout;
    }
    public class ColorPalette : Component
    {
        public bool? allowReselect;
        [Description("The DOM event that will cause a color to be selected. This can be any valid event name (dblclick, contextmenu). Defa...")]
        public string clickEvent;
        [Description("Optional. A function that will handle the select event of this palette. The handler is passed the following parameter...")]
        public Action handler;
        public string itemCls;
        public json scope;
        public string tpl;
        [Description("The initial color to highlight (should be a valid 6-digit color hex code without the # symbol). Note that the hex co...")]
        public string value;
    }
    public class SplitButton : Button
    {
        public Action arrowHandler;
        public string arrowTooltip;
    }
    public class CycleButton : SplitButton
    {
        [Description("A callback function that will be invoked each time the active menu item in the button's menu has changed. If this c...")]
        public Action changeHandler;
        [Description("A css class which sets an image to be used as the static icon for this button. This icon will always be displayed r...")]
        public string forceIcon;
        [Description("An array of Ext.menu.CheckItem config objects to be used when creating the button's menu items (e.g., {text:'Foo', i...")]
        public IList items;
        [Description("A static string to prepend before the active item's text when displayed as the button's text (only applies when show...")]
        public string prependText;
        public bool? showText;
    }
    public class DataView : BoxComponent
    {
        public bool? deferEmptyText;
        public string emptyText;
        [Description("This is a required setting. A simple CSS selector (e.g. div.some-class or span:first-child) that will be used to det...")]
        public string itemSelector;
        [Description("A string to display during data load operations (defaults to undefined). If specified, this text will be displayed i...")]
        public string loadingText;
        [Description("True to allow selection of more than one item at a time, false to allow selection of only a single item at a time or ...")]
        public bool? multiSelect;
        public string overClass;
        public string selectedClass;
        [Description("True to enable multiselection by clicking on multiple items without requiring the user to hold Shift or Ctrl, false t...")]
        public bool? simpleSelect;
        [Description("True to allow selection of exactly one item at a time, false to allow no selection at all (defaults to false). Note t...")]
        public bool? singleSelect;
        public Ext.data.Store store;
        [Description("The HTML fragment or an array of fragments that will make up the template used by this DataView. This should be spec...")]
        public string tpl;
        public bool? trackOver;
    }
    public class DatePicker : Component
    {
        public string cancelText;
        public IList dayNames;
        [Description("An array of 'dates' to disable, as strings. These strings will be used to build a dynamic regular expression so they ...")]
        public IList disabledDates;
        [Description("JavaScript regular expression used to disable a pattern of dates (defaults to null). The disabledDates config will g...")]
        public string disabledDatesRE;
        public string disabledDatesText;
        public IList disabledDays;
        public string disabledDaysText;
        [Description("The default date format string which can be overriden for localization support. The format must be valid according t...")]
        public string format;
        [Description("Optional. A function that will handle the select event of this picker. The handler is passed the following parameters...")]
        public Action handler;
        public DateTime? maxDate;
        public string maxText;
        public DateTime? minDate;
        public string minText;
        public List<string> monthNames;
        public string monthYearText;
        public string nextText;
        public string okText;
        public string prevText;
        public json scope;
        [Description("False to hide the footer area containing the Today button and disable the keyboard handler for spacebar that selects ...")]
        public bool? showToday;
        public int? startDay;
        public string todayText;
        [Description("A string used to format the message for displaying in a tooltip over the button that selects the current date. Defaul...")]
        public string todayTip;
    }
    public class Editor : Component
    {
        public string alignment;
        public bool? allowBlur;
        [Description("True for the editor to automatically adopt the size of the underlying field, \"width\" to adopt the width only, or \"hei...")]
        public bool? autoSize;
        public bool? cancelOnEsc;
        public bool? completeOnEnter;
        public bool? constrain;
        public Ext.form.Field field;
        public bool? hideEl;
        [Description("True to skip the edit completion process (no save, no events fired) if the user completes an edit and the value has n...")]
        public bool? ignoreNoChange;
        public IList offsets;
        [Description("True to automatically revert the field value and cancel the edit when the user completes an edit and the field valida...")]
        public bool? revertInvalid;
        public bool? shadow;
        public bool? swallowKeys;
        public bool? updateEl;
        public json value;
    }
    public class FlashComponent : BoxComponent
    {
        public string backgroundColor;
        [Description("True to prompt the user to install flash if not installed. Note that this uses Ext.FlashComponent.EXPRESS_INSTALL_URL...")]
        public bool? expressInstall;
        [Description("A set of key value pairs to be passed to the flash json as parameters. Possible parameters can be found here: http:...")]
        public json flashParams;
        public json flashVars;
        public string flashVersion;
        public string url;
        public string wmode;
    }
    public class PagingToolbar : Toolbar
    {
        [Description("Customizable piece of the default paging text (defaults to 'of {0}'). Note that this string is formatted using {0} as...")]
        public string afterPageText;
        public string beforePageText;
        [Description("true")]
        public bool? displayInfo;
        [Description("The paging status message to display (defaults to 'Displaying {0} - {1} of {2}'). Note that this string is formatted ...")]
        public string displayMsg;
        public string emptyMsg;
        [Description("The quicktip text displayed for the first page button (defaults to 'First Page'). Note: quick tips must be initialize...")]
        public string firstText;
        [Description("The quicktip text displayed for the last page button (defaults to 'Last Page'). Note: quick tips must be initialized ...")]
        public string lastText;
        [Description("The quicktip text displayed for the next page button (defaults to 'Next Page'). Note: quick tips must be initialized ...")]
        public string nextText;
        public int? pageSize;
        [Description("true")]
        public bool? prependButtons;
        [Description("The quicktip text displayed for the previous page button (defaults to 'Previous Page'). Note: quick tips must be init...")]
        public string prevText;
        [Description("The quicktip text displayed for the Refresh button (defaults to 'Refresh'). Note: quick tips must be initialized for ...")]
        public string refreshText;
        public Ext.data.Store store;
    }
    public class Toolbar : Container
    {
        [Description("The default position at which to align child items. Defaults to \"left\" May be specified as \"center\" to cause items ad...")]
        public string buttonAlign;
        [Description("Defaults to false. Configure true to make the toolbar provide a button which activates a dropdown Menu to show items ...")]
        public bool? enableOverflow;
        [Description("This class assigns a default layout (layout:'toolbar'). Developers may override this configuration option if another ...")]
        public string layout;
    }
    public class ProgressBar : BoxComponent
    {
        public bool? animate;
        public string baseCls;
        public string id;
        public string text;
        public json textEl;
        public float? value;
    }
    public class ToolTip : Tip
    {
        [Description("A numeric pixel value used to offset the default position of the anchor arrow (defaults to 0). When the anchor pos...")]
        public int? anchorOffset;
        [Description("True to anchor the tooltip to the target element, false to anchor it relative to the mouse coordinates (defaults to...")]
        public bool? anchorToTarget;
        [Description("True to automatically hide the tooltip after the mouse exits the target element or after the dismissDelay has expir...")]
        public bool? autoHide;
        [Description("Optional. A DomQuery selector which allows selection of individual elements within the target element to trigger sh...")]
        public string _delegate;
        [Description("Delay in milliseconds before the tooltip automatically hides (defaults to 5000). To disable automatic hiding, set d...")]
        public int? dismissDelay;
        [Description("Delay in milliseconds after the mouse exits the target element but before the tooltip actually hides (defaults to 20...")]
        public int? hideDelay;
        public IList mouseOffset;
        public int? showDelay;
        public bool? trackMouse;
        public json target;
    }
    public class Tip : Panel
    {
        public bool? closable;
        [Description("Experimental. The default Ext.Element.alignTo anchor position value for this tip relative to its element of origin (...")]
        public string defaultAlign;
        public int? maxWidth;
        public int? minWidth;
        [Description("True or \"sides\" for the default effect, \"frame\" for 4-way shadow, and \"drop\" for bottom-right shadow (defaults to \"s...")]
        public string shadow;
        [Description("Width in pixels of the tip (defaults to auto). Width will be ignored if it exceeds the bounds of minWidth or maxWid...")]
        public int? width;
    }
    public class QuickTip : ToolTip
    {
        public bool? interceptTitles;
        public json target;
    }
    public class Slider : BoxComponent
    {
        public bool? animate;
        public bool? clickToChange;
        [Description("The number of decimal places to which to round the Slider's value. Defaults to 0. To disable rounding, configure as ...")]
        public int? decimalPrecision;
        public int? increment;
        [Description("How many units to change the Slider when adjusting with keyboard navigation. Defaults to 1. If the increment config i...")]
        public int? keyIncrement;
        public int? maxValue;
        public int? minValue;
        public int? value;
        public bool? vertical;
    }
    public class Spacer : BoxComponent { }
    public class TabPanel : Panel
    {
        public int? activeTab;
        [Description("True to animate tab scrolling so that hidden tabs slide smoothly into view (defaults to true). Only applies when ena...")]
        public bool? animScroll;
        [Description("The CSS selector used to search for tabs in existing markup when autoTabs = true (defaults to 'div.x-tab'). This can...")]
        public string autoTabSelector;
        [Description("true to query the DOM for any divs with a class of 'x-tab' to be automatically converted to tabs and added to this pa...")]
        public bool? autoTabs;
        public string baseCls;
        [Description("true by default to defer the rendering of child items to the browsers DOM until a tab is activated. false will render...")]
        public bool? deferredRender;
        [Description("True to enable scrolling to tabs that may be invisible due to overflowing the overall TabPanel width. Only available ...")]
        public bool? enableTabScroll;
        [Description("(Optional) A Template or XTemplate which may be provided to process the data json returned from getTemplateArgs to ...")]
        public Template itemTpl;
        [Description("TabPanel implicitly uses Ext.layout.CardLayout as its layout manager. layoutConfig may be used to configure this layo...")]
        public json layoutConfig;
        [Description("Set to true to force a layout of the active tab when the tab is changed. Defaults to false. See Ext.layout.CardLayout...")]
        public bool? layoutOnTabChange;
        public int? minTabWidth;
        public bool? plain;
        [Description("True to automatically resize each tab so that the tabs will completely fill the tab strip (defaults to false). Setti...")]
        public bool? resizeTabs;
        [Description("The number of milliseconds that each scroll animation should last (defaults to .35). Only applies when animScroll = t...")]
        public float? scrollDuration;
        [Description("The number of pixels to scroll each time a tab scroll button is pressed (defaults to 100, or if resizeTabs = true, th...")]
        public int? scrollIncrement;
        public int? scrollRepeatInterval;
        [Description("This config option is used on child Components of ths TabPanel. A CSS class name applied to the tab strip item repres...")]
        public string tabCls;
        [Description("The number of pixels of space to calculate into the sizing and scrolling of tabs. If you change the margin in CSS, yo...")]
        public int? tabMargin;
        [Description("The position where the tab strip should be rendered (defaults to 'top'). The only other supported value is 'bottom'. ...")]
        public string tabPosition;
        public int? tabWidth;
        public int? wheelIncrement;
    }
    public class Toolbar_Item : BoxComponent
    {
        public string overflowText;
    }
    public class Toolbar_Spacer : Toolbar_Item
    {
        public int? width;
    }
    public class Toolbar_Fill : Toolbar_Spacer { }
    public class Toolbar_Seperator : Toolbar_Item { }
    public class Toolbar_TextItem : Toolbar_Item
    {
        public string text;
    }
    public class ViewPort : Container { }
    public class Window : Panel
    {
        public string animateTarget;
        public string baseCls;
        [Description("True to display the 'close' tool button and allow the user to close the window, false to hide the button and disallow...")]
        public bool? closable;
        [Description("The action to take when the close header tool is clicked: 'close'...")]
        public string closeAction;
        [Description("True to render the window collapsed, false to render it expanded (defaults to false). Note that if expandOnShow is tr...")]
        public bool? collapsed;
        [Description("True to constrain the window within its containing element, false to allow it to fall outside of its containing eleme...")]
        public bool? constrain;
        [Description("True to constrain the window header within its containing element (allowing the window body to fall outside of its co...")]
        public bool? constrainHeader;
        [Description("Specifies a Component to receive focus when this Window is focussed. This may be one of...")]
        public string defaultButton;
        [Description("True to allow the window to be dragged by the header bar, false to disable dragging (defaults to true). Note that by...")]
        public bool? draggable;
        [Description("True to always expand the window when it is displayed, false to keep it in its current state (which may be collapsed)...")]
        public bool? expandOnShow;
        public bool? hidden;
        public bool? initHidden;
        public Ext.WindowGroup manager;
        [Description("True to display the 'maximize' tool button and allow the user to maximize the window, false to hide the button and di...")]
        public bool? maximizable;
        public bool? maximized;
        public int? minHeight;
        public int? minWidth;
        [Description("True to display the 'minimize' tool button and allow the user to minimize the window, false to hide the button and di...")]
        public bool? minimizable;
        [Description("True to make the window modal and mask everything behind it when displayed, false to display it without restricting a...")]
        public bool? modal;
        [Description("Allows override of the built-in processing for the escape key. Default action is to close the Window (performing what...")]
        public Action onEsc;
        [Description("True to render the window body with a transparent background so that it will blend into the framing elements, false t...")]
        public bool? plain;
        public bool? resizable;
        public string resizeHandles;
        [Description("The X position of the left edge of the window on initial showing. Defaults to centering the Window within the width o...")]
        public int? x;
        [Description("The Y position of the top edge of the window on initial showing. Defaults to centering the Window within the height o...")]
        public int? y;
    }



    public class KeyMap : json { }
    public class WindowGroup : json { }
}
