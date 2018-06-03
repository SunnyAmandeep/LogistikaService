
namespace Logistika.Service.Common.Entities
{
    public class HealthCareEntityAddress : Address
    {
        public string HealthCareEntityName { get; set; }
        public string AttentionTo { get; set; }
        public string CompanyName { get; set; }
        public Person HealthCareProfessional { get; set; } 
        public string DeaLicenseNumber { get; set; }
        public string StateLicenseNumber { get; set; }
        public string StateOfLicensure { get; set; }
    }
}
