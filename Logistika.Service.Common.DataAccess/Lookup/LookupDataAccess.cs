
using Logistika.Service.Common.DataAccessInterface.Lookup;
using Logistika.Service.Common.EFDataContext;
using Logistika.Service.Common.Entities;
using Logistika.Service.Common.Entities.ErrorLog;
using Logistika.Service.Common.Entities.Lookup;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Logistika.Service.Common.Extension;
using Logistika.Service.Common.Helper;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Linq;
namespace Logistika.Service.Common.DataAccess.Lookup
{
    public class LookupDataAccess : BaseDataAccess, ILookupDataAccess
    {
        public IList<SecurityQuestions> GetSecurityQuestions()
        {
            IList<SecurityQuestions> SecurityQuestions = null;
            var ds = GetDataSetResult("dbo.proc_get_SecurityQuestions");
            if (ds.IsDataSetNotNullAndTableHasRows())
            {
                SecurityQuestions = (from row in ds.Tables[0].AsEnumerable()
                        select new Entities.SecurityQuestions
                        {
                            Sequence = Convert.ToInt32(row["Sequence"].CheckDBNull()),
                            Code = Convert.ToString(row["Code"].CheckDBNull()),
                            Description = Convert.ToString(row["Description"].CheckDBNull()),
                            Value = Convert.ToString(row["Value"].CheckDBNull()),
                            GroupID = Convert.ToInt32(row["GroupID"].CheckDBNull()),
                            IsIncluded = Convert.ToBoolean(row["IsIncluded"].CheckDBNull()),
                        }).ToList();

            
            }
            return SecurityQuestions;
        }

        public IList<DropdownData> GetBranch(int CompanyId)
        {
            return GetList<DropdownData>(new Dictionary<string, string>{
                {"BranchName","Text"},{"BranchId","Value"}
            }, "proc_get_Branch",
             new SqlParameter("Company_FK", CompanyId), OutputParameter);
        }

        public IList<DropdownData> GetPriceStructure(int CompanyID, float Weight, float Distance)
        {
            return GetList<DropdownData>(new Dictionary<string, string>{
                {"ServiceCode","Text"},{"Price","Value"}
            }, "proc_get_Price_Structure",
             new SqlParameter("CompanyID", CompanyID),
             new SqlParameter("Weight", Weight),
             new SqlParameter("Distance", Distance));
        }

        // Document Type
        public IList<DropdownData> GetDocumentType(string AccountType)
        {
            return GetList<DropdownData>(new Dictionary<string, string>{
                {"DocumentType","Text"},{"IssuingAuthority","Value"}
            }, "proc_get_DocumentType",
             new SqlParameter("AccountType", AccountType));
        }
    }
}
