using System.Collections.Generic;

namespace Logistika.Service.Common.Entities.Order
{
    public class OrderShipmentHeader
    {

        public long OrderShipmentHeaderId { get; set; }
        public string ShipmentId { get; set; }
        public IList<OrdershipmentContainerHeader> ShipmentContainers { get; set; }
    }
}
