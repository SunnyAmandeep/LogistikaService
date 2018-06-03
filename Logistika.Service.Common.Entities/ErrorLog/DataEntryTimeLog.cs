
namespace Logistika.Service.Common.Entities.ErrorLog
{
    public class DataEntryTimeLog
    {
        public long LogId { get; set; }
        public string DocumentID { get; set; }
        public string UserName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentOpenedDt { get; set; }
        public string DocumentClosedDt { get; set; }  
    }
}
