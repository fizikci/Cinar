using System;
using System.Web;
using System.Web.SessionState;

namespace Cinar.CMS.Library.Handlers
{
    public abstract class GenericHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public abstract bool RequiresAuthorization { get;}
        public abstract string RequiredRole { get;}

        protected HttpContext context;

        public void ProcessRequest(HttpContext context)
        {
            this.context = context;

            this.context.Response.Clear();
            this.context.Response.CacheControl = "no-cache";
            this.context.Response.Charset = "utf-8";

            Provider.SetHttpContextUser();

            if (this.RequiresAuthorization && !Provider.User.IsInRole(this.RequiredRole))
            {
                sendErrorMessage(Provider.GetResource("You are not authorized to carry out this process!"));
                context.Response.End();
                return;
            }

            try
            {
                this.ProcessRequest();
            }
            catch (Exception ex)
            {
                sendErrorMessage(ex);
            }

            Provider.Database.Connection.Close();
        }

        public abstract void ProcessRequest();

        protected void sendErrorMessage(string message)
        {
            context.Response.Write("ERR: " + message);
        }
        protected void sendErrorMessage(Exception ex)
        {
            sendErrorMessage(Provider.ToString(ex, false));
        }
    }
}
