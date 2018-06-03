using Logistika.Service.Common.Entities.CorsConfig;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Logistika.Service.Common.Extension
{
    public static class ApiCorsConfigExtension
    {

        //public static WsApiCorsConfig GetCorsDetailFromRequest(this HttpRequestMessage Request) {

        //    if (Request == null) return (WsApiCorsConfig)null;

        //    var Method = Request.Method;

        //    return new WsApiCorsConfig { 
        //    Resources = Request.RequestUri.AbsolutePath,
        //    RequestHeaders = "*",
        //    ResponseHeaders = "*",
        //    Origins = Request.RequestUri.AbsoluteUri,
        //    Methods = string.Join(", ", Request.Headers.GetValues()),
        //    };
        //}

        public static bool ValidateRequestedCors(this WsApiCorsConfig RequestCors,IList<WsApiCorsConfig> CorsConfig,HttpResponseMessage Response )
        {
            var dataExists = from config in CorsConfig
                        where RequestCors.Origins == config.Origins
                          select config;
                        //&& config.Methods.Contains(RequestCors.Methods)
                        ////&& config.ResponseHeaders.Contains(RequestCors.ResponseHeaders)
                        ////&& config.ResponseHeaders.Contains(RequestCors.ResponseHeaders)
                        //&& config.Resources.Contains(RequestCors.Resources)
                        //select config;
             if (dataExists != null && dataExists.Count() > 0) { 

                dataExists = from config in dataExists
                where config.Resources.Contains(RequestCors.Resources)
                select config;

                if (dataExists != null && dataExists.Count() > 0) { 

                    dataExists = from config in dataExists
                    where config.Methods.Contains(RequestCors.Methods)
                    select config;

                    if (dataExists != null && dataExists.Count() > 0) { 
                        dataExists = from config in dataExists
                        where  config.ResponseHeaders.Contains(RequestCors.ResponseHeaders)
                        select config;
                     
                        if (dataExists != null && dataExists.Count() > 0) { 
                            dataExists = from config in dataExists
                            where  config.RequestHeaders.Contains(RequestCors.RequestHeaders)
                            select config;
                        }
                    }
                }
            }
            return dataExists.Count() > 0;
        }
    }
}
