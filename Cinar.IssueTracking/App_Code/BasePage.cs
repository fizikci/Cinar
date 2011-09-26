using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Cinar.Entities;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : Page
{
	public BasePage()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public override void ProcessRequest(HttpContext context)
    {
        if (CinarContext.ClientUser.Id == 0 && !context.Request.Url.PathAndQuery.Contains("Login.aspx"))
        {
            context.Response.Redirect("/Login.aspx", true);
            return;
        }
        base.ProcessRequest(context);
    }
}