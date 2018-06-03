using Logistika.Service.Common.BusinessComponentInterface.Logger;
using Logistika.Service.Common.Entities.ErrorLog;
using Logistika.Service.Common.Log;
using System.Web.Http;  

namespace Logistika.Service.Controllers
{
    public class LoggerController : ApiController// BaseController
    {
        IAppLogger _logger = null;
        ILoggerBusinessComponent _dBlogger = null;
        public LoggerController(IAppLogger Instance, ILoggerBusinessComponent DbLoger) 
        {
            _logger = Instance;
            _dBlogger = DbLoger;
        }
        //
        // GET: /Logger/
        [HttpPost]
        [Route("api/Logger/LogError")]
        public string Log([FromBody] AppError Error)
        {
           // Error.User = this.GetRefUser();
            _logger.Info(Error.FormatException());
            _logger.Error(Error.FormatException());
            _logger.Warn(Error.FormatException());
            return "Logged";   
        }
        [HttpPost]
        [Route("api/Logger/LogDataEntry")]
        public long LogDataEntryTime([FromBody] DataEntryTimeLog Data)
        {
            return _dBlogger.LogDataEntryTime(Data);
        }
         
	}
}