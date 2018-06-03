using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Logistika.Service.Common.Helper
{
    public static class ClaimHelper
    {
        public static string UserName
        {
            get
            {
                var name = GetClaim("Name");
                if (string.IsNullOrEmpty(name))
                {
                    name = GetClaim("sub");
                }
                return name;
            }
        }
        public static string CompanyId
        {
            get
            {
                return GetClaim("CompanyId");
            }
        }
        private static string GetClaim(string Key)
        {
            try { return ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims.FirstOrDefault(x => x.Type == Key).Value; }
            catch { }
            return string.Empty;
        }
    }
}
