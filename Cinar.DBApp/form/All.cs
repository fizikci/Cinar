using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace Ext.form
{
public class Checkbox : Field
{
[Description("A DomHelper element spec, or true for a default element spec (defaults to {tag: 'input', type: 'checkbox', autocomple...")]
public String autoCreate;
public string boxLabel;
[Description("true")]
public bool? _checked;
public string fieldClass;
public string focusClass;
[Description("A function called when the checked value changes (can be used instead of handling the check event). The handler is p...")]
public Action handler;
public string inputValue;
public json scope;
}
public class Field : BoxComponent
{
public string cls;
[Description("True to disable the field (defaults to false). Be aware that conformant with the &lt;a href=\"http://www.w3.org/TR/html40...")]
public bool? disabled;
[Description("The type attribute for input fields -- e.g. radio, text, password, file (defaults to 'text'). The types 'file' and 'p...")]
public string inputType;
public string invalidClass;
[Description("The error text to use when marking a field invalid and no message is provided (defaults to 'The value in this field i...")]
public string invalidText;
[Description("Experimental")]
public string msgFx;
[Description("location where the message text set through markInvalid should display. Must be one of the following values: &lt;div cla...")]
public string msgTarget;
[Description("The field's HTML name attribute (defaults to ''). Note: this property must be set if this field is to be automaticall...")]
public string name;
[Description("true")]
public bool? preventMark;
[Description("true to mark the field as readOnly in HTML (defaults to false). Note: this only sets the element's readOnly DOM attri...")]
public bool? readOnly;
public bool? submitValue;
[Description("The tabIndex for this field. Note this only applies to fields that are rendered, not those which are built via applyT...")]
public int? tabIndex;
public bool? validateOnBlur;
public int? validationDelay;
[Description("The event that should initiate field validation. Set to false to disable automatic validation (defaults to 'key...")]
public string validationEvent;
public json value;
[Description("A DomHelper element spec, or true for a default element spec. Used to create the Element which will encapsulate this ...")]
public string autoCreate;
public string fieldClass;
public string focusClass;
}
public class CheckboxGroup : Field
{
[Description("False to validate that at least one item in the group is checked (defaults to true). If no items are selected at vali...")]
public bool? allowBlank;
[Description("Error text to display if the allowBlank validation fails (defaults to \"You must select at least one item in this grou...")]
public string blankText;
[Description("Specifies the number of columns to use when displaying grouped checkbox/radio controls using automatic layout. This ...")]
public int? columns;
public IList items;
[Description("True to distribute contained controls across columns, completely filling each column top to bottom before starting on...")]
public bool? vertical;
}
public class ComboBox : TriggerField
{
public string allQuery;
[Description("A DomHelper element spec, or true for a default element spec. Used to create the Element which will encapsulate this ...")]
public string autoCreate;
[Description("true to select the first result gathered by the data store (defaults to true). A false value would require a manual ...")]
public bool? autoSelect;
[Description("true")]
public bool? clearFilterOnReset;
[Description("The underlying data field name to bind to this ComboBox (defaults to undefined if mode = 'remote' or 'field1' if tran...")]
public string displayField;
[Description("true to restrict the selected value to one of the values in the list, false to allow the user to set arbitrary text i...")]
public bool? forceSelection;
public int? handleHeight;
[Description("If hiddenName is specified, hiddenId can also be provided to give the hidden field a unique id (defaults to the hidde...")]
public string hiddenId;
[Description("If specified, a hidden form field with this name is dynamically generated to store the field's data value (defaults t...")]
public string hiddenName;
[Description("Sets the initial value of the hidden field if hiddenName is specified to contain the selected valueField, from the St...")]
public string hiddenValue;
[Description("A simple CSS selector (e.g. div.some-class or span:first-child) that will be used to determine what nodes the Ext.Dat...")]
public string itemSelector;
[Description("true")]
public bool? lazyInit;
[Description("true to prevent the ComboBox from rendering until requested (should always be used when rendering into an Ext.Editor ...")]
public bool? lazyRender;
[Description("A valid anchor position value. See Ext.Element.alignTo for details on supported anchor positions and offsets. To spec...")]
public string listAlign;
public string listClass;
public string listEmptyText;
[Description("The width (used as a parameter to Ext.Element.setWidth) of the dropdown list (defaults to the width of the ComboBox f...")]
public int? listWidth;
[Description("The text to display in the dropdown list while data is loading. Only applies when mode = 'remote' (defaults to 'Load...")]
public string loadingText;
public int? maxHeight;
[Description("The minimum number of characters the user must type before autocomplete and typeAhead activate (defaults to 4 if mode...")]
public int? minChars;
[Description("The minimum height in pixels of the dropdown list when the list is constrained by its distance to the viewport edges ...")]
public int? minHeight;
public int? minListWidth;
[Description("Acceptable values are: 'remote' : Automatically loads the st...")]
public string mode;
[Description("If greater than 0, a Ext.PagingToolbar is displayed in the footer of the dropdown list and the filter queries will ex...")]
public int? pageSize;
[Description("The length of time in milliseconds to delay between the start of typing and sending the query to filter the dropdown ...")]
public int? queryDelay;
public string queryParam;
[Description("true to add a resize handle to the bottom of the dropdown list (creates an Ext.Resizable with 'se' pinned handles). D...")]
public bool? resizable;
[Description("true to select any existing text in the field immediately on focus. Only applies when editable = true (defaults to fa...")]
public bool? selectOnFocus;
public string selectedClass;
[Description("true")]
public bool? shadow;
[Description("The data source to which this combo is bound (defaults to undefined). Acceptable values for this property are: &lt;div c...")]
public Ext.data.Store store;
[Description("False to clear the name attribute on the field so that it is not submitted during a form post. If a hiddenName is spe...")]
public bool? submitValue;
[Description("If supplied, a header element is created containing this text and added into the top of the dropdown list (defaults t...")]
public string title;
[Description("The template string, or Ext.XTemplate instance to use to display each item in the dropdown list. The dropdown list is...")]
public Ext.XTemplate tpl;
[Description("The id, DOM node or element of an existing HTML SELECT to convert to a ComboBox. Note that if you specify this and th...")]
public json transform;
[Description("The action to execute when the trigger is clicked.")]
public string triggerAction;
[Description("An additional CSS class used to style the trigger button. The trigger will always get the class 'x-form-trigger' and...")]
public string triggerClass;
[Description("true to populate and autoselect the remainder of the text being typed after a configurable delay (typeAheadDelay) if ...")]
public bool? typeAhead;
[Description("The length of time in milliseconds to wait until the typeahead text is displayed if typeAhead = true (defaults to 250...")]
public int? typeAheadDelay;
[Description("The underlying data value name to bind to this ComboBox (defaults to undefined if mode = 'remote' or 'field2' if tran...")]
public string valueField;
[Description("When using a name/value combo, if the value passed to setValue is not found in the store, valueNotFoundText will be d...")]
public string valueNotFoundText;
}
public class TextField : Field
{
public bool? allowBlank;
public string blankText;
public bool? disableKeyFilter;
[Description("The CSS class to apply to an empty field to style the emptyText (defaults to 'x-form-empty-field'). This class is au...")]
public string emptyClass;
[Description("The default text to place into an empty field (defaults to null). Note: that this value will be submitted to the serv...")]
public string emptyText;
[Description("true")]
public bool? enableKeyEvents;
public string maskRe;
[Description("Maximum input field length allowed by validation (defaults to Number.MAX_VALUE). This behavior is intended to provide...")]
public int? maxLength;
[Description("Error text to display if the maximum length validation fails (defaults to 'The maximum length for this field is {maxL...")]
public string maxLengthText;
public int? minLength;
[Description("Error text to display if the minimum length validation fails (defaults to 'The minimum length for this field is {minL...")]
public string minLengthText;
[Description("A JavaScript RegExp json to be tested against the field value during validation (defaults to null). If the test fai...")]
public string regex;
public string regexText;
public string stripCharsRe;
[Description("A custom validation function to be called during field validation (validateValue) (defaults to null). If specified, t...")]
public Action validator;
public string vtype;
[Description("A custom error message to display in place of the default message provided for the vtype currently set for this field...")]
public string vtypeText;
[Description("true")]
public bool? selectOnFocus;
[Description("true")]
public bool? grow;
public int? growMax;
public int? growMin;
}
public class TriggerField : TextField
{
[Description("false to prevent the user from typing text directly into the field, the field will only respond to a click on the tri...")]
public bool? editable;
[Description("true")]
public bool? hideTrigger;
[Description("true to prevent the user from changing the field, and hides the trigger. Superceeds the editable and hideTrigger opt...")]
public bool? readOnly;
[Description("A DomHelper config json specifying the structure of the trigger element for this Field. (Optional). Specify this wh...")]
public json triggerConfig;
public string wrapFocusClass;
[Description("A DomHelper element spec, or true for a default element spec. Used to create the Element which will encapsulate this ...")]
public string autoCreate;
[Description("An additional CSS class used to style the trigger button. The trigger will always get the class 'x-form-trigger' by ...")]
public string triggerClass;
}
public class DateField : TriggerField
{
[Description("Multiple date formats separated by \"|\" to try when parsing a user input value and it does not match the defined forma...")]
public string altFormats;
[Description("A DomHelper element specification json, or true for the default element specification json:autoCreate: {tag: \"inp...")]
public string autoCreate;
[Description("An array of \"dates\" to disable, as strings. These strings will be used to build a dynamic regular expression so they ...")]
public IList disabledDates;
public string disabledDatesText;
[Description("An array of days to disable, 0 based (defaults to null). Some examples:// disable Sunday and Saturday: disabledDays: ...")]
public IList disabledDays;
public string disabledDaysText;
[Description("The default date format string which can be overriden for localization support. The format must be valid according t...")]
public string format;
[Description("The error text to display when the date in the field is invalid (defaults to '{value} is not a valid date - it must b...")]
public string invalidText;
[Description("The error text to display when the date in the cell is after maxValue (defaults to 'The date in this field must be be...")]
public string maxText;
[Description("The maximum allowed date. Can be either a Javascript date json or a string date in a valid format (defaults to null...")]
public string maxValue;
[Description("The error text to display when the date in the cell is before minValue (defaults to 'The date in this field must be a...")]
public string minText;
[Description("The minimum allowed date. Can be either a Javascript date json or a string date in a valid format (defaults to null...")]
public string minValue;
[Description("false to hide the footer area of the DatePicker containing the Today button and disable the keyboard handler for spac...")]
public bool? showToday;
[Description("An additional CSS class used to style the trigger button. The trigger will always get the class 'x-form-trigger' and...")]
public string triggerClass;
}
public class DisplayField
{
public string fieldClass;
[Description("false to skip HTML-encoding the text when rendering it (defaults to false). This might be useful if you want to incl...")]
public bool? htmlEncode;
}
public class FieldSet : Panel
{
[Description("true")]
public bool? animCollapse;
public string baseCls;
public string checkboxName;
[Description("true to render a checkbox into the fieldset frame just in front of the legend to expand/collapse the fieldset when t...")]
public json checkboxToggle;
[Description("true to make the fieldset collapsible and have the expand/collapse toggle button automatically rendered into the leg...")]
public bool? collapsible;
[Description("A css class to apply to the x-form-item of fields (see Ext.layout.FormLayout.fieldTpl for details). This property ...")]
public string itemCls;
public int? labelWidth;
public string layout;
}
public class Container
{
[Description("If true the container will automatically destroy any contained component that is removed from it, else destruction mu...")]
public bool? autoDestroy;
[Description("An array of events that, when fired, should be bubbled to any parent container. See Ext.util.Observable.enableBubble....")]
public IList bubbleEvents;
[Description("This option is a means of applying default settings to all added items whether added through the items config or via ...")]
public json defaults;
[Description("If true the container will force a layout initially even if hidden or collapsed. This option is useful for forcing fo...")]
public bool? forceLayout;
[Description("True to hide the borders of each contained component, false to defer to the component's existing border settings (def...")]
public bool? hideBorders;
[Description("** IMPORTANT: be sure to specify a layout if needed ! ** A single item, or an array of child Components to be added t...")]
public IList items;
public json layoutConfig;
[Description("True to automatically monitor window resize events to handle anything that is sensitive to the current size of the vi...")]
public bool? monitorResize;
[Description("A string component id or the numeric index of the component that should be initially activated within the container's...")]
public string activeItem;
[Description("When set to true (50 milliseconds) or a number of milliseconds, the layout assigned for this container will buffer th...")]
public bool? bufferResize;
[Description("The default xtype of child Components to create in this Container when a child item is specified as a raw configurati...")]
public string defaultType;
}
public class Panel
{
[Description("true to use height:'auto', false to use fixed height (defaults to false). Note: Setting autoHeight: true means that t...")]
public bool? autoHeight;
[Description("A valid url spec according to the Updater Ext.Updater.update method. If autoLoad is not null, the panel will attempt ...")]
public string autoLoad;
[Description("The bottom toolbar of the panel. This can be a Ext.Toolbar json, a toolbar config, or an array of buttons/button co...")]
public json bbar;
[Description("A DomHelper element specification json specifying the element structure of this Panel's bbar Element. See bodyCfg ...")]
public json bbarCfg;
[Description("A DomHelper element specification json may be specified for any Panel Element. By default, the Default element in t...")]
public json bodyCfg;
[Description("Additional css class selector to be applied to the body element in the format expected by Ext.Element.addClass (defau...")]
public string bodyCssClass;
[Description("Custom CSS styles to be applied to the body element in the format expected by Ext.Element.applyStyles (defaults to nu...")]
public string bodyStyle;
[Description("The alignment of any buttons added to this panel. Valid values are 'right', 'left' and 'center' (defaults to 'right'...")]
public string buttonAlign;
[Description("buttons will be used as items for the toolbar in the footer (fbar). Typically the value of this configuration propert...")]
public IList buttons;
[Description("A DomHelper element specification json specifying the element structure of this Panel's bwrap Element. See bodyCfg...")]
public json bwrapCfg;
[Description("Panels themselves do not directly support being closed, but some Panel subclasses do (like Ext.Window) or a Panel Cla...")]
public bool? closable;
[Description("true")]
public bool? collapsed;
public string collapsedCls;
[Description("Render this panel disabled (default is false). An important note when using the disabled config on panels is that IE ...")]
public bool? disabled;
[Description("true to enable dragging of this Panel (defaults to false). For custom drag/drop implementations, an Ext.Panel.DD conf...")]
public bool? draggable;
[Description("A Toolbar json, a Toolbar config, or an array of Buttons/Button configs, describing a Toolbar to be rendered into t...")]
public json fbar;
[Description("A DomHelper element specification json specifying the element structure of this Panel's footer Element. See bodyCf...")]
public json footerCfg;
[Description("A DomHelper element specification json specifying the element structure of this Panel's header Element. See bodyCf...")]
public json headerCfg;
[Description("A Ext.KeyMap config json (in the format expected by Ext.KeyMap.addBinding used to assign custom key handling to thi...")]
public json keys;
[Description("true to mask the panel when it is disabled, false to not mask it (defaults to true). Either way, the panel will alwa...")]
public bool? maskDisabled;
public int? minButtonWidth;
[Description("A shortcut for setting a padding style on the body element. The value can either be a number to be applied to all sid...")]
public string padding;
[Description("Defaults to false. When set to true, an extra css class 'x-panel-normal' will be added to the panel's element, effec...")]
public bool? preventBodyReset;
public string resizeEvent;
[Description("A DomHelper element specification json specifying the element structure of this Panel's tbar Element. See bodyCfg ...")]
public json tbarCfg;
[Description("The title text to be used as innerHTML (html tags are accepted) to display in the panel header (defaults to ''). When...")]
public string title;
[Description("true to allow expanding and collapsing the panel (when collapsible = true) by clicking anywhere in the header bar, fa...")]
public bool? titleCollapse;
[Description("Overrides the baseCls setting to baseCls = 'x-plain' which renders the panel unstyled except for required attributes ...")]
public bool? unstyled;
[Description("true to animate the transition when the panel is collapsed, false to skip the animation (defaults to true if the Ext....")]
public bool? animCollapse;
[Description("The id of the node, a DOM node or an existing Element corresponding to a DIV that is already present in the document ...")]
public json applyTo;
[Description("The base CSS class to apply to this panel's element (defaults to 'x-panel'). Another option available by default is t...")]
public string baseCls;
[Description("True to display an interior border on the body element of the panel, false to hide it (defaults to true). This only a...")]
public bool? bodyBorder;
[Description("True to display the borders of the panel's body element, false to hide them (defaults to true). By default, the bord...")]
public bool? border;
[Description("true to make sure the collapse/expand toggle button always renders first (to the left of) any other tools in the pane...")]
public bool? collapseFirst;
[Description("True to make the panel collapsible and have the expand/collapse toggle button automatically rendered into the header ...")]
public bool? collapsible;
[Description("A comma-delimited list of panel elements to initialize when the panel is rendered. Normally, this list will be gener...")]
public string elements;
[Description("This property is used to configure the underlying Ext.Layer. Acceptable values for this configuration property are:&lt;d...")]
public json floating;
[Description("true to create the footer element explicitly, false to skip creating it. The footer will be created automatically if ...")]
public bool? footer;
[Description("false by default to render with plain 1px square borders. true to render with 9 elements, complete with custom rounde...")]
public bool? frame;
[Description("true to create the Panel's header element explicitly, false to skip creating it. If a title is set the header will b...")]
public bool? header;
[Description("true")]
public bool? headerAsText;
[Description("true")]
public bool? hideCollapseTool;
[Description("The CSS class selector that specifies a background image to be used as the header icon (defaults to ''). An example o...")]
public string iconCls;
[Description("true (or a valid Ext.Shadow Ext.Shadow.mode value) to display a shadow behind the panel, false to display no shadow (...")]
public bool? shadow;
[Description("The number of pixels to offset the shadow if displayed (defaults to 4). Note that this option only applies when float?...")]
public int? shadowOffset;
[Description("false to disable the iframe shim in browsers which need one (defaults to true). Note that this option only applies wh...")]
public bool? shim;
[Description("The top toolbar of the panel. This can be a Ext.Toolbar json, a toolbar config, or an array of buttons/button confi...")]
public json tbar;
[Description("A Template used to create tools in the header Element. Defaults to:new Ext.Template('&amp;lt;div class=\"x-tool x-tool-{id...")]
public string toolTemplate;
[Description("An array of tool button configs to be added to the header tool area. When rendered, each tool is stored as an Element...")]
public IList tools;
}
public class FormPanel : Panel
{
[Description("An array of Ext.Buttons or Ext.Button configs used to add buttons to the footer of this FormPanel. Buttons in the foo...")]
public IList buttons;
public string formId;
[Description("<tt>true</tt> to hide field labels by default (sets <tt>display:none</tt>). Defaults to <tt>false</tt>.")]
public bool? hideLabels;
public string itemCls;
[Description("The label alignment value used for the text-align specification for the container. Valid values are \"left\", \"top\" or ...")]
public string labelAlign;
[Description("The default padding in pixels for field labels (defaults to 5). labelPad only applies if labelWidth is also specified...")]
public int? labelPad;
public string labelSeparator;
[Description("The width of labels in pixels. This property cascades to child containers and can be overridden on any child containe...")]
public int? labelWidth;
[Description("Defaults to 'form'. Normally this configuration property should not be altered. For additional details see Ext.layou...")]
public string layout;
public int? minButtonWidth;
public int? monitorPoll;
[Description("If true, the form monitors its valid state client-side and regularly fires the clientvalidation event passing that st...")]
public bool? monitorValid;
}
public class Hidden : Field
{ 
}
public class HtmlEditor : Field
{
public string createLinkText;
public string defaultLinkValue;
[Description("A default value to be put into the editor to resolve focus issues (defaults to &amp;#160; (Non-breaking space) in Opera a...")]
public string defaultValue;
public bool? enableAlignments;
public bool? enableColors;
public bool? enableFont;
public bool? enableFontSize;
public bool? enableFormat;
public bool? enableLinks;
public bool? enableLists;
public bool? enableSourceEdit;
public IList fontFamilies;
}
public class Label : BoxComponent
{
[Description("The id of the input element to which this label will be bound via the standard HTML 'for' attribute. If not specified...")]
public string forId;
[Description("An HTML fragment that will be used as the label's innerHTML (defaults to ''). Note that if text is specified it will ...")]
public string html;
[Description("The plain text to display within the label (defaults to ''). If you need to include HTML tags within the label's inne...")]
public string text;
}
public class NumberField :TextField
{
public bool? allowDecimals;
public bool? allowNegative;
public string baseChars;
public int? decimalPrecision;
public string decimalSeparator;
public string fieldClass;
[Description("Error text to display if the maximum value validation fails (defaults to \"The maximum value for this field is {maxVal...")]
public string maxText;
public int? maxValue;
[Description("Error text to display if the minimum value validation fails (defaults to \"The minimum value for this field is {minVal...")]
public string minText;
public int? minValue;
[Description("Error text to display if the value is not a valid number. For example, this can happen if a valid character like '.'...")]
public string nanText;
}
public class Radio : Checkbox
{ 
}
public class RadioGroup : CheckboxGroup
{
[Description("True to allow every item in the group to be blank (defaults to true). If allowBlank = false and no items are selected...")]
public bool? allowBlank;
public string blankText;
public IList items;
}
public class TextArea : TextField
{
[Description("A DomHelper element spec, or true for a default element spec. Used to create the Element which will encapsulate this ...")]
public string autoCreate;
public int? growMax;
public int? growMin;
[Description("true to prevent scrollbars from appearing regardless of how much text is in the field. This option is only relevant w...")]
public bool? preventScrollbars;
}
public class TimeField : ComboBox
{
[Description("Multiple date formats separated by \"|\" to try when parsing a user input value and it doesn't match the defined forma...")]
public string altFormats;
[Description("The default time format string which can be overriden for localization support. The format must be valid according ...")]
public string format;
public int? increment;
public string invalidText;
[Description("The error text to display when the time is after maxValue (defaults to 'The time in this field must be equal to or b...")]
public string maxText;
[Description("The maximum allowed time. Can be either a Javascript date json with a valid time value or a string time in a vali...")]
public string maxValue;
[Description("The error text to display when the date in the cell is before minValue (defaults to 'The time in this field must be ...")]
public string minText;
[Description("The minimum allowed time. Can be either a Javascript date json with a valid time value or a string time in a vali...")]
public string minValue;
}
public class TwinTriggerField : TriggerField
{
[Description("An additional CSS class used to style the trigger button. The trigger will always get the class 'x-form-trigger' by ...")]
public string trigger1Class;
[Description("An additional CSS class used to style the trigger button. The trigger will always get the class 'x-form-trigger' by ...")]
public string trigger2Class;
[Description("A DomHelper config json specifying the structure of the trigger elements for this Field. (Optional). Specify this w...")]
public json triggerConfig;
}
}