using System;
using System.IO;
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

            if (fileName.Contains(".")) // bu demektir ki cinar.cms.js gibi bir dosya isteniyor
            {
                if (fileName.EndsWith(".css"))
                    context.Response.ContentType = "text/css";
                else if (fileName.EndsWith(".js"))
                    context.Response.ContentType = "text/javascript";

                if (context.Request.Url.IsLoopback)
                {
                    string resourceFilePath = Path.Combine(Provider.AppSettings["pathToLocalResources"], fileName);
                    //string resourceFilePath = context.Server.MapPath("/").Replace("Cinar.CMS.Web", "Cinar.CMS.Library") + "Resources\\" + fileName;
                    if(File.Exists(resourceFilePath))
                        context.Response.Write(File.ReadAllText(resourceFilePath));
                }
                else
                {
                    string s = Properties.Resources.ResourceManager.GetString(fileName.Replace(".", "_"));
                    context.Response.Write(s ?? ("There is no resource with this name: " + fileName.Replace(".", "_")));
                }
                return;
            }

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
                case "Subscribe":
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