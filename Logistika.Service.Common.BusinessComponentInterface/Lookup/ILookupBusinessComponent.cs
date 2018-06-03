
using Logistika.Service.Common.Entities.ErrorLog;
using Logistika.Service.Common.Entities.Lookup;
using System;
using System.Collections.Generic;
using Logistika.Service.Common.Entities;

namespace Logistika.Service.Common.BusinessComponentInterface.Lookup
{
    public interface ILookupBusinessComponent
    {
        IList<DropdownData> GetBranch();
        IList<DropdownData> GetPriceStructure(int CompanyID, float Weight, float Distance);
        IList<DropdownData> GetDocumentType(string AccountType);

        IList<SecurityQuestions> GetSecurityQuestions();
    }
}
