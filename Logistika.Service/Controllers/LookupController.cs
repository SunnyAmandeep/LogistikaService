using Logistika.Service.Common.BusinessComponentInterface.Lookup;
using Logistika.Service.Common.Entities.Lookup;
using Logistika.Service.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Logistika.Service.Controllers
{
    //[Authorize]

    public class LookupController : ApiController
    {
        ILookupBusinessComponent _businessInstance = null;
        public LookupController(ILookupBusinessComponent Instance)
        {
            _businessInstance = Instance;
        }
        [HttpGet]
        [Route("api/Company/Branch")]
        public IList<DropdownData> GetBranch()
        {
            return _businessInstance.GetBranch();
        }

        [HttpGet]
        [Route("api/Company/PriceStructure")]
        public IList<DropdownData> GetPriceStructure(int CompanyID, float Weight, float Distance)
        {
            return _businessInstance.GetPriceStructure(CompanyID,Weight,Distance);
        }

        [HttpGet]
        [Route("api/Company/DocumentType")]
        public IList<DropdownData> GetDocumentTypeList(string AccountType)
        {
            return _businessInstance.GetDocumentType(AccountType);
        }

        [HttpGet]
        [Route("api/Company/SecurityQuestions")]
        public IList<SecurityQuestions> GetSecurityQuestions()
        {
            return _businessInstance.GetSecurityQuestions();
        }
    }
}
