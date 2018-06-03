
namespace Logistika.Service.Common.Entities.AOC.SearchEntity
{
    public class AOCPickSlipLineItem : BaseObject
    {
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string ShippedQuantity { get; set; }
        public string LotNumber { get; set; }
    }
}
