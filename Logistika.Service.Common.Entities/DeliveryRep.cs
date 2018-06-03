using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities
{
    public class DeliveryRep
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePic { get; set; }
        public double Longitutue { get; set; }
        public double Latitude { get; set; }
        public string PhoneNumber { get; set; }
        public int Company_PK { get; set; }
        public DateTime LastknownDt { get; set; }
        public int FrameApplicationUserID { get; set; }
        public string Speed { get; set; }
    }
}