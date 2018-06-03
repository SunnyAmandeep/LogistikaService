using System;

namespace Logistika.Service.Common.Entities.Lookup.Search
{
    public class SearchParameter
    {
        public string OrderSourceOrderId { get; set; }
        public string KnipperOrderId { get; set; }
        public string OrderFormSequenceNumber { get; set; }
        public string Client { get; set; }
        public int Client_FK { get; set; }
        public string ProjectId { get; set; }
        public string JobId { get; set; }
        public string Status { get; set; }
        public string PractitionerId { get; set; }
        public string PractitionerFirstName { get; set; }
        public string PractitionerLastName { get; set; } 
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedStartDt { get; set; }
        public DateTime? CreatedEndDt { get; set; }
        public DateTime? ModifiedStartDt { get; set; }
        public DateTime? ModifiedEndDt { get; set; }
    }
}
