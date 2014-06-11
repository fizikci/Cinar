using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace $=db.Name$.DTO.ServiceClient
{
    public abstract class BaseAPI
    {
        private static string winAppSessionKey = "";

        protected string apiKey;
        protected string $=db.Name.ToLower()$SessionKey
        {
            get {
                return HttpContext.Current != null ? (string)HttpContext.Current.Session["$=db.Name.ToLower()$SessionKey"] : winAppSessionKey;
            }
            set {
                if (HttpContext.Current != null)
                    HttpContext.Current.Session["$=db.Name.ToLower()$SessionKey"] = value;
                else
                    winAppSessionKey = value;
            }
        }
        protected string clientIP;

        public int ResponseTimeOut;

        protected T Call<T, K>(K request, string methodName)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            ServiceRequest<K> serviceRequest = new ServiceRequest<K>();
            serviceRequest.APIKey = ConfigurationManager.AppSettings["APIKey"];
            serviceRequest.Data = request;
            serviceRequest.ResellerId = int.Parse(ConfigurationManager.AppSettings["ResellerId"]);
            serviceRequest.SessionId = membershipSessionKey;

            if (HttpContext.Current != null) serviceRequest.ClientIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            serviceRequest.Client = HttpContext.Current == null ? System.AppDomain.CurrentDomain.FriendlyName : HttpContext.Current.Request.RawUrl;

            string serviceUrl = GetServiceURL();
            serviceUrl += "?apiType=json&method=" + methodName;

            string data = "data=" + HttpUtility.UrlEncode(Serialize(serviceRequest));

            using (MyWebClient wc = new MyWebClient(ResponseTimeOut))
            {
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)");
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string res = wc.UploadString(serviceUrl, data);

                ServiceResponse<T> serviceResponse = (ServiceResponse<T>)Deserialize(res, typeof(ServiceResponse<T>));

                sw.Stop();

                if (!serviceResponse.IsSuccessful)
                    throw new APIException(
                        serviceResponse.ErrorMessage,
                        (ErrorTypes)serviceResponse.ErrorType,
                        (ErrorCodes)serviceResponse.ErrorCode
                    );


                return serviceResponse.Data;
            }
        }

        protected abstract string GetServiceURL();

        protected string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
        protected object Deserialize(string data, Type type)
        {
            return JsonConvert.DeserializeObject(data, type);
        }



        private class MyWebClient : WebClient
        {
            private int ResTimeOut { get; set; }

            public MyWebClient(int RTimeOut)
            {
                ResTimeOut = RTimeOut == 0 ? (4 * 60 * 1000) : RTimeOut;
            }

            protected override WebRequest GetWebRequest(Uri uri)
            {
                WebRequest w = base.GetWebRequest(uri);
                w.Timeout = ResTimeOut;
                return w;
            }
        }
    }
}
