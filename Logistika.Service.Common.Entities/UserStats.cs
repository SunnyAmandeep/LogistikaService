using System;
using System.Collections.Generic;

namespace Logistika.Service.Common.Entities
{
    public class UserStats
    {
        public string UserType { get; set; }
        public int DriversOnDuty{ get; set; }
        public int TotalWaves { get; set; }
        public int CompletedWaves { get; set; }
    }
}
