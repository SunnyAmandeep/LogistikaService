using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Logistika.Service.Providers.Filter
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {

                
                var errors = new List<string>(); 
                foreach (var v in actionContext.ModelState) { 
                    foreach (var er in v.Value.Errors) {
                        errors.Add(er.ErrorMessage);
                    }              
                }

                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Internal Error")),
                    ReasonPhrase = string.Join(", ", errors)                 
                };
                actionContext.Response = resp;
                    
                    //= actionContext.Request.CreateErrorResponse (
                    //HttpStatusCode.BadRequest, actionContext.ModelState);
               
                   
                }
            }
        }
    } 