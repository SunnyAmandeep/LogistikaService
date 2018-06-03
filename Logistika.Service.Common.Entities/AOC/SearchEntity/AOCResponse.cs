using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities.AOC.SearchEntity
{
    public class AOCResponse
    {

        public int AOCResponseID { get; set; }
        public string PickSlipNumber { get; set; }
        public string BatchNumber { get; set; }
        public DateTime BatchDate { get; set; }
        public string ClientOrderID { get; set; }

        public string KnipperOrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public string AOCResponseStatus { get; set; }
        public DateTime? StatusDate { get; set; }
        public string PractitionerComments { get; set; }

        public string DataEntryComments { get; set; }
        public string InvestigationFindings { get; set; }
        public string AOCResponseModifications { get; set; }
        public string AOCResponseStatusReasons { get; set; }
        public string ShipToAttention { get; set; }

        public string ShipToName { get; set; }
        public string ShipToAddress1 { get; set; }
        public string ShipToAddress2 { get; set; }
        public string ShipToAddress3 { get; set; }
        public string ShipToCity { get; set; }

        public string ShipToState { get; set; }
        public string ShipToZip { get; set; }
        public string ShipToCountry { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }

        public string DivisionID { get; set; }
        public string DivisionName { get; set; }
        public string ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string FulfillmentJobID { get; set; }

        public string BatchJobID { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }

        public string SignedbyDesignation { get; set; }
        public string DateReceived { get; set; }
        public string SignedByName { get; set; }

        public string LetterSequence { get; set; }
        public string SignedByType { get; set; }
        public DateTime? DateSigned { get; set; }
        public String SignatureStamped { get; set; }
        public string ImagePath { get; set; }

        public string ModificationTypes { get; set; }
        public string ShipToCompanyName { get; set; }
        public string ShipToTitle { get; set; }
        public string ShipToFirstName { get; set; }
        public string ShipToMiddleName { get; set; }

        public string ShipToLastName { get; set; }
        public string ShipToSuffix { get; set; }
        public string HCPId { get; set; }
        public string AOCResponseFormTemplateID { get; set; }
        public string ShipToDesignation { get; set; }
        public string SignedByFirstName { get; set; }
        public string SignedByLastName { get; set; }
        public string SignedByMiddleName { get; set; }
        public string Message { get; set; }
        public List<QNA> QNAList { get; set; }
        public List<AOCPickSlipLineItem> PickSlipLineItems { get; set; }
 
    }
}
