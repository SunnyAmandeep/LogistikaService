using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistika.Service.Common.Entities
{
    public class Fleet
    {
        public int?   FleetMasterID { get; set; }
        public int    CompanyAddressID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string OverallLength { get; set; }
        public string OverallWidth { get; set; }
        public string OverallHeight { get; set; }
        public string Payload { get; set; }
        public string Colour { get; set; }
        public int?   ModelYear { get; set; }
        public string OwnerShipType { get; set; }
        public int?   StartingMileage { get; set; }
        public string FleetID { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDt { get; set; }
        public bool IsActive { get; set; }
        public string FleetGuid { get; set; }
    }
    public class FleetModal
    {
        public Fleet Fleet { get; set; }
        public IList<Document> Documents { get; set; }
        public IList<Fleet> Fleets { get; set; }
        public string CallType { get; set; }
    }
}