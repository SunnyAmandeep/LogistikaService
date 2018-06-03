using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    //public class OrderDetail
    //{
    //    public string WaveID { get; set; }
    //    public string OrderID { get; set; }
    //    public DateTime OrderDate { get; set; }
    //    public string TrackingNumber { get; set; }
    //    public string TrackingUrl { get; set; }
    //    public string Status { get; set; }
    //    public double Price { get; set; }
        
    //    public string OrderType { get; set; }
    //    public string ItemType { get; set; }
        
    //    public string DriverName { get; set; }
    //    //public string ReceiverName { get; set; }

    //    public DateTime PickupDate { get; set; }
    //    public DateTime DropOffDate { get; set; }

    //    public string CustomerName { get; set; }
    //    public string CustomerPhone { get; set; }
    //    public string CustomerEmail { get; set; }
    //    public string VehicleNumber { get; set; }
    //    public string PaymentMode { get; set; }
    //    public string InvoiceType { get; set; }
    //    public string ServiceLevel { get; set; }

    //    public IList<LineItemDetail> LineItemDetail { get; set; }
    //    public IList<Document> Documents { get; set; }
    //    public IList<Address> PickupAddress { get; set; }
    //    public IList<Address> DropoffAddress { get; set; }
    //    public string ReceiverSignatureUrl { get; set; }
    //    public string SenderSignatureUrl { get; set; }
    //}
    public class Document
    {
        public string DocumentName { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public DateTime? IssuedDate { get; set; }//
        public DateTime? ExpirationDate { get; set; }//
        public string IssuedState { get; set; }//
        public string DocumentUrl { get; set; }
        public bool IsActive { get; set; }
        public string CreatedDt { get; set; }

    }
    //public class LineItemDetail{    
    //    public string ItemName { get; set; }
    //    public string Quantity { get; set; }
    //    public string ImageUrl { get; set; }
    //    //public DateTime DropOffDate { get; set; }
    //    public string ReceiverName { get; set; }
    //}

}
