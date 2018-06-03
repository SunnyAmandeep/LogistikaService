//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Logistika.Service.Common.Entities.Authentication
{
    #pragma warning disable 1573
    using Logistika.Service.Common.Entities.CorsConfig;
    using System.Collections.Generic;
    
    public partial class WsConsumer
    {
        public WsConsumer()
        {
            this.WsCredential = new HashSet<WsCredential>();
            this.WsApiCorsConfig = new HashSet<WsApiCorsConfig>();
        }
    
        public int Consumer_PK { get; set; }
        public string ConsumerCode { get; set; }
        public string ConsumerName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDt { get; set; }
        public string SysUser { get; set; }
        public System.DateTime SysDt { get; set; }
        public string AuditAction { get; set; }

        public virtual ICollection<WsCredential> WsCredential { get; set; }
        public virtual ICollection<WsApiCorsConfig> WsApiCorsConfig { get; set; }
    }
}