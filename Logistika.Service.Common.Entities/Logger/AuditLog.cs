
namespace Logistika.Service.Common.Entities.Logger
{
    public class AuditLog
    {
        public string UserName { get; set; }
        public string IPAddress { get; set; }
        public string MachineName { get; set; }
        public string Message { get; set; }
        public string UserFK { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
    }
}
