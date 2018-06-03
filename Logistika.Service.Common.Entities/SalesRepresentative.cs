
namespace Logistika.Service.Common.Entities
{
    public class SalesRepresentative : Person
    {
        public string SalesRepCode { get; set; }
        public string TerritoryCode { get; set; }
        public string TerritoryName { get; set; }
        public string ClientName { get; set; }
        public string SalesRepType { get; set; }
        public string SalesRepStatus { get; set; }
        public SalesRepresentative SupervisingManager { get; set; }
        public HealthCareEntityAddress Address { get; set; }
        public Contact Contact { get; set; }
    }
}
