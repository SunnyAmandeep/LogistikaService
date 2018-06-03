
using Logistika.Service.Common.DataAccessInterface.Logger;
using Logistika.Service.Common.EFDataContext;
using Logistika.Service.Common.Entities.ErrorLog;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Logistika.Service.Common.Helper;
using Logistika.Service.Common.Extension;
using Logistika.Service.Common.Entities;
using System.Linq;

namespace Logistika.Service.Common.DataAccess.Logger
{
    public class LoggerDataAccess : BaseDataAccess,ILoggerDataAccess
    {
        public void LogSystemError(SystemErrorLog Log)
        { 
            using (CommonContext con = new CommonContext())
            {
                    con.SystemErrorLog.Add(Log);
                    con.SaveChanges();
            }
        }
        public void LogWebService(WebServiceLog Log)
        {
            
            using (CommonContext con = new CommonContext())
            //try
            { 
                    con.WebServiceLog.Add(Log);
                    con.SaveChanges(); 
            }
            //catch (Exception exception) 
            //{
            //    throw (exception);  
            //}
        }

        public string LogFileImport(string FileName, string Status, string ValidRecords, string InValidRecords,string ErrorFileName,string ImportFileHistory_PK)
        {
            var message = GetOutputParameter("OutputMessage", ParameterDirection.InputOutput);
            var ImportFileHistoryID = GetOutputParameter("ImportFileHistoryID", ParameterDirection.InputOutput);
            var ds = GetDataSetResult("proc_ins_upd_Import_File",
                    new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                    new SqlParameter("FileName", FileName),
                    new SqlParameter("Status", Status),
                    new SqlParameter("ValidRecords", (string.IsNullOrEmpty(ValidRecords) ? DBNull.Value : (object)ValidRecords)),
                    new SqlParameter("InValidRecords", (string.IsNullOrEmpty(InValidRecords) ? DBNull.Value : (object)InValidRecords)),
                    new SqlParameter("ErrorFileName", (string.IsNullOrEmpty(ErrorFileName) ? DBNull.Value : (object)ErrorFileName)),
                    new SqlParameter("ImportFileHistory_PK", (string.IsNullOrEmpty(ImportFileHistory_PK) ? DBNull.Value : (object)ImportFileHistory_PK)),
                    new SqlParameter("LastModifiedBy", ClaimHelper.UserName),
                    new SqlParameter("Operation", (string.IsNullOrEmpty(ImportFileHistory_PK) ? "I" : "U")),
                    message,
                    ImportFileHistoryID);
            if (string.IsNullOrEmpty(ImportFileHistoryID.Value.ToString()))
            {
                return message.Value.ToString();
            }
            return ImportFileHistoryID.Value.ToString();
        }

        public IList<FileImport> GetFileImportList()
        {
            IList<FileImport> fileImportList = null;

            var ds = GetDataSetResult("proc_ins_upd_Import_File",
                    new SqlParameter("CompanyID", ClaimHelper.CompanyId),
                    //new SqlParameter("FileName", DBNull.Value),
                    //new SqlParameter("Status", DBNull.Value),
                    //new SqlParameter("ValidRecords", DBNull.Value),
                    //new SqlParameter("InValidRecords", DBNull.Value),
                    //new SqlParameter("InValidRecords", DBNull.Value),
                    new SqlParameter("LastModifiedBy", ClaimHelper.UserName),
                    new SqlParameter("Operation", "G"));

            if(ds.IsDataSetNotNullAndTableHasRows())
            {
                fileImportList = (from row in ds.Tables[0].AsEnumerable()
                            select new Entities.FileImport
                            {
                                FileName = Convert.ToString(row["FileName"].CheckDBNull()),
                                ValidRecords = Convert.ToInt32(row["ValidRecords"].CheckDBNull()),
                                InValidRecords = Convert.ToInt32(row["InValidRecords"].CheckDBNull()),
                                Status = Convert.ToString(row["Status"].CheckDBNull()),
                                StatusDt = Convert.ToDateTime(row["StatusDt"].CheckDBNull()),
                                LastModifiedBy = Convert.ToString(row["LastModifiedBy"].CheckDBNull()),
                                ErrorFileName = Convert.ToString(row["ErrorFileName"].CheckDBNull()),
                            }).ToList();
                return fileImportList;
            }
            return null;
        }

        public long LogDataEntryTime(DataEntryTimeLog Log)
        {
            var logId = new SqlParameter("LogId", Log.LogId);
            logId.Direction = System.Data.ParameterDirection.InputOutput;

             Exec("proc_Log_Data_Entry_Time",
                         new SqlParameter("DocumentID", Log.DocumentID),
                         new SqlParameter("UserName", Log.UserName),
                         new SqlParameter("DocumentOpenedDt", (string.IsNullOrEmpty(Log.DocumentOpenedDt) ? DBNull.Value : (object)Log.DocumentOpenedDt)),
                         new SqlParameter("DocumentClosedDt",(string.IsNullOrEmpty(Log.DocumentClosedDt) ? DBNull.Value : (object)Log.DocumentClosedDt)),
                         new SqlParameter("DocumentType",  Log.DocumentType),
                         
                         logId,
                         OutputParameter);
             if (Log.LogId > 0)
                 return 0;
             return Convert.ToInt64(logId.Value);
        }
    }
}
