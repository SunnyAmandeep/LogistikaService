using System;

namespace Logistika.Service.Common.Entities.Practitioners
{
    public class WsSlnValidation
    {
        public int MedproWsSlnValidationLog_PK { get; set; }

        public string ShipToLicenseNumber { get; set; }
        public string ShipToLastName { get; set; }
        public string ShipToFirstName { get; set; }
        public string ShipToMiddleName { get; set; }
        public string ShipToLicenseState { get; set; }

        public string FormId { get; set; }
        public string ResponsedFirstName { get; set; }
        public string ResponsedLastName { get; set; }
        public string ResponsedLicense { get; set; }
        public string ResponsedLicenseState { get; set; }
        public string UserName { get; set; }
        public string WsConsumerCode { get; set; }
        public string WsConsumerName { get; set; }

        public string Source { get; set; }
        public string ResponsedOverallSampleabillity { get; set; }
        public string ResponsedReasonMessage { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
