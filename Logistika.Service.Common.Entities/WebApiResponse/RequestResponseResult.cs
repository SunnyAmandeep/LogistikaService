using System.Collections.Generic;
using System.Net;

namespace Logistika.Service.Common.Entities.WebApiResponse
{
   public class RequestResponseResult<T> where T:class
    {
        public HttpStatusCode ReponseStatusCode { get; set; }
        public IList<ErrorResponse> ErrorResponse { get; set; }
        public T Response { get; set; }
    }
}
