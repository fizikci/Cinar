$
function getControlHtml(c){
	
	var str = "";
	if(c.UIMetadata.EditorType.ToString()=="LookUp" || c.UIMetadata.EditorType.ToString()=="ComboBox"){
		if(c.ReferenceColumn){
			var ref = c.ReferenceColumn.Table.Name;
			str = '
                <select ng-model="curr'+table.Name+'.'+c.Name+'" ng-options="i.Id as i.Name for i in '+ref+'s" class="col-sm-9">
                    <option value=""></option>
                </select>
                <input type="text" style="display:none" ng-model="curr'+table.Name+'.'+c.Name+'" name="'+c.Name+'" />
			';
		}
		else
		str = '
                <select ng-model="curr'+table.Name+'.'+c.Name+'" ng-options="i.Id as i.Name for i in XXX" class="col-sm-9">
                    <option value=""></option>
                </select>
                <input type="text" style="display:none" ng-model="curr'+table.Name+'.'+c.Name+'" name="'+c.Name+'" />
		';
	}
	if(c.UIMetadata.EditorType.ToString()=="TagEdit"){
		if(c.ReferenceColumn){
			var ref = c.References.Name;
			str = '
	                <select multiple="" class="width-80 chosen-select" ng-model="curr'+table.Name+'.'+c.Name+'" ng-options="i.Id as i.Name for i in '+ref+'s" data-placeholder="Select tags">
	                </select>
                	<input type="text" style="display:none" ng-model="curr'+table.Name+'.'+c.Name+'" name="'+c.Name+'" />
			';
		}
		else
			str = '
	                <select multiple="" class="width-80 chosen-select" ng-model="curr'+table.Name+'.'+c.Name+'" data-placeholder="Select tags">
	                    <option>One</option>
	                    <option>Two</option>
	                    <option>Three</option>
	                </select>
                	<input type="text" style="display:none" ng-model="curr'+table.Name+'.'+c.Name+'" name="'+c.Name+'" />
			';
	}
	if(c.UIMetadata.EditorType.ToString()=="TextEdit"){
		str = '
				<input type="text" ng-model="curr'+table.Name+'.'+c.Name+'" name="'+c.Name+'" placeholder="" class="col-sm-6" />
		';
	}
	if(c.UIMetadata.EditorType.ToString()=="MemoEdit"){
		str = '
				<textarea ng-model="curr'+table.Name+'.'+c.Name+'" name="'+c.Name+'" name="'+c.Name+'" rows="3" class="col-sm-8"></textarea>
		';
	}
	if(c.UIMetadata.EditorType.ToString()=="DateEdit"){
		str = '
                <input type="text" class="date-picker" ng-model="curr'+table.Name+'.'+c.Name+'" name="'+c.Name+'" data-date-format="dd-mm-yyyy"/>
				<span class="input-group-addon">
					<i class="icon-calendar bigger-110"></i>
				</span>
		';
	}
	if(c.UIMetadata.EditorType.ToString()=="CheckBox"){
		str = '
            <label class="col-sm-4">
            	<input ng-model="curr'+table.Name+'.'+c.Name+'" name="'+c.Name+'" class="ace ace-switch ace-switch-5" value="1" type="checkbox"/>
        		<span class="lbl"></span>
        	</label>
		';
	}
	if(c.UIMetadata.EditorType.ToString()=="NumberEdit"){
		str = '
            <input ng-model="curr'+table.Name+'.'+c.Name+'" name="'+c.Name+'" class="input-mini bkspinner" type="text" value="0"/>
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

<form id="form" class="form-horizontal" role="form" autocomplete="off">

<input type="text" ng-model="curr$=table.Name$.Id" name="Id" style="display:none"/>

<div class="row">
    <div class="col-sm-9">
		$ 
		foreach(var colName in table.GetUIGroupColumns("")){
			var c = table.Columns[colName]; 
			if(c.IsPrimaryKey) continue;
		$
        <div class="form-group">
            <label for="$=c.Name$" class="col-sm-3 control-label no-padding-right"> $=c.UIMetadata.DisplayName$ </label>
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
		<h3 class="header smaller lighter blue">$=grup$</h3>
		$ 
		foreach(var colName in table.GetUIGroupColumns(grup)){ 
			var c = table.Columns[colName];
			if(c.IsPrimaryKey) continue;
		$
	        <div class="form-group">
	            <label for="$=c.Name$" class="col-sm-4 control-label no-padding-right"> $=c.UIMetadata.DisplayName$ </label>
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
		<button class="btn btn-xs btn-primary" type="button" ng-click="save$=table.Name$()">
			<i class="icon-ok bigger-110"></i>
			Save
		</button>
		&nbsp; 
		<button class="btn btn-xs btn-info" type="button" ng-click="cancelEdit$=table.Name$()">
			<i class="icon-undo bigger-110"></i>
			Cancel
		</button>
	</div>
</div>

</form>

</asp:Content>
