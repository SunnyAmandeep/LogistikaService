using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class AuthUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserType { get; set; }
        public DateTime LastLoginDt{ get; set; }
        public Subscription Subscription { get; set; }
        public IList<MenuBar> Menu { get; set; }
    }
    public class Subscription
    {
        public string PlanDetail { get; set; }
        public DateTime ValidTill { get; set; }
        public DateTime MemberSince { get; set; }
    }
}
