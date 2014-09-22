$
function getControlHtml(c){
	var str = "";
	if(c.UIMetadata.EditorType.ToString()=="LookUp" || c.UIMetadata.EditorType.ToString()=="ComboBox"){
		if(c.ReferenceColumn){
			var ref = c.ReferenceColumn.Table.Name;
			str = '
	                <select id="'+c.Name+'" name="'+c.Name+'" class="col-sm-9">
	                    <option value="0"></option>
	                    <% foreach(var e in Provider.Database.ReadList<'+ref+'>("select Id, Name from '+ref+' order by Name")) { %>
	                        <option value="<%=e.Id%>"><%=e.Name%></option>
	                    <% } %>
	                </select>
			';
		}
		else
		str = '
                <select id="'+c.Name+'" name="'+c.Name+'" class="col-sm-9">
                    <option value="0"></option>
					<option>Value</option>
                </select>
		';
	}
	if(c.UIMetadata.EditorType.ToString()=="TagEdit"){
		if(c.ReferenceColumn){
			var ref = c.References.Name;
			str = '
	                <select multiple="" class="width-80 chosen-select" id="'+c.Name+'" name="'+c.Name+'" data-placeholder="Select tags">
	                    <option value="0"></option>
	                    <% foreach(var e in Provider.Database.ReadList<'+ref+'>("select Id, Name from '+ref+' order by Name")) { %>
	                        <option><%=e.Name%></option>
	                    <% } %>
	                </select>
			';
		}
		else
			str = '
	                <select multiple="" class="width-80 chosen-select" id="'+c.Name+'" name="'+c.Name+'" data-placeholder="Select tags">
	                    <option>One</option>
	                    <option>Two</option>
	                    <option>Three</option>
	                </select>
			';
	}
	if(c.UIMetadata.EditorType.ToString()=="TextEdit"){
		str = '
				<input type="text" id="'+c.Name+'" name="'+c.Name+'" placeholder="" class="col-sm-6" />
		';
	}
	if(c.UIMetadata.EditorType.ToString()=="MemoEdit"){
		str = '
				<textarea id="'+c.Name+'" name="'+c.Name+'" rows="3" class="col-sm-8"></textarea>
		';
	}
	if(c.UIMetadata.EditorType.ToString()=="DateEdit"){
		str = '
                <input type="text" class="date-picker" id="'+c.Name+'" name="'+c.Name+'" data-date-format="dd-mm-yyyy"/>
				<span class="input-group-addon">
					<i class="icon-calendar bigger-110"></i>
				</span>
		';
	}
	if(c.UIMetadata.EditorType.ToString()=="CheckBox"){
		str = '
            <label class="col-sm-4">
            	<input id="'+c.Name+'" name="'+c.Name+'" class="ace ace-switch ace-switch-5" value="1" type="checkbox"/>
        		<span class="lbl"></span>
        	</label>
		';
	}
	if(c.UIMetadata.EditorType.ToString()=="NumberEdit"){
		str = '
            <input id="'+c.Name+'" name="'+c.Name+'" class="input-mini bkspinner" type="text" value="0"/>
		';
	}
	return str;
}
$<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Main.Master" AutoEventWireup="true" %>

<%@ Import Namespace="$=db.Name$.DTO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntHead" runat="server">
    <script type="text/javascript" src="<%=ResolveClientUrl("~/Staff/App_Sources/js/AjaxOperations.js") %>"></script>
    <script>
        \$(function () {
            $=table.Name$Handler.GetById(function(app) {
                for (var key in app) {
                    \$('input[name=' + key + ']').val(app[key]);
                }
            }, <%=Request["Id"]%>);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntBody" runat="server">
    <div class="page-header">
        <div class="container-fluid">
            <div class="row-fluid">
                <div class="span12">
                    <h3>$=table.Name$ Edit<small></small></h3>
                </div>
            </div>
        </div>
    </div>

<form id="form" method="post" action="/Staff/Handlers/$=table.Name$Handler.ashx?method=Save" enctype="multipart/form-data" class="form-horizontal" role="form" autocomplete="off">

<input type="hidden" name="Id"/>

<div class="row">
    <div class="col-sm-9">
		$ 
		debugger;
		foreach(var colName in table.GetUIGroupColumns("")){
			var c = table.Columns[colName]; 
		$
        <div class="form-group">
            <label for="$=c.Name$" class="col-sm-3 control-label no-padding-right"> <%=Provider.TR("$=c.UIMetadata.DisplayName$")%> </label>
            <div class="col-sm-9">
				$=getControlHtml(c)$
            </div>
        </div>
    
        <div class="space-4"></div>			
		$ } $
	</div>
    <div class="col-sm-3">
    PUT HERE A PICTURE OR SOMETHING ELSE
	</div>
</div>

<div class="row">
	$ 
	foreach(var grup in table.GetUIGroups()) {
		if(!grup) continue;
	$
	<div class="col-xs-12 col-sm-6">
		<h3 class="header smaller lighter blue"><%=Provider.TR("$=grup$")%></h3>
		$ 
		foreach(var colName in table.GetUIGroupColumns(grup)){ 
			var c = table.Columns[colName];
		$
	        <div class="form-group">
	            <label for="$=c.Name$" class="col-sm-4 control-label no-padding-right"> <%=Provider.TR("$=c.UIMetadata.DisplayName$")%> </label>
	    		<div class="col-sm-8">
	    			$=getControlHtml(c)$
	            </div>
	        </div>		
		$ } $
	</div>
	$ } $
</div>

<div class="clearfix form-actions">
	<div class="text-right">
		<button class="btn btn-info" type="submit">
			<i class="icon-ok bigger-110"></i>
			Save
		</button>

		&nbsp; &nbsp; &nbsp;
		<button class="btn" type="button" onclick="history.go(-1)">
			<i class="icon-undo bigger-110"></i>
			Cancel
		</button>
	</div>
</div>

</form>

</asp:Content>
