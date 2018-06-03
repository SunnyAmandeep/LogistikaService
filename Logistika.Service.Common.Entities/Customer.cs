using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class Customer
    {
        public string ApplicationCode { get; set; }
        public int? FrameworkApplicationUserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PrimaryEmail { get; set; }
        public string PrimaryPhone { get; set; }
        public string PrimaryFax { get; set; }
        public string LastLoginDt { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDt { get; set; }
        public string FrameworkAddressID { get; set; }
        public Address CustomerAddress { get; set; }
        public string AddressType { get; set; }
        public bool IsActive { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string TaxIdentification { get; set; }
        public bool IsCompanyActive { get; set; }
        public string Sales { get; set; }
        public string OrderCount { get; set; }
        public DateTime MemberSince { get; set; }
    }
}