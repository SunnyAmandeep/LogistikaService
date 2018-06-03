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

    public class AocResponse : BaseObject
    {
        //public AocResponse()
        //{             
        //}
    
        public int AOCResponse_PK { get; set; }
        public int SignedByType_FK { get; set; }
        public int BatchJob_FK { get; set; }
        public int FulfillmentJob_FK { get; set; }
        public string BatchJobNumber { get; set; }
        public string BatchNumber { get; set; }
        public System.DateTime? BatchDate { get; set; }
        public string LetterSeqId { get; set; }

        public Nullable<System.DateTime> OrderDate { get; set; }
        public string OrderID { get; set; }
        public string ClientOrderID { get; set; }
        public string PickSlip { get; set; }

        public string Client { get; set; }
        public string Division { get; set; }
        public string Project { get; set; }
        
        public string AOCResponseFormTemplateID { get; set; }
        public string ImagePath { get; set; }
        public string DateReceived { get; set; }

        public string PractitionerComments { get; set; }
        public string DataEntryComments { get; set; }

        public string DocumentImageFilePath { get; set; }

        public string StampedSignature { get; set; }
        public Nullable<System.DateTime> SignedDate { get; set; }
        public string SignedByFirstName { get; set; }
        public string SignedByLastName { get; set; }
        public string SignedByName { get; set; }
        public string SignedbyDesignation { get; set; }
        public string ElectronicSignatureString { get; set; }
        public string SignatureImage { get; set; }


        public string InvestigationResponse { get; set; }


        public Practitioner Practitioner { get; set; }
        public HealthCareEntityAddress ShipToAddress { get; set; }

        public virtual IList<AocResponseLineItem> AocResponseLineItem { get; set; }
        public virtual IList<AocResponseModification> AocResponseModification { get; set; }
        public virtual IList<AocResponseQna> AocResponseQna { get; set; }
        public virtual IList<AocResponseStatusHistory> AocResponseStatusHistory { get; set; }

        
    }
}
