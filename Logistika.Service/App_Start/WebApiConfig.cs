using Newtonsoft.Json;
using System.Web.Http;

namespace Logistika.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

//            config.Routes.MapHttpRoute(
//    name: "WithAction",
//    routeTemplate: "api/{controller}/{action}/{id}",
//    defaults: new { id = RouteParameter.Optional }
//);
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                DateFormatString = "MM/dd/yyyy",
                //ContractResolver = new SubstituteNullWithEmptyStringContractResolver() 
            };
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


        }
    }
}
