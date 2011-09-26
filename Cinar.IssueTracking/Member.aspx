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
            <a class="edit-profile" href="/Uyelik.aspx">Profili Düzenle</a><br/>
            <a class="logout" href="/DoLogin.ashx?logout=1">Çıkış</a>
        </div>
        <div class="user-status-wrap">
            <h1><%=CinarContext.ClientUser.Name %></h1>
        </div>
        <div style="clear: both">
        </div>
        <br />



    <div id="ticketList">
        <div class='groupTitle'><h3>Projects</h3></div>
        <table>
            <tr>
                    <th class="Name">Project Name</th><th class="Name">Created By</th><th class="Name">Team Members</th>
            </tr>
            <% foreach (Project p in projectList)
               {%>
            <tr class='<%=(rowEven = !rowEven)?"even":"odd" %>'>
                <td><%=p.Name %></td><td><%=p.CreatedBy.Name %></td><td><%=p.GetTeamMembers().Select(u=>u.Name).StringJoin(", ") %><input type="text" /></td>
            </tr>
            <%} %>
        </table>
        New Project: <input type="text" name="projectName" /> <button class="btnAdd" onclick="...">Add</button><br />
        <br />
    </div>

        <div class="memberProfile">
        <div class='groupTitle'><h3>Latest News</h3></div>
        <% 
        DataTable dt = CinarContext.Db.GetDataTable(@"
            SELECT
	            eh.InsertDate,
	            eh.Operation,
	            u.Name AS UserName,
	            t.Name as TicketName,
	            p.Name as ProjectName
            FROM
	            EntityHistory eh
	            LEFT JOIN User u ON eh.InsertUserId = u.Id
	            left join Ticket t ON eh.EntityName = 'Ticket' AND eh.EntityId = t.Id
	            LEFT JOIN Project p ON p.Id = t.ProjectId AND t.ProjectId IN (select ProjectId from ProjectUser WHERE UserId = {0})
            ORDER BY
	            eh.InsertDate DESC", CinarContext.ClientUser.Id);
        ArrayList alMonths = new ArrayList();
        %>
        <table>
            <% foreach (DataRow dr in dt.Rows){ 
            DateTime tarih = (DateTime)dr["InsertDate"];
            %>
            <tr>
                <td>
                    <%=alMonths.Contains(tarih.Month) ? "":("<span>"+tarih.ToString("MMMM")+"</span>") %>
                </td>
                <th>
                    <%=tarih.ToString("dd") %>.
                </th>
                <td>
                    "<%=dr["UserName"] %>" <%=dr["Operation"] %>ed:
                    "<%=dr["TicketName"] %>" of
                    <b><%=dr["ProjectName"] %></b>
                </td>
            </tr>
            <% 
        if (!alMonths.Contains(tarih.Month))
            alMonths.Add(tarih.Month);
        } 
            %>
        </table>

    </div>

    </div>

</asp:Content>
