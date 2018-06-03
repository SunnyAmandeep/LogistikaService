using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class CompanySubscription
    {
        public Company Company { get; set; }
        public int UserCompanyID { get; set; }
        public int SubscriptionServiceID { get; set; }
        public string SubscriptionName { get; set; }
        public string SubscriptionCode { get; set; }
        public string SubscriptionPrice { get; set; }
        public string Frequency { get; set; }
        public int DriverLimit { get; set; }
        public int FleetLimit { get; set; }
        public int OrderLimit { get; set; }
        public CompanyAddress CompanyAddress { get; set; }
    }
}