<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" %>

<%@ Import Namespace="Cinar.Entities" %>
<%@ Import Namespace="Cinar.Entities.IssueTracking" %>
<%@ Import Namespace="Cinar.Entities.Standart" %>
<script runat="server">
    TicketQuery query = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        int queryId;
        int.TryParse(Request["queryId"], out queryId);
        if (queryId == 0) queryId = 1;
        query = CinarContext.Db.Read<TicketQuery>(queryId);
        query.SetFieldsByPostData(Request.Form);

        if (Request["command"] == "save")
            query.Save();
        else if (Request["command"] == "saveAs")
        {
            query.Id = 0;
            query.Save();
        }
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
                val = "<span class=\"ticketName\" onclick=\"location.href = '/Ticket.aspx?ticketId=" + ticket.Id + "';\"><img src=\"/images/" + ticket.Type + ".png\"/> " + val + "</span>";
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
            height: 420,
            modal: true
        });
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div>
        <a class="btnAdd" href="/Ticket.aspx">New Ticket</a>
        <a class="btnFilter" href="#" onclick="return filterTickets()">Filter Tickets</a>
    </div>
    <h1 style="float:left"><%=query.Name %></h1>
    <div style="float:right">Select Query: <select id="selQuery" onchange="location.href='TicketList.aspx?queryId='+$(this).val();">
                        <%=CinarContext.Db.ReadList<TicketQuery>("select Id, Name, case when Id={0} then 'selected' else '' end as Selected from TicketQuery", query.Id).Select(u => "<option value=\"#{Id}\" #{[\"Selected\"]}>#{Name}</option>".EvaluateAsTemplate(u)).StringJoin()%>
                    </select> <a href="#" onclick="filterTickets()">edit</a></div>
    <div style="clear:both"></div>
    <div id="ticketList">
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
        if (groupByValue != null)
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
                    <select name="ValReportedById">
                        <option value="0"></option>
                        <%=CinarContext.Db.ReadList<User>("select Id, Name, case when Id={0} then 'selected' else '' end as Selected from User", query.ValReportedById).Select(u=>"<option value=\"#{Id}\" #{[\"Selected\"]}>#{Name}</option>".EvaluateAsTemplate(u)).StringJoin()%>
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
                    <select name="ValAssignedToId">
                        <option value="0"></option>
                        <%=CinarContext.Db.ReadList<User>("select Id, Name, case when Id={0} then 'selected' else '' end as Selected from User", query.ValAssignedToId).Select(u=>"<option value=\"#{Id}\" #{[\"Selected\"]}>#{Name}</option>".EvaluateAsTemplate(u)).StringJoin()%>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Component is
                </td>
                <td>
                    <select name="ValComponent">
                        <option value=""></option>
                        <%=CinarContext.Db.GetList<string>("select distinct Component from Ticket").Select(s => "<option value=\"" + s + "\"" + (query.ValComponent == s ? " selected" : "") + ">" + s + "</option>").StringJoin()%>
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
        <input type="hidden" name="command" id="command" />
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
