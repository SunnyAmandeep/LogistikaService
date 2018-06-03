
using Logistika.Service.Common.Entities.ErrorLog;
using Logistika.Service.Common.Entities.Lookup;
using System.Collections.Generic;
using Logistika.Service.Common.Entities;

namespace Logistika.Service.Common.DataAccessInterface.Lookup
{
    public interface ILookupDataAccess
    {
        IList<DropdownData> GetBranch(int CompanyId);
        IList<DropdownData> GetPriceStructure(int CompanyID,float Weight, float Distance);
        IList<DropdownData> GetDocumentType(string AccountType);

        IList<SecurityQuestions> GetSecurityQuestions();
    }
}
