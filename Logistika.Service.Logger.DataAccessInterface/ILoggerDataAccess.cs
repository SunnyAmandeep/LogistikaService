using Logistika.Service.Common.Entities.Logger;

namespace Logistika.Service.Logger.DataAccessInterface
{
    public interface ILoggerDataAccess
    {
        void SaveLoginAuditLog(AuditLog AuditLog);

    }
}
