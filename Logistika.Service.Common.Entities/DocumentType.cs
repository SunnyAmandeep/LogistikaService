using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class DocumentType
    {
        public int DocumentType_PK { get; set; }
        public string AccountType { get; set; }
        public string DType { get; set; }
        public string IssuingAuthority { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
    }
}
