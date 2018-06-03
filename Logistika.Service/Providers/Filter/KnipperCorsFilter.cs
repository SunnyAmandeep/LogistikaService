
namespace Logistika.Service.Providers.Filter
{
    //public class KnipperCorsFilter : IActionFilter
    //{
    //    string Origin = "Origin";
    //    string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
    //    public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
    //    {
    //        if (actionExecutedContext.Request.Headers.Contains(Origin))
    //        {
    //            string originHeader = actionExecutedContext.Request.Headers.GetValues(Origin).FirstOrDefault();
    //            if (!string.IsNullOrEmpty(originHeader) && !actionExecutedContext.Response.Headers.Contains(AccessControlAllowOrigin))
    //            {
    //                actionExecutedContext.Response.Headers.Add(AccessControlAllowOrigin, originHeader);
    //            }
    //        }
    //    }

    //    System.Threading.Tasks.Task<HttpResponseMessage> IActionFilter.ExecuteActionFilterAsync(HttpActionContext actionContext, 
    //        System.Threading.CancellationToken cancellationToken,
    //        Func<System.Threading.Tasks.Task<HttpResponseMessage>> continuation)
    //    {
    //        return Task.Factory.StartNew(() =>
    //        {
    //           return continuation().Result;
    //        }, cancellationToken);
    //    }

    //    bool IFilter.AllowMultiple
    //    {
    //        get { throw new NotImplementedException(); }
    //    }
    //}
    
}