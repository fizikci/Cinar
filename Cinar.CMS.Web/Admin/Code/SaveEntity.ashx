<%@ WebHandler Language="C#" Class="SaveEntity" %>

using System;
using System.Web;
using Cinar.CMS.Library;
using Cinar.CMS.Library.Entities;
using System.Reflection;
using Cinar.Database;


public class SaveEntity : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "application/json";

        try
        {
            string entityName = context.Request["entityName"];

            BaseEntity entity = Provider.CreateEntity(entityName);
            entity.SetFieldsByPostData(context.Request.Form);

            entity.Save();

            context.Response.Write(new AjaxResponse {IsError=false}.ToJSON());
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