using System;
using System.Web;
using System.Web.SessionState;

namespace Cinar.CMS.Library.Handlers
{
    public class ASHXHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            Provider.OnBeginRequest();

            string path = context.Request.Url.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped);
            string fileName = path.Substring(path.LastIndexOf('/') + 1);
            fileName = fileName.Substring(0, fileName.LastIndexOf('.'));
            switch (fileName)
            { 
                case "ModuleInfo":
                    new ModuleInfo().ProcessRequest(context);
                    break;
                case "EntityInfo":
                    new EntityInfo().ProcessRequest(context);
                    break;
                case "SystemInfo":
                    new SystemInfo().ProcessRequest(context);
                    break;
                case "Admin":
                    new Admin().ProcessRequest(context);
                    break;
                case "RunModuleMethod":
                case "DoLogin":
                case "SaveMember":
                case "LoginWithKeyword":
                case "UserActivation":
                case "RSS":
                case "Print":
                case "Redirect":
                case "AdClick":
                case "UploadContent":
                case "UploadContentTest":
                case "DefaultStyleSheet":
                case "UpdateTags":
                case "Scriptify":
                case "AutoCompleteTag":
                case "LikeIt":
                    new DoCommand().ProcessRequest(context);
                    break;
                case "Console":
                    new Console().ProcessRequest(context);
                    break;
                default:
                    Type type = Provider.GetType("Handlers", fileName);
                    if (type != null)
                    {
                        IHttpHandler handler = (IHttpHandler)Activator.CreateInstance(type);
                        handler.ProcessRequest(context);
                    }
                    break;
            }
        }
    }
}