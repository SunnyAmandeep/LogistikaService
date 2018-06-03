using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities
{
    public class OrderStats
    {

        public string ReportStatus { get; set; }
        public int OrderCount { get; set; }
        public string DeliveryCategory { get; set; }
        public int PickupSLAMet { get; set; }
        public int DropSLAMet { get; set; }
    }
}