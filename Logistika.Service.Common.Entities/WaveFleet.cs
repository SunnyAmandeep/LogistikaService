using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class WaveFleet
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string ModelYear { get; set; }
        public string FleetID { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public string Colour { get; set; }
        public string CompanyAddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string LandMark { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
    }
}
