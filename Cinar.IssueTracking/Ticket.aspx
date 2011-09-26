<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" %>

<%@ Import Namespace="Cinar.Database" %>

<%@ Import Namespace="Cinar.Entities" %>
<%@ Import Namespace="Cinar.Entities.Standart" %>
<%@ Import Namespace="Cinar.Entities.IssueTracking" %>
<script runat="server">
    Ticket ticket = null;
    List<EntityHistory> history = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        bool isPostBack = Request["Id"] != null;
        if (isPostBack)
        {
            int id = int.Parse(Request["Id"]);

            if (Request["command"] == "delete")
            {
                CinarContext.Db.ExecuteNonQuery("delete from Ticket where Id={0}", id);
                Response.Redirect("/TicketList.aspx", true);
                return;
            }

            ticket = id> 0 ? CinarContext.Db.Read<Ticket>(id) : new Ticket();
            ticket.SetFieldsByPostData(Request.Form);
            ticket.Save();
            Response.Redirect("/TicketList.aspx", true);
            return;
        }
        else
        {
            if (Request["ticketId"] != null)
            {
                ticket = CinarContext.Db.Read<Ticket>(int.Parse(Request["ticketId"]));
                history = CinarContext.Db.ReadList<EntityHistory>(FilterExpression
                    .Where("EntityName", CriteriaTypes.Eq, "Ticket")
                    .And("EntityId", CriteriaTypes.Eq, ticket.Id)
                    .OrderBy("InsertDate")
                    .Desc()
                );
            }
            else
                ticket = new Ticket();
        }
    }

</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script type="text/javascript">
    function addNewItem(msg, control) {
        var n = prompt(msg);
        if (n) {
            control.append($("<option></option>").attr("value", n).text(n));
            control.val(n);
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <form id="f1" action="Ticket.aspx" method="post">
    <input type="hidden" name="Id" value="<%=ticket.Id %>" />
    <h1>
        <%=ticket.Id==0 ? "New":"Edit" %> Ticket</h1>
    <div id="ticket">
        <div class="bigInput fullWidth <%=ticket.Status %>">
            <input type="text" name="Name" value='<%=ticket.Id==0?"Enter ticket subject":Server.HtmlEncode(ticket.Name) %>'
                style="color: #CCC" onfocus="if(this.value=='Enter ticket subject') {this.value = '';this.style.color='';}"
                onblur="if(this.value=='') {this.value='Enter ticket subject';this.style.color='#CCC';}" />
        </div>
        <div class="controlWithLabel halfWidth">
            <span>Project</span>
            <select name="Project" id="Project">
                <%=CinarContext.Db.GetList<string>("select distinct Project from Ticket").Select(s => "<option value=\"" + s + "\"" + (ticket.Project == s ? " selected" : "") + ">" + s + "</option>").StringJoin()%>
            </select> <a href="#" onclick="addNewItem('Enter new project name', $(this).prev())">new</a>
        </div>
        <div class="controlWithLabel halfWidth">
            <span>Created On</span>
            <%=(ticket.Id == 0 ? DateTime.Now:ticket.InsertDate).ToString("dd MMMM yyyy")%>
        </div>
        <div class="controlWithLabel halfWidth">
            <span>Component</span>
            <select name="Component" id="Component">
                <%=CinarContext.Db.GetList<string>("select distinct Component from Ticket").Select(s => "<option value=\"" + s + "\"" + (ticket.Component == s ? " selected" : "") + ">" + s + "</option>").StringJoin()%>
            </select> <a href="#" onclick="addNewItem('Enter new component name', $(this).prev())">new</a>
        </div>
        <div class="controlWithLabel halfWidth">
            <span>Type</span>
            <select name="Type">
                <%=new[] { "Task", "Bug" }.Select(s => "<option value=\"" + s + "\"" + (ticket.Type == s ? " selected" : "") + ">" + s + "</option>").ToArray().StringJoin()%>
            </select>
        </div>
        <div class="controlWithLabel halfWidth">
            <span>Reported By</span>
            <select name="ReportedById">
                <option value="0"></option>
                <%=CinarContext.Db.ReadList<User>("select Id, Name, case when Id={0} then 'selected' else '' end as Selected from User", ticket.ReportedById).Select(u=>"<option value=\"#{Id}\" #{[\"Selected\"]}>#{Name}</option>".EvaluateAsTemplate(u)).StringJoin()%>
            </select>
        </div>
        <div class="controlWithLabel halfWidth">
            <span>Status</span>
            <select name="Status">
                <%=new[] { "New", "Accepted", "Rejected", "Resolved" }.Select(s => "<option value=\"" + s + "\"" + (ticket.Status == s ? " selected" : "") + ">" + s + "</option>").ToArray().StringJoin()%>
            </select>
        </div>
        <div class="controlWithLabel halfWidth">
            <span>Assigned To</span>
            <select name="AssignedToId">
                <option value="0"></option>
                <%=CinarContext.Db.ReadList<User>("select Id, Name, case when Id={0} then 'selected' else '' end as Selected from User", ticket.AssignedToId).Select(u=>"<option value=\"#{Id}\" #{[\"Selected\"]}>#{Name}</option>".EvaluateAsTemplate(u)).StringJoin()%>
            </select> <a href="#" onclick="$(this).prev().val(<%=CinarContext.ClientUser.Id %>)">me</a>
        </div>
        <div class="controlWithLabel halfWidth">
            <span>Priority</span>
            <select name="Priority">
                <%=new[] { "Low", "Normal", "High" }.Select(s => "<option value=\"" + s + "\"" + (ticket.Priority == s ? " selected" : "") + ">" + s + "</option>").ToArray().StringJoin()%>
            </select>
        </div>
        <div class="controlWithLabel fullWidth">
            <span>Description</span>
            <textarea name="Description" cols="114" rows="10"><%=Server.HtmlEncode(ticket.Description) %></textarea>
        </div>
        <input type="hidden" name="command" id="command" />
        <p style="margin: 20px">
            <a class="btnSave" href="#" onclick="$('#f1').submit()">
                <%=ticket.Id == 0 ? "Create" : "Save"%>
                Ticket</a> or <a href="/TicketList.aspx">Cancel</a>
            <%if (ticket.Id > 0) { %>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <a class="btnDel" href="#" onclick="if(confirm('Are you sure to delete this ticket?')){$('#command').val('delete'); $('#f1').submit();}">Delete</a>
            <% } %>
        </p>
    </div>
    <%if (history != null && history.Count > 0) { %>
    <div id="ticketHistory">
    <%foreach (EntityHistory entityHistory in history)
{
  %>
      <div class="changeset-wrap">
        <div class="thumb-col">
          <img src="/images/<%=string.IsNullOrWhiteSpace(CinarContext.ClientUser.Avatar) ? "no-picture.png": CinarContext.ClientUser.Avatar%>" alt="User picture">
        </div>
        <div class="changeset-col">
          <div class="changeset-header header-gray">
               
                By <b><%=entityHistory.InsertedBy.Name %></b> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                on <%=entityHistory.InsertDate.ToString("MMM dd, yyyy @ hh:mm") %>
                
                  <div class="changeset-description">
                        <%=Server.HtmlEncode(entityHistory.Details).Replace("\n","\n<br/>") %>
                  </div>
          </div>
        </div>
      </div>
  <%
} %>
    </div>
    <% } %>
    </form>
</asp:Content>
