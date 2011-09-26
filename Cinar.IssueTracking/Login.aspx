<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" %>

<%@ Import Namespace="Cinar.Entities" %>
<%@ Import Namespace="Cinar.Entities.Standart" %>
<%@ Import Namespace="Cinar.Entities.IssueTracking" %>
<script runat="server">
    
    bool alertError = false;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!string.IsNullOrWhiteSpace(Request["Email"]))
        {
            User user = CinarContext.Db.Read<User>("UserName={0} AND Password={1} AND Deleted=0", Request["Email"], Request["Passwd"]);
            if (user != null)
            {
                CinarContext.ClientUser = user;
                Response.Redirect("/Default.aspx", true);
            }
            else
                alertError = true;
        }
    }

</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">

<%if (alertError)
  {%>
  <script>      $("<div title=\"Error\">Undefined user</div>").dialog({modal:true});</script>
<%
  }%>
<div class="ui-widget-content ui-corner-all" id="login_form" style="margin-left:auto;margin-right:auto;margin-top:200px;width:250px;text-align:center;padding:4px">
<div class="ui-widget-header ui-corner-all" style="padding:4px">Cinar Issue Tracking</div>
<form id="fLogin" method="post" action="Login.aspx">
<br />
Email:<br />
<input type="text" name="Email" class="ui-state-default ui-corner-all" style="width:230px" /><br />
<br />
Password:<br />
<input type="password" name="Passwd" class="ui-state-default ui-corner-all" style="width:230px" /><br />
<p align="center"><input type="submit" value="Login" /></p>
</form>
</div></asp:Content>
