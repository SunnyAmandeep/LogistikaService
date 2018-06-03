using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class SecurityQuestions
    {
        public int Sequence { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public int GroupID { get; set; }
        public bool IsIncluded { get; set; }
    }
}
