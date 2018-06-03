using System.Collections.Generic;

namespace Logistika.Service.Common.Entities.Data
{
     
    public class Table
    {
        public string TableName { get; set; }
        public int Sequence { get; set; }
        public IList<string> Columns { get; set; }
        public IList<string> PK { get; set; }
    }
}
