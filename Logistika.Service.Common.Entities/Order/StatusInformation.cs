using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities.Order
{
    public class Status
    {
        public string StatusId { get; set; }
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public DateTime StatusDate { get; set; }
        public Boolean IsCurrentStatus { get; set; }
        public IList<StatusReason> StatusReasons { get; set; }
    }
}
