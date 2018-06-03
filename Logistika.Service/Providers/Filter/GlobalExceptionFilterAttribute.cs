using Logistika.Service.Common.BusinessComponentInterface.Logger;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Logistika.Service.Providers.Filter
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute, IExceptionFilter
    {
        ILoggerBusinessComponent _loggerInstance = null;
        public GlobalExceptionFilterAttribute(ILoggerBusinessComponent Instance)
        {
            _loggerInstance = Instance;
        }
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            try
            {
                _loggerInstance.LogSystemError(actionExecutedContext.Exception);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Internal Error"),
                    ReasonPhrase = actionExecutedContext.Exception.Message
                };

                actionExecutedContext.Response = response;
            }
            catch { }
        }
    }
}