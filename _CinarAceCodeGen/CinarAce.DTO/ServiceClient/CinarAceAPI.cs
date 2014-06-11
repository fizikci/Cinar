using $=db.Name$.DTO.EntityInfo;
using $=db.Name$.DTO.Request;
using $=db.Name$.DTO.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace $=db.Name$.DTO.ServiceClient
{
    public class $=db.Name$API : BaseAPI
    {
        public $=db.Name$API()
        {
        }

        public ResHello Hello(ReqHello req)
        {
            return Call<ResHello, ReqHello>(req, "Hello");
        }

        protected override string GetServiceURL()
        {
            return ConfigurationManager.AppSettings["$=db.Name$ServiceURL"];
        }
    }
}
