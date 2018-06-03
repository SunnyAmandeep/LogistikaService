using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class LogistikaOrderLineItem
    {
        public int      OrderHeader_FK { get; set; }
        public int      DropoffAddressID { get; set; } 
        public int?      ItemType_FK { get; set; }
        public string   Item { get; set; }
        public int      Quantity { get; set; }
        public string   Source { get; set; }
        public string   Image { get; set; }
        public double   Length { get; set; }
        public double   Width { get; set; }
        public double   Height { get; set; }
        public double   Weight { get; set; }
        public string   FreightType { get; set; }
        public string   ShipmentType { get; set; }
        public string   GoodsType { get; set; }
        public string   UOM { get; set; }
        public int      ContainerTrackingCount { get; set; }
        public bool     IsPermitVerifiedFlag { get; set; }
        public bool     IsTrackableFlag { get; set; }
        public bool     IsPackagingRequiredFlag { get; set; }
        public string   LastModifiedBy { get; set; }
    }
}
