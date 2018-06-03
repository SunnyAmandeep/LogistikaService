using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities.Order
{
    public class OrderHeader
    {
        const string _url = "/";
        public string Url { get {
            return _url + Convert.ToString(OrderHeaderId);
        } }
        public long OrderHeaderId { get; set; }
        public string OrderSourceOrderId { get; set; }
        public string OrderFormSequenceNumber { get; set; }
        public string KnipperOrderId { get; set; }
        public string OrderSourcePersonID { get; set; }
        public string PractitionerTargetID { get; set; }
        public DateTime? TransactionDate { get; set; }
        public IList<OrderLineItem> LineItems { get; set; }
        public IList<Status> OrderHeaderStatuses { get; set; }
        public Practitioner Practitioner { get; set; }
        public HealthCareEntityAddress ShipToAddress { get; set; }
        public HealthCareEntityAddress OrderByAddress { get; set; }
        public SalesRepresentative SalesRepresentative { get; set; }
        public IList<OrderShipmentHeader> OrderShipmentHeaders { get; set; }

        

    }
}
