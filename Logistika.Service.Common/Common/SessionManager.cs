using System;
using System.Web;

namespace Logistika.Service.Common.Common
{
    public class SessionManager
    {
        public static string UserId { get {
            var userName = Convert.ToString(HttpContext.Current.Items["Authenticated_User"]);
            return userName;
        }
            set {  HttpContext.Current.Items["Authenticated_User"] = value;
            }
        }
         public static string RefUserId { get; set; }
       
    }
}
