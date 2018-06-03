using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class Address
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? DropOffDate { get; set; }
        public int    CompanyAddress_FK { get; set; }
        public long   FrameworkAddressID { get; set; }
        public string AddressStatus { get; set; }
        public string AddressType { get; set; }
        public string LandMark { get; set; }
        public string Instruction { get; set; }
        public string Locality { get; set; }
        public string AddressLine1 { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string StateCode { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
