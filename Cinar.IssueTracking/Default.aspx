<%@ Page Title="" Language="C#" %>
<%@ Import Namespace="Cinar.Entities" %>
<%@ Import Namespace="Cinar.Entities.IssueTracking" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        if (CinarContext.ClientUser.Id > 0)
        {
            ProjectUser aProjectUser = CinarContext.Db.Read<ProjectUser>("SELECT TOP 1 * FROM ProjectUser WHERE UserId={0}", CinarContext.ClientUser.Id);
            if (aProjectUser != null)
            {
                Response.Redirect("/TicketList.aspx", true);
                return;
            }

            Response.Redirect("/Member.aspx", true);
            return;
        }

        Response.Redirect("/Login.aspx", true);
    }
</script>
