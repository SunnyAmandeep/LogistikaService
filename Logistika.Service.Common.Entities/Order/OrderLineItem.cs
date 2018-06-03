using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities.Order
{
    public class OrderLineItem
    {
        const string _url = "/";
        public string Url
        {
            get
            {
                return _url + Convert.ToString(OrderLineItemId);
            }
        } 
        public long OrderLineItemId { get; set; }
        public string  ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public int OrderedQuantity { get; set; }
        public string ProductNDC { get; set; } 
        public List<OrderShipmentHeader> ShipmentHeaders { get; set; }
        public List<Status> LineItemStatuses { get; set; }
    }
}
