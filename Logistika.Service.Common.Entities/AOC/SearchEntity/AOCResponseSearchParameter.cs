using Logistika.Service.Common.Entities.Enum;
using System;

namespace Logistika.Service.Common.Entities.AOC.SearchEntity
{
    public class AOCResponseSearchParameter: BaseObject
    {
        public string AocResposeId { get; set; }
        public string BatchNumber { get; set; }
        public string PickSlipNumber { get; set; }
        public string OrderId { get; set; }
        public string Client { get; set; }
        public string JobId { get; set; }
        public string AOCResponseStatus { get; set; }
        public string ShipToName { get; set; }
        //public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? BatchStartDt { get; set; }
        public DateTime? BatchEndDt { get; set; }
        public DateTime? ModifiedStartDt { get; set; }
        public DateTime? ModifiedEndDt { get; set; }

        public PageMode Mode { get; set; }
        public string SortExpression { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
