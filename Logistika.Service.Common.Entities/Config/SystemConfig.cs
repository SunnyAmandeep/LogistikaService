using System;

namespace Logistika.Service.Common.Entities.Config
{
    public class SystemConfig
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDt { get; set; }
        public bool IsActive { get; set; } 
    }
}
