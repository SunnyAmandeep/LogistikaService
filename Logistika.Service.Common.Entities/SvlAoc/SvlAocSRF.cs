using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities.SvlAoc
{
    public class SvlAocSRF
    {
        public string Id { get; set; }
        public string BatchDate { get; set; }
        public string BatchNumber { get; set; }
        public string BatchJobNumber { get; set; }
        public string OrderDate { get; set; }
        public string HCPId { get; set; }
        public string ShipToTitle { get; set; }
        public string ShipToFirstName { get; set; }
        public string ShipToMiddleName { get; set; }
        public string ShipToLastName { get; set; }
        public string ShipToSuffix { get; set; }
        public string ShipToAddress1 { get; set; }
        public string ShipToAddress2 { get; set; }
        public string ShipToAddress3 { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToState { get; set; }
        public string ShipToZip { get; set; }
        public string ShipToCountry { get; set; }
        public string ShipToName { get; set; }
        public string ShipToCompanyName { get; set; }
        public string ShipToAttentionTo { get; set; }
        public string ShipToDesignation { get; set; }
        public string SignatureVerificationQNAVerificationType { get; set; }
        public string FlagCheck { get; set; }
        public string SignedByType { get; set; }
        public string SignatureStamped { get; set; }
        public string SignedByName { get; set; }
        public string DateSigned { get; set; }
        public string DataEntryComments { get; set; }
        public string PractitionerComments { get; set; }
        public string TemplateID { get; set; }
        public string LetterSequence { get; set; }
        public string OrderId { get; set; }
        public string ClientOrderId { get; set; }
        public string FulfillmentJobNumber { get; set; }
        public string ModificationTypes { get; set; }
        public List<SvlAocSRFQNA> QNAAnswerList { get; set; }
        public List<AOCPickSlipLineItem> PickSlipLineItems { get; set; }

        //Additional Properties
        public string ImagePath { get; set; }
        public string Message { get; set; }
        public string PendingInvestigation { get; set; }
        public string NoOptionSelected { get; set; }

        //AOC Response Entry Additional Properties
        public string SignedByDesignation { get; set; }
        public string DateReceived { get; set; }
        public string PickSlip { get; set; }

        //OCRDocumentImagePath
        public string OCRHoldQueueDocumentPK { get; set; }
        public string OCRDocumentImagePath { get; set; }
        public string DocumentImageFilePathToSave { get; set; }
        public Boolean IsOCRFlowEntry { get; set; }
        public string ClientName { get; set; }

        public string SignedByFirstName { get; set; }   
        public string SignedByLastName { get; set; }    
        public string SignedByMiddleName { get; set; }  
    }
}
