<%@ Application Language="C#" %>
<%@ Import Namespace="Cinar.Database" %>
<%@ Import Namespace="Cinar.Entities" %>
<%@ Import Namespace="Cinar.Entities.Standart" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        setDb();
    }

    private void setDb()
    {
        CinarContext.Db = new Database(ConfigurationManager.AppSettings["sqlConnection"], (DatabaseProvider)Enum.Parse(typeof(DatabaseProvider), ConfigurationManager.AppSettings["sqlProvider"]));
        //CinarContext.ClientUser = new User() {Name = "Anonim", UserName = "anonim"};
    }

    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        if(Cinar.Entities.CinarContext.Db==null)
            setDb();
    }
</script>
