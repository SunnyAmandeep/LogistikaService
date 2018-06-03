using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class ExpenseLog
    {
        public int      CompanyAddressID { get; set; }
        public DateTime TransactionDt { get; set; }
        public string   TransactionType { get; set; }
        public string   Purpose { get; set; }
        public string   Name { get; set; }
        public decimal    Amount { get; set; }
        public bool     IsApproved { get; set; }
        public string   ApprovedBy { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string   Comment { get; set; }
    }
}
