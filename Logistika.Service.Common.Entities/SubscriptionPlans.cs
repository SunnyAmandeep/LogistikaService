using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class SubscriptionPlans
    {
        public int SubscriptionServiceID { get; set; }
        public int SubscriptionServiceFeatureID { get; set; }
        public string SubscriptionName { get; set; }
        public string SubscriptionCode { get; set; }
        public string SubscriptionPrice { get; set; }
        public string Frequency { get; set; }
        public int DriverLimit { get; set; }
        public int FleetLimit { get; set; }
        public int OrderLimit { get; set; }
        public string FeatureCode { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string ApplicationCode { get; set; }
        public int Sequence { get; set; }
        public bool IsActive { get; set; }
    }
}
