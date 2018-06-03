
using Logistika.Service.Common.DataAccessInterface.Notification;
using Logistika.Service.Common.EFDataContext;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Logistika.Service.Common.DataAccess.Notification
{


    public class NotificationDataAccess :BaseDataAccess,  INotificationDataAccess
    {
        public NotificationDataAccess() : base(new CommonContext()) { 
        
        }
        public void SendMail(string Body, string CreatedBy,  int EmailNotificationId, string ReplyToAddress = null, string ProfileName = null, string Subject = null, string FromEmailAddress = null, string ToEmailAddress = null, string CCEmailAddress = null, string BCCEmailAddress = null, string BodyFormat = null, string Importance = null, string Sensitivity = null, string FileAttachments = null)
        {
            
                SqlParameter emailNotification_PK = new SqlParameter("@EmailNotification_PK", SqlDbType.VarChar, 8000);
                emailNotification_PK.Direction = ParameterDirection.Output; 

                bool isSuccess = Exec("[dbo].[proc_ins_Email_Notification]",               
                new SqlParameter("ProfileName", ProfileName),
                new SqlParameter("CreatedBy", CreatedBy),
                new SqlParameter("Subject", Subject),
                new SqlParameter("Body", Body),
                new SqlParameter("FromEmailAddress", FromEmailAddress),
                new SqlParameter("ToEmailAddress", ToEmailAddress),
                new SqlParameter("CCEmailAddress", CCEmailAddress),
                new SqlParameter("BCCEmailAddress", BCCEmailAddress),
                new SqlParameter("BodyFormat", BodyFormat),
                new SqlParameter("Importance", Importance),
                new SqlParameter("Sensitivity", Sensitivity),
                new SqlParameter("ReplyToAddress", ReplyToAddress),
                new SqlParameter("FileAttachments", FileAttachments),                
                emailNotification_PK
                );

                var emailNotificationId = Convert.ToInt32(emailNotification_PK.Value);
        }
    }
}
