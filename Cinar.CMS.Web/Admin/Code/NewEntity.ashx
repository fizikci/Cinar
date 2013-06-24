<%@ WebHandler Language="C#" Class="NewEntity" %>

using System;
using System.Web;
using Cinar.CMS.Library;
using Cinar.CMS.Library.Entities;
using System.Reflection;
using Cinar.Database;


public class NewEntity : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "application/json";

        try
        {
            string entityName = context.Request["entityName"];

            context.Response.Write(new AjaxResponse {IsError=false, Data = Provider.CreateEntity(entityName)}.ToJSON());
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