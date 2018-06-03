
using Logistika.Service.Common.BusinessComponentInterface.User;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Logistika.Service.Providers.Handler
{

    /*
 Client/Browser side – HTTP request headers

These are headers that clients may use when issuing HTTP requests in order to make use of the cross-sharing feature:

Origin: URI indicating the server from which the request initiated.  It does not include any path information, but only the server name.
Access-Control-Request-Headers:  used when issuing a preflight request to let the server know what HTTP headers will be used when the actual request is made.
Access-Control-Request-Method: used when issuing a preflight request to let the server know what HTTP method will be used when the actual request is made.

     * Server side – HTTP response headers

These are the HTTP headers that the server sends back for access control requests as defined by the Cross-Origin Resource Sharing specification:

Access-Control-Allow-Origin: specifies the authorized domains to make cross-domain request (you should include the domains of your REST clients or “*” if you want 

the resource public and available to everyone – the latter is not an option if credentials are allowed during CORS requests)
Access-Control-Expose-Headers: lets a server white list headers that browsers are allowed to access
Access-Control-Max-Age:  indicates how long the results of a preflight request can be cached.
Access-Control-Allow-Credentials: indicates if the server allows credentials during CORS requests
Access-Control-Allow-Methods: indicates the methods allowed when accessing the resource
Access-Control-Allow-Headers:  used in response to a preflight request to indicate which HTTP headers can be used when making the actual request.
     */
    public class CorsHandler : DelegatingHandler
    {
        const string Origin = "Origin";
        const string AccessControlRequestMethod = "Access-Control-Request-Method";
        const string AccessControlRequestHeaders = "Access-Control-Request-Headers";

        const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        const string AccessControlAllowMethods = "Access-Control-Allow-Methods";
        const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";

        const string AccessControlMaxAge = "Access-Control-Max-Age";
        const string AccessControlAllowCredentials = "Access-Control-Allow-Credentials";
        const string AccessControlExposeHeaders = "Access-Control-Expose-Headers";

        IUserBusinessComponent _authenticationBusinessComponent = null;
        public CorsHandler(IUserBusinessComponent Instance)
        {
            _authenticationBusinessComponent = Instance;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool isCorsRequest = request.Headers.Contains(Origin);
            bool isPreflightRequest = request.Method == HttpMethod.Options;
            if (isCorsRequest)
            {
                if (isPreflightRequest)
                {
                    return Task.Factory.StartNew(() =>
                    {
                        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                        response.Headers.Add(AccessControlAllowOrigin, request.Headers.GetValues(Origin).First());

                        string accessControlRequestMethod = request.Headers.GetValues(AccessControlRequestMethod).FirstOrDefault();
                        if (accessControlRequestMethod != null)
                        {
                            response.Headers.Add(AccessControlAllowMethods, accessControlRequestMethod);
                        }

                        string requestedHeaders = string.Join(", ", request.Headers.GetValues(AccessControlRequestHeaders));
                        if (!string.IsNullOrEmpty(requestedHeaders))
                        {
                            response.Headers.Add(AccessControlAllowHeaders, requestedHeaders);
                        }

                        return response;
                    }, cancellationToken);
                }
                else
                {
                    //var user = request.GetOwinContext().Request.User; 
                    //var owinReq = request.GetOwinContext().Request;
                    //if (user.Identity.IsAuthenticated) {
                    //   var requestCors =  new WsApiCorsConfig
                    //    {
                    //        Resources = owinReq.Path.Value.Replace("/api/", ""),
                    //        RequestHeaders = "*",
                    //        ResponseHeaders = "*",
                    //        Origins = owinReq.Headers["Origin"],
                    //        Methods = owinReq.Method,
                    //    };

                    //    var corsConfig = _authenticationBusinessComponent.GetUserCors(user.Identity.Name); 
                    //        return Task.Factory.StartNew(() =>
                    //            {

                    //                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    //                var dataExists = from config in corsConfig
                    //                where requestCors.Origins == config.Origins
                    //                select config;
                    //                //valid origin
                    //                 if (dataExists != null && dataExists.Count() > 0) {

                    //                    response.Headers.Add(AccessControlAllowOrigin, request.Headers.GetValues(Origin).First());

                    //                    dataExists = from config in dataExists
                    //                                 where config.Resources.Contains(requestCors.Resources)
                    //                    select config;

                    //                    //valid Controler
                    //                    if (dataExists != null && dataExists.Count() > 0) {


                    //                        dataExists = from config in dataExists
                    //                                     where config.Methods.Contains(requestCors.Methods)

                    //                        select config;

                    //                        //valid Response Methods
                    //                        if (dataExists != null && dataExists.Count() > 0) {

                    //                            string accessControlRequestMethod = request.Headers.GetValues(AccessControlRequestMethod).FirstOrDefault();
                    //                            if (accessControlRequestMethod != null)
                    //                            {
                    //                                response.Headers.Add(AccessControlAllowMethods, accessControlRequestMethod);
                    //                            }

                    //                            dataExists = from config in dataExists
                    //                                         where config.RequestHeaders.Contains(requestCors.RequestHeaders)
                    //                                         select config;
                    //                            //Valid Request Headers 
                    //                            if (dataExists != null && dataExists.Count() > 0)
                    //                            {
                    //                                string requestedHeaders = string.Join(", ", request.Headers.GetValues(AccessControlRequestHeaders));
                    //                                if (!string.IsNullOrEmpty(requestedHeaders))
                    //                                {
                    //                                    response.Headers.Add(AccessControlAllowHeaders, requestedHeaders);
                    //                                }
                    //                            }
                    //                        }
                    //                    }
                    //                }

                    //                return response;
                    //            }, cancellationToken); 
                    //} 

                    return base.SendAsync(request, cancellationToken).ContinueWith(t =>
                    {

                        HttpResponseMessage resp = t.Result;
                        //HttpResponseMessage resp = new HttpResponseMessage((HttpStatusCode.OK));
                        resp.Headers.Add(AccessControlAllowOrigin, request.Headers.GetValues(Origin).First());
                        return resp;
                    });


                }
            }
            else
            {
                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}