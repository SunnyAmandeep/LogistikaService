
namespace Logistika.Service.Common.Entities.Practitioners
{
    public class Preference
    {

        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string KId { get; set; }
        public string Scope { get; set; }


        public string RequestSourceType { get; set; }
        public string RequestSourceDetails { get; set; }
        public string RequestType { get; set; }

        public string ProjectName { get; set; }
        public int? ProjectId { get; set; }

        public string PractitionerFirstName { get; set; }
        public string PractitionerLastName { get; set; }
        public string CurrentStatus { get; set; }
        public string PractitionerId { get; set; }
        public string PractitionerEmail { get; set; }
        public string PractitionerFaxNumber { get; set; }
        public string PractitionerPhoneNumber { get; set; }
        public string OrderFormSequenceNumber { get; set; }
        public string StateLicenseNumber { get; set; }
        public string State { get; set; }

        public bool? IsMail { get; set; }
        public bool? IsFax { get; set; }
        public bool? IsEmail { get; set; }
        public bool? IsPhone { get; set; }
        public string CreatedBy { get; set; }

    }
}
