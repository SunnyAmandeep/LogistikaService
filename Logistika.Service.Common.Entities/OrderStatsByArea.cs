using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities
{
    public class OrderStatsArea
    {
        public string CountryCode { get; set; }
        public string StateCode{ get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public int TotalOrders { get; set; }
    }
}
