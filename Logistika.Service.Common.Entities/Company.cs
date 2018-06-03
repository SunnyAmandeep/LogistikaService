using System;
namespace Logistika.Service.Common.Entities
{
    public class Company
    {
        public string CompanyName { get; set; }
        public int    CompanyID { get; set; }
        public string TaxIdentification { get; set; }
        public string MemberSince { get; set; }
        public string SubscriptionName { get; set; }
        public string Revenue { get; set; }
        public Boolean? IsActive { get; set; }
        public string AuthCode { get; set; }
        public string SubscriptionCode { get; set; }
        public string SubscriptionAmount { get; set; }
        public string SubscriptionFrequency { get; set; }


    }
}