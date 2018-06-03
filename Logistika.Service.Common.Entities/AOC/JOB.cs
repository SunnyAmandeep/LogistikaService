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
    
    public partial class JOB
    {
        public JOB()
        {
            this.AocResponse = new HashSet<AocResponse>();
            this.AocResponse1 = new HashSet<AocResponse>();
        }
    
        public int Job_PK { get; set; }
        public int Project_FK { get; set; }
        public string Number { get; set; }
        public string BillingJobNumber { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> StartDt { get; set; }
        public Nullable<System.DateTime> EndDt { get; set; }
        public string OMS { get; set; }
        public Nullable<System.DateTime> StatusDt { get; set; }
        public string Comments { get; set; }
        public int Status_FK { get; set; }
        public Nullable<int> AOCResponsePickslipSP_FK { get; set; }
        public int ProgramType_FK { get; set; }
        public bool IsKnipperFulfillment { get; set; }
        public Nullable<int> OrderEntryScreenFrameworkURL_FK { get; set; }
        public Nullable<int> DesigneeFormEntryScreenFrameworkURL_FK { get; set; }
        public Nullable<int> SignatureVerification_SRF_ScreenFrameworkURL_FK { get; set; }
        public Nullable<int> SignatureVerification_AOC_ScreenFrameworkURL_FK { get; set; }
        public Nullable<int> OrderValidationEngineSP_FK { get; set; }
        public string OMSOrderFileName { get; set; }
        public string OMSOrderFileDropFolderPath { get; set; }
        public string OMSOrderFileArchiveDropFolderPath { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDt { get; set; }
        public string SysUser { get; set; }
        public System.DateTime SysDt { get; set; }
        public string AuditAction { get; set; }
        public Nullable<int> OptOutHcplookupSP_FK { get; set; }
        public Nullable<bool> IsJobSplitApplicable { get; set; }
        public Nullable<int> SplitThreshold { get; set; }
        public Nullable<bool> IsSplitJob { get; set; }
    
        public virtual ICollection<AocResponse> AocResponse { get; set; }
        public virtual ICollection<AocResponse> AocResponse1 { get; set; }
    }
}
