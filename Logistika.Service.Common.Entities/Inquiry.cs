using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class Inquiry
    {
        public int InquiryID { get; set; }
        public string InquirySource { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PreferredCallback { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string SLA { get; set; }
        public string Resolution { get; set; }
        public string Status { get; set; }
        public DateTime ? ResponseDt { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime ? LastModifiedDt { get; set; }
    }
}
