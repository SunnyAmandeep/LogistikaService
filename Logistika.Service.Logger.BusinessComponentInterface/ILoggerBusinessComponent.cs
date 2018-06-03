using Logistika.Service.Common.Entities.Logger;

namespace Logistika.Service.Logger.BusinessComponentInterface
{
    public interface ILoggerBusinessComponent
    {
        void SaveLoginAuditLog(AuditLog AuditLog); 
    }
}
