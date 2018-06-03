using Logistika.Service.Common.EFDataContext;
using Logistika.Service.Common.Entities.Logger;
using Logistika.Service.Logger.DataAccessInterface;
using System.Data.SqlClient;

namespace Logistika.Service.Logger.DataAccess
{
    public class LoggerDataAccess : BaseDataAccess, ILoggerDataAccess
    {
         

        public LoggerDataAccess()
            : base()
        { }
        #region DBMethods 

        public void SaveLoginAuditLog(AuditLog AuditLog)
        {
             Exec("proc_ins_LoginAuditTrail"
                        , new SqlParameter("UserName", AuditLog.UserName)
                        , new SqlParameter("IPAddress", AuditLog.IPAddress)
                        , new SqlParameter("MachineName", AuditLog.MachineName)
                        , new SqlParameter("Message", AuditLog.Message)
                        , new SqlParameter("UserFK", AuditLog.UserFK)
                        , new SqlParameter("Status", AuditLog.Status)
                        , new SqlParameter("CreatedBy", AuditLog.CreatedBy)
                );
        }

        #endregion
    }
}
