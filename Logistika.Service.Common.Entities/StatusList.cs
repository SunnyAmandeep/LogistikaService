using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities
{
    public class StatusList
    {
        public int Status_PK { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ReportStatus { get; set; }
    }
}