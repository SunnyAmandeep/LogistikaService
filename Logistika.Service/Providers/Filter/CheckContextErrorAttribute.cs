using Logistika.Service.Common.Extension;
using Logistika.Service.Common.RequestContextHander;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Logistika.Service.Providers.Filter
{
    public class CheckContextErrorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (KnipperRequestContext.IsError)
            {

                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Internal Error")),
                    ReasonPhrase = KnipperRequestContext.GetError().ListToString(", ")                   
                };
                actionExecutedContext.Response = resp;
                   //=  actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError(KnipperRequestContext.GetError().ListToString()));
            }
        }
    }
}