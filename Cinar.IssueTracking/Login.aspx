<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" %>

<%@ Import Namespace="Cinar.Entities" %>
<%@ Import Namespace="Cinar.Entities.Standart" %>
<%@ Import Namespace="Cinar.Entities.IssueTracking" %>
<script runat="server">
    
    string alertError = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Request["logout"]))
        {
            Session.Clear();
        }

        if(Request["btn"]=="Login" && !string.IsNullOrWhiteSpace(Request["Email"]))
        {
            User user = CinarContext.Db.Read<User>("UserName={0} AND Password={1} AND Deleted=0", Request["Email"], Request["Passwd"]);
            if (user != null)
            {
                CinarContext.ClientUser = user;
                Response.Redirect("/Default.aspx", true);
            }
            else
                alertError = "No such user";
        }

        if (Request["btn"] == "Sign Up")
        {
            User user = CinarContext.Db.Read<User>("UserName={0}", Request["Email"]);
            if (user == null)
            {
                if (string.IsNullOrWhiteSpace(Request["Passwd"]))
                {
                    alertError = "Please provide your email address";
                    return;
                }
                if (string.IsNullOrWhiteSpace(Request["Name"]))
                {
                    alertError = "Please provide your name";
                    return;
                }
                user = new User();
                user.UserName = Request["Email"];
                user.Password = Request["Passwd"];
                user.Name = Request["Name"];
                user.Save();
                CinarContext.ClientUser = user;
                Response.Redirect("/Default.aspx", true);
            }
            else
                alertError = "There is already a user with the chosen email.";
        }
    }

</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">

<%if (alertError!="")
  {%>
  <script>      $("<div title=\"Error\"><%=alertError.ToJS() %></div>").dialog({modal:true});</script>
<%
  }%>
<div class="ui-widget-content ui-corner-all" id="login_form" style="margin-left:auto;margin-right:auto;margin-top:200px;width:250px;text-align:center;padding:4px">
<div class="ui-widget-header ui-corner-all" style="padding:4px">Cinar Issue Tracking</div>
<form id="fLogin" method="post" action="Login.aspx">
<input type="hidden" name="Name" id="nameField"/>
<br />
Email:<br />
<input type="text" name="Email" class="ui-state-default ui-corner-all" style="width:230px" /><br />
<br />
Password:<br />
<input type="password" name="Passwd" class="ui-state-default ui-corner-all" style="width:230px" /><br />
<p align="center"><input type="submit" name="btn" value="Login" /> <input type="submit" name="btn" value="Sign Up" onclick="$('#nameField').val(prompt('Please enter your name'))"/></p>
</form>
</div></asp:Content>
