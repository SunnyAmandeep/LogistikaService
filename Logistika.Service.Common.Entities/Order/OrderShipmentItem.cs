using System;

namespace Logistika.Service.Common.Entities.Order
{
    public class OrderShipmentItem
    {
        public long OrderShipmentItemId { get; set; }
        public int ShippedQuantity { get; set; }
        public string LotNumber { get; set; }
        public DateTime? LotExpirationDate { get; set; }
        public string TrackingNumber { get; set; }
        public string TrackingCarrier { get; set; }
        public string TrackingCarrierURL { get; set; }
    }
}
