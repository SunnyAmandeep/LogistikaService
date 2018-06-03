using System;

namespace Logistika.Service.Common.Entities
{
    public class OrderDriverInfo
    {
        public string VendorOrderID { get; set; }
        public string WaveID { get; set; }
        public string TrackOrder { get; set; }
        public DateTime OrderDate { get; set; }
        public int CompanyID { get; set; }
        public string LogistikaOrderID { get; set; }
        public string ParentLogistikaOrderID { get; set; }
        public string PickupAddress { get; set; }
        public string DropOffAddress { get; set; }
        public string PickUpDate { get; set; }
        public string DropOffDate { get; set; }
        public string PickUpSignature { get; set; }
        public string DropOffSignature { get; set; }
        public string ItemType { get; set; }
        public string AssignedDriver { get; set; }
        public string AssignedVehicle { get; set; }
        public int FrameworkApplicationUserID { get; set; }
        public string OrderStatusCode { get; set;}
        public string OrderStatus { get; set; }
        public string DriverStatus { get; set; }
        public decimal BatteryStatus { get; set; }
    }
}

