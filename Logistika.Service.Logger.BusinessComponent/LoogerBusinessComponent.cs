using Logistika.Service.Logger.BusinessComponentInterface;
using Logistika.Service.Logger.DataAccessInterface;

namespace Logistika.Service.Logger.BusinessComponent
{
    public class LoggerBusinessComponent : ILoggerBusinessComponent
    {
        ILoggerDataAccess _loggerDataAccess = null;

        public LoggerBusinessComponent(ILoggerDataAccess Instance)
        {
            _loggerDataAccess = Instance;
        } 

        public void SaveLoginAuditLog(Common.Entities.Logger.AuditLog AuditLog)
        {
            _loggerDataAccess.SaveLoginAuditLog(AuditLog);
        }
    }
}
