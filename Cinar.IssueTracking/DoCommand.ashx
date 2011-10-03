<%@ WebHandler Language="C#" Class="DoCommand" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using Cinar.Entities;
using Cinar.Entities.IssueTracking;
using Cinar.Entities.Standart;

public class DoCommand : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
        string command = context.Request["command"];
        
        switch (command)
        {
            case "addProject":
                {
                    Project p = new Project();
                    p.Name = context.Request["projectName"];
                    p.Save();

                    ProjectUser pu = new ProjectUser();
                    pu.ProjectId = p.Id;
                    pu.UserId = CinarContext.ClientUser.Id;
                    pu.Save();

                    if (context.Request["noRedirect"] == "1")
                        context.Response.Write("{success:1, data:" + p.ToJSON() + "}");
                    else
                        context.Response.Redirect("Member.aspx", true);
                    break;
                }
            case "addMemberToProject":
                {
                    ProjectUser pu = new ProjectUser();
                    pu.ProjectId = int.Parse(context.Request["projectId"]);
                    pu.UserId = int.Parse(context.Request["userId"]);
                    pu.Save();

                    context.Response.Redirect("Member.aspx", true);
                    break;
                }
            case "getProjectComponents":
                {
                    int projectId = int.Parse(context.Request["projectId"]);
                    List<string> list = CinarContext.Db.GetList<string>("select distinct Component from Ticket where ProjectId={0}", projectId);
                    list.Insert(0,"");

                    string res = "{success:1, data:" + list.ToJSON() + "}";
                    context.Response.Write(res);
                    break;
                }
            case "getUsersAndComponents":
                {
                    int projectId = int.Parse(context.Request["projectId"]);
                    List<string> comps = CinarContext.Db.GetList<string>("select distinct Component from Ticket where ProjectId={0}", projectId);
                    comps.Insert(0,"");

                    List<User> members = CinarContext.Db.Read<Project>(projectId).GetTeamMembers();
                    members.Insert(0, new User() {Id=0, Name="[all]" });
                    
                    string res = "{success:1, components:" + comps.ToJSON() + ", users:" + members.ToJSON() + "}";
                    context.Response.Write(res);
                    break;
                }
            default:
                context.Response.Write("Unavailable command: " + command);
                break;
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}