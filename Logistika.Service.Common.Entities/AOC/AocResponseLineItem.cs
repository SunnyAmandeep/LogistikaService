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

    public partial class AocResponseLineItem
    {
        public long AOCResponseLineItem_PK { get; set; }
        public int AOCResponse_FK { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string LotNumber { get; set; }
        public int ReceivedQuantity { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDt { get; set; } 

        public virtual AocResponse AocResponse { get; set; }
    }
}