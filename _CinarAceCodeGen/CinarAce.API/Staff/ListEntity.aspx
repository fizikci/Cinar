<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Staff/Main.Master" %>
<%@ Import Namespace="$=db.Name$.API" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntHead" runat="server">
    <script>
        \$(function () {
            $=table.Name$Handler.GetList(function (list) {
                bindTable({
                    list: list,
                    tableId: "#table-entity",
                    columns: [
                    $ 
                    foreach(var c in table.Columns) { 
                    	if(!c.UIMetadata.ShowInGrid) continue;
                    $
                        {
                            "mData": "$=c.Name$",
                            "sTitle": "$=c.UIMetadata.DisplayName$",
                            "mRender": function (data, type, full) {
                                return '<a href="/Staff/Edit$=table.Name$.aspx?Id=' + full.Id + '">' + full.Name + '</a>';
                            }
                        },
                    $ 
                    } 
                    $
                        {
                            "mData": null,
                            "mRender": function (data, type, full) {
                                return '<a class="green" href="/Staff/Edit$=table.Name$.aspx?Id=' + full.Id + '"><i class="icon-pencil bigger-130"></i></a> &nbsp; ' +
                                    '<a class="red" href="javascript:$=table.Name$Handler.DeleteById(function(){location.reload();}, ' + full.Id + ')"><i class="icon-trash bigger-130" ></i></a>';
                            }
                        }
                    ]
                });
            }, false, 20, 0);
        });

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntBody" runat="server">



    <div class="page-header">
        <h1><%=Provider.TR("$=table.Name$s") %>
		   
            <small>
                <i class="icon-double-angle-right"></i>
                <%=Provider.TR("is as is") %>
		    </small>
        </h1>
    </div>

        <div class="clearfix form-actions" style="margin: 0; padding: 0;">

        <button class="btn btn-info" type="button" onclick="location.href='/Staff/Edit$=table.Name$.aspx';">
            <i class="icon-add bigger-110"></i>
            <%=Provider.TR("Add New") %>
	   
        </button>
    </div>

    <div class="table-responsive">
        <div id="table-storage_wrapper" class="dataTables_wrapper" role="grid">
            <table id="table-entity" class="table table-striped table-bordered table-hover dataTable" aria-describedby="table-storage_info">
            </table>
        </div>
    </div>

</asp:Content>
