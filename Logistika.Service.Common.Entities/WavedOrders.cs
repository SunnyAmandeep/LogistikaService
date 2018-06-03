using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class WavedOrders
    {
        public int Wave_PK { get; set; }
        public int OrderHeader_FK { get; set; }
        public string LogistikaOrderID { get; set; }
        public string FleetID { get; set; }
        public string OperationType { get; set; }
        public string Sequence { get; set; }
        public Boolean IsActive { get; set; }
        public int WaveOrderID { get; set; }
    }
}
