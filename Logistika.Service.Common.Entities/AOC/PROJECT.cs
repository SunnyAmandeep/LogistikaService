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

    public partial class PROJECT
    {
        public int Project_PK { get; set; }
        public int Division_FK { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Coordinator { get; set; }
        public string Manager { get; set; }
        public Nullable<System.DateTime> StartDt { get; set; }
        public Nullable<System.DateTime> EndDt { get; set; }
        public Nullable<System.DateTime> StatusDt { get; set; }
        public string Comments { get; set; }
        public int Status_FK { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDt { get; set; }
        public string SysUser { get; set; }
        public System.DateTime SysDt { get; set; }
        public string AuditAction { get; set; }
    }
}
