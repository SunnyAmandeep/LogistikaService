
using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities
{
    public class FrameworkUser //: Person
    {

        public string ApplicationCode { get; set; }
        public int FrameworkApplicationUserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PrimaryEmail { get; set; }
        public string PrimaryPhone { get; set; }
        public string PrimaryFax { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public int FrameworkApplicationUserType_FK { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string UserType { get; set; }
        public DateTime MemberSince { get; set; }
        public DateTime ValidTill { get; set; }
        public string PlanDetail { get; set; } 
        public string SessionID { get; set; }
        public DateTime LastLoginDt { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDt { get; set; }
        public Company Company { get; set; }
        public IList<Address> MyAddressList { get; set; }
    }
}

