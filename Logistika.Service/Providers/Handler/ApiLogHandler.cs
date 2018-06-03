using Logistika.Service.Common.BusinessComponentInterface.Logger;
using Logistika.Service.Common.Common;
using Logistika.Service.Common.Entities.ErrorLog;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logistika.Service.Providers.Handler
{
    public class ApiLogHandler : DelegatingHandler
    {
        ILoggerBusinessComponent _loggerInstance = null;
        public ApiLogHandler(ILoggerBusinessComponent Instance)
        {
            _loggerInstance = Instance;
        }

        private string GetContentType(HttpContent content)
        {
            string contentType = string.Empty;
            if (content != null && content.Headers != null && content.Headers.ContentType != null && content.Headers.ContentType.MediaType != null)
            {
                contentType = content.Headers.ContentType.MediaType;
            }
            return contentType;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Knipper-RequestTimeStamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
            try
            {
                SessionManager.UserId = request.GetOwinContext().Request.User.Identity.Name;
            }
            catch { }
            return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>((task) =>
            {

                HttpResponseMessage response = task.Result;
                try
                {
                    DateTime requestTimeStamp = DateTime.Now;
                    if (request.Headers.Contains("Knipper-RequestTimeStamp"))
                    {
                        requestTimeStamp = Convert.ToDateTime(request.Headers.GetValues("Knipper-RequestTimeStamp").FirstOrDefault());
                        request.Headers.Remove("Knipper-RequestTimeStamp");
                    }

                    var context = request.GetOwinContext();
                    var user = context.Request.User;
                    var routeData = request.GetRouteData();
                    var logEntry = new WebServiceLog();
                    logEntry.WSResponseID = ((int)response.StatusCode).ToString();
                    logEntry.WSServiceName = request.RequestUri.ToString();
                    logEntry.WSConsumer_FK = 0;
                    logEntry.WSUserId = user != null ? user.Identity.Name : "Not Logged In";
                    logEntry.IPAddress = context.Request.RemoteIpAddress;
                    logEntry.DeviceType = context.Request.Headers.Get("User-Agent");
                    logEntry.ResponseStatus = response.IsSuccessStatusCode.ToString();
                    var requestInfo = string.Format("{0} {1}", request.Method, request.RequestUri);
                    var requestMessage = request.Content.ReadAsByteArrayAsync();
                    logEntry.RequestDetail = string.Format(" Request: {0}\r\n{1}\r\n{2}", requestInfo, GetContentType(request.Content), Encoding.UTF8.GetString(requestMessage.Result));
                    byte[] responseMessage;

                    if (response.IsSuccessStatusCode)
                        responseMessage = response.Content.ReadAsByteArrayAsync().Result;
                    else
                        responseMessage = Encoding.UTF8.GetBytes(response.ReasonPhrase);


                    logEntry.ResponseDetail = string.Format(" Request: {1}", requestInfo, Encoding.UTF8.GetString(responseMessage));
                    logEntry.ResponseStatus = response.IsSuccessStatusCode.ToString();
                    logEntry.WSResponseID = ((int)response.StatusCode).ToString();




                    logEntry.ResponseTime = (DateTime.Now - requestTimeStamp).TotalSeconds.ToString();

                    _loggerInstance.LogWebService(logEntry);
                    //throw new Exception("Test");
                }
                catch (Exception ex)
                {
                    _loggerInstance.LogSystemError(ex);
                }
                return response;
            });
        }

    }
}
//using Logistika.Service.Common.BusinessComponentInterface.Logger;
//using Logistika.Service.Common.Common;
//using Logistika.Service.Common.Entities.ErrorLog;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web.Http.Routing;

//namespace Logistika.Service.Providers.Handler
//{
//    public class ApiLogHandler : DelegatingHandler
//    {
//        ILoggerBusinessComponent _loggerInstance = null;
//        public ApiLogHandler(ILoggerBusinessComponent Instance)
//        {
//            _loggerInstance = Instance;
//        }

//        private void LogReQuestResponse(HttpRequestMessage request, CancellationToken cancellationToken) { }
//        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//        {
//            request.Headers.Add("Knipper-RequestTimeStamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
//            try
//            {
//                SessionManager.UserId = request.GetOwinContext().Request.User.Identity.Name;
//            }
//            catch { }
//            return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>((task) =>
//            {

//                HttpResponseMessage response = task.Result;
//                try
//                {
//                    DateTime requestTimeStamp = DateTime.Now;
//                    if (request.Headers.Contains("Knipper-RequestTimeStamp"))
//                    {
//                        requestTimeStamp = Convert.ToDateTime(request.Headers.GetValues("Knipper-RequestTimeStamp").FirstOrDefault());
//                        request.Headers.Remove("Knipper-RequestTimeStamp");
//                    }

//                    var context = request.GetOwinContext();
//                    var user = context.Request.User; 
//                    var routeData = request.GetRouteData();
//                    var logEntry = new WebServiceLog();
//                    logEntry.WSResponseID = ((int)response.StatusCode).ToString();
//                    logEntry.WSServiceName = request.RequestUri.ToString();
//                    logEntry.WSConsumer_FK = 0;
//                    logEntry.WSUserId = user!=null?user.Identity.Name:"Not Logged In";
//                    logEntry.IPAddress = context.Request.RemoteIpAddress;
//                    logEntry.DeviceType = context.Request.Headers.Get("User-Agent");
//                    logEntry.ResponseStatus = response.IsSuccessStatusCode.ToString();
//                    if (request.Content != null)
//                    {
//                        logEntry.RequestDetail = string.Format("{0} REQUEST-CONTENT-TYPE: {1} REQUEST-HEADERS: {2} REQUEST-ROUTE-DATA: {3}   REQUEST-CONTENT-BODY: {4} ", request.ToString(),
//                                                                                GetContentType(request.Content),
//                                                                                SerializeHeaders(request.Headers),
//                                                                                SerializeRouteData(routeData),
//                                                                                request.Content.ReadAsStringAsync().Result);
//                    }
//                    else
//                    {
//                        logEntry.RequestDetail = string.Format("{0}  REQUEST-HEADERS: {1} REQUEST-ROUTE-DATA: {2}", request.ToString(), SerializeHeaders(request.Headers),
//                                                                                SerializeRouteData(routeData));
//                    }

//                    if (response.Content != null)
//                    {
//                        logEntry.ResponseDetail = string.Format("{0} RESPONSE-CONTENT-TYPE: {1} RESPONSE-HEADERS: {2} RESPONSE-CONTENT-BODY: {3} ", response.ToString(),
//                                                                                GetContentType(response.Content),
//                                                                                 SerializeHeaders(response.Headers),
//                                                                                response.Content.ReadAsStringAsync().Result);
//                    }
//                    else
//                    {
//                        logEntry.ResponseDetail = string.Format("{0} RESPONSE-HEADERS: {1}", response.ToString(), SerializeHeaders(response.Headers));
//                    } 
//                    logEntry.ResponseTime = (DateTime.Now - requestTimeStamp).TotalSeconds.ToString();

//                  //  _loggerInstance.LogWebService(logEntry);
//                    //throw new Exception("Test");
//                }
//                catch (Exception ex)
//                {
//                    _loggerInstance.LogSystemError(ex);
//                }
//                return response;
//            });
//        }
//        private string SerializeRouteData(IHttpRouteData routeData)
//        {
//            if (routeData != null)
//            {
//                return JsonConvert.SerializeObject(routeData, Formatting.Indented);
//            }
//            else
//            {
//                return string.Empty;
//            }
//        }
        
//        private string SerializeHeaders(HttpHeaders headers)
//        {
//            if (headers != null)
//            {
//                var dict = new Dictionary<string, string>();

//                foreach (var item in headers.ToList())
//                {
//                    if (item.Value != null)
//                    {
//                        var header = String.Empty;
//                        foreach (var value in item.Value)
//                        {
//                            header += value + " ";
//                        }

//                        // Trim the trailing space and add item to the dictionary
//                        header = header.TrimEnd(" ".ToCharArray());
//                        dict.Add(item.Key, header);
//                    }
//                }

//                return JsonConvert.SerializeObject(dict, Formatting.Indented);
//            }
//            else { return string.Empty; }
//        }
        
//        private string GetContentType(HttpContent content)
//        {
//            string contentType = string.Empty;
//            if (content != null && content.Headers != null && content.Headers.ContentType != null && content.Headers.ContentType.MediaType != null)
//            {
//                contentType = content.Headers.ContentType.MediaType;
//            }
//            return contentType;
//        }

//    }
//}