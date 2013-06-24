<%@ WebHandler Language="C#" Class="GetEntityList" %>

using System;
using System.Web;
using Cinar.CMS.Library;
using Cinar.CMS.Library.Entities;
using System.Reflection;
using Cinar.Database;


public class GetEntityList : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";

        try
        {
            string entityName = context.Request["entityName"];

            AjaxResponse res = new AjaxResponse();
            res.Data = Provider.Database.ReadList(Provider.GetEntityType(entityName), FilterExpression.Empty);

            context.Response.Write(res.ToJSON());
        }
        catch (Exception ex)
        {
            context.Response.Write(new AjaxResponse { IsError = true, ErrorMessage = ex.Message }.ToJSON());
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}