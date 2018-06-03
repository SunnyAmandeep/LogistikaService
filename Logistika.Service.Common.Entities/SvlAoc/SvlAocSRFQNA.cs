using System;

namespace Logistika.Service.Common.Entities.SvlAoc
{
    public class SvlAocSRFQNA
    {
        public string QuestionCode { get; set; }
        public int QuestionPK { get; set; }
        public int AnswerPk { get; set; }
        public string AnswerCode { get; set; }
        public string Answer { get; set; }
        public int AnswerSequence { get; set; }
        public string AnswerDescription { get; set; }
        public Boolean IsText { get; set; }
        public string AnswerTextDefault { get; set; }
        public Boolean AnswerIsDefault { get; set; }
        public Boolean IsDateFormat { get; set; }
        public string ImportFieldCode { get; set; }

        public string OtherOption { get; set; }
        public Boolean IsSelected { get; set; }
    }
}
