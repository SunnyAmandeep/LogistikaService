

using Logistika.Service.Common.BusinessComponentInterface.Lookup;
using Logistika.Service.Common.Common;
using Logistika.Service.Common.DataAccessInterface.Logger;
using Logistika.Service.Common.DataAccessInterface.Lookup;
using Logistika.Service.Common.Entities.ErrorLog;
using Logistika.Service.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Logistika.Service.Common.Entities;

namespace Logistika.Service.Common.BusinessComponent.Lookup
{
    public class LookupBusinessComponent : ILookupBusinessComponent
    {
        ILookupDataAccess _lookupDataAccess = null;
        public LookupBusinessComponent(ILookupDataAccess Instance)
        {
            _lookupDataAccess = Instance;
        }

        public System.Collections.Generic.IList<Entities.Lookup.DropdownData> GetBranch()
        {
            // var v = ClaimHelper.UserName;
            var companyId = Convert.ToInt32(ClaimHelper.CompanyId);
            return _lookupDataAccess.GetBranch(companyId);
        }

        public System.Collections.Generic.IList<Entities.Lookup.DropdownData> GetPriceStructure(int CompanyID,  float Weight, float Distance)
        {
            return _lookupDataAccess.GetPriceStructure(CompanyID, Weight, Distance);
        }

        public System.Collections.Generic.IList<Entities.Lookup.DropdownData> GetDocumentType(string AccountType)
        {
            return _lookupDataAccess.GetDocumentType(AccountType);
        }

        public IList<SecurityQuestions> GetSecurityQuestions()
        {
            return _lookupDataAccess.GetSecurityQuestions();
        }
    }
}
