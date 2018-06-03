
using Logistika.Service.Common.Entities;
using Logistika.Service.Common.Entities.ErrorLog;
using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.BusinessComponentInterface.Logger
{
    public interface ILoggerBusinessComponent
    {
        void LogSystemError(Exception Log);
        void LogWebService(WebServiceLog Log);
        long LogDataEntryTime(DataEntryTimeLog Log);
        string LogFileImport(string FileName, string Status, string ValidRecords, string InValidRecords, string ErrorFileName, string ImportFileHistory_PK);
        IList<FileImport> GetFileImportList();
    }
}
