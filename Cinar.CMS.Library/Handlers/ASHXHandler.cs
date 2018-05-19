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
                if (path[path.Length - 1] == '/')
                    path = path.Substring(0, path.LastIndexOf('/'));
            string fileName = path.Substring(path.LastIndexOf('/') + 1);
            if (fileName.ToLower().Contains(".aspx")) {
                Provider.Response.Redirect(Provider.Configuration.MainPage, true);
                return;
            }
            fileName = fileName.Substring(0, fileName.LastIndexOf('.'));

            if (fileName.Contains(".")) // bu demektir ki cinar.cms.js gibi bir dosya isteniyor
            {
                if (fileName.EndsWith(".css"))
                    context.Response.ContentType = "text/css";
                else if (fileName.EndsWith(".js"))
                    context.Response.ContentType = "text/javascript";

                /*
                HttpCachePolicy c = context.Response.Cache;
                c.SetCacheability(HttpCacheability.Public);
                c.SetMaxAge(new TimeSpan(1, 0, 0));
                 */

                if (context.Request.Url.IsLoopback)
                {
                    string resourceFilePath = Path.Combine(Provider.AppSettings["pathToLocalResources"], fileName);
                    //string resourceFilePath = Provider.MapPath("/").Replace("Cinar.CMS.Web", "Cinar.CMS.Library") + "Resources\\" + fileName;
                    if (!File.Exists(resourceFilePath))
                        resourceFilePath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Provider.MapPath("/")))) + "\\Cinar\\Cinar.CMS.Library\\Resources\\" + fileName;
                    if (File.Exists(resourceFilePath))
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
                case "Social":
                    new Social().ProcessRequest(context);
                    break;
                case "VirtualPOSBankAsya":
                    new VirtualPOSBankAsya().ProcessRequest(context);
                    break;
                case "RunModuleMethod":
                case "DoLogin":
                case "SaveMember":
                case "LoginWithKeyword":
                case "UserActivation":
                case "ValidateNewEmail":
                case "RSS":
                case "Print":
                case "Redirect":
                case "AdClick":
                case "UploadContent":
                case "UploadContentTest":
                case "DefaultStyleSheet":
                case "DefaultJavascript":
                case "UpdateTags":
                case "GetAllActivity":
                case "AutoCompleteTag":
                case "LikeIt":
                case "Subscribe":
                case "GetModuleHtml":
                case "KeepSession":
                case "EditImageCrop":
                case "EditImageRotate":
                case "EditImageResize":
                case "EditImageReset":
                case "getLocation":
                case "FacebookLogin":
                case "reportBug":
                case "socialAuthLogin":
                case "loginBySocialAuth":
                case "getSocialFriends":
                case "logout_sauth":
                case "login_sauth":
                case "validate_sauth":
                case "isNickAvailable":
                case "parseWebPageHtmlAsContent":
                    new DoCommand().ProcessRequest(context);
                    break;
                case "DoCommand":
                    new DoCommand().ProcessRequest(context);
                    break;
                case "Console":
                    new Console().ProcessRequest(context);
                    break;
                default:
                    Type type = Provider.GetType("Handlers", fileName);
                    if (type != null)
                    {
                        IHttpHandler handler = (IHttpHandler) Activator.CreateInstance(type);
                        handler.ProcessRequest(context);
                    }
                    else
                        HttpContext.Current.Response.Write("Handler not found: " + fileName + " (URL: " + fileName + ".ashx)");
                    break;
            }
        }
    }
}