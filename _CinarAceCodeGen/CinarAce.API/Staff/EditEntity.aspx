<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Main.Master" AutoEventWireup="true" %>

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

    
    TODO: show controls
</asp:Content>
