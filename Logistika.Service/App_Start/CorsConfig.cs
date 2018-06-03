using System.Web.Http;
using Thinktecture.IdentityModel.Http.Cors;

namespace Logistika.Service
{
    public class CorsConfig
    {
        public static void RegisterCors(HttpConfiguration httpConfig)
        {
            //WebApiCorsConfiguration corsConfig = new WebApiCorsConfiguration();

            //// this adds the CorsMessageHandler to the HttpConfiguration's
            //// MessageHandlers collection
            //corsConfig.RegisterGlobal(httpConfig);

            //// this allow all CORS requests to the Products controller
            //// from the http://foo.com origin.

            ////Load Cors corsConfig from Xml File, Cache the File on Mod, if File changes the Cache will change.
            //corsConfig
            //   .ForOrigins("http://foo.com")
            //   //.AllowAll();

          
        }
        //private void GetCorsPoliciesfromConfiguration()
        //{
        //    IList<CorsPolicy> corsPolicies = null;
        //    string corsPoliciesfromConfiguration = System.Configuration.ConfigurationManager.AppSettings["CorsAttributes"];
        //    if (!string.IsNullOrEmpty(corsPoliciesfromConfiguration))
        //    {
                 
        //        dynamic arrayOfPolicy = JsonConvert.DeserializeObject(corsPoliciesfromConfiguration);
        //        CorsPolicy policy = null;

        //        dynamic stuff = JsonConvert.DeserializeObject(corsPoliciesfromConfiguration);

        //        foreach (var s in stuff)
        //        {

        //            var str = s.origins;
        //            var str1 = s.headers;
        //            var str2 = s.methods;
        //            policy.Origins.Add(Convert.ToString(str.Value));
        //            policy.Headers.Add(Convert.ToString(str1));
        //            policy.Methods.Add(Convert.ToString(str2));
        //        }
        //        foreach (var obj in arrayOfPolicy)
        //        {
        //            policy = new CorsPolicy();
        //            policy.Origins.Add(Convert.ToString(obj.origins));
        //            policy.Headers.Add(Convert.ToString(obj.headers));
        //            policy.Methods.Add(Convert.ToString(obj.methods));
        //            corsPolicies.Add(policy);
        //        }
        //    }
        //    return corsPolicies;
        //}

        private  void ConfigureCors(CorsConfiguration corsConfig)
        {

              //corsConfig
              //.ForResources("~/Handler1.ashx")
              //.ForOrigins("http://foo.com", "http://bar.com")
              //.AllowAll();
            // this allows http://foo.com to do GET or POST on Values1 controller
        //    corsConfig
        //       .ForResources("Values1")
        //       .ForOrigins("http://foo.com")
        //       .AllowMethods("GET", "POST");

        //    // this allows http://foo.com to do GET and POST, pass cookies and
        //    // read the Foo response header on Values2 controller
        //    corsConfig
        //       .ForResources("Values2")
        //       .ForOrigins("http://foo.com")
        //       .AllowMethods("GET", "POST")
        //       .AllowCookies()
        //       .AllowResponseHeaders("Foo");

        //    // this allows http://foo.com and http://foo.com to do GET, POST,
        //    // and PUT and pass the Content-Type header to Values3 controller
        //    corsConfig
        //       .ForResources("Values3")
        //       .ForOrigins("http://foo.com", "http://bar.com")
        //       .AllowMethods("GET", "POST", "PUT")
        //       .AllowRequestHeaders("Content-Type");

        //    // this allows http://foo.com to use any method, pass cookies, and
        //    // pass the Content-Type, Foo and Authorization headers, and read
        //    // the Foo response header for Values4 and Values5 controllers
        //    corsConfig
        //       .ForResources("Values4", "Values5")
        //       .ForOrigins("http://foo.com")
        //       .AllowAllMethods()
        //       .AllowCookies()
        //       .AllowRequestHeaders("Content-Type", "Foo", "Authorization")
        //       .AllowResponseHeaders("Foo");

        //    // this allows all methods and all request headers (but no cookies)
        //    // from all origins to Values6 controller
        //    corsConfig
        //       .ForResources("Values6")
        //       .AllowAllOriginsAllMethodsAndAllRequestHeaders();

        //    // this allows all methods (but no cookies or request headers)
        //    // from all origins to Values7 controller
        //    corsConfig
        //       .ForResources("Values7")
        //       .AllowAllOriginsAllMethods();

        //    // this allows all CORS requests from origin http://bar.com
        //    // for all resources that have not been explicitly configured
        //    corsConfig
        //       .ForOrigins("http://bar.com")
        //       .AllowAll();

        //    // this allows all CORS requests to all other resources that don’t
        //    // have an explicit configuration. This opens them to all origins, all
        //    // HTTP methods, all request headers and cookies. This is the API to use
        //    // to get started, but it’s a sledgehammer in the sense that *everything*
        //    // is wide-open.
        //    corsConfig.AllowAll();
        }
    }
}