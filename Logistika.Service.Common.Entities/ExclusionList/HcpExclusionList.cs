using System.ComponentModel.DataAnnotations;

namespace Logistika.Service.Common.Entities.ExclusionList
{
    public class ExcludedHcp
    {
        public long HCPExclusionList_PK { get; set; }
           [Required(ErrorMessage="HcpId is Required")]
           public string HCPID { get; set; }
           public string ClientName { get; set; }
           public int Client_FK { get; set; }
           public string ExclusionType { get; set; }
           public int ExclusionType_FK { get; set; }
           public string StateLicenseNumber { get;set; }
           public string DEALicenseNumber { get;set; }
           public string Firstname { get;set; }
           public string MiddleName { get;set; }
           public string LastName { get;set; }
           public string Designation { get;set; }
           public string Address1 { get;set; }
           public string Address2 { get;set; }
           public string Address3 { get;set; }
           public string City { get;set; }
           public string State { get;set; }
           public string Zip { get; set; }
           public bool IsActive { get; set; }
           public long OrderHeader_FK { get;set; }
           public string KnipperPersonID { get; set; }
           [Required(ErrorMessage = "CreatedBy is Required")]
           public string CreatedBy { get;set; }
           public string CreatedDt { get;set; }
           public string ModifiedBy { get;set; }
           public string ModifiedDt { get;set; }

    }
}
