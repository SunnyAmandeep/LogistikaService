
using Logistika.Service.Common.BusinessComponentInterface.Logger;
using Logistika.Service.Common.Common;
using Logistika.Service.Common.DataAccessInterface.Logger;
using Logistika.Service.Common.Entities.ErrorLog;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Logistika.Service.Common.Entities;

namespace Logistika.Service.Common.BusinessComponent.Logger
{
    public class LoggerBusinessComponent : ILoggerBusinessComponent
    {
        ILoggerDataAccess _loggerDataAccess = null;
        public LoggerBusinessComponent(ILoggerDataAccess Instance) {
            _loggerDataAccess = Instance;
        }

        public void LogSystemError(Exception Ex)
        {

            SystemErrorLog errorLog = new SystemErrorLog();
            //errorLog.Number = ex.LineNumber();
            errorLog.Severity = "16";
            errorLog.State = "1";
            errorLog.ProcedureID = Ex.TargetSite.Name;
            errorLog.Line = errorLog.Number != null ? errorLog.Number.ToString() : null;
            errorLog.AdditionalInfo = Ex.StackTrace;
            errorLog.Message = Ex.Message;
            errorLog.LastModifiedDt = DateTime.Now; 
            try
            {
                errorLog.LastModifiedBy = SessionManager.UserId;
            }
            catch { errorLog.LastModifiedBy = SiteConfigurationManager.GetAppSettingKey("ApplicationName"); }
            //errorLog.AuditAction = "I";

            StringBuilder sb = new StringBuilder();

            while (Ex.InnerException != null)
            {
                Ex = Ex.InnerException;
                sb.Append("-- inner excption -- "  + Environment.NewLine);
                sb.Append("Message: " + Ex.Message + Environment.NewLine);
                sb.Append("Source: " + Ex.Source + Environment.NewLine);
                sb.Append("TargetSite: " + Ex.TargetSite + Environment.NewLine);
                sb.Append("StackTrace: " + Ex.StackTrace + Environment.NewLine);
                sb.Append(Environment.NewLine + Environment.NewLine); 
            }
            sb.Append("--------------------------- " + Environment.NewLine);
            errorLog.AdditionalInfo = errorLog.AdditionalInfo + sb.ToString();
            
            Task.Factory.StartNew(()=>
            _loggerDataAccess.LogSystemError(errorLog)
            );   
        }

        public void LogWebService(WebServiceLog Log)
        {

            Log.LastModifiedBy = SiteConfigurationManager.GetAppSettingKey("ApplicationName");
            Log.LastModifiedDt = DateTime.Now;
            Task.Factory.StartNew(()=>
                ExceptionHelper.ExceptionHelper.SafeVoidExecute(()=> _loggerDataAccess.LogWebService(Log))
            );           
        }

        public long LogDataEntryTime(DataEntryTimeLog Log)
        {
            return _loggerDataAccess.LogDataEntryTime(Log);       
        }

        public string LogFileImport(string FileName, string Status, string ValidRecords, string InValidRecords, string ErrorFileName, string ImportFileHistory_PK)
        {
            return _loggerDataAccess.LogFileImport(FileName, Status, ValidRecords, InValidRecords, ErrorFileName, ImportFileHistory_PK);
        }

        public IList<FileImport> GetFileImportList()
        {
            return _loggerDataAccess.GetFileImportList();
        }
    }
}
