using System.Web;
using System.Web.Configuration;
namespace Logistika.Service.Common
{
    public class SiteConfigurationManager
    { 
        public static string GetAppSettingKey(string key)
        {
            return WebConfigurationManager.AppSettings[key];
        }

        public static string BaseWorkingFolder{
            get { return HttpContext.Current.Server.MapPath("~"); }
        }   
    }
}
