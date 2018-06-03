using Logistika.Service.Common.BusinessComponentInterface.User;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
//using System.Web.Mvc;

namespace Logistika.Service.Controllers
{
    public class BaseController : ApiController
    {
        IUserBusinessComponent _userBusinessComponent = null;
        public BaseController()
        {
        }
        public BaseController(IUserBusinessComponent UserInstance)
        {
            _userBusinessComponent = UserInstance;
        }
        //public string UserName { 
        //    get {
        //        try { return ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "username").Value; }
        //        catch { } 
        //    return string.Empty;
        //} 
        //}

        //[Route("api/KeepAlive")]
        //[HttpGet]
        //public System.Web.Mvc.EmptyResult KeepAlive()
        //{
        //    return new System.Web.Mvc.EmptyResult();
        //}
        public void Authorize(string Url, string OprationCode)
        {
            //if (!_userBusinessComponent.CheckRestrictedServiceAuthorization(this.GetRefUser(), Url, OprationCode, "MVC_KFISPORTAL"))
            //{
            //    throw new System.Web.HttpException(203, "Action execution not allowed");
            //}
        }
    }
}