using System;

namespace Logistika.Service.Common.Entities
{
    public class Holiday
    {
        public int? Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
