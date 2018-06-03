using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities.Order
{
    public class OrdershipmentContainerHeader
    { 
        public long OrderShipmentContainerHeaderId { get; set; }
        public string TrackingNumber { get; set; }
        public string TrackingCarrier { get; set; }
        public string TrackingCarrierURL { get; set; }
        public DateTime? ReceivedOnDate { get; set; }
        public string ReceivedByName { get; set; }
        public IList<OrderShipmentItem> ShipmentItems { get; set; }
    }
}
