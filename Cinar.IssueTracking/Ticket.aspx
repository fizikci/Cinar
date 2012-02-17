<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" %>

<%@ Import Namespace="Cinar.Database" %>
<%@ Import Namespace="Cinar.Entities" %>
<%@ Import Namespace="Cinar.Entities.Standart" %>
<%@ Import Namespace="Cinar.Entities.IssueTracking" %>

<script runat="server">
    Ticket ticket = null;
    List<EntityHistory> history = null;
    List<User> teamMembers = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        bool isPostBack = Request["Id"] != null;
        int projectId = 0;
        int.TryParse(Request["projectId"], out projectId);
        
        if (isPostBack)
        {
            int id = int.Parse(Request["Id"]);

            if (Request["command"] == "delete")
            {
                CinarContext.Db.ExecuteNonQuery("delete from Ticket where Id = {0}", id);
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
            {
                ticket = new Ticket();
                if (projectId>0)
                    ticket.ProjectId = projectId;
                else
                    ticket.ProjectId = CinarContext.Db.GetInt("select min(ProjectId) from ProjectUser where UserId={0}", CinarContext.ClientUser.Id);
            }
            teamMembers = ticket.GetProject().GetTeamMembers();
        }

        if (teamMembers == null || teamMembers.Count == 0)
        {
            teamMembers = new List<User>();
            teamMembers.Add(CinarContext.ClientUser);
        }
    }

</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script type="text/javascript">
    function addNewComponent(control) {
        var n = prompt('Enter new component name');
        if (n) {
            control.append($("<option></option>").attr("value", n).text(n));
            control.val(n);
        }
    }
    function addNewProject(control) {
        var n = prompt('Enter new project name');
        if (n) {
            var data = { projectName: n, command: 'addProject', noRedirect: 1 };
            $.post("/DoCommand.ashx", data, function (resp) {
                resp = eval("(" + resp + ")");
                if (resp.success) {
                    control.append($("<option></option>").attr("value", resp.data.Id).text(n));
                    control.val(resp.data.Id);
                }
            });
        }
    }
    var ticket = <%=ticket.ToJSON() %>;
    function projectChanged(projectId) {
        var selComponent = $('#Component'); selComponent.html("");
        var selAssignedTo = $('#AssignedTo'); selAssignedTo.html("");
        var selReportedBy = $('#ReportedBy'); selReportedBy.html("");
        if(projectId<=0)
            return;

        var data = { projectId: projectId, command: 'getUsersAndComponents' };
        $.post("/DoCommand.ashx", data, function (resp) {
            resp = eval("(" + resp + ")");
            if (resp.success) {
                for (var i = 0; i < resp.components.length; i++)
                    selComponent.append($("<option></option>").attr("value", resp.components[i]).text(resp.components[i]));
                for (var i = 0; i < resp.users.length; i++) {
                    selAssignedTo.append($("<option></option>").attr("value", resp.users[i].Id).text(resp.users[i].Name));
                    selReportedBy.append($("<option></option>").attr("value", resp.users[i].Id).text(resp.users[i].Name));
                }
                selComponent.val(ticket.Component);
                selAssignedTo.val(ticket.AssignedToId);
                selReportedBy.val(ticket.ReportedById);
            }
        });
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div>
        <a class="btnBack" href="/TicketList.aspx">Return to List</a>
    </div>
    <form id="f1" action="Ticket.aspx" method="post">
    <input type="hidden" name="Id" value="<%=ticket.Id %>" />
    <h1>
        <%=ticket.Id==0 ? "New":"Edit" %> Ticket</h1>
    <div id="ticket">
        <div class="bigInput fullWidth <%=ticket.Status %>">
            <input type="text" name="Name" value='<%=ticket.Id==0?"Enter ticket subject":Server.HtmlEncode(ticket.Name) %>'
                style='color: #<%=ticket.Id>0 ? "444":"ccc"%>' onfocus="if(this.value=='Enter ticket subject') {this.value = '';this.style.color='#444';}"
                onblur="if(this.value=='') {this.value='Enter ticket subject';this.style.color='#CCC';}" />
        </div>
        <div class="controlWithLabel halfWidth">
            <span>Project</span>
            <select name="ProjectId" id="ProjectId" onchange="projectChanged($(this).val())">
                <%=CinarContext.Db.ReadList<Project>("select Id, Name from Project where Id in (SELECT ProjectId FROM ProjectUser WHERE UserId={0})", CinarContext.ClientUser.Id).Select(p=>"<option value=\""+p.Id+"\" "+(p.Id==ticket.ProjectId?"selected":"")+">"+p.Name+"</option>").StringJoin()%>
            </select> <a href="#" onclick="addNewProject($(this).prev())">new</a>
        </div>
        <div class="controlWithLabel halfWidth">
            <span>Created On</span>
            <%=(ticket.Id == 0 ? DateTime.Now:ticket.InsertDate).ToString("dd MMMM yyyy")%>
        </div>
        <div class="controlWithLabel halfWidth">
            <span>Component</span>
            <select name="Component" id="Component">
                <%=CinarContext.Db.GetList<string>("select distinct Component from Ticket where ProjectId = {0}", ticket.ProjectId).Select(s => "<option value=\"" + s + "\"" + (ticket.Component == s ? " selected" : "") + ">" + s + "</option>").StringJoin()%>
            </select> <a href="#" onclick="addNewComponent($(this).prev())">new</a>
        </div>
        <div class="controlWithLabel halfWidth">
            <span>Type</span>
            <select name="Type">
                <%=Enum.GetNames(typeof(TicketType)).Select(s => "<option value=\"" + s + "\"" + (ticket.Type.ToString() == s ? " selected" : "") + ">" + s + "</option>").ToArray().StringJoin()%>
            </select>
        </div>
        <div class="controlWithLabel halfWidth">
            <span>Reported By</span>
            <select name="ReportedById" id="ReportedBy">
                <option value="0"></option>
                <%=teamMembers.Select(u=>"<option value=\""+u.Id+"\" "+(u.Id==ticket.ReportedById?"selected":"")+">"+u.Name+"</option>").StringJoin()%>
            </select>
        </div>
        <div class="controlWithLabel halfWidth">
            <span>Status</span>
            <select name="Status">
                <%=Enum.GetNames(typeof(TicketStatus)).Select(s => "<option value=\"" + s + "\"" + (ticket.Status.ToString() == s ? " selected" : "") + ">" + s + "</option>").ToArray().StringJoin()%>
            </select>
        </div>
        <div class="controlWithLabel halfWidth">
            <span>Assigned To</span>
            <select name="AssignedToId" id="AssignedTo">
                <option value="0"></option>
                <%=teamMembers.Select(u=>"<option value=\""+u.Id+"\" "+(u.Id==ticket.AssignedToId?"selected":"")+">"+u.Name+"</option>").StringJoin()%>
            </select> <a href="#" onclick="$(this).prev().val(<%=CinarContext.ClientUser.Id %>)">me</a>
        </div>
        <div class="controlWithLabel halfWidth">
            <span>Priority</span>
            <select name="Priority">
                <%=Enum.GetNames(typeof(TicketPriority)).Select(s => "<option value=\"" + s + "\"" + (ticket.Priority.ToString() == s ? " selected" : "") + ">" + s + "</option>").ToArray().StringJoin()%>
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
               
                By <b><%=entityHistory.InsertedBy.Name %></b>
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
