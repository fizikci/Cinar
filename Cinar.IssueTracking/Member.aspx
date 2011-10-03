<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" %>

<%@ Import Namespace="Cinar.Database" %>

<%@ Import Namespace="Cinar.Entities" %>
<%@ Import Namespace="Cinar.Entities.IssueTracking" %>
<%@ Import Namespace="Cinar.Entities.Standart" %>
<%@ Import Namespace="System.Data" %>
<script runat="server">
    List<Project> projectList;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        projectList = CinarContext.Db.ReadList<Project>("select * from Project where ID in (select ProjectId from ProjectUser WHERE UserId = {0})", CinarContext.ClientUser.Id);
    }
    bool rowEven = false;

</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script>
    function filterTickets() {
        $("#findUserDialog").dialog({
            height: 420,
            modal: true
        });
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">

    <div class="memberProfile">
        <div class="user-pic-wrap">
            <div class="avatar">
                <% 
                if (!string.IsNullOrEmpty(CinarContext.ClientUser.Avatar))
                {
                %>
                <img src="<%=CinarContext.ClientUser.Avatar %>" width="100" height="100" />
                <%
                }
                %>
            </div>
            <a class="edit-profile" href="/Membership.aspx">Profili Düzenle</a><br/>
            <a class="logout" href="/Login.aspx?logout=1">Çıkış</a>
        </div>
        <div class="user-status-wrap">
            <h1><%=CinarContext.ClientUser.Name %></h1>
        </div>
        <div style="clear: both"></div>
        <br />
    </div>

    <div id="ticketList">
        <div class='groupTitle'><h3>Projects</h3></div>
        <table>
            <tr>
                    <th class="Name" style="min-width:50%">Project Name</th><th class="CreatedBy">Created By</th><th class="TeamMembers" style="min-width:50%">Team Members</th>
            </tr>
            <% foreach (Project p in projectList)
               {%>
            <tr class='<%=(rowEven = !rowEven)?"even":"odd" %>'>
                <td><span onclick="location.href = '/TicketList.aspx?projectId=<%=p.Id %>';" class="link"><%=p.Name %></span></td>
                <td><%=p.CreatedBy.Name %></td>
                <td><form action="DoCommand.ashx" method="post" id="f<%=p.Id %>">
                        <%=p.GetTeamMembers().Select(u=>u.Name).StringJoin(", ") %> 
                        <input type="hidden" name="command" value="addMemberToProject" />
                        <input type="hidden" name="projectId" value="<%=p.Id %>" />
                        <select name="userId">
                            <%=CinarContext.Db.ReadList<User>("select Id, Name from User where Deleted=0 AND Id NOT IN (select UserId from ProjectUser WHERE ProjectId={0})", p.Id).Select(u => "<option value=\"#{Id}\">#{Name}</option>".EvaluateAsTemplate(u)).StringJoin()%>
                        </select>
                        <span class="link" onclick="$('#f<%=p.Id %>').submit()">Add</span>
                    </form></td>
            </tr>
            <%} %>
        </table>
        <form action="DoCommand.ashx" method="post">
        <input type="hidden" name="command" value="addProject" />
        New Project: <input type="text" name="projectName" /> <input type="submit" class="btnAdd" value="Add"/><br />
        </form>
        <br />
    </div>

    <div class="memberProfile">
        <% 
        DataTable dt = CinarContext.Db.GetDataTable(@"
            SELECT
	            eh.InsertDate,
                eh.EntityId,
	            CASE WHEN eh.Operation=0 THEN 'added' WHEN eh.Operation=1 THEN 'updated' ELSE 'deleted' END AS Operation,
	            u.Name AS UserName,
	            t.Name AS TicketName,
	            p.Id AS ProjectId,
	            p.Name AS ProjectName
            FROM
	            EntityHistory eh
	            INNER JOIN User u ON eh.InsertUserId = u.Id
	            INNER JOIN Ticket t ON eh.EntityName = 'Ticket' AND eh.EntityId = t.Id
	            INNER JOIN Project p ON p.Id = t.ProjectId AND t.ProjectId IN (SELECT ProjectId FROM ProjectUser WHERE UserId = {0})
            ORDER BY
	            eh.InsertDate DESC", CinarContext.ClientUser.Id);
        ArrayList alMonths = new ArrayList();
        %>
        <% if (dt != null && dt.Rows.Count > 0){%> 
        <div class='groupTitle'><h3>Latest News</h3></div> 
        <% }%>
        <table>
            <%
            foreach (DataRow dr in dt.Rows){ 
                DateTime tarih = (DateTime)dr["InsertDate"];
                string dayMonth = tarih.ToString("dd MMMM");
            %>
            <tr>
                <td>
                    <%=alMonths.Contains(dayMonth) ? "":("<span>"+tarih.ToString("dd MMMM")+"</span>") %>
                </td>
                <th>
                    <%=tarih.ToString("hh:mm") %>.
                </th>
                <td>
                    <%=dr["UserName"] %> <%=dr["Operation"] %>:
                    <a href="Ticket.aspx?ticketId=<%=dr["EntityId"] %>"><%=dr["TicketName"] %></a> of <a href="TicketList.aspx?ticketId=<%=dr["ProjectId"] %>"><%=dr["ProjectName"] %></a>
                </td>
            </tr>
            <% 
                if (!alMonths.Contains(dayMonth))
                    alMonths.Add(dayMonth);
            } 
            %>
        </table>
    </div>


</asp:Content>
