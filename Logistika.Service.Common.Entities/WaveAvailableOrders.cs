using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class WaveAvailableOrders
    {
        public int OrderHeader_PK { get; set; }
        public string LogistikaOrderID { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public string OrderType { get; set; }
        public string OperationType { get; set; }
        public string OrderStatus { get; set; }
        public Address OrderAddress { get; set; }
    }
}
