using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class WaveModal
    {
        public IList<WavePlanner> WavePlannerList { get; set; }
        public IList<WaveAvailableOrders> AvailableOrders { get; set; }
        public IList<WavedOrders> WavedOrders { get; set; }
    }
}
