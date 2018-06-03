
using Logistika.Service.Common.BusinessComponentInterface.Notification;
using Logistika.Service.Common.DataAccessInterface.Notification;
using System;

namespace Logistika.Service.Common.BusinessComponent.Notification
{
    public class NotificationBusinessComponent : INotificationBusinessComponent
    {
        INotificationDataAccess _notificationDataAccess = null;

        public NotificationBusinessComponent(INotificationDataAccess Instance)
        {
            _notificationDataAccess = Instance;
        }

        public void SendMail(
               string Body
             , string CreatedBy
             , int EmailNotificationId
             , string ReplyToAddress = null
             , string ProfileName = null
             , string Subject = null
             , string FromEmailAddress = null
             , string ToEmailAddress = null
             , string CCEmailAddress = null
             , string BCCEmailAddress = null
             , string BodyFormat = null
             , string Importance = null
             , string Sensitivity = null
             , string FileAttachments = null)
        {
            if (string.IsNullOrEmpty(ProfileName)) { ProfileName = SiteConfigurationManager.GetAppSettingKey("ProfileName"); }
            if (string.IsNullOrEmpty(Subject)) { Subject = SiteConfigurationManager.GetAppSettingKey("Subject"); }
            if (string.IsNullOrEmpty(ToEmailAddress)) { ToEmailAddress = SiteConfigurationManager.GetAppSettingKey("ToEmailAddress"); }
            if (string.IsNullOrEmpty(FromEmailAddress)) { FromEmailAddress = SiteConfigurationManager.GetAppSettingKey("FromEmailAddress"); }
            if (string.IsNullOrEmpty(CCEmailAddress)) { CCEmailAddress = SiteConfigurationManager.GetAppSettingKey("CCEmailAddress"); }
            if (string.IsNullOrEmpty(BCCEmailAddress)) { BCCEmailAddress = SiteConfigurationManager.GetAppSettingKey("BCCEmailAddress"); }
            if (string.IsNullOrEmpty(ReplyToAddress)) { ReplyToAddress = SiteConfigurationManager.GetAppSettingKey("ReplyToAddress"); }

            if ((string.IsNullOrEmpty(ToEmailAddress) && ToEmailAddress.Length == 0) || (string.IsNullOrEmpty(FromEmailAddress) && FromEmailAddress.Length == 0))
            {
                throw new Exception("ToEmailAddress or FromEmailAddress is not provided in the config file.");
                // SaveApplicationErrorLog("ExceptionHandlerDataAccess", "ToEmailAddress or FromEmailAddress is not provided!", "");
                 
            }


            _notificationDataAccess.SendMail(
                   Body
                 , CreatedBy
                 , EmailNotificationId
                 , ReplyToAddress
                 , ProfileName
                 , Subject
                 , FromEmailAddress
                 , ToEmailAddress
                 , CCEmailAddress
                 , BCCEmailAddress
                 , BodyFormat
                 , Importance
                 , Sensitivity
                 , FileAttachments
                 );
        }
    }
}
