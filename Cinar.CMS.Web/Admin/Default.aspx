<%@ Page Language="C#" %>
<%@ Import Namespace="Cinar.CMS.Library" %>
<%@ Import Namespace="Cinar.CMS.Library.Entities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Provider.User.IsInRole("Editor"))
        {
            Response.Redirect("Login.aspx", true); 
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Çınar CMS Yönetim Paneli</title>
    <link href="../external/resources/css/ext-all-notheme.css" rel="stylesheet" type="text/css" />
    <link href="../external/resources/css/xtheme-blue.css" rel="stylesheet" type="text/css" />
    <script src="../external/adapter/ext/ext-base.js" type="text/javascript"></script>
    <script src="../external/ext-all.js" type="text/javascript"></script>
    <link href="ext/superboxselect.css" rel="stylesheet" type="text/css" />
    <script src="ext/SuperBoxSelect.js" type="text/javascript"></script>
    <script src="ext/FileBrowser.js" type="text/javascript"></script>
    <link href="ext/UploadDialog/css/Ext.ux.UploadDialog.css" rel="stylesheet" type="text/css" />
    <script src="ext/UploadDialog/Ext.ux.UploadDialog.js" type="text/javascript"></script>
	<script type='text/javascript' src='ext/UploadDialog/locale/tr.utf-8.js'></script>
    <script src="menu.js" type="text/javascript"></script>
    <script src="entityList.js" type="text/javascript"></script>
    <style>
        .floatLeft {float:left;margin-right:2%;}
		body, td {
			font-family:arial,tahoma,verdana,helvetica;
			font-size:11px;
		}
		#logout {color:red; display:block; float:right; margin:10px; font-weight:bold}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <script type="text/javascript">
    var langs = <%= System.Utility.ToJSON(Provider.Database.ReadList(typeof(Lang), "select * from Lang")) %>;
    var defLangCode = '<%= Provider.Database.GetValue("select Code from Lang where Id={0}", Provider.Configuration.DefaultLang) %>';
    var DATA = {};
    DATA.categories = <%=Provider.GetIdNameListAsJson("Content","","ClassName='Category'") %>;
    DATA.authors = <%=Provider.GetIdNameListAsJson("Author","","") %>;
    DATA.sources = <%=Provider.GetIdNameListAsJson("Source","","") %>;
    Ext.onReady(function(){
    
        // NOTE: This is an example showing simple state management. During development,
        // it is generally best to disable state management as dynamically-generated ids
        // can change across page loads, leading to unpredictable results.  The developer
        // should ensure that stable state ids are set for stateful components in real apps.
        //Ext.state.Manager.setProvider(new Ext.state.CookieProvider());

        var viewport = new Ext.Viewport({
            layout: 'border',
            items: [
            // create instance immediately
            new Ext.BoxComponent({
                region: 'north',
                height: 51, // give north and south regions a height
                autoEl: {
                    tag: 'div',
                    html:'<p style="background:black"><a href="DoLogin.ashx?logout=1" id="logout">Oturumu Kapat</a><img src="menulogo.png"/></p>'
                }
            }), {
                region: 'east',
                title: 'Yardım',
                collapsible: true,
                collapsed: true,
                split: true,
                width: 225, // give east and west regions a width
                minSize: 175,
                maxSize: 400,
                margins: '0 5 0 0',
                layout: 'fit', // specify layout manager for items
                items: {
					html:'Yardım metni gelecek'
				}
            }, {
                region: 'west',
                id: 'west-panel', // see Ext.getCmp() below
                title: 'Yönetim',
                split: true,
                width: 200,
                minSize: 175,
                maxSize: 400,
                collapsible: true,
                margins: '0 0 0 5',
                layout: {
                    type: 'accordion',
                    animate: true
                },
                items: leftMenu
            },
            // in this instance the TabPanel is not wrapped by another panel
            // since no title is needed, this Panel is added directly
            // as a Container
            new Ext.TabPanel({
                id: 'tabPanel',
                region: 'center', // a center region is ALWAYS required for border layout
                activeTab: 0,     // first tab initially active
                items: [{
                    html: 'Çınar CMS pano.',
                    title: 'Pano',
					bodyStyle:'padding:5px 5px 5px 5px',
                    autoScroll: true
                }]
            })]
        });
    });
    </script>
    </div>
    </form>
</body>
</html>
