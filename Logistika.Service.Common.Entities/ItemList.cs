using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class ItemList
    {
        public int ItemTypeID { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string GoodsType { get; set; }
        public string ShipmentType { get; set; }
        public string UnitOfMeasure { get; set; }
        public bool IsTaxableItem { get; set; }
    }
}

