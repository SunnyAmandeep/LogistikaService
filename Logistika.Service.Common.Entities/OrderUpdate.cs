using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class OrderUpdate
    {
        public string RequestType { get; set; }
        public string OrderID { get; set; }
        public string PickupDate { get; set; }
        public string StatusDt { get; set; }
    }
}
