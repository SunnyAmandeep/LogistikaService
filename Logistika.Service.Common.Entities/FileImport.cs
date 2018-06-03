using System;

namespace Logistika.Service.Common.Entities
{
    public class FileImport
    {
        public string FileName { get; set; }
        public int ValidRecords { get; set; }
        public int InValidRecords { get; set; }
        public string Status { get; set; }
        public DateTime StatusDt { get; set; }
        public string LastModifiedBy { get; set; }
        public string ErrorFileName { get; set; }
    }
}
