<%@ WebHandler Language="C#" Class="DeleteEntity" %>

using System;
using System.Web;
using Cinar.CMS.Library;
using Cinar.CMS.Library.Entities;
using System.Reflection;
using Cinar.Database;


public class DeleteEntity : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "application/json";

        try
        {
            string entityName = context.Request["entityName"];
            int entityId = int.Parse(context.Request["id"]);

            SimpleBaseEntity entity = (SimpleBaseEntity)Provider.Database.Read(Provider.GetEntityType(entityName), entityId);
            
            entity.Delete();

            context.Response.Write(new AjaxResponse { IsError = false }.ToJSON());
        }
        catch (Exception ex)
        {
            context.Response.Write(new AjaxResponse { IsError = true, ErrorMessage=ex.Message }.ToJSON());
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}

