//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Logistika.Service.Common.Entities.AOC
{
    #pragma warning disable 1573
    using System;
    using System.Collections.Generic;
    
    public partial class AocResponseStatusHistory
    {
    
        public int AOCResponseStatusHistory_PK { get; set; }
        public int AOCResponse_FK { get; set; }
        public int Status_FK { get; set; }
        public System.DateTime StatusDt { get; set; }
        public bool CurrentStatus { get; set; }
        public Nullable<int> ExportHistory_FK { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDt { get; set; }

        public virtual AocResponse AocResponse { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<AocResponseStatusReason> AocResponseStatusReason { get; set; }
    }
}
