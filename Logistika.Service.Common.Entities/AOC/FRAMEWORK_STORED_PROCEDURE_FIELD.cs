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

    public partial class FRAMEWORK_STORED_PROCEDURE_FIELD
    {
        public int FrameworkStoredProcedureField_PK { get; set; }
        public int FrameworkStoredProcedure_FK { get; set; }
        public string BindingName { get; set; }
        public string Description { get; set; }
        public int DataType_FK { get; set; }
        public string Direction { get; set; }
        public bool IsRequired { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDt { get; set; }
        public string SysUser { get; set; }
        public System.DateTime SysDt { get; set; }
        public string AuditAction { get; set; }
    }
}