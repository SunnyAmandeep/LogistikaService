

namespace Logistika.Service.Common.BusinessComponentInterface.Notification
{
    public interface INotificationBusinessComponent
    {
        void SendMail(
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
            , string FileAttachments = null);
    }
}
