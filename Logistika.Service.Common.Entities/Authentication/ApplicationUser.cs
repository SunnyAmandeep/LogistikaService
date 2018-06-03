using Logistika.Service.Common.Entities.CorsConfig;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities.Authentication
{
    public class ApplicationUser
    {

        public int? ConsumerPK { get; set; }
        public int? CredentialPK { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public IList<WsApiCorsConfig> CorsConfig { get; set; }
    }
}
