
using Logistika.Service.Common.Entities.ErrorLog;
using Logistika.Service.Common.Entities;
using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.DataAccessInterface.Logger
{
    public interface ILoggerDataAccess
    {
        void LogSystemError(SystemErrorLog Log);
        void LogWebService(WebServiceLog Log);
        long LogDataEntryTime(DataEntryTimeLog Log);
        string LogFileImport(string FileName, string Status, string ValidRecords, string InValidRecords, string ErrorFileName, string ImportFileHistory_PK);
        IList<FileImport> GetFileImportList();
        
    }
}
