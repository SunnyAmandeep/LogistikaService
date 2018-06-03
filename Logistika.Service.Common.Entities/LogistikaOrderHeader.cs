using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class LogistikaOrderHeader
    {
        public int?     CompanyID { get; set; }
        public string   OrderHeaderID { get; set; }
        public string   ClientOrderSource { get; set; }
        public string   VendorOrderID { get; set; }
        public string   LogistkaOrderID { get; set; }
        public int?      WaveID { get; set; }
        public DateTime OrderDate { get; set; }
        public string   TrackingNumber { get; set; }
        public string   TrackingUrl { get; set; }
        public string   Status { get; set; }
        public IList <Address> PickupAddress { get; set; }
        public IList <Address> DropoffAddress { get; set; }
        public string   OrderByName { get; set; }
        public DateTime? PickUpDate { get; set; }
        //public DateTime? DropOffDate { get; set; }
        public string   CustomerName { get; set; }
        public string   CustomerPhone { get; set; }
        public string   CustomerEmail { get; set; }
        public string   VehicleNumber { get; set; }
        public string   InvoiceType { get; set; }
        //public string   ServiceLevel { get; set; }
        public string   OrderByPhoneNumber { get; set; }
        public string   OrderByEmail { get; set; }
        //public string   ShipToPhoneNumber { get; set; }
        //public string   ShipToEmail { get; set; }
        public string   PrimaryEmailAddress { get; set; }
        public string   ParentLogistikaOrderID { get; set; }
        public string   OrderType { get; set; }
        public string   ItemType { get; set; }
        public string   DriverName { get; set; }
        public string   ReceiverName { get; set; }
        public string   FreightType { get; set; }
        public string   ServiceCode { get; set; }
        public Boolean  IsCancellableFlag { get; set; }
        public Boolean  IsSalesTaxPaidFlag { get; set; }
        public Boolean  IsInsuredFlag { get; set; }
        public double?  InsuredAmount { get; set; }
        public string   LastModifiedBy { get; set; }
        public IList <LogistikaOrderLineItem> LineItem {get; set;}
        public Payment  Payment { get; set; }
        public Quote    Quote { get; set; }
        public IList<Document> Documents { get; set; }
        public string   ReceiverSignatureUrl { get; set; }
        public string   SenderSignatureUrl { get; set; }

        public string   CallType { get; set; }

        public double? QuoteExpedited { get; set; }
        public double? QuoteExpress { get; set; }
        public double? QuoteEconomy { get; set; }
    }
}
public class Quote {
    //Name and Price
    public Dictionary<string, Double> Quotes { get; set; } 
    public string OptedQuote  { get; set; } 
}
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