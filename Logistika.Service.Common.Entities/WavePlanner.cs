using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class WavePlanner
    {
        public int Wave_PK { get; set; }
        public string EmployeeID { get; set; }
        public string WaveNumber { get; set; }
        public DateTime WaveDate { get; set; }
        public string WaveStartTime { get; set; }
        public string WaveEndTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDt { get; set; }
        public string WaveStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FleetID { get; set; }
    }
}
