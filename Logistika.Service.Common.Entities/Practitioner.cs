using Logistika.Service.Common.Entities.Lookup;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities
{
    public class Practitioner : Person
    {
        public string PractitionerId { get; set; }
        public string StateLicenseNumber { get; set; }
        public string State { get; set; }
        public string PracitionerTargetId { get; set; }
        public string Specialty { get; set; }
        public string SecondarySpecialty { get; set; }
        public string ProfessionalDesignation { get; set; }
        public string MeNumber { get; set; }
        public string NpiNumber { get; set; }
        public string ImsId { get; set; }
        public Contact Contact { get; set; }
        public IList<DropdownData> OtherValues { get; set; }

    }
}
