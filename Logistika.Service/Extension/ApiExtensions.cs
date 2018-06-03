using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace Logistika.Service.Controllers
{
    public static class ApiExtensions
    {
        public static string GetRefUser(this ApiController cntl)
        {
            try { return ((ClaimsIdentity)cntl.User.Identity).Claims.FirstOrDefault(x => x.Type == "Name").Value; }
            catch { }
            return "LogistikaAppUser";
        }
        public static int GetCompanyId(this ApiController cntl)
        {
            try { return Convert.ToInt32(((ClaimsIdentity)cntl.User.Identity).Claims.FirstOrDefault(x => x.Type == "CompanyId").Value); }
            catch { }
            return 0;
        }
    }
}