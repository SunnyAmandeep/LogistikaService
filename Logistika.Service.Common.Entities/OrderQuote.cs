using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class OrderQuote
    {
        public string  OrderID { get; set; }
        public double? QuoteExpedited { get; set; }
        public double? QuoteExpress { get; set; }
        public double? QuoteEconomy { get; set; }

        public string  OptedService { get; set; }
        public string  OptedAmount  { get; set; }
        public Payment Payment { get; set; }
    }
}
