using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using rfl = System.Reflection;
using System.Threading;
using System.Web;
using System.Xml.Linq;
using Cinar.Database;
using Newtonsoft.Json;
using System.Threading.Tasks;
using $=db.Name$.DTO;
using $=db.Name$.DTO.Response;
using $=db.Name$.DTO.Request;
using $=db.Name$.API.Entity;
using $=db.Name$.DTO.EntityInfo;

namespace $=db.Name$.API
{
    /// <summary>
    /// Summary description for ApiJson
    /// </summary>
    public class BaseAPI : IHttpHandler
    {
        protected HttpContext context;
        private string apiType = "json";

        public Application Application { get; set; }
        public Reseller Reseller { get; set; }
        public string ClientIp { get; set; }
        public APISession Session { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            this.context = context;
            if (context.Request["apiType"] == "xml") apiType = "xml";

            string clientIPAddress = getIPAddress();

            string data = "", method = "", clientName = "";
            object req = null;
            try
            {
                if (!clientIPAddress.StartsWith("93.89.226") && !ConfigurationManager.AppSettings["allowedIPs"].Contains(clientIPAddress))
                    throw new Exception("Access denied for " + context.Request.UserHostAddress);

                if (apiType == "xml")
                    context.Response.ContentType = "application/xml";
                else
                    context.Response.ContentType = "application/json";

                method = context.Request["method"];

                if (string.IsNullOrWhiteSpace(method))
                    throw new Exception("Service request method needed");

                rfl.MethodInfo mi = this.GetType().GetMethod(method);

                if (mi == null)
                    throw new Exception("There is no service method with the name " + method);

                if (mi.GetParameters().Length != 1)
                    throw new Exception("A service request method should have only one parameter");

                data = context.Request["data"];
                if (string.IsNullOrWhiteSpace(data))
                    throw new Exception("Service request data needed");
                data = data.Replace("%2B", "+");
                Type t = getServiceRequestType(mi.GetParameters()[0].ParameterType);

                req = deserialize(data, t);

                this.Application = Provider.Database.Read<Application>("Key={0}", req.GetMemberValue("APIKey"));
                if(this.Application==null)
                    throw new Exception("No such application");

                this.Reseller = Provider.Database.Read<Reseller>("Id={0}", req.GetMemberValue("ResellerId"));
                if (this.Reseller == null)
                    throw new Exception("No such reseller");

                var client = req.GetMemberValue("Client");
                clientName = client == null ? "" : client.ToString();

                this.Session = Provider.Database.Read<APISession>("Token={0}", req.GetMemberValue("SessionId"));
                if (this.Session == null)
                    createSession();

                //TODO: session timeout'u prametrik yapabiliriz.
                if (this.Session.LastAccess.AddMinutes(30) < DateTime.Now)
                    throw new APIException("Session timeout");

                this.Session.LastAccess = DateTime.Now;
                this.Session.Save();

                object res = mi.Invoke(this, new[] { req.GetMemberValue("Data") });

                t = getServiceResponseType(mi.ReturnType);
                object serviceResponse = Activator.CreateInstance(t);
                serviceResponse.SetMemberValue("Data", res);
                serviceResponse.SetMemberValue("IsSuccessful", true);
                serviceResponse.SetMemberValue("ClientIPAddress", clientIPAddress);

                sw.Stop();

                serviceResponse.SetMemberValue("ServerProcessTime", sw.ElapsedMilliseconds);

                context.Response.Write(serialize(serviceResponse, apiType));
            }
            catch (Exception ex)
            {
                sw.Stop();

                if (ex.InnerException is APIException)
                {
                    var exInner = ex.InnerException as APIException;
                    context.Response.Write(serialize(new ServiceResponse<object>
                        {
                            Data = null,
                            IsSuccessful = false,
                            ErrorMessage = exInner.Message,
                            ErrorType = (int)exInner.ErrorType,
                            ErrorCode = (int)exInner.ErrorCode,
                            ClientIPAddress = clientIPAddress,
                            ServerProcessTime = sw.ElapsedMilliseconds
                        }, apiType));
                }
                else
                {
                    context.Response.Write(serialize(new ServiceResponse<object>
                        {
                            Data = null,
                            IsSuccessful = false,
                            ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                            ErrorCode = 500,
                            ClientIPAddress = clientIPAddress,
                            ServerProcessTime = sw.ElapsedMilliseconds
                        }, apiType));
                }
            }
        }

        #region utility
        private void createSession()
        {
            this.Session = new APISession()
                {
                    LastAccess = DateTime.Now,
                    Token = Utility.MD5("MA"+DateTime.Now.Ticks.ToString())
                };
            this.Session.Save();
        }
        private string serialize(object obj, string apiType)
        {
            if (apiType == "json")
                return JsonConvert.SerializeObject(obj, Formatting.Indented);

            return obj.Serialize();
        }
        private object deserialize(string data, Type type)
        {
            if (apiType == "json")
                return JsonConvert.DeserializeObject(data, type);
            return data.Deserialize(type);
        }

        private Type getServiceRequestType(Type item)
        {
            var t = typeof(ServiceRequest<>);
            Type[] typeArgs = { item };
            return t.MakeGenericType(typeArgs);
        }
        private Type getServiceResponseType(Type item)
        {
            var t = typeof(ServiceResponse<>);
            Type[] typeArgs = { item };
            return t.MakeGenericType(typeArgs);
        }

        public List<rfl.MethodInfo> GetServiceMethods()
        {
            return this.GetType().GetMethods(rfl.BindingFlags.DeclaredOnly | rfl.BindingFlags.Public | rfl.BindingFlags.Instance).ToList();
        }
        public string GetServiceMethodDescription(rfl.MethodInfo mi)
        {
            DescriptionAttribute desc = mi.GetAttribute<DescriptionAttribute>();

            string description = "<table><tr><td colspan=\"2\"><i>" + (desc == null ? "No description." : desc.Description) + "</i></td></tr>";
            description += "<tr><td colspan=\"2\">&nbsp;</td></tr>";

            foreach (rfl.PropertyInfo pi in mi.GetParameters()[0].ParameterType.GetProperties())
            {
                DescriptionAttribute desc2 = pi.GetAttribute<DescriptionAttribute>();
                description += string.Format("<tr><td><b>{0}</b></td><td>{1}</td></tr>", pi.Name, (desc2 == null ? "" : desc2.Description));
            }

            description += "</table>";

            return description;
        }
        public string GetServiceMethodRequestSample(rfl.MethodInfo mi, string apiType)
        {
            if (mi == null)
                return "No such service method";

            if (mi.GetParameters().Length != 1)
                return "A service request method should have only one parameter";

            Type t = getServiceRequestType(mi.GetParameters()[0].ParameterType);
            object req = Activator.CreateInstance(t);
            req.SetMemberValue("APIKey", "SAMPLE_API_KEY");
            req.SetMemberValue("ResellerId", ConfigurationManager.AppSettings["ResellerId"]);
            object data;
            if (mi.GetParameters()[0].ParameterType == typeof(string))
                data = "";
            else
                data = Activator.CreateInstance(mi.GetParameters()[0].ParameterType);
            req.SetMemberValue("Data", data);

            foreach (rfl.PropertyInfo pi in data.GetType().GetProperties(rfl.BindingFlags.DeclaredOnly | rfl.BindingFlags.Public | rfl.BindingFlags.Instance))
                if (!(pi.PropertyType.IsPrimitive || pi.PropertyType == typeof(string)) && pi.GetSetMethod() != null)
                    pi.SetValue(data, pi.PropertyType.IsArray ? Array.CreateInstance(pi.PropertyType.GetElementType(), 0) : Activator.CreateInstance(pi.PropertyType), null);

            return serialize(req, apiType);
        }

        private string getIPAddress()
        {
            HttpContext context = HttpContext.Current;

            if (!string.IsNullOrWhiteSpace(context.Request.ServerVariables["HTTP_CLIENT_IP"]))
                return context.Request.ServerVariables["HTTP_CLIENT_IP"];

            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.UserHostAddress;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
        protected object Deserialize(string data, Type type)
        {
            return JsonConvert.DeserializeObject(data, type);
        }
        #endregion
    }

    /// <summary>
    /// Summary description for ApiJson1
    /// </summary>
    public class ApiJson : BaseAPI
    {
        public ResHello Hello(ReqHello req)
        {
            return new ResHello() {Message = "Hello " + req.Name};
        }
    }
}