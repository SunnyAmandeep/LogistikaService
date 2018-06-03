
using System;
namespace Logistika.Service.Common.Entities
{
    public class CompanyAddress //: Person
    {

        public int CompanyID { get; set; }
        public int CompanyAddressID { get; set; }
        public string CompanyName { get; set; }
        public Boolean? IsCompanyActive { get; set; }
        public int FrameworkAddressID { get; set; }
        public string AddressType { get; set; }
        public string LandMark { get; set; }
        public string AddressLine1 { get; set; }
        public string Locality { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string StateCode { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Boolean? IsCompanyAddressActive { get; set; }
    }
}