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

    public partial class FRAMEWORK_STORED_PROCEDURE_CLIENT
    {
        public int FrameworkStoredProcedureClient_PK { get; set; }
        public int FrameworkStoredProcedure_FK { get; set; }
        public int Client_FK { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDt { get; set; }
        public string Sysuser { get; set; }
        public System.DateTime SysDt { get; set; }
        public string AuditAction { get; set; }
    }
}
