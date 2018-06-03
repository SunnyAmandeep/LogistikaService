using System;

namespace Logistika.Service.Report.Common.Entities.Teva
{
    public class ZipToTerritory
    {
        public string TerritoryNumber { get; set; }
        public string Zip { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string TerritoryName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
