using System;

namespace Logistika.Service.Common.Entities.Lookup
{
   public class SignatureVerficationModificationType
    {
        public int SVLSRFAOCVerificationQuestionTypePK { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Boolean IsPositiveResponse { get; set; }
        public int Sequence { get; set; }
    }
}
