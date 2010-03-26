using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace Ext.grid
{
    public class Column
    {
        public string align;
        [Description("Optional. An inline style definition string which is applied to all table cells in the column (excluding headers). D...")]
        public string css;
        [Description("Required. The name of the field in the grid's Ext.data.Store's Ext.data.Record definition from which to draw the co...")]
        public string dataIndex;
        [Description("Optional. Defaults to true, enabling the configured editor. Set to false to initially disable editing on this colum...")]
        public bool? editable;
        [Description("Optional. The Ext.form.Field to use when editing values in this column if editing is supported by the grid. See edit...")]
        public Ext.form.Field editor;
        [Description("Optional. If the grid is being rendered by an Ext.grid.GroupingView, this option may be used to specify the text to ...")]
        public string emptyGroupText;
        public bool? _fixed;
        [Description("Optional. If the grid is being rendered by an Ext.grid.GroupingView, this option may be used to specify the text wit...")]
        public string groupName;
        [Description("Optional. If the grid is being rendered by an Ext.grid.GroupingView, this option may be used to specify the function...")]
        public Action groupRenderer;
        [Description("Optional. If the grid is being rendered by an Ext.grid.GroupingView, this option may be used to disable the header m...")]
        public bool? groupable;
        [Description("Optional. The header text to be used as innerHTML (html tags are accepted) to display in the Grid view. Note: to h...")]
        public string header;
        [Description("Optional. true to initially hide this column. Defaults to false. A hidden column may be shown via the header row men...")]
        public bool? hidden;
        [Description("Optional. Specify as false to prevent the user from hiding this column (defaults to true). To disallow column hidin...")]
        public bool? hideable;
        [Description("Optional. A name which identifies this column (defaults to the column's initial ordinal position.) The id is used to...")]
        public string id;
        public bool? menuDisabled;
        [Description("For an alternative to specifying a renderer see xtype Optional. A renderer is an 'interceptor' method which can be u...")]
        public json renderer;
        public bool? resizable;
        public json scope;
        [Description("Optional. true if sorting is to be allowed on this column. Defaults to the value of the Ext.grid.ColumnModel.default...")]
        public bool? sortable;
        [Description("Optional. A text string to use as the column header's tooltip. If Quicktips are enabled, this value will be used as...")]
        public string tooltip;
        [Description("Optional. The initial width in pixels of the column. The width of each column can also be affected if any of the fol...")]
        public int? width;
        [Description("Optional. A String which references a predefined Ext.grid.Column subclass type which is preconfigured with an approp...")]
        public string xtype;
    }
    public class BooleanColumn : Column
    {
        public string falseText;
        public string trueText;
        public string undefinedText;
    }
    public class CheckboxSelectionModel : RowSelectionModel
    {
        [Description("true")]
        public bool? checkOnly;
        [Description("Any valid text or HTML fragment to display in the header cell for the checkbox column. Defaults to:'&amp;lt;div class=\"...")]
        public string header;
        [Description("true")]
        public bool? sortable;
        public int? width;
    }
    public class RowSelectionModel : util.Observable
    {
        [Description("false to turn off moving the editor to the next row down when the enter key is pressed or the next row up when shift ...")]
        public bool? moveEditorOnEnter;
        [Description("true")]
        public bool? singleSelect;
    }
    public class ColumnModel : util.Observable
    {
        [Description("An Array of json literals. The config options defined by Ext.grid.Column are the options which may appear in the o...")]
        public IList columns;
        [Description("Default sortable of columns which have no sortable specified (defaults to false). This property shall preferably be ...")]
        public bool? defaultSortable;
        [Description("The width of columns which have no width specified (defaults to 100). This property shall preferably be configured t...")]
        public int? defaultWidth;
        [Description("Object literal which will be used to apply Ext.grid.Column configuration options to all columns. Configuration optio...")]
        public json defaults;
    }
    public class DateColumn : Column
    {
        public string format;
    }
    public class EditorGridPanel : GridPanel
    {
        public bool? autoEncode;
        [Description("The number of clicks on a cell required to display the cell's editor (defaults to 2). Setting this option to 'auto' m...")]
        public int? clicksToEdit;
        public bool? forceValidation;
        [Description("Any subclass of AbstractSelectionModel that will provide the selection model for the grid (defaults to Ext.grid.CellS...")]
        public json selModel;
    }
    public class GridPanel : Panel
    {
        [Description("The id of a column in this grid that should expand to fill unused space. This value specified here can not be 0. N...")]
        public string autoExpandColumn;
        public int? autoExpandMax;
        public int? autoExpandMin;
        [Description("An array of events that, when fired, should be bubbled to any parent container. See Ext.util.Observable.enableBubble...")]
        public IList bubbleEvents;
        public json cm;
        public json colModel;
        [Description("true")]
        public bool? columnLines;
        [Description("An array of columns to auto create a Ext.grid.ColumnModel. The ColumnModel may be explicitly created via the colMo...")]
        public IList columns;
        public string ddGroup;
        [Description("Configures the text in the drag proxy. Defaults to: ddText : '{0} selected row{1}' {0} is replaced with the number...")]
        public string ddText;
        [Description("Defaults to true to enable deferred row rendering. This allows the GridPanel to be initially rendered empty, with th...")]
        public bool? deferRowRender;
        [Description("<tt>true</tt> to disable selections in the grid. Defaults to <tt>false</tt>.")]
        public bool? disableSelection;
        public bool? enableColumnHide;
        public bool? enableColumnMove;
        [Description("false")]
        public bool? enableColumnResize;
        [Description("Enables dragging of the selected rows of the GridPanel. Defaults to false. Setting this to true causes this GridPane...")]
        public bool? enableDragDrop;
        public bool? enableHdMenu;
        public bool? hideHeaders;
        public json loadMask;
        public int? maxHeight;
        public int? minColumnWidth;
        public json sm;
        [Description("An array of events that, when fired, should trigger this component to save its state. Defaults to:stateEvents: ['col...")]
        public IList stateEvents;
        public Ext.data.Store store;
        [Description("true to stripe the rows. Default is false. This causes the CSS class x-grid3-row-alt to be added to alternate rows o...")]
        public bool? stripeRows;
        public json view;
        [Description("A config json that will be applied to the grid's UI view. Any of the config options available for Ext.grid.GridVi...")]
        public json viewConfig;
        [Description("Any subclass of Ext.grid.AbstractSelectionModel that will provide the selection model for the grid (defaults to Ext....")]
        public json selModel;
        public bool? trackMouseOver;
    }
    public class GridView : util.Observable
    {
        [Description("Defaults to false. Specify true to have the column widths re-proportioned when the grid is initially rendered. The ...")]
        public bool? autoFill;
        public string cellSelector;
        public int? cellSelectorDepth;
        public string columnsText;
        public bool? deferEmptyText;
        [Description("Default text (html tags are accepted) to display in the grid body when no rows are available (defaults to ''). This v...")]
        public string emptyText;
        [Description("True to add a second TR element per row that can be used to provide a row body that spans beneath the data row. Use ...")]
        public bool? enableRowBody;
        [Description("Defaults to false. Specify true to have the column widths re-proportioned at all times. The initially configured wi...")]
        public bool? forceFit;
        [Description("True to disable the grid column headers (defaults to false). Use the ColumnModel menuDisabled config to disable the ...")]
        public bool? headersDisabled;
        public string rowBodySelector;
        public int? rowBodySelectorDepth;
        public string rowSelector;
        public int? rowSelectorDepth;
        [Description("The amount of space to reserve for the vertical scrollbar (defaults to undefined). If an explicit value isn't specifi...")]
        public int? scrollOffset;
        [Description("The CSS class applied to a selected row (defaults to 'x-grid3-row-selected'). An example overriding the default styli...")]
        public string selectedRowClass;
        public string sortAscText;
        public List<string> sortClasses;
        public string sortDescText;
    }
    public class GroupingView : GridView
    {
        [Description("The text to display when there is an empty group value (defaults to '(None)'). May also be specified per column, see...")]
        public string emptyGroupText;
        [Description("false")]
        public bool? enableGrouping;
        [Description("true")]
        public bool? enableGroupingMenu;
        [Description("true")]
        public bool? enableNoGroups;
        public string groupByText;
        [Description("Indicates how to construct the group identifier. 'value' constructs the id using raw value, 'display' constructs the...")]
        public string groupMode;
        public Action groupRenderer;
        [Description("The template used to render the group header (defaults to '{text}'). This is used to format an json which contains...")]
        public string groupTextTpl;
        [Description("true")]
        public bool? hideGroupedColumn;
        [Description("true")]
        public bool? ignoreAdd;
        [Description("If true will display a prefix plus a ': ' before the group field value in the group header line. The prefix will co...")]
        public bool? showGroupName;
        public string showGroupsText;
        [Description("true")]
        public bool? startCollapsed;
    }
    public class NumberColumn : Column
    {
        [Description("A formatting string as used by Ext.util.Format.number to format a numeric value for this Column (defaults to '0,000....")]
        public string format;
    }
    public class PropertyGrid : EditorGridPanel
    {
        [Description("An json containing name/value pairs of custom editor type definitions that allow the grid to support additional typ...")]
        public json customEditors;
        [Description("An json containing name/value pairs of custom renderer type definitions that allow the grid to support custom rende...")]
        public json customRenderers;
        [Description("An json containing property name/display name pairs. If specified, the display name will be shown in the name colum...")]
        public json propertyNames;
        public json source;
    }
    public class RowNumberer
    {
        public string header;
        public int? width;
    }
    public class TemplateColumn : Column
    {
        [Description("An XTemplate, or an XTemplate definition string to use to process a Record's data to produce a column's rendered val...")]
        public string tpl;
    }
    public class PropertyStore : util.Observable{}
}