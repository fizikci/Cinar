<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" %>

<%@ Import Namespace="Cinar.Entities" %>
<%@ Import Namespace="Cinar.Entities.IssueTracking" %>
<%@ Import Namespace="Cinar.Entities.Standart" %>
<script runat="server">
    TicketQuery query {
        get
        {
            return Session["query"] as TicketQuery;
        }
        set {
            Session["query"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int queryId;
        int.TryParse(Request["queryId"], out queryId);
        if (query != null && queryId == 0)
            queryId = query.Id;
         
        int projectId;
        int.TryParse(Request["projectId"], out projectId);

        if (query == null)
        {
            query = CinarContext.Db.Read<TicketQuery>(1);
        }

        if (Request["command"] == "filter")
        {
            query.SetFieldsByPostData(Request.Form);
        }
        else if (Request["command"] == "save")
        {
            query.SetFieldsByPostData(Request.Form);
            query.Save();
        }
        else if (Request["command"] == "saveAs")
        {
            query.SetFieldsByPostData(Request.Form);
            query.Id = 0;
            query.Save();
        }

        if(queryId!=query.Id)
        {
            if (queryId == 0) queryId = 1;
            query = CinarContext.Db.Read<TicketQuery>(queryId);
        }

        if (projectId > 0)
            query.ValProjectId = projectId;
    }
    
    bool rowEven = false;

    private string getTd(Ticket ticket, string fieldName)
    {
        object val = ticket.GetMemberValue(fieldName);
        switch (fieldName)
        {
            case "Status":
                val = "<div class=\"statusIndicator " + val + "\"></div> " + val;
                break;
            case "Name":
                val = "<span class=\"link\" onclick=\"location.href = '/Ticket.aspx?ticketId=" + ticket.Id + "';\"><img src=\"/images/" + ticket.Type + ".png\"/> " + val + "</span>";
                break;
            case "Id":
                val = "<b>#" + ticket.Id + "</b>";
                break;
            case "Type":
                val = "<img src=\"/images/" + ticket.Type + ".png\"/> " + ticket.Type;
                break;
        }
        return "<td>" + val + "</td>";
    }

</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script>
    function filterTickets() {
        $("#editTicketQuery").dialog({
            height: 450,
            modal: true
        });
    }
    var query = <%=query.ToJSON() %>;
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
                selComponent.val(query.ValComponent);
                selAssignedTo.val(query.ValAssignedToId);
                selReportedBy.val(query.ValReportedById);
            }
        });
    }</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div>
        <a class="btnAdd" href="/Ticket.aspx?projectId=<%=query.ValProjectId %>">New Ticket</a>
        <a class="btnFilter" href="#" onclick="return filterTickets()">Filter Tickets</a>
    </div>
    <h1 style="float:left"><%=query.Name %></h1>
    <div style="float:right">Select Query: <select id="selQuery" onchange="location.href='TicketList.aspx?queryId='+$(this).val();">
                        <%=CinarContext.Db.ReadList<TicketQuery>("select Id, Name, case when Id={0} then 'selected' else '' end as Selected from TicketQuery where InsertUserId={1} or InsertUserId=0;", query.Id, CinarContext.ClientUser.Id).Select(u => "<option value=\"#{Id}\" #{[\"Selected\"]}>#{Name}</option>".EvaluateAsTemplate(u)).StringJoin()%>
                    </select> <a href="#" onclick="filterTickets()">edit</a></div>
    <div style="clear:both"></div>
    <div id="ticketList">
    <%=query.ValProjectId>0 ? "<h2>Project: "+ query.Project+"</h2>":"" %>
    <%
        List<Ticket> list = query.ExecuteQuery(0);
        List<string> distinctGroupBy = new List<string>() { "" };
        string showFields = query.ShowFields.SplitWithTrim(',').StringJoin(",");
        if (!string.IsNullOrWhiteSpace(query.GroupByField))
        {
            distinctGroupBy = list.Select(t => t.GetMemberValue(query.GroupByField) ?? "").Select(t=>t.ToString()).Distinct().ToList();
            showFields = showFields.Replace("," + query.GroupByField, "").Replace(query.GroupByField + ",", "");
        }
        
        foreach(var groupByValue in distinctGroupBy.OrderBy(x=>x))
    {
        List<Ticket> groupList = list;
        if (groupByValue != "")
        {
            Response.Write("<div class='groupTitle'><h3>" + groupByValue + "</h3></div>");
            groupList = list.Where(t => { var v = t.GetMemberValue(query.GroupByField) ?? ""; return groupByValue == v.ToString(); }).ToList();
        }

%>
        <table>
            <tr>
                    <%=showFields.Split(',').Select(f => "<th class=\"" + f + "\">" + f + "</th>").StringJoin()%>
            </tr>
            <% foreach (Ticket t in groupList)
               {%>
            <tr class='<%=(rowEven = !rowEven)?"even":"odd" %>'>
                <%= showFields.Split(',').Select(fieldName=>getTd(t, fieldName)).StringJoin()%>
            </tr>
            <%} %>
        </table>
    <% } %>
        <p align="right"><br />Total Hours: <%=(decimal)list.Sum(t=>t.RealMinutes)/60 %></p>
    </div>
    <div id="editTicketQuery" style="display:none" title="Filter Tickets">
        <form id="f1" action="TicketList.aspx" method="post">
        <table>
            <tr>
                <td>
                    Subject like
                </td>
                <td>
                    <input type="text" name="ValName" value='<%=query.ValName %>' />
                </td>
            </tr>
            <tr>
                <td>
                    Status is
                </td>
                <td>
                    <select name="ValStatus">
                        <option value=""></option>
                        <%=new[] { "New", "Accepted", "Rejected", "Resolved" }.Select(s => "<option value=\"" + s + "\"" + (query.ValStatus == s ? " selected" : "") + ">" + s + "</option>").ToArray().StringJoin()%>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Reported by
                </td>
                <td>
                    <select name="ValReportedById" id="ReportedBy">
                        <option value="0">[all]</option>
                        <%=CinarContext.Db.ReadList<User>("select Id, Name, case when Id={0} then 'selected' else '' end as Selected from User where Id in (select UserId from ProjectUser where UserId={1})", query.ValReportedById, CinarContext.ClientUser.Id).Select(u=>"<option value=\"#{Id}\" #{[\"Selected\"]}>#{Name}</option>".EvaluateAsTemplate(u)).StringJoin()%>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Priority is
                </td>
                <td>
                    <select name="ValPriority">
                        <option value=""></option>
                        <%=new[] { "Low", "Normal", "High" }.Select(s => "<option value=\"" + s + "\"" + (query.ValPriority == s ? " selected" : "") + ">" + s + "</option>").ToArray().StringJoin()%>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Assigned to
                </td>
                <td>
                    <select name="ValAssignedToId" id="AssignedTo">
                        <option value="0">[all]</option>
                        <%=CinarContext.Db.ReadList<User>("select Id, Name, case when Id={0} then 'selected' else '' end as Selected from User where Id in (select UserId from ProjectUser where UserId={1})", query.ValAssignedToId, CinarContext.ClientUser.Id).Select(u => "<option value=\"#{Id}\" #{[\"Selected\"]}>#{Name}</option>".EvaluateAsTemplate(u)).StringJoin()%>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Project is
                </td>
                <td>
                    <select name="ValProjectId" id="ProjectId" onchange="projectChanged($(this).val())">
                        <option value="0">[all]</option>
                        <%=CinarContext.Db.ReadList<Project>("select Id, Name from Project where Id in (SELECT ProjectId FROM ProjectUser WHERE UserId={0})", CinarContext.ClientUser.Id).Select(p=>"<option value=\""+p.Id+"\" "+(p.Id==query.ValProjectId?"selected":"")+">"+p.Name+"</option>").StringJoin()%>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Component is
                </td>
                <td>
                    <select name="ValComponent" id="Component">
                        <option value=""></option>
                        <%=CinarContext.Db.GetList<string>("select distinct Component from Ticket where ProjectId = {0}", query.ValProjectId).Select(s => "<option value=\"" + s + "\"" + (query.ValComponent == s ? " selected" : "") + ">" + s + "</option>").StringJoin()%>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Description like
                </td>
                <td>
                    <input type="text" name="ValDescription" value='<%=query.ValDescription %>' />
                </td>
            </tr>
            <tr>
                <td colspan="2"><hr /></td>
            </tr>
            <tr>
                <td>
                    Show Fields
                </td>
                <td>
                    <input type="text" name="ShowFields" value='<%=query.ShowFields %>' />
                </td>
            </tr>
            <tr>
                <td>
                    Group By
                </td>
                <td>
                    <select name="GroupByField">
                        <option value=""></option>
                        <%=new[] { "Name", "Status", "Priority", "Project", "Component", "AssignedTo", "CreatedOn" }.Select(s => "<option value=\"" + s + "\"" + (query.GroupByField == s ? " selected" : "") + ">" + s + "</option>").ToArray().StringJoin()%>
                    </select>
                </td>
            </tr>
        </table>
        <input type="hidden" name="queryId" value="<%=query.Id %>" />
        <input type="hidden" name="command" id="command" value="filter" />
        <p align="right">
            <a class="btnFilter" href="#" onclick="$('#command').val('filter'); $('#f1').submit()">Filter</a>
            <a class="btnSave" href="#" onclick="$('#command').val('save'); $('#f1').submit()">Save</a>
        </p>
        <fieldset><legend>Save as</legend>
            <input type="text" name="Name" value='<%=query.Name %>' />
            <a class="btnSave" href="#" onclick="$('#command').val('saveAs'); $('#f1').submit()">Save</a>
        </fieldset>
        </form>
    </div>
</asp:Content>
