using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Ext.layout
{
    public class AbsoluteLayout : AnchorLayout{}
public class AnchorLayout : ContainerLayout
{
[Description("This configuation option is to be applied to child items of a container managed by this layout (ie. configured with l...")]
public string anchor;
}
public class ContainerLayout
{
[Description("An optional extra CSS class that will be added to the container. This can be useful for adding customized styles to t...")]
public string extraCls;
public bool? renderHidden;
}
public class AccordionLayout : FitLayout
{
[Description("True to swap the position of each panel as it is expanded so that it becomes the first item in the container, false ...")]
public bool? activeOnTop;
[Description("True to slide the contained panels open and closed during expand/collapse using animation, false to open and close d...")]
public bool? animate;
[Description("True to set each contained item's width to 'auto', false to use the item's current width (defaults to true). Note th...")]
public bool? autoWidth;
[Description("True to make sure the collapse/expand toggle button always renders first (to the left of) any other tools in the con...")]
public bool? collapseFirst;
[Description("True to adjust the active item's height to fill the available space in the container, false to use the item's curren...")]
public bool? fill;
[Description("True to hide the contained panels' collapse/expand toggle buttons, false to display them (defaults to false). When s...")]
public bool? hideCollapseTool;
[Description("Experimental")]
public bool? sequence;
[Description("True to allow expand/collapse of each contained panel by clicking anywhere on the title bar, false to allow expand/c...")]
public bool? titleCollapse;
}
    public class BorderLayout : ContainerLayout
    {}
public class BorderLayout_Region
{
[Description("When a collapsed region's bar is clicked, the region's panel will be displayed as a floated panel that will close aga...")]
public bool? animFloat;
[Description("When a collapsed region's bar is clicked, the region's panel will be displayed as a floated panel. If autoHide = tru...")]
public bool? autoHide;
[Description("An json containing margins to apply to the region when in the collapsed state in the format:{ top: (top margin)...")]
public json cmargins;
[Description("collapseMode supports two configuration values")]
public string collapseMode;
[Description("true to allow the user to collapse this region (defaults to false). If true, an expand/collapse tool button will aut...")]
public bool? collapsible;
[Description("true to allow clicking a collapsed region's bar to display the region's panel floated above the layout, false to forc...")]
public bool? floatable;
[Description("An json containing margins to apply to the region when in the expanded state in the format:{ top: (top margin),...")]
public json margins;
[Description("The minimum allowable height in pixels for this region (defaults to 50) maxHeight may also be specified. Note: settin...")]
public int? minHeight;
[Description("The minimum allowable width in pixels for this region (defaults to 50). maxWidth may also be specified. Note: setting...")]
public int? minWidth;
[Description("true to create a SplitRegion and display a 5px wide Ext.SplitBar between this region and its neighbor, allowing the u...")]
public bool? split;
}
public class BorderLayout_SplitRegion : BorderLayout_Region
{
[Description("The tooltip to display when the user hovers over a collapsible region's split bar (defaults to \"Drag to resize. Doubl...")]
public string collapsibleSplitTip;
[Description("The tooltip to display when the user hovers over a non-collapsible region's split bar (defaults to \"Drag to resize.\")...")]
public string splitTip;
public int? tickSize;
[Description("true to display a tooltip when the user hovers over a region's split bar (defaults to false). The tooltip text will ...")]
public bool? useSplitTips;
}
public class BoxLayout : ContainerLayout
{
[Description("If the individual contained items do not have a margins property specified, the default margins from this property wi...")]
public string defaultMargins;
[Description("Sets the padding to be applied to all child items managed by this layout. This property must be specified as a string...")]
public string padding;
}
public class CardLayout : FitLayout
{
[Description("True to render each contained item at the time it becomes active, false to render all contained items as soon as the...")]
public bool? deferredRender;
public bool? layoutOnCardChange;
}
    public class ColumnLayout : ContainerLayout
    {}
    public class FitLayout : ContainerLayout
    {}
public class FormLayout : AnchorLayout
{
[Description("A compiled Ext.Template for rendering the fully wrapped, labeled and styled form Field. Defaults to:new Ext.Template(...")]
public Ext.Template fieldTpl;
public string labelSeparator;
public bool? trackLabels;
}
public class HBoxLayout : BoxLayout
{
[Description("Controls how the child items of the container are aligned. Acceptable configuration values for this property are: &lt;di...")]
public string align;
[Description("This configuation option is to be applied to child items of the container managed by this layout. Each child item wit...")]
public int? flex;
[Description("Controls how the child items of the container are packed together. Acceptable configuration values for this property ...")]
public string pack;
}
    public class MenuLayout : ContainerLayout
    {}
public class TableLayout : ContainerLayout
{
[Description("The total number of columns to create in the table for this layout. If not specified, all Components added to this ...")]
public int? columns;
[Description("An json containing properties which are added to the DomHelper specification used to create the layout's &amp;lt;table...")]
public json tableAttrs;
}
public class VBoxLayout : BoxLayout
{
[Description("Controls how the child items of the container are aligned. Acceptable configuration values for this property are: &lt;di...")]
public string align;
[Description("This configuation option is to be applied to child items of the container managed by this layout. Each child item wit...")]
public int? flex;
[Description("Controls how the child items of the container are packed together. Acceptable configuration values for this property ...")]
public string pack;
}
public class ToolbarLayout : ContainerLayout{}
}
