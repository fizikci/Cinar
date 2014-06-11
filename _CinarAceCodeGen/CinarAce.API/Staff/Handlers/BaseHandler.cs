using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using Cinar.Database;
using $=db.Name$.API.Entity;
using Newtonsoft.Json;

namespace $=db.Name$.API.Staff.Handlers
{
    public class AjaxResponse
    {
        public bool isError { get; set; }
        public string errorMessage { get; set; }
        public object data { get; set; }
    }

    public class BaseHandler : IHttpHandler, IRequiresSessionState
    {
        protected HttpContext context;

        public void ProcessRequest(HttpContext context)
        {
            this.context = context;

            string method = "";
            try
            {
                context.Response.ContentType = "application/json";

                method = context.Request["method"];

                if (string.IsNullOrWhiteSpace(method))
                    throw new Exception("Ajax method needed");

                MethodInfo mi = this.GetType().GetMethod(method);

                if (mi == null)
                    throw new Exception("There is no ajax method with the name " + method);

                object[] paramValues = new object[mi.GetParameters().Length];

                if (mi.GetParameters().Length > 0)
                {
                    for (int i = 0; i < mi.GetParameters().Length; i++ )
                    {
                        ParameterInfo pi = mi.GetParameters()[i];
                        paramValues[i] = Convert.ChangeType(context.Request[pi.Name], pi.ParameterType);
                    }
                }

                var res = mi.Invoke(this, paramValues);

                context.Response.Write(Serialize(new AjaxResponse()
                    {
                        isError = false,
                        errorMessage = "",
                        data = res
                    }));
            }
            catch (Exception ex)
            {
                context.Response.Write(Serialize(new AjaxResponse()
                {
                    isError = true,
                    errorMessage = ex.Message,
                    data = null
                }));
            }
        }

        #region utility
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

    public class BaseEntityHandler<T> : BaseHandler where T : BaseEntity, new()
    {
        public T GetById(int id)
        {
            return Provider.Database.Read<T>(id);
        }

        public List<T> GetList(bool isDeleted, int pageSize, int pageNo)
        {
            return Provider.Database.ReadList<T>(FilterExpression.Where("IsDeleted", CriteriaTypes.Eq, isDeleted), pageNo, pageSize);
        }

        public bool DeleteById(int id)
        {
            Provider.Database.Read<T>(id).Delete();
            return true;
        }

        public bool Save(int id)
        {
            try
            {
                var entity = Provider.Database.Read<T>(id);
                if (entity == null) entity = new T();
                entity.SetFieldsByPostData(context.Request.Form);

                entity.Save();

                context.Response.Redirect("/Staff/List" + typeof (T).Name + ".aspx", true);

                return true;
            }
            catch (Exception ex)
            {
                context.Server.Transfer("/Staff/Edit" + typeof(T).Name + ".aspx");
                return false;
            }
        }
    }
}